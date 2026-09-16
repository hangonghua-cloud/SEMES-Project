<template>
	<view class="Quality_QC_IPQCDetail">
		<view class="top">
			<u-form :model="form" ref="uForm" label-width="auto">
				<u-form-item :label="$t('Quality_QC_IPQCDetail.CardCode')" required>
					<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('Quality_QC_IPQCDetail.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('Quality_QC_IPQCDetail.ProductOrder')">
					<u-input v-model="form.ProductOrder" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('Quality_QC_IPQCDetail.ContainerNO')">
					<u-input v-model="form.ContainerNO" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('Quality_QC_IPQCDetail.Spec')">
					<u-input v-model="form.Spec" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<!--  <u-form-item label="检验工序:" required>
                    <u-input v-model="form.ProcessName" type="select" @click="clickSelFun('process')" disabled="" border
                        placeholder="请选择检验工序" />
                </u-form-item> -->
				<u-form-item :label="$t('Quality_QC_IPQCDetail.ProductionMachine')" required>
					<u-search v-model="form.ProductionMachine" @custom="custom1" @search="searchCardCode1"
						@clear="clear1" :placeholder="$t('Quality_QC_IPQCDetail.ProductionMachine_placeholder')"
						shape="square" border :show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>

				<u-form-item :label="$t('Quality_QC_IPQCDetail.TestMethodName')">
					<u-input v-model="form.TestMethodName" disabled type="text" placeholder="" border
						class="readonly" />
				</u-form-item>

				<u-form-item :label="$t('Quality_QC_IPQCDetail.Remark')"
					style="height: auto;margin-bottom: -5px;margin-top: 1px;">
					<u-input v-model="form.Remark" type="textarea" placeholder="" border :focus="focus2" />
				</u-form-item>
				<u-form-item :label="$t('common.photosUpload')">
					<u-upload :upload-text="$t('common.chooseTips')" ref="uUpload" :action="action"
						:file-list="fileList" :max-count="20"></u-upload>
				</u-form-item>
			</u-form>
		</view>
		<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;"> -->
		<u-table style="margin-top: 20rpx;">
			<u-tr class="u-tr">
				<u-th>{{$t('Quality_QC_IPQCDetail.TestItemName')}}</u-th>
				<u-th>{{$t('Quality_QC_IPQCDetail.TestItemStandard')}}</u-th>
				<u-th width="36%">{{$t('Quality_QC_IPQCDetail.TestItemResult')}}</u-th>
			</u-tr>
			<u-tr v-for="(item,index) of EP_EquipmentMaintainDetailList" :key="index">
				<u-th>{{item.TestItemName}}</u-th>
				<u-th>{{item.TestItemStandard}}</u-th>
				<u-th width="36%" v-if="item.DataTypeName == '数值'">
					<u-input v-model="item.TestItemResult" :placeholder="$t('common.Number_placeholder')" type="number"
						border />
				</u-th>
				<u-th width="36%" v-else-if="item.DataTypeName == '文本'">
					<u-input v-model="item.TestItemResult" :placeholder="$t('common.String_placeholder')" type="text"
						border />
				</u-th>
				<u-th width="36%" v-else-if="item.DataTypeName == '日期'">
					<u-input v-model="item.TestItemResult" :placeholder="$t('common.Date_placeholder')"
						@click="ShowactionDATE(item.TestItemCoading,item.DataTypeName)" type="select" border />
				</u-th>
				<u-th width="36%" v-else>
					<!-- ="item.DataTypeName.index('/') > 0" -->
					<u-input v-model="item.TestItemResult" :placeholder="$t('common.Select_placeholder')" type="select"
						@click="ShowactionSheetLis(item.TestItemCoading,item.DataTypeName,item.Options)" border />
					<u-action-sheet :list="actionSheetList" v-model="IsShowactionSheetList"
						@click="actionSheetCallback"></u-action-sheet>
				</u-th>
			</u-tr>
		</u-table>
		<!-- </scroll-view> -->
		<view style="height: 205rpx;"></view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="SaveQCTestItemFormPost"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('Quality_QC_IPQCDetail.SaveBtn')}}
			</u-button>
			<u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="search"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('Quality_QC_IPQCDetail.SearchBtn')}}
			</u-button>
		</view>
		<!-- <view class="btn">
            <u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087" style="width: '40%';margin-left: auto;"
                @click="SaveQCTestItemForm">生成过程检验记录
            </u-button>
            <u-button :type="'success'" :ripple="true" ripple-bg-color="#138087" style="width: '40%';margin-left: auto;"
                @click="search">记录查询
            </u-button>
        </view> -->
		<view style="height: 15upx;"></view>
		<u-popup border-radius="10" v-model="show_shd" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<!-- <br>
            <br> -->
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('Quality_QC_IPQCDetail.SearchTitle')}}</u-divider>
				</view>
				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ProductOrder')" required>
						<u-input v-model="form2.ProductOrder" type="text" placeholder="请输入订单号" border focus="focus3" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ContainerNO')" required>
						<u-input v-model="form2.ContainerNO" type="text" placeholder="请输入柜号" border />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ProcessName')">
						<u-input v-model="form2.ProcessName" type="select" @click="clickSelFun('process2')" disabled=""
							border :placeholder="$t('Quality_QC_IPQCDetail.ProcessName_placeholder')" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ProductionMachine')">
						<u-input v-model="form2.ProductionMachine" type="select"
							@click="clickSelFun('ProductionMachine2')" disabled="" border
							:placeholder="$t('Quality_QC_IPQCDetail.ProductionMachine_placeholder2')" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.date')" required>
						<u-input v-model="form2.date" type="select" border @click="clickSelFun('date')"
							:placeholder="$t('Quality_QC_IPQCDetail.date_placeholder')" />
					</u-form-item>
				</u-form>
				<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 620upx;"> -->
				<u-table style="margin-top: 20rpx;">
					<u-tr class="u-tr">
						<u-th>{{$t('Quality_QC_IPQCDetail.ProductOrder')}}</u-th>
						<u-th width="10%">{{$t('Quality_QC_IPQCDetail.ContainerNO')}}</u-th>
						<u-th>{{$t('Quality_QC_IPQCDetail.ProductionWorkshopName')}}</u-th>
						<u-th>{{$t('Quality_QC_IPQCDetail.TestMachineName')}}</u-th>
						<u-th>{{$t('Quality_QC_IPQCDetail.InspectionTimeStr')}}</u-th>
					</u-tr>
					<u-tr v-for="(item,index) of SparePartsItemDetailList" :key="index">
						<u-th>
							<view class="u-text" @click="showQCTestResultRecordList(item)">
								{{item.ProductOrder}}
							</view>
						</u-th>
						<u-th width="10%">
							<view class="u-text" @click="showQCTestResultRecordList(item)">
								{{item.ContainerNO}}
							</view>
						</u-th>
						<u-th>
							<view class="u-text" @click="showQCTestResultRecordList(item)">
								{{item.ProductionWorkshopName}}
							</view>
						</u-th>
						<u-th>
							<view class="u-text" @click="showQCTestResultRecordList(item)">
								{{item.TestMachineName}}
							</view>
						</u-th>
						<u-th>
							<view class="u-text" @click="showQCTestResultRecordList(item)">
								{{item.InspectionTimeStr}}
							</view>
						</u-th>
					</u-tr>
				</u-table>
				<!-- </scroll-view> -->
				<br>
				<!-- <br>
                <br>
                <br> -->
				<view class="btn" style="margin-bottom: 20upx;">
					<u-button :ripple="true" ripple-bg-color="#138087" style="width: 43%;margin-left: 4%;"
						@click="exit">{{$t('Quality_QC_IPQCDetail.CancelBtn')}}</u-button>
					<u-button type="primary" :ripple="true" ripple-bg-color="#138087"
						style="width: 43%;margin-left: 2%;"
						@click="jiluQuery">{{$t('Quality_QC_IPQCDetail.SearchBtn2')}}</u-button>
				</view>
				<!-- <view class="" style="display: flex;">
                    <u-button :type="'primary'" class='return'
                        :custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}" @click="exit"
                        style="position: fixed;bottom: 30rpx;margin-left: 1%;">
                        <text>返回</text>
                    </u-button>
                    <u-button :type="'primary'" :custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}"
                        @click="jiluQuery" style="position: fixed;bottom: 30rpx;margin-left: 50%;">
                        <text>查询</text>
                    </u-button>
                </view> -->
			</view>
		</u-popup>
		<view style="height: 15upx;"></view>
		<u-popup border-radius="10" v-model="show_shd2" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable = true" :close-icon-pos="UpcloseIconPos">
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('Quality_QC_IPQCDetail.TestDetail')}}</u-divider>
				</view>
				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ProductOrder')">
						<u-input v-model="form3.ProductOrder" type="text" disabled="" :focus="focus3" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ContainerNO')">
						<u-input v-model="form3.ContainerNO" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.FlowCardId')">
						<u-input v-model="form3.FlowCardId" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.ProductionWorkshopName')">
						<u-input v-model="form3.ProductionWorkshopName" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.TestMachineName')">
						<u-input v-model="form3.TestMachineName" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.InspectionTimeStr')">
						<u-input v-model="form3.InspectionTimeStr" disabled="" type="text" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_IPQCDetail.Inspector')">
						<u-input v-model="form3.Inspector" disabled="" type="text" />
					</u-form-item>
				</u-form>
				<u-table style="margin-top: 20rpx;">
					<u-tr class="u-tr">
						<u-th>{{$t('Quality_QC_IPQCDetail.TestItemName')}}</u-th>
						<u-th>{{$t('Quality_QC_IPQCDetail.TestItemStandard')}}</u-th>
						<u-th>{{$t('Quality_QC_IPQCDetail.TestItemResult')}}</u-th>
					</u-tr>
					<u-tr v-for="(item,index) of SparePartsItemDetailList2" :key="index">
						<u-th>
							{{item.TestItemName}}
						</u-th>
						<u-th>
							{{item.TestItemStandard}}
						</u-th>
						<u-th>
							{{item.TestItemResult}}
						</u-th>
					</u-tr>
				</u-table>
				<br>
				<!--  <br>
                <br>
                <br> -->
				<view class="btn" style="margin-bottom: 20upx;">
					<u-button :ripple="true" ripple-bg-color="#138087" style="width: 90%;margin-left: 4%;"
						@click="exit2">{{$t('Quality_QC_IPQCDetail.CancelBtn')}}</u-button>
					<!-- <u-button :type="'primary'" class='return'
                        :custom-style="{width: '80%',height: '70rpx',borderRadius: '10rpx'}" @click="exit2"
                        style="position: fixed;bottom: 30rpx;margin-left: 10%;">
                        <text>返回</text>
                    </u-button> -->
					<!-- <u-button :type="'primary'" :custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}"
                        @click="jiluQuery" style="position: fixed;bottom: 30rpx;margin-left: 50%;">
                        <text>查询</text>
                    </u-button> -->
				</view>
			</view>
		</u-popup>
		<!-- 工序选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
		<!-- 检验机台选择 -->
		<u-select v-model="isShowResult" @confirm="changeProductionMachine" :list="ProductionMachineList"></u-select>
		<!-- 日期范围选择 -->
		<u-calendar v-model="isShowDate" :mode="mode" @change="dateChange"></u-calendar>
		<!-- 检验项目单日期选择 -->
		<u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
		<!-- 工序选择 查询 -->
		<u-select v-model="showProcess2" @confirm="changeProcess2" :list="processList2"></u-select>
		<!-- 检验机台选择 -->
		<u-select v-model="isShowResult2" @confirm="changeProductionMachine2" :list="ProductionMachineList2"></u-select>
		<!-- 检验方法选择 -->
		<u-select v-model="IsShowTestMethodCoading" @confirm="changeTestMethodCoading" :list="TestMethodCoadingList">
		</u-select>
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
				filePath: global.FilePath,
				fileList: [], //文件上传列表
				form: {
					Id: "", //流程转卡ID
					FactoryCode: "", //工厂编码
					ProductOrder: "", //订单号
					WorkOrder: "", //工单号
					ExeWorkOrder: "", //执行工单号
					CardCode: "", //流转卡
					CardName: "", //流转卡名称
					ContainerNO: "", //柜号
					Spec: "", //规格
					ProcessName: "", //工序:
					ProcessCode: "",
					ProductionMachine: "", //检验机台
					Remark: "", //备注
					Attachment: "", //附件名称
					TestMethodCoading: "", //检验方法
					TestMethodName: "",
					ProductType: "", // 1:正常 2：自制

				},
				MaterialGrouplist: [], //物料组

				form2: {
					FactoryCode: "", //工厂编码
					ProductOrder: "", //订单号                    
					ContainerNO: "", //柜号 
					ProcessName: "", //工序
					ProductionMachine: "", //检验机台
					date: "",
					StartDate: "", //开始日期
					EndDate: "", //结束日期
				},
				form3: {}, //检验结果明细
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
				isShowDate2: false, //显示日期
				IsShowTestMethodCoading: false, //显示检验方法
				TestMethodCoadingList: [], //检验方法列表
				Upmode: 'right', //显示弹窗从右出到左
				Upmask: true, // 是否显示遮罩
				Upcloseable: false, //是否显示弹窗关闭按钮
				UpcloseIconPos: 'top-right', //显示弹窗关闭按钮 显示位置top-left
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
				radioResult: "",
				//焦点
				focus1: false,
				focus2: false,
				focus3: false,
				focus4: false,
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
			//this.getProcessList();
			this.getLoginInfo();
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.QualityModel/Quality_QC_IPQCDetail")
			});
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('Quality', ['GetQCTransferCardEntity', 'GetQCTestItemList', 'GetQCTestMethodList',
				'SaveQCTestItemForm', 'GetQCTestRecordList', 'GetQCTestResultRecordList', 'SavePollingDetailForm'
			]),
			...mapActions('common', ['GetDictionary', 'GetModelResourceExtendInfoByLevelCode', 'GetUserList',
				'GetBaseMaterialList', 'GetProcessModel', 'GetListByParentResource', 'GetListByProductionMachine'
			]),
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
					this.getCodes(value);
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
						self.searchCardCode1(res.result);
					}
				});
			},

			//机台查询
			searchCardCode1(value) {
				this.form.ProductionMachine = value; //流转卡
				if (this.form.ProductionMachine) {
					this.getProductionMachine(value);
				}
			},

			clear1() {
				this.form.ProductionMachine = ""; //流转卡
			},

			custom(val) {
				console.info('', val);
			},

			custom1(val) {
				console.info('', val);
			},
			getCodes(val) {
				//巡检/过程检验根据流转卡获取信息
				debugger;
				let dd=this.filePath;
				let dc=global.FilePath;
				let ddd=global.REQUEST_URL;
				let dcs=global.FileHandler;
				console.log(val)
				let queryJson = {
					"CardCode": val
				};
				console.info('流转卡查询参数', JSON.stringify(queryJson));
				this.GetQCTransferCardEntity(queryJson).then(res => {
					if (res && res.success) {
						console.info('流转卡获取信息', JSON.stringify(res.resultData));
						this.form.FactoryCode = res.resultData[0].FactoryCode;
						this.form.ProductOrder = res.resultData[0].ProductOrder;
						this.form.WorkOrder = res.resultData[0].WorkOrder;
						this.form.ExeWorkOrder = res.resultData[0].ExeWorkOrder;
						this.form.CardCode = res.resultData[0].CardCode;
						this.form.CardName = res.resultData[0].CardName;
						this.form.ContainerNO = res.resultData[0].ContainerNO;
						this.form.Spec = res.resultData[0].Spec;
						this.MaterialGrouplist = res.resultData[0].MaterialGrouplist;
						this.form.ProductType = res.resultData[0].ProductType;
						//工序
						// this.getProcessList();

						this.searchTestMethodCoading();

					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},

			//机台查询项目列表
			getProductionMachine(val) {
				if (!val)
					return;

				console.log(val)
				let queryJson = {
					"ProductionMachine": val
				};
				console.info('流转卡查询参数', JSON.stringify(queryJson));
				this.GetListByProductionMachine(queryJson).then(res => {
					if (res && res.success) {
						console.info('流转卡获取信息', JSON.stringify(res.resultData));

						if (res.resultData.length > 0) {
							res.resultData.forEach((item, index) => {

								this.form.ProcessName = item.ResourceName;
								this.form.ProcessCode = item.ResourceCode;
							});
							this.searchTestMethodCoading();
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

			//初始化工序列表
			getProcessList() {
				this.form.ProcessCode = "";
				this.form.ProcessName = "";
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
				//初始化检验机台列表
				this.searchSpareParts();
				//检验方法选择列表
				this.searchTestMethodCoading();
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
			//start 弹窗选择*****************            
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
							label: this.$t("common.None")
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
				if (!this.form.ProcessCode || this.MaterialGrouplist.length == 0) {
					return;
				}
				this.form.TestMethodName = "";
				this.form.TestMethodCoading = "";
				var data = {
					FactoryCode: this.form.FactoryCode, //工厂
					ProcessCode: this.form.ProcessCode, //工序编码
					MaterialGrouplist: this.MaterialGrouplist, //物料组
					TestType: "2" //巡检:1 过程检验:2
				}
				console.info('检验方法查询参数', JSON.stringify(data));
				this.GetQCTestMethodList(data).then(res => {
					this.TestMethodCoadingList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.TestMethodCoadingList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.form.TestMethodCoading = item.TestMethodCoading;
								this.form.TestMethodName = item.TestMethodName;
								// this.TestMethodCoadingList.push({
								//     value: item.TestMethodCoading,
								//     label: item.TestMethodName
								// });
							});
							this.searchGetEP_EquipmentMaintainDetailList();

						}
					} else {
						this.TestMethodCoadingList = [{
							value: '',
							label: this.$t("common.None")
						}];
						this.$refs.uToast.show({
							title: res.returnMsg,
							type: 'warning',
							icon: true
						});
						return;
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
				_self.setFocus("focus2");
			},
			//根据物料组,检验工序获取检测项目
			searchGetEP_EquipmentMaintainDetailList() {
				let postdata = {
					FactoryCode: this.form.FactoryCode, //工厂
					MaterialGrouplist: this.MaterialGrouplist, //物料组
					ProcessCode: this.form.ProcessCode, //工序
					TestMethodCoading: this.form.TestMethodCoading, //检验方法
					TestType: "2" //巡检:1 过程检验:2
				}
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
			Upclose() {},
			//end 弹窗选择*******************
			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "process") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_IPQCDetail.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					//工序
					this.showProcess = true;
				} else if (val == "ProductionMachine") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_IPQCDetail.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_IPQCDetail.MessageTips_2'),
							type: 'warning',
							icon: true
						});
						return;
					}
					//检验机台  选择显示 面板
					this.isShowResult = true;
				} else if (val == "ProductionMachine2") {
					//检验机台  选择显示 面板
					this.isShowResult2 = true;
				} else if (val == "process2") {
					//初始化工序列表
					//this.getProcessList2();
					//工序
					this.showProcess2 = true;
				} else if (val == "TestMethodCoading") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_IPQCDetail.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_IPQCDetail.MessageTips_2'),
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
							//检验机台选择列表
							this.searchSpareParts2()
						}
					} else {
						this.processList2 = [{
							value: '',
							label: this.$t("common.None")
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
				var data = {
					ParentResource: this.form2.ProcessCode //工序编码
				}
				this.GetListByParentResource(data).then(res => {
					this.ProductionMachineList2 = [];
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
							label: this.$t("common.None")
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
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form2.ContainerNO) {
					this.$refs.uToast.show({
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_4"),
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
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_5"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let postData = {
					TestType: "2", //巡检:1 过程检验:2
					"queryJson": {
						ProductOrder: this.form2.ProductOrder, //订单号
						ContainerNO: this.form2.ContainerNO, //柜号 
						ProductionWorkshop: this.form2.ProcessCode, //工序
						TestMachine: this.form2.ProductionMachineCode, //检验机台                    
						StartTime: this.form2.StartDate, //开始日期
						EndTime: this.form2.EndDate //结束日期
					},
				}
				console.info('记录查询提交参数', JSON.stringify(postData));
				this.GetQCTestRecordList(postData).then(res => {
					if (res && res.success) {
						this.SparePartsItemDetailList = res.resultData;
						console.info('检测结果列表', JSON.stringify(res.resultData));
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
					TestType: "2", //巡检:1 过程检验:2
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
			//生成过程检验记录
			SaveQCTestItemFormPost() {

				if (!this.form.ProcessName) {
					this.$refs.uToast.show({
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_6"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.ProductionMachine) {
					this.$refs.uToast.show({
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_7"),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form.TestMethodCoading) {
					this.$refs.uToast.show({
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_8"),
						type: 'warning',
						icon: true
					});
					return;
				}
				let IsWarning = false; //是否显示提醒
				//检测保养项目是否都填写
				if (this.EP_EquipmentMaintainDetailList.length > 0) {
					this.EP_EquipmentMaintainDetailList.forEach((item, index) => {
						if (item.TestItemResult == '') {
							IsWarning = true; //是否显示提醒
							this.$refs.uToast.show({
								title: this.$t("Quality_QC_IPQCDetail.MessageTips_9"),
								type: 'warning',
								icon: true
							});
							return;
						}
					});
				}
				if (IsWarning) {
					this.$refs.uToast.show({
						title: this.$t("Quality_QC_IPQCDetail.MessageTips_9"),
						type: 'warning',
						icon: true
					});
					return;
				}
				console.info('任务表单', JSON.stringify(this.form));
				console.info('检验项目', JSON.stringify(this.EP_EquipmentMaintainDetailList));
				console.info('上传文件列表', JSON.stringify(this.$refs.uUpload.lists));
				//当前登录的用户信息
				console.info('当前登录人信息', JSON.stringify(this.loginInfo));
				let UrlStr = '';
				//通过filter，筛选出上传进度为100的文件(因为某些上传失败的文件，进度值不为100，这个是可选的操作)
				var files = this.$refs.uUpload.lists.filter(val => {
					return val.progress == 100;
				})

				var fileUrl = [];
				console.log(this.filePath);
				console.log(this.action);
				
				files.forEach(item => {
					debugger;
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

				//提交数据
				let posdata = {
					TestType: "2", //巡检:1  过程检验:2
					entity: {
						FlowCardId: this.form.CardCode, //流转卡
						CalibrationMethod: this.form.TestMethodCoading, //检测方法
						TestProcess: this.form.ProcessCode, //检测工序 巡检:1 
						ProductionMachine: this.form.ProductionMachine, //检测机台 巡检:2                        
						ProductionWorkshop: this.form.ProcessCode, //检测工序 过程检验:2
						TestMachine: this.form.ProductionMachine, //检测机台 过程检验:2 
						Attachment: UrlStr, //附件地址
						Remark: this.form.Remark, //备注
						ProductType: this.form.ProductType, //产品类型
						//附件地址
						Inspector: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App' //检测人
					},
					data: this.EP_EquipmentMaintainDetailList,
					fileUrl: fileUrl,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				console.info('提交保存内容', JSON.stringify(posdata));

				//return;
				this.SaveQCTestItemForm(posdata).then(res => {
					console.log(JSON.stringify(res));
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_IPQCDetail.MessageTips_10'),
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
			//返回页面
			goBack() {
				uni.switchTab({
					url: "/pages/index/index",
				});
			},
			//查询登录信息
			getLoginInfo() {
				let data = {
					userCode: this.loginInfo.result.UserCode,
				};
				this.GetLoginInfo(data).then(res => {
					if (res.success) {
						this.form.ProductionMachine = res.resultData.MachineCode; //机台编码
						this.getProductionMachine(this.form.ProductionMachine);
					}
				});
			},
			reset() {
				this.form.FactoryCode = "";
				this.form.ProductOrder = "";
				this.form.WorkOrder = "";
				this.form.ExeWorkOrder = "";
				this.form.CardCode = "";
				this.form.CardName = "";
				this.form.ContainerNO = "";
				this.form.Spec = "";
				this.MaterialGrouplist = [];
				this.form.ProductType = "";
				this.form.TestMethodName = "";
				this.form.TestMethodCoading = "";

				this.ProductionMachineList = []; //检验机台列表
				this.processList = []; //工序列表
				this.actionSheetList = []; //检验项目 数据类型 下拉  
				this.EP_EquipmentMaintainDetailList = []; //检验项目 TABLE  

				//清空内部上传文件列表
				this.$refs.uUpload.clear();
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
			}
		}
	}
</script>

<style lang="scss" scoped>
	.Quality_QC_IPQCDetail {
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