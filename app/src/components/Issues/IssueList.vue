<template>
  <b-container>
    <h1 v-t="'issues.title'" />
    <div class="my-2">
      <icon-button class="mx-1" :disabled="loading" icon="sync-alt" :loading="loading" text="actions.refresh" variant="primary" @click="refresh()" />
      <icon-button class="mx-1" icon="plus" text="actions.create" variant="success" v-b-modal.createIssue />
      <create-issue-modal />
    </div>
    <b-row>
      <search-field class="col" v-model="search" />
      <project-select class="col" v-model="projectId" />
      <issue-type-select class="col" :projectId="projectId" v-model="typeId" />
    </b-row>
    <b-row>
      <priority-select class="col" v-model="priority" />
      <sort-select class="col" :desc="desc" :options="sortOptions" v-model="sort" @desc="desc = $event" />
      <count-select class="col" v-model="count" />
    </b-row>
    <template v-if="items.length">
      <table class="table table-striped" id="items">
        <thead>
          <tr>
            <th scope="col" v-t="'issue.key'" />
            <th scope="col" v-t="'name.label'" />
            <th scope="col" v-t="'issueType.label'" />
            <th scope="col" v-t="'issue.priority.label'" />
            <th scope="col" v-t="'updatedAt'" />
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>
              <router-link :to="{ name: 'IssueEdit', params: { key: item.key } }">{{ item.key }}</router-link>
            </td>
            <td v-text="item.name" />
            <td v-text="item.type.name" />
            <td v-text="$t(`issue.priorities.${item.priority}`)" />
            <td v-text="$d(new Date(item.updatedAt || item.createdAt), 'medium')" />
          </tr>
        </tbody>
      </table>
      <b-pagination :total-rows="total" :per-page="count" v-model="page" aria-controls="items" />
    </template>
    <p v-else v-t="'noResult'" />
  </b-container>
</template>

<script>
import CreateIssueModal from './CreateIssueModal.vue'
import IssueTypeSelect from './IssueTypeSelect.vue'
import PrioritySelect from './PrioritySelect.vue'
import ProjectSelect from './ProjectSelect.vue'
import { getIssues } from '@/api/issues'

export default {
  components: {
    CreateIssueModal,
    IssueTypeSelect,
    PrioritySelect,
    ProjectSelect
  },
  data: () => ({
    count: 10,
    desc: false,
    items: [],
    loading: false,
    page: 1,
    priority: null,
    projectId: null,
    search: null,
    sort: null,
    total: 0,
    typeId: null
  }),
  computed: {
    params() {
      return {
        deleted: false,
        priority: this.priority,
        projectId: this.projectId,
        search: this.search,
        typeId: this.projectId ? this.typeId : null,
        sort: this.sort,
        desc: this.sort === null ? null : this.desc,
        index: (this.page - 1) * this.count,
        count: this.count
      }
    },
    sortOptions() {
      return Object.entries(this.$t('issues.sort'))
        .map(([value, text]) => ({
          text,
          value
        }))
        .sort((a, b) => (a < b ? -1 : a > b ? 1 : 0))
    }
  },
  methods: {
    async refresh(params = null) {
      if (!this.loading) {
        this.loading = true
        try {
          const { data } = await getIssues(params ?? this.params)
          this.items = data.items
          this.total = data.total
        } catch (e) {
          this.handleError(e)
        } finally {
          this.loading = false
        }
      }
    }
  },
  watch: {
    count(newValue, oldValue) {
      if (newValue !== oldValue) {
        this.page = 1
      }
    },
    params: {
      deep: true,
      immediate: true,
      async handler(params) {
        await this.refresh(params)
      }
    },
    projectId(newValue, oldValue) {
      // TODO(fpion): bug, changed after params has executed
      if (newValue !== oldValue) {
        this.typeId = null
      }
    },
    sort(value) {
      if (value === null) {
        this.desc = false
      }
    }
  }
}
</script>
