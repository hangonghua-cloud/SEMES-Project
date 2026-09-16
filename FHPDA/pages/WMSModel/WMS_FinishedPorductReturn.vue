<template>
	<view class="WMS_FinishedPorductReturn">
		<view class="top">
			<u-form-item :label="$t('WMS_FinishedPorductReturn.MarkCode')" required>
				<u-search v-model="form.MarkCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('WMS_FinishedPorductReturn.MarkCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
		</view>
		<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
			<!-- <u-divider halfWidth="100%">记录查询</u-divider> -->
			<u-line color="blue" />
		</view>
		<u-form :model="form" ref="uForm">
			<u-form-item :label="$t('WMS_FinishedPorductReturn.ProductOrder')">
				<u-input v-model="form.ProductOrder" type="text" disabled="" :placeholder="$t('common.placeholder')" />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturn.ContainerNO')">
				<u-input v-model="form.ContainerNO" type="text" disabled="" :placeholder="$t('common.placeholder')" />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturn.CustomerPO')">
				<u-input v-model="form.CustomerPO" type="text" disabled="" :placeholder="$t('common.placeholder')" />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturn.TotalPalletQty')">
				<u-input v-model="form.TotalPalletQty" type="text" disabled=""
					:placeholder="$t('common.placeholder')" />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturn.TotalBoxQty')">
				<u-input v-model="form.TotalBoxQty" type="text" disabled="" :placeholder="$t('common.placeholder')" />
			</u-form-item>
		</u-form>
		<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
			<u-divider halfWidth="100%">
				<u-checkbox @change="checkboxChange" v-model="allchecked"
					:name="allname">{{$t('WMS_FinishedPorductReturn.checkboxChange')}}</u-checkbox>
			</u-divider>
			<!-- <u-line color="blue" /> -->
		</view>
		<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;"> -->
		<u-table style="margin-top: 20rpx;" align="left">
			<u-tr class="u-tr">
				<u-th width="12%" align="center">{{$t('common.CheckItem')}}</u-th>
				<u-th align="center">{{$t('WMS_FinishedPorductReturn.ShowDetails')}}</u-th>
			</u-tr>
			<u-tr v-for="(item,index) of SparePartsItemDetailList" :key="index">
				<u-th width="12%" align="center">
					<u-checkbox v-model="item.Checked">
						<!-- <view class="label u-line-1"></view> -->
					</u-checkbox>
				</u-th>
				<u-th
					align="left">唛头号:{{item.MarkCode}}<br>客户型号:{{item.MaterialCode}}<br>库存位置:{{item.LocationCode}}</u-th>
			</u-tr>
		</u-table>
		<view class="header">
			<u-form :model="form" ref="uForm">
				<u-form-item :label="$t('WMS_FinishedPorductReturn.ProcessName')" required>
					<view style="width: 100%;" @click="clickSelFun('process')">
						<u-input v-model="form.ProcessName" type="select" disabled="" border
							:placeholder="$t('WMS_FinishedPorductReturn.ProcessName_placeholder')"
							style="pointer-events: none;" />
					</view>
				</u-form-item>
			</u-form>
		</view>
		<!-- </scroll-view> -->
		<view style="height: 205rpx;"></view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="SaveQCTestItemFormPost" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_FinishedPorductReturn.SaveBtn')}}</text>
			</u-button>
		</view>
		<view style="height: 15upx;"></view>
		<!-- 工序选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
		<!-- 责任部门(工序) -->
		<u-select v-model="showDetermination" @confirm="changeDetermination" :list="DeterminationList"></u-select>
		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<homeBtn></homeBtn>
	</view>
</template>
<script>
	import {
		mapState,
		mapActions
	} from 'vuex'
	import {
		formatDate
	} from "@/utils/date.js"; //转换日期格式
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	import timePicker from '@/components/timePicker/timePicker.vue'
	import scanCode from '@/components/scanCode/scanCode.vue'
	import selectPicker from '@/components/select/select.vue'
	import global from '@/utils/global'
	export default {
		data() {
			return {
				action: global.FileHandler, //图片上传地址
				fileList: [], //文件上传列表
				CardCode: "", //唛头码
				//mtm: "", //唛头码
				jyd: "", //OQC检验单号
				allchecked: false, //全选
				focus1: true,
				allname: "成品返工唛头清单", //全选名称
				form: {
					Id: "", //流程转卡ID
					FactoryCode: this.$FactoryCode, //工厂编码
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					CustomerPO: "", //PO号
					TotalBoxQty: "", //总盒数
					TotalPalletQty: "", //总托数
					MaterialCode: "",
					MarkCode: "", //唛头码                    
					ProcessName: "", //工序:
					ProcessCode: "" //工序编码
				},
				//工序列表
				processList: [],
				//工序是否显示弹窗
				showProcess: false,
				//工序列表
				processList2: [],
				//工序是否显示弹窗
				showProcess2: false,
				ProductionMachineList: [], //检验机台列表
				ProductionMachineList2: [], //检验机台列表
				isShowResult: false, //检验机台 下拉显示面板
				isShowResult2: false, //检验机台 下拉显示面板
				DeterminationList: [{
						label: this.$t('common.qualified'),
						value: '1'
					},
					{
						label: this.$t('common.unqualified'),
						value: '2'
					}
				], //检验判定 1合格, 2 不合格
				showDetermination: false,
				list: 15,
				page: 0,
				isShowDate: false, //显示日期范围选择面板
				mode: 'range',
				gridList: [], //设备保养任务列表
				//isShowCheck: false, //设备类别 下拉显示面板               
				selectCheck: [], //设备类别数组
				SparePartsItemDetailList: [], //检测结果数组
				SparePartsItemDetailList2: [], //检测明细结果数组
				show_shd: false, //记录查询页面
				show_shd2: false, //记录查询明细页面
				show_shd3: false, //质量最终判定页面
				isShowDate2: false, //显示日期
				IsShowTestMethodCoading: false, //显示检验方法
				TestMethodCoadingList: [], //检验方法列表
				Upmode: 'right', //显示弹窗从右出到左
				Upmask: true, // 是否显示遮罩
				Upcloseable: false, //是否显示弹窗关闭按钮
				UpcloseIconPos: 'top-left', //显示弹窗关闭按钮 显示位置
				UserList: [],
				EP_EquipmentMaintainDetailList: [], //检测项目列表
				actionSheetList: [{
						text: this.$t('common.GenderMan')
					},
					{
						text: this.$t('common.GenderWoMan')
					},
				], //检验项目 数据类型 下拉
				IsShowactionSheetList: false, //是否显示保养任务项目选择下拉框
				TestItemCoading: '', //检验任务项目编码
				isShowUser: false,
				selectUser: [],
				isShowSpare: false,
				SpareList: [],
				radioResult: ""
			};
		},
		filters: {

		},
		components: {
			timePicker,
			scanCode,
			selectPicker
		},
		mixins: [commonMixin],
		onLoad: function(option) {
			//当前登录的用户信息
			console.info('当前登录人信息', JSON.stringify(this.loginInfo));
			console.info('传参数据.mtm', option.mtm);
			this.CardCode = option.mtm; //唛头码
			this.form.CardCode = this.CardCode; //唛头码
			this.jyd = option.jyd; //OQC检验单号
			if (this.CardCode != "") {
				console.info('this.CardCode', this.CardCode);
				this.getCodes(this.CardCode);
			}
			//初始化工序列表
			this.getProcessList();
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.QualityModel.WMSModel/WMS_FinishedPorductReturn")
			});
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('WMS', ['ProductReWorkMarkScan', ]),
			...mapActions('common', ['GetDictionary', 'GetModelResourceExtendInfoByLevelCode', 'GetUserList',
				'GetBaseMaterialList', 'GetProcessModel', 'GetListByParentResource'
			]),
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
			//唛头码查询
			searchCardCode(value) {
				this.CardCode = value; //唛头码
				this.form.CardCode = value; //唛头码
				if (this.CardCode != "") {
					this.getCodes(value);
				}
			},
			clear() {
				this.CardCode = ""; //唛头码
			},
			custom(val) {
				console.info('', val);
			},
			//唛头码查询
			getCodes(val) {
				//唛头码查询获取信息
				console.log(val)
				if (val == "" || val == undefined) {
					return;
				}
				let queryJson = {
					"markCode": val
				};
				// console.info('唛头码查询参数', JSON.stringify(queryJson));
				// this.form.ProductOrder = " 20-0100#";
				// this.form.ContainerNO= "175柜";
				// this.form.Po= "2956580";
				// this.form.TestProcessName="V097707033";
				// this.form.zhs= "1872";
				// this.form.zts= "26";

				//根据唛头码 校验唛头码  返回 唛头码所在的订单、柜号的相关信息
				this.ProductReWorkMarkScan(queryJson).then(res => {
					if (res && res.success) {
						console.info('唛头码获取信息', JSON.stringify(res.resultData));
						this.form = res.resultData;
						this.SparePartsItemDetailList = res.resultData.detail
						//工序
						//this.getProcessList();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//初始化工序列表
			getProcessList() {
				this.form.ProcessCode = "";
				this.form.ProcessName = "";
				this.processList = [{
					value: '',
					label: this.$t('common.None')
				}];
				var data = {
					FactoryCode: this.$FactoryCode
				}
				this.GetProcessModel(data).then(res => {
					this.processList = [{
						value: '',
						label: this.$t('common.None')
					}];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.processList = [{
								value: '',
								label: this.$t('common.None')
							}];
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
							label: this.$t('common.None')
						}];
					}
				});
			},
			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
				//初始化检验机台列表
				this.searchSpareParts();
				//检验方法选择列表
				this.searchTestMethodCoading();
			},
			// 选中某个复选框时，由checkbox时触发
			checkboxChange(e) {
				console.info('全选事件', JSON.stringify(e));
				console.info('全选事件-allchecked', this.allchecked);
				if (this.allchecked) {
					this.SparePartsItemDetailList.map(val => {
						val.Checked = true;
					})
				} else {
					this.SparePartsItemDetailList.map(val => {
						val.Checked = false;
					})
				}
			},
			//保养项目下拉点击初始下拉内容与显示下拉面板
			ShowactionSheetLis(TestItemCoading, DataTypeName, Options) {
				this.TestItemCoading = TestItemCoading;
				console.info('检验项目编码与内容', TestItemCoading, DataTypeName);
				this.actionSheetList = Options; //[];
				// let ep_item = DataTypeName.split("/"); //字符分割
				// ep_item.forEach((item, index) => {
				//     this.actionSheetList.push({
				//         text: item
				//     })
				// });

				this.IsShowactionSheetList = true; //显示检验项目下拉框
			},
			//检验项目下拉回调事件
			actionSheetCallback(index) {
				console.info('检验项目下拉回调内容', this.actionSheetList[index].text);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.TestItemCoading == this
					.TestItemCoading);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].TestItemResult = this.actionSheetList[index].text;
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
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.TestItemCoading == this
					.TestItemCoading);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].TestItemResult = e.result;
				}
			},
			getDate(val) {
				console.info('检验项目日期回调内容', val);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.EquipmentMaintainId == this
					.EquipmentMaintainId);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].TestItemResult = val;
				}
			},
			//选择保养人
			SearchUser() {
				this.isShowUser = true;

				var queryJson = {
					"UserCode": this.form2.RepairingPersonName
				};
				this.GetUserList(queryJson).then(res => {
					this.UserList = [];
					if (res && res.success) {
						res.resultData.forEach((item, index) => {
							this.UserList.push({
								value: item.Code,
								label: item.Name //item.Code + '-' + 
							});
						});
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})
			},
			//保养人选择回调事件
			changeUserFun(val) {
				this.form2.RepairingPerson = val[0].value;
				this.form2.RepairingPersonName = val[0].label;
			},
			//检验机台选择列表
			searchSpareParts() {
				this.form.ProductionMachineCode = "";
				this.form.ProductionMachine = "";
				var data = {
					ParentResource: this.form.ProcessCode //工序编码
				}
				this.GetListByParentResource(data).then(res => {
					this.ProductionMachineList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.ProductionMachineList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.ProductionMachineList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
						}
					} else {
						this.ProductionMachineList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//检验机台回调事件
			changeProductionMachine(val) {
				console.info('选择回调备件', JSON.stringify(val));
				this.form.ProductionMachineCode = val[0].value;
				this.form.ProductionMachine = val[0].label;
			},
			//检验方法选择列表
			searchTestMethodCoading() {
				this.form.TestMethodCoadingCode = "";
				this.form.TestMethodCoading = "";
				var data = {
					FactoryCode: this.form.FactoryCode, //工厂
					ProcessCode: this.form.ProcessCode, //工序编码
					SmallClass: this.form.SmallClass, //物料小类
					TestType: "1" //巡检:1 过程检验:2
				}
				this.GetQCTestMethodList(data).then(res => {
					this.TestMethodCoadingList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.TestMethodCoadingList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.TestMethodCoadingList.push({
									value: item.TestMethodCoading,
									label: item.TestMethodName
								});
							});
						}
					} else {
						this.TestMethodCoadingList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//检验方法回调事件
			changeTestMethodCoading(val) {
				console.info('选择回调备件', JSON.stringify(val));
				this.form.TestMethodCoadingCode = val[0].value;
				this.form.TestMethodCoading = val[0].label;
				//根据物料小类,检验工序和检验方法 获取检测项目
				this.searchGetEP_EquipmentMaintainDetailList();
			},
			//根据物料小类,检验工序获取检测项目
			searchGetEP_EquipmentMaintainDetailList() {
				let postdata = {
					FactoryCode: this.form.FactoryCode, //工厂
					SmallClass: this.form.SmallClass, //物料小类
					ProcessCode: this.form.ProcessCode, //工序
					TestMethodCoading: this.form.TestMethodCoadingCode, //检验方法
					TestType: "1" //巡检:1 过程检验:2
				}
				console.info('根据物料小类,检验工序获取检测项目-参数', JSON.stringify(postdata));
				this.GetQCTestItemList(postdata).then(res => {
					this.EP_EquipmentMaintainDetailList = [];
					console.info('根据物料小类,检验工序获取检测项目-返回结果', JSON.stringify(res));
					if (res && res.success) {
						res.resultData.forEach((item, index) => {
							this.EP_EquipmentMaintainDetailList.push({
								"TestMethodCoading": item.TestMethodCoading, //检测方法
								"TestItemCoading": item.TestItemCoading, //检测项目编码
								"TestItemName": item.TestItemName, //检测项目名称
								"TestItemStandard": item.TestItemStandard, //检测标准
								"TestDepartment": item.TestDepartment, //检测部门
								"DataType": item.DataType, //数据类型
								"DataTypeName": item.DataTypeName, //数据类型名称
								"TestItemResult": item.TestItemResult, //检测结果
								"Options": item.Options //下拉数值
							});
						});
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})
			},
			//返回按钮 关闭弹窗
			exit() {
				this.show_shd = !this.show_shd;
			},
			//返回按钮 关闭弹窗
			exit2() {
				this.show_shd2 = !this.show_shd2;
			},
			//返回按钮 关闭弹窗
			exit3() {
				this.show_shd3 = !this.show_shd3;
			},
			Upclose() {},
			//end 弹窗选择*******************
			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "process") {
					// if (!this.form.CardCode) {
					//     this.$refs.uToast.show({
					//         title: '请先扫描流转卡',
					//         type: 'warning',
					//         icon: true
					//     });
					//     return;
					// }
					//工序
					this.showProcess = true;
				} else if (val == "ProductionMachine") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturn.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturn.MessageTips_2'),
							type: 'warning',
							icon: true
						});
						return;
					}
					//检验机台  选择显示 面板
					this.isShowResult = true;
				} else if (val == "ProductionMachine2") {
					if (!this.form2.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturn.MessageTips_2'),
							type: 'warning',
							icon: true
						});
						return;
					}
					//检验机台  选择显示 面板
					this.isShowResult2 = true;
				} else if (val == "process2") {
					//初始化工序列表
					//工序
					this.showProcess2 = true;
				} else if (val == "TestMethodCoading") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturn.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturn.MessageTips_3'),
							type: 'warning',
							icon: true
						});
						return;
					}
					//初始化检验方法列表
					this.IsShowTestMethodCoading = true;
				} else if (val == "date") {
					this.isShowDate = true;
				} else if (val == "date2") {
					this.isShowDate2 = true;
				} else if (val == 'Determination') {
					//责任部门显示
					this.showDetermination = true;
				}
			},
			//设备下拉框选择事件回调
			changeCheckFun(val) {
				this.gridList = [];
				console.log(JSON.stringify(val))
				this.form.RepairingType = val[0].value; //val[0].label;
				this.form.RepairingTypeName = val[0].label;
			},

			//日期范围回调事件
			dateChange(e) {
				this.form2.date = e.startDate + this.$t('common.To') + e.endDate;
				this.form2.StartDate = e.startDate;
				this.form2.EndDate = e.endDate;
				console.log(e);
			},
			//记录查询
			search() {
				//清空记录查询表单
				this.form2 = {};
				//清空检验结果
				this.SparePartsItemDetailList = [];
				//清空记录查询明细表单
				this.form3 = {};
				//清空检验明细结果
				this.SparePartsItemDetailList2 = [];
				//隐藏保养执行弹窗
				this.show_shd = !this.show_shd;
				//初始化工序列表
				this.getProcessList2();
			},
			//初始化工序列表
			getProcessList2() {
				this.form2.ProcessCode = "";
				this.form2.ProcessName = "";
				var data = {
					FactoryCode: this.$FactoryCode
				}
				this.processList2 = [];
				this.GetProcessModel(data).then(res => {
					this.processList2 = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.processList2 = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.processList2.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							//检验机台清空
							this.form2.ProductionMachineCode = "";
							this.form2.ProductionMachine = "";
						}
					} else {
						this.processList2 = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//选择工序
			changeProcess2(val) {
				this.form2.ProcessCode = val[0].value;
				this.form2.ProcessName = val[0].label;
				//初始化检验机台列表
				this.searchSpareParts2();
			},
			//检验机台选择列表
			searchSpareParts2() {
				this.form2.ProductionMachineCode = "";
				this.form2.ProductionMachine = "";
				let data = {
					ParentResource: this.form2.ProcessCode //工序编码
				}
				console.info('记录查询-检验机台获取数据提交参数', JSON.stringify(data));
				this.ProductionMachineList2 = [];
				this.GetListByParentResource(data).then(res => {
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.ProductionMachineList2 = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.ProductionMachineList2.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
						}
					} else {
						this.ProductionMachineList2 = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//检验机台回调事件
			changeProductionMachine2(val) {
				console.info('选择回调备件', JSON.stringify(val));
				this.form2.ProductionMachineCode = val[0].value;
				this.form2.ProductionMachine = val[0].label;
			},
			//记录查询 执行
			jiluQuery() {
				if (!this.form2.ProductOrder) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturn.MessageTips_5'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form2.ContainerNO) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturn.MessageTips_6'),
						type: 'warning',
						icon: true
					});
					return;
				}

				// if (!this.form2.ProcessName) {
				//     this.$refs.uToast.show({
				//         title: '请选择工序！',
				//         type: 'warning',
				//         icon: true
				//     });
				//     return;
				// }

				if (!this.form2.StartDate) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturn.MessageTips_7'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let postData = {
					TestType: "1", //巡检:1 过程检验:2
					"queryJson": {
						ProductOrder: this.form2.ProductOrder, //订单号
						ContainerNO: this.form2.ContainerNO, //柜号 
						ProcessCode: this.form2.ProcessCode, //工序
						MachineCode: this.form2.ProductionMachineCode, //检验机台                    
						StartTime: this.form2.StartDate, //开始日期
						EndTime: this.form2.EndDate //结束日期
					},
				}
				console.info('记录查询提交参数', JSON.stringify(postData));
				this.GetQCTestRecordList(postData).then(res => {
					if (res && res.success) {
						this.SparePartsItemDetailList = res.resultData;
						console.info('记录查询检测结果列表', JSON.stringify(res.resultData));
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//查询检测明细结果
			showQCTestResultRecordList(item) {
				console.info('查询检测明细结果入参', JSON.stringify(item));
				this.form3 = item;
				let postData = {
					TestType: "1", //巡检:1 过程检验:2
					"queryJson": {
						ParentId: item.Id //父ID
					},
				}
				console.info('记录查询明细提交参数', JSON.stringify(postData));
				this.GetQCTestResultRecordList(postData).then(res => {
					if (res && res.success) {
						this.SparePartsItemDetailList2 = res.resultData;
						console.info('检测明细结果列表', JSON.stringify(res.resultData));
						this.show_shd2 = !this.show_shd2;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//创建OQC检验单记录
			SaveQCTestItemFormPost() {
				if (!this.CardCode) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturn.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.ProcessName) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturn.MessageTips_8'),
						type: 'warning',
						icon: true
					});
					return;
				}

				this.EP_EquipmentMaintainDetailList = [];
				//成品发货详情选择
				if (this.SparePartsItemDetailList.length > 0) {
					this.SparePartsItemDetailList.forEach((item, index) => {
						if (item.Checked) {
							this.EP_EquipmentMaintainDetailList.push(item)
						}
					});
				}
				if (this.EP_EquipmentMaintainDetailList.length == 0) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturn.MessageTips_9'),
						type: 'warning',
						icon: true
					});
					return;
				}
				console.info('任务表单', JSON.stringify(this.form));
				console.info('检验项目', JSON.stringify(this.EP_EquipmentMaintainDetailList));
				//当前登录的用户信息
				console.info('当前登录人信息', JSON.stringify(this.loginInfo));
				this.$refs.uToast.show({
					title: this.$t('WMS_FinishedPorductReturn.MessageTips_10'),
					type: 'success',
					icon: true
				});
				//清空
				this.CardCode = "";
				this.form = {
					Id: "", //流程转卡ID
					FactoryCode: "", //工厂编码
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					Po: "", //PO号
					zhs: "", //总盒数
					zts: "", //总托数
					CardCode: "", //唛头码                    
					ProcessName: "", //工序:
					ProcessCode: "" //工序编码
				};
				console.info('this.form', JSON.stringify(this.form));
				this.SparePartsItemDetailList = [];
				console.info('this.SparePartsItemDetailList', JSON.stringify(this
					.SparePartsItemDetailList));
				this.EP_EquipmentMaintainDetailList = []; //检验项目 TABLE  
				console.info('this.EP_EquipmentMaintainDetailList', JSON.stringify(this
					.EP_EquipmentMaintainDetailList));
				//全选清空
				this.allchecked = false;
				return;
				//提交数据
				let posdata = {
					TestType: "1", //巡检:1  过程检验:2
					entity: {
						FlowCardId: this.form.CardCode, //流转卡
						CalibrationMethod: this.form.TestMethodCoadingCode, //检测方法
						TestProcess: this.form.ProcessCode, //检测工序 巡检:1
						ProductionMachine: this.form.ProductionMachineCode, //检测机台 巡检:1                        
						ProductionWorkshop: this.form.ProcessCode, //检测工序 过程检验:2
						TestMachine: this.form.ProductionMachineCode, //检测机台 过程检验:2 
						Remark: this.form.Remark, //备注
						Attachment: UrlStr, //附件地址
						Inspector: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App' //检测人
					},
					data: this.EP_EquipmentMaintainDetailList
				};
				console.info('提交保存内容', JSON.stringify(posdata));

				return;
				this.生成返工单提交(posdata).then(res => {
					console.log(JSON.stringify(res));
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturn.MessageTips_11'),
							type: 'success',
							icon: true
						});
						//清空
						//清空
						this.CardCode = "";
						this.form = {
							Id: "", //流程转卡ID
							FactoryCode: "", //工厂编码
							ProductOrder: "", //订单号
							ContainerNO: "", //柜号
							Po: "", //PO号
							zhs: "", //总盒数
							zts: "", //总托数
							CardCode: "", //唛头码                    
							ProcessName: "", //工序:
							ProcessCode: "" //工序编码
						};
						console.info('this.form', JSON.stringify(this.form));
						this.SparePartsItemDetailList = [];
						console.info('this.SparePartsItemDetailList', JSON.stringify(this
							.SparePartsItemDetailList));
						this.EP_EquipmentMaintainDetailList = []; //检验项目 TABLE  
						console.info('this.EP_EquipmentMaintainDetailList', JSON.stringify(this
							.EP_EquipmentMaintainDetailList));
						//全选清空
						this.allchecked = false;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//选择判定
			changeDetermination(val) {
				this.form.DeterminationCode = val[0].value;
				this.form.Determination = val[0].label;
			},
			//返回页面
			goBack() {
				uni.switchTab({
					url: "/pages/index/index",
				});
			}
		}
	}
</script>

<style lang="scss" scoped>
	.WMS_FinishedPorductReturn {
		padding: 20upx;
		//background: #f9f9f9;
		font-size: 32upx;
		height: 100vh;
	}

	.readonly {
		background-color: Gainsboro;
	}

	.u-form-item {
		height: auto;
	}

	.u-form {
		background: #fff;
		padding: 0 20upx 20upx 20upx;
	}

	.btn {
		display: flex;

		uni-button {
			width: 48%;
		}
	}

	.wrap {
		margin-top: 24rpx;
	}

	.item {
		display: flex;
		background: #fff;
		padding: 20upx;
		flex-direction: column;
		margin-bottom: 8upx !important;
		align-items: center;
		//box-shadow: 0px 3px 3px #7b7b7b;

		.left {
			width: 160upx;
		}

		.top {
			display: flex;
			width: 100%;
			padding: 0 20upx 20upx 0;
			align-items: center;
			border-bottom: 2px solid #138087;

			.name {
				font-size: 30upx;
				font-weight: 600;
			}
		}

		.bottom {
			width: 100%;

			.center {
				flex: 1;

				.deviedeItem {
					display: flex;
					font-size: 30upx;

					.nr {
						color: #999999;
					}

					view {
						padding: 10upx;
					}
				}
			}
		}
	}
</style>