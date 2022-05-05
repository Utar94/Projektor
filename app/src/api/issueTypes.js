import { get, post, put } from '.'

export async function createIssueType({ description, name, projectId }) {
  return await post('/issues/types', { description, name, projectId })
}

export async function getIssueType(id) {
  return await get(`/issues/types/${id}`)
}

export async function getIssueTypes({ deleted, projectId, search, sort, desc, index, count }) {
  const query = [
    ['deleted', deleted],
    ['projectId', projectId],
    ['search', search],
    ['sort', sort],
    ['desc', desc],
    ['index', index],
    ['count', count]
  ]
    .filter(([, value]) => typeof value !== 'undefined' && value !== null)
    .map(pair => pair.join('='))
    .join('&')

  return await get(`/issues/types${query ? '?' + query : ''}`)
}

export async function updateIssueType(id, { description, name }) {
  return await put(`/issues/types/${id}`, { description, name })
}
