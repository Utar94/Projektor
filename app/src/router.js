import Vue from 'vue'
import VueRouter from 'vue-router'
import store from './store'
import Confirm from './components/Identity/Confirm.vue'
import Home from './components/Home.vue'
import IssueEdit from './components/Issues/IssueEdit.vue'
import IssueList from './components/Issues/IssueList.vue'
import IssueTypeEdit from './components/Issues/IssueTypeEdit.vue'
import IssueTypeList from './components/Issues/IssueTypeList.vue'
import NotFound from './components/NotFound.vue'
import Profile from './components/Identity/Profile.vue'
import ProjectEdit from './components/Projects/ProjectEdit.vue'
import ProjectList from './components/Projects/ProjectList.vue'
import RecoverPassword from './components/Identity/RecoverPassword.vue'
import ResetPassword from './components/Identity/ResetPassword.vue'
import SignIn from './components/Identity/SignIn.vue'
import SignUp from './components/Identity/SignUp.vue'

Vue.use(VueRouter)

const router = new VueRouter({
  mode: 'history',
  routes: [
    // Global
    {
      name: 'Home',
      path: '/',
      component: Home,
      meta: { public: true }
    },
    {
      name: 'NotFound',
      path: '/404',
      component: NotFound,
      meta: { public: true }
    },
    // Identity
    {
      name: 'Confirm',
      path: '/user/confirm',
      component: Confirm,
      meta: { public: true }
    },
    {
      name: 'Profile',
      path: '/user/profile',
      component: Profile
    },
    {
      name: 'RecoverPassword',
      path: '/user/recover-password',
      component: RecoverPassword,
      meta: { public: true }
    },
    {
      name: 'ResetPassword',
      path: '/user/reset-password',
      component: ResetPassword,
      meta: { public: true }
    },
    {
      name: 'SignIn',
      path: '/user/sign-in',
      component: SignIn,
      meta: { public: true }
    },
    {
      name: 'SignUp',
      path: '/user/sign-up',
      component: SignUp,
      meta: { public: true }
    },
    // Projects
    {
      name: 'ProjectEdit',
      path: '/projects/:key',
      component: ProjectEdit
    },
    {
      name: 'ProjectList',
      path: '/projects',
      component: ProjectList
    },
    // Issue Types
    {
      name: 'IssueTypeEdit',
      path: '/issue-types/:id',
      component: IssueTypeEdit
    },
    {
      name: 'IssueTypeList',
      path: '/issue-types',
      component: IssueTypeList
    },
    // Issues
    {
      name: 'IssueEdit',
      path: '/issues/:key',
      component: IssueEdit
    },
    {
      name: 'IssueList',
      path: '/issues',
      component: IssueList
    },
    // Catch All
    {
      path: '*',
      redirect: '/404'
    }
  ]
})

router.beforeEach((to, _, next) => {
  if (!to.meta.public && !store.state.token) {
    return next({ name: 'SignIn' })
  }
  next()
})

export default router
