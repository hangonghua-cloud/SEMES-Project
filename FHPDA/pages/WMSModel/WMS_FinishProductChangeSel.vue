<template>

	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">

			<u-form-item :label="$t('WMS_FinishProductChangeSel.Begintime')">
				<u-input v-model="form.Begintime" disabled
					:placeholder="$t('WMS_FinishProductChangeSel.Begintime_placeholder')" type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item>
			<u-form-item :label="$t('WMS_FinishProductChangeSel.MarkCode')">
				<u-input v-model="form.MarkCode" type="text" placeholder="" border />
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('WMS_FinishProductChangeSel.InWareHouseInfo')}}</u-divider>
		</view>
		<view style="height: 630rpx;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text
									style="font-size: 14px;">{{$t('WMS_FinishProductChangeSel.ContainerNO')}}：{{item.ContainerNO}}</text>
								<text
									style="font-size: 14px;">{{$t('WMS_FinishProductChangeSel.MaterialCode')}}：{{item.MaterialCode}}</text>
								<!-- <text style="font-size: 14px;">报工数量：{{item.Qty}}</text> -->
								<!-- <text style="font-size: 14px;">不良数量：{{item.BadQty}}</text> -->
							</template>
							<view>{{$t('WMS_FinishProductChangeSel.MarkCode')}}：{{item.MarkCode}}</view>
							<view>{{$t('WMS_FinishProductChangeSel.ProductOrder')}}：{{item.ProductOrder}}</view>
							<view>{{$t('WMS_FinishProductChangeSel.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view>{{$t('WMS_FinishProductChangeSel.MaterialCode')}}：{{item.MaterialCode}}</view>
							<view>{{$t('WMS_FinishProductChangeSel.LocationCode')}}：{{item.LocationCode}}</view>
							<view>{{$t('WMS_FinishProductChangeSel.Creator')}}：{{item.Creator}}</view>
							<view>{{$t('WMS_FinishProductChangeSel.CreateTime')}}：{{item.CreateTime}}</view>
						</u-collapse-item>
					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_FinishProductChangeSel.SearchBtn')}}</text>
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

					ProductOrder: "",
					MaterialCode: "",
					Qty: "",
					BadQty: "",
					ContainerNO: "",
					CustomerPO: "",
					MarkCode: "",
					Begintime: "",
					Endtime: "",
					Creator: "",
					processList: [],
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
				title: this.$t("menu.WMSModel.WMSModel/WMS_FinishProductChangeSel")
			});
		},

		methods: {
			...mapActions('WMS', ['ProductMoveQuery']),
			...mapActions('common', ['GetDictionary', 'GetProcessModel']),

			getDate(val) {
				this.form.Begintime = val;
			},
			getDate1(val) {
				this.form.Endtime = val;
			},



			//查询
			search() {

				var query = {
					queryJson: {
						"StartTime": this.form.Begintime,
						"MarkCode": this.form.MarkCode,
					}
				}
				this.ProductMoveQuery(query).then(res => {
					if (res && res.success) {
						this.gridList = [];
						if (res.resultData == null || res.resultData.length == 0) {
							this.gridList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.gridList.push({
									MarkCode: item.MarkCode,
									ProductOrder: item.ProductOrder,
									ContainerNO: item.ContainerNO,
									MaterialCode: item.MaterialCode,
									LocationCode: item.LocationCode,
									Creator: item.Creator,
									CreateTime: item.CreateTime,
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