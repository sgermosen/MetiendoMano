import Vue from 'vue'
import Router from 'vue-router'

import Default from '@/components/Default'
import UserInfo from '@/components/user/info'
import UserProfile from '@/components/user/profile'
import Report from '@/components/admin/report'

Vue.use(Router)

const routes = [
  { path: '/', name: 'Default', component: Default },
  { path: '/mi-informacion', name: 'UserInfo', component: UserInfo },
  { path: '/u/:url', name: 'UserProfile', component: UserProfile },

  { path: '/reports', name: 'Report', component: Report },
]

export default new Router({
  routes
})
