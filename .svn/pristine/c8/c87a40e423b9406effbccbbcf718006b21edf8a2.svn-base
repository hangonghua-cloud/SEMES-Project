import _config from './config'; // 导入私有配置
import global from '@/utils/global'
import _this from '@/main.js'

export default function $http(options) {
	// 进行url字符串拼接，_config.url是再config中配置要请求的域名或者id+端口号这样方便管理，
	// options.url是index中请求配置的，完美拼接


	// options.url = _config.url + options.url;
	options.url = global.REQUEST_URL + options.url; //正式的时候可以开启
	
	//alert(options.url);
	return new Promise((resolve, reject) => {
		if (options.data && options.data.authCode) {
			_config.header['Fex-auth'] = options.data.authCode
		}
		// 拦截请求
		let token = uni.getStorageSync('token')
		if (token) {
			_config.header.Authorization = token;
		}
		_config.header.UserName = encodeURIComponent(uni.getStorageSync('storage_UserName'))
		_config.header.UserCode = uni.getStorageSync('storage_UserCode')
		_config.header.Language = global.LangLocale;
		_config.complete = (response) => {


			// if (!this.loginInfo.hasLogin) {
			// 	uni.navigateTo({
			// 		url: '/pages/public/login'
			// 	})
			// 	return;
			// }

			resolve(response.data);

			// 			//登录失效这边后台是返回403看情况
			// 			if (response.data.code === 403) {
			// 				//返回登录界面
			// 				uni.navigateTo({
			// 					url: '/pages/public/login'
			// 				})
			// 				uni.showToast({
			// 					icon: 'none',
			// 					title: '登录已失效'
			// 				});
			// 				alert(403)
			// 			} else if (response.data.code === 200) {
			// 				resolve(response.data);
			// 				alert(200)
			// 			} else {
			// 			uni.showToast({
			// 				icon: 'none',
			// 				title: response.data.msg
			// 			});
			// alert('else')
			// 			}
			//reject(response.data);
		}
		_config.fail = (response) => {
			uni.showToast({
				icon: 'none',
				title: _this.$t("common.systemError")
			});
		}
		// 开始请求
		uni.request(Object.assign({}, _config, options));
	})
}

export function erphttp(options) {
	// 进行url字符串拼接，_config.url是再config中配置要请求的域名或者id+端口号这样方便管理，
	// options.url是index中请求配置的，完美拼接
	// options.url = _config.url + options.url;
	options.url = global.REQUEST_ERP_SYNC_URL + options.url; //正式的时候可以开启
	//alert(options.url);
	return new Promise((resolve, reject) => {
		if (options.data && options.data.authCode) {
			_config.header['Fex-auth'] = options.data.authCode
		}
		// 拦截请求
		let token = uni.getStorageSync('token')
		if (token) {
			_config.header.Authorization = token;
		}
		_config.complete = (response) => {
			resolve(response.data);
		}
		_config.fail = (response) => {
			uni.showToast({
				icon: 'none',
				title: _this.$t("common.systemError")
			});
		}
		// 开始请求
		uni.request(Object.assign({}, _config, options));
	})
}