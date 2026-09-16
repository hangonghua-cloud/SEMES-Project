<template>
	<view class="Quality_QC_FinishedPorductCheck">
		<view class="top">
			<u-form :model="form" ref="uForm" label-width="auto">
				<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.CardCode')" required>
					<!-- <u-input v-model="form.EquipmentId"  clearable type="text"  border placeholder="设备编码扫描" /> -->
					<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('Quality_QC_FinishedPorductCheck.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.ProductOrder')">
					<u-input v-model="form.ProductOrder" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.ContainerNO')">
					<u-input v-model="form.ContainerNO" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.Spec')">
					<u-input v-model="form.Spec" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>

				<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.TestMethodName')">
					<u-input v-model="form.TestMethodName" disabled type="text" placeholder="" border
						class="readonly" />
				</u-form-item>

				<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.Remark')"
					style="height: auto;margin-bottom: -5px;margin-top: 1px;">
					<u-input v-model="form.Remark" type="textarea" placeholder="" border :focus="focus2" />
				</u-form-item>
				<u-form-item :label="$t('common.photosUpload')" style="height: auto;">
					<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="40"
						:upload-text="$t('common.chooseTips')"></u-upload>
				</u-form-item>
			</u-form>
		</view>
		<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;"> -->
		<u-table style="margin-top: 20rpx;">
			<u-tr class="u-tr">
				<u-th>{{$t('Quality_QC_FinishedPorductCheck.TestItemName')}}</u-th>
				<u-th>{{$t('Quality_QC_FinishedPorductCheck.TestItemStandard')}}</u-th>
				<u-th>{{$t('Quality_QC_FinishedPorductCheck.CheckResult')}}</u-th>
				<u-th>{{$t('Quality_QC_FinishedPorductCheck.Distinguish')}}</u-th>
			</u-tr>
			<u-tr v-for="(item,index) of EP_EquipmentMaintainDetailList" :key="index">
				<u-th>{{item.TestItemName}}</u-th>
				<u-th>{{item.TestItemStandard}}</u-th>
				<u-th v-if="item.DataTypeName == '数值'">
					<u-input v-model="item.CheckResult" :placeholder="$t('common.Number_placeholder')" type="number"
						border />
				</u-th>
				<u-th v-else-if="item.DataTypeName == '文本'">
					<u-input v-model="item.CheckResult" :placeholder="$t('common.String_placeholder')" type="text"
						border />
				</u-th>
				<u-th v-else-if="item.DataTypeName == '日期'">
					<u-input v-model="item.CheckResult" :placeholder="$t('common.Date_placeholder')"
						@click="ShowactionDATE(item.TestItemCoading,item.DataTypeName)" type="select" border />
				</u-th>
				<u-th v-else>
					<!-- ="item.DataTypeName.index('/') > 0" -->
					<u-input v-model="item.CheckResult" :placeholder="$t('common.Select_placeholder')" type="select"
						@click="ShowactionSheetLis(item.TestItemCoading,item.DataTypeName,item.Options)" border />
					<u-action-sheet :list="actionSheetList" v-model="IsShowactionSheetList"
						@click="actionSheetCallback"></u-action-sheet>
				</u-th>
				<u-th>
					<u-input v-model="item.Distinguish" :placeholder="$t('common.Select_placeholder')" type="select"
						@click="ShowactionSheetLis2(item.TestItemCoading,item.DataTypeName,item.Options)" border />
					<u-action-sheet :list="actionSheetList2" v-model="IsShowactionSheetList2"
						@click="actionSheetCallback2"></u-action-sheet>
				</u-th>
			</u-tr>
		</u-table>
		<!-- </scroll-view> -->
		<view style="height: 205rpx;"></view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="SaveQCTestItemFormPost"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('Quality_QC_FinishedPorductCheck.SaveBtn')}}
			</u-button>
			<u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="search"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('Quality_QC_FinishedPorductCheck.SearchBtn')}}
			</u-button>
		</view>
		<view style="height: 15upx;"></view>
		<u-popup border-radius="10" v-model="show_shd" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable = true" :close-icon-pos="UpcloseIconPos">
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('Quality_QC_FinishedPorductCheck.SearchTitle')}}</u-divider>
				</view>
				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.ProductOrder')" required>
						<u-input v-model="form2.ProductOrder" type="text"
							:placeholder="$t('Quality_QC_FinishedPorductCheck.ProductOrder_placeholder')" border />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.ContainerNO')" required>
						<u-input v-model="form2.ContainerNO" type="text"
							:placeholder="$t('Quality_QC_FinishedPorductCheck.ContainerNO_placeholder')" border />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.date')" required>
						<view style="width: 100%;" @click="clickSelFun('date')">
							<u-input v-model="form2.date" type="select" border
								:placeholder="$t('Quality_QC_FinishedPorductCheck.date_placeholder')"
								style="pointer-events: none;" />
						</view>
					</u-form-item>
				</u-form>
				<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 620upx;" show-scrollbar="true"> -->
				<u-table style="margin-top: 20rpx;" align="left">
					<u-tr class="u-tr">
						<u-th width="12%" align="center">{{$t('common.CheckItem')}}</u-th>
						<u-th align="center">{{$t('common.ShowDetails')}}</u-th>
					</u-tr>
					<u-tr>
						<u-checkbox v-show="SparePartsItemDetailList.length>0" v-model="chkAll" @change="chkAllChange">
							<text class="u-font-14">{{$t('common.SelectAll')}}</text></u-checkbox>
					</u-tr>
					<u-tr v-for="(item,index) of SparePartsItemDetailList" :key="index">
						<u-th width="12%" align="center">
							<u-checkbox v-model="item.Checked">
								<!-- <view class="label u-line-1"></view> -->
							</u-checkbox>
						</u-th>
						<u-th align="left">
							<view class="u-text" @click="showQCTestResultRecordList(item)">
								{{$t('Quality_QC_FinishedPorductCheck.InspectNo')}}: {{item.InspectNo}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.PackTransferCode')}}:
								{{item.PackTransferCode}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.ProductOrder')}}: {{item.ProductOrder}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.ContainerNO')}}: {{item.ContainerNO}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.MaterialCode')}}: {{item.MaterialCode}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.CheckResult2')}}: {{item.CheckResult}}
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
					<u-button type="info" :ripple="true" ripple-bg-color="#dad0d5" style="width: 43%;margin-left: 4%;"
						@click="jiluQuery">{{$t('Quality_QC_FinishedPorductCheck.SearchBtn2')}}</u-button>
					<u-button type="primary" :ripple="true" ripple-bg-color="#138087"
						style="width: 43%;margin-left: 2%;"
						@click="ShowJYDDialog">{{$t('Quality_QC_FinishedPorductCheck.SaveBtn4')}}</u-button>
				</view>
			</view>
		</u-popup>
		<view style="height: 15upx;"></view>
		<u-popup border-radius="10" v-model="show_shd2" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable = true" :close-icon-pos="UpcloseIconPos">
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('Quality_QC_FinishedPorductCheck.InspectTitle')}}</u-divider>
				</view>
				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.PackTransferCode')">
						<u-input v-model="form3.PackTransferCode" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.ProductOrder')">
						<u-input v-model="form3.ProductOrder" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.ContainerNO')">
						<u-input v-model="form3.ContainerNO" type="text" disabled="" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.MaterialCode')">
						<u-input v-model="form3.MaterialCode" type="text" disabled="" />
					</u-form-item>
				</u-form>
				<u-table style="margin-top: 10rpx;" align="left">
					<!-- <u-tr class="u-tr">
                        <u-th>检验项目</u-th>
                        <u-th>检验标准</u-th>
                        <u-th>检验结果</u-th>
                    </u-tr> -->
					<u-tr v-for="(item,index) of SparePartsItemDetailList2" :key="index">
						<u-th>
							<view class="u-text">
								{{$t('Quality_QC_FinishedPorductCheck.TestItemName')}}: {{item.TestItemName}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.TestItemStandard')}}:
								{{item.TestItemStandard}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.TestDepartmentName')}}:
								{{item.TestDepartmentName}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.CheckResult')}}: {{item.CheckResult}}
								<br>{{$t('Quality_QC_FinishedPorductCheck.Distinguish')}}: {{item.Distinguish}}
							</view>
						</u-th>
					</u-tr>
				</u-table>
				<br>
				<br>
				<br>
				<br>
				<view class="" style="display: flex;">
					<u-button :type="'primary'" class='return'
						:custom-style="{width: '40%',height: '70rpx',borderRadius: '10rpx'}" @click="exit2"
						style="position: fixed;bottom: 30rpx;margin-left: 8%;">{{$t('Quality_QC_FinishedPorductCheck.CancelBtn')}}
					</u-button>
					<u-button :type="'primary'" :custom-style="{width: '40%',height: '70rpx',borderRadius: '10rpx'}"
						@click="ShowDeterminationDialog" style="position: fixed;bottom: 30rpx;margin-left: 52%;">
						{{$t('Quality_QC_FinishedPorductCheck.SaveBtn2')}}
					</u-button>
				</view>
			</view>
		</u-popup>
		<view style="height: 15upx;"></view>
		<u-popup border-radius="10" v-model="show_shd3" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('Quality_QC_FinishedPorductCheck.InspectionTtitle2')}}</u-divider>
				</view>
				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.Determination')" required>
						<u-input v-model="form4.Determination" type="text" disabled=""
							@click="clickSelFun('Determination')" border
							:placeholder="$t('Quality_QC_FinishedPorductCheck.Determination_placeholder')" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.Remark')"
						style="height: auto;margin-bottom: -5px;margin-top: 1px;">
						<u-input v-model="form4.Remark" type="textarea" placeholder="" border />
					</u-form-item>
				</u-form>
				<br>
				<br>
				<br>
				<br>
				<view class="" style="display: flex;">
					<u-button :type="'primary'" class='return'
						:custom-style="{width: '40%',height: '70rpx',borderRadius: '10rpx'}" @click="exit3"
						style="position: fixed;bottom: 30rpx;margin-left: 8%;">{{$t('Quality_QC_FinishedPorductCheck.CancelBtn')}}
					</u-button>
					<u-button :type="'primary'" :custom-style="{width: '40%',height: '70rpx',borderRadius: '10rpx'}"
						@click="SaveDetermination" style="position: fixed;bottom: 30rpx;margin-left: 52%;">
						{{$t('Quality_QC_FinishedPorductCheck.SaveBtn3')}}
					</u-button>
				</view>
			</view>
		</u-popup>
		<view style="height: 15upx;"></view>
		<u-popup border-radius="10" v-model="show_shd4" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable=true" :close-icon-pos="UpcloseIconPos">
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('Quality_QC_FinishedPorductCheck.InspectTitle')}}</u-divider>
				</view>
				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.InspectNo2')" required>
						<u-input v-model="form5.InspectNo" type="select" disabled="" @click="clickSelFun('jyd')" border
							:placeholder="$t('Quality_QC_FinishedPorductCheck.InspectNo2_placeholder')" />
					</u-form-item>
					<u-form-item :label="$t('Quality_QC_FinishedPorductCheck.Remark')"
						style="height: auto;margin-bottom: -5px;margin-top: 1px;">
						<u-input v-model="form5.Remark" type="textarea" placeholder="" border />
					</u-form-item>
				</u-form>
				<br>
				<br>
				<br>
				<br>
				<view class="" style="display: flex;">
					<u-button :type="'primary'" class='return'
						:custom-style="{width: '40%',height: '70rpx',borderRadius: '10rpx'}" @click="exit4"
						style="position: fixed;bottom: 30rpx;margin-left: 8%;">{{$t('Quality_QC_FinishedPorductCheck.CancelBtn')}}
					</u-button>
					<u-button :type="'primary'" :custom-style="{width: '40%',height: '70rpx',borderRadius: '10rpx'}"
						@click="SaveHPJyd" style="position: fixed;bottom: 30rpx;margin-left: 52%;">
						{{$t('Quality_QC_FinishedPorductCheck.SaveBtn3')}}
					</u-button>
				</view>
			</view>
		</u-popup>
		<!-- 检验单号选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
		<!-- 检验机台选择 -->
		<u-select v-model="isShowResult" @confirm="changeProductionMachine" :list="ProductionMachineList"></u-select>
		<!--  检验方法选择
        <u-select v-model="IsShowTestMethodCoading" @confirm="changeTestMethodCoading" :list="TestMethodCoadingList">
        </u-select> -->
		<!-- 日期范围选择 -->
		<u-calendar v-model="isShowDate" :mode="mode" @change="dateChange"></u-calendar>
		<!-- 检验项目单日期选择 -->
		<u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
		<!-- 工序选择 查询 -->
		<u-select v-model="showProcess2" @confirm="changeProcess2" :list="processList2"></u-select>
		<!-- 检验机台选择 -->
		<u-select v-model="isShowResult2" @confirm="changeProductionMachine2" :list="ProductionMachineList2"></u-select>
		<!-- 判定选择 -->
		<u-select v-model="showDetermination" @confirm="changeDetermination" :list="DeterminationList"></u-select>
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
					CustomerPO: "", //客户PO号
					MaterialCode: "", //客户型号
					CardCode: "", //唛头码
					PackTransferCode: "", //唛头码
					ContainerNO: "", //柜号
					Spec: "", //规格
					TestMethodCoading: "", //检验方法
					Remark: "", //备注
					Attachment: "" //附件名称
				},
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
				form3: {
					PackTransferCode: "", //唛头码
					ProductOrder: "", //订单号
					ContainerNO: "", //柜号
					MaterialCode: "" //客户型号
				}, //检验结果明细
				form4: {
					Id: "", //检查结果ID
					DeterminationCode: "1", //判定 合格1 不合格 2    
					Determination: this.$t('common.qualified'),
					Remark: "", //备注 
					ModfiyBy: "" //修改人
				},
				form5: {
					Id: "", //检查结果ID
					InspectNo: "", //检验单号
					jydlist: "", //检验单号数组 , 分隔
					Remark: "", //备注 
					UserCode: "" //修改人
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
				show_shd4: false, //成品检验单合批页面
				isShowDate2: false, //显示日期
				IsShowTestMethodCoading: false, //显示检验方法
				TestMethodCoadingList: [], //检验方法列表
				CheckItemList: [],

				Upmode: 'right', //显示弹窗从右出到左
				Upmask: true, // 是否显示遮罩
				Upcloseable: false, //是否显示弹窗关闭按钮
				UpcloseIconPos: 'top-right', //显示弹窗关闭按钮 显示位置
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
				actionSheetList2: [{
						text: this.$t('common.qualified')
					},
					{
						text: this.$t('common.unqualified')
					},
				], //检验项目 数据类型 下拉
				IsShowactionSheetList2: false, //是否显示保养任务项目选择下拉框
				TestItemCoading: '', //检验任务项目编码
				isShowUser: false,
				selectUser: [],
				isShowSpare: false,
				SpareList: [],
				radioResult: "",
				chkAll: false,
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
			//检验方法选择列表
			//this.searchTestMethodCoading();
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.QualityModel.QualityModel/Quality_QC_FinishedPorductCheck")
			});
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('Quality', ['GetOrderByTransferCode', 'GetOQCCheckConfigItem', 'SaveOQCCheckConfigForm',
				'GetOQCQualityCheckRecord', 'SaveOQCQualityChecInspectNo', 'GetOQCQualityCheckItemRecord',
				'SaveOQCQualityCheckResult'
			]),
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
			custom(val) {
				console.info('', val);
			},
			getCodes(val) {
				//巡检/过程检验根据流转卡获取信息
				console.log(val)

				let queryJson = {
					"PackTransferCode": val
				};
				console.info('唛头码查询参数', JSON.stringify(queryJson));
				this.GetOrderByTransferCode(queryJson).then(res => {
					if (res && res.success) {
						console.info('唛头码获取订单信息', JSON.stringify(res.resultData));
						this.form.PackTransferCode = res.resultData.PackTransferCode; //唛头码
						this.form.ProductOrder = res.resultData.ProductOrder; //订单号
						this.form.CardCode = res.resultData.PackTransferCode; //唛头码
						this.form.ContainerNO = res.resultData.ContainerNO; //柜号
						this.form.CustomerPO = res.resultData.CustomerPO; //客户PO号
						this.form.MaterialCode = res.resultData.MaterialCode; //客户型号
						this.form.Spec = res.resultData.Spec; //规格型号
						this.form.TestMethodCoading = res.resultData.OpetionList[0].label;
						this.form.TestMethodCoadingCode = res.resultData.OpetionList[0].value;
						// this.TestMethodCoadingList = res.resultData.OpetionList;                     
						res.resultData.OpetionList.forEach((item, index) => {
							this.form.TestMethodCoading = item.TestMethodCoading;
							this.form.TestMethodName = item.TestMethodName;
							// this.TestMethodCoadingList.push({
							//     value: item.TestMethodCoading,
							//     label: item.TestMethodName
							// });
						});


						console.info('检验方法数组', JSON.stringify(this.TestMethodCoadingList));

						this.searchGetEP_EquipmentMaintainDetailList();

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
							label: this.$t('common.None')
						}];
					}
				});
			},
			//选择检验单号
			changeProcess(val) {
				//this.form5.InspectNo = val[0].value; //val[0].label;				
				this.form5.InspectNo = val[0].label;
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
					filterList[0].CheckResult = this.actionSheetList[index].text;
				}
			},
			//保养项目下拉点击初始下拉内容与显示下拉面板
			ShowactionSheetLis2(TestItemCoading, DataTypeName, Options) {
				this.TestItemCoading = TestItemCoading;
				console.info('检验项目编码与内容', TestItemCoading, DataTypeName);
				//this.actionSheetList2 = Options; //[];
				// let ep_item = DataTypeName.split("/"); //字符分割
				// ep_item.forEach((item, index) => {
				//     this.actionSheetList.push({
				//         text: item
				//     })
				// });

				this.IsShowactionSheetList2 = true; //显示检验项目下拉框
			},
			//检验项目下拉回调事件
			actionSheetCallback2(index) {
				console.info('检验项目下拉回调内容', this.actionSheetList2[index].text);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.TestItemCoading == this
					.TestItemCoading);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].Distinguish = this.actionSheetList2[index].text;
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
					filterList[0].CheckResult = e.result;
				}
			},
			getDate(val) {
				console.info('检验项目日期回调内容', val);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.EquipmentMaintainId == this
					.EquipmentMaintainId);
				console.info('当前选择检验项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].CheckResult = val;
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
					FactoryCode: this.$FactoryCode //工厂
				}
				this.获取检验方法(data).then(res => {
					this.TestMethodCoadingList = [{
						value: '',
						label: this.$t('common.None')
					}];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.TestMethodCoadingList = [{
								value: '',
								label: this.$t('common.None')
							}];
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
				_self.setFocus("focus2");

				//根据物料小类,检验工序和检验方法 获取检测项目
				this.searchGetEP_EquipmentMaintainDetailList();
			},
			//根据物料小类,检验工序获取检测项目
			searchGetEP_EquipmentMaintainDetailList() {

				let postdata = {
					FactoryCode: this.$FactoryCode, //工厂
					Id: this.form.TestMethodCoading //检验方法
				}
				console.info('根据检验方法获取检验项目-参数', JSON.stringify(postdata));
				this.GetOQCCheckConfigItem(postdata).then(res => {
					this.EP_EquipmentMaintainDetailList = [];
					console.info('根据检验方法获取检验项目-返回结果', JSON.stringify(res));
					if (res && res.success) {
						res.resultData.forEach((item, index) => {
							this.EP_EquipmentMaintainDetailList.push({
								"TestMethodCoading": item.TestItemCoading, //检测方法
								"TestItemCoading": item.TestItemCoading, //检测项目编码
								"TestItemName": item.TestItemName, //检测项目名称
								"TestItemStandard": item.TestItemStandard, //检测标准
								"TestDepartment": item.TestDepartment, //检测部门
								"DataType": item.DataType, //数据类型
								"DataTypeName": item.DataTypeName, //数据类型名称
								"CheckResult": item.TestItemResult, //检测结果
								"Distinguish": item.Distinguish, //判别
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
			exit4() {
				this.show_shd4 = !this.show_shd4;
			},
			Upclose() {},
			//end 弹窗选择*******************
			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "jyd") {
					//检验单号
					this.showProcess = true;
				} else if (val == "ProductionMachine") {
					if (!this.form.CardCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_1'),
							type: 'warning',
							icon: true
						});
						return;
					}
					if (!this.form.ProcessCode) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_2'),
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
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_2'),
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
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_3'),
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
					//判定显示
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
			//获取T-3到当日
			GetTime() {
				let date = new Date();
				let base = Date.parse(date); // 转换为时间戳
				//console.info('时间戳',base,this.$u.timeFormat(base, 'yyyy-mm-dd'));
				let year = date.getFullYear(); //获取当前年份
				let mon = date.getMonth() + 1; //获取当前月份
				let day = date.getDate(); //获取当前日
				let oneDay = 24 * 3600 * 1000
				let daytime = `${year}${mon >= 10 ? mon : '0' + mon}${day >= 10 ? day : '0' + day}`; //今日时间
				let daytimeArr = []
				//T3天的日期
				let base3 = base - oneDay * 2;
				//console.info('时间戳',base3,this.$u.timeFormat(base3, 'yyyy-mm-dd'));
				let now = new Date(base3);
				let myear = now.getFullYear();
				let month = now.getMonth() + 1;
				let mday = now.getDate()
				daytimeArr.push([myear, month >= 10 ? month : '0' + month, mday >= 10 ? mday : '0' + mday].join(
					'-'))

				daytimeArr.push([year, mon >= 10 ? mon : '0' + mon, day >= 10 ? day : '0' + day].join(
					'-')); // 今日时间赋值给变量
				return daytimeArr
			},
			//记录查询
			search() {
				let GetTime_list = this.GetTime();
				//清空记录查询表单
				this.form2 = {
					FactoryCode: "", //工厂编码
					ProductOrder: "", //订单号                    
					ContainerNO: "", //柜号                   
					date: GetTime_list[0] + this.$t('common.To') + GetTime_list[1],
					StartDate: GetTime_list[0], //开始日期
					EndDate: GetTime_list[1], //结束日期
				};
				//清空检验结果
				this.SparePartsItemDetailList = [];
				//清空记录查询明细表单
				//this.form3 = {};
				//清空检验明细结果
				this.SparePartsItemDetailList2 = [];
				//隐藏保养执行弹窗
				this.show_shd = !this.show_shd;
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
				console.info('记录查询', JSON.stringify(this.form5));
				if (!this.form2.ProductOrder) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_5'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form2.ContainerNO) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_6'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form2.StartDate) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_7'),
						type: 'warning',
						icon: true
					});
					return;
				}

				this.SparePartsItemDetailList = [];
				let postData = {
					"queryJson": {
						//PackTransferCode: this.form.PackTransferCode,//唛头码
						ProductOrder: this.form2.ProductOrder, //订单号
						ContainerNO: this.form2.ContainerNO, //柜号  
						StartTime: this.form2.StartDate, //开始日期
						EndTime: this.form2.EndDate //结束日期
					},
				}
				console.info('检测查询提交参数', JSON.stringify(postData));
				this.GetOQCQualityCheckRecord(postData).then(res => {
					if (res && res.success) {
						//this.SparePartsItemDetailList = res.resultData;
						res.resultData.forEach((item, index) => {
							this.SparePartsItemDetailList.push({
								Checked: false,
								Id: item.Id,
								InspectNo: item.InspectNo, //检验单号
								PackTransferCode: item.PackTransferCode, //唛头号
								ProductOrder: item.ProductOrder, //订单号
								ContainerNO: item.ContainerNO, //柜号
								MaterialCode: item.MaterialCode, //客户型号
								Spec: item.Spec, //规格
								CheckStatus: item.CheckStatus, //检验状态
								CheckResult: item.CheckResult //检验 结论
							});
						});
						console.info('检测查询检测结果列表', JSON.stringify(res.resultData));
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
				this.SparePartsItemDetailList2 = []; //清空上次结果
				this.form3 = item;

				let postData = {
					"queryJson": {
						OQCQualityCheckId: item.Id //父ID
					},
				}
				console.info('成品检验项目记录提交参数', JSON.stringify(postData));
				this.GetOQCQualityCheckItemRecord(postData).then(res => {
					if (res && res.success) {
						this.SparePartsItemDetailList2 = res.resultData;

						console.info('成品检验项目记录结果列表', JSON.stringify(res.resultData));
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
				if (this.form.CardCode == "" || this.form.CardCode == undefined) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.TestMethodCoading) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_8'),
						type: 'warning',
						icon: true
					});
					return;
				}
				console.info('检验项目', JSON.stringify(this.EP_EquipmentMaintainDetailList));
				let IsTestItemResult = false;
				let IsTestItemResult2 = false;
				//检测保养项目是否都填写
				if (this.EP_EquipmentMaintainDetailList.length > 0) {
					this.EP_EquipmentMaintainDetailList.forEach((item, index) => {
						if (item.CheckResult == '') {
							IsTestItemResult = true;
							this.$refs.uToast.show({
								title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_9'),
								type: 'warning',
								icon: true
							});
							return;
						}
						if (item.TestItemResult2 == '') {
							IsTestItemResult2 = true;
							this.$refs.uToast.show({
								title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_11'),
								type: 'warning',
								icon: true
							});
							return;
						}
					});
				}
				if (IsTestItemResult) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_9'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (IsTestItemResult2) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_11'),
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
				// //上传地址转字符串并用管道符分隔 '|'
				// let files = [];
				// //通过filter，筛选出上传进度为100的文件(因为某些上传失败的文件，进度值不为100，这个是可选的操作)
				// files = this.$refs.uUpload.lists.filter(val => {
				//     return val.progress == 100;
				// })
				// //如果您不需要进行太多的处理，直接如下即可
				// //files = this.$refs.uUpload.lists;
				// files.forEach(function(item, index) {
				//     UrlStr += item.response.resultData + '|';
				// })
				// UrlStr = UrlStr.slice(0, UrlStr.length - 1);
				// console.info('上传地址', UrlStr);

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
					UrlStr = this.$t('common.Have')
				} else {
					UrlStr = this.$t('common.None')
				}

				//提交数据
				let posdata = {
					entity: {
						OQCCheckConfigId: this.form.TestMethodCoadingCode, //检测方法ID
						ProductOrder: this.form.ProductOrder, //订单
						PackTransferCode: this.form.PackTransferCode, //唛头码
						Remark: this.form.Remark, //备注
						Attachment: UrlStr, //附件地址
						Creator: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App' //检测人
					},
					data: this.EP_EquipmentMaintainDetailList,
					fileUrl: fileUrl,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				console.info('提交保存内容', JSON.stringify(posdata));

				this.SaveOQCCheckConfigForm(posdata).then(res => {
					console.log(JSON.stringify(res));
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_10'),
							type: 'success',
							icon: true
						});
						//显示检验单合并与最终判定界面
						let GetTime_list = this.GetTime();
						//清空记录查询表单
						this.form2 = {
							FactoryCode: "", //工厂编码
							ProductOrder: this.form.ProductOrder, //订单号                    
							ContainerNO: this.form
								.ContainerNO, //柜号                                               
							date: GetTime_list[0] + this.$t('common.To') + GetTime_list[1],
							StartDate: GetTime_list[0], //开始日期
							EndDate: GetTime_list[1], //结束日期
						};
						//清空检验结果
						this.SparePartsItemDetailList = [];
						//清空记录查询明细表单                        
						this.SparePartsItemDetailList2 = [];
						//显示记录查询弹窗
						this.show_shd = !this.show_shd;
						//查询
						this.jiluQuery();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			//显示质量最终判定对话框
			ShowDeterminationDialog() {
				this.form4.Id = this.form3.Id;
				console.info('form4', JSON.stringify(this.form4));
				this.show_shd3 = !this.show_shd3;
			},
			//质量最终判定
			SaveDetermination() {
				if (!this.form4.Determination) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_12'),
						type: 'warning',
						icon: true
					});
					return;
				}

				console.info('判定表单', JSON.stringify(this.form4));
				//当前登录的用户信息
				console.info('当前登录人信息', JSON.stringify(this.loginInfo));
				//提交数据
				let posdata = {
					Id: this.form4.Id, //巡检检验ID
					CheckResult: this.form4.Determination, //判定结果  Code                      
					Remark: this.form4.Remark, //备注
					UserCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'MesApp' //修改人
				};
				console.info('判定提交保存内容', JSON.stringify(posdata));

				//return; //测试 最终屏蔽
				this.SaveOQCQualityCheckResult(posdata).then(res => {
					console.log(JSON.stringify(res));
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_13'),
							type: 'success',
							icon: true
						});
						//清空
						this.form4 = {
							Id: "", //检查结果ID
							DeterminationCode: "1", //判定 合格1 不合格 2    
							Determination: this.$t('common.qualified'),
							Remark: "", //备注 
							UserCode: "" //修改人
						};
						console.info('this.form4', JSON.stringify(this.form4));
						//返回上一层页面
						this.show_shd3 = !this.show_shd3;
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
					this.SparePartsItemDetailList.forEach(item => {

						item.Checked = true;

					})
				} else {
					this.SparePartsItemDetailList.forEach(item => {
						item.Checked = false;
					})
				}
			},

			//选择判定
			changeDetermination(val) {
				this.form4.DeterminationCode = val[0].value;
				this.form4.Determination = val[0].label;
			},
			//显示检验单合批对话框
			ShowJYDDialog() {
				this.processList = [];
				//勾选检验单号数据
				let filterList = this.SparePartsItemDetailList.filter(item => item.Checked == true);
				if (filterList.length <= 1) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_14'),
						type: 'warning',
						icon: true
					});
					return;
				}
				console.info('勾选检验单号数据', JSON.stringify(filterList));
				//取所检验单号
				let arr = filterList.map(item => {
					return item.InspectNo;
				});
				console.info('取所检验单号', JSON.stringify(arr));
				this.form5.Id = filterList.map(item => {
					return item.Id;
				}).join(",");
				this.form5.jydlist = filterList.map(item => {
					return item.Id;
				}); //arr.join(",");
				// //临时
				// let tempArr = [];
				// filterList.forEach((item, index) => {
				//     tempArr.push(item.InspectNo);
				// });
				/* 多条勾选打印结算单会出现重复的结算单号 去重处理 */
				let getProtocalNum = new Set(arr);
				//console.info('去重', getProtocalNum);
				let getProtocalNumArr = Array.from(getProtocalNum);
				console.info('去重后转数组', JSON.stringify(getProtocalNumArr));
				getProtocalNumArr.forEach((item, index) => {
					this.processList.push({
						value: item, //.InspectNo
						label: item
					});
				});
				console.info('this.processList', JSON.stringify(this.processList));
				this.form5.InspectNo = arr[0];
				console.info('form5', JSON.stringify(this.form5));
				this.show_shd4 = !this.show_shd4;
			},
			//合批成品检验单号
			SaveHPJyd() {
				if (!this.form5.InspectNo) {
					this.$refs.uToast.show({
						title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_15'),
						type: 'warning',
						icon: true
					});
					return;
				}

				console.info('合批表单', JSON.stringify(this.form5));
				//当前登录的用户信息
				console.info('当前登录人信息', JSON.stringify(this.loginInfo));

				//提交数据
				let posdata = {
					data: this.form5.jydlist, //巡检检验ID
					InspectNo: this.form5.InspectNo, //合并后批次   
					Remark: this.form5.Remark, //备注
					UserCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'MesApp' //修改人
				};
				console.info('合批提交保存内容', JSON.stringify(posdata));

				this.SaveOQCQualityChecInspectNo(posdata).then(res => {
					console.log(JSON.stringify(res));
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('Quality_QC_FinishedPorductCheck.MessageTips_13'),
							type: 'success',
							icon: true
						});
						//清空
						this.form5 = {
							Id: "", //检查结果ID
							InspectNo: "", //检验单号
							jydlist: "", //检验单号数组 , 分隔
							Remark: "", //备注 
							UserCode: "" //修改人
						};
						console.info('this.form5', JSON.stringify(this.form5));
						//重新查询订单号 柜号 日期范围
						this.jiluQuery();
						//返回上一层页面
						this.show_shd4 = !this.show_shd4;
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
	.Quality_QC_FinishedPorductCheck {
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