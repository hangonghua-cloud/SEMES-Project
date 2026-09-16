<template>
	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('WMS_FinishProductDeliverySel.DocNum')">
				<u-input v-model="form.DocNum" type="text" placeholder="" border />
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishProductDeliverySel.InfoTitle')" style="height: auto;">
				<view style="border: 1px solid Gainsboro;">
					<view class="label">{{$t('WMS_FinishProductDeliverySel.DocNum')}}：{{form.DocNum}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.ProductOrder')}}：{{form.ProductOrder}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.ContainerNO')}}：{{form.ContainerNO}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.CustomerPO')}}：{{form.CustomerPO}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.BoxQty')}}：{{form.BoxQty}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.PalletQty')}}：{{form.PalletQty}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.ContainerID')}}：{{form.ContainerID}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.Operator')}}：{{form.Operator}}</view>
					<view class="label">{{$t('WMS_FinishProductDeliverySel.CreateTime')}}：{{form.CreateTime}}</view>
				</view>
			</u-form-item>

		</u-form>



		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('WMS_FinishProductDeliverySel.InfoTitle2')}}</u-divider>
		</view>
		<view style="height: 630rpx;">

			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text
									style="font-size: 14px;">{{$t('WMS_FinishProductDeliverySel.MarkCode')}}：{{item.MarkCode}}</text>
							</template>
							<view>{{$t('WMS_FinishProductDeliverySel.MarkCode')}}：{{item.MarkCode}}</view>
							<view>{{$t('WMS_FinishProductDeliverySel.MaterialCode')}}：{{item.MaterialCode}}</view>
							<view>{{$t('WMS_FinishProductDeliverySel.LocationCode')}}：{{item.LocationCode}}</view>
						</u-collapse-item>
					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_FinishProductDeliverySel.SearchBtn')}}</text>
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
		formatDate
	} from "@/utils/date.js"; //转换日期格式
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	import datePicker from '@/components/timePicker/datePicker.vue'
	import scanCode from '@/components/scanCode/scanCode.vue'
	export default {
		data() {
			return {
				form: {
					DocNum: "",
					ProductOrder: "",
					ContainerNO: "",
					CustomerPO: "",
					BoxQty: "",
					PalletQty: "",
					ContainerID: "",
					Operator: "",
					CreateTime: "",
				},
				showProcess: false,
				gridList: [],
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

		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.WMSModel.WMSModel/WMS_FinishProductDelivery")
			});
		},


		methods: {
			...mapActions('WMS', ['ProductDispatchQuery']),
			...mapActions('common', ['GetDictionary', 'GetProcessModel']),


			//查询
			search() {
				var query = {
					"docNum": this.form.DocNum,
				}
				this.ProductDispatchQuery(query).then(res => {
					if (res && res.success) {
						this.gridList = [];
						if (res.resultData != null || res.resultData.item.length > 0) {
							this.form.DocNum = res.resultData.item.DocNum;
							this.form.ProductOrder = res.resultData.item.ProductOrder;
							this.form.ContainerNO = res.resultData.item.ContainerNO;
							this.form.CustomerPO = res.resultData.item.CustomerPO;
							this.form.BoxQty = res.resultData.item.BoxQty;
							this.form.PalletQty = res.resultData.item.PalletQty;
							this.form.ContainerID = res.resultData.item.ContainerID;
							this.form.Operator = res.resultData.item.Operator;
							this.form.CreateTime = res.resultData.item.CreateTime;
						}
						if (res.resultData.detail == null || res.resultData.detail.length == 0) {
							this.gridList = [];
						} else {
							console.log(JSON.stringify(res.resultData.detail));
							res.resultData.detail.forEach((item, index) => {
								this.gridList.push({
									MarkCode: item.MarkCode,
									MaterialCode: item.MaterialCode,
									LocationCode: item.LocationCode,
								});
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

				this.gridList = [];
			},


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
</style>