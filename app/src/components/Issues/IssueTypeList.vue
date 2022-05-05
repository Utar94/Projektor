<template>
  <b-container>
    <h1 v-t="'issueTypes.title'" />
    <div class="my-2">
      <icon-button class="mx-1" :disabled="loading" icon="sync-alt" :loading="loading" text="actions.refresh" variant="primary" @click="refresh()" />
      <icon-button class="mx-1" icon="plus" text="actions.create" variant="success" v-b-modal.createIssueType />
      <create-issue-type-modal />
    </div>
    <b-row>
      <search-field class="col" v-model="search" />
      <project-select class="col" v-model="projectId" />
    </b-row>
    <b-row>
      <sort-select class="col" :desc="desc" :options="sortOptions" v-model="sort" @desc="desc = $event" />
      <count-select class="col" v-model="count" />
    </b-row>
    <template v-if="items.length">
      <table class="table table-striped" id="items">
        <thead>
          <tr>
            <th scope="col" v-t="'name.label'" />
            <th scope="col" v-t="'project.label'" />
            <th scope="col" v-t="'updatedAt'" />
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>
              <router-link :to="{ name: 'IssueTypeEdit', params: { id: item.id } }">{{ item.name }}</router-link>
            </td>
            <td>
              <router-link :to="{ name: 'ProjectEdit', params: { key: item.project.key } }">{{ item.project.name }}</router-link>
            </td>
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
import CreateIssueTypeModal from './CreateIssueTypeModal.vue'
import ProjectSelect from './ProjectSelect.vue'
import { getIssueTypes } from '@/api/issueTypes'

export default {
  components: {
    CreateIssueTypeModal,
    ProjectSelect
  },
  data: () => ({
    count: 10,
    desc: false,
    items: [],
    loading: false,
    page: 1,
    projectId: null,
    search: null,
    sort: null,
    total: 0
  }),
  computed: {
    params() {
      return {
        deleted: false,
        projectId: this.projectId,
        search: this.search,
        sort: this.sort,
        desc: this.sort === null ? null : this.desc,
        index: (this.page - 1) * this.count,
        count: this.count
      }
    },
    sortOptions() {
      return Object.entries(this.$t('issueTypes.sort'))
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
          const { data } = await getIssueTypes(params ?? this.params)
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
    sort(value) {
      if (value === null) {
        this.desc = false
      }
    }
  }
}
</script>
