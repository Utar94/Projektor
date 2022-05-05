<template>
  <create-modal :disabled="loading" :id="id" :loading="loading" title="issueTypes.new" @hidden="reset" @ok="submit">
    <validation-observer ref="form">
      <b-form @submit.prevent="submit">
        <project-select required v-model="projectId" />
        <form-field id="name" label="name.label" :maxLength="100" placeholder="name.placeholder" required v-model="name" />
      </b-form>
    </validation-observer>
  </create-modal>
</template>

<script>
import ProjectSelect from './ProjectSelect.vue'
import { createIssueType } from '@/api/issueTypes'

export default {
  components: {
    ProjectSelect
  },
  props: {
    id: {
      type: String,
      default: 'createIssueType'
    }
  },
  data: () => ({
    loading: false,
    name: null,
    projectId: null
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
            const { data } = await createIssueType({
              name: this.name,
              projectId: this.projectId
            })
            this.$router.push({ name: 'IssueTypeEdit', params: { id: data.id } })
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
