<template>

	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMFirstQuality.FactoryName')" prop="FactoryName">
				<view style="width: 100%;" @click="showSel('factory')">
					<u-input v-model="form.FactoryName" disabled
						:placeholder="$t('PMFirstQuality.FactoryNamer_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('PMFirstQuality.ProcessName')" prop="ProcessName">
				<view style="width: 100%;" @click="showSel('process')">
					<u-input v-model="form.ProcessName" disabled
						:placeholder="$t('PMFirstQuality.ProcessName_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<!-- 	<u-form-item label="开工时间:">
				<u-input v-model="form.reworkDate" disabled placeholder="请选择返工时间" type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item> -->

			<u-form-item :label="$t('PMFirstQuality.ProductOrder')">
				<u-input v-model="form.ProductOrder" type="text" placeholder="" border :focus="focus1" />
			</u-form-item>
			<u-form-item :label="$t('PMFirstQuality.ContainerNO')">
				<u-input v-model="form.ContainerNO" type="text" placeholder="" border :focus="focus2" />
			</u-form-item>

		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PMFirstQuality.TestTaskInfo')}}</u-divider>
		</view>
		<view style="height: 630rpx;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
						<u-collapse-item class="u-collapse-item" :index="item.Id" @change="chkItem">
							<template slot="title">
								<text
									style="font-size: 14px;">{{$t('PMFirstQuality.ProductOrder')}}：{{item.ProductOrder}}</text>
								<text
									style="font-size: 14px;">{{$t('PMFirstQuality.MaterialCode')}}：{{item.MaterialCode}}</text>
							</template>
							<view>{{$t('PMFirstQuality.MaterialCode')}}：{{item.MaterialCode}}</view>
							<view>{{$t('PMFirstQuality.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view>{{$t('PMFirstQuality.MMXH')}}：{{item.MMXH}}</view>
							<view>{{$t('PMFirstQuality.ProcessName')}}：{{item.ProcessName}}</view>
						</u-collapse-item>

					</view>
				</u-collapse>
			</scroll-view>
		</view>

		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="quality"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('PMFirstQuality.SaveBtn')}}
			</u-button>
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="search"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('PMFirstQuality.SearchBtn')}}
			</u-button>
		</view>

		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"></u-select>
		<!-- 责任工序选择 -->
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
					ProductOrder: "",
					ContainerNO: "",
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					ProcessCode: "",
					ProcessName: "",
				},
				factoryList: [], //工厂列表
				processList: [],
				showFactory: false, //工厂弹窗
				showProcess: false,
				gridList: [],

				//焦点
				focus1: false,
				focus2: false,
				focus3: false,
				focus4: false,


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
				title: this.$t("menu.QualityModel.ProduceModel/PMFirstQuality")
			});
		},


		methods: {
			...mapActions('Produce', ['FirstInspectionConfirmQuery']),
			...mapActions('common', ['GetDictionary', 'GetResourceByLevelCode', 'GetProcessModel']),

			getDate(val) {
				this.form.reworkDate = val;

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
				_self.setFocus("focus1");

			},

			//初始化工厂列表
			getFactoryList() {
				var data = {
					LevelCode: "Factory"
				}
				this.GetResourceByLevelCode(data).then(res => {
					this.processList = [];
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

			//查询
			search() {

				var query = {
					queryJson: {
						"Process": this.form.ProcessCode,
						"ProductOrder": this.form.ProductOrder,
						"ContainerNO": this.form.ContainerNO,

					}
				}
				this.FirstInspectionConfirmQuery(query).then(res => {
					if (res && res.success) {
						this.gridList = [];
						if (res.resultData == null || res.resultData.length == 0) {
							this.gridList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.gridList.push({
									Id: item.Id,
									ProductOrder: item.ProductOrder,
									ContainerNO: item.ContainerNO,
									MaterialCode: item.MaterialCode,
									MaterialName: item.MaterialName,
									ProcessName: item.ProcessName,
									MMXH: item.MMXH,
									FirstResult: item.FirstResult,
									FirstUser: item.FirstUser,
									FirstTime: item.FirstTime,
									SecondResult: item.SecondResult,
									SecondUser: item.SecondUser,
									SecondTime: item.SecondTime,
									SecondMarkName: item.SecondMarkName,
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
				this.gridList = [];
				this.pdList = [];
			},

			chkItem(item) {
				this.form.id = item.index;
				console.log(item.index);
			},



			//跳转质量复检页面
			quality(val) {

				uni.navigateTo({
					url: '/pages/ProduceModel/PMSecondQuality?id=' + this.form.id,
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