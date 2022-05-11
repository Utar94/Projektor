import { get, post, put } from '.'

export async function createIssue({ description, dueDate, estimate, name, priority, score, typeId }) {
  return await post('/issues', { description, dueDate, estimate, name, priority, score, typeId })
}

export async function getIssue(id) {
  return await get(`/issues/${id}`)
}

export async function getIssues({ deleted, priority, projectId, search, typeId, sort, desc, index, count }) {
  const query = [
    ['deleted', deleted],
    ['priority', priority],
    ['projectId', projectId],
    ['search', search],
    ['typeId', typeId],
    ['sort', sort],
    ['desc', desc],
    ['index', index],
    ['count', count]
  ]
    .filter(([, value]) => typeof value !== 'undefined' && value !== null)
    .map(pair => pair.join('='))
    .join('&')

  return await get(`/issues${query ? '?' + query : ''}`)
}

export async function updateIssue(id, { description, dueDate, estimate, name, priority, score }) {
  return await put(`/issues/${id}`, { description, dueDate, estimate, name, priority, score })
}
