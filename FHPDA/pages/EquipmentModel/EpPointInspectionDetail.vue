<template>
	<view class="maintainAcceptance">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('EpPointInspectionDetail.FactoryName')" prop="FactoryName">
				<view style="width: 100%;" @click="clickSelFun('factory')">
					<u-input v-model="form.FactoryName" type="text" disabled
						:placeholder="$t('EpPointInspectionDetail.FactoryName_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpPointInspectionDetail.ProcessBelongName')">
				<view style="width: 100%;" @click="clickSelFun('process')">
					<u-input v-model="form.ProcessBelongName" type="text"  disabled="" border
						:placeholder="$t('EpPointInspectionDetail.ProcessBelongName_placeholder')"
						 style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpPointInspectionDetail.EquipmentTypeName')">
				<view style="width: 100%;" @click="clickSelFun('equipType')">
					<u-input v-model="form.EquipmentTypeName" type="text"  disabled=""
						border :placeholder="$t('EpPointInspectionDetail.EquipmentTypeName_placeholder')"
						 style="pointer-events: none;"/>
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpPointInspectionDetail.StartTime')">
				<u-input v-model="form.StartTime" disabled
					:placeholder="$t('EpPointInspectionDetail.StartTime_placeholder')" type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item>
			<scroll-view scroll-y="true" class="scroll-Y">
				<u-collapse>
					<view class="grid" v-for="(item, index) in gridList">

						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text
									style="font-size: 32rpx;">{{$t('EpPointInspectionDetail.EquipmentName')}}：{{item.EquipmentName}}</text>
							</template>
							<view>{{$t('EpPointInspectionDetail.CheckTaskName')}}:{{item.CheckTaskName}}</view>
							<view>{{$t('EpPointInspectionDetail.CheckConclusionName')}}:{{item.CheckConclusionName}}
							</view>
							<view>{{$t('EpPointInspectionDetail.TypeInTime')}}:{{item.TypeInTime}}</view>
							<view v-for="(detail,deIndex) in  item.CheckItemList">
								<view>{{detail.CheckItemName}}:{{detail.CheckResult}}</view>
							</view>
						</u-collapse-item>
					</view>
				</u-collapse>

			</scroll-view>
			<view>
				<u-top-tips ref="uTips"></u-top-tips>
				<u-toast ref="uToast" />
			</view>
			<homeBtn></homeBtn>



			<view class="" style="display: flex;justify-content: center;">
				<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
					@click="search" style="position: fixed;bottom: 30rpx;">
					<text>{{$t('EpPointInspectionDetail.SearchBtn')}}</text>
				</u-button>
			</view>
		</u-form>
		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
		<u-select v-model="isShowCheck" @confirm="changeCheckFun" :list="selectCheck"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
		<u-select v-model="isShowResult" @confirm="changeResultFun" :list="selectResult"
			:confirm-text="$t('showModal.confirm')" :cancel-text="$t('showModal.cancel')"></u-select>
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
	import datePicker from '@/components/timePicker/datePicker.vue'
	import scanCode from '@/components/scanCode/scanCode.vue'
	var _self;
	export default {
		data() {
			return {

				form: {
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					ProcessBelongName: '',
					EquipmentTypeName: "",
					ProcessBelong: '',
					EquipmentType: "",
					StartTime: "",
				},
				list: 15,
				page: 0,
				gridList: [],
				value1: 1,
				factoryList: [], //工厂列表
				showFactory: false, //工厂弹窗
				isShowCheck: false,
				isShowResult: false,
				selectCheck: [],
				selectResult: []
			};
		},
		filters: {
			formatDate(time) {
				var date = new Date(time);
				return formatDate(date, "yyyy-MM-dd hh:mm");
			}
		},
		components: {
			datePicker,
			scanCode
		},
		mixins: [commonMixin],
		onLoad: function(option) {
			//console.log(JSON.stringify(this.loginInfo));
			//this.LoadList("");
			_self = this;
			_self.getFactoryList();

			this.GetDictionary({
				"EnCode": "EquipmentTypes"
			}).then(res => {
				if (res && res.success) {
					this.selectResult = res.resultData;
				}
			})
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.EquipmentModel.EquipmentModel/EpPointInspectionDetail")
			});
		},
		methods: {
			...mapActions('Equipment', ['GetDataTableCheckResult']),
			...mapActions('common', ['GetDictionary', 'GetResourceByLevelCode', 'GetProcessModel']),

			getDate(val) {
				this.form.StartTime = val;
			},
			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "factory")
					this.showFactory = true;
				else if (val == "process") {
					this.isShowCheck = true;
				} else if (val == "equipType") {
					this.isShowResult = true;
				}
			},
			//选择工厂
			changeFactory(val) {
				this.form.FactoryCode = val[0].value; //val[0].label;				
				this.form.FactoryName = val[0].label;
				_self.getProcessList();
			},
			//下拉框选择事件
			changeCheckFun(val) {
				this.form.ProcessBelong = val[0].value; //val[0].label;
				this.form.ProcessBelongName = val[0].label;
			},
			changeResultFun(val) {
				this.form.EquipmentType = val[0].value; //val[0].label;
				this.form.EquipmentTypeName = val[0].label;
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
							_self.getProcessList();
						}
					} else {
						this.factoryList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			getProcessList() {
				this.GetProcessModel({
					FactoryCode: this.form.FactoryCode
				}).then(res => {
					this.selectCheck = [];
					if (res && res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.selectCheck = [];
						} else {

							res.resultData.forEach((item, index) => {
								this.selectCheck.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.form.ProcessBelong = "";
							this.form.ProcessBelongName = "";
						}
					}
				})
			},
			//查询
			search() {
				if (!this.form.ProcessBelong) {
					this.$refs.uToast.show({
						title: this.$t('EpPointInspectionDetail.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.EquipmentType) {
					this.$refs.uToast.show({
						title: this.$t('EpPointInspectionDetail.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.StartTime) {
					this.$refs.uToast.show({
						title: this.$t('EpPointInspectionDetail.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}

				var postData = {
					"queryJson": this.form,
				}
				this.GetDataTableCheckResult(postData).then(res => {
					if (res && res.success) {
						this.gridList = res.resultData;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});

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
		background: #f9f9f9;
		font-size: 38upx;
		//height: 100vh;
	}

	.u-form {
		position: fixed;
		top: var(--window-top);
		left: 0;
		background: #fff;
		width: 100%;
		height: 140upx;
		z-index: 10000;
		//box-shadow: 0px 3px 3px #7b7b7b;

	}

	.readonly {
		background-color: Gainsboro;
	}

	.u-form-item--left {
		font-size: 36upx !important;
	}

	.u-form-item {
		font-size: 38upx;
	}

	.wrap {
		margin-top: 24rpx;
	}

	.scroll-Y {
		padding: 10rpx;
		font-size: 38upx;
		font-weight: 600rpx;
		height: 820rpx;

		.grid {
			border: 1px solid white;
			//font-size: 38upx;

			.u-collapse-item {
				background-color: Gainsboro;
				font-size: 32rpx;
				width: 320%;

				view {
					background-color: #F0F3FA;
					font-size: 32rpx;
				}
			}

		}
	}
</style>