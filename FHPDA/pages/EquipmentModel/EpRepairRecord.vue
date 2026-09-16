<template>
	<view class="maintainAcceptance">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('EpRepairRecord.FactoryName')" prop="FactoryName">
				<view style="width: 100%;" @click="clickSelFun('factory')">
					<u-input v-model="form.FactoryName" type="text" disabled
						:placeholder="$t('EpRepairRecord.FactoryName_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpRepairRecord.date')">
				<view style="width: 100%;" @click="clickSelFun('date')">
					<u-input v-model="form.date" type="text" border disabled
						:placeholder="$t('EpRepairRecord.date_placeholder')" style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpRepairRecord.WorkShopName')">
				<view style="width: 100%;" @click="clickSelFun('cj')">
					<u-input v-model="form.WorkShopName" disabled="" border
						:placeholder="$t('EpRepairRecord.WorkShopName_placeholder')" style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpRepairRecord.RepairingTypeName')">
				<view style="width: 100%;" @click="clickSelFun('Check')">
					<u-input v-model="form.RepairingTypeName" disabled="" border
						:placeholder="$t('EpRepairRecord.RepairingTypeName_placeholder')"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<view>
				<u-top-tips ref="uTips"></u-top-tips>
				<u-toast ref="uToast" />
			</view>
			<homeBtn></homeBtn>
		</u-form>

		<view style="margin-top:10rpx;">
			<u-divider halfWidth="100%">{{$t('EpRepairRecord.RepairTitle')}}</u-divider>
		</view>
		<scroll-view scroll-y="true" class="scroll-Y"
			style="height: 760rpx;margin-top: 10rpx;border: 1px solid Gainsboro;">
			<view class="item" v-for="(item, index) in gridList" :key="index">

				<view class="top" @click="Check_Mater(item)">
					<view class="name">{{$t('EpRepairRecord.EquipmentName')}}:{{item.EquipmentName}}</view>
					<view class="name">{{$t('EpRepairRecord.RepairingTypeName2')}}:{{item.RepairingTypeName}}</view>
					<view class="name">{{$t('EpRepairRecord.CreatorName')}}:{{item.CreatorName}}</view>
					<view class="name">{{$t('EpRepairRecord.CreateTime')}}:{{item.CreateTime}}</view>
					<view class="name">{{$t('EpRepairRecord.MalfunctionDescription')}}:{{item.MalfunctionDescription}}
					</view>
				</view>
			</view>
		</scroll-view>

		<u-popup border-radius="10" v-model="show_shd" @close="Upclose()" :mode="Upmode" length="99%"
			:closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<br>
			<br>
			<!-- 滚屏 -->
			<view class="header">
				<u-form :model="form2" ref="uForm">
					<u-form-item :label="$t('EpRepairRecord.EquipmentName')">
						<u-input class="readonly" v-model="form2.EquipmentName" type="text" disabled border
							placeholder="" />
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.RepairingTypeName')">
						<u-input class="readonly" v-model="form2.RepairingTypeName" type="text" disabled border
							placeholder="" />
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.FinishTime')">
						<u-input v-model="form2.FinishTime" type="text" border @click="clickSelFun('date2')" />
						<u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.TimeLength')">
						<u-input v-model="form2.TimeLength" type="number" border placeholder="" />
					</u-form-item>
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.RepairingContent')">
						<u-input v-model="form2.RepairingContent" type="text" border placeholder="" />
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.RepairingPersonName')">
						<u-input v-model="form4.RepairingPersonName"
							:placeholder="$t('EpRepairRecord.RepairingPersonName_placeholder')" type="text" border />
						<u-icon name="search" size="70upx" color="#138087" @click="OpenModel"></u-icon>
					</u-form-item>
					<u-form-item :label="$t('common.photosUpload')" style="height: auto;">
						<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="9"
							:upload-text="$t('common.chooseTips')"></u-upload>
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.SparePartsName')">
						<u-input v-model="form4.SparePartsName"
							:placeholder="$t('EpRepairRecord.SparePartsName_placeholder')" type="text" border />
						<u-icon name="search" size="70upx" color="#138087" @click="OpenModel1"></u-icon>
					</u-form-item>
					<u-form-item :label="$t('EpRepairRecord.SparePartsQty')">
						<u-input v-model="form4.SparePartsQty" type="number" border placeholder="" />
						<u-icon name="plus-circle-fill" size="70upx" color="#138087" @click="addForm"></u-icon>
						<u-icon name="trash-fill" size="70upx" color="#138087" @click="deleteForm"></u-icon>
					</u-form-item>
				</u-form>

				<!-- <view style="height: 250rpx; border: 1rpx solid #E6E6E6;margin-top: 10rpx;">
					<scroll-view scroll-y="true" class="scroll-Y" style="height: 250rpx;">
						<u-radio-group v-model="radioResult">
							<u-radio style="width:100%;" v-model="item.Checked" v-for="(item, index) in SpareList" :key="item.MaterialName"
							 :name="item.MaterialCode">
								<view style="border-bottom:1px solid Gainsboro;">
									<view class="label u-line-1">物料名称：{{item.MaterialName}}</view>
									<view class="label u-line-1">物料编码：{{item.MaterialCode}}</view>
									<view class="label u-line-1">物料型号：{{item.Spec}}</view>
								</view>
							</u-radio>
						</u-radio-group>
					</scroll-view>
				</view>
				 -->
				<u-table style="margin-top: 20rpx;">
					<u-tr class="u-tr">
						<u-th>{{$t('EpRepairRecord.SparePartsName')}}</u-th>
						<u-th>{{$t('EpRepairRecord.SpecificationsModels')}}</u-th>
						<u-th width="20%">{{$t('EpRepairRecord.Num')}}</u-th>
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
				<!-- 
				<scroll-view scroll-y="true" style="height: 260rpx;border:1px solid Gainsboro;margin-top: 10rpx;">
					<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;" v-for="(item, index) in SaveSpareList">
						<u-checkbox v-model="item.Checked">
							<view class="label u-line-1">物料名称：{{item.SparePartsName}}</view>
							<view class="label u-line-1">物料编码：{{item.SparePartsId}}</view>
							<view class="label u-line-1">备件数量：{{item.Num}}</view>
						</u-checkbox>
					</view>
				</scroll-view> -->



				<!-- 			<view style="height: 250rpx;border: 1rpx solid #E6E6E6;margin-top: 10rpx;">
					<scroll-view scroll-y="true" class="scroll-Y" style="height: 250rpx;width: 100%;">
						<u-radio-group v-model="saveRadioResult" style="width:100%;">
							<u-radio style="width:100%;" v-model="item.Checked" v-for="(item, index) in SaveSpareList" :key="item.SparePartsName"
							 :name="item.SparePartsId">
								<view style="border: 1px solid white;background-color:Gainsboro;width: 100%;">
									<view style="width: 100%;">物料名称：{{item.SparePartsName}}</view>
									<view style="width: 100%;">物料编码：{{item.SparePartsId}}</view>
									<view style="width: 100%;">备件数量：{{item.Num}}</view>
								</view>
							</u-radio>
						</u-radio-group>
					</scroll-view>
				</view> -->

				<view class="" style="display: flex;">
					<u-button :type="'primary'" class='return'
						:custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}" @click="exit"
						style="position: fixed;bottom: 30rpx;margin-left: 1%;">
						<text>{{$t('EpRepairRecord.CancelBtn')}}</text>
					</u-button>
					<u-button :type="'primary'" :custom-style="{width: '48%',height: '70rpx',borderRadius: '10rpx'}"
						@click="saveForm" style="position: fixed;bottom: 30rpx;margin-left: 50%;">
						<text>{{$t('EpRepairRecord.SaveBtn')}}</text>
					</u-button>
				</view>
			</view>

		</u-popup>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('EpRepairRecord.SearchBtn')}}</text>
			</u-button>
		</view>

		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
		<u-select v-model="isShowCheck" @confirm="changeCheckFun" :list="selectCheck"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
		<!-- 备件名称 -->
		<u-select v-model="isShowResult" @confirm="changecjFun" :list="selectResult"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
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
				action: global.FileHandler, //图片上传地址
				filePath: global.FilePath,
				fileList: [], //文件上传列表
				form: {
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					date: "",
					StartDate: "",
					EndDate: "",
					RepairingType: "",
					RepairingTypeName: "",
					WorkShopName: "",
					WorkShop: ""
				},
				form2: {
					Id: "",
					EquipmentId: "",
					EquipmentName: "",
					RepairingTypeName: "",
					FinishTime: "",
					RepairingPersonCode: "",
					RepairingPersonName: "",
					RepairNum: "",
					TimeLength: "",
					ModifyBy: "",

				},
				form3: {
					SparePartsCode: "",
					SparePartsName: "",
					SpecificationsModels: "",
					Unit: "",
					SmallClass: "",
					SparePartsQty: "1",

				},

				form4: {
					RepairingPersonCode: "",
					RepairingPersonName: "",
					SparePartsCode: "",
					SparePartsName: "",
					SpecificationsModels: "",
					Unit: "",
					SmallClass: "",
					SparePartsQty: "",
				},

				factoryList: [], //工厂列表
				showFactory: false, //工厂弹窗
				SparePartsItemDetailList: [],
				list: 15,
				page: 0,
				isShowDate: false,
				mode: 'range',
				gridList: [],
				isShowCheck: false,
				isShowResult: false,
				selectCheck: [],
				selectResult: [],
				show_shd: false,

				isShowDate2: false,
				Upmode: 'right',
				Upmask: true, // 是否显示遮罩
				Upcloseable: true,
				UpcloseIconPos: 'top-left',
				UserList: [],

				isShowUser: false,
				selectUser: [],
				isShowSpare: false,
				SpareList: [],
				radioResult: "",
				SaveSpareList: []
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
			//console.log(JSON.stringify(this.loginInfo));
			_self = this;
			_self.getFactoryList();

			this.GetDictionary({
				"EnCode": "RepairsCategory"
			}).then(res => {
				this.selectCheck = [];

				if (res && res.success) {
					this.selectCheck = res.resultData;
					this.selectCheck.splice(0, 0, {
						value: "",
						label: this.$t('common.firstSelect')
					});
				}
			})

		},

		onShow: function(option) {
			uni.$on("to-parent", res => {
				console.log(res.result.UserName);

				this.form4.RepairingPersonCode = res.result.UserCode;
				this.form4.RepairingPersonName = res.result.UserName;
				console.log(this.form2.RepairingPersonName);
				uni.$off("to-parent");

			});

			uni.$on("to-parent1", res => {
				console.log(res.result.Unit);
				this.form4.SparePartsCode = res.result.SparePartsCode;
				this.form4.SparePartsName = res.result.SparePartsName;
				this.form4.SpecificationsModels = res.result.SpecificationsModels;
				this.form4.Unit = res.result.Unit;
				this.form4.SmallClass = res.result.SmallClass;
				console.log(this.form4.SpecificationsModels);
				uni.$off("to-parent1");

			});
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.EquipmentModel.EquipmentModel/EpRepairRecord")
			});

		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('Equipment', ['GetEquipmentMalfunctionRepair', 'SaveEquipmentRepairRecord']),
			...mapActions('common', ['GetDictionary', 'GetResourceByLevelCode', 'GetUserList', 'GetBaseMaterialList',
				'GetListByParentResource'
			]),


			//start 弹窗选择*****************
			Check_Mater(item) {

				console.log(JSON.stringify(item));
				//this.code = val
				this.show_shd = !this.show_shd;
				this.form2 = item;
			},

			//打开选择框
			OpenModel() {
				console.log("模态框");
				// if (!this.form.ProcessCode) {
				// 	this.$refs.uToast.show({
				// 		title: "请选择工序",
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }
				uni.navigateTo({
					url: '/pages/public/UserModel',
				});
			},

			OpenModel1() {
				uni.navigateTo({
					url: '/pages/public/SpareModel',
				});
			},

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
							_self.getWorkShop();
						}
					} else {
						this.factoryList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			getWorkShop() {
				this.GetListByParentResource({
					ParentResource: this.form.FactoryCode
				}).then(res => {
					this.selectResult = [];
					if (res && res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.selectResult = [];
						} else {
							this.selectResult.push({
								value: "",
								label: this.$t('common.firstSelect')
							});
							res.resultData.forEach((item, index) => {
								this.selectResult.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.form.WorkShop = "";
							this.form.WorkShopName = "";
						}
					}
				})
			},
			SearchUser() {
				var queryJson = {
					"UserCode": this.form2.RepairingPersonName,
				};
				this.GetUserList(queryJson).then(res => {
					this.UserList = [];
					if (res && res.success) {
						res.resultData.forEach((item, index) => {
							this.UserList.push({
								value: item.Code,
								label: item.Code + '-' + item.Name
							});
							this.isShowUser = true;
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

			changeUserFun(val) {
				this.form2.RepairingPerson = val[0].value;
				this.form2.RepairingPersonName = val[0].label;
				//this.show_shd = !this.show_shd;
				// this.searchDetail();
			},
			dateChange2(e) {
				this.form2.FinishTime = e.result;
			},

			searchSpareParts() {
				this.isShowSpare = true;

				var postdata = {
					queryJson: {
						"Material": this.form3.SparePartsName,
						"MaterialClassName": "'DQYP','BPBJ'"
					}
				}

				this.GetBaseMaterialList(postdata).then(res => {

					this.selectResult = [];
					if (res && res.success) {
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
			changesearchSpareFun(val) {
				debugger
				this.form3.SparePartsId = val[0].value;
				this.form3.SparePartsName = val[0].MaterialName;
			},
			addForm(val) {
				if (!this.form4.SparePartsCode) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}

				if (!this.form4.SparePartsQty) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//检测是否存在重复数据
				let filterList = this.SparePartsItemDetailList.filter(item => item.SparePartsId == this.form4
					.SparePartsCode);
				console.info('备件重复数据', JSON.stringify(filterList));
				if (filterList.length > 0) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}


				//备件数组
				this.SparePartsItemDetailList.push({
					Checked: false,
					RepairId: this.form2.Id, //父任务主键ID
					SparePartsId: this.form4.SparePartsCode, //备件编码
					SparePartsName: this.form4.SparePartsName, //备件名称
					SpecificationsModels: this.form4.SpecificationsModels, //备件规格
					Unit: this.form4.Unit, //单位
					SmallClass: this.form4.SmallClass, //小类
					Num: this.form4.SparePartsQty, //数量
					EnabledMark: 1, //启用
					UseType: "", //使用类型
					Creator: this.loginInfo.result ? this.loginInfo.result.UserCode : this.form2
						.RepairingPerson //当前登录人编码
				});
				console.info('新增后备件数组', JSON.stringify(this.SparePartsItemDetailList));
				this.form4.SparePartsCode = "";
				this.form4.SparePartsName = "";
				this.form4.SpecificationsModels = "";
				this.form4.Unit = "";
				this.form4.SmallClass = "";
				this.form4.SparePartsQty = "";
			},
			deleteForm(val) {
				this.SparePartsItemDetailList = this.SparePartsItemDetailList.filter(item => {
					return item.Checked == false;
				});
				console.info('删除后备件数组', JSON.stringify(this.SparePartsItemDetailList));
			},
			saveForm() {
				if (this.SparePartsItemDetailList.length < 1) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form2.FinishTime) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_5'),
						type: 'warning',
						icon: true
					});
					return;
				}


				if (!this.form2.TimeLength) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_6'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form2.TimeLength <= 0) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_7'),
						type: 'warning',
						icon: true
					});
					return;

				}


				if (!this.form4.RepairingPersonName) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_8'),
						type: 'warning',
						icon: true
					});
					return;
				}
				//通过filter，筛选出上传进度为100的文件(因为某些上传失败的文件，进度值不为100，这个是可选的操作)
				let files = this.$refs.uUpload.lists.filter(val => {
					return val.progress == 100;
				})

				let fileUrl = [];
				files.forEach(item => {
					fileUrl.push({
						FileName: item.file.name,
						ImgType: item.file.type,
						FilePath: this.filePath + item.response.resultData
					})
				})

				console.log(this.form4.Unit);
				this.form2.ModifyBy = this.loginInfo.result.UserCode;
				this.form2.RepairingPerson = this.loginInfo.result.UserCode;
				var postData = {
					entity: this.form2,
					data: this.SparePartsItemDetailList,
					fileUrl: fileUrl
				}


				this.SaveEquipmentRepairRecord(postData).then(res => {
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('EpRepairRecord.MessageTips_9'),
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
				})


			},

			//返回
			exit() {
				this.show_shd = !this.show_shd;
				// uni.navigateBack({
				// 	delta: 1
				// })
			},

			Upclose() {},

			//end 弹窗选择*******************


			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "factory")
					this.showFactory = true;
				else if (val == "Check") {
					this.isShowCheck = true;
				} else if (val == "Result") {
					this.searchSpareParts();
				} else if (val == "date") {
					this.isShowDate = true;
				} else if (val == "date2") {
					this.isShowDate2 = true;
				} else if (val = 'cj') {
					this.isShowResult = true;
				}
			},
			//选择工厂
			changeFactory(val) {
				this.form.FactoryCode = val[0].value; //val[0].label;				
				this.form.FactoryName = val[0].label;
				_self.getWorkShop();
			},
			//下拉框选择事件
			changeCheckFun(val) {
				this.gridList = [];
				this.form.RepairingType = val[0].value; //val[0].label;
				this.form.RepairingTypeName = val[0].label;

			},
			changeResultFun(val) {
				this.form.WorkShop = val[0].value; //val[0].label;
				this.form.WorkShopName = val[0].label;
			},
			changecjFun(val) {
				this.form.WorkShop = val[0].value; //MaterialCode
				this.form.WorkShopName = val[0].label; //MaterialName;
			},
			changebjFun(val) {
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

			dateChange(e) {
				this.form.date = e.startDate + this.$t('common.To') + e.endDate;
				this.form.StartDate = e.startDate;
				this.form.EndDate = e.endDate;
			},


			//查询
			search() {

				if (!this.form.StartDate) {
					this.$refs.uToast.show({
						title: this.$t('EpRepairRecord.MessageTips_10'),
						type: 'warning',
						icon: true
					});
					return;
				}

				//this.form.Creator = this.loginInfo.result.UserCode;
				var postData = {
					"queryJson": this.form,
				}
				this.GetEquipmentMalfunctionRepair(postData).then(res => {
					if (res && res.success) {
						this.gridList = res.resultData;
					} else {
						this.gridList = [];
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})

			},

			getDetaile(val) {
				console.log(898)
				uni.navigateTo({
					url: '/pages/EquipmentModel/checkInput?CheckNumber=' + val.CheckNumber + '&CheckType=' + val
						.CheckType
				})
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
	.maintainAcceptance {
		padding: 20upx;
		//background: #f9f9f9;
		font-size: 32upx;
		//height: 100vh;
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
		//padding: 20upx;
		flex-direction: column;
		margin-top: 2rpx;
		//margin-bottom: 8upx !important;
		align-items: center;
		//box-shadow: 0px 3px 3px #7b7b7b;

		.left {
			width: 160upx;
		}

		.top {
			//display: flex;
			width: 100%;
			//padding: 0 20upx 20upx 0;
			align-items: center;
			border-bottom: 1px solid white;
			background-color: Gainsboro;

			.name {
				font-size: 30upx;
				//font-weight: 600;
			}
		}
	}
</style>