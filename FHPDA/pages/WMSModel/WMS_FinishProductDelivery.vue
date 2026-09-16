<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<u-form-item :label="$t('WMS_FinishProductDelivery.ProductOrder')">
					<u-input v-model="form.ProductOrder" type="text"
						:placeholder="$t('WMS_FinishProductDelivery.ProductOrder_placeholder')" border disabled
						class="readonly" />
					<u-icon name="search" size="70rpx" color="#138087" @click="showProductOrderDialog=true"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('WMS_FinishProductDelivery.ContainerNO')">
					<u-input v-model="form.ContainerNO" type="number"
						:placeholder="$t('WMS_FinishProductDelivery.ContainerNO_placeholder')" border disabled
						class="readonly" />
					<u-icon name="search" size="70rpx" color="#138087" @click="showContainerNoDialog=true"></u-icon>
				</u-form-item>

				<u-form-item :label="$t('WMS_FinishProductDelivery.InfoTitle')" style="height: auto;">
					<view style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('WMS_FinishProductDelivery.DocNum')}}：{{form.DocNum}}</view>
						<!-- <view class="label">
							{{$t('WMS_FinishProductDelivery.CreateTime')}}：{{formatTime(form.CreateTime)}}
						</view> -->
						<!-- <view class="label">{{$t('WMS_FinishProductDelivery.ProductOrder')}}：{{form.ProductOrder}}
						</view>
						<view class="label">{{$t('WMS_FinishProductDelivery.ContainerNO')}}：{{form.ContainerNO}}</view> -->
						<view class="label">{{$t('WMS_FinishProductDelivery.CustomerPO')}}：{{form.CustomerPO}}</view>
						<view class="label">{{$t('WMS_FinishProductDelivery.BoxNum')}}：{{form.BoxNum}}</view>
						<view class="label">{{$t('WMS_FinishProductDelivery.PalletQty')}}：{{form.PalletQty}}</view>
					</view>
				</u-form-item>
			</u-form>
			<scroll-view scroll-y="true" class="scroll-Y">
				<view style="min-height: 200rpx;max-height: 400rpx;border: 1px solid Gainsboro;">
					<u-table>
						<u-tr class="u-tr">
							<u-th>{{$t('WMS_FinishProductDelivery.MaterialCode')}}</u-th>
							<u-th>{{$t('WMS_FinishProductDelivery.PalletQty')}}</u-th>
						</u-tr>
						<u-tr v-for="(item,index) of DispatchDetailList" :key="index">
							<u-th>{{item.MaterialCode}}</u-th>
							<u-th>{{item.PalletQty}}</u-th>
						</u-tr>
					</u-table>
				</view>
			</scroll-view>
			<u-form :model="form3">
				<u-form-item :label="$t('WMS_FinishProductDelivery.MarkCode')">
					<u-search v-model="form3.MarkCode" @search="codeScan" @clear="clear" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
			</u-form>
			<scroll-view scroll-y="true" class="scroll-Y">
				<view style="min-height: 200rpx;border: 1px solid Gainsboro;">
					<u-table>
						<u-tr class="u-tr">
							<u-th>{{$t('WMS_FinishProductDelivery.MarkCode')}}</u-th>
							<u-th>{{$t('WMS_FinishProductDelivery.MarkName')}}</u-th>
						</u-tr>
						<u-tr v-for="(item,index) of markList" :key="index">
							<u-th>{{item.MarkCode}}</u-th>
							<u-th>{{item.MarkName}}</u-th>
						</u-tr>
					</u-table>
				</view>
			</scroll-view>
		</view>

		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="delivery()" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_FinishProductDelivery.SaveBtn')}}</text>
			</u-button>
		</view>

		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<!-- 订单选择弹窗 -->
		<u-popup v-model="showProductOrderDialog" mode="right" length="60%">
			<u-form :model="form2" ref="uForm">
				<u-input v-model="form2.ProductOrder" type="text"
					:placeholder="$t('WMS_FinishProductDelivery.ProductOrder_placeholder')" border
					style='margin: 5px;' />
			</u-form>
			<view style="display: flex;margin-left: 5px;margin-right: 5px;">
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='return'
					@click="showProductOrderDialog=false"
					size="return">{{$t('WMS_FinishProductDelivery.CancelBtn')}}</u-button>
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='submits'
					@click="getProductOrderList" size="default">{{$t('WMS_FinishProductDelivery.SearchBtn')}}
				</u-button>
			</view>
			<view class="container">
				<view class="item" v-for="(item,index) of OrderList" @click="selectProductOrder(item.value)"
					:key='index'>
					<view style="border:1px solid white;">
						<view class="name" style="background-color: Gainsboro;padding: 10rpx;width: 200px;">
							{{item.value}}
						</view>
					</view>
				</view>
			</view>
			<view style="height: 9%;"></view>
		</u-popup>
		<!-- 柜号选择弹窗 -->
		<u-popup v-model="showContainerNoDialog" mode="right" length="60%">
			<view class="container">
				<view class="item" v-for="(item,index) of ContainerNOList" @click="selectContainerNo(item.value)"
					:key='index'>
					<view style="border:1px solid white;">
						<view class="name" style="background-color: Gainsboro;padding: 10rpx;width: 200px;">
							{{item.value}}
						</view>
					</view>
				</view>
			</view>
			<view style="height: 9%;"></view>
			<view class="" style="display: flex;">
				<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
					:custom-style="{width: '80%',height: '70rpx',borderRadius: '10rpx'}"
					@click="showContainerNoDialog=false" style="position: fixed;bottom: 30rpx;margin-left: 10%;">返回
				</u-button>
				<!-- <u-button :type="'primary'" :ripple="true" ripple-bg-color="#00aa00"
					:custom-style="{width: '35%',height: '78rpx',borderRadius: '10rpx'}" @click="addBadItem()"
					style="position: fixed;bottom: 30rpx;margin-left: 52%;">确定
				</u-button> -->
			</view>
		</u-popup>
	</view>
</template>

<script>
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	import scanCode from '@/components/scanCode/scanCode.vue'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				form: {
					Id: "",
					PackTransferCode: "", //唛头码
					DocNum: "", //发货单号
					CreateTime: "", //单据日期
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					CustomerPO: "", //po号
					BoxQty: "", //盒数
					PalletQty: "", //托数
				},
				form2: {
					ProductOrder: ""
				},
				form3: {
					MarkCode: ""
				},
				chkAll: false,

				gridList: [],
				showContainerNoDialog: false, //柜号弹窗
				showProductOrderDialog: false, //订单号弹窗
				OrderList: [], //订单号列表
				ContainerNOList: [], //柜号列表
				DispatchDetailList: [], //发货详情
				markList: [], //唛头列表
				rules: {
					code: [{
						required: true,
						message: '请输入姓名',
						trigger: 'blur,change'
					}],
					intro: [{
						min: 5,
						message: '简介不能少于5个字',
						trigger: 'change'
					}]
				},
				//焦点
				focus1: false,
			}
		},

		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		//预加载
		onLoad() {
			_self = this;
			this.getProductOrderList();
		},

		onNavigationBarButtonTap: function(option) {
			//点击事件
			console.info('点击查询按钮事件', JSON.stringify(option));
			uni.navigateTo({
				url: '/pages/WMSModel/WMS_FinishProductDeliverySel'
			})
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.WMSModel.WMSModel/WMS_FinishProductDelivery")
			});
		},
		methods: {

			...mapActions('WMS', ['ProductDispatchProductOrder', 'ProductDispatchContainerNO', 'ProductDispatchInfo',
				'ProductDispatchMarkScan', 'ProductDispatchMarkBook'
			]),
			...mapActions('common', ['GetProcessModel']),

			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.codeScan(res.result);
					}
				});
			},

			//条码查询
			codeScan(value) {
				this.form3.MarkCode = value;
				if (this.form3.MarkCode) {
					this.markScan();
				}
			},

			clear() {
				this.form3.MarkCode = "";
			},
			//初始化订单号列表
			getProductOrderList() {
				var data = {
					key: this.form2.ProductOrder
				};
				this.ProductDispatchProductOrder(data).then(res => {
					this.OrderList = [];
					if (res.success) {
						this.OrderList = res.resultData;
					}
				});
			},
			//初始化柜号列表
			getContainerNOList() {
				if (!this.form.ProductOrder) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishProductDelivery.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				let data = {
					productOrder: this.form.ProductOrder
				}
				this.ProductDispatchContainerNO(data).then(res => {
					this.ContainerNOList = [];
					if (res.success && res.resultData) {
						res.resultData.forEach((item, index) => {
							this.ContainerNOList.push({
								value: item.value,
								label: item.label
							});
						});
					}
				});
			},
			//弹窗选择订单
			selectProductOrder(value) {
				this.form.ProductOrder = value;
				this.showProductOrderDialog = false;
				this.form.ContainerNO = "";
				_self.getContainerNOList();
			},

			//弹窗选择柜号
			selectContainerNo(value) {
				this.form.ContainerNO = value;
				_self.productDispatchInfo();
				this.showContainerNoDialog = false;
				_self.setFocus("focus1");
			},
			//发货单信息
			productDispatchInfo() {
				this.DispatchDetailList = [];
				let query = {
					productOrder: this.form.ProductOrder,
					containerNo: this.form.ContainerNO
				};
				this.ProductDispatchInfo(query).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {

						console.log(JSON.stringify(res.resultData));
						this.form.Id = res.resultData.DispatchEntity.Id;
						this.form.DocNum = res.resultData.DispatchEntity.DocNum;
						this.form.CreateTime = res.resultData.DispatchEntity.CreateTime;
						this.form.ContainerNO = res.resultData.DispatchEntity.ContainerNO;
						this.form.CustomerPO = res.resultData.DispatchEntity.CustomerPO;
						this.form.BoxNum = res.resultData.DispatchEntity.BoxNum;
						this.form.PalletQty = res.resultData.DispatchEntity.PalletQty;
						this.DispatchDetailList = res.resultData.DispatchDetailList;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//跳转发货信息维护界面
			delivery(val) {

				if (!this.form.Id) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishProductDelivery.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.markList.length != this.form.PalletQty) {
					this.$refs.uToast.show({
						// title: this.$t('WMS_FinishProductDelivery.MessageTips_2'),
						title: '唛头数量与发货数量不符',
						type: 'warning',
						icon: true
					});
					return;
				}
				_self.markBook();
				// uni.navigateTo({
				// 	url: '/pages/WMSModel/WMS_FinishProductDeliveryList?id=' + this.form.Id + '&DocNum=' + this
				// 		.form.DocNum + '&ContainerNO=' + this.form.ContainerNO,
				// });
			},
			//唛头码扫描
			markScan() {
				let query = {
					markCode: this.form3.MarkCode,
					dispatchId: this.form.Id
				};
				this.ProductDispatchMarkScan(query).then(res => {
					console.log(JSON.stringify(res));
					this.form3.MarkCode = "";
					if (res.success) {
						if (this.markList.find(t => t.MarkCode == res.resultData.MarkCode)) {
							this.$refs.uToast.show({
								//唛头已扫描
								title: this.$t('WMS_FinishProductDelivery.MessageTips_3'),
								type: 'warning',
								icon: true
							});
							return;
						}

						this.markList.push({
							seq: res.resultData.MarkName.substr(res.resultData.MarkName.indexOf('-') + 1,
								2),
							MarkCode: res.resultData.MarkCode,
							MarkName: res.resultData.MarkName
						})
						this.markList.sort((a, b) => a.seq - b.seq); // 升序

						_self.setFocus("focus1");
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//唛头标记
			markBook() {
				let arrMarkCode = this.markList.map(item => {
					return item.MarkCode;
				});
				let postData = {
					arrMarkCode: arrMarkCode
				};
				this.ProductDispatchMarkBook(postData).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						uni.navigateTo({
							url: '/pages/WMSModel/WMS_FinishProductDeliveryList?id=' + this.form.Id +
								'&DocNum=' + this.form.DocNum + '&ContainerNO=' + this.form.ContainerNO,
						});
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			initFocus() {
				this.focus1 = false
			},
			setFocus(focusName) {
				_self.initFocus();
				setTimeout(() => {
					this[focusName] = true;
				}, 0)
			}
		}
	}
</script>
<style lang='scss' scoped>
	.container {
		padding: 20upx;
		/* background: #f9f9f9; */
		/* font-size: 32upx; */
		/* height: 100vh; */
	}

	.readonly {
		background-color: Gainsboro;
	}

	.u-collapse-item {
		background-color: Gainsboro;
		width: 320%;

		view {
			background-color: #F0F3FA;
			font-size: 14px;
		}
	}

	.label {
		line-height: 20px;
		width: 250px;
	}
</style>