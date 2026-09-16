import {
	sendSms,
	coinList,
	coinTips,
	marketList,
	adList,
	noticeList,
	currencyList,
	fiatList,
	CallGetEntity,
	GetFactoryModel,
	GetDictionary,
	SelfAction,
	GetOpenType,
	GetResourceByLevelCode, //获取指定层级下的所有资源
	GetProcessModel,
	GetModelResourceExtendInfoByLevelCode, //获取工厂建模
	GetUserList,//模糊获取人员
	GetBaseMaterialList, // 模糊查询物料
    GetListByParentResource,//根据父级获取子集
	GetListByProductionMachine,
	GetModelResourceByChild,//根据子集获取父类
	GetWarehouseByFactory,//根据工厂获取仓库
	GetWarehouseByFactoryExtendInfo//根据工厂、属性获取仓库
} from '@/api/common'
import {
	COMMON_COIN_LIST
} from './../mutations_type'


const common = {
	state: {
		coins: null,
		coinMap: {}
	},

	mutations: {
		[COMMON_COIN_LIST](state, payload) {
			if (payload.code == 200) {
				state.coins = payload.data,
					state.coins.forEach((item, i) => {
						state.coinMap[item.symbol] = item
					})
			}
		}
	},

	actions: {
		coinList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				coinList().then(res => {
					commit(COMMON_COIN_LIST, res)
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		coinTips({
			commit
		}, coin) {
			return new Promise((resolve, reject) => {
				coinTips(coin).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		marketList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				marketList().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		// 手机验证码发送
		sendSms({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				sendSms(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		adList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				adList().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		noticeList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				noticeList().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		currencyList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				currencyList().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		fiatList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				fiatList().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//-------------查询表
		CallGetEntity({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				CallGetEntity(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		//-------------查询表
		GetFactoryModel({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetFactoryModel(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		GetDictionary({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetDictionary(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetModelResourceExtendInfoByLevelCode({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetModelResourceExtendInfoByLevelCode(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetUserList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetUserList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		//FuHua 模糊查询物料信息
		GetBaseMaterialList({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetBaseMaterialList(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		
		
		SelfAction({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				SelfAction(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		GetOpenType({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetOpenType(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
		GetResourceByLevelCode({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetResourceByLevelCode(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		GetProcessModel({
			commit
		}, data) {
			return new Promise((resolve, reject) => {
				GetProcessModel(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		
       GetListByParentResource({
       	commit
       }, data) {
       	return new Promise((resolve, reject) => {
       		GetListByParentResource(data).then(res => {
       			resolve(res)
       		}).catch(error => {
       			reject(error)
       		})
       	})
       },

	   
	   GetListByProductionMachine({
	   	commit
	   }, data) {
	   	return new Promise((resolve, reject) => {
	   		GetListByProductionMachine(data).then(res => {
	   			resolve(res)
	   		}).catch(error => {
	   			reject(error)
	   		})
	   	})
	   },
	   GetModelResourceByChild({
	   	commit
	   }, data) {
	   	return new Promise((resolve, reject) => {
	   		GetModelResourceByChild(data).then(res => {
	   			resolve(res)
	   		}).catch(error => {
	   			reject(error)
	   		})
	   	})
	   },
	   GetWarehouseByFactory({
	   	commit
	   }, data) {
	   	return new Promise((resolve, reject) => {
	   		GetWarehouseByFactory(data).then(res => {
	   			resolve(res)
	   		}).catch(error => {
	   			reject(error)
	   		})
	   	})
	   },
	   GetWarehouseByFactoryExtendInfo({
	   	commit
	   }, data) {
	   	return new Promise((resolve, reject) => {
	   		GetWarehouseByFactoryExtendInfo(data).then(res => {
	   			resolve(res)
	   		}).catch(error => {
	   			reject(error)
	   		})
	   	})
	   },
	}
}

export default common
