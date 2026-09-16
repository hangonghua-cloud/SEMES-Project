<template>
	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">
			<u-form-item label="车牌号">
				<u-input v-model="form.CarNumber" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item label="集装箱ID">
				<u-input v-model="form.ContainerID" placeholder="" type="text" border />
			</u-form-item>

			<u-form-item label="叉车工">
				<u-input v-model="form.ForkliftWorker" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item label="木工">
				<u-input v-model="form.WoodWorker" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item label="封箱号">
				<u-input v-model="form.SealingNo" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item label="备注" style="height: auto;margin-bottom: -5px;margin-top: 1px;">
				<u-input v-model="form.Remark" type="textarea" placeholder="" border />
			</u-form-item>
			<u-form-item label="拍照上传">
				<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="40"></u-upload>
			</u-form-item>

		</u-form>
		<!-- <view style="margin-top:5px;">
			<u-divider halfWidth="100%">成品发货详情</u-divider>
		</view>
		<view style="height: 630rpx;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in ProductDispatchList">

						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text style="font-size: 14px;">唛头码：{{item.MarkCode}}</text>

							</template>
							<view>客户型号：{{item.MaterialCode}}</view>
							<view>库存位置：{{item.LocationCode}}</view>
						</u-collapse-item>

					</view>
				</u-collapse>
			</scroll-view>
		</view> -->
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save()" style="position: fixed;bottom: 30rpx;">
				<text>提交发货</text>
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
	import global from '@/utils/global'
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				action: global.FileHandler, //图片上传地址
				filePath: global.FilePath,
				fileList: [],
				form: {
					CarNumber: "", //车牌号
					ContainerID: "", //集装箱ID
					ForkliftWorker: "", //叉车工
					WoodWorker: "", //木工
					SealingNo: "", //封箱号
					Remark: "", //备注
					CurrentProcessName: "",

				},
				chkAll: false,
				ProductDispatchList: [],
				imgList: [],
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
			}
		},

		onReady() {
			// this.$refs.uForm.setRules(this.rules);
			// this.mescroll.resetUpScroll()
			// this.mescroll.showNoMore()
		},
		//预加载
		onLoad(options) {
			this.Id = options.id;
			this.DocNum = options.DocNum;
			this.ContainerNO = options.ContainerNO;
			this.GetProductDispatchDetailList();
		},

		onNavigationBarButtonTap: function(option) {
			//点击事件
			console.info('点击查询按钮事件', JSON.stringify(option));
			uni.navigateTo({
				url: '/pages/WMSModel/WMS_FinishProductDeliverySel'
			})
		},
		onShow() {
			// window.scrollTo(0, 0)
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('WMS', ['ProductDispatchDetailShow', 'ProductDispatchSave']),
			...mapActions('common', ['GetProcessModel']),


			//初始化成品发货详情列表
			GetProductDispatchDetailList() {
				var data = {
					docNum: this.DocNum,
					containerNO: this.ContainerNO,
				}
				this.ProductDispatchDetailShow(data).then(res => {
					this.ProductDispatchList = [];
					if (res.success) {
						if (res.resultData.dispatchDetailList == null || res.resultData.dispatchDetailList
							.length == 0) {
							this.ProductDispatchList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.dispatchDetailList.forEach((item, index) => {
								this.ProductDispatchList.push({
									MarkCode: item.MarkCode,
									MaterialCode: item.MaterialCode,
									LocationCode: item.LocationCode,

								});
							});
						}
					} else {
						this.ProductDispatchList = [{
							value: '',
							label: '无'
						}];
					}
				});
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


			reworkCardScan() {
				this.CardList = [];
				let query = {
					cardCode: this.form.CardCode
				};
				this.ReworkCardScan(query).then(res => {
					console.log(JSON.stringify(res));
					if (res.success) {
						console.log(JSON.stringify(res.resultData));
						this.form.WorkOrder = res.resultData.WorkOrder;
						this.form.CurrentProcess = res.resultData.CurrentProcess;
						this.form.CurrentProcessName = res.resultData.CurrentProcessName;

						if (res.resultData.CardList == null || res.resultData.CardList.length == 0) {
							this.CardList = [];
						} else {
							res.resultData.CardList.forEach((item, index) => {
								this.CardList.push({
									ProductOrder: item.ProductOrder,
									ContainerNO: item.ContainerNO,
									BGQty: item.BGQty,
									CardName: item.CardName, //托盘号
									CardCode: item.CardCode,
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
					//console.log(JSON.stringify(this.form.MarkingManageId))
				});
			},


			//全选change事件
			chkAllChange(e) {
				if (this.chkAll) {
					this.gridList.forEach(item => {
						item.Checked = true;
					})
				} else {
					this.gridList.forEach(item => {
						item.Checked = false;
					})
				}
			},

			//提交发货
			save() {
				// if (this.form.CardCode == "") {
				// 	this.$refs.uToast.show({
				// 		title: '流转卡编码不能为空',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }

				// if (this.form.dutyProcessName == "") {
				// 	this.$refs.uToast.show({
				// 		title: '请选择责任工序！',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }
				// if (this.form.reworkProcessName == "") {
				// 	this.$refs.uToast.show({
				// 		title: '请选择返工工序！',
				// 		type: 'warning',
				// 		icon: true
				// 	});
				// 	return;
				// }

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



				let data = {
					id: this.Id,
					carNumber: this.form.CarNumber,
					containerID: this.form.ContainerID,
					forkliftWorker: this.form.ForkliftWorker,
					woodWorker: this.form.WoodWorker,
					sealingNo: this.form.SealingNo,
					remark: this.form.Remark,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
					imgList: fileUrl,
				};
				this.ProductDispatchSave(data).then(res => {
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
				this.form.CarNumber = "";
				this.form.ContainerID = "";
				this.form.ForkliftWorker = "";
				this.form.WoodWorker = "";
				this.form.Remark = "";
				this.ProductDispatchList = [];
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

	.u-form-item {
		height: auto;
	}

	.u-form {
		background: #fff;
		padding: 0 20upx 20upx 20upx;
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