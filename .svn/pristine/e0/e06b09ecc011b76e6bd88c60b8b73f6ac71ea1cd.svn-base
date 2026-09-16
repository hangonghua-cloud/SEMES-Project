<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<u-form-item :label="$t('WMS_FinishProductStore.MarkCode')" required>
					<u-search v-model="form.MarkCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('WMS_FinishProductStore.MarkCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('WMS_FinishProductStore.locationCode')" required>
					<u-search v-model="form.locationCode" @custom="custom" @search="searchCardCode1" @clear="clear1"
						:placeholder="$t('WMS_FinishProductStore.locationCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('WMS_FinishProductStore.CardInfo')" style="height: auto;">
					<view style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('WMS_FinishProductStore.MarkName')}}：{{form.MarkName}}</view>
						<view class="label">{{$t('WMS_FinishProductStore.ProductOrder')}}：{{form.ProductOrder}}</view>
						<view class="label">{{$t('WMS_FinishProductStore.ContainerNO')}}：{{form.ContainerNO}}</view>
						<view class="label">{{$t('WMS_FinishProductStore.MaterialCode')}}：{{form.MaterialCode}}</view>
						<view class="label">{{$t('WMS_FinishProductStore.SourceWhsName')}}：{{form.SourceWhsName}}</view>
						<view class="label">
							{{$t('WMS_FinishProductStore.SourceLocationCode')}}：{{form.SourceLocationCode}}
						</view>
					</view>
				</u-form-item>
				<u-form-item :label="$t('WMS_FinishProductStore.GoodLocationCode')" style="height: auto;">
					<scroll-view scroll-y="true" style="height: 250rpx;border:1px solid Gainsboro;">
						<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;"
							v-for="(item, index) in lstLoc">
							<view class="label u-line-1">
								{{$t('WMS_FinishProductStore.LocationCode')}}：{{item.LocationCode}}&#12288{{item.PalletQty}}{{$t('WMS_FinishProductStore.PalletQty')}}
							</view>
						</view>
					</scroll-view>
				</u-form-item>
				<u-form-item :label="$t('WMS_FinishProductStore.LocationCode2')" style="height: auto;">
					<view style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('WMS_FinishProductStore.WhsName')}}：{{form.WhsName}}</view>
						<view class="label">{{$t('WMS_FinishProductStore.LocationCode')}}：{{form.LocationCode}}</view>
					</view>
				</u-form-item>
				<u-form-item :label="$t('WMS_FinishProductStore.LocationCode3')" style="height: auto;">
					<scroll-view scroll-y="true" style="height: 260rpx;border:1px solid Gainsboro;">
						<view style="border-bottom:1px solid Gainsboro;" v-for="(item, index) in inList">
							<view class="label u-line-1">{{item.MarkCode}}</view>
						</view>
					</scroll-view>
				</u-form-item>
			</u-form>
		</view>

		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save()" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_FinishProductStore.SaveBtn')}}</text>
			</u-button>
			<!-- <u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="add"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">添加
			</u-button>
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="save"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">提交
			</u-button> -->
		</view>
		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
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

					MarkCode: "",
					ProductOrder: "",
					WorkOrder: "",
					ContainerNO: "",
					MaterialCode: "",
					CustomerPO: "",
					SourceWhsCode: "",
					SourceWhsName: "",
					SourceLocationCode: "",
					SourceLocationName: "",
					WhsCode: "",
					WhsName: "",
					locationCode: "",
					LocationCode: "",
					LocationName: "",
				},
				chkAll: false,
				//工序是否显示弹窗
				showdutyProcess: false,
				showreworkProcess: false,
				lstLoc: [],
				inList: [],
				//焦点
				focus1: false,
				focus2: false,
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
			_self.setFocus("focus1");
		},
		onNavigationBarButtonTap: function(option) {
			//点击事件
			console.info('点击查询按钮事件', JSON.stringify(option));
			uni.navigateTo({
				url: '/pages/WMSModel/WMS_FinishProductStoreSel'
			})
		},

		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.WMSModel.WMSModel/WMS_FinishProductStore")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('WMS', ['ProductInMarkScan', 'ProductInLocationScan', 'ProductInSave']),
			...mapActions('common', ['GetProcessModel']),




			//显示下拉框
			showSel(val, item) {
				if (val == "dutyprocess")
					this.showdutyProcess = true;
				else if (val == "reworkprocess")
					this.showreworkProcess = true;
			},
			//选择责任工序
			changedutyProcess(val) {
				this.form.dutyProcessCode = val[0].value; //val[0].label;				
				this.form.dutyProcessName = val[0].label;
			},

			//选择返工工序
			changereworkProcess(val) {
				this.form.reworkProcessCode = val[0].value; //val[0].label;				
				this.form.reworkProcessName = val[0].label;
			},

			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode(res.result);
					}
				});
			},

			//条码查询
			searchCardCode(value) {
				this.form.MarkCode = value; //唛头码
				if (this.form.MarkCode != "") {
					this.productInMarkScan();
				}
			},

			clear() {
				this.form.MarkCode = ""; //唛头码
			},

			//库位扫描事件
			searchQR1() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode1(res.result);
					}
				});
			},

			//库位查询
			searchCardCode1(value) {
				this.form.locationCode = value; //库位
				if (this.form.locationCode != "") {
					this.productInLocationScan();
				}
			},

			clear1() {
				this.form.locationCode = ""; //库位
			},

			//唛头码扫描
			productInMarkScan() {
				this.lstLoc = [];
				let query = {
					markCode: this.form.MarkCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ProductInMarkScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.MarkName = res.resultData.MarkName;
						this.form.ProductOrder = res.resultData.ProductOrder;
						this.form.WorkOrder = res.resultData.WorkOrder;
						this.form.ContainerNO = res.resultData.ContainerNO;
						this.form.MaterialCode = res.resultData.MaterialCode;
						this.form.CustomerPO = res.resultData.CustomerPO;
						this.form.SourceWhsCode = res.resultData.SourceWhsCode;
						this.form.SourceWhsName = res.resultData.SourceWhsName;
						this.form.SourceLocationCode = res.resultData.SourceLocationCode;
						this.form.SourceLocationName = res.resultData.SourceLocationName;
						this.lstLoc = res.resultData.lstLoc;

						_self.add();
						this.form.MarkCode = "";
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


			//库位扫描
			productInLocationScan() {
				let query = {
					locationCode: this.form.locationCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ProductInLocationScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.WhsCode = res.resultData.WhsCode;
						this.form.WhsName = res.resultData.WhsName;
						this.form.LocationCode = res.resultData.LocationCode;
						this.form.LocationName = res.resultData.LocationName;

					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//添加
			add() {
				if (!this.form.MarkCode) {
					this.$refs.uToast.show({
						title: this.$t("WMS_FinishProductStore.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				// if (!this.form.LocationCode) {
				// 	this.$refs.uToast.show({
				// 		title: '请扫描入库库位',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }
				// if (this.form.SourceLocationCode == this.form.LocationCode) {
				// 	this.$refs.uToast.show({
				// 		title: '源库位和目标库位相同，无法入库库',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }

				let filterList = this.inList.filter(item => item.MarkCode == this.form.MarkCode);
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t("WMS_FinishProductStore.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}

				this.inList.push({
					MarkCode: this.form.MarkCode,
					MarkName: this.form.MarkName,
					ProductOrder: this.form.ProductOrder,
					WorkOrder: this.form.WorkOrder,
					ContainerNO: this.form.ContainerNO,
					MaterialCode: this.form.MaterialCode,
					CustomerPO: this.form.CustomerPO,
				});
			},

			//确认
			save() {
				if (!this.form.LocationCode) {
					this.$refs.uToast.show({
						title: this.$t("WMS_FinishProductStore.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.inList.length == 0) {
					this.$refs.uToast.show({
						title: this.$t("WMS_FinishProductStore.MessageTips_4"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					whsCode: this.form.WhsCode,
					locationCode: this.form.LocationCode,
					inList: this.inList
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ProductInSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("WMS_FinishProductStore.MessageTips_5"),
							type: 'success',
							icon: true
						});
						_self.reset();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			reset() {
				this.form.MarkCode = "";
				this.form.ProductOrder = "";
				this.form.ContainerNO = "";
				this.form.MaterialCode = "";
				this.form.SourceWhsCode = "";
				this.form.SourceWhsName = "";
				this.form.SourceLocationCode = "";
				this.form.SourceLocationName = "";
				this.form.lstLoc = "";
				this.form.WhsCode = "";
				this.form.WhsName = "";
				this.form.locationCode = "";
				this.form.LocationCode = "";
				this.form.LocationName = "";
				this.inList = [];
			},

			//查询
			sel() {
				uni.navigateTo({
					url: '/pages/WMSModel/WMS_FinishProductStoreSel',
				});

			},
			//待入库清单
			search() {
				uni.navigateTo({
					url: '/pages/WMSModel/WMS_FinishProductVoucher',
				});
			},
			initFocus() {
				this.focus1 = false
				this.focus2 = false
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