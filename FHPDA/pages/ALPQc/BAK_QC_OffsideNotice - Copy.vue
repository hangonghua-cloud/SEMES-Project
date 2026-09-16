<template>
	<view>
	<view class="u-tabs-box">
		<u-tabs-swiper activeColor="#138087" ref="tabs" :list="tablist" :current="current" @change="change" :is-scroll="false" swiperWidth="750"></u-tabs-swiper>
	</view>
		
		
		<u-button @click="btnClick">唤起弹窗</u-button>
		<u-popup border-radius="10" v-model="show" 
							@close="close" @open="open" :mode="mode" 
							length="50%" :mask="mask"
							:closeable="closeable"
							:close-icon-pos="closeIconPos"
						>
							
			<view class="close-btn" v-if="mode != 'center'">
		    <u-button size="medium" @click="show = false;"> 保存</u-button>
			</view>
		</u-popup>
		
		<swiper class="swiper-box" :current="swiperCurrent" @transition="transition" @animationfinish="animationfinish">
			<swiper-item class="swiper-item">
				<scroll-view scroll-y style="height: 100%;width: 100%;" @scrolltolower="reachBottom">
					<view class="page-box">
						<view>11111</view>
						<u-loadmore :status="loadStatus[0]" bgColor="#f2f2f2"></u-loadmore>
					</view>
				</scroll-view>
			</swiper-item>
			<swiper-item class="swiper-item">
				<scroll-view scroll-y style="height: 100%;width: 100%;" @scrolltolower="reachBottom">
					<view class="page-box">
							<view>2222222222</view>
						<u-loadmore :status="loadStatus[1]" bgColor="#f2f2f2"></u-loadmore>
					</view>
				</scroll-view>
			</swiper-item>
     	</swiper>
		
		<!-- <mescroll-body ref="mescrollRef" :down="downOption" :up="upOption" @init="mescrollInit" :auto="false" @down="downCallback"
		 @up="upCallback">
		
			<view v-for="(data, i) in list" :key="data.id">

				<view class="order-item little-line">
					<view class="row user-info">
						<view class="name">
							<view class="profile">工</view>{{data.WONo}}
							
						</view>
<view class="nomarl"><button class="btn sell"  v-if="data.OffsideNoticeFilePath != null" @click="buy">查看图片</button>
							</view>
					</view>
					<view class="row">
						<view class="nomarl">工序{{(data.WorkShopName) }}</view>
						<view class="nomarl">销售订单</view>
					</view>
					<view class="row">
						<view class="price">整车序列号{{data.BusSN}}</view>
						<view class="nomarl"> {{data.SalesDisVoucherProNo}}</view>

					</view>
					

				</view>

			
			</view>
		</mescroll-body> -->
		
	</view>
</template>

<script>
	
	
	
	import MescrollBody from "@/components/mescroll-uni/mescroll-body.vue";
	import MescrollMixin from "@/components/mescroll-uni/mescroll-mixins.js";
	
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	export default {
		mixins: [MescrollMixin, commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			MescrollBody
		},
		data() {
			return {
				page: {
					page: 1,
					limit: 10,
				},
				list: [[],[]], // 数据列表
				downOption: {
					auto: true // 不自动加载 (mixin已处理第一个tab触发downCallback)
				},
				upOption: {
					auto: true, // 不自动加载
					noMoreSize: 5, //如果列表已无数据,可设置列表的总数量要大于半页才显示无更多数据;避免列表数据过少(比如只有一条数据),显示无更多数据会不好看; 默认5
					page: {
						num: 0, // 当前页码,默认0,回调之前会加1,即callback(page)会从1开始
						size: 10 // 每页数据的数量
					},
					empty: {
						tip: '~ 空空如也 ~'
					}
				},
			
				//sit页
				tablist: [
					{
						name: '已上传'
					},
					{
						name: '未上传'
					},
				],
				current: 0,
				swiperCurrent: 0,
				tabsHeight: 0,
				dx: 0,
				loadStatus: ['loadmore','loadmore'],
				//查询条件框
				show: false,
				 mode: 'left',
				 mask: true, // 是否显示遮罩
				 closeable: true,
				 closeIconPos: 'top-right'
			}
		},
		onLoad() {
			 console.log('onLoad');
			// this.fiatList().then(res => {
			// 	this.fiatCoins = res.data
			// 	this.page.coin = this.fiatCoins[0]
			// 	this.mescroll.resetUpScroll()
			// }).catch(error => {})
		},
		onShow() {
			console.log('onShow');
			// uni.$on("refresh", (res) => {
			// 	this.mescroll.resetUpScroll()
			// })
			// uni.$on("filter", (res) => {
			// 	this.page.price = res.price
			// 	this.page.payment = res.payment
			// 	this.mescroll.resetUpScroll()
			// })
			// this.$fire.$emit('refreshCoin')
		},
		onUnload() {
			console.log('onUnload');
			// uni.$off("refresh", (res) => {})
			// uni.$off("filter", (res) => {})
		},
		methods: {
			...mapActions('common', ['fiatList']),
			...mapActions('otc', ['advertList']),
			...mapActions('QC', ['CallGetEntity']),
			mescrollInit() {
				upCallback({num:1,page:10});
			},
			//查询栏方法
			close() {
				// console.log('close');
			},
			open() {
				// console.log('open');
			},
			btnClick() {
				this.show = true;
			},
			// sit栏切换
			change(index) {
				this.swiperCurrent = index;
			
				this.getOrderList(this.swiperCurrent);
			},
			transition({ detail: { dx } }) {
				this.$refs.tabs.setDx(dx);
			},
			animationfinish({ detail: { current } }) {
				
				this.$refs.tabs.setFinishCurrent(current);
				this.swiperCurrent = current;
				this.current = current;
			},
			reachBottom() {
				// 此tab为空数据
				if(this.current != 2) {
					this.loadStatus.splice(this.current,1,"loading")
					setTimeout(() => {
						this.getOrderList(this.current);
					}, 1200);
				}
			},
			// 页面数据
			getOrderList(idx) {
				// for(let i = 0; i < 5; i++) {
				// 	let index = this.$u.random(0, this.dataList.length - 1);
				// 	let data = JSON.parse(JSON.stringify(this.dataList[index]));
				// 	data.id = this.$u.guid();
				// 	this.orderList[idx].push(data);
				// }
				// this.loadStatus.splice(this.current,1,"loadmore")
			},
			/*下拉刷新的回调 */
			downCallback() {
				// 这里加载你想下拉刷新的数据, 比如刷新轮播数据
				// loadSwiper();
				// 下拉刷新的回调,默认重置上拉加载列表为第一页 (自动执行 page.num=1, 再触发upCallback方法 )
				this.mescroll.resetUpScroll();
			},
			/*上拉加载的回调: 其中page.num:当前页 从1开始, page.size:每页数据条数,默认10 */
			upCallback(page) {
				alert(123);
				if (page.num <= 1) {
					this.list = [];
				}
				this.page.page = page.num;
				console.log(this.page.page)
			

			
					// this.$u.post('http://localhost:52521/api/Test/CallGetEntity', {
					// 	TableName: "WorkOrderEntity",
					// 	Current: this.page.page,
					// 	Page: this.page.limit,
					//     OrderBy:"OffsideNoticeFilePath",
					//     Sort:"asc"
					// }).then(res => {
					// 	$this.mescroll.endSuccess(res.result.length);
					// 	$this.list = $this.list.concat(res.result);
					
					// });
				
					this.CallGetEntity({
						TableName: "WorkOrderEntity",
						Current: this.page.page,
						Page: this.page.limit,
						OrderBy:"OffsideNoticeFilePath",
						Sort:"asc"
					}).then(res => {
						
						this.mescroll.endSuccess(res.result.length);
						this.list[0] = this.list.concat(res.result);
					});
			
			},
			//顶部tab点击
			sideTabClick(side, index) {
				this.sideIndex = index
				this.page.side = side
				this.mescroll.resetUpScroll()
			},
			//顶部tab点击
			coinTabClick(index) {
				this.coinIndex = index;
				this.page.coin = this.fiatCoins[index]
				this.mescroll.resetUpScroll()
			},
			filter() {
				uni.getSubNVueById('otcFilterDrawer').show('slide-in-right', 200);
			},
		}
	}
</script>

<style lang="scss" scoped>
	.order-item {
		width: 100%;
		padding: 20upx $page-row-spacing;

		.user-info {
			display: flex;
			flex-direction: row;
			justify-content: space-between;
			align-items: center;
			height: 80upx;
			line-height: 80upx;

			.name {
				font-size: $font-md;
				font-weight: bold;
				display: flex;
				flex-direction: row;
				align-items: center;
			}

			.profile {
				width: 50upx;
				height: 50upx;
				line-height: 50upx;
				border-radius: 50%;
				text-align: center;
				background: $uni-color-blue;
				color: #fff;
				font-weight: 100;
				font-weight: bold;
				font-size: $font-md;
				margin-right: 10upx;
			}
		}

		.opt {
			margin: 20upx 0;
		}

		.row {
			width: 100%;
			display: flex;
			flex-wrap: wrap;
			justify-content: space-between;
			padding: 2upx 0;
			align-items: flex-end;

			.price {
				color: $font-color-blue;
			}

			.nomarl {
				font-size: $font-sm;
				color: $font-color-light;
			}

			.pay {
				image {
					width: 25px;
					height: 25px;
				}
			}

			.buy {
				background: $uni-color-blue;
			}

			.sell {
				background: #475F78;
			}

			.btn {
				border: 0;
				color: #fff;
				font-size: $font-sm;
				height: 60upx;
				line-height: 60upx;
				padding: 0 50upx;
			}
		}
	}

	.box {
		background: #fff;
		display: flex;
		flex-direction: column;
		height: 280px;
		padding: 30upx 30upx;
		font-size: $font-base;
		color: $font-color-light;

		.coin {
			display: flex;
			flex-direction: row;
			flex-wrap: wrap;
			justify-content: space-between;
			margin-bottom: 10upx;

			.name {
				font-size: $font-lg;
				color: $font-color-base;
				padding-bottom: 10upx;
			}

			.price {
				color: $font-color-blue;
			}

			.icon {
				width: 40px;
				height: 40px;
			}
		}

		.type {
			display: flex;
			flex-direction: row;
			padding: 10upx 0;

			view {
				margin-right: 30upx;
				position: relative;
				padding: 10upx 0;

				&.active {
					color: $uni-color-blue;

					&:after {
						content: '';
						position: absolute;
						left: 50%;
						bottom: 0;
						transform: translateX(-50%);
						width: 100%;
						height: 0;
						border-bottom: 2px solid $uni-color-blue;
					}
				}
			}
		}

		.input {
			display: flex;
			flex-direction: row;
			justify-content: space-between;
			width: 100%;
			padding: 14upx 10upx;
			margin: 20upx 0;
			border: 1upx solid #8B9FAA;

			input {
				color: $font-color-light;
				font-size: $font-base;
			}

			.cny {
				margin-right: 20upx;
				color: $font-color-base;
			}

			.all {
				margin-left: 20upx;
				color: $font-color-blue;
			}
		}

		.amount {
			display: flex;
			flex-direction: row;
			flex-wrap: wrap;
			justify-content: space-between;
			padding: 10upx 0;

			.p {
				font-size: $font-lg;
				color: $font-color-blue;
			}
		}

		.btns {
			display: flex;
			flex-direction: row;
			justify-content: space-between;
			width: 100%;
			padding: 10upx 0;

			.btn {
				display: block;
				width: 48%;
				height: 70upx;
				line-height: 70upx;
				text-align: center;
				color: #fff;
				border-radius: 0;
				font-size: $font-base;
			}

			.cancel {
				background: #96A7BA;
			}

			.submit {
				background: $uni-color-blue;
			}
		}
	}
	.wrap {
		display: flex;
		flex-direction: column;
		height: calc(100vh - var(--window-top));
		width: 100%;
	}
	.swiper-box {
		flex: 1;
	}
	.swiper-item {
		height: 100%;
	}
</style>
