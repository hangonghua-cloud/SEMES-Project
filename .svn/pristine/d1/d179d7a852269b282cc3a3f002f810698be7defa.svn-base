<template style="height: 100px;">
	<view class="container" style="background-color: #F9F9F9;">
		<view
			style="text-align: center;padding-top: 4px;margin-top: -5px;padding-bottom: 4px;background-color: rgb(19, 128, 135);line-height: 30px;margin-bottom:  5px;color:  white;font-size: 17px;">
			{{$t("title")}}
		</view>
		<view class="workitem" v-for="(item, i) in list">
			<view class="wrap item title" style="clear: both; height: 50rpx;margin-right: 10rpx;line-height: 50rpx;">
				{{$t("menu."+item.module+".title")}}
			</view>
			<view class="order-section" style="height:initial;">
				<view v-for="(Pitem, i) in item.path" style="height: 140rpx;" class="orderplan"
					@click="navTo(Pitem.url, true)" hover-class="common-hover" :hover-stay-time="50">
					<view :class='Pitem.ico' v-bind:style="setStyle(Pitem.color)"></view>
					<view class="itemlable">
						{{checkI18n(item.module,Pitem.code,Pitem.name)}}
					</view>
				</view>
			</view>
			<view style="height: 15px; clear: both;"></view>
		</view>
	</view>
</template>
<script>
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		formatUnit
	} from '../../utils/number'
	import {
		uniNoticeBar,
		uniTag,
		uniSwiperDot
	} from '@dcloudio/uni-ui'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	export default {
		components: {
			uniNoticeBar,
			uniTag,
			uniSwiperDot
		},
		mixins: [commonMixin],
		data() {
			return {
				markets: [],
				titleNViewBackground: '',
				ads: [],
				current: 0,
				mode: 'round',
				topSymbols: [{
						symbol: 'btcusdt',
						title: 'BTC/USDT'
					},
					{
						symbol: 'ethusdt',
						title: 'ETH/USDT'
					},
					{
						symbol: 'eosusdt',
						title: 'EOS/USDT'
					}
				],
				list: [],

				CraftModelList: [],
				CommonModelList: [],
				EquipmentModelList: [],
				PlanModelList: [],
				ProduceModelList: [],
				QualityModelList: [],
				ReportModelList: [],
				SystemModelList: [],
				WMSModelList: [],

				clientid: "",
				pushList: null,
				topMakretMap: {
					'market.btcusdt.detail': {},
					'market.ethusdt.detail': {},
					'market.eosusdt.detail': {}
				}
			};
		},
		filters: {
			formatChange(v) {
				return (v > 0 ? '+' : '') + parseFloat(v).toFixed(2) + '%'
			},
			formatChangeCls(v) {
				if (v == 0) {
					return ''
				} else if (v > 0) {
					return 'upper-text'
				} else {
					return 'lower-text'
				}
			},
			formatMarketcap(v) {
				return formatUnit(v);
			}
		},

		onLoad() {

		},
		onShow() {
			this.initData();
			//官网手册是用的json文件包，这个项目使用的js文件包，所以官网的在pages.json配置%tabBar.first%的形式不生效，才用这种方式强制更改
			uni.setTabBarItem({
				index: 0,
				text: this.$t('tabBar.first')
			})
			uni.setTabBarItem({
				index: 1,
				text: this.$t('tabBar.second')
			})
		},
		onUnload() {
			for (let i = 0; i < 3; i++) {
				let ch = `market.${this.topSymbols[i].symbol}.detail`
				let data = {
					"unsub": ch,
					"id": Date.now() + ""
				}
				this.$store.dispatch('WEBSOCKET_SEND', JSON.stringify(data))
				uni.$off(ch, (res) => {})
			}
		},
		methods: {
			...mapActions('AnDeng', ['CallCloseAPPPlus']),
			loadTopMarket() {
				let $this = this
				for (let i = 0; i < 3; i++) {
					let ch = `market.${this.topSymbols[i].symbol}.detail`
					let data = {
						"sub": ch,
						"id": Date.now() + ""
					}
					this.$store.dispatch('WEBSOCKET_SEND', JSON.stringify(data))
					uni.$on(ch, (res) => {
						let d = res.data
						d.tick.close = d.tick.close.toFixed(2)
						d.change = parseFloat((d.tick.close - d.tick.open) / d.tick.open * 100).toFixed(2);
						d.cny = parseFloat(d.tick.close * 7.04).toFixed(2)
						$this.topMakretMap[res.data.ch] = d
					})
				}
			},
			checkI18n(modeName, key, name) {
				const i8Data = this.$t("menu." + modeName)[key];

				if (i8Data)
					return i8Data;
				return name;
			},
			setStyle(str) { // 设置绑定 style 方法

				return 'color:' + str

			},
			async loadData() {
				this.adList().then(res => {
					let casrousels = res.data.casrousels
					this.ads = res.data.ads
				})
			},

			getModelList(mo) {
				//debugger
				if (mo == 'CraftModel') {
					return this.CraftModelList;
				} else if (mo == 'CommonModel') {
					return this.CommonModelList;
				} else if (mo == 'EquipmentModel') {
					return this.EquipmentModelList;
				} else if (mo == 'PlanModel') {
					return this.PlanModelList;
				} else if (mo == 'ProduceModel') {
					return this.ProduceModelList;
				} else if (mo == 'QualityModel') {
					return this.QualityModelList;
				} else if (mo == 'ReportModel') {
					return this.ReportModelList;
				} else if (mo == 'SystemModel') {
					return this.SystemModelList;
				} else if (mo == 'WMSModel') {
					return this.WMSModelList;
				}
			},

			getMaketList() {
				// this.marketList().then(res => {
				// 	this.markets = res.data
				// })
			},
			initData() {

				this.list = [];
				this.CraftModelList = [];
				this.CommonModelList = [];
				this.EquipmentModelList = [];
				this.PlanModelList = [];
				this.ProduceModelList = [];
				this.QualityModelList = [];
				this.ReportModelList = [];
				this.SystemModelList = [];
				this.WMSModelList = [];

				// console.log('JSON.stringify(this.loginInfo).....');
				//console.log(JSON.stringify(this.loginInfo));


				if (this.loginInfo.result == null || this.loginInfo.result == undefined) {
					uni.navigateTo({
						url: '/pages/public/login'
					})
				} else {

					var res = this.loginInfo.result.AppModelPageList;

					for (var i = 0; i < res.length; i++) {
						//生产模块
						if (res[i].OwnedPage == 'ProduceModel') {
							this.ProduceModelList.push({
								// url: "/pages/ProduceModel/" + res[i].ModelCode,
								url: "/pages/" + res[i].ModelCode,
								name: res[i].ModelName,
								code: res[i].ModelCode,
								ico: res[i].ModelIco,
								color: res[i].ModelColor,
							});
						} else if (res[i].OwnedPage == 'EquipmentModel') {
							this.EquipmentModelList.push({
								// url: "/pages/EquipmentModel/" + res[i].ModelCode,
								url: "/pages/" + res[i].ModelCode,
								name: res[i].ModelName,
								code: res[i].ModelCode,
								ico: res[i].ModelIco,
								color: res[i].ModelColor,
							});
						}
						//质量模块
						else if (res[i].OwnedPage == 'QualityModel') {
							this.QualityModelList.push({
								// url: "/pages/QualityModel/" + res[i].ModelCode,
								url: "/pages/" + res[i].ModelCode,
								name: res[i].ModelName,
								code: res[i].ModelCode,
								ico: res[i].ModelIco,
								color: res[i].ModelColor,
							});
						}
						//设备模块
						else if (res[i].OwnedPage == 'WMSModel') {
							//debugger
							this.WMSModelList.push({
								// url: "/pages/WMSModel/" + res[i].ModelCode,
								url: "/pages/" + res[i].ModelCode,
								name: res[i].ModelName,
								code: res[i].ModelCode,
								ico: res[i].ModelIco,
								color: res[i].ModelColor,
							});
						}
						//工艺模块
						// else if (res[i].OwnedPage == 'CraftModel') {
						// 	this.CraftModelList.push({
						// 		url: "/pages/CraftModel/" + res[i].ModelCode,
						// 		name: res[i].ModelName,
						// 		ico: res[i].ModelIco,
						// 		color: res[i].ModelColor,
						// 	});
						// }
						//公共模块
						// else if (res[i].OwnedPage == 'CommonModel') {
						// 	this.CommonModelList.push({
						// 		url: "/pages/CommonModel/" + res[i].ModelCode,
						// 		name: res[i].ModelName,
						// 		ico: res[i].ModelIco,
						// 		color: res[i].ModelColor,
						// 	});
						// }
						//设备模块

						//计划模块
						// else if (res[i].OwnedPage == 'PlanModel') {
						// 	this.PlanModelList.push({
						// 		url: "/pages/PlanModel/" + res[i].ModelCode,
						// 		name: res[i].ModelName,
						// 		ico: res[i].ModelIco,
						// 		color: res[i].ModelColor,
						// 	});
						// }
						//报表模块
						// else if (res[i].OwnedPage == 'ReportModel') {
						// 	this.ReportModelList.push({
						// 		url: "/pages/ReportModel/" + res[i].ModelCode,
						// 		name: res[i].ModelName,
						// 		ico: res[i].ModelIco,
						// 		color: res[i].ModelColor,
						// 	});
						// }
						// //系统模块
						// else if (res[i].OwnedPage == 'SystemModel') {
						// 	this.SystemModelList.push({
						// 		url: "/pages/SystemModel/" + res[i].ModelCode,
						// 		name: res[i].ModelName,
						// 		ico: res[i].ModelIco,
						// 		color: res[i].ModelColor,
						// 	});
						// }

					}
				};

				var res1 = this.loginInfo.result.AppModelList;
				for (var j = 0; j < res1.length; j++) {
					//debugger
					var modelList = this.getModelList(res1[j].OwnedPage);
					this.list.push({
						module: res1[j].OwnedPage,
						moduleName: res1[j].OwnedPageName,
						path: modelList,
					});
				}

			},
		},
		// #ifndef MP
		// 标题栏input搜索框点击
		onNavigationBarSearchInputClicked: async function(e) {
			this.$api.msg('点击了搜索框');
		},
		//点击导航栏 buttons 时触发
		onNavigationBarButtonTap(e) {
			const index = e.index;
			if (index === 0) {
				this.$api.msg('点击了扫描');
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
		}
		// #endif
	}
</script>

<style lang="scss">
	page {
		background: #ffffff;
		padding-top: 10upx;
	}

	.wrap {
		padding: 0rpx;
	}

	.customicon {
		font-size: 58upx;
		margin-bottom: 18upx;
	}

	.item {
		margin: 10rpx 0;
		// margin-top: 20rpx;

		&:first-child {
			margin-top: 0;
		}

		&:last-child {
			margin-bottom: 0;
		}
	}

	.title {
		font-size: 30rpx;
		position: relative;
		line-height: 1;
		padding-left: 22rpx;
		margin-left: 10rpx;
		background-color: rgb(240, 240, 240);

		&:before {
			width: 4px;
			height: 50rpx;
			border-radius: 100rpx;
			background-color: rgb(19, 128, 135);
			content: '';
			position: absolute;
			left: 0rpx;
			top: 0px;
		}
	}

	.m-t {
		margin-top: 16upx;
	}

	.advert {
		padding: 0;

		.swiper-box {
			width: 100%;
			height: 230upx;
		}

		.swiper-item {
			padding: 0;

			image {
				width: 100%;
			}
		}
	}

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

	.itemlable {
		width: 100%;
		font-size: 22rpx;
		margin-top: 13rpx;
	}

	.orderplan {
		width: 25%;
		height: 130rpx;
		text-align: center;
		float: left;
		margin-bottom: 5rpx;
		margin-top: 10rpx;
	}

	.order-section {

		//padding: 28upx 0;
		margin-top: 25rpx;
		padding: 10rpx;

		.order-itemindex {
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

		.customicon {
			font-size: 58upx;
			margin-bottom: 18upx;
			color: #2696ff;
		}


		.icon-shouhoutuikuan {
			font-size: 44upx;
		}
	}

	.scroll-view-market {
		width: 100%;
	}

	.customicon {
		font-size: 58upx;
		margin-bottom: 18upx;
	}

	.market-item {
		display: inline-block;
		width: 33%;

		.item {
			padding: 30upx 0 30upx 0;
			width: 100%;
			display: flex;
			flex-direction: column;
			justify-content: center;
			text-align: center;

			.t {
				font-weight: bold;
				font-size: $font-sm;
			}

			.c {
				padding: 10upx 0 10upx 0;
				font-size: $font-xl;
				font-weight: bold;
			}

			.b {
				font-size: $font-sm;
				color: $font-color-disabled;
			}
		}
	}

	.menu {
		padding: 20upx 0upx;
		display: flex;
		flex-direction: row;
		justify-content: space-between;
		font-size: $font-base;
		font-weight: bold;
		background: $uni-color-gap;

		.fiat {
			display: flex;
			flex-direction: row;
			flex: 1;
			align-items: center;
			background: #ffffff;
			padding-left: 30upx;
			margin-right: 10upx;

			.label {
				display: flex;
				flex-direction: column;
				padding-left: 20upx;
			}

			.sub {
				font-size: $font-sm;
				font-weight: normal;
			}

			image {
				width: 100upx;
			}

			text {
				font-size: $font-md;
			}
		}

		.ex {
			display: flex;
			flex-direction: column;
			flex: 1;

			.item {
				width: 100%;
				height: 100upx;
				line-height: 100upx;
				background: #ffffff;
				align-items: center;
				text-align: center;
				vertical-align: middle;
			}

			.shop {
				margin-top: 10upx;
			}

			image {
				vertical-align: middle;
				width: 50upx;
				height: 55upx;
				margin-right: 20upx;
			}

			.miner {
				width: 45upx;
				height: 45upx;
			}
		}
	}

	/* 头部 轮播图 */
	.carousel-section {
		position: relative;
		padding-top: 10px;
		background: #ffffff;

		.titleNview-placing {
			height: var(--status-bar-height);
			padding-top: 44px;
			box-sizing: content-box;
		}

		.titleNview-background {
			position: absolute;
			top: 0;
			left: 0;
			width: 100%;
			height: 426upx;
			transition: .4s;
		}
	}

	.carousel {
		width: 100%;
		height: 350upx;

		.carousel-item {
			width: 100%;
			height: 100%;
			padding: 0 28upx;
			overflow: hidden;
		}

		image {
			width: 100%;
			height: 100%;
			border-radius: 10upx;
		}
	}

	.swiper-dots {
		display: flex;
		position: absolute;
		left: 60upx;
		bottom: 15upx;
		width: 72upx;
		height: 36upx;
		background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMgAAABkCAYAAADDhn8LAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTMyIDc5LjE1OTI4NCwgMjAxNi8wNC8xOS0xMzoxMzo0MCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6OTk4MzlBNjE0NjU1MTFFOUExNjRFQ0I3RTQ0NEExQjMiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6OTk4MzlBNjA0NjU1MTFFOUExNjRFQ0I3RTQ0NEExQjMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTcgKFdpbmRvd3MpIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6Q0E3RUNERkE0NjExMTFFOTg5NzI4MTM2Rjg0OUQwOEUiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6Q0E3RUNERkI0NjExMTFFOTg5NzI4MTM2Rjg0OUQwOEUiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz4Gh5BPAAACTUlEQVR42uzcQW7jQAwFUdN306l1uWwNww5kqdsmm6/2MwtVCp8CosQtP9vg/2+/gY+DRAMBgqnjIp2PaCxCLLldpPARRIiFj1yBbMV+cHZh9PURRLQNhY8kgWyL/WDtwujjI8hoE8rKLqb5CDJaRMJHokC6yKgSCR9JAukmokIknCQJpLOIrJFwMsBJELFcKHwM9BFkLBMKFxNcBCHlQ+FhoocgpVwwnv0Xn30QBJGMC0QcaBVJiAMiec/dcwKuL4j1QMsVCXFAJE4s4NQA3K/8Y6DzO4g40P7UcmIBJxbEesCKWBDg8wWxHrAiFgT4fEGsB/CwIhYE+AeBAAdPLOcV8HRmWRDAiQVcO7GcV8CLM8uCAE4sQCDAlHcQ7x+ABQEEAggEEAggEEAggEAAgQACASAQQCCAQACBAAIBBAIIBBAIIBBAIABe4e9iAe/xd7EAJxYgEGDeO4j3EODp/cOCAE4sYMyJ5cwCHs4rCwI4sYBxJ5YzC84rCwKcXxArAuthQYDzC2JF0H49LAhwYUGsCFqvx5EF2T07dMaJBetx4cRyaqFtHJ8EIhK0i8OJBQxcECuCVutxJhCRoE0cZwMRyRcFefa/ffZBVPogePihhyCnbBhcfMFFEFM+DD4m+ghSlgmDkwlOgpAl4+BkkJMgZdk4+EgaSCcpVX7bmY9kgXQQU+1TgE0c+QJZUUz1b2T4SBbIKmJW+3iMj2SBVBWz+leVfCQLpIqYbp8b85EskIxyfIOfK5Sf+wiCRJEsllQ+oqEkQfBxmD8BBgA5hVjXyrBNUQAAAABJRU5ErkJggg==);
		background-size: 100% 100%;

		.num {
			width: 36upx;
			height: 36upx;
			border-radius: 50px;
			font-size: 24upx;
			color: #fff;
			text-align: center;
			line-height: 36upx;
		}

		.sign {
			position: absolute;
			top: 0;
			left: 50%;
			line-height: 36upx;
			font-size: 12upx;
			color: #fff;
			transform: translateX(-50%);
		}
	}

	/* 公告 */
	.cate-section {
		padding: 20upx 22upx 20upx 22upx;
		background: #fff;
	}

	/* 市值排行 */
	.coin-section {
		padding: 4upx 30upx 24upx;
		background: #fff;

		.s-header {
			display: flex;
			align-items: center;
			height: 30upx;
			line-height: 30upx;
			padding-top: 30upx;
			padding-bottom: 30upx;

			.col {
				font-size: $font-base;
				color: $font-color-light;
				flex: 1;
			}

			.r {
				text-align: right;
			}
		}

		.s-row {
			display: flex;
			align-items: center;
			height: 120upx;

			.subtitle {
				font-size: $font-sm;
				font-weight: normal;
				color: $font-color-light;
				padding: 4upx 0 10upx 0;
			}

			.uni-tag--success {
				color: #fff;
				background-color: $uni-color-upper;
				border-width: 0.5px;
				border-style: solid;
				border-color: $uni-color-upper;
				width: 160upx;
				float: right;
			}

			.uni-tag--error {
				color: #fff;
				background-color: $uni-color-lower;
				border-width: 0.5px;
				border-style: solid;
				border-color: $uni-color-lower;
				width: 160upx;
				float: right;
			}

			.col {
				font-size: $font-base;
				color: $font-color-dark;
				flex: 1;
			}

			.coinLogo {
				width: 36upx;
				height: 36upx;
				margin-right: 8px;
				display: inline-block;
				vertical-align: middle;
				float: left;
			}

			.light {
				font-weight: bold;
				font-size: $font-lg;
				color: $font-color-dark;
			}

			.r {
				text-align: right;
			}
		}
	}

	.f-header {
		display: flex;
		align-items: center;
		height: 140upx;
		padding: 6upx 30upx 8upx;
		background: #fff;

		image {
			flex-shrink: 0;
			width: 80upx;
			height: 80upx;
			margin-right: 20upx;
		}

		.tit-box {
			flex: 1;
			display: flex;
			flex-direction: column;
		}

		.tit {
			font-size: $font-lg +2upx;
			color: #font-color-dark;
			line-height: 1.3;
		}

		.tit2 {
			font-size: $font-sm;
			color: $font-color-light;
		}

		.icon-you {
			font-size: $font-lg +2upx;
			color: $font-color-light;
		}
	}

	/* 猜你喜欢 */
	.guess-section {
		display: flex;
		flex-wrap: wrap;
		padding: 0 30upx;
		background: #fff;

		.guess-item {
			display: flex;
			flex-direction: column;
			width: 48%;
			padding-bottom: 40upx;

			&:nth-child(2n+1) {
				margin-right: 4%;
			}
		}

		.image-wrapper {
			width: 100%;
			height: 330upx;
			border-radius: 3px;
			overflow: hidden;

			image {
				width: 100%;
				height: 100%;
				opacity: 1;
			}
		}

		.title {
			font-size: $font-lg;
			color: $font-color-dark;
			background-color: red;
			// margin-bottom: 20px;
			//line-height: 80upx;
		}

		.price {
			font-size: $font-lg;
			color: $uni-color-primary;
			line-height: 1;
		}
	}

	.workitem {
		margin: 15px;
		background-color: white;
		padding-top: 10px;
		border-radius: 10px;
	}
</style>