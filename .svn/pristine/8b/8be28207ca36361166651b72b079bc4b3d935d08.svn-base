<template>
	<view class="container" style="background-color: #F5F5F5;">
		<view class="user-section">
			<image mode="widthFix" class="bg" src="../../static/personal-bg.jpg"></image>
			<!-- <view class="user-info-box" style="height: 0rpx;"> -->
			<!-- <view class="portrait-box">
					<image class="portrait" :src="'/static/missing-face.png'"></image>
				</view> -->
			<!-- <view class=""> -->
			<!-- <text class="username">欢迎使用海辰科技MES系统</text> -->
			<!-- <view class="tip">欢迎使用海辰科技MES系统</view>
					<view class="tip">当前版本：{{version}}</view> -->
			<!-- </view> -->
			<!-- <view class="info-box"> -->
			<!-- <text class="username">用户编号：{{loginInfo.result.UserCode}}</text> -->
			<!-- <view class="tip">欢迎使用海辰科技MES系统</view>
					<view class="username">编号：{{loginInfo.result.UserCode}}</view>
					<view class="username">姓名：{{loginInfo.result.UserName}}</view>
					<view class="tip">当前版本：{{version}}</view> -->
			<!-- </view> -->
			<!-- </view> -->
			<view class="vip-card-box" style="width: 750rpx;height: 450rpx;margin: -100rpx 0 0 -30rpx;">
				<view class="b-btn" style="">
					{{$t("user.userInfo")}}
				</view>
				<view class="tit" style="margin-top: 45rpx;">
					<text class="yticon icon-iLinkapp-"></text>
					{{$t("user.userCode")}}：{{loginInfo.result.UserCode}}
				</view>
				<view class="tit">
					<text class="yticon icon-iLinkapp-"></text>
					{{$t("user.userName")}}：{{loginInfo.result.UserName}}
				</view>
				<view class="tit">
					<text class="yticon icon-iLinkapp-"></text>
					{{$t("user.uerFactory")}}：{{currentFactory}}
				</view>
				<view class="tit">
					<text class="yticon icon-iLinkapp-"></text>
					{{$t("user.version")}}：{{version}}
				</view>
			</view>
		</view>

		<view class="cover-container" :style="[{
				transform: coverTransform,
				transition: coverTransition
			}]">
			<image class="arc" src="/static/arc.png"></image>

			<!-- 	<view class="order-section">
				<view class="order-item" @click="navTo('/pages/notice/notice')" hover-class="common-hover"  :hover-stay-time="50">
					<text class="yticon icon-shouye"></text>
					<text>公告</text>
				</view>
				<view class="order-item" @click="navTo('/pages/user/encrypAddress')"  hover-class="common-hover" :hover-stay-time="50">
					<text class="yticon icon-daifukuan"></text>
					<text>地址本</text>
				</view>
				<view class="order-item" @click="navTo('/pages/user/invit')" hover-class="common-hover"  :hover-stay-time="50">
					<text class="yticon icon-yishouhuo"></text>
					<text>推荐</text>
				</view>
			</view> -->
			<!-- 浏览历史 -->
			<view class="history-section icon">
				<!-- <list-cell icon="icon-iconfontweixin" iconColor="#e07472" @eventClick="navTo('/pages/user/safe', true)" title="账户与安全"></list-cell> -->
				<!-- <list-cell icon="icon-bangzhu1" iconColor="#ee883b" title="帮助中心" @eventClick="navTo('/pages/set/help')"></list-cell> -->
				<!-- <list-cell icon="icon-pinglun-copy" iconColor="#54b4ef" title="问题反馈"></list-cell> -->
				<list-cell icon="icon-shezhi1" iconColor="#e07472" :title="$t('user.updateLabel')" border=""
					@eventClick="navTo('/pages/public/register')"></list-cell>
			</view>
			<view class="history-section icon">
				<list-cell icon="icon-shanchu4" iconColor="#54b4ef" :title="$t('user.loginOut')" border=""
					@eventClick="toLogout">
				</list-cell>
			</view>
		</view>
		<view v-if="logList.length>0" style="background-color: white;">
			<view class="u-p-t-20 u-p-l-20">{{$t("user.updateLog")}}：</view>
			<view class="u-p-t-20 u-p-l-20" v-for="(item, index) in logList">{{item}}</view>
		</view>
	</view>

</template>
<script>
	import listCell from '@/components/mix-list-cell';
	import {
		mapState,
		mapActions
	} from 'vuex'
	import global from '@/utils/global'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	let startY = 0,
		moveY = 0,
		pageAtTop = true;
	var _self;
	export default {
		components: {
			listCell
		},
		mixins: [commonMixin],
		data() {
			return {
				userInfo: {},
				coverTransform: 'translateY(0px)',
				coverTransition: '0s',
				moving: false,
				version: 'v1.05.20.0',
				isMer: false,
				logList: [],
				currentFactory: uni.getStorageSync("LocationLocaleName") || '',
			}
		},
		onLoad() {
			if (global.AppType == 1) {
				this.version = global.ApkVersion;
			} else if (global.AppType == 2) {
				this.version = global.IosVersion;
			}
			_self = this;
			this.getUpdateLog();
		},
		onShow() {
			if (this.loginInfo.hasLogin) {
				// this.isMerchant().then(res => {
				// 	this.isMer = res.data
				// })
			}
			this.currentFactory = uni.getStorageSync("LocationLocaleName");
		},
		// #ifndef MP
		onNavigationBarButtonTap(e) {
			const index = e.index;
			if (index === 0) {
				this.navTo('/pages/set/set');
			} else if (index === 1) {
				// #ifdef APP-PLUS
				const pages = getCurrentPages();
				const page = pages[pages.length - 1];
				const currentWebview = page.$getAppWebview();
				currentWebview.hideTitleNViewButtonRedDot({
					index
				});
				// #endif
				uni.navigateTo({
					url: '/pages/notice/notice'
				})
			}
		},
		// #endif
		computed: {
			...mapState('user', ['loginInfo']),
			// currentFactory(){
			// 	return uni.getStorageSync("LocationLocaleName");
			// }
		},
		methods: {
			...mapActions('otc', ['isMerchant']),
			...mapActions('user', ['logout', 'GetUpdateLog']),
			toLogin() {
				if (!this.loginInfo.hasLogin) {
					uni.navigateTo({
						url: '/pages/public/login'
					})
				}
			},
			//退出登录
			toLogout() {
				let $this = this;

				uni.showModal({
					content: $this.$t("showModal.Logout_content"),
					cancelText: $this.$t("showModal.cancel"),
					confirmText: $this.$t("showModal.confirm"),
					success: (e) => {
						if (e.confirm) {
							console.log('**************************');
							console.log(global.FilePath);
							console.log(global.LocationLocale);
							console.log(uni.getStorageSync('defaultLocationLocale'))

							this.logout();
							setTimeout(() => {
								$this.navTo('/pages/public/login')
							}, 200)
						}
					}
				});
			},
			getUpdateLog() {
				this.logList = [];
				let query = {
					businessType: '1',
					version: this.version
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.GetUpdateLog(query).then(res => {
					uni.hideLoading();
					if (res.success && res.resultData) {
						this.logList = res.resultData.Content.split('#');
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
		}
	}
</script>
<style lang='scss' scoped>
	%flex-center {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
	}

	%section {
		display: flex;
		justify-content: space-around;
		align-content: center;
		background: #fff;
		border-radius: 10upx;
	}

	.user-section {
		height: 510upx;
		padding: 100upx 30upx 0;
		position: relative;

		.bg {
			position: absolute;
			left: 0;
			top: 0;
			width: 100%;
			height: 100%;
		}
	}

	.user-info-box {
		height: 180upx;
		display: flex;
		align-items: center;
		position: relative;
		z-index: 1;

		.portrait {
			width: 130upx;
			height: 130upx;
			border: 5upx solid #fff;
			border-radius: 50%;
		}

		.username {
			font-size: $font-lg + 6upx;
			color: #ffffff;
			margin-left: 20upx;
		}

		.tip {
			font-size: $font-sm + 2upx;
			color: #ffffff;
			margin-left: 20upx;
		}
	}

	.vip-card-box {
		display: flex;
		flex-direction: column;
		color: #f7d680;
		height: 240upx;
		background: linear-gradient(left, rgba(0, 0, 0, .7), rgba(0, 0, 0, .8));
		/* border-radius: 16upx 16upx 0 0; */
		overflow: hidden;
		position: relative;
		padding: 20upx 24upx;

		.card-bg {
			position: absolute;
			top: 20upx;
			right: 0;
			width: 380upx;
			height: 260upx;
		}

		.b-btn {
			position: absolute;
			right: 30upx;
			top: 60upx;
			width: 132upx;
			height: 40upx;
			text-align: center;
			line-height: 40upx;
			font-size: 22upx;
			color: #36343c;
			border-radius: 20px;
			background: linear-gradient(left, #f9e6af, #ffd465);
			z-index: 1;
		}

		.tit {
			font-size: $font-base+2upx;
			color: #f7d680;
			margin-bottom: 5upx;
			margin-top: 25upx;

			.yticon {
				color: #f6e5a3;
				margin-right: 22upx;
			}
		}

		.e-b {
			font-size: $font-sm;
			color: #d8cba9;
			margin-top: 10upx;
		}
	}

	.cover-container {
		background: $page-color-base;
		margin-top: -150upx;
		padding: 0 30upx;
		position: relative;
		background: #f5f5f5;
		padding-bottom: 20upx;

		.arc {
			position: absolute;
			left: 0;
			top: -34upx;
			width: 100%;
			height: 36upx;
		}
	}

	.tj-sction {
		@extend %section;

		.tj-item {
			@extend %flex-center;
			flex-direction: column;
			height: 140upx;
			font-size: $font-sm;
			color: #75787d;
		}

		.num {
			font-size: $font-lg;
			color: $font-color-dark;
			margin-bottom: 8upx;
		}
	}

	.order-section {
		@extend %section;
		padding: 28upx 0;
		margin-top: 20upx;

		.order-item {
			@extend %flex-center;
			width: 120upx;
			height: 120upx;
			border-radius: 10upx;
			font-size: $font-base;
			color: $font-color-dark;
		}

		.yticon {
			font-size: 58upx;
			margin-bottom: 18upx;
			color: #2696ff;
		}

		.icon-shouhoutuikuan {
			font-size: 44upx;
		}
	}

	.history-section {
		padding: 30upx 0 0;
		margin-top: 20upx;
		background: #fff;
		border-radius: 10upx;

		.sec-header {
			display: flex;
			align-items: center;
			font-size: $font-base;
			color: $font-color-dark;
			line-height: 40upx;
			margin-left: 30upx;

			.yticon {
				font-size: 44upx;
				color: #5eba8f;
				margin-right: 16upx;
				line-height: 40upx;
			}
		}

		.h-list {
			white-space: nowrap;
			padding: 30upx 30upx 0;

			image {
				display: inline-block;
				width: 160upx;
				height: 160upx;
				margin-right: 20upx;
				border-radius: 10upx;
			}
		}
	}
</style>
