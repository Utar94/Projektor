import Vue from 'vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import {
  faBan,
  faBoxOpen,
  faEdit,
  faEye,
  faHome,
  faKey,
  faPaperPlane,
  faPlus,
  faSave,
  faSearch,
  faSignInAlt,
  faSignOutAlt,
  faSyncAlt,
  faTags,
  faTasks,
  faTrashAlt,
  faUser
} from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(
  faBan,
  faBoxOpen,
  faEdit,
  faEye,
  faHome,
  faKey,
  faPaperPlane,
  faPlus,
  faSave,
  faSearch,
  faSignInAlt,
  faSignOutAlt,
  faSyncAlt,
  faTags,
  faTasks,
  faTrashAlt,
  faUser
)

Vue.component('font-awesome-icon', FontAwesomeIcon)
