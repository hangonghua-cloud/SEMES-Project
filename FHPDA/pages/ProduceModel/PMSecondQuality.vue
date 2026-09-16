<template>
	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMSecondQuality.TransferCode')" required>
				<u-search v-model="form.TransferCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('PMSecondQuality.TransferCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>

			<u-form-item :label="$t('PMSecondQuality.ProductOrder')">
				<u-input v-model="form.ProductOrder" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMSecondQuality.ContainerNO')">
				<u-input v-model="form.ContainerNO" disabled="" type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMSecondQuality.MMXH')">
				<u-input v-model="form.MMXH" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMSecondQuality.Spec')">
				<u-input v-model="form.Spec" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>

			<!-- <u-form-item label="生产机台" required>
				<u-search v-model="form.MachineCode" @custom="custom" @search="searchCardCode1" @clear="clear1" placeholder="生产机台扫描"
				 shape="square" border :show-action="showAction=false" :focus="focus2">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
			</u-form-item> -->

			<u-form-item :label="$t('common.photosUpload')" style="height: auto;">
				<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="40"
					:upload-text="$t('common.chooseTips')"></u-upload>
			</u-form-item>


		</u-form>

		<view style="margin-top:85px;">
			<u-divider halfWidth="100%">{{$t('PMSecondQuality.TestInfo')}}</u-divider>
		</view>
		<scroll-view scroll-y="true" class="scroll-Y">
			<view style="height: 500rpx;">
				<u-table style="margin-top: 20rpx;">
					<u-tr class="u-tr">
						<u-th>{{$t('PMSecondQuality.TestItemName')}}</u-th>
						<u-th>{{$t('PMSecondQuality.TestItemStandard')}}</u-th>

						<u-th width="36%">{{$t('PMSecondQuality.QualityResult')}}</u-th>
					</u-tr>
					<u-tr v-for="(item,index) of CheckList" :key="index">
						<u-th>{{item.TestItemName}}</u-th>
						<u-th>{{item.TestItemStandard}}</u-th>
						<u-th width="36%" v-if="item.DataTypeName == '数值'">
							<u-input v-model="item.QualityResult" :placeholder="$t('common.Number_placeholder')"
								type="number" border />
						</u-th>
						<u-th width="36%" v-else-if="item.DataTypeName == '文本'">
							<u-input v-model="item.QualityResult" :placeholder="$t('common.String_placeholder')"
								type="text" border />
						</u-th>

						<u-th width="36%" v-else-if="item.DataTypeName == '日期'">
							<u-input v-model="item.QualityResult" :placeholder="$t('common.Date_placeholder')"
								@click="ShowactionDATE(item.TestItemCoading,item.DataTypeName)" type="select" border />
						</u-th>
						<u-th width="36%" v-else>
							<!-- ="item.DataTypeName.index('/') > 0" -->
							<u-input v-model="item.QualityResult" :placeholder="$t('common.Select_placeholder')"
								type="select"
								@click="ShowactionSheetLis(item.TestItemCoading,item.DataTypeName,item.Options)"
								border />
							<u-action-sheet :list="TestItemList" v-model="IsTestItemList" @click="actionSheetCallback">
							</u-action-sheet>
						</u-th>
					</u-tr>
				</u-table>
			</view>
		</scroll-view>
		<view style=";">
			<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
				<u-form-item :label="$t('PMSecondQuality.secondResultNAME')">
					<u-input v-model="form.secondResultNAME" @click="showSel('showTestItem')" type="text" disabled
						:placeholder="$t('PMSecondQuality.secondResultNAME_placeholder')" border />
				</u-form-item>
			</u-form>
		</view>

		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="save"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('PMSecondQuality.SaveBtn')}}
			</u-button>
			<u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="sel"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('PMSecondQuality.SearchBtn')}}
			</u-button>
		</view>


		<!-- 保养项目单日期选择 -->
		<u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
		<!--复检判定-->
		<u-select v-model="showTestItem" @confirm="changeTestItem" :list="pdjgList"></u-select>

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
					MachineName: "",
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					MMXH: "", //面膜型号
					Spec: "", //规格型号
					secondResultCode: "",
					secondResultNAME: "",
					firstInspectionConfirmId: "",
					laboratoryTestStatus: "",
				},
				chkAll: false,
				//工序列表
				processList: [],
				//工序是否显示弹窗
				IsTestItemList: false,
				showTestItem: false,
				isShowDate2: false,
				CheckList: [], //检测项目

				CheckListAll: [],
				TestItemList: [],
				TestItemId: "",
				pdList: [],
				pdjgList: [],
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
			//this.id = options.id;
			//this.getSecondList();
			this.getsJList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.QualityModel.ProduceModel/PMSecondQuality")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['FirstInspectionCardScan', 'FirstInspectionConfirmSave',
				'FirstInspectionConfirmMachieScan', 'GetprocessList'
			]),
			...mapActions('common', ['GetDictionary']),

			//首检结果下拉回调事件
			actionSheetCallback(index) {
				console.info('保养项目下拉回调内容', this.TestItemList[index].text);
				let filterList = this.CheckList.filter(item => item.TestItemCoading == this
					.TestItemCoading);
				//console.info('当前选择保养项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].QualityResult = this.TestItemList[index].text;
				}
			},
			//检验项目日期点击初始下拉内容与显示下拉面板
			ShowactionDATE(TestItemCoading, DataTypeName) {
				this.TestItemCoading = TestItemCoading;
				console.info('检验项目编码与内容', TestItemCoading, DataTypeName);

				this.isShowDate2 = true; //显示日期选择
			},


			//检验项目日期点击初始下拉内容与显示下拉面板  回调
			dateChange2(e) {
				console.info('检验项目日期回调内容', e.result);
				let filterList = this.CheckList.filter(item => item.TestItemId == this
					.TestItemId);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].QualityResult = e.result;
				}
			},
			getDate(val) {
				console.info('检验项目日期回调内容', val);
				let filterList = this.CheckList.filter(item => item.TestItemId == this
					.TestItemId);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].QualityResult = val;
				}
			},


			//保养项目日期点击初始下拉内容与显示下拉面板
			ShowactionDATE(TestItemId, DataTypeName) {
				this.TestItemId = TestItemId;
				//console.info('保养项目编码与内容', EquipmentMaintainId, DataTypeName);

				this.isShowDate2 = true; //显示日期选择
			},


			// //首检项目下拉点击初始下拉内容与显示下拉面板
			// ShowactionSheetLis(TestItemId, DataTypeName) {
			// 	this.TestItemId = TestItemId;
			// 	//console.info('保养项目编码与内容', TestItemId, DataTypeName);
			// 	this.TestItemList = [];
			// 	let ep_item = DataTypeName.split('/'); //字符分割
			// 	ep_item.forEach((item, index) => {
			// 		this.TestItemList.push({
			// 			text: item
			// 		})
			// 	});

			// 	this.IsTestItemList = true; //显示首检结果下拉框
			// },
			//保养项目下拉点击初始下拉内容与显示下拉面板
			ShowactionSheetLis(TestItemCoading, DataTypeName, Options) {
				this.TestItemCoading = TestItemCoading;
				console.info('检验项目编码与内容', TestItemCoading, DataTypeName, Options);
				this.TestItemList = Options; //[];      
				this.IsTestItemList = true; //显示检验项目下拉框
			},



			//显示下拉框
			showSel(val, item) {
				if (val == "process")
					this.showProcess = true;
				else if (val == "showTestItem") {
					this.showTestItem = true;
				}
			},


			//选择判定结果
			changeTestItem(val) {

				this.form.secondResultCode = val[0].value;
				this.form.secondResultNAME = val[0].label;
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
				this.form.TransferCode = value; //流转卡
				_self.setFocus("focus2");
				if (this.form.TransferCode != "") {
					_self.getSecondList();
				}
			},

			clear() {
				this.form.TransferCode = ""; //流转卡
			},


			//机台扫描事件
			searchQR1() {
				var self = this;

				if (this.form.TransferCode == "") {
					this.$refs.uToast.show({
						title: '请先扫描流转卡',
						type: 'warning',
						icon: true
					});
					return;
				}

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
				if (this.form.MachineCode != "") {
					_self.getSecondList1();
				}
			},

			clear1() {
				this.form.MachineCode = ""; //机台
			},



			//初始化页面列表
			getSecondList() {


				var data = {
					cardCode: this.form.TransferCode,
				}
				this.FirstInspectionCardScan(data).then(res => {
					this.pdList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.pdList = [];
						} else {

							this.form.ProductOrder = res.resultData.ProductOrder;
							this.form.ContainerNO = res.resultData.ContainerNO;
							this.form.MMXH = res.resultData.MMXH;
							this.form.Spec = res.resultData.Spec;
							this.getProcessListByResume();

						}
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},



			//查询流转履历中的工序
			getProcessListByResume() {
				this.form.ProcessCode = "";
				this.form.ProcessName = "";
				var data = {
					CardCode: this.form.TransferCode
				}
				this.GetprocessList(data).then(res => {

					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.ProcessCode = res.resultData;
						// this.searchTestMethodCoading();						
						this.getSecondList1();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},


			//查询页面列表
			getSecondList1() {
				this.CheckList = [];

				var data = {
					cardcode: this.form.TransferCode,
					containerNO: this.form.ContainerNO,
					machineCode: this.form.MachineCode,
					mmXH: this.form.MMXH,
					processcode: this.form.ProcessCode,
				}


				this.FirstInspectionConfirmMachieScan(data).then(res => {
					this.CheckList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.CheckList = [];
						} else {
							this.form.firstInspectionConfirmId = res.resultData.firstInspectionConfirmId;
							this.form.laboratoryTestStatus = res.resultData.laboratoryTestStatus;
							res.resultData.checkList.forEach((item, index) => {
								this.CheckList.push({
									"TestItemId": item.TestItemId,
									"TestItemCoading": item.TestItemCoading,
									"TestItemName": item.TestItemName,
									"TestItemStandard": item.TestItemStandard,
									"DataType": item.DataType,
									"DataTypeName": item.DataTypeName,
									"TestDepartment": item.TestDepartment,
									"QualityResult": "",
									"Options": item.Options //下拉数值								 							  
								})
							});

							res.resultData.checkList1.forEach((item, index) => {
								this.CheckListAll.push({
									"TestItemId": item.TestItemId,
									"TestItemCoading": item.TestItemCoading,
									"TestItemName": item.TestItemName,
									"TestItemStandard": item.TestItemStandard,
									"DataType": item.DataType,
									"DataTypeName": item.DataTypeName,
									"TestDepartment": item.TestDepartment,
									"Options": item.Options //下拉数值								 							  
								})
							});


						}
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},




			//初始化判定选择列表
			getsJList() {
				var data = {
					EnCode: "TestConclusion"
				}
				this.GetDictionary(data).then(res => {
					this.pdjgList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.pdjgList = [];
						} else {
							this.pdjgList = res.resultData;

						}
					} else {
						this.pdjgList = [{
							value: '',
							label: '无'
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


			save() {
				if (this.form.TransferCode == undefined) {
					this.$refs.uToast.show({
						title: '流转卡编码不能为空！',
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
								title: '请填写检验结果',
								type: 'warning',
								icon: true
							});

							return;
						}
					});
				}
				if (IsWarning) {
					this.$refs.uToast.show({
						title: '请填写检验结果',
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
					UrlStr = '有'
				} else {
					UrlStr = '无'
				}



				let data = {

					cardCode: this.form.TransferCode,
					MachineCode: this.form.MachineCode,
					firtResult: this.form.secondResultNAME,
					checkList: this.CheckList,
					checkList1: this.CheckListAll,
					Attachment: UrlStr,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					firstInspectionConfirmId: this.form.firstInspectionConfirmId,
					laboratoryTestStatus: this.form.laboratoryTestStatus,
					fileUrl: fileUrl,
				};






				console.log(JSON.stringify(data));
				this.FirstInspectionConfirmSave(data).then(res => {

					if (res.success) {
						this.$refs.uToast.show({
							title: '保存成功！',
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
				this.form = {
					CardCode: "", //流转卡编码
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					TransferBy: "", //流转方式
					TransferByName: "",
					MachineCode: "", //机台
					MachineName: "",
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					MMXH: "", //面膜型号
					Spec: "", //规格型号
					secondResultCode: "",
					secondResultNAME: "",
				}
				this.CheckList = [];
				this.fileList = [];
				this.$refs.uUpload.clear();
				uni.navigateBack()
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