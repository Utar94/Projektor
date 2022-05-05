<template>
  <form-select :id="id" :label="label" :options="options" :placeholder="placeholder" :required="required" :value="value" @input="$emit('input', $event)" />
</template>

<script>
import { getProjects } from '@/api/projects'

export default {
  props: {
    id: {
      type: String,
      default: 'projectId'
    },
    label: {
      type: String,
      default: 'project.label'
    },
    placeholder: {
      type: String,
      default: 'project.placeholder'
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
  async created() {
    try {
      const { data } = await getProjects({
        deleted: false,
        sort: 'Name'
      })
      this.items = data.items
    } catch (e) {
      this.handleError(e)
    }
  }
}
</script>
