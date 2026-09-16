<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
				<u-form-item :label="$t('PMShopCheckUp.CardCode')" required>
					<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('PMShopCheckUp.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMShopCheckUp.ProductOrder')">
					<u-input v-model="form.ProductOrder" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMShopCheckUp.ContainerNO')">
					<u-input v-model="form.ContainerNO" disabled="" type="text" placeholder="" border
						class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMShopCheckUp.MMXH')">
					<u-input v-model="form.MMXH" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMShopCheckUp.Spec')">
					<u-input v-model="form.Spec" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMShopCheckUp.MachineCode')" required>
					<u-search v-model="form.MachineCode" @custom="custom" @search="searchMachineCode" @clear="clear1"
						:placeholder="$t('PMShopCheckUp.MachineCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('common.photosUpload')" style="height: auto;">
					<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="9"
						:upload-text="$t('common.chooseTips')"></u-upload>
				</u-form-item>
			</u-form>
			<view>
				<u-divider halfWidth="100%">{{$t('PMShopCheckUp.InputResult')}}</u-divider>
			</view>
			<scroll-view scroll-y="true" class="scroll-Y">
				<view style="height: 400rpx;border-bottom: 1px solid Gainsboro;">
					<u-table style="margin-top: 20rpx;height: 350rpx;">
						<u-tr class="u-tr">
							<u-th>{{$t('PMShopCheckUp.TestItemName')}}</u-th>
							<u-th>{{$t('PMShopCheckUp.TestItemStandard')}}</u-th>
							<u-th width="36%">{{$t('PMShopCheckUp.QualityResult')}}</u-th>
						</u-tr>
						<u-tr v-for="(item,index) of CheckList" :key="index">
							<u-th>{{item.TestItemName}}</u-th>
							<u-th>{{item.TestItemStandard}}</u-th>
							<u-th width="36%" v-if="item.DataTypeName == '数值'">
								<u-input v-model="item.QualityResult"
									:placeholder="$t('common.Number_placeholder')" type="number" border />
							</u-th>
							<u-th width="36%" v-else-if="item.DataTypeName == '文本'">
								<u-input v-model="item.QualityResult"
									:placeholder="$t('common.String_placeholder')" type="text" border />
							</u-th>

							<u-th width="36%" v-else-if="item.DataTypeName == '日期'">
								<u-input v-model="item.QualityResult"
									:placeholder="$t('common.Date_placeholder')"
									@click="ShowactionDATE(item.TestItemCoading,item.DataTypeName)" type="select"
									border />
							</u-th>
							<u-th width="36%" v-else>
								<!-- ="item.DataTypeName.index('/') > 0" -->
								<u-input v-model="item.QualityResult"
									:placeholder="$t('common.Select_placeholder')" type="select"
									@click="ShowactionSheetLis(item.TestItemCoading,item.DataTypeName)" border />
								<u-action-sheet :list="TestItemList" v-model="IsTestItemList"
									@click="actionSheetCallback">
								</u-action-sheet>
							</u-th>
						</u-tr>
					</u-table>
				</view>
			</scroll-view>
			<view style=";">
				<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
					<u-form-item :label="$t('PMShopCheckUp.firtResultNAME')">
						<u-input v-model="form.firtResultNAME" @click="showSel('showTestItem')" type="text" disabled
							:placeholder="$t('PMShopCheckUp.firtResultNAME_placeholder')" border />
					</u-form-item>
				</u-form>
			</view>
		</view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="save"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('PMShopCheckUp.SaveBtn')}}
			</u-button>
			<u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="sel"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('PMShopCheckUp.SearchBtn')}}
			</u-button>
		</view>

		<!-- 保养项目单日期选择 -->
		<u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
		<!--首检判定-->
		<u-select v-model="showTestItem" @confirm="changeTestItem" :list="pdList"></u-select>

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
	import global from '@/utils/global'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				action: global.FileHandler, //图片上传地址
				filePath: global.FilePath,
				fileList: [], //文件上传列表

				form: {
					CardCode: "", //流转卡编码
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					TransferBy: "", //流转方式
					TransferByName: "",
					MachineCode: "", //机台
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					MMXH: "", //面膜型号
					Spec: "", //规格型号
					firtResultCode: "",
					firtResultNAME: "",
					processCode: "",

				},
				chkAll: false,
				//工序列表
				processList: [],
				//工序是否显示弹窗

				showTestItem: false,
				isShowDate2: false,
				CheckList: [], //检测项目
				TestItemList: [],
				pdList: [],
				IsTestItemList: false,
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
			_self.setFocus("focus1");
			this.getsJList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMShopCheckUp")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['FirstInspectionCardScan', 'FirstInspectionMachineScan', 'FirstInspectionSave']),
			...mapActions('common', ['GetDictionary']),

			//首检结果下拉回调事件
			actionSheetCallback(index) {
				console.info('保养项目下拉回调内容', this.TestItemList[index].text);
				let filterList = this.CheckList.filter(item => item.TestItemCoading == this
					.TestItemCoading);
				console.info('当前选择保养项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].QualityResult = this.TestItemList[index].text;
				}
			},

			//保养项目日期点击初始下拉内容与显示下拉面板
			ShowactionDATE(TestItemCoading, DataTypeName) {
				this.TestItemCoading = TestItemCoading;
				console.info('保养项目编码与内容', TestItemCoading, DataTypeName);
				this.isShowDate2 = true; //显示日期选择
			},


			//首检项目下拉点击初始下拉内容与显示下拉面板
			ShowactionSheetLis(TestItemCoading, DataTypeName) {
				this.TestItemCoading = TestItemCoading;
				console.info('保养项目编码与内容', TestItemCoading, DataTypeName);
				this.TestItemList = [];
				let ep_item = DataTypeName.split("/"); //字符分割
				ep_item.forEach((item, index) => {
					this.TestItemList.push({
						text: item
					})
				});

				this.IsTestItemList = true; //显示首检结果下拉框
			},


			//显示下拉框
			showSel(val, item) {
				if (val == "process")
					this.showProcess = true;
				else if (val == "showTestItem") {
					this.showTestItem = true;
				}
			},
			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
			},
			//选择判定结果
			changeTestItem(val) {

				this.form.firtResultCode = val[0].value;
				this.form.firtResultNAME = val[0].label;
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
					this.firstInspectionCardScan();
				}
			},

			clear() {
				this.form.CardCode = ""; //流转卡
			},


			//机台扫描事件
			searchQR1() {
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
				if (this.form.MachineCode) {
					this.firstInspectionMachineScan();
				}
			},

			clear1() {
				this.form.MachineCode = ""; //机台
			},


			firstInspectionCardScan() {
				this.cardList = [];
				let query = {
					cardCode: this.form.CardCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.FirstInspectionCardScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.ProductOrder = res.resultData.ProductOrder;
						this.form.ContainerNO = res.resultData.ContainerNO;
						this.form.MMXH = res.resultData.MMXH;
						this.form.Spec = res.resultData.Spec;

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
			firstInspectionMachineScan() {
				this.CheckList = [];
				let query = {
					cardCode: this.form.CardCode,
					machineCode: this.form.MachineCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.FirstInspectionMachineScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						console.log(JSON.stringify(res.resultData));

						this.form.processCode = res.resultData.processCode;
						res.resultData.scanResult.CheckList.forEach((item, index) => {
							this.CheckList.push({
								"TestItemId": item.TestItemId,
								"TestItemCoading": item.TestItemCoading,
								"TestItemName": item.TestItemName,
								"TestItemStandard": item.TestItemStandard,
								"DataType": item.DataType,
								"DataTypeName": item.DataTypeName,
								"QualityResult": "",
								"TestDepartment": item.TestDepartment,
								"Options": item.Options, //下拉数值	

							})
						});
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


			//初始化判定选择列表
			getsJList() {

				var data = {
					EnCode: "TestConclusion"
				}
				this.GetDictionary(data).then(res => {
					this.pdList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.pdList = [];
						} else {
							this.pdList = res.resultData;

						}
					} else {
						this.pdList = [{
							value: '',
							label: this.$t("common.None")
						}];
					}
				});
			},

			//查询
			sel() {

				uni.navigateTo({
					url: '/pages/ProduceModel/PMShopSel',
				});

			},


			save() {
				if (this.form.CardCode == "") {
					this.$refs.uToast.show({
						title: this.$t("PMShopCheckUp.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.MachineCode == "") {
					this.$refs.uToast.show({
						title: this.$t("PMShopCheckUp.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}


				let IsWarning = false; //是否显示提醒
				//检测保养项目是否都填写
				if (this.CheckList.length > 0) {
					this.CheckList.forEach((item, index) => {
						if (item.QualityResult == '') {
							IsWarning = true; //是否显示提醒
							this.$refs.uToast.show({
								title: this.$t("PMShopCheckUp.MessageTips_3"),
								type: 'warning',
								icon: true
							});

							return;
						}
					});
				}
				if (IsWarning) {
					this.$refs.uToast.show({
						title: this.$t("PMShopCheckUp.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let UrlStr = '';
				//通过filter，筛选出上传进度为100的文件(因为某些上传失败的文件，进度值不为100，这个是可选的操作)
				var files = this.$refs.uUpload.lists.filter(val => {
					return val.progress == 100;
				})

				var fileUrl = [];
				files.forEach(item => {
					fileUrl.push({
						FileName: item.file.name,
						ImgType: item.file.type,
						FilePath: this.filePath + item.response.resultData
					})
				})

				if (fileUrl.length > 0) {
					UrlStr = this.$t("common.Have")
				} else {
					UrlStr = this.$t("common.None")
				}




				let data = {
					processCode: this.form.processCode,
					cardCode: this.form.CardCode,
					MachineCode: this.form.MachineCode,
					firtResult: this.form.firtResultCode,
					checkList: this.CheckList,
					Attachment: UrlStr,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					fileUrl: fileUrl,

				};
				console.log(JSON.stringify(data));
				this.FirstInspectionSave(data).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMShopCheckUp.MessageTips_5"),
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
				this.form.PTeamCode = "";
				this.form.ProductOrder = "";
				this.form.ContainerNO = "";
				this.form.MMXH = "";
				this.form.Spec = "";
				this.form.MachineCode = "";
				this.firtResultCode = "",
					this.firtResultNAME = "",
					this.form.TotalPallet = "";
				this.CheckList = [];
				this.pdList = [];
				this.fileList = [];
				this.$refs.uUpload.clear();
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