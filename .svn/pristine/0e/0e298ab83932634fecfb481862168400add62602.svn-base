<template>
	<view class="WMS_FinishedPorductReturnManage">
		<view class="top">
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.fangongdan')" required>
				<u-input v-model="fangongdan" type="select" disabled="" @click="clickSelFun('process')" border
					:placeholder="$t('WMS_FinishedPorductReturnManage.fangongdan_placeholder')" />
			</u-form-item>
			<!-- <u-form-item label="返工信息" style="height: auto;">
                <view style="border: 0px solid Gainsboro;">
                    <view class="label">返工单号：{{form.fgd}}</view>
                    <view class="label">订单号：{{form.ddh}}</view>
                    <view class="label">柜号：{{form.gh}}</view>
                    <view class="label">PO号：{{form.PO}}</view>
                    <view class="label">总返工片数：{{form.zfps}}</view>
                    <view class="label">已返片数：{{form.yfps}}</view>
                    <view class="label">待返工片数：{{form.dfps}}</view>
                </view>
            </u-form-item> -->
			<u-table align="left" padding="5rpx 10rpx 5rpx 10rpx">
				<u-tr>
					<u-td>
						<view style="border: 0px solid Gainsboro;">
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.fgd')}}：{{form.fgd}}
							</view>
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.ddh')}}：{{form.ddh}}
							</view>
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.gh')}}：{{form.gh}}
							</view>
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.PO')}}：{{form.PO}}
							</view>
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.zfps')}}：{{form.zfps}}
							</view>
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.yfps')}}：{{form.yfps}}
							</view>
							<view class="label">
								{{$t('WMS_FinishedPorductReturnManage.dfps')}}：{{form.dfps}}
							</view>
						</view>
					</u-td>
				</u-tr>
			</u-table>
		</view>
		<u-form :model="form" ref="uForm">
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.PTeamCode')">
				<u-search v-model="form.PTeamCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('WMS_FinishedPorductReturnManage.PTeamCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.UserName')">
				<u-input v-model="form.UserName" type="text" class="readonly"
					:placeholder="$t('WMS_FinishedPorductReturnManage.UserName_placeholder')" disabled="" border />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.bgsl')">
				<u-input v-model="form.bgsl" type="number"
					:placeholder="$t('WMS_FinishedPorductReturnManage.bgsl_placeholder')" border :focus="focus2" />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.bls')">
				<u-input v-model="form.bls" type="number" disabled="" border class="readonly" />
			</u-form-item>
		</u-form>
		<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
			<u-divider halfWidth="100%">
				{{$t('WMS_FinishedPorductReturnManage.bgslTitle')}}
			</u-divider>
			<!-- <u-line color="blue" /> -->
		</view>
		<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;"> -->
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.SparePartsName')" prop="SparePartsCode">
				<u-input v-model="form3.SparePartsName" type="text"
					:placeholder="$t('WMS_FinishedPorductReturnManage.SparePartsName_placeholder')" border />
				<u-icon name="search" size="70upx" color="#138087" @click="clickSelFun('Result')"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishedPorductReturnManage.qty')">
				<u-input v-model="form3.qty" type="number" placeholder="" border :focus="focus3" />
				<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="addBadItem"></u-icon>
				<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
			</u-form-item>
		</u-form>
		<u-table style="margin-top: 10rpx;">
			<u-tr class="u-tr">
				<u-th>{{$t('WMS_FinishedPorductReturnManage.SparePartsName')}}</u-th>
				<u-th width="20%">{{$t('WMS_FinishedPorductReturnManage.qty')}}</u-th>
			</u-tr>
			<u-tr v-for="(item,index) of badItemDetailList" :key="index">
				<u-th>
					<u-checkbox v-model="item.Checked">
						<view class="u-line-1">{{item.BadItemName}}</view>
					</u-checkbox>
				</u-th>
				<u-th width="20%">{{item.BadQty}}</u-th>
			</u-tr>
		</u-table>
		<!-- </scroll-view> -->
		<view style="height: 205rpx;"></view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_FinishedPorductReturnManage.SaveBtn')}}</text>
			</u-button>
		</view>
		<view style="height: 15upx;"></view>
		<!-- 返工单号选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
		<!-- 责任部门(工序) -->
		<u-select v-model="showDetermination" @confirm="changeDetermination" :list="DeterminationList"></u-select>
		<!-- 不良项目 -->
		<u-select v-model="isShowResult" @confirm="changeCheckFun" :list="selectCheck"></u-select>
		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<!-- <homeBtn></homeBtn> -->
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
	var _self;
	export default {
		data() {
			return {
				action: global.FileHandler, //图片上传地址
				fileList: [], //文件上传列表
				fangongdan: "", //返工单号
				jyd: "", //OQC检验单号
				allchecked: false, //全选
				allname: "成品返工唛头清单", //全选名称
				focus1: false,
				focus2: false,
				focus3: false,
				form: {
					Id: "", //ID
					FactoryCode: this.$FactoryCode, //工厂编码
					fgd: "",
					ddh: "", //订单号
					gh: "", //柜号
					PO: "", //PO号
					zfps: "", //总返工片数
					yfps: "", //已返片数
					dfps: "", //待返工片数
					PTeamCode: "", //人员组别
					userinfo: "", //人员信息
					bgsl: 0, //报工数量
					bls: 0 //不良数量
				},
				form2: {
					BadItemCode: "",
					BadItemName: "",
					BadQty: "",
				},
				badItemDetailList: [], //不良信息

				form3: {
					qty: "",

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
				selectCheck: [], //不良项目数组

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
			_self = this;
			_self.setFocus("focus1");
			//当前登录的用户信息
			console.info('当前登录人信息', JSON.stringify(this.loginInfo));
			//初始化返工单号
			this.getProcessList();
			//不良项目选择初始化列表
			this.searchSpareParts2();
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.WMSModel/WMS_FinishedPorductReturnManage")
			});
		},
		onNavigationBarButtonTap: function(option) {
			//点击事件
			console.info('点击查询按钮事件', JSON.stringify(option));
			uni.navigateTo({
				url: '/pages/WMSModel/WMS_FinishProductReturnQuery'
			})
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('WMS', ['ProductReworkBGOrderQuery', 'ProductReworkBGPTeamScan', 'ProductReworkBGSave',
				'SaveQCTestItemForm', 'GetQCTestRecordList', 'GetQCTestResultRecordList', 'SavePollingDetailForm'
			]),
			...mapActions('common', ['GetDictionary', 'GetModelResourceExtendInfoByLevelCode', 'GetUserList',
				'GetBaseMaterialList', 'GetProcessModel', 'GetListByParentResource'
			]),
			//人员组别扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode(res.result);
					}
				});
			},
			//人员组别查询
			searchCardCode(value) {
				this.form.CardCode = value; //人员组别
				if (this.form.CardCode != "") {
					this.getCodes(value);
				}
			},
			clear() {
				this.form.CardCode = ""; //人员组别
			},
			custom(val) {
				console.info('', val);
			},
			//人员组别查询
			getCodes(val) {
				//人员组别查询获取信息
				console.log(val)
				if (val == "" || val == undefined) {
					return;
				}
				let queryJson = {
					"pTeamCode": val
				};
				console.info('人员组别查询参数', JSON.stringify(queryJson));

				//根据唛头码 校验唛头码  返回 唛头码所在的订单、柜号的相关信息
				this.ProductReworkBGPTeamScan(queryJson).then(res => {
					_self.setFocus("focus2");
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
			//初始化返工单号列表
			getProcessList() {
				this.fangongdan = "";
				this.processList = [];

				var data = {
					FactoryCode: this.$FactoryCode
				}
				this.ProductReworkBGOrderQuery(data).then(res => {
					this.processList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.processList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.processList.push({
									value: item.ReWorkOrder,
									label: item.ReWorkOrder
								});
							});
						}
					} else {
						this.processList = [];
					}
				});
			},
			//选择返工单
			changeProcess(val) {
				this.fangongdan = val[0].value; //val[0].label;				
				//this.fangongdan = val[0].label;
				//获取返工单号详细信息
				this.searchSpareParts();
				//检验方法选择列表
				//this.searchTestMethodCoading();
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
			//获取返工单信息
			searchSpareParts() {
				this.form.Id = ""; //ID
				this.form.FactoryCode = this.$FactoryCode; //工厂编码
				this.form.fgd = "";
				this.form.ddh = ""; //订单号
				this.form.gh = ""; //柜号
				this.form.PO = ""; //PO号
				this.form.zfps = ""; //总返工片数
				this.form.yfps = ""; //已返片数
				this.form.dfps = ""; //待返工片数   
				//this.form.CardCode = ""; //人员组别
				//this.form.userinfo = ""; //人员信息
				//this.form.bgsl = 0; //报工数量
				//this.form.bls = 0; //不良数量


				var data = {
					ParentResource: this.fangongdan //成品返工单号
				}

				this.ProductReworkBGOrderQuery(data).then(res => {
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
				} else if (val == "Result") {
					//初始化不良项目列表
					//this.searchSpareParts2();
					//选择显示 面板
					this.isShowResult = true;
				} else if (val == "ProductionMachine") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_2'),
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
							title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_2'),
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
							title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_3'),
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
			//不良项目下拉框选择事件回调
			changeCheckFun(val) {
				console.log(JSON.stringify(val))
				this.form3.SparePartsCode = val[0].value; //val[0].label;
				this.form3.SparePartsName = val[0].label;
				_self.setFocus("focus3");
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
				//不良项目选择初始化列表
				//this.searchSpareParts2();
			},
			//不良项目选择初始化列表
			searchSpareParts2() {
				this.selectCheck = [];

				//测试数据
				this.selectCheck.push({
					value: this.$t('WMS_FinishedPorductReturnManage.BasicData_1'),
					label: this.$t('WMS_FinishedPorductReturnManage.BasicData_1')
				}, {
					value: this.$t('WMS_FinishedPorductReturnManage.BasicData_2'),
					label: this.$t('WMS_FinishedPorductReturnManage.BasicData_2')
				});
				return; //测试结束
				let data = {
					FactoryCode: this.$FactoryCode //工厂编码
				}
				console.info('不良项目选择初始化列表获取数据提交参数', JSON.stringify(data));

				this.GetListByParentResource(data).then(res => {
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.selectCheck = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.selectCheck.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
						}
					} else {
						this.selectCheck = [{
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
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form2.ContainerNO) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_5'),
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
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_6'),
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

			//生产报工
			save() {
				if (!this.form.ProductOrder) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_7'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form.ContainerNO) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_8'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form.CardCode) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_9'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.PTeamCode) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_10'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					reworkOrder: this.fangongdan,

					qty: this.form.qty,
					badQty: this.form.bgsl,
					pTeamCode: this.form.PTeamCode,
					badItemDetailList: this.badItemDetailList,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				this.PackingBGSave(data).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_11'),
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
				this.form.ProductOrder = "";
				this.form.CardCode = "";
				this.form.CardName = "";
				this.form.ProductOrder = "";
				this.form.MachineCode = "";
				this.form.MaterialCode = "";
				this.form.ContainerNOorCardName = "";
				this.form.ProductOrderID = "";
				this.form.ContainerNO = "";
				this.form.Qty = "";
				this.form.BadQty = "";
				this.form2.BadItemCode = "";
				this.form2.BadItemName = "";
				this.form2.BadQty = "";
				this.badItemDetailList = []; //不良信息
			},


			//选择判定
			changeDetermination(val) {
				this.form.DeterminationCode = val[0].value;
				this.form.Determination = val[0].label;
			},
			//添加不良项目
			addBadItem() {
				if (!this.form3.SparePartsCode) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_12'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form3.SparePartsQty) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_13'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//检测是否存在重复数据
				let filterList = this.SparePartsItemDetailList.filter(item => item.SparePartsId == this.form3
					.SparePartsCode);
				console.info('重复数据', JSON.stringify(filterList));
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t('WMS_FinishedPorductReturnManage.MessageTips_14'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//备件数组
				this.SparePartsItemDetailList.push({
					Checked: false,
					RepairId: this.form.Id, //父任务主键ID
					SparePartsId: this.form3.SparePartsCode, //不良项目编码
					SparePartsName: this.form3.SparePartsName, //不良项目名称
					Num: this.form3.SparePartsQty, //数量
					EnabledMark: 1, //启用
					UseType: "", //使用类型
					Creator: this.loginInfo.result ? this.loginInfo.result.UserCode : "" //当前登录人编码
				});
				console.info('新增后备件数组', JSON.stringify(this.SparePartsItemDetailList));
				this.form3.SparePartsCode = this.$t('WMS_FinishedPorductReturnManage.BasicData_1');
				this.form3.SparePartsName = this.$t('WMS_FinishedPorductReturnManage.BasicData_1');
				this.form3.SparePartsQty = "1";
				let arr = this.SparePartsItemDetailList.map(item => {
					return item.Num;
				});
				this.form.bls = eval(arr.join("+"));
			},
			//删除备件
			deleteBadItem() {
				this.SparePartsItemDetailList = this.SparePartsItemDetailList.filter(item => {
					return item.Checked == false;
				});
				console.info('删除后备件数组', JSON.stringify(this.SparePartsItemDetailList));
				// let arr = this.SparePartsItemDetailList.map(item => {
				//     return item.SparePartsQty;
				// });
				// this.form.SparePartsQty = eval(arr.join("+"));
			},
			//返回页面
			goBack() {
				uni.switchTab({
					url: "/pages/index/index",
				});
			},
			initFocus() {
				this.focus1 = false;
				this.focus2 = false;
				this.focus3 = false;
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

<style lang="scss" scoped>
	.WMS_FinishedPorductReturnManage {
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

	.label {
		line-height: 20px;
		width: 268px;
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