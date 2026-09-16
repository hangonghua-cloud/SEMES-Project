<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" label-width="auto">
				<u-form-item :label="$t('PMSortingBG.MachineCode')" required>
					<u-search v-model="form.MachineCode" @custom="custom" @search="search1" @clear="clear1"
						:placeholder="$t('PMSortingBG.MachineCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.ProcessName')" prop="ProcessName">
					<u-input v-model="form.ProcessName" type="text" disabled placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.PTeamCode')" required>
					<u-search v-model="form.PTeamCode" @custom="custom" @search="search2" @clear="clear2"
						:placeholder="$t('PMSortingBG.PTeamCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR2"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.UserNames')">
					<u-input v-model="form.UserNames" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.CardCode')" required>
					<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('PMSortingBG.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus3">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.SortingPort')" required>
					<u-search v-model="form.SortingPort" @custom="custom" @search="search3" @clear="clear3"
						:placeholder="$t('PMSortingBG.SortingPort_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus4">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR3"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.Qty')">
					<u-input v-model="form.Qty" type="number" placeholder="" border :focus="focus5" />
					<text style="margin:0px 40rpx">{{$t('PMSortingBG.BadQty')}}</text>
					<u-input v-model="form.BadQty" disabled type="number" placeholder="" border class="readonly" />
				</u-form-item>

				<u-form-item :label="$t('PMSortingBG.NotBGTS')">
					<u-input v-model="form.NotBGTS" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>

				<u-form-item :label="$t('PMSortingBG.OrderInfo')" style="height: auto;">
					<view v-show="form.ProductOrder" style="border: 1px solid Gainsboro;">
						<view class="label">
							{{$t('PMSortingBG.ProductOrder')}}：{{form.ProductOrder}}&#12288{{$t('PMSortingBG.ContainerNO')}}：{{form.ContainerNO}}
						</view>
						<view class="label">
							{{$t('PMSortingBG.CardName')}}：{{form.CardName}}&#12288{{$t('PMSortingBG.HasBGQty')}}：{{form.HasBGQty}}
						</view>
					</view>
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.Remark')">
					<u-input v-model="form.Remark" type="text" placeholder="" border />
				</u-form-item>
			</u-form>
			<view style="margin-top:10rpx;">
				<u-divider halfWidth="100%">{{$t('PMSortingBG.MachineCode')}}</u-divider>
			</view>
			<u-form :model="form2" label-width="auto">
				<u-form-item :label="$t('PMSortingBG.BadItemName')" prop="BadItemCode">
					<u-input v-model="form2.BadItemName" @click="OpenModel" type="text" disabled
						:placeholder="$t('PMSortingBG.BadItemName_placeholder')" border />
					<u-icon name="search" size="70rpx" color="#138087" @click="OpenModel"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMSortingBG.BadQty')">
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
						<view class="label u-line-1">{{$t('PMSortingBG.BadItemName')}}：{{item.BadItemName}}</view>
						<view class="label u-line-1">{{$t('PMSortingBG.BadQty')}}：{{item.BadQty}}</view>
					</u-checkbox>
				</view>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMSortingBG.SaveBtn')}}</text>
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
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					CardCode: "", //流转卡编码
					CardName: "",
					ProductOrder: "",
					ContainerNO: "", //柜号
					MachineCode: "", //机台编码
					SortingPort: "", //分拣口
					PTeamCode: "", //生产小组
					UserNames: "", //人员信息
					Qty: "", //报工数量
					BadQty: "", //不良数量
					HasBGQty: 0, //已报工数量
					NotBGTS: "",
					Remark: "", //备注
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
				focus1: false,
				focus2: false,
				focus3: false,
				focus4: false,
				focus5: false,
				focus6: false,
			}
		},
		//预加载
		onLoad() {
			_self = this;
			_self.setFocus("focus3");
			// this.getProcessList();
			this.getLoginInfo();
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
				_self.setFocus("focus6");
			});

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMSortingBG")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['GetPMProcessBadItem', 'SortingBGMachineScan', 'SortingBGCardScan',
				'SortingBGPTeamScan', 'SortingBGSave'
			]),
			...mapActions('common', ['GetProcessModel']),
			...mapActions('user', ['GetLoginInfo']),

			//流转卡扫描
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode(res.result);
					}
				});
			},

			//流转卡查询
			searchCardCode(value) {
				this.form.CardCode = value; //流转卡
				if (this.form.CardCode != "") {
					this.sortingBGCardScan();
				}
			},
			//流转卡清空
			clear() {
				this.form.CardCode = ""; //流转卡
			},

			//机台扫描
			searchQR1() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search1(res.result);
					}
				});
			},

			//机台查询
			search1(value) {
				this.form.MachineCode = value; //机台
				if (this.form.MachineCode) {
					_self.sortingBGMachineScan();
				}
			},
			//机台清空
			clear1() {
				this.form.MachineCode = ""; //机台
			},

			//生产小组扫描
			searchQR2() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search2(res.result);
					}
				});
			},

			//生产小组查询
			search2(value) {
				this.form.PTeamCode = value; //流转卡
				if (this.form.PTeamCode != "") {
					this.sortingBGPTeamScan();

				}
			},
			//生产小组清空
			clear2() {
				this.form.PTeamCode = ""; //流转卡
			},
			//分拣口扫描
			searchQR3() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search3(res.result);
					}
				});
			},

			//分拣口查询
			search3(value) {
				this.form.SortingPort = value;
				_self.setFocus("focus5");
			},
			//分拣口清空
			clear3() {
				this.form.SortingPort = "";
			},

			//机台描方法
			sortingBGMachineScan() {
				if (!this.form.MachineCode)
					return;

				let query = {
					machineCode: this.form.MachineCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.SortingBGMachineScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.ProcessCode = res.resultData.ProcessCode;
						this.form.ProcessName = res.resultData.ProcessName;
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

			//流转卡扫描方法
			sortingBGCardScan() {

				if (!this.form.ProcessCode) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_2"),
						type: 'warning',
						icon: true
					});
				}
				let query = {
					cardCode: this.form.CardCode,
					processCode: this.form.ProcessCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.SortingBGCardScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.ProductOrder = res.resultData.ProductOrder;
						this.form.ContainerNO = res.resultData.ContainerNO;
						this.form.CardName = res.resultData.CardName;
						//this.form.ProcessCode = res.resultData.ProcessCode;
						this.form.NotBGTS = res.resultData.NotBGTS;
						this.form.HasBGQty = res.resultData.HasBGQty;
						if (this.form.HasBGQty && this.form.HasBGQty > 0) {
							uni.showModal({
								title: '提示',
								cancelText: this.$t("showModal.cancel"),
								confirmText: this.$t("showModal.confirm"),
								content: '当前流转卡已报工数量' + this.form.HasBGQty + ',请确认是否再次进行报工',
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
						_self.setFocus("focus4");
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
						this.form.CardCode = ""; //清空
					}
				});
			},
			//生产小组扫描方法
			sortingBGPTeamScan() {
				if (!this.form.PTeamCode)
					return;

				let query = {
					pTeamCode: this.form.PTeamCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.SortingBGPTeamScan(query).then(res => {
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
				if (!this.form.ProcessCode) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_11"),
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
						title: this.$t("PMSortingBG.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form2.BadQty) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_4"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let filterList = this.badItemDetailList.filter(item => item.BadItemCode == this.form2.BadItemCode);
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_5"),
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
				if (!this.form.CardCode) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_6"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.MachineCode) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_7"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.SortingPort) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_8"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.PTeamCode) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_9"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form2.BadItemCode) {
					this.$refs.uToast.show({
						title: this.$t("PMSortingBG.MessageTips_10"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let data = {
					processCode: this.form.ProcessCode,
					cardCode: this.form.CardCode,
					machineCode: this.form.MachineCode,
					sortingPort: this.form.SortingPort,
					qty: this.form.Qty,
					badQty: this.form.BadQty,
					pTeamCode: this.form.PTeamCode,
					userNames: this.form.UserNames,
					remark: this.form.Remark,
					badItemDetailList: this.badItemDetailList
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.SortingBGSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMSortingBG.MessageTips_1"),
							type: 'success',
							icon: true
						});
						_self.reset();
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
			reset() {
				this.form.CardCode = "";
				this.form.CardName = "";
				this.form.ProductOrder = "";
				this.form.ContainerNO = "";
				this.form.Qty = "";
				this.form.BadQty = "";
				this.form.SortingPort = "";
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
				_self.setFocus("focus3");
				//this.getPMProcessBadItem();
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
						_self.sortingBGMachineScan();
						_self.sortingBGPTeamScan(); //生产小组
					}
				});
			},
			initFocus() {
				this.focus1 = false
				this.focus2 = false
				this.focus3 = false
				this.focus4 = false
				this.focus5 = false
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