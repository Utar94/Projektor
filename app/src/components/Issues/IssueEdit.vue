<template>
  <b-container>
    <template v-if="issue">
      <h1 v-text="issue.name" />
      <validation-observer ref="form">
        <b-form @submit.prevent="submit">
          <div class="my-2">
            <icon-submit class="mx-1" :disabled="loading || !hasChanges" icon="save" :loading="loading" text="actions.save" variant="primary" />
            <icon-button class="mx-1" icon="ban" text="actions.cancel" :to="{ name: 'IssueList' }" variant="secondary" />
          </div>
          <form-field id="name" label="name.label" :maxLength="100" placeholder="name.placeholder" required v-model="name" />
          <b-row>
            <form-datetime class="col" id="dueDate" label="issue.dueDate" v-model="dueDate" />
            <form-field class="col" id="estimate" label="issue.estimate" :minValue="0" :step="1" type="number" v-model.number="estimate" />
            <form-field class="col" id="score" label="issue.score" :minValue="0" :step="0.1" type="number" v-model.number="score" />
          </b-row>
          <form-textarea id="description" label="description.label" placeholder="description.placeholder" v-model="description" />
        </b-form>
      </validation-observer>
    </template>
  </b-container>
</template>

<script>
import { getIssue, updateIssue } from '@/api/issues'

export default {
  data: () => ({
    description: null,
    dueDate: null,
    estimate: null,
    issue: null,
    loading: false,
    name: null,
    score: null
  }),
  computed: {
    hasChanges() {
      return (
        this.name !== this.issue.name ||
        (this.description ?? '') !== (this.issue.description ?? '') ||
        (this.dueDate ?? '') !== (this.issue.dueDate ?? '') ||
        (this.estimate ?? 0) !== (this.issue.estimate ?? 0) ||
        (this.score ?? 0) !== (this.issue.score ?? 0)
      )
    }
  },
  methods: {
    setModel(model) {
      this.issue = model
      this.description = model.description
      this.dueDate = model.dueDate
      this.estimate = model.estimate
      this.name = model.name
      this.score = model.score
    },
    async submit() {
      if (!this.loading) {
        this.loading = true
        try {
          if (await this.$refs.form.validate()) {
            const { data } = await updateIssue(this.issue.id, {
              description: this.description,
              dueDate: this.dueDate,
              estimate: this.estimate,
              name: this.name,
              score: this.score
            })
            this.setModel(data)
            this.$refs.form.reset()
            this.toast('success', 'issue.saved')
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
      const { data } = await getIssue(this.$route.params.key)
      this.setModel(data)
    } catch (e) {
      this.handleError(e)
    }
  }
}
</script>
