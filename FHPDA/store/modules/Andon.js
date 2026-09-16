import {
	CallSaveDemoData,//保存Demo数据
} from '@/api/Andon'
import {} from './../mutations_type'


const Andon = {
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

export default Andon
