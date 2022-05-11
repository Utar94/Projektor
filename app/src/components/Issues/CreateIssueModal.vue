<template>
  <create-modal :disabled="loading" :id="id" :loading="loading" title="issues.new" @hidden="reset" @ok="submit">
    <validation-observer ref="form">
      <b-form @submit.prevent="submit">
        <project-select required v-model="projectId" />
        <issue-type-select :projectId="projectId" required v-model="typeId" />
        <form-field id="name" label="name.label" :maxLength="100" placeholder="name.placeholder" required v-model="name" />
        <priority-select required v-model="priority" />
      </b-form>
    </validation-observer>
  </create-modal>
</template>

<script>
import IssueTypeSelect from './IssueTypeSelect.vue'
import PrioritySelect from './PrioritySelect.vue'
import ProjectSelect from './ProjectSelect.vue'
import { createIssue } from '@/api/issues'

export default {
  components: {
    IssueTypeSelect,
    PrioritySelect,
    ProjectSelect
  },
  props: {
    id: {
      type: String,
      default: 'createIssue'
    }
  },
  data: () => ({
    loading: false,
    name: null,
    priority: 'Medium',
    projectId: null,
    typeId: null
  }),
  methods: {
    reset() {
      this.name = null
    },
    async submit() {
      if (!this.loading) {
        this.loading = true
        try {
          if (await this.$refs.form.validate()) {
            const { data } = await createIssue({
              name: this.name,
              priority: this.priority,
              typeId: this.typeId
            })
            this.$router.push({ name: 'IssueEdit', params: { key: data.key } })
          }
        } catch (e) {
          this.handleError(e)
        } finally {
          this.loading = false
        }
      }
    }
  }
}
</script>
