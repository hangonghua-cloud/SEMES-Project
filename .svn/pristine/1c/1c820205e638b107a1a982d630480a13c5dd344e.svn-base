<!-- Demo -->
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
								<u-icon name="scan" size="40" @click="ScanQR"></u-icon>
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
					<view class="s-row" style="padding-left: 0px;">
						<table style="width: 100%;">
							<tr>
								<td>
									<view class="s-row s-rowP" style="" @click="itemshowclick(i)">
										<view style="font-weight: 900;">
											<image src="/static/bus7.png" class="coinLogo"></image>
											<text class="coin">{{item.BusSN}}</text>
										</view>
										<view class="col r light">
											<u-button shape="circle" size="mini" plain style="color:ForestGreen;
												width:100rpx;height:40rpx;background-color:Chartreuse;">{{item.Status}}</u-button>
										</view>
									</view>
									<view @click="actionShowClick(i,item.Status)">
										<view class="s-row">
											<view class=" Itemlable">工序名称：</view>
											<view class=" Itemvalue">{{item.ProcessName }} | {{item.MLineName }}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">销售订单：</view>
											<view class=" Itemvalue">{{item.OrderNo }}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">生产工单：</view>
											<view class=" Itemvalue">{{item.WONo}}</view>
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
				form: {
					OrderNo: "",
					BusSN: "",
					WONo: "",
					Status: "已锁定",
				},
				searchlistshow: false,
				actionShow: false,
				searchmodel: "",
				testShow: -1,
				testShowi: -1,
				selectshow: false,
				selectlist0: [
					{
						text: '销售订单',
					},
					{
						text: '生产工单',
					}
				],
				actioni: "",

				actionList: [{
					text: '测试功能',
					color: '#55aaff',
				}],
				SeachTit: "",
				defaultseach: 0,

				borderBottom: false,
				activeColor: '#138087',
				imglistshow: true,

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
			this.SeachTit = this.selectlist0[0].text;
		},

		methods: {
			...mapActions('WMS', ['CallSaveDemoData']),
			...mapActions('common', ['CallGetEntity']),
			mescrollInit() {

			},
			Selclose() {
				// console.log('close');
			},
			Selopen() {
				// console.log('open');
			},
			SeachChange(value) {
				//---------文本查询改变------------
				this.SeachTit = this.selectlist0[value].text;
				if (this.searchmodel != "")
					this.search(this.searchmodel);
			},
			search(value) {
				//------------查询条件-------------

				this.searchmodel = value;

				if (this.searchmodel != "") {
					this.mescroll.resetUpScroll();
				}
			},
			clear(type) {
				this.form.WONo = "";
				this.form.OrderNo = "";
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

			itemshowclick(id) {
				this.testShow = id;
				if (this.testShowi == id) {
					this.testShow = -1;
					id = -1;
				}
				this.testShowi = id;
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
				//this.clear();
				switch (this.SeachTit) {
					case "生产工单":
						this.form.WONo = this.searchmodel;
						break;
					case "销售订单":
						this.form.OrderNo = this.searchmodel;
						break;
				}
				// this.form.WorkShop = this.loginInfo.result.WorkShop;
				// this.form.Station = this.loginInfo.result.Station;
				this.CallGetEntity({
					TableName: "DemoEntity",
					Params: this.form,
					Current: this.page.page,
					Page: this.page.limit,
					OrderBy: "",
					Sort: ""
				}).then(res => {
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
						res.Message = '消息提示！！！！！！！！！';
						this.mescroll.ShowModalBox(this, res.Message, "error", 20);
						this.mescroll.endSuccess(0);
					}
				});
			},
			actionShowClick(index, Status) {
				//--------------事件点击---------------------
				var btn1 = false;
				if (this.loginInfo.result.ActionRole.length > 0) {
					for (var i = 0; i < this.loginInfo.result.ActionRole.length; i++) {
						if (this.loginInfo.result.ActionRole[i].ModelCode == 'OnLine_OnLine') {
							btn1 = true;
							break;
						}
					}
				}
				if (Status == "已锁定" && btn1) {
					this.actionShow = true;
					this.actioni = index;
				}
			},
			actionClick(value) {
				if (this.actioni < 0) {
					this.mescroll.ShowModalBox(this, "请选择单据", "error", 20);
					// this.$refs.uToast.show({
					// 	title: `请选择单据`,
					// 	type: 'error'
					// });
				} else {
					this.CallSaveDemoData({
						"RecordId": this.list[this.actioni].Id,
						"Mline": this.list[this.actioni].MLine,
						"UserCode": this.loginInfo.result.UserCode + "---" + this.loginInfo.result.UserName,
					}).then(res => {
						if (res.Success) {
							// this.$refs.uToast.show({
							// 	title: `操作成功!`,
							// 	type: 'success'
							// });
							this.mescroll.ShowModalBox(this, "操作成功！", "success", 20);
							this.actioni = "";
							this.mescroll.resetUpScroll();
						} else {
							this.mescroll.ShowModalBox(this, `操作失败!` + res.Message, "error", 20);
							// this.$refs.uToast.show({
							// 	title: `操作失败!` + res.Message,
							// 	type: 'error'
							// });
						}
					});

				}
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
