<template>

	<view class="container">
		<u-form :model="form" :rules="rules" ref="uForm" label-width="auto">

			<u-form-item :label="$t('PMPackingSel.ProductOrder')">
				<u-input v-model="form.ProductOrder" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item :label="$t('PMPackingSel.ContainerNO')">
				<u-input v-model="form.ContainerNO" type="text" placeholder="" border />
			</u-form-item>

			<u-form-item :label="$t('PMPackingSel.reworkDate')">
				<u-input v-model="form.reworkDate" disabled :placeholder="$t('PMPackingSel.reworkDate_placeholder')"
					type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PMPackingSel.ReportInfo')}}</u-divider>
		</view>
		<view style="height: 630rpx;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text
									style="font-size: 14px;">{{$t('PMPackingSel.ContainerNO')}}：{{item.ContainerNO}}</text>
								<text
									style="font-size: 14px;">{{$t('PMPackingSel.MaterialCode')}}：{{item.MaterialCode}}</text>
								<!-- <text style="font-size: 14px;">报工数量：{{item.Qty}}</text> -->
								<!-- <text style="font-size: 14px;">不良数量：{{item.BadQty}}</text> -->
							</template>
							<view>{{$t('PMPackingSel.ProductOrder')}}：{{item.ProductOrder}}</view>
							<view>{{$t('PMPackingSel.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view>{{$t('PMPackingSel.CustomerPO')}}：{{item.CustomerPO}}</view>
							<view>{{$t('PMPackingSel.CardName')}}：{{item.CardName}}</view>
							<view>{{$t('PMPackingSel.MaterialCode')}}：{{item.MaterialCode}}</view>
							<view>{{$t('PMPackingSel.Qty')}}：{{item.Qty}}</view>
							<view>{{$t('PMPackingSel.BadQty')}}：{{item.BadQty}}</view>
							<view>{{$t('PMPackingSel.CreateTime')}}：{{item.CreateTime}}</view>
							<view>{{$t('PMPackingSel.Creator')}}：{{item.Creator}}</view>

						</u-collapse-item>
					</view>
				</u-collapse>
			</scroll-view>
		</view>



		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('PMPackingSel.SearchBtn')}}</text>
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
	var _self;
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

					reworkDate: "",


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
			_self = this;
			this.getProcessList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMPackingSel")
			});
		},


		methods: {
			...mapActions('Produce', ['PackingBGQuery']),
			...mapActions('common', ['GetDictionary', 'GetProcessModel']),

			getDate(val) {
				this.form.reworkDate = val;
			},



			//查询
			search() {

				var query = {
					queryJson: {
						"ProductOrder": this.form.ProductOrder,
						"ContainerNO": this.form.ContainerNO,
						"CreateTime": this.form.reworkDate,
					}
				}
				this.PackingBGQuery(query).then(res => {
					if (res && res.success) {
						this.gridList = [];
						if (res.resultData == null || res.resultData.length == 0) {
							this.gridList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.gridList.push({
									ProductOrder: item.ProductOrder,
									ContainerNO: item.ContainerNO,
									MaterialCode: item.MaterialCode,
									Qty: item.Qty,
									CustomerPO: item.CustomerPO,
									CardName: item.CardName,
									BadQty: item.BadQty,
									CreateTime: item.CreateTime,
									Creator: item.Creator,
									SecondResult: item.SecondResult,
									SecondUser: item.SecondUser,
									SecondTime: item.SecondTime,
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
		width: 110%;

		view {
			background-color: #F0F3FA;
			font-size: 14px;
		}
	}
</style>