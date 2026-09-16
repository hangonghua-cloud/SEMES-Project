<template>
	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMStart.FactoryName')" prop="FactoryName">
				<view @click="showSel('factory')" style="width: 100%;">
					<u-input v-model="form.FactoryName" disabled placeholder="请选择工厂" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('PMStart.ProcessName')" prop="ProcessName">
				<view @click="showSel('process')" style="width: 100%;">
					<u-input v-model="form.ProcessName" disabled placeholder="请选择工序" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('PMStart.CardCode')" required>
				<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('PMStart.CardCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMStart.TransferByName')">
				<u-input v-model="form.TransferByName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMStart.HealthTime')">
				<u-input v-model="form.HealthTime" disabled="" type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMStart.InProcessName')">
				<u-input v-model="form.InProcessName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMStart.TotalPallet')">
				<u-input v-model="form.TotalPallet" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PMStart.CardInfo')}}</u-divider>
		</view>
		<view style="height: auto;">
			<u-checkbox v-show="cardList.length>0" v-model="chkAll" @change="chkAllChange"><text
					class="u-font-14">{{$t('PMStart.SelectAll')}}</text></u-checkbox>
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 500rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in cardList">
						<u-checkbox v-model="item.Checked" style="width:100%;"
							:disabled="!cardStatus.includes(item.CardStatus)">
							<u-collapse-item class="u-collapse-item">
								<template slot="title">
									<text style="font-size: 14px;">
										{{item.CardName}}&#12288{{item.ProcessName}}&#12288{{item.CardStatusName}}&#12288{{item.BusinessTypeName}}&#12288{{item.BGQty}}
									</text>
								</template>
								<view>{{$t('PMStart.PalletQty')}}：{{item.PalletQty}}</view>
								<view>{{$t('PMStart.CardTypeName')}}：{{item.CardTypeName}}</view>
							</u-collapse-item>
						</u-checkbox>
					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMStart.SaveBtn')}}</text>
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
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					TransferBy: "", //流转方式
					TransferByName: "",
					HealthTime: "", //养生时间
					InProcessCode: "", //所在工序编码
					InProcessName: "", //所在工序名称
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
			_self.getFactoryList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMStart")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['GetPMStartInfo', 'SavePMStartInfo']),
			...mapActions('common', ['GetResourceByLevelCode', 'GetProcessModel']),
			...mapActions('user', ['GetLoginInfo']),

			//初始化工厂列表
			getFactoryList() {
				var data = {
					LevelCode: "Factory"
				}
				this.GetResourceByLevelCode(data).then(res => {
					// debugger;
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
							label: this.$t("PMStart.None")
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
							label: this.$t("PMStart.None")
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
				// debugger;
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
					this.getPMStartInfo();
				}
			},

			clear() {
				this.form.CardCode = ""; //流转卡
			},

			getPMStartInfo() {
				this.cardList = [];
				let query = {
					cardCode: this.form.CardCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.GetPMStartInfo(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.TransferBy = res.resultData.TransferBy;
						this.form.TransferByName = res.resultData.TransferByName;
						this.form.HealthTime = res.resultData.HealthTime;
						this.form.InProcessCode = res.resultData.ProcessCode;
						this.form.InProcessName = res.resultData.ProcessName;
						this.form.TotalPallet = res.resultData.TotalPallet;
						this.cardList = res.resultData.CardList;
						this.cardList.forEach(item => {
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
				if (this.form.CardCode == "") {
					this.$refs.uToast.show({
						title: this.$t("PMStart.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.ProcessName == "") {
					this.$refs.uToast.show({
						title: this.$t("PMStart.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let selectedItems = this.cardList.filter(item => {
					return item.Checked == true;
				});
				if (selectedItems.length == 0) {
					this.$refs.uToast.show({
						title: this.$t("PMStart.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					processCode: this.form.ProcessCode,
					processName: this.form.ProcessName,
					cardCode: this.form.CardCode,
					transferBy: this.form.TransferBy,
					healthTime: this.form.HealthTime,
					inProcessCode: this.form.InProcessCode,
					inProcessName: this.form.InProcessName,
					totalPallet: this.form.TotalPallet,
					cardList: selectedItems,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp'
				};
				uni.showLoading({
					title: this.$t('common.loading')
				});
				this.SavePMStartInfo(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMStart.MessageTips_5"),
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
				this.form.TransferBy = "";
				this.form.TransferByName = "";
				this.form.HealthTime = "";
				this.form.InProcessCode = "";
				this.form.InProcessName = "";
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
						if (res.resultData.ProcessCode) {
							this.form.ProcessCode = res.resultData.ProcessCode; //开工工序
							this.form.ProcessName = this.processList.find(item => item.value == this.form
									.ProcessCode)
								.label;
							_self.setFocus("focus1");
						}
						console.log("工序名称：" + this.form.ProcessName);
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