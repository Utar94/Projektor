<template>
  <form-field
    id="username"
    label="user.emailOrUsername.label"
    placeholder="user.emailOrUsername.placeholder"
    ref="username"
    :rules="{ required: true }"
    :value="value"
    @input="$emit('input', $event)"
  />
</template>

<script>
export default {
  props: {
    value: {}
  },
  methods: {
    focus() {
      this.$refs.username.focus()
    }
  }
}
</script>
