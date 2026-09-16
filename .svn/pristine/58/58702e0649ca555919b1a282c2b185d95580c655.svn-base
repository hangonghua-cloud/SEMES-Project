<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<u-form-item :label="$t('WMS_MarksChange.MarkCode')" required>
					<u-search v-model="form.MarkCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('WMS_MarksChange.MarkCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('WMS_MarksChange.MarkCode')" style="height: auto;">
					<view style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('WMS_MarksChange.ProductOrder')}}：{{form.ProductOrder}}</view>
						<view class="label">{{$t('WMS_MarksChange.ContainerNO')}}：{{form.ContainerNO}}</view>
						<view class="label">{{$t('WMS_MarksChange.CustomerPO')}}：{{form.CustomerPO}}</view>
						<view class="label">{{$t('WMS_MarksChange.MaterialCode')}}：{{form.MaterialCode}}</view>
						<view class="label">{{$t('WMS_MarksChange.WorkOrderType')}}：{{form.WorkOrderType}}</view>
						<view class="label">{{$t('WMS_MarksChange.TotalPalletQty')}}：{{form.TotalPalletQty}}</view>
					</view>
				</u-form-item>
				<u-form-item :label="$t('WMS_MarksChange.OldMarkCode')" required>
					<u-search v-model="form.OldMarkCode" @custom="custom" @search="searchCardCode1" @clear="clear1"
						:placeholder="$t('WMS_MarksChange.OldMarkCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>
				<!-- <u-form-item label="旧唛头信息" style="height: auto;">
					<view style="border: 1px solid Gainsboro;">
						<view class="label">订单号：{{form.ProductOrderOld}}</view>
						<view class="label">柜号：{{form.ContainerNOOld}}</view>
						<view class="label">PO号：{{form.CustomerPOOld}}</view>
						<view class="label">客户型号：{{form.MaterialCodeOld}}</view>
						<view class="label">工单类型：{{form.WorkOrderTypeOld}}</view>
						<view class="label">唛头总数：{{form.TotalPalletQtyOld}}</view>
					</view>
				</u-form-item> -->
			</u-form>
			<view style="height: auto;">
				<u-checkbox v-show="oldMarkList.length>0" v-model="chkAll" @change="chkAllChange"><text
						class="u-font-14">{{$t('common.SelectAll')}}</text></u-checkbox>
				<scroll-view scroll-y="true" class="scroll-Y" style="height: 660rpx;">
					<u-collapse>
						<view style="border:1px solid white" v-for="(item, index) in oldMarkList">
							<u-checkbox v-model="item.Checked" style="width:100%;">
								<u-collapse-item class="u-collapse-item">
									<template slot="title">
										<text style="font-size: 14px;">
											{{item.Mark}}
										</text>
									</template>
									<view class="label">
										{{$t('WMS_MarksChange.ProductOrderOld')}}：{{form.ProductOrderOld}}</view>
									<view class="label">{{$t('WMS_MarksChange.ContainerNOOld')}}：{{form.ContainerNOOld}}
									</view>
									<view class="label">{{$t('WMS_MarksChange.CustomerPOOld')}}：{{form.CustomerPOOld}}
									</view>
									<view class="label">
										{{$t('WMS_MarksChange.MaterialCodeOld')}}：{{form.MaterialCodeOld}}</view>
									<view class="label">
										{{$t('WMS_MarksChange.WorkOrderTypeOld')}}：{{form.WorkOrderTypeOld}}</view>
								</u-collapse-item>
							</u-checkbox>
						</view>
					</u-collapse>
				</scroll-view>
			</view>
		</view>

		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save()" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_MarksChange.SaveBtn')}}</text>
			</u-button>
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
					OldMarkCode: "",
					ProductOrder: "",
					ContainerNO: "",
					MaterialCode: "",
					WorkOrderType: "",
					TotalPalletQty: "",
					WhsCode: "",
					WhsName: "",
					ProductOrderOld: "",
					ContainerNOOld: "",
					CustomerPOOld: "",
					MaterialCodeOld: "",
					WorkOrderTypeOld: "",
					TotalPalletQtyOld: "",
				},
				oldMarkList: [], //旧唛头列表
				chkAll: false,
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
				url: '/pages/WMSModel/WMS_SemiFinishProductChangeSel'
			})
		},

		onNavigationBarButtonTap: function(option) {
			//点击事件
			console.info('点击查询按钮事件', JSON.stringify(option));
			uni.navigateTo({
				url: '/pages/WMSModel/WMS_MarksChangeSel'
			})
		},

		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.WMSModel/WMS_MarksChange")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('WMS', ['MarkChangeMarkScan', 'MarkChangeSave']),
			...mapActions('common', ['GetProcessModel']),

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
					this.markChangeMarkScan();
				}
			},

			clear() {
				this.form.MarkCode = ""; //唛头码
			},

			//旧唛头扫描事件
			searchQR1() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode1(res.result);
					}
				});
			},

			//旧唛头查询
			searchCardCode1(value) {
				this.form.OldMarkCode = value; //库位
				if (this.form.OldMarkCode != "") {
					this.OldmarkChangeMarkScan();
				}
			},

			clear1() {
				this.form.OldMarkCode = ""; //库位
			},

			//唛头码扫描
			markChangeMarkScan() {
				let query = {
					markCode: this.form.MarkCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.MarkChangeMarkScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.ProductOrder = res.resultData.ProductOrder;
						this.form.ContainerNO = res.resultData.ContainerNO;
						this.form.CustomerPO = res.resultData.CustomerPO;
						this.form.MaterialCode = res.resultData.MaterialCode;
						this.form.WorkOrderType = res.resultData.WorkOrderType;
						this.form.TotalPalletQty = res.resultData.TotalPalletQty;

						_self.setFocus("focus2");
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
					//console.log(JSON.stringify(this.form.MarkingManageId))
				});
			},

			//旧唛头码扫描
			OldmarkChangeMarkScan() {
				let query = {
					markCode: this.form.OldMarkCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.MarkChangeMarkScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.ProductOrderOld = res.resultData.ProductOrder;
						this.form.ContainerNOOld = res.resultData.ContainerNO;
						this.form.CustomerPOOld = res.resultData.CustomerPO;
						this.form.MaterialCodeOld = res.resultData.MaterialCode;
						this.form.WorkOrderTypeOld = res.resultData.WorkOrderType;
						this.form.TotalPalletQtyOld = res.resultData.TotalPalletQty;
						this.oldMarkList = res.resultData.MarkList;
						this.oldMarkList.forEach(item => {
							this.$set(item, "Checked", false);
						})
						this.chkAll = true;
						_self.chkAllChange();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//全选change事件
			chkAllChange(e) {
				if (this.chkAll) {
					this.oldMarkList.forEach(item => {
						item.Checked = true;
					})
				} else {
					this.oldMarkList.forEach(item => {
						item.Checked = false;
					})
				}
			},

			//保存
			save() {
				if (this.form.MarkCode == "") {
					this.$refs.uToast.show({
						title: this.$t('WMS_MarksChange.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.locationCode == "") {
					this.$refs.uToast.show({
						title: this.$t('WMS_MarksChange.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				let selectedItems = this.oldMarkList.filter(item => {
					return item.Checked == true;
				});
				if (selectedItems.length == 0) {
					this.$refs.uToast.show({
						title: this.$t('WMS_MarksChange.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					newMarkCode: this.form.MarkCode,
					oldMarkCode: this.form.OldMarkCode,
					oldMarkList: selectedItems
				};

				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.MarkChangeSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('WMS_MarksChange.MessageTips_4'),
							type: 'success',
							icon: true
						});
						this.reset();
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
				this.form.OldMarkCode = "";
				this.form.ProductOrder = "";
				this.form.ContainerNO = "";
				this.form.MaterialCode = "";
				this.form.WorkOrderType = "";
				this.form.TotalPalletQty = "";
				this.form.WhsCode = "";
				this.form.WhsName = "";
				this.form.ProductOrderOld = "";
				this.form.ContainerNOOld = "";
				this.form.CustomerPOOld = "";
				this.form.MaterialCodeOld = "";
				this.form.WorkOrderTypeOld = "";
				this.form.TotalPalletQtyOld = "";
				this.oldMarkList = [];
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