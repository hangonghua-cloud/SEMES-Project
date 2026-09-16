<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<!-- <u-form-item label="操作工序" prop="ProcessName">
					<u-input v-model="form.ProcessName" @click="showSel('process')" type="text" disabled placeholder="请选择工序" border />
				</u-form-item> -->
				<u-form-item :label="$t('PMOwnProductBG.TransferCode')" required>
					<u-search v-model="form.TransferCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('PMOwnProductBG.TransferCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focusCardCode">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMOwnProductBG.BGQty')">
					<u-input v-model="form.BGQty" type="number" placeholder="" border :focus="focusQty" />
					<text style="margin:0px 40rpx">{{$t('PMOwnProductBG.BadQty')}}</text>
					<u-input v-model="form.BadQty" disabled type="number" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMOwnProductBG.MachineCode')" required>
					<u-search v-model="form.MachineCode" @custom="custom" @search="searchCardCode1" @clear="clear1"
						:placeholder="$t('PMOwnProductBG.MachineCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focusMachineCode">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>

				<u-form-item :label="$t('PMOwnProductBG.PTeamCode')" required>
					<u-search v-model="form.PTeamCode" @custom="custom" @search="searchCardCode2" @clear="clear2"
						:placeholder="$t('PMOwnProductBG.PTeamCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focusPTeamCode">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR2"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMOwnProductBG.OrderInfo')" style="height: auto;">
					<view v-show="form.MaterialCode" style="border: 1px solid Gainsboro;">
						<view class="label">{{$t('PMOwnProductBG.MaterialCode')}}：{{form.MaterialCode}}</view>
						<view class="label">
							{{$t('PMOwnProductBG.TransferName')}}：{{form.TransferName}}&#12288{{$t('PMOwnProductBG.SmallClassName')}}：{{form.SmallClassName}}
						</view>
						<view class="label">{{$t('PMOwnProductBG.HasBGQty')}}：{{form.HasBGQty}}</view>
					</view>
				</u-form-item>
				<u-form-item :label="$t('PMOwnProductBG.UserNames')">
					<u-input v-model="form.UserNames" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
			</u-form>
			<view style="margin-top:10rpx;">
				<u-divider halfWidth="100%">{{$t('PMOwnProductBG.BadInformation')}}</u-divider>
			</view>
			<u-form :model="form2" label-width="auto">
				<u-form-item :label="$t('PMOwnProductBG.BadItemName')" prop="BadItemCode">
					<u-input v-model="form2.BadItemName" @click="OpenModel" type="text" disabled
						:placeholder="$t('PMOwnProductBG.BadItemName_placeholder')" border />
					<u-icon name="search" size="70rpx" color="#138087" @click="OpenModel"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMOwnProductBG.BadQty')">
					<u-input v-model="form2.BadQty" type="number" placeholder="" border :focus="focusBadQty"
						@confirm="addBadItem" />
					<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="addBadItem"></u-icon>
					<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
				</u-form-item>
			</u-form>
			<scroll-view scroll-y="true" style="height: 260rpx;border:1px solid Gainsboro;margin-top: 10rpx;">
				<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;"
					v-for="(item, index) in badItemDetailList">
					<u-checkbox v-model="item.Checked">
						<view class="label u-line-1">{{$t('PMOwnProductBG.BadItemName')}}：{{item.BadItemName}}</view>
						<view class="label u-line-1">{{$t('PMOwnProductBG.BadQty')}}：{{item.BadQty}}</view>
					</u-checkbox>
				</view>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMOwnProductBG.SaveBtn')}}</text>
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
					OwnProductId: "",
					TransferCode: "", //流转卡编码
					TransferName: "",
					WorkOrder: "",
					SmallClass: "",
					SmallClassName: "",
					MachineCode: "", //机台编码
					MachineName: "", //机台编码
					BGProcess: "",
					BGMachine: "",
					UserGroup: "",
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					PTeamCode: "", //生产小组
					UserNames: "", //人员信息
					BGQty: "", //报工数量
					BadQty: "", //不良数量
					HasBGQty: 0, //已报工数量
					BatchNo: "",
				},
				form2: {
					BadItemCode: "",
					BadItemName: "",
					BadQty: "",
				},
				badItemDetailList: [], //不良信息

				processList: [], //工序列表
				showProcess: false, //工序是否显示弹窗
				badItemList: [], //不良项目
				showBadItem: false, //不良项目弹窗
				//焦点
				focusCardCode: false,
				focusPTeamCode: false,
				focusMachineCode: false,
				focusQty: false,
				focusBadQty: false,
			}
		},
		//预加载
		onLoad() {
			_self = this;

			this.getLoginInfo();
			//this.getProcessList();
			//_self.setFocus("focusCardCode");

		},
		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.$on("to-parent", res => {
				console.log("回传");
				this.form2.BadItemCode = res.result.BadItemCode;
				this.form2.BadItemName = res.result.BadItemName;
				uni.$off("to-parent");
				_self.setFocus("focusBadQty");
			});
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMOwnProductBG")
			});

		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['GetPMProcessBadItem', 'OwnProductBGCardScan', 'TransferCardBGPTeamScan',
				'OwnProductBGSave'
			]),
			...mapActions('common', ['GetProcessModel', 'GetModelResourceByChild']),
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
				this.form.TransferCode = value; //流转卡
				if (this.form.TransferCode != "") {
					_self.OwnProductBGCard_Scan();
				}
			},

			clear() {
				this.form.TransferCode = ""; //流转卡
			},

			//机台扫描事件
			searchQR1() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode1(res.result);
					}
				});
			},

			//机台扫描方法
			searchCardCode1(value) {
				this.form.MachineCode = value; //机台

				//ResourceCode
				this.GetModelResourceByChild({
					ResourceCode: this.form.MachineCode
				}).then(res => {
					if (res.success) {
						this.form.ProcessCode = res.resultData.ResourceCode
					} else {
						this.form.ProcessCode = ""
					}
				})


				if (this.form.MachineCode) {
					_self.setFocus("focusPTeamCode");
				}
			},

			clear1() {
				this.form.MachineCode = ""; //机台
			},


			//生产小组扫描方法
			searchQR2() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode2(res.result);
					}
				});
			},

			//扫码事件-生产小组
			searchCardCode2(value) {
				this.form.PTeamCode = value;
				if (this.form.PTeamCode != "") {
					this.transferCardBGPTeamScan();

				}
			},

			clear2() {
				this.form.PTeamCode = ""; //流转卡
			},


			//流转卡扫描方法
			OwnProductBGCard_Scan() {
				console.log(1)
				let query = {
					cardCode: this.form.TransferCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.OwnProductBGCardScan(query).then(res => {
					uni.hideLoading();

					if (res.success) {

						this.form.OwnProductId = res.resultData.OwnProductId;
						this.form.WorkOrder = res.resultData.WorkOrder;
						this.form.TransferCode = res.resultData.TransferCode;
						this.form.TransferName = res.resultData.TransferName;
						this.form.MaterialCode = res.resultData.MaterialCode;
						this.form.MaterialName = res.resultData.MaterialName;
						this.form.SmallClass = res.resultData.SmallClass;
						this.form.SmallClassName = res.resultData.SmallClassName;
						this.form.BatchNo = res.resultData.BatchNo;
						this.form.HasBGQty = res.resultData.HasBGQty;
						if (this.form.HasBGQty && this.form.HasBGQty > 0) {
							uni.showModal({
								title: this.$t("PMOwnProductBG.modalTitle"),

								cancelText: this.$t("showModal.cancel"),
								confirmText: this.$t("showModal.confirm"),
								content: this.$t("PMOwnProductBG.modalContent", [this.form.HasBGQty]),
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
						_self.setFocus("focusQty");
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
				let query = {
					pTeamCode: this.form.PTeamCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.TransferCardBGPTeamScan(query).then(res => {
					uni.hideLoading();
					_self.setFocus("focusCardCode")
					if (res.success) {
						let arr = res.resultData.map(item => {
							return item.UserName;
						});
						this.form.UserNames = arr.join(',');
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
				if (!this.form.ProcessCode) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				uni.navigateTo({
					url: '/pages/public/BadModel?ProcessCode=' + this.form.ProcessCode,
				});
			},
			//返回页面
			goBack() {
				uni.switchTab({
					url: "/pages/index/index",
				});
			},
			//添加不良
			addBadItem() {
				if (!this.form2.BadItemCode) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form2.BadQty) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				let filterList = this.badItemDetailList.filter(item => item.BadItemCode == this.form2.BadItemCode);
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_5'),
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

			save() {
				if (!this.form.TransferCode) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_6'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.MachineCode) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.PTeamCode) {
					this.$refs.uToast.show({
						title: this.$t('PMOwnProductBG.MessageTips_8'),
						type: 'warning',
						icon: true
					});
					return;
				}
				this.form.BGProcess = this.form.ProcessCode;
				this.form.BGMachine = this.form.MachineCode;
				this.form.UserGroup = this.form.PTeamCode;

				let data = {
					entity: this.form,
					badQty: this.form.BadQty,
					userNames: this.form.UserNames,
					badItemDetailList: this.badItemDetailList,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp'
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.OwnProductBGSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('PMOwnProductBG.MessageTips_9'),
							type: 'success',
							icon: true
						});
						_self.reset();
						_self.setFocus("focusCardCode");
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
				this.form.OwnProductId = "";
				this.form.HasBGQty = ""; //已报工数量
				this.form.TransferCode = "";
				this.form.TransferName = "";
				this.form.MaterialCode = "";
				this.form.MaterialName = "";
				this.form.ProductOrder = "";
				this.form.WorkOrder = "";
				this.form.SmallClassName = "";
				this.form.BGQty = "";
				this.form.BadQty = "";
				this.form2.BadItemCode = "";
				this.form2.BadItemName = "";
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
			//初始化工序列表
			getProcessList() {
				var data = {
					FactoryCode: this.$FactoryCode
				}
				this.GetProcessModel(data).then(res => {
					this.processList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.processList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.processList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
						}
					} else {
						this.processList = [{
							value: '',
							label: this.$t("common.None")
						}];
					}
				});
			},
			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
				_self.setFocus("focusCardCode");

			},
			// //初始化不良项目
			// getPMProcessBadItem() {
			// 	if (!this.form.ProcessCode) {
			// 		this.$refs.uToast.show({
			// 			title: '请选择操作工序',
			// 			type: 'warning',
			// 			icon: true
			// 		});
			// 		return;
			// 	}
			// 	let data = {
			// 		processCode: this.form.ProcessCode
			// 	}
			// 	this.GetPMProcessBadItem(data).then(res => {
			// 		if (res.success) {
			// 			this.badItemList = res.resultData;
			// 		} else {
			// 			this.badItemList = [{
			// 				value: '',
			// 				label: '无'
			// 			}];
			// 		}
			// 	});
			// },
			// //选择不良项目
			// changeBadItem(val) {
			// 	this.form2.BadItemCode = val[0].value; //val[0].label;				
			// 	this.form2.BadItemName = val[0].label;
			// },
			//查询登录信息
			getLoginInfo() {
				let data = {
					userCode: this.loginInfo.result.UserCode,
				};
				this.GetLoginInfo(data).then(res => {
					if (res.success) {
						this.form.MachineCode = res.resultData.MachineCode; //机台编码
						this.form.PTeamCode = res.resultData.PTeamCode; //生产小组
						_self.transferCardBGPTeamScan(); //生产小组
						_self.searchCardCode1(this.form.MachineCode);
					}
				});
			},
			initFocus() {
				this.focusCardCode = false
				this.focusPTeamCode = false
				this.focusMachineCode = false
				this.focusQty = false
				this.focusBadQty = false
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

	.label {
		line-height: 20px;
		width: 250px;
	}
</style>