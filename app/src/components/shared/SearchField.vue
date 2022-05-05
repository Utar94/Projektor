<template>
  <form-field :id="id" :label="label" :placeholder="placeholder" :value="value" @input="$emit('input', $event)" />
</template>

<script>
export default {
  props: {
    id: {
      type: String,
      default: 'search'
    },
    label: {
      type: String,
      default: 'search.label'
    },
    placeholder: {
      type: String,
      default: 'search.placeholder'
    },
    value: {}
  }
}
</script>
