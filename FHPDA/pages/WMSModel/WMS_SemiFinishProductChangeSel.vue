<template>

	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('WMS_SemiFinishProductChangeSel.FactoryName')" prop="FactoryName">
				<u-input v-model="form.FactoryName" @click="showSel('factory')" type="text" disabled
					:placeholder="$t('WMS_SemiFinishProductChangeSel.FactoryName_placeholder')" border />
				</u-button>
			</u-form-item>
			<u-form-item :label="$t('WMS_SemiFinishProductChangeSel.Begintime')">
				<u-input v-model="form.Begintime" disabled
					:placeholder="$t('WMS_SemiFinishProductChangeSel.Begintime_placeholder')" type="text" border />
				<date-picker @getTime="getDate"></date-picker>
			</u-form-item>

			<u-form-item :label="$t('WMS_SemiFinishProductChangeSel.WhsName')">
				<u-input v-model="form.WhsName" @click="showSel('ck')" type="text" disabled
					:placeholder="$t('WMS_SemiFinishProductChangeSel.WhsName_placeholder')" border />
			</u-form-item>
			<u-form-item :label="$t('WMS_SemiFinishProductChangeSel.CardCode')">
				<u-search v-model="form.CardCode" @custom="custom" @search="searchCardCode" @clear="clear"
					:placeholder="$t('WMS_SemiFinishProductChangeSel.CardCode_placeholder')" shape="square" border
					:show-action="showAction=false">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('WMS_SemiFinishProductChangeSel.InWareHouseInfo')}}</u-divider>
		</view>
		<view style="height: 630rpx;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in gridList">
						<u-collapse-item class="u-collapse-item">
							<template slot="title">
								<text
									style="font-size: 14px;">{{$t('WMS_SemiFinishProductChangeSel.ContainerNO')}}：{{item.ContainerNO}}</text>
								<text
									style="font-size: 14px;">{{$t('WMS_SemiFinishProductChangeSel.MaterialCode')}}：{{item.MaterialCode}}</text>
								<!-- <text style="font-size: 14px;">报工数量：{{item.Qty}}</text> -->
								<!-- <text style="font-size: 14px;">不良数量：{{item.BadQty}}</text> -->
							</template>
							<view>{{$t('WMS_SemiFinishProductChangeSel.MarkCode')}}：{{item.MarkCode}}</view>
							<view>{{$t('WMS_SemiFinishProductChangeSel.ProductOrder')}}：{{item.ProductOrder}}</view>
							<view>{{$t('WMS_SemiFinishProductChangeSel.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view>{{$t('WMS_SemiFinishProductChangeSel.MaterialCode')}}：{{item.MaterialCode}}</view>
							<view>{{$t('WMS_SemiFinishProductChangeSel.LocationCode')}}：{{item.LocationCode}}</view>
							<view>{{$t('WMS_SemiFinishProductChangeSel.Creator')}}：{{item.Creator}}</view>
							<view>{{$t('WMS_SemiFinishProductChangeSel.CreateTime')}}：{{item.CreateTime}}</view>
						</u-collapse-item>
					</view>
				</u-collapse>
			</scroll-view>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="search()" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('WMS_SemiFinishProductChangeSel.SearchBtn')}}</text>
			</u-button>
		</view>
		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"></u-select>
		<!-- 责任工序选择 -->
		<u-select v-model="showck" @confirm="changeWarehouse" :list="ckList"></u-select>
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
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
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
					WhsCode: "",
					WhsName: "",
				},
				ckList: [],
				showck: false,
				factoryList: [], //工厂列表
				showFactory: false, //工厂弹窗
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
			_self.getFactoryList();
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.WMSModel.WMSModel/WMS_SemiFinishProductChangeSel")
			});
		},


		methods: {
			...mapActions('WMS', ['SemiProductMoveQuery']),
			...mapActions('common', ['GetResourceByLevelCode', 'GetWarehouseByFactory']),

			getDate(val) {
				this.form.Begintime = val;
			},
			getDate1(val) {
				this.form.Endtime = val;
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
				this.form.CardCode = value; //唛头码 
			},

			clear() {
				this.form.MarkCode = ""; //唛头码
			},

			//显示下拉框
			showSel(val, item) {
				if (val == "factory")
					this.showFactory = true;
				else if (val == "ck")
					this.showck = true;
			},
			//选择工厂
			changeFactory(val) {
				this.form.FactoryCode = val[0].value; //val[0].label;				
				this.form.FactoryName = val[0].label;
				_self.getckList();
			},
			//选择仓库
			changeWarehouse(val) {
				this.form.WhsCode = val[0].value; //val[0].label;				
				this.form.WhsName = val[0].label;

			},
			//初始化工厂列表
			getFactoryList() {
				var data = {
					LevelCode: "Factory"
				}
				this.GetResourceByLevelCode(data).then(res => {
					this.factoryList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.factoryList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.factoryList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.form.FactoryCode = res.resultData[0].ResourceCode;
							this.form.FactoryName = res.resultData[0].ResourceName;
							_self.getckList();
						}
					} else {
						this.factoryList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//初始化仓库列表
			getckList() {
				var data = {
					factoryCode: this.form.FactoryCode
				}
				this.GetWarehouseByFactory(data).then(res => {
					this.ckList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.ckList = [];
						} else {
							console.log(JSON.stringify(res.resultData));
							res.resultData.forEach((item, index) => {
								this.ckList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.form.WhsCode = "";
							this.form.ProcessName = "";
						}
					} else {
						this.ckList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},

			//查询
			search() {

				var query = {
					queryJson: {
						"CreateTime": this.form.Begintime,
						"WhsCode": this.form.WhsCode,
						"CardCode": this.form.CardCode,
					}
				}
				this.SemiProductMoveQuery(query).then(res => {
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