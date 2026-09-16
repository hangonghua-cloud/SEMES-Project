import Vue from 'vue'
import Vuex from 'vuex'


import user from './modules/user'
import common from './modules/common'
import exchange from './modules/exchange'
import account from './modules/account'
import cms from './modules/cms'
import websocket from './modules/websocket'
//ALP-----

import Craft from './modules/Craft'
import Equipment from './modules/Equipment'
import Plan from './modules/Plan'
import Produce from './modules/Produce'
import Quality from './modules/Quality'
import Report from './modules/Report'
import Andon from './modules/Andon'
import System from './modules/System'
import WMS from './modules/WMS'

Vue.use(Vuex)

const store = new Vuex.Store({
  modules: {
	websocket: websocket,
	user: {
	  namespaced: true,
	  ...user
	},
	common: {
	  namespaced: true,
	  ...common
	},
	exchange: {
	  namespaced: true,
	  ...exchange
	},
	account: {
	  namespaced: true,
	  ...account
	},
	cms: {
	  namespaced: true,
	  ...cms
	},Craft: {
	  namespaced: true,
	  ...Craft
	},Equipment: {
	  namespaced: true,
	  ...Equipment
	},Plan: {
	  namespaced: true,
	  ...Plan
	},Produce: {
	  namespaced: true,
	  ...Produce
	},Quality: {
	  namespaced: true,
	  ...Quality
	},Report: {
	  namespaced: true,
	  ...Report
	},Andon: {
	  namespaced: true,
	  ...Andon
	},System: {
	  namespaced: true,
	  ...System
	},WMS: {
	  namespaced: true,
	  ...WMS
	}
  }
})

export default store
