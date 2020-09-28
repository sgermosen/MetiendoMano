import Vue from 'vue'
import Vuex from 'Vuex'
import services from './services'

Vue.use(Vuex)

const state = {
    services
}

export default new Vuex.Store({
    state
})
