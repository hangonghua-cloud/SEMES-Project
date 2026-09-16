<!-- 物料Marking -->
<template>
	<mescroll-body ref="mescrollRef" :down="downOption" :up="upOption" @init="mescrollInit" :auto="false"
		@down="downCallback" @up="upCallback">
		<view class="bodybackcolor">
			<u-toast ref="uToast"></u-toast>
			<view class="" style="padding:0 10rpx 0 10rpx;">
				<view class="" style="width:100%;float:left;">
					<view class="upseach" style="">
						<view class="upseach-seach">
							<u-search v-model="deviceId" @custom="custom" @search="deviceSearceh"
								v-bind:placeholder="SeachTit" placeholder="目标机台 " :focus="true"
								 :show-action="showAction=false">
							</u-search>
						</view>
						<view class="upseach-ico-plan" style="">
							<view class="upseach-ico" style="">
								<u-icon name="scan" size="40" @click="deviceScanQR"></u-icon>
							</view>
						</view>
						<view class="" style="clear:both;"></view>
					</view>
				</view>
				<view class="" style="clear:both;"></view>
			</view>
					
			<view class="" style="padding:0 10rpx 0 10rpx;">
				<view class="" style="width:100%;float:left;">
					<view class="upseach" style="">
						<view class="upseach-seach">
							<u-search v-model="searchmodel" @custom="custom" @search="search"
								v-bind:placeholder="SeachTit" placeholder="条码扫描" :focus="txtfocus"
								:show-action="showAction=false">
							</u-search>
						</view>
						<view class="upseach-ico-plan" style="">
							<view class="upseach-ico" style="">
								<u-icon name="scan" size="40" @click="ScanQR"></u-icon>
							</view>
						</view>
						<view class="" style="clear:both;"></view>
					</view>
				</view>
				<view class="" style="clear:both;"></view>
			</view>
			
			<view>
				<!-- 弹出提示 -->
				<u-top-tips ref="uTips"></u-top-tips>
				<u-toast ref="uToast" />
			</view>
			<div style="width: 90%;margin-left: 3%;margin-top: 20rpx; height: 70%;background-color: #ffffff;">
				<view class=""
					style="font-size: 22rpx ;width:98%;margin-top:5px ; padding-top: 5rpx; padding-left: 10px;">
					<u-form-item label="物料名称">
						<u-input v-model="form.mrName" type="text" placeholder="" />
					</u-form-item>
				</view>
			</div>
			<view class="" style="width:98%;margin-top:15rpx;padding-left: 10px;font-size: 22rpx;margin-bottom: 15rpx;">
				<view class="u-demo-area">
					<u-button @click="Inner" :type="'primary'" style="font-size: 30rpx; margin-top: 30rpx; width: 50%;">确认
					</u-button>
				</view>
			</view>


			<view width="100%" closeable close-icon-size="40" v-for="(item,index) of list" :key='index'>
				<scroll-view class="list" scroll-y="true">

					<view class="item">
						<view>
							<view class="name">物料条码</view>
							<view class="name">{{item.mBarCode}}</view>
						</view>
						<view>
							<view class="name">物料名称</view>
							<view class="name">{{item.mrName}}</view>
						</view>
						<view>
							<view class="name">目标机台</view>
							<view class="name">{{item.deviceId}}</view>
						</view>

					</view>
				</scroll-view>
			</view>

		</view>
		<u-modal v-model="show" @confirm='confirm' :show-cancel-button=true :content="content" :mask-close-able="true"
			confirm-text="出库"></u-modal>
	</mescroll-body>
</template>
<link href="main.css" rel="stylesheet" />

<script>
	import MescrollBody from "@/components/mescroll-uni/mescroll-body.vue";
	import MescrollMixin from "@/components/mescroll-uni/mescroll-mixins.js";
	import {
		mapState,
		mapActions,

	} from 'vuex'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	export default {
		mixins: [MescrollMixin, commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			MescrollBody
		},
		data() {
			const generateData = _ => {
				const data = [];
				for (let i = 1; i <= 15; i++) {
					data.push({
						key: i,
						label: `备选项 ${ i }`,
						disabled: i % 4 === 0
					});
				}
				return data;
			};
			return {
				data: generateData(),
				value: [1, 4],
				page: {
					page: 1,
					limit: 10,
				},
				list: [], // 数据列表
				downOption: {
					auto: false // 不自动加载 (mixin已处理第一个tab触发downCallback)
				},
				upOption: {
					auto: false, // 不自动加载
					noMoreSize: 5, //如果列表已无数据,可设置列表的总数量要大于半页才显示无更多数据;避免列表数据过少(比如只有一条数据),显示无更多数据会不好看; 默认5
					page: {
						num: 0, // 当前页码,默认0,回调之前会加1,即callback(page)会从1开始
						size: 10 // 每页数据的数量
					},
					empty: {
						tip: '~ 空空如也 ~'
					}
				},
				border: false,
				btnloading: false,
				content: "",
				show: false,
				inputheight: '30',
				actionList: [

				],
				item: [],
				filter_ListDeatil: [],
				form: {
					mrCode: "",
					mrName: "",
					number: "",
				},
				params: {},
				//查询条件框
				Selshow: false,
				searchlistshow: false,
				actionShow: false,
				searchmodel: "",
				selectshow: false,
				CallTypeShow: false,
				SeachTit: "",
				deviceId: "",
				//上传控件
				action: uni.getStorageSync('FileHandler'),
				// 预置上传列表
				fileList: [],
				showUploadList: true, //自定义显示样式
				customBtn: false, //预览区域或者上传按钮
				autoUpload: false, //自动上传
				showProgress: true,
				txtfocus:false,
				deletable: true, //删除按钮
				customStyle: false,
				showpo: false, //入库单按钮
				maxCount: 1, //最大上传数量
				lists: [], // 组件内部的文件列表

			}
		},
		onLoad() {},
		methods: {
			...mapActions('WMS', ['CallSaveDemoData', 'GetWorkInProcessBarcodeInfo', 'GetLatestBarCodeInfo',
				'SaveWMS_WorkInProcessOutputStock'
			]),
			...mapActions('common', ['CallGetEntity']),
			mescrollInit() {},
			Selclose() {
				// console.log('close');
			},
			Selopen() {
				// console.log('open');
			},
			custom() {},
			SeachChange(value) {
				this.SeachTit = this.selectlist0[value].text;
				this.search(this.searchmodel);
			},
			search(value) {
				this.clear();
				this.searchmodel = value;
				if (this.searchmodel != "") {
					this.getitem();
				}
			},
			//确认之后关闭
			confirm() {
				uni.showLoading({
					title: '保存中···'
				});
				this.SaveWMS_WorkInProcessOutputStock({
					"mBarcode": this.searchmodel,
					"deviceId": this.deviceId
				}).then(res => {
					uni.hideLoading();
					console.log(JSON.stringify(res));
					if (res.Success) {
						this.$refs.uTips.show({
							title: '' + res.ReturnMsg,
							type: 'success',
							icon: true
						});
						this.list.push({
							"mBarCode": this.searchmodel,
							"mrName": this.form.mrName,
							"deviceId": this.deviceId,
						});
						this.clearall();
					} else {
						this.$refs.uTips.show({
							title: '' + res.ReturnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			getitem() {
				this.GetWorkInProcessBarcodeInfo({
					"mBarcode": this.searchmodel,
				}).then(res => {
					if (res.Success) {
						if (res.ResultData == null || res.ResultData.length == 0) {
							this.form.mrCode = '';
							this.form.mrName = '';
							this.form.number = 0;
						} else {
							this.form.mrCode = res.ResultData.mrCode;
							this.form.mrName = res.ResultData.mrName;
							this.form.number = res.ResultData.number
							console.log(JSON.stringify(res.ResultData));
						}
					} else {
						this.$refs.uToast.show({
							title: '' + res.ReturnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},
			clear() {
				this.searchmodel = "";
				this.lists = [];
			},
			ScanQR() {
				var self = this;
				// 允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search(res.result);
					}
				});
			},
			deviceScanQR() {
				var self = this;
				// 允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.deviceSearceh(res.result);
					}
				});
			},
			deviceSearceh(value) {
				this.deviceId="";
				this.deviceId = value;
				this.txtfocus=true;
			},
			actionClick(index) {
				this.item.CallType = this.actionList[index].value;
				this.item.CallTypeName = this.actionList[index].text;
			},
			Inner() {
				if (this.$u.test.isEmpty(this.searchmodel)) {
					this.$refs.uTips.show({
						title: '条码信息不能为空, 请扫描或输入条码信息!',
						type: 'warning',
						icon: true
					});
					return;
				}
				if (this.$u.test.isEmpty(this.deviceId)) {
					this.$refs.uTips.show({
						title: '目标机台不能为空, 请扫描或输入机台信息!',
						type: 'warning',
						icon: true
					});
					return;
				}
				uni.showLoading({
					title: '保存中···'
				});
				//--------------入库----------------
				this.GetLatestBarCodeInfo({
					"mBarcode": this.searchmodel,
					"mrCode": this.form.mrCode,
				}).then(res => {
					uni.hideLoading();
					if (res.Success) {
						if (res.ResultData == null || res.ResultData.length == 0) {
							this.form.mrCode = '';
							this.form.mrName = '';
							this.form.number = 0;
						} else {
							this.form.mrCode = res.ResultData.entity.Materiel_Code;
							this.form.mrName = res.ResultData.entity.mrName;
							this.form.number = res.ResultData.entity.number;
							this.content = "当前半成品库内存在时间更早的库存" + "\n 存放位置:" + res.ResultData.entity.Location +
								"\n 物料编码:" + res.ResultData
								.entity.Materiel_Code + "\n 物料条码:" + res.ResultData.entity.Barcode +
								"\n 请确认是否继续出库?";
							this.showAlert();
							console.log(JSON.stringify(res.ResultData));
						}
					} else {
						this.confirm();
					}
				});

			},
			showAlert() {
				this.show = true;
			},
			btnClick() {
				//--------------提交----------------
				var ret = "";

				var ImgName = null;
				var ExceptionImg = null;
				var isimg = (this.lists.length > 0 ? true : false);
				if (isimg) {
					this.blobToBase64(this.lists[0].file).then(res => {
						ImgName = this.lists[0].file.name;
						ExceptionImg = res.split(',')[1];
						//	ExceptionImg = res;
						// // 转化后的base64
						// console.log('base64', res)
					})
				}
				var self = this;
				uni.showLoading({
					title: '请稍后···'
				});
			},
			clearall() {
				this.clear();
				this.item = [];
				this.deviceId = "";
				this.form.mrName = "";
			},

			blobToBase64(blob) {
				return new Promise((resolve, reject) => {
					const fileReader = new FileReader();
					fileReader.onload = (e) => {
						resolve(e.target.result);
					};
					// readAsDataURL
					fileReader.readAsDataURL(blob);
					fileReader.onerror = () => {
						reject(new Error('文件流异常'));
					};
				});
			},

			onListChange(lists) {
				//----------------赋值-------------------
				// this.lists = lists;
				// this.$refs.uUpload.upload();

			},
			checkButton() {
				alert: ("123确认事件")
			},
			onSuccess(res, index, lists) {
				if (res.Success) {
					this.lists[index].FilePath = res.Message;
				} else {
					self.$refs.uToast.show({
						title: `保存异常，请重新上传!`,
						type: 'success'
					});
				}

			},
			/*下拉刷新的回调 */
			/* 			downCallback() {
							// 这里加载你想下拉刷新的数据, 比如刷新轮播数据
							// loadSwiper();
							// 下拉刷新的回调,默认重置上拉加载列表为第一页 (自动执行 page.num=1, 再触发upCallback方法 )
							//this.mescroll.resetUpScroll();
						}, */
			/*上拉加载的回调: 其中page.num:当前页 从1开始, page.size:每页数据条数,默认10 */
			/* 			upCallback(page) {
							// if (page.num <= 1) {
							// 	this.list = [];
							// }
							// this.page.page = page.num;
						}, */

			handleUpload() {
				this.$upload(this.uploadSuccess, this.uploadProgress)
			},
			uploadSuccess(res) {
				this.form.payQrcode = res.url
			},
			uploadProgress(res) {
				console.log("上传进度:", res)
			},
			//顶部tab点击
			sideTabClick(side, index) {
				this.sideIndex = index
				this.page.side = side
				this.mescroll.resetUpScroll()
			},
			//顶部tab点击
			coinTabClick(index) {
				this.coinIndex = index;
				this.page.coin = this.fiatCoins[index]
				this.mescroll.resetUpScroll()
			},
			filter() {
				//uni.getSubNVueById('otcFilterDrawer').show('slide-in-right', 200);
			},


		},
	}
</script>

<style lang='scss' scoped>
	.firstarticleInspection {
		width: 100%;
		height: calc(100vh - 44px);

		.header {
			width: 100%;

			.popup {
				padding: 20upx;
			}
		}

		.content {
			position: relative;

			.title {
				position: absolute;
				top: 0;
				width: 100%;
				left: 0;
				z-index: 200;
				background: #fff;
				padding: 10upx;
			}

			.name {
				margin-left: 20upx;
			}
		}

		.nr {
			padding: 20upx;

			.item {
				display: flex;
				position: relative;
				padding: 10upx;
				flex-direction: column;
				box-shadow: 3px 3px 3px #ccc;
				margin-bottom: 10upx;
				border: 1px #ccc solid;
				border-left: 4px solid #138087;

				.items {
					display: flex;
					align-items: center;

					view {
						padding: 10upx 0;
					}

					.name {
						margin-right: 20upx;
					}
				}

				.close {
					height: 90upx;
					width: 100upx;
					display: flex;
					align-items: center;
					justify-content: center;
					position: absolute;
					right: 0upx;
				}
			}
		}
	}

	.list {
		.item {
			padding: 10upx 20upx;
			width: 100%;
			display: flex;
			flex-direction: column;
			justify-content: center;
			box-shadow: 3px 3px 3px #ccc;
			margin-bottom: 10upx;
			border: 1px solid #ccc;
			border-left: 4px solid #138087;
			font-size: 38upx;

			view {
				display: flex;
				padding: 4upx 0;
				flex: 1;
				align-items: center;
			}
		}
	}

	.u-input__input {
		font-size: 20upx !important;
	}
</style>
