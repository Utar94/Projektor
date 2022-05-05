import { get, post, put } from '.'

export async function createProject({ description, key, name }) {
  return await post('/projects', { description, key, name })
}

export async function getProject(id) {
  return await get(`/projects/${id}`)
}

export async function getProjects({ deleted, search, sort, desc, index, count }) {
  const query = [
    ['deleted', deleted],
    ['search', search],
    ['sort', sort],
    ['desc', desc],
    ['index', index],
    ['count', count]
  ]
    .filter(([, value]) => typeof value !== 'undefined' && value !== null)
    .map(pair => pair.join('='))
    .join('&')

  return await get(`/projects${query ? '?' + query : ''}`)
}

export async function updateProject(id, { description, name }) {
  return await put(`/projects/${id}`, { description, name })
}
