<template>
	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<!-- <u-form-item label="返工时间:">
				<u-input v-model="form.reworkDate" placeholder="请选择返工时间" clearable type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item> -->
			<u-form-item :label="$t('PMRecordSel.FactoryName')" prop="FactoryName">
				<view style="width: 100%;" @click="showSel('factory')">
					<u-input v-model="form.FactoryName" disabled
						:placeholder="$t('PMRecordSel.FactoryNamer_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('PMRecordSel.ProcessName')" prop="ProcessName">
				<view style="width: 100%;" @click="showSel('process')">
					<u-input v-model="form.ProcessName" disabled
						:placeholder="$t('PMRecordSel.ProcessName_placeholder')" border style="pointer-events: none;" />
				</view>
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PMRecordSel.RecordInfo')}}</u-divider>
		</view>
		<view style="height: auto;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 500px;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text style="font-size: 14px;">
									{{item.ProductOrder}}&#12288{{item.ContainerNO}}&#12288{{item.MMXH}}
								</text>
							</template>
							<view>{{$t('PMRecordSel.PalletQty')}}：{{item.PalletQty}}</view>
							<view>{{$t('PMRecordSel.ReworkProcessName')}}：{{item.ReworkProcessName}}</view>
							<view>{{$t('PMRecordSel.DutyProcessName')}}：{{item.DutyProcessName}}</view>
							<view>{{$t('PMRecordSel.DutyUserNames')}}：{{item.DutyUserNames}}</view>
							<view>
								{{$t('PMRecordSel.ReworkProcessFinishTime')}}：{{formatTime(item.ReworkProcessFinishTime)}}
							</view>
						</u-collapse-item>

					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<!-- 
		<view style="margin-top: 40rpx;">
			<u-button @click="search" :type="'primary'" style="width: 50%;">查询
			</u-button>
		</view> -->
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
					ProcessBelongName: '',
					reworkDate: "",
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
				},
				gridList: [],
				factoryList: [], //工厂列表
				showFactory: false, //工厂弹窗
				//工序列表
				processList: [],
				//工序是否显示弹窗
				showProcess: false,

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
				title: this.$t("menu.ProduceModel.ProduceModel/PMRecordSel")
			});
		},


		methods: {
			...mapActions('Produce', ['ReworkQuery']),
			...mapActions('common', ['GetDictionary', 'GetResourceByLevelCode', 'GetProcessModel']),

			getDate(val) {
				this.form.reworkDate = val;
			},


			//查询
			search() {

				// if (!this.form.reworkDate) {
				// 	this.$refs.uToast.show({
				// 		title: '请选择返工日期',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }

				var postData = {
					// reworkDate: this.form.reworkDate,
					processCode: this.form.ProcessCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ReworkQuery(postData).then(res => {
					uni.hideLoading();
					if (res && res.success) {
						this.gridList = [];
						if (res.resultData == null || res.resultData.length == 0) {
							this.gridList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.gridList.push({
									ProductOrder: item.ProductOrder,
									ContainerNO: item.ContainerNO,
									MMXH: item.MMXH,
									PalletQty: item.PalletQty,
									ReworkProcess: item.ReworkProcess,
									ReworkProcessName: item.ReworkProcessName,
									DutyProcess: item.DutyProcess,
									DutyProcessName: item.DutyProcessName,
									DutyUserNames: item.DutyUserNames,
									ReworkProcessFinishTime: item.ReworkProcessFinishTime,
								});
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
							label: this.$t("common.None")
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
						}

					} else {
						this.processList = [{
							value: '',
							label: this.$t("common.None")
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
				this.form.FactoryCode = val[0].value; //val[0].label;				
				this.form.FactoryName = val[0].label;
				_self.getProcessList();
			},
			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
				_self.search();
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