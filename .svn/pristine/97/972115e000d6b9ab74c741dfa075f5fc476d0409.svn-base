<template>
	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
			<!-- <u-form-item label="工厂" prop="FactoryName">
				<u-input v-model="form.FactoryName" @click="showSel('factory')" type="text" disabled placeholder="请选择工厂"
					border />
				</u-button>
			</u-form-item>
			<u-form-item label="操作工序" prop="ProcessName">
				<u-input v-model="form.ProcessName" @click="showSel('process')" type="text" disabled placeholder="请选择工序"
					border />
			</u-form-item> -->
			<u-form-item :label="$t('PerPalletStart.MachineCode')" required>
				<u-search v-model="form.MachineCode" @custom="custom" @search="search2" @clear="clear2"
					:placeholder="$t('PerPalletStart.MachineCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR2"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PerPalletStart.ProcessName')">
				<u-input v-model="form.ProcessName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PerPalletStart.CardCode')" required>
				<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('PerPalletStart.CardCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus2">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PerPalletStart.MaterialCode')">
				<u-input v-model="form.MaterialCode" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PerPalletStart.MMXH')">
				<u-input v-model="form.MMXH" disabled="" type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PerPalletStart.Spec')">
				<u-input v-model="form.Spec" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PerPalletStart.TotalPallet')">
				<u-input v-model="form.TotalPallet" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PerPalletStart.CardCodeInfo')}}</u-divider>
		</view>
		<view style="height: auto;">
			<!-- <u-checkbox v-show="cardList.length>0" v-model="chkAll" @change="chkAllChange"><text
					class="u-font-14">全选</text></u-checkbox> -->
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 500rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in cardList">
						<!-- <u-checkbox v-model="item.Checked" style="width:100%;"
							:disabled="!cardStatus.includes(item.CardStatus)"> -->
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text style="font-size: 14px;">
									{{item.CardCode}}&#12288&#12288{{item.PieceQty}}
								</text>
							</template>
							<!-- <view>托盘张数：{{item.PalletQty}}</view>
								<view>流转卡类型：{{item.CardTypeName}}</view> -->
						</u-collapse-item>
						<!-- </u-checkbox> -->
					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PerPalletStart.SaveBtn')}}</text>
			</u-button>

		</view>
		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"></u-select>
		<!-- 工序选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
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
					FactoryCode: "",
					FactoryName: "",
					CardCode: "", //流转卡编码
					ProcessCode: "", //工序编码
					ProcessName: "", //工序名称
					MachineCode: "", //机台编码
					MachineName: "", //机台名称
					MaterialCode: "", //客户型号
					MMXH: "", //面膜型号
					Spec: "", //规格
					TotalPallet: "", //总托数
				},
				chkAll: false,
				factoryList: [], //工厂列表
				showFactory: false, //工厂弹窗
				//工序列表
				processList: [],
				//工序是否显示弹窗
				showProcess: false,
				seachTit: "",
				cardList: [],
				cardStatus: ['1', '2', '4'], //1：正常 2：待返工 4:已返工
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
			// _self.getFactoryList();
			_self.getLoginInfo();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PerPalletStart")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['PerPalletStartMachineScan', 'PerPalletStartCardScan', 'PerPalletStartSave']),
			...mapActions('common', ['GetResourceByLevelCode', 'GetProcessModel']),
			...mapActions('user', ['GetLoginInfo']),

			//初始化工厂列表
			getFactoryList() {
				var data = {
					LevelCode: "Factory"
				}
				this.GetResourceByLevelCode(data).then(res => {
					this.factoryList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.factoryList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.factoryList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.form.FactoryCode = res.resultData[0].ResourceCode;
							this.form.FactoryName = res.resultData[0].ResourceName;
							_self.getProcessList();
						}
					} else {
						this.factoryList = [{
							value: '',
							label: this.$t("common.None")
						}];
					}
				});
			},
			//初始化工序列表
			getProcessList() {
				var data = {
					FactoryCode: this.form.FactoryCode
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
							this.form.ProcessCode = "";
							this.form.ProcessName = "";
							_self.getLoginInfo();
						}
					} else {
						this.processList = [{
							value: '',
							label: this.$t("common.None")
						}];
					}
				});
			},
			//显示下拉框
			showSel(val, item) {
				if (val == "factory")
					this.showFactory = true;
				else if (val == "process")
					this.showProcess = true;
			},
			//选择工厂
			changeFactory(val) {
				this.form.FactoryCode = val[0].value; //val[0].label;				
				this.form.FactoryName = val[0].label;
				_self.getProcessList();
			},
			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
				_self.setFocus("focus1");
			},
			//机台扫描事件
			searchQR2() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search2(res.result);
					}
				});
			},

			//条码查询
			search2(value) {
				this.form.MachineCode = value; //机台
				if (this.form.MachineCode) {
					this.perPalletStartMachineScan();
				}
			},

			clear2() {
				this.form.MachineCode = ""; //机台
			},
			//机台扫描方法
			perPalletStartMachineScan() {
				if (!this.form.MachineCode)
					return;

				let query = {
					machineCode: this.form.MachineCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.PerPalletStartMachineScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.MachineName = res.resultData.MachineName;
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
				if (this.form.CardCode) {
					this.perPalletStartCardScan();
				}
			},

			clear() {
				this.form.CardCode = ""; //流转卡
			},
			//流转卡扫描
			perPalletStartCardScan() {
				if (!this.form.ProcessCode) {
					this.$refs.uToast.show({
						title: this.$t("PerPalletStart.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					this.form.CardCode = "";
					return;
				}
				if (this.cardList.find(t => t.CardCode == this.form.CardCode)) {
					this.$refs.uToast.show({
						title: this.$t("PerPalletStart.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					this.form.CardCode = "";
					return;
				}

				let query = {
					cardCode: this.form.CardCode,
					processCode: this.form.ProcessCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.PerPalletStartCardScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						let findEntity = this.cardList.find(t => t.MaterialCode == res.resultData.MaterialCode);
						if (this.cardList.length > 0 && !findEntity) {
							this.$refs.uToast.show({
								title: this.$t("PerPalletStart.MessageTips_3"),
								type: 'warning',
								icon: true
							});
							this.form.CardCode = "";
							return;
						}
						this.form.MaterialCode = res.resultData.MaterialCode;
						this.form.MMXH = res.resultData.MMXH;
						this.form.Spec = res.resultData.Spec;
						this.form.TotalPallet = this.cardList.length + 1;
						res.resultData.MachineCode = this.form.MachineCode;
						this.cardList.push(res.resultData);
						// this.cardList.forEach(item => {
						// 	this.$set(item, "Checked", false);
						// })
						// this.chkAll = true;
						// _self.chkAllChange();
						this.form.CardCode = "";
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
					this.cardList.forEach(item => {
						if (this.cardStatus.includes(item.CardStatus)) {
							item.Checked = true;
						}
					})
				} else {
					this.cardList.forEach(item => {
						item.Checked = false;
					})
				}
			},
			save() {
				if (!this.form.MachineCode) {
					this.$refs.uToast.show({
						title: this.$t("PerPalletStart.MessageTips_4"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.ProcessName) {
					this.$refs.uToast.show({
						title: this.$t("PerPalletStart.MessageTips_5"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let data = {
					machineCode: this.form.MachineCode,
					processCode: this.form.ProcessCode,
					cardList: this.cardList
				};
				uni.showLoading({
					title: this.$t('common.loading')
				});
				this.PerPalletStartSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PerPalletStart.MessageTips_6"),
							type: 'success',
							icon: true
						});
						this.reset();
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
			reset() {
				this.form.CardCode = "";
				// this.form.ProcessCode = "";
				// this.form.ProcessName = "";
				this.form.MaterialCode = "";
				this.form.MMXH = "";
				this.form.Spec = "";
				this.form.TotalPallet = "";
				this.cardList = [];
			},
			//获取登录人员绑定的开工工序
			getLoginInfo() {
				let data = {
					userCode: this.loginInfo.result.UserCode,
				};
				this.GetLoginInfo(data).then(res => {
					if (res.success) {
						if (res.resultData.MachineCode) {
							this.form.MachineCode = res.resultData.MachineCode; //机台编码
							this.PerPalletStartMachineScan();

						} else {
							_self.setFocus("focus1");
						}
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