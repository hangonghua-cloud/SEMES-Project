<template>
	<view class="container">
		<view style="margin-bottom: 570px">
			<u-form :model="form1" :rules="rules" ref="uForm" label-width="auto">
				<u-form-item :label="$t('PMQualityConfirm.CardCode')" required>
					<u-search v-model="form1.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
						:placeholder="$t('PMQualityConfirm.CardCode_placeholder')" shape="square" border
						:show-action="showAction=false" :focus="focus1">
					</u-search>
					<u-icon name="scan" size="70" @click="searchQR"></u-icon>
				</u-form-item>
			</u-form>
			<view style="height: auto" v-show="form.ReworkOrder">
				<view>{{$t('PMQualityConfirm.ReworkOrder')}}：{{form.ReworkOrder}}</view>
				<view v-show="form.ReworkProductType=='1'">{{$t('PMQualityConfirm.ProductOrder')}}：{{form.ProductOrder}}
				</view>
				<view v-show="form.ReworkProductType=='1'">{{$t('PMQualityConfirm.ContainerNO')}}：{{form.ContainerNO}}
				</view>
				<view v-show="form.ReworkProductType=='2'">{{$t('PMQualityConfirm.WorkOrder')}}：{{form.WorkOrder}}
				</view>
				<view>{{$t('PMQualityConfirm.RePallet')}}：{{form.RePallet}}</view>
				<view>{{$t('PMQualityConfirm.BGPallet')}}：{{form.BGPallet}}</view>
				<view>{{$t('PMQualityConfirm.StatusName')}}：{{form.StatusName}}</view>
				<view>{{$t('PMQualityConfirm.ReworkProcessName')}}：{{form.ReworkProcessName}}</view>
				<view>{{$t('PMQualityConfirm.LastTime')}}：{{form.LastTime}}</view>
				<view>{{$t('PMQualityConfirm.QualityConfirmUser')}}：{{form.QualityConfirmUser}}</view>
				<view>{{$t('PMQualityConfirm.QualityConfirmTime')}}：{{form.QualityConfirmTime}}</view>
			</view>
			<view style="height: 600rpx;">
				<u-table style="margin-top: 20rpx;">
					<u-tr class="u-tr">
						<u-th>{{$t('PMQualityConfirm.CardName')}}</u-th>
						<u-th>{{$t('PMQualityConfirm.Qty')}}</u-th>
						<u-th>{{$t('PMQualityConfirm.BadQty')}}</u-th>
						<u-th>{{$t('PMQualityConfirm.BGUser')}}</u-th>
					</u-tr>
					<u-tr v-for="(item,index) of OptionList" :key="index">
						<u-th>{{item.CardName}}</u-th>
						<u-th>{{item.Qty}}</u-th>
						<u-th>{{item.BadQty}}</u-th>
						<u-th>{{item.BGUser}}</u-th>

					</u-tr>
				</u-table>
			</view>
		</view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="save"
				style="position: fixed;bottom: 30rpx;margin-left: 28%;">{{$t('PMQualityConfirm.SaveBtn')}}
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
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				form1: {
					CardCode: "",
				},
				form: {
					ReworkProductType: "", //返工产品类型
					Id: "",
					ReworkOrder: "",
					ProductOrder: "",
					WorkOrder: "",
					ContainerNO: "",
					ReworkProcess: "",
					ReworkProcessName: "",
					StatusName: "",
					QualityConfirmUser: "",
					QualityConfirmTime: "",
					RePallet: "",
					BGPallet: "",
					ProcessCode: ""
				},
				OptionList: [],
				focus1: true,
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
		onLoad() {
			_self = this;

		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.QualityModel.ProduceModel/PMQualityConfirm")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
			...mapActions('Produce', ['GetReworkRecordDetail', 'ReworkRecordQualityConfirmSave']),

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
				this.form1.CardCode = value; //流转卡
				if (this.form1.CardCode != "") {
					this.reworkCardScan();
				}
			},

			clear() {
				this.form1.CardCode = ""; //流转卡
			},

			reworkCardScan() {
				this.CardList = [];
				let query = {
					cardCode: this.form1.CardCode
				};
				uni.showLoading({
					title: '加载中'
				});
				this.GetReworkRecordDetail(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form = res.resultData.Entity;
						this.OptionList = res.resultData.OptionList;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}

				});
			},

			save() {
				if (this.form.ReworkProductType != "3") {
					if (this.form.Id.length < 1) {
						this.$refs.uToast.show({
							title: '请选择已经完成的返工单！',
							type: 'warning',
							icon: true
						});
						return;
					}
				}

				uni.showLoading({
					title: '加载中'
				});
				let data = {
					Id: this.form.Id,
					ReworkProductType: this.form.ReworkProductType,
					CardCode: this.form1.CardCode,
					ProcessCode: this.form.ProcessCode,
					userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
					userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp',
				};
				this.ReworkRecordQualityConfirmSave(data).then(res => {
					uni.hideLoading();
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
				this.form1.CardCode = "";
				this.form = {
						ReworkProductType: "", //返工产品类型
						Id: "",
						ReworkOrder: "",
						ProductOrder: "",
						ContainerNO: "",
						ReworkProcess: "",
						ReworkProcessName: "",
						StatusName: "",
						QualityConfirmUser: "",
						QualityConfirmTime: "",
						RePallet: "",
						BGPallet: ""
					},
					this.OptionList = [];
			},

			//查询
			sel() {

				uni.navigateTo({
					url: '/pages/ProduceModel/PMRecordSel',
				});

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