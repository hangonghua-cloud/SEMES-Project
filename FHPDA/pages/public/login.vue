<template>
	<view class="container">
		<u-toast ref="uToast"></u-toast>
		<view class="">
			<u-modal v-model="VersionModel" :content="VersionContent" @confirm="confirm" ref="uModal"
				:async-close="true"></u-modal>
		</view>
		<div style="width: 33%;margin:15% auto 0px auto;">
			<img src="static/logo.png" style="height:auto;width: 100%;" @click="popupDialog=true">
		</div>
		<div style="margin: 3% auto 0px auto;">
			<img src="static/login-style.png" style="height:auto;width: 100%;">
		</div>
		<view class="input-content" style="margin-top: 80rpx;">
			<view class="" style="background-color: #F8F6FC;height: 100rpx;border-radius:15rpx;">
				<view class="" style="float: left;width: 15%;">
					<img src="static/user1.png" style="width: 50rpx;height: 50rpx;margin: 33rpx 0 0 20rpx;">
				</view>
				<view class="" style="width: 85%;">
					<input v-model="form.UserCode" style="height: 100rpx;font-size:40rpx;"
						:placeholder="$t('login.account_placeholder')" data-key="UserCode" @input="inputChange" />
				</view>
				<view class="" style="clear: both;"></view>
			</view>

			<view class="" style="background-color: #FFFFFF;height: 10rpx;"></view>

			<view class="" style="background-color: #F8F6FC;height: 100rpx;border-radius:15rpx;">
				<view class="" style="float: left;width: 15%;">
					<img src="static/pwd1.png" style="width: 50rpx;height: 50rpx;margin: 33rpx 0 0 20rpx;">
				</view>
				<view class="" style="width: 85%;">
					<input style="height: 100rpx;font-size:40rpx;" v-model="form.password"
						:placeholder="$t('login.password_placeholder')" placeholder-class="input-empty" maxlength="20"
						password data-key="password" @input="inputChange" @confirm="toLogin" />
				</view>
				<view class="" style="clear: both;"></view>
			</view>
		</view>
		<view style="width: 100%;margin-top: 10px;">

			<u-dropdown ref="uDropDown" @open="onLansClick()">
				<u-dropdown-item v-model="lang" :title="langName" :options="lans" @change="onLans()"></u-dropdown-item>
			</u-dropdown>

		</view>
		<view class="" style="margin-top: 150rpx;">
			<u-button type="primary" :ripple="true" ripple-bg-color="#138087" @click="toLogin" :disabled="logining"
				style="width: 500rpx;margin-left: auto;">{{$t('login.login_button')}}</u-button>
		</view>

		<view class="" style="width: 80%;margin: auto;padding-top: 100rpx;">
			<u-line-progress :striped="true" v-show="download_show" :percent="downLoadPercent" :striped-active="true"
				:show-percent="true" :round="true" active-color="#ff9900">
			</u-line-progress>
		</view>
		<!-- 服务器地址设置弹窗 -->
		<u-popup v-model="popupDialog" mode="top" length="50%">
			<view style="margin-top: 20px;">
				<u-form :model="form" ref="uForm" label-width="auto">
					<u-form-item :label="$t('login.serivceLocation')">
						<view @click="showSelect2=true" style="width: 100%;">
							<u-input v-model="form.LocationLocaleName" style="pointer-events: none;" disabled
								:placeholder="$t('login.serivceLocation_placeholder1')" border />
						</view>
					</u-form-item>
					<u-form-item :label="$t('login.serivceAddress')">
						<view @click="showSelect=true" style="width: 100%;">
							<u-input v-model="form.AddressName" style="pointer-events: none;" disabled
								:placeholder="$t('login.serivceAddress_placeholder1')" border />
						</view>
					</u-form-item>
					<view class="input-item">
						<view class="tit">{{$t('login.serivceAddress')}}</view>
						<input type="mobile" v-model="form.REQUEST_URL"
							:placeholder="$t('login.serivceAddress_placeholder2')" />
					</view>
					<view class="input-item">
						<view class="tit">{{$t('login.appAddress')}}</view>
						<input type="mobile" v-model="form.DownLoadUrl"
							:placeholder="$t('login.appAddress_placeholder')" />
					</view>
					<view class="input-item">
						<view class="tit">{{$t('login.fileAddress')}}</view>
						<input type="mobile" v-model="form.FileHandler"
							:placeholder="$t('login.fileAddress_placeholder')" />
					</view>
				</u-form>
				<view class="" style="display: flex;">
					<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
						:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="popupDialog=false"
						style="position: fixed;bottom: 30rpx;margin-left: 5%;">{{$t('login.cancel')}}
					</u-button>
					<u-button :type="'primary'" :ripple="true" ripple-bg-color="#00aa00"
						:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}"
						@click="handleSettingConfirm" style="position: fixed;bottom: 30rpx;margin-left: 50%;">
						{{$t('login.confrim')}}
					</u-button>
				</view>
			</view>
		</u-popup>
		<u-select v-model="showSelect" @confirm="changeSelect" :list="addressTypeList"></u-select>
		<u-select v-model="showSelect2" @confirm="changeSelect2" :list="addressTypeList2"></u-select>
	</view>
</template>

<script>
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		isMobile,
		isPassword
	} from '../../utils/validate'
	import global from '@/utils/global'
	export default {
		data() {

			return {
				lang: "",
				langName: "",
				lans: [{
					label: "简体中文",
					value: "zh-CN"
				}, {
					label: "Tiếng Việt",
					value: "vi-VN"
				}, {
					label: "ภาษาไทย",
					value: "th-TH"
				}, {
					label: "English",
					value: "en-US"
				}],
				langUpdateStyle: { //如果两个条件都是true，触发刷新事件，来切换style
					click: false, //判断是不是手动触发的
					change: false //是否切换
				},
				form: {
					// REQUEST_URL: (uni.getStorageSync('storage_URL') == "" || uni.getStorageSync('storage_URL') == undefined) ? global.REQUEST_URL :
					// 	uni.getStorageSync('storage_URL'),

					REQUEST_URL: global.REQUEST_URL,
					DownLoadUrl: global.DownLoadUrl,
					FileHandler: global.FileHandler,
					FilePath: global.FilePath,
					LocationLocale: global.LocationLocale,
					LocationLocaleName: "",

					// UserCode: uni.getStorageSync('storage_UserCode'),
					// password: uni.getStorageSync('storage_Password'),
					UserCode: global.loginUserCode,
					password: '',

					AddressCode: global.defaultAddressCode,
					AddressName: "",
				},
				download_show: false,
				downLoadPercent: 0,
				version: false,
				VersionModel: false,
				VersionContent: this.$t("login.updateTips"),
				logining: false,
				popupDialog: false,
				showSelect: false,
				showSelect2: false,
				addressTypeList: [{
						value: "1",
						label: this.$t("login.outAddress")
					},
					{
						value: "2",
						label: this.$t("login.inAddress")
					}
				],
				addressTypeList2: [{
					value: "CN",
					label: this.$t("login.Location_CN")
				}, {
					value: "VN",
					label: this.$t("login.Location_VN")
				}, {
					value: "TH",
					label: this.$t("login.Location_TH")
				}],
			}
		},

		onShow() {
			console.log('========')
			console.log(global.LocationLocale)
			console.log(this.addressTypeList2.filter(x => x.value == this.form.LocationLocale))
			console.log(uni.getStorageSync('defaultLocationLocale'))
		},
		methods: {
			...mapActions('user', ['login']),
			...mapActions('user', ['getVersion']),
			inputChange(e) {
				const key = e.currentTarget.dataset.key;
				this[key] = e.detail.value;
			},

			//基础信息跟随语言更新
			basicDataLang() {
				this.addressTypeList = [{
						value: "1",
						label: this.$t("login.outAddress")
					},
					{
						value: "2",
						label: this.$t("login.inAddress")
					}
				];
				this.addressTypeList2 = [{
					value: "CN",
					label: this.$t("login.Location_CN")
				}, {
					value: "VN",
					label: this.$t("login.Location_VN")
				}, {
					value: "TH",
					label: this.$t("login.Location_TH")
				}];
			},
			onLansClick() {
				this.langUpdateStyle.click = true;
			},
			onLans() {
				// debugger
				this.$i18n.locale = this.lang;
				// this.$i18njson.locale = this.lang;
				var i18nInfo = this.lans.find(x => x.value == this.lang);
				this.langName = i18nInfo ? i18nInfo.label : "";
				uni.setStorageSync('lang_locale', this.lang);
				global.LangLocale = this.lang;
				//数据更新 基础数据国际化，需要重新刷新
				this.VersionContent = this.$t("login.updateTips");
				//基础信息跟随语言更新
				this.basicDataLang();

				this.changeSelect(this.addressTypeList.filter(x => x.value == this.form.AddressCode));
				this.changeSelect2(this.addressTypeList2.filter(x => x.value == this.form.LocationLocale));

				//调用全局监听
				this.langUpdateStyle.change = true;
				uni.$emit("RefreshStyle", this.lang, this.langUpdateStyle);
			},
			initLang() {
				this.$i18n.locale = 'zh-CN';
				this.lang = 'zh-CN'
				uni.setStorageSync('lang_locale', 'zh-CN');
				global.LangLocale = this.lang;
				return "zh-CN";
			},
			initSetting() {
				// this.handleLoadSetting(this.form.LocationLocale);

				// debugger
				var i18nLocale = uni.getStorageSync('lang_locale') ? uni.getStorageSync('lang_locale') : this.initLang();
				var i18nInfo = this.lans.find(x => x.value == i18nLocale);
				this.lang = i18nInfo ? i18nInfo.value : "";
				this.langName = i18nInfo ? i18nInfo.label : "";
				this.onLans()
			},
			confirm() {
				this.VersionModel = false;
				this.download_show = true;
				uni.showLoading({
					title: this.$t("common.waiting")
				});
				//var strPath = 'http://192.168.247.128:8088/AppDownLoad/ZT-MES.apk';
				const downloadTask = uni.downloadFile({
					url: global.DownLoadUrl, //
					success: (res) => {
						if (res.statusCode === 200) {
							console.log(this.$t("login.downSucesss"));
							//this.version = false;
						}
						//this.dd = res.tempFilePath;
						//console.log(this.dd);
						//let that = this;
						// uni.saveFile({
						// 	tempFilePath: res.tempFilePath,
						// 	success: function(red) {
						// 		that.luj = red.savedFilePath;
						// 		console.log(red);
						// 	}
						// });
					}
				});

				downloadTask.onProgressUpdate((res) => {
					this.downLoadPercent = res.progress;
					if (res.progress == 100) {
						uni.hideLoading();
						this.$refs.uToast.show({
							title: this.$t("login.downSucesss"),
							type: 'success'
						});
						this.download_show = false;
					}
					// console.log('下载进度' + res.progress);
					// console.log('已经下载的数据长度' + res.totalBytesWritten);
					// console.log('预期需要下载的数据总长度' + res.totalBytesExpectedToWrite);
				});
			},
			onLoad() {
				this.initSetting();

				this.form.AddressName = this.addressTypeList.find(t => t.value == this.form.AddressCode).label;
				this.form.LocationLocaleName = this.addressTypeList2.find(t => t.value == this.form.LocationLocale).label;

				this.loginInfo = null;
				//var self = this;
				//1，从main.js中拿到当前的版本号
				var myVerson = this.$current.verson_apk; //1.Android(默认)  2.ISO
				if (global.AppType == 2) {
					myVerson = this.$current.verson_ios;
				}
				//var myVerson = this.$current.verson_iso;//1.Android(默认)  2.ISO
				var obj = {
					AppType: global.AppType, //1.Android(默认)  2.ISO 
					AppVersion: myVerson
				}
				/*暂时关闭版本检查*/
				this.getVersion(obj).then(res => {
					console.log(JSON.stringify(res));
					if (res == undefined) {
						console.log('连接服务器失败！');
					} else {
						if (res.IsUpdate == true) {
							console.log('需要更新版本！');
							this.version = true;
							//this.VersionModel = true;
							uni.showModal({
								title: this.$t("login.update_ModelTitle"),
								content: this.$t("login.update_ModelContent"),
								confirmText: this.$t("login.update_ModelConfrim"),
								cancelText: this.$t("login.update_ModelCancel"),
								success: function(res) {
									if (res.confirm) {
										var downloadUrl = global
											.DownLoadUrl; //GLOBAL.DOMAIN_URL + "/apk/mzz2.apk";

										if (global.AppType == 2) {
											plus.runtime.openURL(downloadUrl, '', '');
											// window.location.href = downloadUrl;
											//document.location.href = downloadUrl;
											//window.open(downloadUrl,'_blank') // 新窗口打开外链接 
											return;
										}
										// uni.showToast({
										// 	icon: "none",
										// 	mask: true,
										// 	title: '有新的版本发布，检测到您目前为Wifi连接，程序已启动自动更新。新版本下载完成后将自动弹出安装程序',
										// 	duration: 5000,
										// });
										//设置 最新版本apk的下载 

										var dtask = plus.downloader.createDownload(downloadUrl, {},
											function(d, status) {
												// 下载完成 
												if (status == 200) {
													plus.runtime.install(plus.io
														.convertLocalFileSystemURL(d
															.filename), {}, {},
														function(error) {
															uni.showToast({
																title: this.$t(
																	"login.install_Error"
																),
																duration: 1500
															});
														})
												} else {
													uni.showToast({
														title: this.$t(
															"login.update_Error"),
														duration: 1500
													});
												}
											});
										dtask.start();
										var prg = 0;
										var showLoading = plus.nativeUI.showWaiting(
											this.$t("login.downloading")); //创建一个showWaiting对象 
										dtask.addEventListener('statechanged', function(
											task,
											status
										) {
											// 给下载任务设置一个监听 并根据状态  做操作
											switch (task.state) {
												case 1:
													showLoading.setTitle(this.$t(
														"login.downloading"));
													// this.download_show = true;
													// uni.showLoading({
													// 	title: '下载中，请稍后···'
													// });
													break;
												case 2:
													//showLoading.setTitle("已连接到服务器");
													break;
												case 3:
													prg = parseInt(
														(parseFloat(task
																.downloadedSize) /
															parseFloat(task.totalSize)
														) *
														100
													);
													showLoading.setTitle(this.$t(
															"login.downloading") + prg +
														"%  ");
													//this.downLoadPercent = prg;
													break;
												case 4:
													plus.nativeUI.closeWaiting();
													//this.download_show = false;
													//uni.hideLoading();
													//下载完成
													break;
											}
										});
									} else if (res.cancel) {
										this.version = false;
										this.logining = false;
										console.info('状态', this.version, this.logining);
										//plus.runtime.quit();
										//plus.runtime.restart();
										console.log('稍后更新');
									}
								}
							});
						} else {
							this.logining = false;
							console.log('不需要更新版本！');
						}
					}
				}).catch(error => {
					console.log('访问失败，请联系管理员！！');
				})
			},
			navBack() {
				uni.navigateBack();
			},
			toRegist() {
				uni.navigateTo({
					url: '/pages/public/register'
				})
			},
			toLogin() {
				this.logining = true;
				if (this.form.password == '') {
					this.$api.msg(this.$t("login.passwordError"))
					this.logining = false
					return;
				}
				var self = this;
				//修改访问服务器，正式发布的时候开启
				global.REQUEST_URL = self.form.REQUEST_URL;
				global.DownLoadUrl = self.form.DownLoadUrl;
				global.FileHandler = self.form.FileHandler;
				global.FilePath = self.form.FilePath;
				global.defaultAddressCode = self.form.AddressCode;
				global.LocationLocale = self.form.LocationLocale;

				// global.loginUserCode = self.form.UserCode;

				// uni.switchTab({
				//     url: "/pages/index/index",
				// });
				uni.showLoading({
					title: this.$t("login.loginLoading")
				});
				this.logining = false
				this.login(this.form).then(res => {

					if (res == undefined) {
						this.$api.msg(this.$t("login.loginErrorService"), 3000, false, 'none', function() {})
					} else {

						if (res.code == '100') {
							this.$api.msg(this.$t("login.loginSuccess"), 2000, false, 'green', () => {
								self.handleSettingConfirm();
								//保存账号
								uni.setStorageSync('storage_URL', self.form.REQUEST_URL);
								uni.setStorageSync('storage_UserCode', self.form.UserCode);
								// uni.setStorageSync('storage_Password', self.form.password);
								uni.setStorageSync('storage_UserName', res.data.result.UserName);
								uni.setStorageSync('FileHandler', global.FileHandler);
								uni.setStorageSync('FilePath', global.FilePath);
								uni.setStorageSync('LocationLocaleName', this.form.LocationLocaleName);
								setTimeout(function() {
									uni.hideLoading();
									uni.switchTab({
										url: "/pages/index/index",
									});
									// uni.navigateBack({})
								}, 500)
							})
						} else {
							this.$api.msg(res.ret, 3000, false, 'none', function() {})
						}
						this.logining = false
					}
				}).catch(error => {
					uni.hideLoading();
					this.$api.msg(this.$t("common.requestError"), 3000, false, 'none', function() {})
					this.logining = false
				})
			},
			changeSelect(val) {
				this.form.AddressCode = val[0].value;
				this.form.AddressName = val[0].label;
				// this.handleLoadSetting(this.form.LocationLocale);
				var addresscode = this.form.AddressCode;
				var location = this.form.LocationLocale;


				this.form.REQUEST_URL = global.handleGetUrlString("REQUEST_URL", addresscode, location);
				this.form.DownLoadUrl = global.handleGetUrlString("DownLoadUrl", addresscode, location);
				this.form.FileHandler = global.handleGetUrlString("FileHandler", addresscode, location);
				this.form.FilePath = global.handleGetUrlString("FilePath", addresscode, location);
			},
			changeSelect2(val) {
				this.form.LocationLocale = val[0].value;
				this.form.LocationLocaleName = val[0].label;
				// this.handleLoadSetting(this.form.LocationLocale);
				var addresscode = this.form.AddressCode;
				var location = this.form.LocationLocale;


				this.form.REQUEST_URL = global.handleGetUrlString("REQUEST_URL", addresscode, location);
				this.form.DownLoadUrl = global.handleGetUrlString("DownLoadUrl", addresscode, location);
				this.form.FileHandler = global.handleGetUrlString("FileHandler", addresscode, location);
				this.form.FilePath = global.handleGetUrlString("FilePath", addresscode, location);
			},
			handleSettingConfirm() {
				var self = this;
				var jsonLoginSetting = uni.getStorageSync('login_setting') ? uni.getStorageSync('login_setting') : "";
				var jsonData = {};
				if (jsonLoginSetting) {
					jsonData = JSON.parse(jsonLoginSetting);
				}
				var settingForm = Object.assign({}, self.form); //copy当前form信息
				settingForm.UserCode = '';
				settingForm.password = '';

				var locationData = jsonData[self.form.LocationLocale]; //获取当前所在地的集合信息
				if (locationData) {
					if (locationData.length) {
						let IsHave = false; //是否已经替换
						var NewlocationData = []; //创建新集合
						locationData.forEach(x => {
							if (x.AddressCode == settingForm.AddressCode) {
								NewlocationData.push(settingForm);
								IsHave = true;
							} else {
								NewlocationData.push(x);
							}
						});
						if (!IsHave) {
							NewlocationData.push(settingForm); //如果未替换直接新增
						}
						locationData = NewlocationData; //让新集合替换老集合
					} else {
						locationData = [];
						locationData.push(settingForm);
					}
				} else {
					locationData = [];
					locationData.push(settingForm);
				}


				jsonData[self.form.LocationLocale] = locationData;

				uni.setStorageSync('login_setting', JSON.stringify(jsonData));
				uni.setStorageSync('defaultAddressCode', self.form.AddressCode);
				uni.setStorageSync('defaultLocationLocale', self.form.LocationLocale);
				this.popupDialog = false;
			},
			handleLoadSetting(key) {
				var jsonLoginSetting = uni.getStorageSync('login_setting') ? uni.getStorageSync('login_setting') : "";
				if (jsonLoginSetting) {
					let jsonData = JSON.parse(jsonLoginSetting);
					var localData = jsonData[key];
					if (localData && localData.length) {
						var newGlobalDatas = [];
						var globalData = global.datas[key];
						if (globalData && globalData.length) {
							global.datas[key].forEach(item => {
								localData.forEach(element => {
									if (item.AddressCode == element.AddressCode) {
										newGlobalDatas.push(element);
									} else {
										newGlobalDatas.push(item);
									}
								});
							});
							global.datas[key] = newGlobalDatas;
						} else {
							global.datas[key] = localData;
						}
					}
				}
			},
		},

	}
</script>

<style lang='scss' scoped>
	page {
		background: #fff;
	}


	/* /deep/.u-dropdown__menu {
		height: 1px !important;
	}

	/deep/.u-dropdown__menu__item {
		.u-flex {
			display: none;
		}
	} */

	.container {
		/* padding-top: 115px; */
		position: relative;
		width: 100vw;
		height: 100vh;
		overflow: hidden;
		background: #fff;
	}

	.wrapper {
		position: relative;
		z-index: 90;
		background: #fff;
		padding-bottom: 40upx;
	}

	.back-btn {
		position: absolute;
		left: 40upx;
		z-index: 9999;
		padding-top: var(--status-bar-height);
		top: 40upx;
		font-size: 40upx;
		color: $font-color-dark;
	}

	.left-top-sign {
		font-size: 120upx;
		color: $page-color-base;
		position: relative;
		left: -16upx;
	}

	.right-top-sign {
		position: absolute;
		top: 80upx;
		right: -30upx;
		z-index: 95;

		&:before,
		&:after {
			display: block;
			content: "";
			width: 400upx;
			height: 80upx;
			background: #b4f3e2;
		}

		&:before {
			transform: rotate(50deg);
			border-radius: 0 50px 0 0;
		}

		&:after {
			position: absolute;
			right: -198upx;
			top: 0;
			transform: rotate(-50deg);
			border-radius: 50px 0 0 0;
			/* background: pink; */
		}
	}

	.left-bottom-sign {
		position: absolute;
		left: -270upx;
		bottom: -320upx;
		border: 100upx solid #d0d1fd;
		border-radius: 50%;
		padding: 180upx;
	}

	.welcome {
		position: relative;
		left: 50upx;
		top: -90upx;
		font-size: 46upx;
		color: #555;
		text-shadow: 1px 0px 1px rgba(0, 0, 0, .3);
	}

	.input-content {
		padding: 0 60upx;
	}

	.input-item {
		display: flex;
		flex-direction: column;
		align-items: flex-start;
		justify-content: center;
		padding: 0 30upx;
		background: $page-color-light;
		height: 120upx;
		border-radius: 4px;
		margin-top: 20upx;
		/* 	margin-bottom: 10upx; */

		&:last-child {
			margin-bottom: 0;
		}

		.tit {
			height: 50upx;
			line-height: 56upx;
			font-size: $font-sm+2upx;
			color: $font-color-base;
		}

		input {
			height: 60upx;
			font-size: $font-base + 2upx;
			color: $font-color-dark;
			width: 100%;
		}
	}

	.confirm-btn {
		width: 630upx;
		height: 76upx;
		line-height: 76upx;
		border-radius: 50px;
		margin-top: 70upx;
		background: $uni-color-blue;
		color: #fff;
		font-size: $font-lg;

		&:after {
			border-radius: 100px;
		}
	}

	.forget-section {
		font-size: $font-sm+2upx;
		color: $font-color-spec;
		text-align: center;
		margin-top: 40upx;
	}

	.register-section {
		position: absolute;
		left: 0;
		bottom: 50upx;
		width: 100%;
		font-size: $font-sm+2upx;
		color: $font-color-base;
		text-align: center;

		text {
			color: $font-color-spec;
			margin-left: 10upx;
		}
	}
</style>