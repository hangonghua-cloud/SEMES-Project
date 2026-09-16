<template>
	<view class="maintainAcceptance">

		<u-form-item :label="$t('EqMalfunctionRepair.EquipmentId')">
			<u-search v-model="form.EquipmentId" @custom="custom" @search="search" @clear="clear"
				:placeholder="$t('EqMalfunctionRepair.EquipmentId_placeholder')" shape="square" border
				:show-action="showAction=false" :focus="focus1">
			</u-search>
			<u-icon name="scan" size="70" @click="searchQR"></u-icon>
		</u-form-item>

		<u-form-item :label="$t('EqMalfunctionRepair.EquipmentName')">
			<u-input class="readonly" v-model="form.EquipmentName" type="text" disabled border placeholder="" />
		</u-form-item>
		<u-form-item :label="$t('EqMalfunctionRepair.RepairingTypeName')">
			<view style="width: 100%;" @click="clickSelFun('Check')">
				<u-input v-model="form.RepairingTypeName" disabled="" border
					:placeholder="$t('EqMalfunctionRepair.RepairingTypeName_placeholder')"
					style="pointer-events: none;" />
			</view>
		</u-form-item>
		<u-form-item :label="$t('EqMalfunctionRepair.TypeInPersonName')">
			<u-input v-model="form.TypeInPersonName" class="readonly" disabled
				:placeholder="$t('EqMalfunctionRepair.TypeInPersonName_placeholder')" type="text" border
				@click="Check_Mater" />
			<u-icon name="search" size="70upx" color="#138087" @click="Check_Mater"></u-icon>
		</u-form-item>
		<u-form-item :label="$t('EqMalfunctionRepair.MalfunctionDescription')"
			style="height: auto;margin-bottom: -5px;margin-top: 1px;">
			<u-input v-model="form.MalfunctionDescription" type="textarea" placeholder="" border :focus="focus2" />
		</u-form-item>
		<u-form-item :label="$t('common.photosUpload')">
			<u-upload ref="uUpload" :action="action" :file-list="fileList" :max-count="9"
				:upload-text="$t('common.chooseTips')"></u-upload>
		</u-form-item>
		</u-form>
		<view>
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<homeBtn></homeBtn>
		<u-popup border-radius="10" v-model="show_shd" @close="Upclose()" :mode="Upmode" length="99%"
			:closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<br>
			<br>
			<!-- 滚屏 -->
			<view class="header">

				<u-form :model="form" ref="uForm">
					<u-form-item :label="$t('EqMalfunctionRepair.TypeInPersonName2')">
						<u-input v-model="form.TypeInPersonName" type="text"
							:placeholder="$t('EqMalfunctionRepair.TypeInPersonName2_placeholder')" />
					</u-form-item>

					<!-- 弹出框 -->
					<!-- 		<view>
						<u-toast ref="uToast" />
					</view> -->

				</u-form>
				<view style="display: flex;">
					<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='return' @click="exit"
						size="return">{{$t('EqMalfunctionRepair.CancelBtn')}}</u-button>
					<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='submits'
						@click="SearchUser" size="default">{{$t('EqMalfunctionRepair.SearchBtn')}}
					</u-button>
				</view>
			</view>

			<view class="bottom" style="margin-top: 10px;">
				<scroll-view scroll-y="true" style="height: 800rpx;">
					<view class="item" v-for="(item,index) of UserList" @click="userChange(item)" :key='index'>
						<view style="border:1px solid white;">

							<view class="name" style="background-color: Gainsboro;height: 50px;padding: 10rpx;">
								{{item.Code}}:{{item.Name}}
							</view>




						</view>

					</view>
				</scroll-view>

			</view>
		</u-popup>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('EqMalfunctionRepair.SaveBtn')}}</text>
			</u-button>
		</view>

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
					RepairingTypeName: "",
					RepairingType: "",
					MalfunctionDescription: "",
					TypeInPersonName: "",
					Creator: ""
				},
				list: 15,
				page: 0,
				eqlist: [],
				isShowCheck: false,
				isShowResult: false,
				selectCheck: [],
				selectResult: [],
				show_shd: false,

				Upmode: 'right',
				Upmask: true, // 是否显示遮罩
				Upcloseable: true,
				UpcloseIconPos: 'top-left',
				UserList: [],
				//焦点
				focus1: false,
				focus2: false,
				focus3: false,
				focus4: false,
			};
		},
		filters: {

		},
		components: {
			timePicker,
			scanCode,

		},
		mixins: [commonMixin],
		onLoad: function(option) {
			//console.log(JSON.stringify(this.loginInfo));
			this.GetDictionary({
				"EnCode": "RepairsCategory"
			}).then(res => {
				if (res && res.success) {
					this.selectCheck = res.resultData;
				}
			})
		},
		onShow() {

			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.EquipmentModel.EquipmentModel/EqMalfunctionRepair")
			});
		},
		computed: {
			...mapState('user', ['loginInfo'])
		},
		methods: {
			...mapActions('Equipment', ['GetEquipmentManageByCode', 'SaveEquipmentMalfunctionRepair']),
			...mapActions('common', ['GetDictionary', 'GetUserList']),


			//start 弹窗选择人员*****************
			Check_Mater() {
				//this.code = val
				this.show_shd = !this.show_shd;
			},

			SearchUser() {

				var queryJson = {
					"UserCode": this.form.TypeInPersonName,
				};
				this.GetUserList(queryJson).then(res => {
					if (res && res.success) {
						this.UserList = res.resultData;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				})
			},

			//从查询页面转换回来到主页面上，还有查询明细表
			userChange(val) {

				this.form.Creator = val.Code;
				this.form.TypeInPersonName = val.Name;
				this.show_shd = !this.show_shd;
				_self.setFocus("focus2");
				// this.searchDetail();
			},

			//返回
			exit() {
				this.show_shd = !this.show_shd;
				// uni.navigateBack({
				// 	delta: 1
				// })
			},

			Upclose() {},

			//end 弹窗选择人员*******************

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
					this.GetEquipmentByCode(value);
				}
			},
			clear() {
				this.form.EquipmentId = "";
			},


			GetEquipmentByCode(val) {

				//根据设备编码获取检测信息
				console.log(val)
				var queryJson = {
					"EquipmentId": val,
				};

				this.GetEquipmentManageByCode(queryJson).then(res => {
					if (res && res.success) {
						this.eqlist = res.resultData;
						this.eqlist.forEach(item => {
							this.form.EquipmentId = item.EquipmentId;
							this.form.EquipmentName = item.EquipmentName;
						})
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
				this.form.RepairingType = val[0].value; //val[0].label;
				this.form.RepairingTypeName = val[0].label;

			},
			changeResultFun(val) {
				this.form.Creator = val[0].value; //val[0].label;
				this.form.TypeInPersonName = val[0].label;
			},
			//保存
			save() {

				console.log("save");
				if (!this.form.EquipmentName) {
					this.$refs.uToast.show({
						title: this.$t('EqMalfunctionRepair.MessageTips_1'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.RepairingType) {
					this.$refs.uToast.show({
						title: this.$t('EqMalfunctionRepair.MessageTips_2'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.Creator) {
					this.$refs.uToast.show({
						title: this.$t('EqMalfunctionRepair.MessageTips_3'),
						type: 'warning',
						icon: true
					});
					return;
				}
				if (!this.form.MalfunctionDescription) {
					this.$refs.uToast.show({
						title: this.$t('EqMalfunctionRepair.MessageTips_4'),
						type: 'warning',
						icon: true
					});
					return;
				}

				//通过filter，筛选出上传进度为100的文件(因为某些上传失败的文件，进度值不为100，这个是可选的操作)
				let files = this.$refs.uUpload.lists.filter(val => {
					return val.progress == 100;
				})

				let fileUrl = [];
				files.forEach(item => {
					fileUrl.push({
						FileName: item.file.name,
						ImgType: item.file.type,
						FilePath: this.filePath + item.response.resultData
					})
				})

				let postData = {
					entity: this.form,
					fileUrl: fileUrl
				}
				this.SaveEquipmentMalfunctionRepair(postData).then(res => {
					if (res && res.success) {
						this.$refs.uToast.show({
							title: this.$t('EqMalfunctionRepair.MessageTips_5'),
							type: 'success',
							icon: true
						});
						this.form = {
							EquipmentId: '',
							EquipmentName: "",
							RepairingTypeName: "",
							RepairingType: "",
							MalfunctionDescription: "",
							TypeInPerson: "",
							TypeInPersonName: "",
							Creator: ""
						}
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

	.wrap {
		margin-top: 24rpx;
	}
</style>