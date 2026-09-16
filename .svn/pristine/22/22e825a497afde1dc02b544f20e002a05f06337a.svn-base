<template>
	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item label="责任工序" prop="ProcessName">
				<u-input v-model="form.ProcessName" @click="showSel('process')" type="text" disabled placeholder="请选择工序"
					border />
			</u-form-item>
			<u-form-item label="开始时间:">
				<u-input v-model="form.startDate" disabled placeholder="请选择开始时间" type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item>
			<u-form-item label="结束时间">
				<u-input v-model="form.closeDate" disabled placeholder="请选择开始时间" type="text" border />
				<date-picker @getTime="getDate1"></date-picker>
			</u-form-item>
			<u-form-item label="返工状态">
				<u-input v-model="form.reworkProcessName" @click="showSel('ReworkStatus')" type="text" disabled
					placeholder="请选择返工状态" border />
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">返工任务</u-divider>
		</view>
		<view style="height: 630rpx;">
			<!-- <u-checkbox v-show="reworkList.length>0" v-model="chkAll" @change="chkAllChange"><text
					class="u-font-14">全选</text></u-checkbox> -->
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in reworkList">
						<!-- <u-checkbox v-model="item.Checked" style="width:100%;"> -->
						<u-collapse-item :index="item.Id" class="u-collapse-item" @change="chkItem">
							<template slot="title">
								<text
									style="font-size: 14px;">{{item.ProductOrder}}-{{item.ContainerNO}}&#12288DCTWG-D-17321-D04/1</text>
							</template>
							<view>返工托数：{{item.PalletQty}}</view>
							<view>指定返工工序：{{item.ReworkProcessName}}</view>
							<view>返工状态：{{item.StatusName}}</view>
						</u-collapse-item>
						<!-- </u-checkbox> -->

					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<view class="" style="width:98%;margin-top:15rpx;padding-left: 10px;font-size: 22rpx;margin-bottom: 15rpx;">
			<view class="btn">
				<u-button @click="SelDeail" :size="'medium'" type="primary" :ripple="true" ripple-bg-color="#138087"
					style="font-size: 30rpx; margin-top: 30rpx; width: 45%;">详情查询</u-button>
				<u-button @click="Submit" :size="'medium'" type="primary" :ripple="true" ripple-bg-color="#138087"
					style="font-size: 30rpx; margin-left: 30rpx; width: 45%;">返工报工</u-button>
			</view>
		</view>
		<!-- 工序选择 -->
		<u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
		<!--返工状态-->
		<u-select v-model="showReworkStatus" @confirm="changeReworkStatus" :list="ReworkStatusList"></u-select>
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
		commonMixin
	} from '@/common/mixin/mixin.js'
	import datePicker from '@/components/timePicker/datePicker.vue'
	import scanCode from '@/components/scanCode/scanCode.vue'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			datePicker,
			scanCode

		},
		data() {
			return {
				form: {
					CardCode: "", //流转卡编码
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					ReworkStatusCode: "",
					ReworkStatusName: "",
					startDate: "",
					closeDate: "",
					id: "",
				},
				chkAll: false,
				//工序列表
				processList: [],
				//工序是否显示弹窗
				showProcess: false,
				showReworkStatus: false,
				cardList: [],
				ReworkStatusList: [],
				reworkList: [],
			}
		},

		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		//预加载
		onLoad() {
			_self = this;
			this.getProcessList();
			this.getReworkStatusList();
		},
		onShow() {
			// window.scrollTo(0, 0)
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['ReworkBGTaskQuery', 'SavePMStartInfo']),
			...mapActions('common', ['GetProcessModel', 'GetDictionary']),

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
							label: '无'
						}];
					}
				});
			},

			getDate(val) {
				this.form.startDate = val;
				this.Sel();
			},

			getDate1(val) {
				this.form.closeDate = val;
				this.Sel();
			},
			//初始化返工状态列表
			getReworkStatusList() {
				var data = {
					EnCode: "ReworkStatus"
				}
				this.GetDictionary(data).then(res => {
					this.ReworkStatusList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.ReworkStatusList = [];
						} else {
							this.ReworkStatusList = res.resultData;


						}
					} else {
						this.ReworkStatusList = [{
							value: '',
							label: '无'
						}];
					}
				});
			},

			//显示下拉框
			showSel(val, item) {
				if (val == "process")
					this.showProcess = true;
				else if (val == "ReworkStatus")
					this.showReworkStatus = true;

			},
			//选择工序
			changeProcess(val) {
				this.form.ProcessCode = val[0].value; //val[0].label;				
				this.form.ProcessName = val[0].label;
				this.Sel();
			},
			//选择返工状态
			changeReworkStatus(val) {
				this.form.ReworkStatusCode = val[0].value;
				this.form.reworkProcessName = val[0].label;
				this.Sel();
			},


			chkItem(item) {
				this.form.id = item.index;
				console.log(item.index);
			},

			//查询
			Sel() {

				this.reworkList = [];
				let query = {
					queryJson: {
						DutyProcess: this.form.ProcessCode,
						StartTime: this.form.startDate,
						EndTime: this.form.closeDate,
						Status: this.form.ReworkStatusCode,
					}

				};
				uni.showLoading({
					title: "加载中..."
				});
				this.ReworkBGTaskQuery(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						if (res.resultData == null || res.resultData.count == 0) {
							this.reworkList = [];
						} else {
							res.resultData.forEach((item, index) => {
								// this.reworkList.push({
								// 	Checked: false,
								// 	Id: item.Id,
								// 	ProductOrder: item.ProductOrder,
								// 	ContainerNO: item.ContainerNO,
								// 	PalletQty: item.PalletQty,
								// 	ReworkProcess: item.ReworkProcess,
								// 	Status: item.Status,
								// })
								this.reworkList = res.resultData;
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

			//详情查询
			SelDeail(val) {

				uni.navigateTo({
					url: '/pages/ProduceModel/PMReworkTaskSel?id=' + this.form.id,
				});
			},

			Submit(val) {
				uni.navigateTo({
					url: '/pages/ProduceModel/PMReworkBooking?id=' + this.form.id,
				});
			},

			//全选change事件
			chkAllChange(e) {
				if (this.chkAll) {
					this.cardList.forEach(item => {
						item.Checked = true;
					})
				} else {
					this.cardList.forEach(item => {
						item.Checked = false;
					})
				}
			},


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
