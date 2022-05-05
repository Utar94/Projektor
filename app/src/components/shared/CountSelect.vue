<template>
  <form-select :id="id" :label="label" :options="countOptions" :placeholder="placeholder" :value="value" @input="$emit('input', $event)" />
</template>

<script>
export default {
  props: {
    id: {
      type: String,
      default: 'count'
    },
    label: {
      type: String,
      default: 'count'
    },
    options: {
      type: Array,
      default: () => [10, 25, 50, 100]
    },
    placeholder: {
      type: String,
      default: ''
    },
    value: {}
  },
  computed: {
    countOptions() {
      return this.options.map(value => ({
        text: value.toString(),
        value
      }))
    }
  }
}
</script>
