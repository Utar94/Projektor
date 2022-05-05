<template>
  <validation-provider :name="$t(label).toLowerCase()" :rules="allRules" :vid="id" v-slot="validationContext" slim>
    <b-form-group :label="required ? '' : $t(label)" :label-for="id" :invalid-feedback="validationContext.errors[0]">
      <template #label v-if="required"><span class="text-danger">*</span> {{ $t(label) }}</template>
      <b-form-input
        :disabled="disabled"
        :id="id"
        :max="max"
        :min="min"
        :ref="id"
        :state="hasRules ? getValidationState(validationContext) : null"
        type="datetime-local"
        :value="formattedValue"
        @input="$emit('input', $event ? new Date($event) : null)"
      />
      <slot />
    </b-form-group>
  </validation-provider>
</template>

<script>
import { v4 as uuidv4 } from 'uuid'

export default {
  props: {
    disabled: {
      type: Boolean,
      default: false
    },
    id: {
      type: String,
      default: () => uuidv4()
    },
    label: {
      type: String,
      default: ''
    },
    maxDate: {
      type: Date,
      default: () => new Date(9999, 12, 31, 23, 59, 59)
    },
    minDate: {},
    required: {
      type: Boolean,
      default: false
    },
    rules: {
      type: Object,
      default: null
    },
    value: {}
  },
  computed: {
    allRules() {
      const rules = this.rules ?? {}
      if (this.required) {
        rules.required = true
      }
      return rules
    },
    formattedValue() {
      if (typeof this.value === 'undefined' || this.value === null) {
        return null
      }
      return this.getDatetimeLocal(this.value)
    },
    hasRules() {
      return Object.keys(this.allRules).length
    },
    max() {
      if (typeof this.maxDate === 'undefined' || this.maxDate === null) {
        return null
      }
      return this.getDatetimeLocal(this.maxDate)
    },
    min() {
      if (typeof this.minDate === 'undefined' || this.minDate === null) {
        return null
      }
      return this.getDatetimeLocal(this.minDate)
    }
  },
  methods: {
    getDatetimeLocal(value) {
      const instance = value instanceof Date ? value : new Date(value)
      const date = [instance.getFullYear(), (instance.getMonth() + 1).toString().padStart(2, '0'), instance.getDate().toString().padStart(2, '0')].join('-')
      const time = [instance.getHours().toString().padStart(2, '0'), instance.getMinutes().toString().padStart(2, '0')].join(':')
      return [date, time].join('T')
    }
  }
}
</script>
