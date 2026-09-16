<template>
	<view class="EP_EquipmentMaintainTask">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('EP_EquipmentMaintainTask.FactoryName')" prop="FactoryName">
				<view style="width: 100%;" @click="clickSelFun('factory')">
					<u-input v-model="form.FactoryName" type="text" disabled
						:placeholder="$t('EP_EquipmentMaintainTask.FactoryName_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EP_EquipmentMaintainTask.date')">
				<view style="width: 100%;" @click="clickSelFun('date')">
					<u-input v-model="form.date" type="select" border
						:placeholder="$t('EP_EquipmentMaintainTask.date_placeholder')" style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EP_EquipmentMaintainTask.RepairingTypeName')">
				<view style="width: 100%;" @click="clickSelFun('Check')">
					<u-input v-model="form.RepairingTypeName" type="select" disabled="" border
						:placeholder="$t('EP_EquipmentMaintainTask.RepairingTypeName_placeholder')"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
		</u-form>
		<scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;">
			<view class="item" v-for="(item, index) in gridList" :key="index">

				<view class="top" @click="Check_Mater(item)">
					<u-icon name="coupon-fill" color="#138087" size="30"></u-icon>
					<view class="name">{{item.EquipmentName}}</view>
				</view>
				<view class="bottom">
					<view class="center" @click="Check_Mater(item)">
						<u-row span="10" @click="Check_Mater(item)">
							<u-col span="5">
								<view class="demo-layout bg-purple">
									<view class="name">{{$t('EP_EquipmentMaintainTask.EquipmentMaintainTaskName')}}:
									</view>
								</view>
							</u-col>
							<u-col span="5">
								<view class="demo-layout bg-purple-light ">
									<view class="nr">{{item.EquipmentMaintainTaskName}}</view>
								</view>
							</u-col>
						</u-row>
						<u-row span="10" @click="Check_Mater(item)">
							<u-col span="5">
								<view class="demo-layout bg-purple">
									<view class="name">{{$t('EP_EquipmentMaintainTask.PlanDate')}}:</view>
								</view>
							</u-col>
							<u-col span="5">
								<view class="demo-layout bg-purple-light ">
									<view class="nr">{{item.PlanDate}}</view>
								</view>
							</u-col>
						</u-row>
					</view>
				</view>

			</view>
		</scroll-view>
		<u-popup border-radius="10" v-model="show_shd" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<!-- <br>
            <br> -->
			<!-- 滚屏 -->
			<view class="header">
				<!-- <view class="title" style="border-left:4px solid #138087;margin-left: 10upx;margin-top: 10rpx;margin-bottom: 10rpx;">设备保养执行</view>
                <u-line color="#138087" style="margin-bottom: 10rpx;"/> -->
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('EP_EquipmentMaintainTask.TaskTitle')}}</u-divider>
				</view>
				<u-form :model="form2" ref="uForm">
					<u-form-item :label="$t('EP_EquipmentMaintainTask.EquipmentMaintainTaskId')">
						<u-input class="readonly" v-model="form2.EquipmentMaintainTaskId" type="text" disabled border
							placeholder="" />
					</u-form-item>
					<u-form-item :label="$t('EP_EquipmentMaintainTask.EquipmentName')">
						<u-input class="readonly" v-model="form2.EquipmentName" type="text" disabled border
							placeholder="" />
					</u-form-item>
					<u-form-item :label="$t('EP_EquipmentMaintainTask.EquipmentMaintainTaskName')">
						<u-input class="readonly" v-model="form2.EquipmentMaintainTaskName" type="text" disabled border
							placeholder="" />
					</u-form-item>
					<u-form-item :label="$t('EP_EquipmentMaintainTask.RepairingPersonName')" required>
						<u-input v-model="form2.RepairingPersonName"
							:placeholder="$t('EP_EquipmentMaintainTask.RepairingPersonName_placeholder')" type="text"
							border />
						<u-icon name="search" size="70upx" color="#138087" @click="SearchUser"></u-icon>
						<u-select v-model="isShowUser" @confirm="changeUserFun" :list="UserList"></u-select>
					</u-form-item>
				</u-form>
				<scroll-view scroll-y="true" class="scroll-Y" style="height: 750upx;">
					<u-table style="margin-top: 20rpx;">
						<u-tr class="u-tr">
							<u-th>{{$t('EP_EquipmentMaintainTask.EquipmentMaintainName')}}</u-th>
							<u-th>{{$t('EP_EquipmentMaintainTask.EquipmentMaintainStandard')}}</u-th>
							<u-th width="36%">{{$t('EP_EquipmentMaintainTask.EquipmentMaintainResult')}}</u-th>
						</u-tr>
						<u-tr v-for="(item,index) of EP_EquipmentMaintainDetailList" :key="index">
							<u-th>{{item.EquipmentMaintainName}}</u-th>
							<u-th>{{item.EquipmentMaintainStandard}}</u-th>
							<u-th width="36%" v-if="item.DataTypeName == '数值'">
								<u-input v-model="item.EquipmentMaintainResult"
									:placeholder="$t('common.Number_placeholder')" type="number" border />
							</u-th>
							<u-th width="36%" v-else-if="item.DataTypeName == '文本'">
								<u-input v-model="item.EquipmentMaintainResult"
									:placeholder="$t('common.String_placeholder')" type="text" border />
							</u-th>
							<u-th width="36%" v-else-if="item.DataTypeName == '日期'">
								<u-input v-model="item.EquipmentMaintainResult"
									:placeholder="$t('common.Date_placeholder')"
									@click="ShowactionDATE(item.EquipmentMaintainId,item.DataTypeName)" type="select"
									border />
							</u-th>
							<u-th width="36%" v-else>
								<!-- ="item.DataTypeName.index('/') > 0" -->
								<u-input v-model="item.EquipmentMaintainResult"
									:placeholder="$t('common.Select_placeholder')" type="select"
									@click="ShowactionSheetLis(item.EquipmentMaintainId,item.DataTypeName)" border />
								<u-action-sheet :list="actionSheetList" v-model="IsShowactionSheetList"
									@click="actionSheetCallback"></u-action-sheet>
							</u-th>
						</u-tr>
					</u-table>
					<view style="margin-top:10rpx;margin-bottom: 10rpx;">
						<u-divider halfWidth="100%">{{$t('EP_EquipmentMaintainTask.BackTitle')}}</u-divider>
					</view>
					<u-form :model="form3" ref="uForm" label-width="auto">
						<u-form-item :label="$t('EP_EquipmentMaintainTask.SparePartsName')" prop="SparePartsCode">
							<u-input v-model="form3.SparePartsName" type="text"
								:placeholder="$t('EP_EquipmentMaintainTask.SparePartsName_placeholder')" border />
							<u-icon name="search" size="70upx" color="#138087" @click="clickSelFun('Result')"></u-icon>
						</u-form-item>
						<u-form-item :label="$t('EP_EquipmentMaintainTask.SparePartsQty')">
							<u-input v-model="form3.SparePartsQty" type="number" placeholder="" border />
							<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="addBadItem"></u-icon>
							<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
						</u-form-item>
					</u-form>
					<u-table style="margin-top: 20rpx;">
						<u-tr class="u-tr">
							<u-th>{{$t('EP_EquipmentMaintainTask.SparePartsName')}}</u-th>
							<u-th>{{$t('EP_EquipmentMaintainTask.SpecificationsModels')}}</u-th>
							<u-th width="20%">{{$t('EP_EquipmentMaintainTask.Num')}}</u-th>
						</u-tr>
						<u-tr v-for="(item,index) of SparePartsItemDetailList" :key="index">
							<u-th>
								<u-checkbox v-model="item.Checked">
									<view class="label u-line-1">{{item.SparePartsName}}</view>
								</u-checkbox>
							</u-th>
							<u-th>{{item.SpecificationsModels}}</u-th>
							<u-th width="20%">{{item.Num}}</u-th>
						</u-tr>
					</u-table>
				</scroll-view>
				<br>
				<!-- <br>
                <br>
                <br> -->
				<view class="btn" style="margin-bottom: 20upx;">
					<u-button type="primary" :ripple="true" ripple-bg-color="#138087"
						style="width: 43%;margin-left: 4%;"
						@click="save">{{$t('EP_EquipmentMaintainTask.SaveBtn')}}</u-button>
					<u-button :ripple="true" ripple-bg-color="#138087" style="width: 43%;margin-left: 2%;"
						@click="exit">{{$t('EP_EquipmentMaintainTask.CancelBtn')}}</u-button>
				</view>
				<!-- <view class="bottom" style="display: flex;">
                    <u-button :type="'primary'" class='return'
                        :custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}" @click="exit"
                        style="position: fixed;bottom: 30rpx;margin-left: 1%;">
                        <text>返回</text>
                    </u-button>
                    <u-button :type="'primary'" :custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}"
                        @click="save" style="position: fixed;bottom: 30rpx;margin-left: 50%;">
                        <text>保存</text>
                    </u-button>
                </view> -->
				<!-- 保养项目单日期选择 -->
				<u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
				<!-- 备件名称 -->
				<u-select v-model="isShowResult" @confirm="changeResultFun" :list="selectResult"></u-select>
			</view>
		</u-popup>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('EP_EquipmentMaintainTask.SearchBtn')}}</text>
			</u-button>
		</view>
		<view>
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<homeBtn></homeBtn>
		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"></u-select>
		<!-- 设备类别 -->
		<u-select v-model="isShowCheck" @confirm="changeCheckFun" :list="selectCheck"></u-select>
		<!-- 日期范围选择 -->
		<u-calendar v-model="isShowDate" :mode="mode" @change="dateChange"></u-calendar>
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
	var _self;
	export default {
		data() {
			return {
				form: {
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					date: "",
					StartDate: "", //开始日期
					EndDate: "", //结束日期
					RepairingType: "", //设备类别编码
					RepairingTypeName: "" //设备类别名称
				},
				form2: {
					Id: "",
					EquipmentId: "", //设备编码
					EquipmentName: "", //设备名称
					RepairingType: "", //设备类别  需要再次赋值
					EquipmentMaintainTaskName: "", //任务名称
					EquipmentMaintainTaskId: "", //任务编码
					RepairingPerson: "", //保养人编码
					RepairingPersonName: "" //保养人姓名
				},
				form3: {
					SparePartsCode: "",
					SparePartsName: "",
					SpecificationsModels: "",
					Unit: "",
					SmallClass: "",
					SparePartsQty: "1",
				},
				SparePartsItemDetailList: [], //备件信息
				list: 15,
				page: 0,
				isShowDate: false, //显示日期范围选择面板
				mode: 'range',
				gridList: [], //设备保养任务列表
				isShowCheck: false, //设备类别 下拉显示面板
				isShowResult: false, //备件名称 下拉显示面板
				selectCheck: [], //设备类别数组
				selectResult: [], //备件名称数组
				show_shd: false, //任务执行页面
				isShowDate2: false, //显示日期
				Upmode: 'right', //显示弹窗从右出到左
				Upmask: true, // 是否显示遮罩
				Upcloseable: false, //是否显示弹窗关闭按钮
				UpcloseIconPos: 'top-left', //显示弹窗关闭按钮 显示位置
				UserList: [],
				EP_EquipmentMaintainDetailList: [], //保养任务项目列表
				actionSheetList: [{
						text: this.$t('common.GenderMan')
					},
					{
						text: this.$t('common.GenderWoMan')
					},
				], //保养项目 数据类型 下拉
				IsShowactionSheetList: false, //是否显示保养任务项目选择下拉框
				EquipmentMaintainId: '', //保养任务项目编码
				isShowUser: false,
				selectUser: [],
				isShowSpare: false,
				SpareList: [],
				radioResult: "",
				factoryList: [], //工厂列表
				showFactory: false, //工厂弹窗
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
			_self.getFactoryList();
			//当前登录的用户信息
			console.info('当前登录人信息', JSON.stringify(this.loginInfo));
			//设备类别 初始选择数据
			this.GetDictionary({
				"EnCode": "EquipmentTypes"
			}).then(res => {
				if (res && res.success) {
					this.selectCheck = res.resultData;
				}
			})
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.EquipmentModel.EquipmentModel/EP_EquipmentMaintainTask")
			});
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('Equipment', ['GetEquipmentMaintainTaskByDateAndType', 'GetEP_EquipmentMaintainDetailList',
				'SaveEP_EquipmentMaintainTask'
			]),
			...mapActions('common', ['GetDictionary', 'GetModelResourceExtendInfoByLevelCode', 'GetUserList',
				'GetBaseMaterialList', 'GetResourceByLevelCode'
			]),
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
						}
					} else {
						this.factoryList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//保养项目下拉点击初始下拉内容与显示下拉面板
			ShowactionSheetLis(EquipmentMaintainId, DataTypeName) {
				this.EquipmentMaintainId = EquipmentMaintainId;
				console.info('保养项目编码与内容', EquipmentMaintainId, DataTypeName);
				this.actionSheetList = [];
				let ep_item = DataTypeName.split("/"); //字符分割
				ep_item.forEach((item, index) => {
					this.actionSheetList.push({
						text: item
					})
				});

				this.IsShowactionSheetList = true; //显示保养项目下拉框
			},
			//保养项目下拉回调事件
			actionSheetCallback(index) {
				console.info('保养项目下拉回调内容', this.actionSheetList[index].text);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.EquipmentMaintainId == this
					.EquipmentMaintainId);
				console.info('当前选择保养项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].EquipmentMaintainResult = this.actionSheetList[index].text;
				}
			},
			//保养项目日期点击初始下拉内容与显示下拉面板
			ShowactionDATE(EquipmentMaintainId, DataTypeName) {
				this.EquipmentMaintainId = EquipmentMaintainId;
				console.info('保养项目编码与内容', EquipmentMaintainId, DataTypeName);

				this.isShowDate2 = true; //显示日期选择
			},
			//保养项目日期点击初始下拉内容与显示下拉面板  回调
			dateChange2(e) {
				console.info('保养项目日期回调内容', e.result);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.EquipmentMaintainId == this
					.EquipmentMaintainId);
				console.info('当前选择保养项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].EquipmentMaintainResult = e.result;
				}
			},
			getDate(val) {
				console.info('保养项目日期回调内容', val);
				let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.EquipmentMaintainId == this
					.EquipmentMaintainId);
				console.info('当前选择保养项目', JSON.stringify(filterList));
				if (filterList.length > 0) {
					filterList[0].EquipmentMaintainResult = val;
				}
			},
			//start 弹窗选择*****************
			Check_Mater(item) {
				console.log(JSON.stringify(item));
				//this.code = val
				this.show_shd = !this.show_shd;
				this.form2 = item;
				if (this.form.RepairingType != "") {
					this.form2.RepairingType = this.form.RepairingType; //设备类别编码
				}
				//根据保养任务编码查询保养项目
				this.searchGetEP_EquipmentMaintainDetailList();
			},
			//选择保养人
			SearchUser() {
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
						this.isShowUser = true;
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
			//备件选择列表
			searchSpareParts() {
				//this.isShowSpare = true;               
				let postdata = {
					queryJson: {
						"Material": this.form3.SparePartsName,
						"MaterialClassName": "'DQYP','BPBJ'" //电气用品、备品备件
					}
				}
				console.info('备件查询条件', JSON.stringify(postdata));
				this.GetBaseMaterialList(postdata).then(res => {
					this.selectResult = [];
					if (res && res.success) {
						console.info('查询备件返回数组', JSON.stringify(res.resultData));
						res.resultData.forEach((item, index) => {
							this.selectResult.push({
								value: item.MaterialCode,
								label: item.MaterialName,
								SpecificationsModels: item.Spec,
								Unit: item.Unit,
								SmallClass: item.SmallClass,
								Checked: false,
							});
						});
						//备件  选择显示 面板
						this.isShowResult = true;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})
			},
			//根据保养任务编码查询保养项目
			searchGetEP_EquipmentMaintainDetailList() {
				let postdata = {
					queryJson: {
						"EquipmentTaskId": this.form2.EquipmentMaintainTaskId //任务编码
					}
				}
				console.info('根据保养任务编码查询保养项目-参数', JSON.stringify(postdata));
				this.GetEP_EquipmentMaintainDetailList(postdata).then(res => {
					this.EP_EquipmentMaintainDetailList = [];
					console.info('根据保养任务编码查询保养项目-返回结果', JSON.stringify(res));
					if (res && res.success) {
						//this.EP_EquipmentMaintainDetailList = res.resultData;
						res.resultData.forEach((item, index) => {
							this.EP_EquipmentMaintainDetailList.push({
								"Id": "", //item.Id,
								"ParentId": this.form2.Id, //父任务主键ID
								"EquipmentTaskId": item.EquipmentTaskId,
								"EquipmentMaintainId": item.EquipmentMaintainId,
								"EquipmentMaintainName": item.EquipmentMaintainName,
								"EquipmentMaintainStandard": item.EquipmentMaintainStandard,
								"DataType": item.DataType,
								"Creator": item.Creator,
								"CreateTime": item.CreateTime,
								"DataTypeName": item.DataTypeName,
								"EquipmentMaintainResult": item.EquipmentMaintainResult
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

			Upclose() {},
			//end 弹窗选择*******************
			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "factory")
					this.showFactory = true;
				else if (val == "Check") {
					//设备类别 选择显示
					this.isShowCheck = true;
				} else if (val == "Result") {
					//初始化备件列表
					this.searchSpareParts();
					// //备件  选择显示 面板
					// this.isShowResult = true;
				} else if (val == "date") {
					this.isShowDate = true;
				} else if (val == "date2") {
					this.isShowDate2 = true;
				}
			},
			//选择工厂
			changeFactory(val) {
				this.form.FactoryCode = val[0].value; //val[0].label;				
				this.form.FactoryName = val[0].label;
			},
			//设备下拉框选择事件回调
			changeCheckFun(val) {
				this.gridList = [];
				console.log(JSON.stringify(val))
				this.form.RepairingType = val[0].value; //val[0].label;
				this.form.RepairingTypeName = val[0].label;
			},
			//备件回调事件
			changeResultFun(val) {
				console.info('选择回调备件', JSON.stringify(val));
				this.form3.SparePartsCode = val[0].value; //MaterialCode
				this.form3.SparePartsName = val[0].label; //MaterialName;
				let filterList = this.selectResult.filter(item => item.value == val[0].value);
				console.info('当前选择备件', JSON.stringify(filterList));
				if (filterList.length > 0) {
					this.form3.SpecificationsModels = filterList[0].SpecificationsModels;
					this.form3.Unit = filterList[0].Unit;
					this.form3.SmallClass = filterList[0].SmallClass;
				}
			},
			//日期范围回调事件
			dateChange(e) {
				this.form.date = e.startDate + this.$t('common.To') + e.endDate;
				this.form.StartDate = e.startDate;
				this.form.EndDate = e.endDate;
				console.log(e);

			},
			//查询
			search() {
				// if (!this.form.StartDate) {
				// 	this.$refs.uToast.show({
				// 		title: '请输入日期区间！',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }

				//this.form.Creator = this.loginInfo.result.UserCode;
				var postData = {
					"queryJson": this.form,
				}
				console.info('提交参数', JSON.stringify(postData));
				this.GetEquipmentMaintainTaskByDateAndType(postData).then(res => {
					if (res && res.success) {
						this.gridList = res.resultData;
						console.info('保养任务列表', JSON.stringify(res.resultData));
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})

			},
			//添加备件
			addBadItem() {
				if (!this.form3.SparePartsCode) {
					this.$refs.uToast.show({
						title: this.$t('EP_EquipmentMaintainTask.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form3.SparePartsQty) {
					this.$refs.uToast.show({
						title: this.$t('EP_EquipmentMaintainTask.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//检测是否存在重复数据
				let filterList = this.SparePartsItemDetailList.filter(item => item.SparePartsId == this.form3
					.SparePartsCode);
				console.info('备件重复数据', JSON.stringify(filterList));
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t('EP_EquipmentMaintainTask.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//备件数组
				this.SparePartsItemDetailList.push({
					Checked: false,
					RepairId: this.form2.Id, //父任务主键ID
					SparePartsId: this.form3.SparePartsCode, //备件编码
					SparePartsName: this.form3.SparePartsName, //备件名称
					SpecificationsModels: this.form3.SpecificationsModels, //备件规格
					Unit: this.form3.Unit, //单位
					SmallClass: this.form3.SmallClass, //小类
					Num: this.form3.SparePartsQty, //数量
					EnabledMark: 1, //启用
					UseType: "", //使用类型
					Creator: this.loginInfo.result ? this.loginInfo.result.UserCode : this.form2
						.RepairingPerson //当前登录人编码
				});
				console.info('新增后备件数组', JSON.stringify(this.SparePartsItemDetailList));
				this.form3.SparePartsCode = "";
				this.form3.SparePartsName = "";
				this.form3.SpecificationsModels = "";
				this.form3.Unit = "";
				this.form3.SmallClass = "";
				this.form3.SparePartsQty = "1";
				// let arr = this.SparePartsItemDetailList.map(item => {
				//     return item.SparePartsQty;
				// });
				// this.form2.SparePartsQty = eval(arr.join("+"));
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
			//保存设备保养执行
			save() {
				if (!this.form2.RepairingPerson) {
					this.$refs.uToast.show({
						title: this.$t('EP_EquipmentMaintainTask.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//检测保养项目是否都填写
				if (this.EP_EquipmentMaintainDetailList.length > 0) {
					this.EP_EquipmentMaintainDetailList.forEach((item, index) => {
						if (item.EquipmentMaintainResult == '') {
							this.$refs.uToast.show({
								title: this.$t('EP_EquipmentMaintainTask.MessageTips_5'),
								type: 'warning',
								icon: true
							});
							return;
						}
					});
				}
				console.info('任务表单', JSON.stringify(this.form2));
				console.info('保养项目', JSON.stringify(this.EP_EquipmentMaintainDetailList));
				console.info('备件更换', JSON.stringify(this.SparePartsItemDetailList));
				//当前登录的用户信息
				console.info('当前登录人信息', JSON.stringify(this.loginInfo));
				//提交数据
				let posdata = {
					Id: this.form2.Id, //保养任务执行ID
					EquipmentId: this.form2.EquipmentId, //保养设备编码
					EquipmentName: this.form2.EquipmentName, //保养设备名称
					RepairingType: this.form2.RepairingType, //保养设备类别
					EquipmentMaintainTaskName: this.form2.EquipmentMaintainTaskName, //保养任务名称
					EquipmentMaintainTaskId: this.form2.EquipmentMaintainTaskId, //保养任务ID
					RepairingPerson: this.form2.RepairingPerson, //保养人编码
					RepairingPersonName: this.form2.RepairingPersonName, //保养人姓名
					EP_EquipmentMaintainDetailList: this.EP_EquipmentMaintainDetailList, //保养项目
					SparePartsItemDetailList: this.SparePartsItemDetailList, //备件更换
					ModifyBy: this.loginInfo.result ? this.loginInfo.result.UserCode : this.form2
						.RepairingPerson //当前登录人编码
				};
				console.info('提交保存内容', JSON.stringify(posdata));
				//return;
				this.SaveEP_EquipmentMaintainTask(posdata).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('EP_EquipmentMaintainTask.MessageTips_6'),
							type: 'success',
							icon: true
						});
						//过滤掉 已经执行完的保养任务
						this.gridList = this.gridList.filter(item => item.Id != this.form2.Id);
						//隐藏保养执行弹窗
						this.show_shd = !this.show_shd;
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
			}
		}
	}
</script>

<style lang="scss" scoped>
	.EP_EquipmentMaintainTask {
		//display: flex;
		flex-direction: column;
		padding: 20upx;
		//background: #f9f9f9;
		font-size: 32upx !important;
		//min-height: 100vh;  

		.top,
		.center,
		.bottom {
			margin-bottom: 20upx;
			background: #fff;
			//padding: 20upx;
		}

		.title {
			font-size: 38upx;
			font-weight: 600;
			//padding: 8upx;
		}

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

	.btn {
		display: flex;

		uni-button {
			width: 48%;
		}
	}

	.u-form-item {
		height: auto;
	}

	.u-form {
		background: #fff;
	}

	.readonly {
		background-color: Gainsboro;
	}

	.wrap {
		margin-top: 24rpx;
	}

	.item {
		display: flex;
		background: #fff;
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
						//padding: 10upx;
					}
				}
			}
		}
	}
</style>