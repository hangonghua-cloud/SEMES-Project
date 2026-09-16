<template>
	<view class="maintainAcceptance">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('EpPointInspection.EquipmentId')">
				<u-search v-model="form.EquipmentId" @custom="custom" @search="search" @clear="clear"
					:placeholder="$t('EpPointInspection.EquipmentId_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('EpPointInspection.EquipmentName')">
				<u-input class="readonly" v-model="form.EquipmentName" type="text" disabled border placeholder="" />
			</u-form-item>
			<u-form-item :label="$t('EpPointInspection.CheckTaskName')">
				<view style="width: 100%;" @click="clickSelFun('Check')">
					<u-input v-model="form.CheckTaskName" disabled="" border type="text"
						:placeholder="$t('EpPointInspection.CheckTaskName_placeholder')"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('EpPointInspection.CheckConclusionId')">
				<view style="width: 100%;" @click="clickSelFun('Result')">
					<u-input v-model="form.CheckConclusionId" type="text" disabled="" border
						:placeholder="$t('EpPointInspection.CheckConclusionId_placeholder')"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;">
				<view class="item" v-for="(item, index) in gridList" :key="index">

					<view class="top">
						<u-icon name="coupon-fill" color="#138087" size="30"></u-icon>
						<view class="name">{{item.CheckItemName}}</view>
					</view>
					<view class="bottom ">
						<view class="center">
							<u-row gutter="16" style="margin-top: 24rpx;">
								<u-col span="3">
									<view class="demo-layout bg-purple">
										<view class="name">{{$t('EpPointInspection.CheckItemStandard')}}:
										</view>
									</view>
								</u-col>
								<u-col span="9">
									<view class="demo-layout bg-purple-light ">
										<view class="nr">{{item.CheckItemStandard}}</view>
									</view>
								</u-col>

							</u-row>
							<u-row gutter="16" justify="space-between">
								<u-col span="3">
									<view class="demo-layout bg-purple">
										<view class="name">{{$t('EpPointInspection.CheckResult')}}:
										</view>
									</view>
								</u-col>
								<u-col span="9">
									<view class="demo-layout bg-purple-light">

										<view class="nr" v-if="item.DataType === '2'">
											<u-input v-model="item.CheckResult" type="text" border />
										</view>
										<view class="nr" v-if="item.DataType === '1'">
											<u-input v-model="item.CheckResult" type="number" border />
										</view>
										<view class="nr" v-if="item.DataType > '3'">
											<u-radio-group v-model="item.CheckResult">
												<u-radio v-model="boxitem.checked"
													v-for="(boxitem, boxindex) in item.Options" :key="boxitem.value"
													:name="boxitem.name">{{boxitem.name}}</u-radio>
											</u-radio-group>

										</view>
									</view>
								</u-col>
							</u-row>

						</view>

					</view>
				</view>
			</scroll-view>
			<view>
				<u-top-tips ref="uTips"></u-top-tips>
				<u-toast ref="uToast" />
			</view>
			<homeBtn></homeBtn>

			<view class="u-demo-area" style="text-align: center;">
				<u-button @click="save" :type="'primary'"
					style="font-size: 30rpx; margin-top: 30rpx; width: 50%;">{{$t('EpPointInspection.SaveBtn')}}
				</u-button>
			</view>
		</u-form>

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
	import timePicker from '@/components/timePicker/timePicker.vue'
	import scanCode from '@/components/scanCode/scanCode.vue'
	var _self;
	export default {
		data() {
			return {
				action: global.FileHandler, //图片上传地址
				filePath: global.FilePath,
				fileList: [], //文件上传列表
				form: {
					EquipmentId: '',
					EquipmentName: "",
					CheckTaskId: "",
					CheckTaskName: "",
					CheckConclusionId: "",
					CheckConclusion: "",
					TypeInPerson: ""
				},
				list: 15,
				page: 0,
				gridList: [],
				isShowCheck: false,
				isShowResult: false,
				selectCheck: [],
				selectResult: [],
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
			timePicker,
			scanCode,

		},
		mixins: [commonMixin],
		onLoad: function(option) {
			_self = this;
			_self.setFocus("focus1");

			//console.log(JSON.stringify(this.loginInfo));
			this.GetDictionary({
				"EnCode": "InspectionResult"
			}).then(res => {
				if (res && res.success) {
					this.selectResult = res.resultData;
				}
			})
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.EquipmentModel.EquipmentModel/EpPointInspection")
			});
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('Equipment', ['GetEquipmentCheckByCode', 'SaveEquipmentTackResult']),
			...mapActions('common', ['GetDictionary']),
			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.search(res.result);
					}
				});
			},
			//条码查询
			search(value) {
				this.form.EquipmentId = value;
				if (this.form.EquipmentId != "") {
					this.getCodes(value);
				}
			},
			clear() {
				this.form.EquipmentId = "";
			},
			custom(val) {

			},
			getCodes(val) {
				//根据设备编码获取检测信息
				console.log(val)
				var queryJson = {
					"EquipmentId": val,
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.GetEquipmentCheckByCode(queryJson).then(res => {
					uni.hideLoading();
					if (res && res.success) {
						this.form.EquipmentId = res.resultData.EquipmentId;
						this.form.EquipmentName = res.resultData.EquipmentName;
						this.selectCheck = res.resultData.TaskList
						//默认第一个点检任务
						this.gridList = [];
						this.form.CheckTaskId = this.selectCheck[0].value;
						this.form.CheckTaskName = this.selectCheck[0].label;
						this.gridList = this.selectCheck.find(t => t.value == this.selectCheck[0].value).GridList;

					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},

			//点击事件 判断调用那个下拉框
			clickSelFun(val, item) {
				if (val == "Check") {
					this.isShowCheck = true;
				} else if (val == "Result") {
					this.isShowResult = true;
				}
			},
			//下拉框选择事件
			changeCheckFun(val) {
				this.gridList = [];
				this.form.CheckTaskId = val[0].value; //val[0].label;
				this.form.CheckTaskName = val[0].label;
				this.gridList = this.selectCheck.find(t => t.value == val[0].value).GridList;
			},
			changeResultFun(val) {
				this.form.CheckConclusion = val[0].value; //val[0].label;
				this.form.CheckConclusionId = val[0].label;
			},
			//保存
			save() {
				console.log("save");
				if (!this.form.CheckTaskId) {
					this.$refs.uToast.show({
						title: this.$t('EpPointInspection.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.CheckConclusion) {
					this.$refs.uToast.show({
						title: this.$t('EpPointInspection.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}


				var flag = false;

				this.gridList.some(item => {
					if (!item.CheckResult) {
						flag = true;
						return true;
					}
				})
				if (flag || this.gridList.length < 1) {
					this.$refs.uToast.show({
						title: this.$t('EpPointInspection.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return false;
				}
				//附件
				//通过filter，筛选出上传进度为100的文件(因为某些上传失败的文件，进度值不为100，这个是可选的操作)
				// let files = this.$refs.uUpload.lists.filter(val => {
				// 	return val.progress == 100;
				// })

				// let fileUrl = [];
				// files.forEach(item => {
				// 	fileUrl.push({
				// 		FileName: item.file.name,
				// 		ImgType: item.file.type,
				// 		FilePath: this.filePath + item.response.resultData
				// 	})
				// })

				this.form.TypeInPerson = this.loginInfo.result.UserCode;
				var postData = {
					entity: this.form,
					data: this.gridList,
					// fileUrl: fileUrl
				}
				this.SaveEquipmentTackResult(postData).then(res => {
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('EpPointInspection.MessageTips_4'),
							type: 'success',
							icon: true
						});
						this.form = {
							EquipmentId: '',
							EquipmentName: "",
							CheckTaskId: "",
							CheckTaskName: "",
							CheckConclusionId: "",
							CheckConclusion: "",
							TypeInPerson: ""
						}
						this.gridList = [];
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})

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
			}

		}
	}
</script>

<style lang="scss" scoped>
	.maintainAcceptance {
		padding: 20upx;
		//background: #f9f9f9;
		font-size: 32upx;
		//height: 100vh;
	}

	.readonly {
		background-color: Gainsboro;
	}

	.item {

		display: flex;
		background: #fff;
		padding: 20upx;
		flex-direction: column;
		margin-bottom: 8upx !important;
		align-items: center;
		//box-shadow: 0px 3px 3px #7b7b7b;

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
			}
		}
	}
</style>