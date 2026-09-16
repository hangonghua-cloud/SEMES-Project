<template>
	<view>
		<u-form :model="form" label-width="auto">
			<u-form-item :label="$t('RawMaterialTransfer.FactoryName')" prop="FactoryName">
				<view @click="showSel('factory')" style="width: 100%;">
					<u-input v-model="form.FactoryName" disabled
						:placeholder="$t('RawMaterialTransfer.Factory_placeholder')" border
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('RawMaterialTransfer.WhsName')" prop="WhsName">
				<view @click="showSel('warehouse')" style="width: 100%;">
					<u-input v-model="form.WhsName" type="text" disabled
						:placeholder="$t('RawMaterialTransfer.Warehouse_placeholder')" border class="readonly"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('RawMaterialTransfer.LocationName')" prop="LocationName">
				<view @click="showSel('location')" style="width: 100%;">
					<u-input v-model="form.LocationName" type="text" disabled
						:placeholder="$t('RawMaterialTransfer.Location_placeholder')" border class="readonly"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('RawMaterialTransfer.BatchNo')">
				<u-search v-model="form.BatchNo" @custom="custom" @search="searchBatchNo" @clear="clearBatchNo"
					:placeholder="$t('RawMaterialTransfer.BatchNo_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('RawMaterialTransfer.TargetQty')">
				<u-input v-model="form.TargetQty" type="number" placeholder="" border :focus="focus2" />
			</u-form-item>
			<u-form-item :label="$t('RawMaterialTransfer.TargetWhsName')" prop="TargetWhsName">
				<view @click="showSel('targetWarehouse')" style="width: 100%;">
					<u-input v-model="form.TargetWhsName" type="text" disabled
						:placeholder="$t('RawMaterialTransfer.Warehouse_placeholder')" border class="readonly"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
			<u-form-item :label="$t('RawMaterialTransfer.TargetLocationName')" prop="TargetLocationName">
				<view @click="showSel('targetLocation')" style="width: 100%;">
					<u-input v-model="form.TargetLocationName" type="text" disabled
						:placeholder="$t('RawMaterialTransfer.Location_placeholder')" border class="readonly"
						style="pointer-events: none;" />
				</view>
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('RawMaterialTransfer.StockInfo')}}</u-divider>
		</view>
		<view style="height: auto;">
			<u-radio-group v-model="radiovalue" @change="radioGroupChange">
				<u-radio @change="radioChange" v-for="(item, index) in stockList" :key="index" :name="item.Id"
					:disabled="item.disabled" style="border-bottom: 1px solid lightgray;width: 140%;">
					<view style="border-bottom: 0px solid lightgray;width: 200%;">
						<view>{{$t('RawMaterialTransfer.MaterialCode')}}:{{item.MaterialCode}}</view>
						<view>{{$t('RawMaterialTransfer.MaterialName')}}:{{item.MaterialName}}</view>
						<view>{{$t('RawMaterialTransfer.Qty')}}:{{item.Qty}}</view>
					</view>
				</u-radio>
			</u-radio-group>
		</view>
		<view class="" style="display: flex;justify-content: center;">
			<u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
				@click="save" style="position: fixed;bottom: 30rpx;">
				<text>{{$t('RawMaterialTransfer.SaveBtn')}}</text>
			</u-button>

		</view>

		<!-- 仓库弹窗 -->
		<u-popup v-model="showWarehouse" mode="right" length="80%">
			<u-form :model="formWarehouse" ref="uForm">
				<u-form-item :label="$t('RawMaterialTransfer.WhsName')">
					<u-input v-model="formWarehouse.WhsName" type="text" border
						:placeholder="$t('RawMaterialTransfer.WhsName_placeholder')" />
				</u-form-item>
			</u-form>
			<view style="display: flex;">
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='return'
					@click="dialogWarehouseClose" size="return">{{$t('RawMaterialTransfer.cancel')}}</u-button>
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='submits'
					@click="warehouseSearch" size="default">{{$t('RawMaterialTransfer.search')}}
				</u-button>
			</view>
			<view class="bottom" style="margin-top: 10px;">
				<scroll-view scroll-y="true" style="height: 800rpx;">
					<view class="item" v-for="(item,index) of warehouseList" @click="changeWarehouse(item)"
						:key='index'>
						<view style="border:1px solid white;">
							<view class="name" style="background-color: Gainsboro;height: 50px;padding: 10rpx;">
								{{item.label}}
							</view>
						</view>
					</view>
				</scroll-view>

			</view>
		</u-popup>
		<!-- 库位弹窗 -->
		<u-popup v-model="showLocation" mode="right" length="80%">
			<u-form :model="formLocation" ref="uForm">
				<u-form-item :label="$t('RawMaterialTransfer.LocationName')">
					<u-input v-model="formLocation.LocationName" type="text"
						:placeholder="$t('RawMaterialTransfer.LocationName_placeholder')" />
				</u-form-item>
			</u-form>
			<view style="display: flex;">
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='return'
					@click="dialogLocationClose" size="return">{{$t('RawMaterialTransfer.cancel')}}</u-button>
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='submits'
					@click="locationSearch" size="default">{{$t('RawMaterialTransfer.search')}}
				</u-button>
			</view>
			<view class="bottom" style="margin-top: 10px;">
				<scroll-view scroll-y="true" style="height: 800rpx;">
					<view class="item" v-for="(item,index) of locationList" @click="changeLocation(item)" :key='index'>
						<view style="border:1px solid white;">
							<view class="name" style="background-color: Gainsboro;height: 50px;padding: 10rpx;">
								{{item.label}}
							</view>
						</view>
					</view>
				</scroll-view>

			</view>
		</u-popup>

		<!-- 工厂选择 -->
		<u-select v-model="showFactory" @confirm="changeFactory" :list="factoryList"></u-select>
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
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				form: {
					FactoryCode: "", //工厂编码
					FactoryName: "", //工厂名称
					WhsCode: "", //仓库编码
					WhsName: "", //仓库名称
					LocationCode: "", //库位编码
					LocationName: "", //库位名称
					Qty: "", //库存数量
					TargetWhsCode: "",
					TargetWhsName: "",
					TargetLocationCode: "",
					TargetLocationName: "",
					TargetQty: "",
				},
				showFactory: false, //工厂弹框
				showWarehouse: false, //仓库弹窗
				formWarehouse: {
					WhsName: "", //仓库名称
				},
				showLocation: false, //库位弹窗
				formLocation: {
					LocationName: "", //库位名称
				},
				factoryList: [], //工厂列表
				warehouseList: [], //仓库列表
				warehouseListFilter: [], //模糊查询用
				locationList: [], //库位列表
				locationListFilter: [], //模糊查询用
				stockList: [], //库存列表
				radiovalue: "",
				warehouseType: "warehouse", //选择仓库类型 warehouse：源仓库 targetWarehouse：调拨仓库
				locationType: "location", //选择仓库类型 location：源库位 targetLocation：调拨仓库

				focus1: false,
				focus2: false,
			}
		},
		//预加载
		onLoad() {
			_self = this;
			this.getFactoryList();

		},
		methods: {
			...mapActions('common', ['GetResourceByLevelCode', 'GetWarehouseByFactory', 'GetListByParentResource']),
			...mapActions('WMS', ['RawMaterialTransferStockQuery', 'RawMaterialTransferSave']),

			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchBatchNo(res.result);
					}
				});
			},
			//条码查询
			searchBatchNo(value) {
				this.form.BatchNo = value;
				if (this.form.BatchNo) {
					this.rawMaterialTransferStockQuery();
				}
			},
			clearBatchNo() {
				this.form.BatchNo = "";
			},

			//显示下拉框
			showSel(val, item) {
				if (val == "factory")
					this.showFactory = true;
				else if (val == "warehouse" || val == 'targetWarehouse') {
					this.warehouseType = val;
					this.showWarehouse = true;
					this.formWarehouse.WhsName = "";
					this.warehouseList = Object.assign([], this.warehouseListFilter);
				} else if (val == "location" || val == "targetLocation") {
					this.locationType = val;
					this.showLocation = true;
					this.formLocation.LocationName = "";
					this.locationList = Object.assign([], this.locationListFilter);
				}
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

							res.resultData.forEach((item, index) => {
								this.factoryList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.form.FactoryCode = res.resultData[0].ResourceCode;
							this.form.FactoryName = res.resultData[0].ResourceName;
							this.getWarehouseList();
						}
					} else {
						this.factoryList = [{
							value: '',
							label: this.$t("common.None")
						}];
					}
				});
			},
			//选择工厂
			changeFactory(val) {

				this.form.FactoryCode = val[0].value;
				this.form.FactoryName = val[0].label;
				//加载仓库
				this.getWarehouseList();
			},
			//仓库弹窗查询按钮
			warehouseSearch() {
				this.warehouseList = this.warehouseListFilter.filter(t => {
					return t.label.indexOf(this.formWarehouse.WhsName) >= 0
				})
			},
			//仓库列表
			getWarehouseList() {
				var data = {
					factoryCode: this.form.FactoryCode
				}
				this.GetWarehouseByFactory(data).then(res => {
					this.warehouseList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.warehouseList = [];
						} else {

							res.resultData.forEach((item, index) => {
								this.warehouseList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.warehouseListFilter = Object.assign([], this.warehouseList);
							this.form.WhsCode = "";
							this.form.WhsName = "";
							this.form.LocationCode = "";
							this.form.LocationName = "";
						}
					} else {
						this.warehouseList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//选择仓库
			changeWarehouse(item) {

				if (this.warehouseType == "warehouse") {
					this.form.WhsCode = item.value;
					this.form.WhsName = item.label;
				} else if (this.warehouseType == "targetWarehouse") {
					this.form.TargetWhsCode = item.value;
					this.form.TargetWhsName = item.label;
				}
				this.showWarehouse = false;

				//加载库位
				this.getLocationList(item.value);
			},
			dialogWarehouseClose() {
				this.showWarehouse = false;
			},

			//库位弹窗查询按钮
			locationSearch() {
				this.locationList = this.locationListFilter.filter(t => {
					return t.label.indexOf(this.formLocation.LocationName) >= 0
				})
			},
			//加载库位
			getLocationList(val) {
				var data = {
					ParentResource: val
				}
				this.GetListByParentResource(data).then(res => {
					this.locationList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.locationList = [];
						} else {

							res.resultData.forEach((item, index) => {
								this.locationList.push({
									value: item.ResourceCode,
									label: item.ResourceName
								});
							});
							this.locationListFilter = Object.assign([], this.locationList);

							if (this.warehouseType == "warehouse") {
								this.form.LocationCode = "";
								this.form.LocationName = "";
							} else {
								this.form.TargetLocationCode = "";
								this.form.TargetLocationName = "";
							}

						}
					} else {
						this.locationList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});
			},
			//选择库位
			changeLocation(item) {

				if (this.locationType == "location") {
					this.form.LocationCode = item.value;
					this.form.LocationName = item.label;
				} else {
					this.form.TargetLocationCode = item.value;
					this.form.TargetLocationName = item.label;
				}

				this.showLocation = false;
			},
			//库位弹窗关闭
			dialogLocationClose() {
				this.showLocation = false;
			},
			//查询库存
			rawMaterialTransferStockQuery() {
				var data = {
					factoryCode: this.form.FactoryCode,
					locationCode: this.form.LocationCode,
					batchNo: this.form.BatchNo
				};
				this.RawMaterialTransferStockQuery(data).then(res => {
					this.stockList = [];
					if (res.success) {
						if (res.resultData == null || res.resultData.length == 0) {
							this.stockList = [];
						} else {
							this.stockList = res.resultData;
							this.stockList.forEach(item => {
								this.$set(item, "disabled", false);
							});
							this.radiovalue="";
						}
					} else {
						this.warehouseList = [{
							value: '',
							label: this.$t('common.None')
						}];
					}
				});

			},
			radioGroupChange(val) {

				console.log('radioGroupChange', val);
				console.log('radioGroupChange', this.radiovalue);
				var row = this.stockList.find(t => t.Id == val);
				this.form.Qty = row.Qty;
			},
			radioChange(val) {
				console.log('radioChange', val);
				console.log('radioChange', this.radiovalue);
			},

			//保存
			save() {
				//仓库不能为空
				if (!this.form.WhsCode) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_1"),
						type: 'warning',
						icon: true
					});
					return;
				}
				//库位不能为空
				if (!this.form.LocationCode) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_2"),
						type: 'warning',
						icon: true
					});
					return;
				}
				//调拨仓库不能为空
				if (!this.form.TargetWhsCode) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_3"),
						type: 'warning',
						icon: true
					});
					return;
				}
				//调拨库位不能为空
				if (!this.form.TargetLocationCode) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_4"),
						type: 'warning',
						icon: true
					});
					return;
				}
				//调拨数量不能为空
				if (!this.form.TargetQty) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_5"),
						type: 'warning',
						icon: true
					});
					return;
				}

				//调拨数量不能大于库存数量
				if (this.form.TargetQty > this.form.Qty) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_6"),
						type: 'warning',
						icon: true
					});
					return;
				}
				//调拨仓库不能和原仓库一致
				if (this.form.WhsCode == this.form.TargetWhsCode) {
					this.$refs.uToast.show({
						title: this.$t("RawMaterialTransfer.MessageTips_8"),
						type: 'warning',
						icon: true
					});
					return;
				}

				uni.showModal({
					title: this.$t('RawMaterialTransfer.modalTitle'),
					cancelText: this.$t("showModal.cancel"),
					confirmText: this.$t("showModal.confirm"),
					content: this.$t('RawMaterialTransfer.modalContent'),
					success: (res) => {
						if (res.confirm) {
							console.log('用户点击确定', this.radiovalue);
					
							var data = {
								KeyValue: this.radiovalue,
								Entity: this.form
							};
							uni.showLoading({
								title: this.$t("RawMaterialTransfer.loading")
							});
							this.RawMaterialTransferSave(data).then(res => {
								uni.hideLoading();
								if (res.success) {
									this.$refs.uToast.show({
										title: this.$t("RawMaterialTransfer.MessageTips_7"),
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
						} else if (res.cancel) {
							console.log('用户点击取消');
							// _self.reset();
						}
					}
				});
			},
			reset() {
				this.form.Qty = 0;
				this.form.TargetQty = "";
				this.stockList = [];
				this.form.radiovalue = "";
				this.form.BatchNo = "";
			},
			//焦点初始化
			initFocus() {
				this.focus1 = false; //批次焦点
			},
			//设置焦点位置
			setFocus(focusName) {
				_self.initFocus();
				setTimeout(() => {
					this[focusName] = true;
				}, 0)
			}

		}
	}
</script>

<style>
	.readonly {
		background-color: Gainsboro;
	}
</style>