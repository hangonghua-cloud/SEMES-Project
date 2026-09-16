<template>

	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">

			<u-form-item :label="$t('PMShopSel.ProductOrder')">
				<u-input v-model="form.ProductOrder" type="text" placeholder="" border />
			</u-form-item>
			<u-form-item :label="$t('PMShopSel.ContainerNO')">
				<u-input v-model="form.ContainerNO" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item :label="$t('PMShopSel.ProcessName')" prop="ProcessName">
				<u-input v-model="form.ProcessName" @click="showSel('process')" type="text" disabled placeholder="请选择工序"
					border />
			</u-form-item>

			<u-form-item :label="$t('PMShopSel.reworkDate')">
				<u-input v-model="form.reworkDate" disabled :placeholder="$t('PMShopSel.reworkDate_placeholder')"
					type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item>

			<u-form-item :label="$t('PMShopSel.reworkDate1')">
				<u-input v-model="form.reworkDate1" disabled :placeholder="$t('PMShopSel.reworkDate1_placeholder')"
					type="text" border />
				<date-picker @getTime="getDate1"></date-picker>
			</u-form-item>

		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PMShopSel.QualityInfo')}}</u-divider>
		</view>

		<u-form>
			<u-form-item :label="$t('PMShopSel.QualityMSG')" style="height: auto;">
				<view style="border: 1px solid Gainsboro;">
					<view class="label">{{$t('PMShopSel.ProductOrder')}}：{{form1.ProductOrder}}</view>
					<view class="label">{{$t('PMShopSel.ContainerNO')}}：{{form1.ContainerNO}}</view>
					<view class="label">{{$t('PMShopSel.ProcessName')}}：{{form1.ProcessName}}</view>
					<view class="label">{{$t('PMShopSel.MachineName')}}：{{form1.MachineName}}</view>
					<view class="label">{{$t('PMShopSel.CreateTime')}}：{{form1.CreateTime}}</view>
					<view class="label">{{$t('PMShopSel.Determination')}}：{{form1.Determination}}</view>
				</view>
			</u-form-item>
		</u-form>


		<u-table style="margin-top: 20rpx;">
			<u-tr class="u-tr">
				<u-th>{{$t('PMShopSel.TestItemName')}}</u-th>
				<u-th>{{$t('PMShopSel.TestDepartmentName')}}</u-th>
				<u-th>{{$t('PMShopSel.QualityResult')}}</u-th>
			</u-tr>
			<u-tr v-for="(item,index) of gridList" :key="index">
				<u-th>
					<view class="u-text" @click="showQCTestResultRecordList(item)">
						{{item.TestItemName}}
					</view>
				</u-th>
				<u-th>
					<view class="u-text" @click="showQCTestResultRecordList(item)">
						{{item.TestDepartmentName}}
					</view>
				</u-th>
				<u-th>
					<view class="u-text" @click="showQCTestResultRecordList(item)">
						{{item.QualityResult}}
					</view>
				</u-th>

			</u-tr>
		</u-table>


		<!--确认按钮-->


		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="search"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('PMShopSel.SearchBtn')}}
			</u-button>
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="search1"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('PMShopSel.SaveBtn')}}
			</u-button>
		</view>



		<u-popup border-radius="10" v-model="show_shd" @close="Upclose()" :mode="Upmode" length="100%"
			:closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<!-- <br>
			      <br> -->
			<!-- 滚屏 -->
			<view class="header">
				<view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
					<u-divider halfWidth="100%">{{$t('PMShopSel.PMShopSelModelTitle')}}</u-divider>
				</view>

				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('PMShopSel.secondResultNAME')">
						<u-input v-model="form.secondResultNAME" @click="showSel1('showTestItem')" type="text" disabled
							:placeholder="$t('PMShopSel.secondResultNAME_placeholder')" border />
					</u-form-item>

					<u-form-item :label="$t('PMShopSel.Remark')"
						style="height: auto;margin-bottom: -5px;margin-top: 1px;">
						<u-input v-model="form.Remark" type="textarea" placeholder="" border />
					</u-form-item>
				</u-form>

				<view class="btn" style="margin-bottom: 20upx;">
					<u-button :ripple="true" ripple-bg-color="#138087" style="width: 43%;margin-left: 4%;"
						@click="exit">{{$t('PMShopSel.CancelBtn')}}</u-button>
					<u-button type="primary" :ripple="true" ripple-bg-color="#138087"
						style="width: 43%;margin-left: 2%;" @click="Save">{{$t('PMShopSel.ConfirmBtn')}}</u-button>
				</view>
				<!--复检判定-->
				<u-select v-model="showTestItem" @confirm="changeTestItem" :list="pdjgList"></u-select>

			</view>
		</u-popup>


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
				usercode: "",
				form: {
					ProcessBelongName: '',
					ProductOrder: "",
					WorkOrder: "",
					ExeWorkOrder: "",
					CreateTime: "",
					ProcessName: "",
					ProcessCode: "",
					MachineName: "",
					Determination: "",
					reworkDate: "",
					reworkDate1: "",
					processList: [],
					secondResultCode: "",
					secondResultNAME: "",

				},
				showTestItem: false,
				pdjgList: [],
				form1: {
					Id: "",
					ContainerNO: "",
					ProductOrder: "",
					WorkOrder: "",
					ExeWorkOrder: "",
					CreateTime: "",
					ProcessName: "",
					MachineName: "",
					Determination: "",
					CreateTime: "",
				},

				form2: {
					secondResultNAME: "",
					remarks: "",
				},

				showProcess: false,
				gridList: [],
				show_shd: false, //记录查询页面
				Upmode: 'right', //显示弹窗从右出到左
				Upmask: true, // 是否显示遮罩
				Upcloseable: false, //是否显示弹窗关闭按钮
				UpcloseIconPos: 'top-right', //显示弹窗关闭按钮 显示位置top-left
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
			//_self = this;
			this.getProcessList();
			this.getsJList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.QualityModel.ProduceModel/PMShopSel")
			});
		},


		methods: {
			...mapActions('Produce', ['FirstInspectionQueryResult', 'FirstInspectionLastSave']),
			...mapActions('common', ['GetDictionary', 'GetProcessModel']),

			getDate(val) {
				this.form.reworkDate = val;
			},

			getDate1(val) {
				this.form.reworkDate1 = val;
			},
			//显示下拉框
			showSel(val, item) {
				if (val == "process") {

					this.showProcess = true;
				}

			},


			//显示下拉框
			showSel1(val, item) {
				if (val == "showTestItem") {
					this.showTestItem = true;
				}
			},


			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
			},


			//初始化工序列表
			getProcessList() {
				var data = {
					FactoryCode: this.$FactoryCode
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

			search1() {

				this.show_shd = !this.show_shd;

			},

			//查询
			search() {



				var query = {
					queryJson: {
						ProductOrder: this.form.ProductOrder,
						ContainerNO: this.form.ContainerNO,
						"ProcessCode": this.form.ProcessCode,
						"StartDate": this.form.reworkDate,
						"EndDate": this.form.reworkDate1,
					}

				}
				this.FirstInspectionQueryResult(query).then(res => {
					if (res && res.success) {
						this.gridList = [];
						if (res.resultData == null || res.resultData.length == 0) {
							this.gridList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							this.form1.Id = res.resultData[0].Id,
								this.form1.ProductOrder = res.resultData[0].ProductOrder,
								this.form1.ContainerNO = res.resultData[0].WorkOrder,
								this.form1.MachineName = res.resultData[0].MachineName,
								this.form1.CreateTime = res.resultData[0].CreateTime,
								this.form1.ProcessName = res.resultData[0].ProcessName,
								this.form1.Determination = res.resultData[0].Determination

							if (res.resultData[0].optionList != null) {

								res.resultData[0].optionList.forEach((item, index) => {
									this.gridList.push({
										TestItemName: item.TestItemName,
										DataTypeName: item.DataTypeName,
										TestItemStandard: item.TestItemStandard,
										QualityResult: item.QualityResult,
										TestDepartmentName: item.TestDepartmentName,
									});

								});
							}
							this.usercode = this.loginInfo.result ? this.loginInfo.result.UserCode : 'App';

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
				this.form1.Id = "",
					this.form1.ContainerNO = "",
					this.form1.ProductOrder = "",
					this.form1.WorkOrder = "",
					this.form1.ExeWorkOrder = "",
					this.form1.CreateTime = "",
					this.form1.ProcessName = "",
					this.form1.MachineName = "",
					this.form1.Determination = "",

					this.gridList = [];
			},
			//返回按钮 关闭弹窗
			exit() {
				this.show_shd = !this.show_shd;
			},

			Save() {
				var data = {
					id: this.form1.Id,
					determination: this.form.secondResultCode,
					userCode: this.usercode,
					//userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',																			
				}

				this.FirstInspectionLastSave(data).then(res => {
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('PMShopSel.MessageTips_1'),
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


			//选择判定结果
			changeTestItem(val) {

				this.form.secondResultCode = val[0].value;
				this.form.secondResultNAME = val[0].label;
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
							label: this.$t("common.None")
						}];
					}
				});
			},


			Upclose() {},
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

	.label {
		line-height: 20px;
		width: 250px;
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