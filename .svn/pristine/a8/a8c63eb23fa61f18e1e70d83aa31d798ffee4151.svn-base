<template>
	
	<view class="container">
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">入库信息</u-divider>
		</view>
		<view style="height: 630rpx;">	
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
							<u-collapse-item class="u-collapse-item">
								<template slot="title">
									<text style="font-size: 14px;">柜号：{{item.ContainerNO}}</text>
									 <text style="font-size: 14px;">客户型号：{{item.MaterialCode}}</text>
									<!-- <text style="font-size: 14px;">报工数量：{{item.Qty}}</text> -->
									<!-- <text style="font-size: 14px;">不良数量：{{item.BadQty}}</text> -->
								</template>
								<view>订单号：{{item.ProductOrder}}</view>
								<view>柜号：{{item.ContainerNO}}</view>
								<view>PO号：{{item.CustomerPO}}</view>
								<view>托号：{{item.CardName}}</view>
								<view>客户型号：{{item.MaterialCode}}</view>
								<view>报工数量：{{item.Qty}}</view>
								<view>不良数量：{{item.BadQty}}</view>
								<view>报工时间：{{item.CreateTime}}</view>
								<view>报工人：{{item.Creator}}</view>
								
							</u-collapse-item>	
					</view>
				</u-collapse>
			</scroll-view>
		</view>	
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search" style="position: fixed;bottom: 30rpx;">
				<text>查询</text>
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
					MaterialCode:"",
					Qty:"",
					BadQty:"",
					ContainerNO: "",
					CustomerPO:"",
					
					Begintime: "",
					StartTime: "",
					
					processList:[],
				},
				showProcess:false,
			gridList:[],
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
			this.getProcessList();
		},
		onShow() {
			// window.scrollTo(0, 0)
		},
		
	
		methods: {
			...mapActions('Produce', ['PackingBGQuery']),
			...mapActions('common', ['GetDictionary','GetProcessModel']),
	
			getDate(val) {
				this.form.Begintime = val;
			},
			getDate1(val) {
				this.form.StartTime = val;
			},
			
		
			
			//查询
			search() {

				var query = {
					queryJson:{
						"ProductOrder":this.form.ProductOrder,
						"ContainerNO": this.form.ContainerNO,
						"CreateTime":this.form.reworkDate,		
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
									Qty:item.Qty,
									CustomerPO: item.CustomerPO,
									CardName: item.CardName,
									BadQty: item.BadQty,
									CreateTime: item.CreateTime,
									Creator: item.Creator,
									SecondResult: item.SecondResult,
									SecondUser: item.SecondUser,
									SecondTime:item.SecondTime,
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
				
				this.gridList=[];
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
