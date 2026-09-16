<template>
	<view class="container">
		<view style="margin-bottom: 15%;">
			<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
				<u-form-item :label="$t('PMCreateRework.CardCode')" required>
					<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('PMCreateRework.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
				<u-form-item :label="$t('PMCreateRework.WorkOrder')">
					<u-input v-model="form.WorkOrder" disabled type="text" placeholder="" border class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMCreateRework.ErrorMsg')">
					<u-icon name="plus-circle-fill" size="70rpx" color="#138087" @click="showBadItemList=true"></u-icon>
					<u-icon name="trash-fill" size="70rpx" color="#138087" @click="deleteBadItem"></u-icon>
				</u-form-item>
				<view style="border-bottom:1px solid Gainsboro; padding-left: 10rpx;"
					v-for="(item, index) in badItemDetailList">
					<u-checkbox v-model="item.Checked">
						<view class="label u-line-1 u-border-top">{{item.BadItemName}}</view>
					</u-checkbox>
				</view>
				<u-form-item :label="$t('PMCreateRework.CurrentProcessName')">
					<u-input v-model="form.CurrentProcessName" disabled="" type="text" placeholder="" border
						class="readonly" />
				</u-form-item>
				<u-form-item :label="$t('PMCreateRework.dutyProcessName')">
					<view style="width: 100%;" @click="showSel('dutyprocess')">
						<u-input v-model="form.dutyProcessName" disabled
							:placeholder="$t('PMCreateRework.dutyProcessName_placeholder')" border
							style="pointer-events: none;" />
					</view>
				</u-form-item>
				<u-form-item :label="$t('PMCreateRework.reworkProcessName')">
					<view style="width: 100%;" @click="showSel('reworkprocess')">
						<u-input v-model="form.reworkProcessName" type="text" disabled
							:placeholder="$t('PMCreateRework.reworkProcessName_placeholder')" border
							style="pointer-events: none;" />
					</view>
				</u-form-item>
				<u-form-item :label="$t('PMCreateRework.Remark')"
					style="height: auto;margin-bottom: -5px;margin-top: 1px;">
					<u-input v-model="form.Remark" type="textarea" placeholder="" border :focus="focus2" />
				</u-form-item>
				<u-form-item :label="$t('common.photosUpload')" style="height: auto;">
					<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="40"
						:upload-text="$t('common.chooseTips')"></u-upload>
				</u-form-item>
			</u-form>
			<view style="margin-top:10px;">
				<u-divider halfWidth="100%">{{$t('PMCreateRework.PartyTitle')}}</u-divider>
			</view>
			<view style="height: auto;">
				<u-checkbox v-show="CardList.length>0" v-model="chkAll" @change="chkAllChange"><text
						class="u-font-14">{{$t('common.SelectAll')}}</text></u-checkbox>
				<!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 400rpx;"> -->
				<u-collapse>
					<view v-show="form.ReworkProductType=='1'" style="border:1px solid white"
						v-for="(item, index) in CardList" :disabled="item.CardStatus!='1'">
						<u-checkbox v-model="item.Checked" style="width:100%;">
							<u-collapse-item class="u-collapse-item">
								<template slot="title">
									<text
										style="font-size: 14px;">{{item.CardName}}&#12288{{item.ProcessName}}&#12288{{item.BusinessTypeName}}&#12288{{item.CardStatusName}}
									</text>
								</template>
								<view>{{$t('PMCreateRework.CardCode')}}：{{item.CardCode}}</view>
								<view>{{$t('PMCreateRework.ProductOrder')}}：{{item.ProductOrder}}</view>
								<view>{{$t('PMCreateRework.ContainerNO')}}：{{item.ContainerNO}}</view>
								<view>{{$t('PMCreateRework.BGQty')}}：{{item.BGQty}}</view>
							</u-collapse-item>
						</u-checkbox>
					</view>
					<view v-show="form.ReworkProductType=='2'" style="border:1px solid white"
						v-for="(item, index) in CardList">
						<u-checkbox v-model="item.Checked" style="width:100%;" :disabled="item.BGStatus!='2'">
							<u-collapse-item class="u-collapse-item">
								<template slot="title">
									<text
										style="font-size: 14px;">{{$t('PMCreateRework.CardName')}}：{{item.CardName}}&#12288{{$t('PMCreateRework.BGStatusName')}}：{{item.BGStatusName}}
									</text>
								</template>

							</u-collapse-item>
						</u-checkbox>
					</view>
				</u-collapse>
				<!-- </scroll-view> -->
			</view>
		</view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="save"
				style="position: fixed;bottom: 30rpx;margin-left: 2%;">{{$t('PMCreateRework.SaveBtn')}}
			</u-button>
			<u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
				:custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="sel"
				style="position: fixed;bottom: 30rpx;margin-left: 50%;">{{$t('PMCreateRework.SearchBtn')}}
			</u-button>
		</view>
		<!-- 责任工序选择 -->
		<u-select v-model="showdutyProcess" @confirm="changedutyProcess" :list="dutyprocessList"></u-select>
		<!--返工工序-->
		<u-select v-model="showreworkProcess" @confirm="changereworkProcess" :list="reworkprocessList"></u-select>
		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<u-popup v-model="showBadItemList" mode="right" length="100%">
			<view class="container">
				<view class="item" v-for="(item,index) of badItemList" :key='index'>
					<u-checkbox v-model="item.Checked">
						<view style="border:1px solid white;">
							<view class="name" style="background-color: Gainsboro;padding: 10rpx;width: 300px;">
								{{item.value}}:{{item.label}}
							</view>
						</view>
					</u-checkbox>
				</view>
			</view>
			<view style="height: 9%;"></view>
			<view class="" style="display: flex;">
				<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
					:custom-style="{width: '35%',height: '70rpx',borderRadius: '10rpx'}" @click="close()"
					style="position: fixed;bottom: 30rpx;margin-left: 10%;">{{$t('PMCreateRework.CancelBtn')}}
				</u-button>
				<u-button :type="'primary'" :ripple="true" ripple-bg-color="#00aa00"
					:custom-style="{width: '35%',height: '78rpx',borderRadius: '10rpx'}" @click="addBadItem()"
					style="position: fixed;bottom: 30rpx;margin-left: 52%;">{{$t('PMCreateRework.ConfirmBtn')}}
				</u-button>
			</view>
		</u-popup>
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
	import global from '@/utils/global'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				action: global.FileHandler, //图片上传地址
				filePath: global.FilePath,
				fileList: [], //文件上传列表
				focus1: false,
				focus2: false,
				form: {
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					ReworkProductType: "", //返工产品类型
					CardCode: "", //流转卡编码
					WorkOrder: "", //工单号
					ProcessName: "", //工序名称
					ProcessCode: "", //工序编码
					CurrentProcess: "",
					CurrentProcessName: "",
					dutyProcessCode: "",
					dutyProcessName: "",
					reworkProcessCode: "",
					reworkProcessName: "",
					CardName: "",
					ProductOrder: "",
					ContainerNO: "",
					BGQty: "",
				},
				badItemDetailList: [], //不良信息
				chkAll: false,
				//责任工序列表
				dutyprocessList: [],
				// 返工工序列表
				reworkprocessList: [],
				//工序是否显示弹窗
				showdutyProcess: false,
				showreworkProcess: false,
				CardList: [],
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
				focusBadQty: false,
				showBadItemList: false,
				badItemList: [],
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
			// uni.$on("to-parent", res => {
			// 	console.log("回传");
			// 	this.form2.BadItemCode = res.result.BadItemCode;
			// 	this.form2.BadItemName = res.result.BadItemName;
			// 	_self.addBadItem();
			// 	uni.$off("to-parent");
			// 	_self.setFocus("focusBadQty");
			// });

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.QualityModel.ProduceModel/PMCreateRework")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['ReworkCardScan', 'ReworkSave']),
			...mapActions('common', ['GetProcessModel', 'GetDictionary']),

			//初始化责任工序列表
			getdutyProcessList() {
				if (!this.form.FactoryCode)
					return;

				var data = {
					FactoryCode: this.form.FactoryCode
				}
				this.GetProcessModel(data).then(res => {
					this.dutyprocessList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.dutyprocessList = [];
						} else {
							res.resultData.forEach((item, index) => {
								this.dutyprocessList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
						}
					} else {
						this.dutyprocessList = [{
							value: '',
							label: '无'
						}];
					}
				});
			},

			//初始化返工工序列表
			getreworkProcessList() {
				if (!this.form.FactoryCode)
					return;

				var data = {
					FactoryCode: this.form.FactoryCode
				}
				this.GetProcessModel(data).then(res => {
					this.reworkprocessList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.reworkprocessList = [];
						} else {

							res.resultData.forEach((item, index) => {
								this.reworkprocessList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
						}
					} else {
						this.reworkprocessList = [{
							value: '',
							label: '无'
						}];
					}
				});
			},
			//显示下拉框
			showSel(val, item) {
				if (val == "dutyprocess")
					this.showdutyProcess = true;
				else if (val == "reworkprocess")
					this.showreworkProcess = true;
			},
			//选择责任工序
			changedutyProcess(val) {
				this.form.dutyProcessCode = val[0].value; //val[0].label;				
				this.form.dutyProcessName = val[0].label;
				_self.setFocus("focus2");
			},

			//选择返工工序
			changereworkProcess(val) {
				this.form.reworkProcessCode = val[0].value; //val[0].label;				
				this.form.reworkProcessName = val[0].label;
			},

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
					this.reworkCardScan();
				}
			},

			clear() {
				this.form.CardCode = ""; //流转卡
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

			reworkCardScan() {
				this.CardList = [];
				let query = {
					cardCode: this.form.CardCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.ReworkCardScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.ReworkProductType = res.resultData.ReworkProductType;
						this.form.WorkOrder = res.resultData.WorkOrder;
						this.form.CurrentProcess = res.resultData.CurrentProcess;
						this.form.CurrentProcessName = res.resultData.CurrentProcessName;
						//自制半成品责任工序、返工工序默认当前工序
						if (this.form.ReworkProductType == "2") {
							this.form.dutyProcessCode = this.form.CurrentProcess;
							this.form.dutyProcessName = this.form.CurrentProcessName;
							this.form.reworkProcessCode = this.form.CurrentProcess;
							this.form.reworkProcessName = this.form.CurrentProcessName;
						}
						this.form.FactoryCode = res.resultData.FactoryCode;
						this.form.FactoryName = res.resultData.FactoryName;

						if (res.resultData.CardList == null || res.resultData.CardList.length == 0) {
							this.CardList = [];
						} else {
							this.CardList = res.resultData.CardList;
							this.CardList.forEach(item => {
								this.$set(item, "Checked", false);
							})
							// res.resultData.CardList.forEach((item, index) => {
							// 	this.CardList.push({
							// 		ProductOrder: item.ProductOrder,
							// 		ContainerNO: item.ContainerNO,
							// 		BGQty: item.BGQty,
							// 		CardName: item.CardName, //托盘号
							// 		CardCode: item.CardCode,
							// 	})
							// });
						}
						//改为工艺路线的工序
						// this.getdutyProcessList();
						// this.getreworkProcessList();
						res.resultData.OperationList.forEach((item, index) => {
							this.dutyprocessList.push({
								value: item.OperationCode,
								label: item.OperationName
							});
						});
						res.resultData.OperationList.forEach((item, index) => {
							this.reworkprocessList.push({
								value: item.OperationCode,
								label: item.OperationName
							});
						});

						this.getBadItemList();
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}

				});
			},
			getBadItemList() {
				if (!this.form.FactoryCode)
					return;

				let data = {
					EnCode: 'PoorWorkReport',
					Remark1: this.form.FactoryCode
				}
				this.GetDictionary(data).then(res => {
					if (res.success) {
						this.badItemList = res.resultData;
						this.badItemList.forEach(item => {
							this.$set(item, "Checked", false);
						})
					} else {
						this.badItemList = []
					}
				});
			},

			//全选change事件
			chkAllChange(e) {
				if (this.chkAll) {
					this.CardList.forEach(item => {
						if (this.form.ReworkProductType == "1" && item.CardStatus == "1") {
							item.Checked = true;
						} else if (this.form.ReworkProductType == "2" && item.BGStatus == "2") {
							item.Checked = true;
						}
					})
				} else {
					this.CardList.forEach(item => {
						item.Checked = false;
					})
				}
			},
			//添加不良
			addBadItem() {
				// if (!this.form2.BadItemCode) {
				// 	this.$refs.uToast.show({
				// 		title: '请选择不良项目',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }

				// let filterList = this.badItemDetailList.filter(item => item.BadItemCode == this.form2.BadItemCode);
				// if (filterList.length > 0) {
				// 	this.$refs.uToast.show({
				// 		title: '已存在，不允许重复录入',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }
				// debugger;
				let selectedItems2 = this.badItemList.filter(item => {
					return item.Checked == true;
				});
				if (selectedItems2.length == 0) {
					this.$refs.uToast.show({
						title: this.$t('PMCreateRework.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}

				selectedItems2.forEach(item => {
					let filterList = this.badItemDetailList.filter(t => t.BadItemCode == item.value);
					if (filterList.length == 0) {
						this.badItemDetailList.push({
							Checked: false,
							BadItemCode: item.value,
							BadItemName: item.label,
						});
					}
				});
				this.showBadItemList = false;
				this.badItemList.forEach(item => {
					item.Checked = false;
				})
			},
			//删除不良
			deleteBadItem() {
				this.badItemDetailList = this.badItemDetailList.filter(item => {
					return item.Checked == false;
				});


			},
			save() {

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

				if (this.form.CardCode == "") {
					this.$refs.uToast.show({
						title: this.$t('PMCreateRework.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.dutyProcessName == "") {
					this.$refs.uToast.show({
						title: this.$t('PMCreateRework.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.form.reworkProcessName == "") {
					this.$refs.uToast.show({
						title: this.$t('PMCreateRework.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.badItemDetailList.length == 0) {
					this.$refs.uToast.show({
						title: this.$t('PMCreateRework.MessageTips_5'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let selectedItems = this.CardList.filter(item => {
					return item.Checked == true;
				});
				if (selectedItems.length == 0) {
					this.$refs.uToast.show({
						title: this.$t('PMCreateRework.MessageTips_6'),
						type: 'warning',
						icon: true
					});
					return;
				}

				let data = {
					reworkProductType: this.form.ReworkProductType,
					cardCode: this.form.CardCode,
					currentProcess: this.form.CurrentProcess,
					dutyProcess: this.form.dutyProcessCode,
					reworkProcess: this.form.reworkProcessCode,
					healthTime: this.form.HealthTime,
					inProcessCode: this.form.InProcessCode,
					inProcessName: this.form.InProcessName,
					totalPallet: this.form.TotalPallet,
					remark: this.form.Remark,
					detail: selectedItems,
					badItemDetailList: this.badItemDetailList,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					fileUrl: fileUrl
				};
				uni.showLoading({
					title: this.$t("common.saveing")
				});
				this.ReworkSave(data).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.$refs.uToast.show({
							title: this.$t('PMCreateRework.MessageTips_7'),
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
				this.form.WorkOrder = "";
				this.form.CurrentProcessName = "";
				this.form.dutyProcessName = "";
				this.form.reworkProcessName = "";
				this.form.Remark = "";
				this.CardList = [];
				this.badItemDetailList = [];
				//清空内部上传文件列表
				this.$refs.uUpload.clear();
			},

			//查询
			sel() {

				uni.navigateTo({
					url: '/pages/ProduceModel/PMRecordSel',
				});

			},
			close() {
				this.showBadItemList = false
				// console.log('close');
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

	.u-form-item {
		height: auto;
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