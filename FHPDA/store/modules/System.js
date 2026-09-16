import {
	CallSaveDemoData,//保存Demo数据
} from '@/api/System'
import {} from './../mutations_type'


const System = {
	state: {

	},

	mutations: {

	},

	actions: {
		CallSaveDemoData({
			commit
		},data) {
			return new Promise((resolve, reject) => {
				CallSaveDemoData(data).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},

	}
}

export default System
