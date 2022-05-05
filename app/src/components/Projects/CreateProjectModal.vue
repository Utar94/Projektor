<template>
  <create-modal :disabled="loading" :id="id" :loading="loading" title="projects.new" @hidden="reset" @ok="submit">
    <validation-observer ref="form">
      <b-form @submit.prevent="submit">
        <form-field id="name" label="name.label" :maxLength="100" placeholder="name.placeholder" required v-model="name" />
        <project-key-field :name="name || ''" v-model="key" />
      </b-form>
    </validation-observer>
  </create-modal>
</template>

<script>
import ProjectKeyField from './ProjectKeyField.vue'
import { createProject } from '@/api/projects'

export default {
  components: {
    ProjectKeyField
  },
  props: {
    id: {
      type: String,
      default: 'createProject'
    }
  },
  data: () => ({
    key: null,
    loading: false,
    name: null
  }),
  methods: {
    reset() {
      this.key = null
      this.name = null
    },
    async submit() {
      if (!this.loading) {
        this.loading = true
        try {
          if (await this.$refs.form.validate()) {
            const { data } = await createProject({
              name: this.name,
              key: this.key
            })
            this.$router.push({ name: 'ProjectEdit', params: { key: data.key } })
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
