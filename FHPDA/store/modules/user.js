import {
	USER_LOGIN,
	USER_LOGOUT,
	USER_UPDATE_PAY_PWD,
	USER_ENABLE_GOOGLE,
	USER_DISABLE_GOOGLE
}
from './../mutations_type'
import {
	getVersion,
	register,
	login,
	updatePayPwd,
	updatePwd,
	encryptBookList,
	addEncryptBook,
	deleteEncryptBook,
	withdraw,
	depositAddress,
	withdrawList,
	depositList,
	invitRank,
	getGoogleKey,
	bindGoogle,
	UpdateAppPassword,
	unbindGoogle,
	
	GetLoginInfo,//获取登陆人信息-富华
	GetUpdateLog//获取更新日志
} from '@/api/user'

const user = {
	state: {
		loginInfo: {
			nickname: null,
			profile: null,
			hasLogin: false,
			isGoogle: false,
			result: null,
		}
	},

	mutations: {
		
		[USER_LOGIN](state, payload) {
			if (payload.code == 100) {
				//-------------------数据类型转换--------------
				
				state.loginInfo = {};
				state.loginInfo.hasLogin = false;
				state.loginInfo.AppModelPageList=[];
				uni.setStorageSync('token', '');
				//说明当前用户有配置数据
				state.loginInfo = payload.data;
				state.loginInfo.hasLogin = true;
				// state.kkk.name='ceshi';
				// state.kkk.hasLogin = true;
				uni.setStorageSync('token', payload.data.token);
			

			}
		},
		[USER_LOGOUT](state, payload) {
			uni.setStorageSync('token', '');
			state.loginInfo = {
				nickname: null,
				profile: null,
				hasLogin: false,
				isGoogle:false,
				result:{}
				//result.UserCode:""
			}
		},
		[USER_UPDATE_PAY_PWD](state, payload) {
			if (payload.code == 200) {
				state.loginInfo.isCapitalPasswd = true

			}
		},
		[USER_ENABLE_GOOGLE](state, payload) {
			if (payload.code == 200) {
				state.loginInfo.isGoogle = true
			}
		},
		[USER_DISABLE_GOOGLE](state, payload) {
			if (payload.code == 200) {
				state.loginInfo.isGoogle = false
			}
		}
	},

	actions: {
		hasLogin() {
			const token = uni.getStorageSync('token');
			if (token != undefined && token != null && token != '') {
				return true;
			}
			return false;
		},
		// 注册
		register({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				register(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//获取版本号
		getVersion({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				getVersion(data).then(res => {
					commit(USER_LOGIN, res)
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		login({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				login(data).then(res => {
					
					commit(USER_LOGIN, res)
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		logout({
			commit
		}) {
			commit(USER_LOGOUT)
		},
		updatePwd({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				updatePwd(data).then(res => {
					resolve()
				}).catch(error => {
					reject(error)
				})
			})
		},
		updatePayPwd({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				updatePayPwd(data).then(res => {
					commit(USER_UPDATE_PAY_PWD, res)
					resolve()
				}).catch(error => {
					reject(error)
				})
			})
		},
		encryptBookList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				encryptBookList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		addEncryptBook({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				addEncryptBook(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		deleteEncryptBook({
			commit
		}, id) {
			return new Promise((resolve, reject) => {
				deleteEncryptBook(id).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		withdraw({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				withdraw(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		depositAddress({
			commit
		}, coin) {
			return new Promise((resolve, reject) => {
				depositAddress(coin).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		withdrawList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				withdrawList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		depositList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				depositList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		invitRank({
			commit
		}) {
			return new Promise((resolve, reject) => {
				invitRank().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		getGoogleKey({
			commit
		}) {
			return new Promise((resolve, reject) => {
				getGoogleKey().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		bindGoogle({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				bindGoogle(data).then(res => {
					commit(USER_ENABLE_GOOGLE, res)
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		unbindGoogle({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				unbindGoogle(data).then(res => {
					commit(USER_DISABLE_GOOGLE, res)
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		UpdateAppPassword({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				UpdateAppPassword(data).then(res => {

					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetLoginInfo({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetLoginInfo(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetUpdateLog({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetUpdateLog(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		}
	}
}

export default user
