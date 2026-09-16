///type url类型 
///address 内外网类型
///location 工厂所在地 
function handleGetUrlString(type, address, location) {
	handleLoadSetting(location, () => {});
	var data = datas[location]; //先查询所在地

	if (data && data.length) {
		var urlForm = data.find(x => x.AddressCode == address); //查询内外网类型
		if (urlForm) {
			return urlForm[type]; //返回url类型
		}
	}
}

function handleLoadSetting(key, callback) {
	var jsonLoginSetting = uni.getStorageSync('login_setting') ? uni.getStorageSync('login_setting') : "";
	if (jsonLoginSetting) {
		let jsonData = JSON.parse(jsonLoginSetting);
		var localData = jsonData[key];
		if (localData && localData.length) {
			var newGlobalDatas = [];
			var globalData = datas[key];
			if (globalData && globalData.length) {
				datas[key].forEach(item => {
					localData.forEach(element => {
						if (item.AddressCode == element.AddressCode) {
							newGlobalDatas.push(element);
						} else {
							newGlobalDatas.push(item);
						}
					});
				});
				datas[key] = newGlobalDatas;
			} else {
				datas[key] = localData;
			}
		}
	}
	callback();
}
//初始化数据
var datas = {
	//1:外网地址 2：内网地址
	defaultAddressCode: uni.getStorageSync('defaultAddressCode') ? uni.getStorageSync(
		'defaultAddressCode') : "2",
	defaultLocationLocale: uni.getStorageSync('defaultLocationLocale') ? uni.getStorageSync(
		'defaultLocationLocale') : "CN",
	CN: [{
		AddressCode: "1", //外网地址
		FilePath: 'http://172.168.11.131/MWebAPI/', //虚拟机发布保存图片地址
		DownLoadUrl: 'http://mes.hualifloors.cn:8800/apk/JSFH-MES.apk', //虚拟机下载App地址
		FileHandler: 'http://mes.hualifloors.cn:8800/MWebAPI/UploadFile/Upload', //上传图片地址
		REQUEST_URL: 'http://mes.hualifloors.cn:8800/MWebAPI/', //后台正式地址
	}, {
		AddressCode: "2", //内网地址
		FilePath: 'http://172.168.11.131/MWebAPI/', //虚拟机发布保存图片地址
		DownLoadUrl: 'http://172.168.11.122/apk/JSFH-MES.apk', //虚拟机下载App地址
		FileHandler: 'http://172.168.11.131/MWebAPI/UploadFile/Upload', //上传图片地址
		REQUEST_URL: 'http://172.168.11.122/MWebAPI/', //后台正式地址
	}],
	VN: [{
		AddressCode: "1", //越南外网地址
		FilePath: 'http://192.168.19.102/MWebAPI/', //虚拟机发布保存图片地址
		DownLoadUrl: 'http://mes.hualifloors.cn:8800/apk/JSFH-MES.apk', //虚拟机下载App地址
		FileHandler: 'http://117.7.228.227:8800/MWebAPI/UploadFile/Upload', //上传图片地址
		REQUEST_URL: 'http://117.7.228.227:8800/MWebAPI/', //后台正式地址
	}, {
		AddressCode: "2", //越南内网地址
		FilePath: 'http://192.168.19.102/MWebAPI/', //虚拟机发布保存图片地址
		DownLoadUrl: 'http://172.168.11.122/apk/JSFH-MES.apk', //虚拟机下载App地址
		FileHandler: 'http://192.168.19.102/MWebAPI/UploadFile/Upload', //上传图片地址
		REQUEST_URL: 'http://192.168.19.102/MWebAPI/', //后台正式地址
	}],
	TH: [{
		AddressCode: "1", //泰国外网地址
		FilePath: 'http://172.168.11.130/MWebAPI/', //虚拟机发布保存图片地址
		DownLoadUrl: 'http://mes.hualifloors.cn:8800/apk/JSFH-MES.apk', //虚拟机下载App地址
		FileHandler: 'http://117.7.228.227:8800/MWebAPI/UploadFile/Upload', //上传图片地址
		REQUEST_URL: 'http://117.7.228.227:8800/MWebAPI/', //后台正式地址
	}, {
		AddressCode: "2", //泰国内网地址
		FilePath: 'http://172.16.10.102/MWebAPI/', //虚拟机发布保存图片地址
		DownLoadUrl: 'http://172.168.11.122/apk/JSFH-MES.apk', //虚拟机下载App地址
		FileHandler: 'http://172.16.10.102/MWebAPI/UploadFile/Upload', //上传图片地址
		REQUEST_URL: 'http://172.16.10.102/MWebAPI/', //后台正式地址
	}]
}

export default {
	CAPTCHA_TYPE: {
		COMMON: 'common'
	},
	AppType: 1, //1.Android(默认)  2.IOS
	ApkVersion: 'v5.04.15.1', //Android当前版本(版本规则：'v.' + 年编号（2021年为1，2022年为2，以此类推） + '.' + 月份（两位） + '.' + 日（两位） + '.' + 当天发布次数（第一次为0，第二次为1，以此类推）)
	IosVersion: 'v1.12.30.1', //Ios当前版本
	loginUserCode: "", //记录登陆人编码
	LangLocale: 'zh-CN',
	LocationLocale: datas.defaultLocationLocale,
	handleGetUrlString: handleGetUrlString,
	datas: datas,

	//正式内网
	FilePath: handleGetUrlString("FilePath", datas.defaultAddressCode, datas.defaultLocationLocale), //虚拟机发布保存图片地址
	DownLoadUrl: handleGetUrlString("DownLoadUrl", datas.defaultAddressCode, datas.defaultLocationLocale), //虚拟机下载App地址
	FileHandler: handleGetUrlString("FileHandler", datas.defaultAddressCode, datas.defaultLocationLocale), //上传图片地址
	REQUEST_URL: handleGetUrlString("REQUEST_URL", datas.defaultAddressCode, datas.defaultLocationLocale), //后台正式地址

	//服务器参数
	//外网//1:外网地址 2：内网地址
	defaultAddressCode: datas.defaultAddressCode,

}
