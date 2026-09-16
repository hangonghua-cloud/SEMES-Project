import {
	CallSaveDemoData,//保存Demo数据
} from '@/api/deviceMaintain'
import {} from './../mutations_type'


const deviceMaintain = {
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

export default deviceMaintain
