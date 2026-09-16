<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">

				<!-- <u-form-item label="流转卡" required>
				    <u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
				        placeholder="流转卡扫描" shape="square" border :show-action="showAction=false">
				    </u-search>
				    <u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				
				
				<u-form-item label="订单柜号">
					<u-input v-model="form.ContainerNO" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item label="工单详情" style="height: auto;margin-bottom: -5px;margin-top: 1px;">
					<scroll-view scroll-y="false" style="border: 1px solid Gainsboro;">
						<view class="label">PO：{{form.CustomerPO}}</view>
						<view class="label">工单类型：{{form.WorkOrderTypeName}}</view>
						<view class="label">客户型号：{{form.CustomerModel}}</view>
						<view class="label">面膜型号：{{form.MMXH}}</view>
						<view class="label u-line-1">规格：{{form.Spec}}</view>
					</scroll-view>
				</u-form-item> -->

				<u-form-item :label="$t('PMRawMBatchUp.MachineCode')" required>
					<u-search v-model="form.MachineCode" @custom="custom" @search="searchCardCode1" @clear="clear1"
						:placeholder="$t('PMRawMBatchUp.MachineCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR1"></u-icon>
				</u-form-item>

				<u-form-item :label="$t('PMRawMBatchUp.RawMaterial')" required>
					<u-search v-model="form.CodeBar" @custom="custom" @search="searchCardCode2" @clear="clear2"
						:placeholder="$t('PMRawMBatchUp.RawMaterial_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus2">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR2"></u-icon>
				</u-form-item>

				<u-form-item :label="$t('PMRawMBatchUp.MaterialName')">
					<u-input v-model="form.MaterialName" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMRawMBatchUp.MaterialCode')">
					<u-input v-model="form.MaterialCode" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMRawMBatchUp.BatchNo')">
					<u-input v-model="form.BatchNo" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMRawMBatchUp.Binding')"
					style="height: auto;margin-bottom: -5px;margin-top: 1px;">
					<scroll-view scroll-y="true" style="border: 1px solid Gainsboro;height:120px;">
						<view class="label u-line-1" v-for="(item, index) in HistoryList">
							{{item.MaterialName}},{{$t('PMRawMBatchUp.BatchNo2')}}:{{item.BatchNo}}
						</view>
					</scroll-view>
				</u-form-item>
				<u-form-item label="">
					<u-button :type="'primary'" :custom-style="{width: '60%',height: '30rpx',borderRadius: '10rpx'}"
						@click="remove" style="margin-left: 0%;margin-top: 10rpx;">
						<text>{{$t('PMRawMBatchUp.CancelBinding')}}</text>
					</u-button>
				</u-form-item>
				<u-form-item :label="$t('PMRawMBatchUp.Record')"
					style="height: auto;margin-bottom: -5px;margin-top: 1px;">
					<scroll-view scroll-y="true" style="border: 1px solid Gainsboro;height:120px;">
						<view class="label u-line-1" v-for="(item, index) in form.bingList">
							{{item.MaterialName}},{{item.MaterialCode}},{{item.BatchNo}}
						</view>
					</scroll-view>
				</u-form-item>
			</u-form>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '41%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMRawMBatchUp.MainBind')}}</text>
			</u-button>
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
	import scanCode from '@/components/scanCode/scanCode.vue'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				form: {
					CardCode: "", //流转卡编码
					ContainerNO: "", //柜号
					CustomerPO: "", //PO号
					WorkOrderType: "", //工单类型
					WorkOrderTypeName: "", //工单类型名称
					CustomerModel: "", //客户型号
					MMXH: "", //面膜型号
					Spec: "", //规格型号
					MachineCode: "", //机台编码
					CodeBar: "", //条码
					MaterialCode: "", //物料编码
					MaterialName: "", //物料名称
					userCode: "",
					userName: "",
					BatchNo: "", //批次号
					bingList: [], //绑定记录
				},
				HistoryList: [],
				rules: {
					code: [{
						required: true,
						message: '请输入姓名',
						trigger: 'blur,change'
					}],
					intro: [{
						min: 5,
						message: '简介不能少于5个字',
						trigger: 'change'
					}]
				},
				//焦点
				focus1: false,
				focus2: false,
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
			_self.setFocus("focus1");
		},
		onShow() {
			// window.scrollTo(0, 0)

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMRawMBatchUp")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['RawMBatchUpMachineScan',
				'RawMBatchUpCodeBarScan', 'RawMBatchUpCodeBarSave', 'RawMBatchUpMachineRemove'
			]),




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
			//扫码事件-机台
			searchCardCode1(value) {
				this.form.MachineCode = value; //机台
				if (this.form.MachineCode != "") {
					this.RawMBatchUpMachine_Scan();
				}
			},
			clear1() {
				this.form.MachineCode = ""; //机台
			},

			//关键件扫描事件
			searchQR2() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCardCode2(res.result);
					}
				});
			},
			//扫码事件-关键件
			searchCardCode2(value) {
				this.form.CodeBar = value; //关键件
				if (this.form.CodeBar != "") {
					this.rawMBatchUpCodeBar_Scan();
				}
			},
			clear2() {
				this.form.CodeBar = ""; //关键件
			},


			//机台扫描方法
			RawMBatchUpMachine_Scan() {
				console.log(this.form.MachineCode)
				let query = {
					MachineCode: this.form.MachineCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.RawMBatchUpMachineScan(query).then(res => {
					console.log(JSON.stringify(res));
					uni.hideLoading();
					if (res.success && res.resultData.length > 0) {
						this.HistoryList = res.resultData;
						_self.setFocus("focus2");
					} else {
						this.HistoryList = [];
					}
					//console.log(JSON.stringify(this.form.MarkingManageId))
				});
			},
			//关键件扫描方法
			rawMBatchUpCodeBar_Scan() {
				let query = {
					codeBar: this.form.CodeBar
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.RawMBatchUpCodeBarScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.MaterialCode = res.resultData.MaterialCode;
						this.form.MaterialName = res.resultData.MaterialName;
						this.form.BatchNo = res.resultData.BatchNo;
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

			save() {
				// if (!this.form.CardCode) {
				// 	this.$refs.uToast.show({
				// 		title: '流转卡编码不能为空',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }
				if (!this.form.MachineCode) {
					this.$refs.uToast.show({
						title: this.$t("PMRawMBatchUp.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.CodeBar) {
					this.$refs.uToast.show({
						title: this.$t("PMRawMBatchUp.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					entity: this.form,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp'
				};
				uni.showLoading({
					title: this.$t("common.saveing")
				});
				this.RawMBatchUpCodeBarSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMRawMBatchUp.MessageTips_3"),
							type: 'success',
							icon: true
						});
						this.form.bingList.push({
							MaterialName: this.form.MaterialName,
							MaterialCode: this.form.MaterialCode,
							BatchNo: this.form.BatchNo
						})
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
			remove() {
				if (!this.form.MachineCode) {
					this.$refs.uToast.show({
						title: this.$t("PMRawMBatchUp.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				var data = {
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					data: this.HistoryList,
				}
				this.RawMBatchUpMachineRemove(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t("PMRawMBatchUp.MessageTips_3"),
							type: 'success',
							icon: true
						});

						this.HistoryList = [];

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
				this.form.CodeBar = "";
				this.form.MaterialCode = "";
				this.form.MaterialName = "";
				this.form.BatchNo = "";
			},
			initFocus() {
				this.focus1 = false
				this.focus2 = false
			},
			setFocus(focusName) {
				_self.initFocus();
				setTimeout(() => {
					this[focusName] = true;
				}, 0)
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

	.label {
		line-height: 20px;
		width: 250px;
	}
</style>