<template>
  <b-container>
    <template v-if="issueType">
      <h1 v-text="issueType.name" />
      <validation-observer ref="form">
        <b-form @submit.prevent="submit">
          <div class="my-2">
            <icon-submit class="mx-1" :disabled="loading || !hasChanges" icon="save" :loading="loading" text="actions.save" variant="primary" />
            <icon-button class="mx-1" icon="ban" text="actions.cancel" :to="{ name: 'IssueTypeList' }" variant="secondary" />
          </div>
          <form-field id="name" label="name.label" :maxLength="100" placeholder="name.placeholder" required v-model="name" />
          <form-textarea id="description" label="description.label" placeholder="description.placeholder" v-model="description" />
        </b-form>
      </validation-observer>
    </template>
  </b-container>
</template>

<script>
import { getIssueType, updateIssueType } from '@/api/issueTypes'

export default {
  data: () => ({
    description: null,
    issueType: null,
    loading: false,
    name: null
  }),
  computed: {
    hasChanges() {
      return this.name !== this.issueType.name || (this.description ?? '') !== (this.issueType.description ?? '')
    }
  },
  methods: {
    setModel(model) {
      this.issueType = model
      this.description = model.description
      this.name = model.name
    },
    async submit() {
      if (!this.loading) {
        this.loading = true
        try {
          if (await this.$refs.form.validate()) {
            const { data } = await updateIssueType(this.issueType.id, {
              description: this.description,
              name: this.name
            })
            this.setModel(data)
            this.$refs.form.reset()
            this.toast('success', 'issueType.saved')
          }
        } catch (e) {
          this.handleError(e)
        } finally {
          this.loading = false
        }
      }
    }
  },
  async created() {
    try {
      const { data } = await getIssueType(this.$route.params.id)
      this.setModel(data)
    } catch (e) {
      this.handleError(e)
    }
  }
}
</script>
