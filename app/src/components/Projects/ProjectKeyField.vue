<template>
  <form-field
    :disabled="!custom"
    :id="id"
    :label="label"
    :maxLength="12"
    :placeholder="placeholder"
    required
    :rules="{ alpha_num: true }"
    :value="value"
    @input="$emit('input', $event)"
  >
    <b-form-checkbox v-model="custom">{{ $t('project.key.custom') }}</b-form-checkbox>
  </form-field>
</template>

<script>
import { isLetterOrDigit } from '@/helpers/stringUtils'

export default {
  props: {
    id: {
      type: String,
      default: 'key'
    },
    label: {
      type: String,
      default: 'project.key.label'
    },
    name: {
      type: String,
      required: true
    },
    placeholder: {
      type: String,
      default: 'project.key.placeholder'
    },
    value: {}
  },
  data: () => ({
    custom: false
  }),
  methods: {
    getKey(value) {
      return value
        .split('')
        .filter(c => isLetterOrDigit(c))
        .join('')
        .toUpperCase()
    }
  },
  watch: {
    custom(newValue, oldValue) {
      if (newValue !== oldValue && !newValue) {
        this.$emit('input', this.getKey(this.name))
      }
    },
    name(value) {
      if (!this.custom) {
        this.$emit('input', this.getKey(value))
      }
    }
  }
}
</script>
