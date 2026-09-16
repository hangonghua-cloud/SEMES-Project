<template>

	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">

			<u-form-item label="返工单号">
				<u-input v-model="form.ReworkOrder" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item label="打返工时间:">
				<u-input v-model="form.CreateTime" disabled type="text" border class="readonly" />
			</u-form-item>
			<u-form-item label="打返工人员">
				<u-input v-model="form.Operator" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item label="返工托数">
				<u-input v-model="form.PalletQty" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>

			<u-form-item label="返工状态">
				<u-input v-model="form.StatusName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">返工托号列表</u-divider>
		</view>
		<view style="height: 550rpx;">

			<scroll-view scroll-y="true" class="scroll-Y" style="height: 520rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in reworkList">
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text style="font-size: 14px;">流转卡号：{{item.CardCode}}</text>
							</template>
							<view>合格数量：{{item.Qty}}</view>
							<view>不良数量：{{item.BadQty}}</view>
							<view>返工人员：{{item.BGUser}}</view>
							<view>返工时间：{{item.CreateTime}}</view>
						</u-collapse-item>
					</view>
				</u-collapse>
			</scroll-view>
		</view>
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
		onLoad(options) {
			_self = this;
			this.id = options.id
			this.getReworkList();
			// this.getReworkStatusList();
		},
		onShow() {
			// window.scrollTo(0, 0)
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['ReworkBGTaskDetailQuery', 'SavePMStartInfo']),
			...mapActions('common', ['GetProcessModel', 'GetDictionary']),

			//初始化明细列表
			getReworkList() {
				var data = {
					id: this.id
				}
				this.ReworkBGTaskDetailQuery(data).then(res => {
					this.reworkList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.reworkList = [];
						} else {
							console.log(JSON.stringify(res.resultData));

							this.form.ReworkOrder = res.resultData.ReworkOrder;
							this.form.CreateTime = res.resultData.CreateTime;
							this.form.Operator = res.resultData.Operator;
							this.form.PalletQty = res.resultData.PalletQty;
							this.form.StatusName = res.resultData.StatusName;
							// res.resultData.TaskDetail.forEach((item, index) => {
							// 	this.reworkList.push({
							// 		CardCode: item.CardCode,
							// 		Qty: item.Qty,
							// 		BadQty:item.BadQty,
							// 		BGUser:item.BGUser,
							// 		CreateTime:item.CreateTime,
							// 	});
							// });

							this.reworkList = res.resultData.TaskDetail;


						}
					} else {
						this.reworkList = [{
							value: '',
							label: '无'
						}];
					}
				});
			},

			getDate(val) {
				this.form.startDate = val;
			},

			getDate1(val) {
				this.form.closeDate = val;
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
			changeReworkStatus(val) {
				this.form.ReworkStatusCode = val[0].value;
				this.form.reworkProcessName = val[0].label;
			},


			//查询
			Sel() {

				this.reworkList = [];
				let queryJson = {
					ProcessCode: this.form.ProcessCode,
					StartTime: this.form.startDate,
					EndTime: this.form.closeDate,
					Status: this.form.ReworkStatusCode,
				};
				uni.showLoading({
					title: "加载中..."
				});
				this.ReworkBGTaskQuery(queryJson).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.reworkList = [];
						if (res.resultData == null || res.resultData.count == 0) {

						} else {
							res.resultData.forEach((item, index) => {
								this.reworkList.push({
									Checked: false,
									ProductOrder: item.ProductOrder,
									ContainerNO: item.ContainerNO,
									PalletQty: item.PalletQty,
									ReworkProcess: item.ReworkProcess,
									Status: item.Status,
								})
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
				this.reworkList = [];
				this.pdList = [];
			},




			//扫码事件
			scanQR(value) {
				this.form.CardCode = value;
				if (this.form.CardCode) {
					this.getPMStartInfo();
				}
			},

			getPMStartInfo() {
				this.cardList = [];
				let query = {
					cardCode: this.form.CardCode
				};
				this.GetPMStartInfo(query).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.TransferBy = res.resultData.TransferBy;
						this.form.TransferByName = res.resultData.TransferByName;
						this.form.HealthTime = res.resultData.HealthTime;
						this.form.InProcessCode = res.resultData.ProcessCode;
						this.form.InProcessName = res.resultData.ProcessName;
						this.form.TotalPallet = res.resultData.TotalPallet;
						this.cardList = res.resultData.CardList;
						this.cardList.forEach(item => {
							this.$set(item, "Checked", false);
						})

					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
					//console.log(JSON.stringify(this.form.MarkingManageId))
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

			save() {
				if (this.form.CardCode == "") {
					this.$refs.uToast.show({
						title: '流转卡编码不能为空',
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.ProcessName == "") {
					this.$refs.uToast.show({
						title: '请选择操作工序！',
						type: 'warning',
						icon: true
					});
					return;
				}
				let selectedItems = this.cardList.filter(item => {
					return item.Checked == true;
				});
				if (selectedItems.length == 0) {
					this.$refs.uToast.show({
						title: '请选择流转卡',
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					processCode: this.form.ProcessCode,
					processName: this.form.ProcessName,
					cardCode: this.form.CardCode,
					transferBy: this.form.TransferBy,
					healthTime: this.form.HealthTime,
					inProcessCode: this.form.InProcessCode,
					inProcessName: this.form.InProcessName,
					totalPallet: this.form.TotalPallet,
					cardList: selectedItems
				};
				this.SavePMStartInfo(data).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						this.$refs.uToast.show({
							title: '保存成功！',
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
				this.form.CardCode = "";
				this.form.ProcessCode = "";
				this.form.ProcessName = "";
				this.form.TransferBy = "";
				this.form.TransferByName = "";
				this.form.HealthTime = "";
				this.form.InProcessCode = "";
				this.form.InProcessName = "";
				this.form.TotalPallet = "";
				this.cardList = [];
			},
		}
	}
</script>

<style>



</style>
