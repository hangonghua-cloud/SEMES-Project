<template>
	<view class="container">

		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMPackBooking.MachineCode')" required>
				<u-search v-model="form.MachineCode" @custom="custom" @search="searchMachineCode" @clear="clear2"
					:placeholder="$t('PMPackBooking.MachineCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus4">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR2"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.PTeamCode')" required>
				<u-search v-model="form.PTeamCode" @custom="custom" @search="searchCardCode1" @clear="clear1"
					:placeholder="$t('PMPackBooking.PTeamCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus5">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.UserNames')">
				<u-input v-model="form.UserNames" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.CardCode')" required>
				<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('PMPackBooking.CardCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus2">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.ProductOrder')">
				<u-input v-model="form.ProductOrder" type="text" disabled placeholder="" class="readonly" border />
				<u-icon name="search" size="70rpx" color="#138087" @click="showProductOrderDialog=true"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.ContainerNO')">
				<u-input v-model="form.ContainerNO" type="number"
					:placeholder="$t('PMPackBooking.ContainerNO_placeholder')" border :focus="focus1"
					@blur="packingBGCardScan('2')" />
				<u-icon name="search" size="70rpx" color="#138087" @click="showContainerNoDialog=true"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.MachineCode1')">
				<u-input v-model="form.MachineCode1" disabled type="text" placeholder="" border />
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.qty')">
				<u-input v-model="form.qty" type="number" placeholder="" border :focus="focus3" />
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.MaterialCode')">
				<u-input v-model="form.MaterialCode" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.CanBGQty')">
				<u-input v-model="form.CanBGQty" disabled type="text" placeholder="" border class="readonly" />
				<text style="margin:0px 40rpx">{{$t('PMPackBooking.PackNoReport')}}</text>
				<u-input v-model="form.PackNoBGQty" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.Remark')">
				<u-input v-model="form.Remark" type="text" placeholder="" border />
			</u-form-item>
			<view style="margin-top:10rpx;">
				<u-divider halfWidth="100%">{{$t('PMPackBooking.BadInformation')}}</u-divider>
			</view>
		</u-form>
		<u-form :model="form2" label-width="auto">
			<u-form-item :label="$t('PMPackBooking.BadItemName')" prop="BadItemCode">
				<u-input v-model="form2.BadItemName" @click="OpenModel" type="text" disabled placeholder="请选择不良项目"
					border />
				<u-icon name="search" size="70rpx" color="#138087" @click="OpenModel"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMPackBooking.BadQty')">
				<u-input v-model="form2.BadQty" type="number" placeholder="" border :focus="focus6"
					@confirm="addBadItem" />
				<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="addBadItem"></u-icon>
				<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
			</u-form-item>
		</u-form>

		<scroll-view scroll-y="true" style="height: 260rpx;border:1px solid Gainsboro;margin-top: 10rpx;">
			<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;"
				v-for="(item, index) in badItemDetailList">
				<u-checkbox v-model="item.Checked">
					<view class="label u-line-1">{{$t('PMPackBooking.BadItemName')}}：{{item.BadItemName}}</view>
					<view class="label u-line-1">{{$t('PMPackBooking.BadQty')}}：{{item.BadQty}}</view>
				</u-checkbox>
			</view>
		</scroll-view>
		<view style="height: 205rpx;"></view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="save"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('PMPackBooking.SaveBtn')}}
			</u-button>
			<u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="sel"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('PMPackBooking.SearchBtn')}}
			</u-button>
		</view>
		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<!-- 订单号选择弹窗 -->
		<u-popup v-model="showProductOrderDialog" mode="right" length="60%">
			<view class="container">
				<view class="item" v-for="(item,index) of productOrderList" @click="selectProductOrder(item)"
					:key='index'>
					<view style="border:1px solid white;">
						<view class="name" style="background-color: Gainsboro;padding: 10rpx;width: 200px;">
							{{item}}
						</view>
					</view>
				</view>
			</view>
			<view style="height: 9%;"></view>
			<view class="" style="display: flex;">
				<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
					:custom-style="{width: '80%',height: '70rpx',borderRadius: '10rpx'}"
					@click="showProductOrderDialog=false"
					style="position: fixed;bottom: 30rpx;margin-left: 10%;">{{$t('PMPackBooking.CancelBtn')}}
				</u-button>
			</view>
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
					@click="showContainerNoDialog=false"
					style="position: fixed;bottom: 30rpx;margin-left: 10%;">{{$t('PMPackBooking.CancelBtn')}}
				</u-button>
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
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					CardCode: "", //流转卡编码
					CardName: "", //托号
					ProductOrder: "",
					ProductOrderID: "",
					ContainerNO: "", //柜号
					ContainerNOID: "",
					ContainerNOorCardName: "", //柜号/托号
					MaterialCode: "",
					MachineCode: "", //机台编码
					PTeamCode: "", //生产小组
					UserNames: "", //人员信息
					CanBGQty: "", //可报工数量
					PackNoBGQty: "", //包装未报工数量（当前工单）
					BadQty: "", //不良数量
					qty: "",
					Remark: "", //备注
				},
				form2: {
					BadItemCode: "",
					BadItemName: "",
					BadQty: "",
				},
				resetFlag: false,
				badItemList: [], //不良项目
				badItemDetailList: [], //不良信息

				showProductOrderDialog: false, //订单号弹窗
				productOrderList: [], //订单号列表
				showContainerNoDialog: false, //柜号弹窗
				ContainerNOList: [], //柜号列表
				//焦点
				focus1: false,
				focus2: false,
				focus3: false,
				focus4: false,
				focus5: false,
				focus6: false,
			}
		},

		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		//预加载
		onLoad(options) {
			_self = this;
			_self.setFocus("focus2");
			_self.getLoginInfo();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.$on("to-parent", res => {
				console.log("回传");
				this.form2.BadItemCode = res.result.BadItemCode;
				this.form2.BadItemName = res.result.BadItemName;
				uni.$off("to-parent")
				_self.setFocus("focus6");
			});
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMPackBooking")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['PackingBGCardScan', 'GetProductOrderSelect', 'GetContainerNOSelect',
				'PackingBGPTeamScan', 'PackingBGSave'
			]),
			...mapActions('common', ['GetProcessModel']),
			...mapActions('user', ['GetLoginInfo']),

			//初始化柜号列表
			getContainerNOList() {
				let data = {
					productOrder: this.form.ProductOrder,
					materialCode: this.form.MaterialCode
				}
				this.GetContainerNOSelect(data).then(res => {
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
				_self.packingBGCardScan('2');
				this.showProductOrderDialog = false;
			},
			//弹窗选择柜号
			selectContainerNo(value) {
				this.form.ContainerNO = value;
				_self.packingBGCardScan('2');
				this.showContainerNoDialog = false;
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
				this.form.CardCode = value; //流转卡
				if (this.form.CardCode != "") {
					this.packingBGCardScan('1');
				}
			},

			clear() {
				this.form.CardCode = ""; //流转卡
			},


			//机台扫描事件
			searchQR2() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchMachineCode(res.result);
					}
				});
			},

			//机台查询
			searchMachineCode(value) {
				this.form.MachineCode = value; //机台
				_self.setFocus("focus5");
			},

			clear2() {
				this.form.MachineCode = ""; //机台
			},


			//人员组别扫描事件
			searchQR1() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode1(res.result);
					}
				});
			},

			//人员组别查询
			searchCardCode1(value) {
				this.form.PTeamCode = value;
				if (this.form.PTeamCode != "") {
					this.packingBGPTeamScan();
				}
			},
			clear() {
				this.form.MachineCode = "";
			},
			//流转卡扫描方法
			packingBGCardScan(type) {
				if (!this.form.MachineCode) {
					this.$refs.uToast.show({
						title: this.$t('PMPackBooking.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let query = {
					cardCode: this.form.CardCode,
					productOrder: this.form.ProductOrder,
					containerNO: type == '1' ? "" : this.form.ContainerNO,
					machineCode: this.form.MachineCode,
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.PackingBGCardScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.productOrderList = res.resultData.ProductOrderList;
						this.form.ProductOrder = res.resultData.ProductOrder;
						if (type == '1') {
							this.form.ContainerNO = res.resultData.ContainerNO; //柜号
						}
						this.form.ProcessCode = res.resultData.ProcessCode;
						this.form.CardName = res.resultData.CardName; //托号
						this.form.MaterialCode = res.resultData.MaterialCode;
						this.form.CanBGQty = res.resultData.CanBGQty;
						this.form.PackNoBGQty = res.resultData.PackNoBGQty;
						this.form.ContainerNOorCardName = res.resultData.ContainerNO + "/" + res.resultData
							.CardName;
						if (res.resultData.batItemList == null || res.resultData.batItemList.length == 0) {
							this.badItemList = [{
								value: '',
								label: this.$t("common.None")
							}];
						} else {
							this.badItemList = res.resultData.batItemList;
						}
						if (!this.form.ContainerNO) {
							_self.setFocus("focus1");
						} else {
							_self.setFocus("focus3");
						}
						if (this.form.CanBGQty < 0) {
							this.$refs.uToast.show({
								title: this.$t("PMPackBooking.MessageTips_2"),
								type: 'warning',
								icon: true
							});
						}
						//初始化柜号弹窗列表
						if (type == '1' && this.form.ProductOrder && this.form.MaterialCode) {
							_self.getContainerNOList();
						}
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
						_self.reset();
					}
				});
			},
			//生产小组扫描方法
			packingBGPTeamScan() {
				let query = {
					pTeamCode: this.form.PTeamCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.PackingBGPTeamScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						let arr = res.resultData.map(item => {
							return item.UserName;
						});
						this.form.UserNames = arr.join(',');
						_self.setFocus("focus2");
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//打开选择框
			OpenModel() {
				console.log("模态框");

				uni.navigateTo({
					url: '/pages/public/BadModel?ProcessCode=' + this.form.ProcessCode,
				});
			},
			//添加不良
			addBadItem() {
				if (!this.form2.BadItemCode) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form2.BadQty) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_4"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let filterList = this.badItemDetailList.filter(item => item.BadItemCode == this.form2.BadItemCode);
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_5"),
						type: 'warning',
						icon: true
					});
					return;
				}

				this.badItemDetailList.push({
					Checked: false,
					BadItemCode: this.form2.BadItemCode,
					BadItemName: this.form2.BadItemName,
					BadQty: this.form2.BadQty
				});
				this.form2.BadItemCode = "";
				this.form2.BadItemName = "";
				this.form2.BadQty = "";
				let arr = this.badItemDetailList.map(item => {
					return item.BadQty;
				});
				this.form.BadQty = eval(arr.join("+"));
			},
			//删除不良
			deleteBadItem() {
				this.badItemDetailList = this.badItemDetailList.filter(item => {
					return item.Checked == false;
				});
				let arr = this.badItemDetailList.map(item => {
					return item.BadQty;
				});
				this.form.BadQty = eval(arr.join("+"));
			},

			//查询跳转
			sel() {
				uni.navigateTo({
					url: '/pages/ProduceModel/PMPackingSel',
				});
			},

			//包装报工
			save() {
				if (!this.form.ProductOrder) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_6"),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form.ContainerNO) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_7"),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form.CardCode) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_8"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.PTeamCode) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_9"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form2.BadItemCode) {
					this.$refs.uToast.show({
						title: this.$t("PMPackBooking.MessageTips_10"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					productOrder: this.form.ProductOrder,
					containerNO: this.form.ContainerNO,
					processCode: this.form.ProcessCode,
					cardCode: this.form.CardCode,
					machineCode: this.form.MachineCode,
					qty: this.form.qty,
					badQty: this.form.BadQty,
					pTeamCode: this.form.PTeamCode,
					remark: this.form.Remark,
					badItemDetailList: this.badItemDetailList,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				console.log(this.form.ProcessCode);
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.PackingBGSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMPackBooking.MessageTips_11"),
							type: 'success',
							icon: true
						});
						this.reset();
						_self.setFocus("focus2");
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
				this.form.CardCode = "";
				this.form.CardName = "";
				this.form.MaterialCode = "";
				this.form.ContainerNOorCardName = "";
				this.form.ProductOrderID = "";

				this.form.CanBGQty = "";
				this.form.PackNoBGQty = "";
				this.form.qty = "";
				this.form.BadQty = "";
				this.form2.BadItemCode = "";
				this.form2.BadItemName = "";
				this.form2.BadQty = "";
				this.badItemDetailList = []; //不良信息

			},

			//查询登录信息
			getLoginInfo() {
				let data = {
					userCode: this.loginInfo.result.UserCode,
				};
				this.GetLoginInfo(data).then(res => {
					if (res.success) {
						this.form.MachineCode = res.resultData.MachineCode; //机台编码
						this.form.PTeamCode = res.resultData.PTeamCode; //生产小组
						_self.packingBGPTeamScan(); //生产小组
					}
				});
			},
			initFocus() {
				this.focus1 = false
				this.focus2 = false
				this.focus3 = false
				this.focus4 = false
				this.focus5 = false
				this.focus6 = false
			},
			setFocus(focusName) {
				_self.initFocus();
				setTimeout(() => {
					this[focusName] = true;
				}, 0)
			},
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
</style>