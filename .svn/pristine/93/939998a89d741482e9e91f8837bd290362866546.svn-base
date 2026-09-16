import {
	accountList, 
	getAccount
} from '@/api/account'

const account = {
	state: {

	},

	actions: {
		accountList({
			commit
		}) {
			return new Promise((resolve, reject) => {
				accountList().then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		},
		getAccount({
			commit
		}, coin) {
			return new Promise((resolve, reject) => {
				getAccount(coin).then(res => {
					resolve(res)
				}).catch(error => {
					reject(error)
				})
			})
		}
	}
}

export default account
