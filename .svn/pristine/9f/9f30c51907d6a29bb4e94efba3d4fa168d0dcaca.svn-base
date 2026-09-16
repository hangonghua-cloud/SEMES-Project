<template>
	<view class="container">

		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<u-form-item :label="$t('PMReworkBooking.CardCode')" required>
					<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('PMReworkBooking.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMReworkBooking.OrderMsg')" style="height: auto;">
					<view v-show="form.ReworkProductType=='1' ||form.ReworkProductType=='3'"
						style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('PMReworkBooking.ProductOrder')}}：{{form.ProductOrder}}</view>
						<view class="label">{{$t('PMReworkBooking.ContainerNO')}}：{{form.ContainerNO}}</view>
						<view class="label">{{$t('PMReworkBooking.CardName')}}：{{form.CardName}}</view>
					</view>
					<view v-show="form.ReworkProductType=='2'" style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('PMReworkBooking.WorkOrder')}}：{{form.WorkOrder}}</view>
						<view class="label">{{$t('PMReworkBooking.BatchNo')}}：{{form.BatchNo}}</view>
						<view class="label">{{$t('PMReworkBooking.CardName')}}：{{form.CardName}}</view>
					</view>
				</u-form-item>
				<u-form-item :label="$t('PMReworkBooking.PTeamCode')" required>
					<u-search v-model="form.PTeamCode" @custom="custom" @search="searchCardCode1" @clear="clear"
						:placeholder="$t('PMReworkBooking.PTeamCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMReworkBooking.UserNames')">
					<u-input v-model="form.UserNames" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMReworkBooking.Qty')">
					<u-input v-model="form.Qty" type="number" placeholder="" border :focus="focus3" />
				</u-form-item>
				<u-form-item :label="$t('PMReworkBooking.Remark')">
					<u-input v-model="form.Remark" type="text" placeholder="" border />
				</u-form-item>
			</u-form>
			<view style="margin-top:10rpx;">
				<u-divider halfWidth="100%">{{$t('PMReworkBooking.BadInformation')}}</u-divider>
			</view>
			<u-form :model="form2" label-width="auto">
				<u-form-item :label="$t('PMReworkBooking.BadItemName')" prop="BadItemCode">
					<u-input v-model="form2.BadItemName" @click="OpenModel" type="text" disabled placeholder="请选择不良项目"
						border />
					<u-icon name="search" size="70rpx" color="#138087" @click="OpenModel"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMReworkBooking.BadQty')">
					<u-input v-model="form2.BadQty" type="number" placeholder="" border :focus="focus4"
						@confirm="addBadItem" />
					<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="addBadItem"></u-icon>
					<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
				</u-form-item>
			</u-form>
			<scroll-view scroll-y="true" style="height: 260rpx;border:1px solid Gainsboro;margin-top: 10rpx;">
				<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;"
					v-for="(item, index) in badItemDetailList">
					<u-checkbox v-model="item.Checked">
						<view class="label u-line-1">{{$t('PMReworkBooking.BadItemName')}}：{{item.BadItemName}}</view>
						<view class="label u-line-1">{{$t('PMReworkBooking.BadQty')}}：{{item.BadQty}}</view>
					</u-checkbox>
				</view>
			</scroll-view>
			<view>
				{{$t('PMReworkBooking.NoReworkPalletNo')}}：{{NoReworkPalletNo}}
			</view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMReworkBooking.SaveBtn')}}</text>
			</u-button>
		</view>
		<!-- 工序选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
		<!-- 不良项目选择 -->
		<u-select v-model="showBadItem" @confirm="changeBadItem" :list="badItemList"></u-select>
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
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					ReworkProductType: "", //返工产品类型
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					CardCode: "", //流转卡编码
					CardName: "",
					ReworkBGQty: "", //返工已报工数量
					ProductOrder: "",
					ContainerNO: "", //柜号
					MachineCode: "", //机台编码
					PTeamCode: "", //生产小组
					UserNames: "", //人员信息
					Qty: "", //报工数量
					BadQty: "", //不良数量
					Remark: "", //备注

					//自制半成品
					WorkOrder: "", //工单号
					BatchNo: "", //批次号
				},
				form2: {
					BadItemCode: "",
					BadItemName: "",
					BadQty: "",
				},
				badItemDetailList: [], //不良信息
				taskId: "", //返工任务Id
				NoReworkPalletNo: "", //未返工托号
				processList: [], //工序列表
				showProcess: false, //工序是否显示弹窗
				badItemList: [], //不良项目
				showBadItem: false, //不良项目弹窗
				//焦点
				focus1: false,
				focus2: false,
				focus3: false,
				focus4: false,
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
			_self.setFocus("focus1");
			// this.taskId = options.id
			_self.getLoginInfo();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.$on("to-parent", res => {
				console.log("回传");
				this.form2.BadItemCode = res.result.BadItemCode;
				this.form2.BadItemName = res.result.BadItemName;
				uni.$off("to-parent")
			});

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMReworkBooking")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['ReworkBGCardScan', 'ReworkBGPTeamScan', 'ReworkBGSave']),
			...mapActions('common', ['GetProcessModel']),
			...mapActions('user', ['GetLoginInfo']),

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
					this.transferCardBGCardScan();
				}
			},

			clear() {
				this.form.CardCode = ""; //流转卡
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
					this.transferCardBGPTeamScan();
				}
			},

			clear() {
				this.form.MachineCode = "";
			},
			//流转卡扫描方法
			transferCardBGCardScan() {
				let query = {
					cardCode: this.form.CardCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ReworkBGCardScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.taskId = res.resultData.TaskId;
						this.form.ReworkProductType = res.resultData.ReworkProductType;
						if (this.form.ReworkProductType == "1" || this.form.ReworkProductType == "3") { //正常
							this.form.ProductOrder = res.resultData.ProductOrder;
							this.form.ContainerNO = res.resultData.ContainerNO;
						} else if (this.form.ReworkProductType == "2") { //自制
							this.form.WorkOrder = res.resultData.WorkOrder;
							this.form.BatchNo = res.resultData.BatchNo;
						}
						this.form.CardName = res.resultData.CardName;
						this.form.ProcessCode = res.resultData.ReworkProcess;
						this.NoReworkPalletNo = res.resultData.NoReworkPalletNo;
						if (res.resultData.batItemList == null || res.resultData.batItemList.length == 0) {
							this.badItemList = [{
								value: '',
								label: this.$t("common.None")
							}];
						} else {
							this.badItemList = res.resultData.batItemList;
						}
						this.form.ReworkBGQty = res.resultData.ReworkBGQty;
						if (this.form.ReworkBGQty && this.form.ReworkBGQty > 0) {
							uni.showModal({
								title: this.$t('PMReworkBooking.modalTitle'),

								cancelText: this.$t("showModal.cancel"),
								confirmText: this.$t("showModal.confirm"),
								content: this.$t('PMReworkBooking.modalContent', [this.form.ReworkBGQty]),
								success: function(res) {
									if (res.confirm) {
										console.log('用户点击确定');
									} else if (res.cancel) {
										console.log('用户点击取消');
										_self.reset();
									}
								}
							});
						}
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
			//生产小组扫描方法
			transferCardBGPTeamScan() {
				if (!this.form.PTeamCode)
					return;

				let query = {
					pTeamCode: this.form.PTeamCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ReworkBGPTeamScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						let arr = res.resultData.map(item => {
							return item.UserName;
						});
						this.form.UserNames = arr.join(',');
						_self.setFocus("focus3");
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
						title: this.$t('PMReworkBooking.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form2.BadQty) {
					this.$refs.uToast.show({
						title: this.$t('PMReworkBooking.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				let filterList = this.badItemDetailList.filter(item => item.BadItemCode == this.form2.BadItemCode);
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t('PMReworkBooking.MessageTips_3'),
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


			//返工确认
			save() {
				if (!this.form.CardCode) {
					this.$refs.uToast.show({
						title: this.$t('PMReworkBooking.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.Qty) {
					this.$refs.uToast.show({
						title: this.$t('PMReworkBooking.MessageTips_5'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.PTeamCode) {
					this.$refs.uToast.show({
						title: this.$t('PMReworkBooking.MessageTips_6'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form2.BadItemCode) {
					this.$refs.uToast.show({
						title: this.$t('PMReworkBooking.MessageTips_7'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					reworkProductType: this.form.ReworkProductType,
					taskId: this.taskId,
					cardCode: this.form.CardCode,
					qty: this.form.Qty,
					badQty: this.form.BadQty,
					pTeamCode: this.form.PTeamCode,
					remark: this.form.Remark,
					badItemDetailList: this.badItemDetailList,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				uni.showLoading({
					title: this.$t('common.saveing')
				});
				this.ReworkBGSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('PMReworkBooking.MessageTips_8'),
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
				this.form.ReworkProductType = "";
				this.form.CardCode = "";
				this.form.CardName = "";
				this.form.ProductOrder = "";
				this.form.ContainerNO = "";
				this.form.WorkOrder = ""; //工单号
				this.form.BatchNo = ""; //批次号
				this.form.Qty = "";
				this.form.BadQty = "";
				this.form2.BadItemCode = "";
				this.form2.BadItemName = "";
				this.form2.BadQty = "";
				this.badItemDetailList = []; //不良信息
			},
			//显示下拉框
			showSel(val, item) {
				if (val == "process") {
					this.showProcess = true;
				} else if (val == "badItem") {
					this.showBadItem = true;
				}
			},

			//选择不良项目
			changeBadItem(val) {
				this.form2.BadItemCode = val[0].value; //val[0].label;				
				this.form2.BadItemName = val[0].label;
				_self.setFocus("focus4");
			},
			//查询登录信息
			getLoginInfo() {
				let data = {
					userCode: this.loginInfo.result.UserCode,
				};
				this.GetLoginInfo(data).then(res => {
					if (res.success) {
						this.form.PTeamCode = res.resultData.PTeamCode; //生产小组
						_self.transferCardBGPTeamScan(); //生产小组
					}
				});
			},
			initFocus() {
				this.focus1 = false
				this.focus2 = false
				this.focus3 = false
				this.focus4 = false
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

	.label {
		line-height: 20px;
		width: 250px;
	}
</style>