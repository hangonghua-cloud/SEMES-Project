<!-- 越位通知单管理 -->
<template>
	<view style="background-color: #F9F9F9;">
		<mescroll-body ref="mescrollRef" :down="downOption" :up="upOption" @init="mescrollInit" :auto="false" @down="downCallback"
		 @up="upCallback">
			<view class="" style="padding:0 10rpx 0 10rpx;">
				<view class="" style="width:85%;float:left;">
					<view class="upseach" style="">
						<view class="upseach-seach">
							<u-search v-model="searchmodel" @custom="custom" @search="search" v-bind:placeholder="SeachTit" @clear="clear"
							 :show-action="showAction=false">
							</u-search>
						</view>
						<view class="upseach-ico-plan" style="">
							<view class="upseach-ico" style="">
								<u-icon name="scan" size="40"></u-icon>
							</view>
						</view>
						<view class="" style="clear:both;"></view>
					</view>
				</view>
				<view class="" style="width:15%;float:left;">
					<image class="" style="width:45rpx;height:45rpx;margin:18rpx 0 0 30rpx;" @click="searchlistshow = true" src="@/static/filter5.png"></image>
				</view>
				<view class="" style="clear:both;"></view>
			</view>

			<u-toast ref="uToast"></u-toast>
			<u-action-sheet :list="selectlist0" v-model="searchlistshow" @click="SeachChange" border-radius="20"></u-action-sheet>
			<u-action-sheet :list="actionList" v-model="actionShow" @click="actionClick" border-radius="20"></u-action-sheet>
			<view style="display: block;float: none; width: 10px; clear: both;"></view>
			<view class="coin-section m-t" style="background-color: #F9F9F9;">
				<view v-for="(item, i) in list" :key="item.id" class="block little-line listitem">
					<view class="s-row" style="padding-left: 0rpx;">
						<table style="width: 100%;">
							<tr>
								<td>
									<view class="s-row s-rowP" style="" @click="itemshowclick(i)">
										<view style="font-weight: 900;">
											<image src="/static/bus7.png" class="coinLogo"></image>
											<text class="coin">{{item.BusSN}}</text>
										</view>
										<!-- <view class="col r light">
											<u-icon name="arrow-right" size="30" style="margin-left: 3rpx;" v-if=" testShow != i"></u-icon>
											<u-icon name="arrow-down" size="30" style="margin-left: 3rpx;" v-if=" testShow == i"></u-icon>
										</view> -->
									</view>
									<view @click="actionclick(i)">
										<view class="s-row">
											<view class=" Itemlable">工序名称：</view>
											<view class=" Itemvalue">{{item.ProcessName }}</view>
										</view>
										<!-- 	<view class="s-row">
											<view class=" Itemlable">销售订单：</view>
											<view class=" Itemvalue">{{item.SalesDisVoucherProNo }}</view>
										</view> -->
										<view class="s-row">
											<view class=" Itemlable">工单：</view>
											<view class=" Itemvalue">{{item.WONO}}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">焊装决策：</view>
											<view class=" Itemvalue">{{item.HzDecision}}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">涂装决策：</view>
											<view class=" Itemvalue">{{item.TzDecision}}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">总装决策：</view>
											<view class=" Itemvalue">{{item.ZzDecision}}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">试交决策：</view>
											<view class=" Itemvalue">{{item.SjDecision}}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">总检状态：</view>
											<view class=" Itemvalue">{{item.EntireStatus}}</view>
										</view>
									</view>
								</td>
							</tr>
						</table>
					</view>
				</view>
			</view>
			<!-- 分页的数据列表 -->
		</mescroll-body>

	</view>
</template>
<link href="main.css" rel="stylesheet" />
<script>
	import MescrollBody from "@/components/mescroll-uni/mescroll-body.vue";
	import MescrollMixin from "@/components/mescroll-uni/mescroll-mixins.js";
	import $ from "@/utils/jquery-3.4.1.js";

	import {
		mapState,
		mapActions,

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
				list: [], // 数据列表
				downOption: {
					auto: false // 不自动加载 (mixin已处理第一个tab触发downCallback)
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
				tablist: [{
						name: '待投产'
					},
					{
						name: '待下线'
					},
					{
						name: '待质检'
					},
					{
						name: '已完成'
					}
				],
				//标签
				type: "primary",
				shape: "circle",
				closeable: true,

				selectTagList: [],

				//查询条件框
				Selshow: false,
				Selmode: 'left',
				Selmask: true, // 是否显示遮罩
				Selcloseable: true,
				SelcloseIconPos: "",
				//操作页面框
				Upshow: false,
				Upmode: 'right',
				Upmask: true, // 是否显示遮罩
				Upcloseable: true,
				UpcloseIconPos: "",
				searchpla: "生产工单",
				form: {
					SalesDisVoucherProNo: "",
					BusNo: "",
					WONo: "",
					WorkShopCode: "",
					WorkShopCodeCN: "--请选择工序--",
					SalesOrderNo: "",
					WorkOrderNo: "",
				},
				up: {
					Ids: null,
					Name: null,
					OffsideNoticeBy: null,
					Contents: null,
				},
				searchlistshow: false,
				actionShow: false,
				searchmodel: "",
				testShow: -1,
				testShowi: -1,
				selectshow: false,
				selectlist0: [{
						text: '整车序列号',

					},
					{
						text: '销售订单',
					},
					{
						text: '生产工单',
					}
				],
				actionList: [{
					text: '总检合格',
					color: 'green',
				}],
				SeachTit: "",
				defaultseach: 0,
				selectlist1: [{
						label: '车间1',
						value: '车间1',
					},
					{
						label: '车间2',
						value: '车间2',
					},
					{
						label: '车间3',
						value: '车间3',
					}
				],
				borderBottom: false,
				activeColor: '#138087',
				imglistshow: true,
				actionI: -1,
				hoverClass: 'hover2',
				itemStyle: {

				},
				key: true
			}
		},
		onLoad() {
			//不是登录状态的时候
			if (!this.loginInfo.hasLogin) {
				uni.navigateTo({
					url: '/pages/public/login'
				})
				return;
			}
			this.SeachChange(this.defaultseach);
			debugger
		},
		onShow() {
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
			// uni.$off("refresh", (res) => {})
			// uni.$off("filter", (res) => {})
		},
		onNavigationBarButtonTap(e) {
			//下标从右开始
			const index = e.index;
			if (index === 0) {
				//添加
				this.ShowHideUp(true);
			} else if (index === 1) {
				//查询
				this.ShowHideSel();
			}
		},
		methods: {
			...mapActions('QC', ['CallGetEntireInspectionList']),
			...mapActions('QC', ['CallAddOrUpdataEntireInspectionState']),
			mescrollInit() {

			},
			Selclose() {
				// console.log('close');
			},
			Selopen() {
				// console.log('open');
			},



			actionClick(value) {
				var btn1 = false;
				if (this.loginInfo.result.ActionRole.length > 0) {
					for (var i = 0; i < this.loginInfo.result.ActionRole.length; i++) {
						if (this.loginInfo.result.ActionRole[i].ModelCode == 'QC_TransitionDecision_CallInspectionResultDecision') {
							btn1 = true;
							break;
						}
					}
				}
				this.Decision();
			},
			Decision() {
				//this.actionI
				debugger
				this.CallAddOrUpdataEntireInspectionState({
					"WONO": this.list[this.actionI].WONO,
					"SalesOrderNo": this.list[this.actionI].SaleOrderNo,
					"HzDecision": this.list[this.actionI].HzDecision,
					"TzDecision": this.list[this.actionI].TzDecision,
					"ZzDecision": this.list[this.actionI].ZzDecision,
					"SjDecision": this.list[this.actionI].SjDecision,
					"EntireStatus": "合格",
					"UserCode": this.loginInfo.result.UserCode + "---" + this.loginInfo.result.UserName,
				}).then(res => {
					if (res.Success) {
						this.$refs.uToast.show({
							title: '判定成功！',
							type: 'success'
						})
						this.mescroll.resetUpScroll();
					} else {
						// this.$refs.uToast.show({
						// 	title: res.Message,
						// 	type: 'error'
						// });
						this.mescroll.ShowModalBox(this, res.Message, "error", 20);
					}
				});


			},
			yellow() {
				this.$refs.uToast.show({
					title: '存在不合格项目，判签失败！',
					type: 'warning'
				})
			},
			red() {
				this.$refs.uToast.show({
					title: '红签判定成功！',
					type: 'success'
				})
			},
			itemshowclick(id) {
				debugger
				this.testShow = id;
				if (this.testShowi == id) {
					this.testShow = -1;
					id = -1;
				} else {

				}
				this.testShowi = id;
			},
			actionclick(i) {
				debugger
				this.actionShow = true;
				this.actionI = i;

			},
			SeachChange(value) {
				//---------文本查询改变------------
				this.SeachTit = this.selectlist0[value].text;
				if (this.searchmodel != "")
					this.search(this.searchmodel);
			},
			search(value) {
				//------------查询条件-------------
				this.clear();
				this.searchmodel = value;


				if (this.searchmodel != "") {
					this.mescroll.resetUpScroll();
				}
			},
			clear(type) {
				this.form.WorkOrderNo = "";
				this.form.BusNo = "";
				this.form.SalesOrderNo = "";
				if (type == undefined)
					this.searchmodel = "";
			},
			ScanQR() {
				var self = this;
				// 允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search(res.result);
					}
				});
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
				//不是登录状态的时候
				if (!this.loginInfo.hasLogin) {
					uni.navigateTo({
						url: '/pages/public/login'
					})
					return;
				}
				if (page.num <= 1) {
					this.list = [];
				}
				this.page.page = page.num;
				this.clear(true);
				switch (this.SeachTit) {
					case "生产工单":
						this.form.WorkOrderNo = this.searchmodel;
						break;
					case "整车序列号":
						this.form.BusNo = this.searchmodel;
						break;
					case "销售订单":
						this.form.SalesOrderNo = this.searchmodel;
						break;
				}
				this.form.WorkShop = this.loginInfo.result.WorkShop;
				debugger
				this.CallGetEntireInspectionList({
					"data":{}
				}).then(res => {
					debugger
					if (res.Success) {
						if (res.result == null || res.result.length == 0)
							this.mescroll.endSuccess(0);
						else {
							this.list = this.list.concat(res.result);
							this.mescroll.endSuccess(res.result.length);
						}
					} else {
						// this.$refs.uToast.show({
						// 	title: res.Message,
						// 	type: 'error'
						// });
						this.mescroll.ShowModalBox(this, res.Message, "error", 20);
						this.mescroll.endSuccess(0);
					}
				});


				// var res = [];
				// for (var i = 0; i < 15; i++) {
				// 	res = res.concat({
				// 		"id": i,
				// 		"checklist": false,
				// 		"OffsideNoticeFilePath": "1111",
				// 		"WorkShopName": "海辰科技总装车间",
				// 		"BusSN": "B571205041B00",
				// 		"SalesDisVoucherProNo": "0020012537",
				// 		"WONo": "0020012537_000" + i,
				// 		"WKStation": "VIN打码工位" + i,
				// 		icon: "/static/bus8.png",

				// 	});

				// }
				// this.list = this.list.concat(res);
				// this.mescroll.endSuccess(res.length);
			},

			handleUpload() {
				this.$upload(this.uploadSuccess, this.uploadProgress)
			},
			uploadSuccess(res) {
				this.form.payQrcode = res.url
			},
			uploadProgress(res) {
				console.log("上传进度:", res)
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


<style>
	.wrap {
		display: flex;
		flex-direction: column;
		height: calc(100vh - var(--window-top));
		width: 100%;
	}

	.content {
		padding: 5%;
		text-align: center;
	}

	.select_item {
		padding: 20rpx;
		font-size: 30rpx;
		border-bottom: 1rpx solid LightGray;
	}

	.showDetail_css {
		padding: 5rpx;
		margin: 0rpx 10rx;
		border-radius: 10rpx;
		/* background-color: CornflowerBlue; */
	}

	.detail_lab {
		margin: 2rpx;
		width: 25%;
		font-size: 15rpx;
	}

	.detail_input {
		margin: 2rpx;
		width: 73%;
		font-size: 15rpx;
	}
</style>
