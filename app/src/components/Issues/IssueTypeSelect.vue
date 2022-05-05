<template>
  <form-select :id="id" :label="label" :options="options" :placeholder="placeholder" :required="required" :value="value" @input="$emit('input', $event)" />
</template>

<script>
import { getIssueTypes } from '@/api/issueTypes'

export default {
  props: {
    id: {
      type: String,
      default: 'issueTypeId'
    },
    label: {
      type: String,
      default: 'issueType.label'
    },
    placeholder: {
      type: String,
      default: 'issueType.placeholder'
    },
    projectId: {
      type: String,
      required: true
    },
    required: {
      type: Boolean,
      default: false
    },
    value: {}
  },
  data: () => ({
    items: []
  }),
  computed: {
    options() {
      return this.items.map(({ id, name }) => ({
        text: name,
        value: id
      }))
    }
  },
  watch: {
    projectId: {
      immediate: true,
      async handler(value) {
        if (value) {
          try {
            const { data } = await getIssueTypes({
              deleted: false,
              projectId: this.projectId,
              sort: 'Name'
            })
            this.items = data.items
          } catch (e) {
            this.handleError(e)
          }
        }
      }
    }
  }
}
</script>
