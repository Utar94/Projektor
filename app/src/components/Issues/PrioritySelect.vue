<template>
  <form-select :id="id" :label="label" :options="options" :placeholder="placeholder" :required="required" :value="value" @input="$emit('input', $event)" />
</template>

<script>
import Priority from '@/enums/priority'

export default {
  props: {
    id: {
      type: String,
      default: 'priority'
    },
    label: {
      type: String,
      default: 'issue.priority.label'
    },
    placeholder: {
      type: String,
      default: 'issue.priority.placeholder'
    },
    required: {
      type: Boolean,
      default: false
    },
    value: {}
  },
  computed: {
    options() {
      return Object.keys(Priority).map(value => ({
        text: this.$i18n.t(`issue.priorities.${value}`),
        value
      }))
    }
  }
}
</script>
