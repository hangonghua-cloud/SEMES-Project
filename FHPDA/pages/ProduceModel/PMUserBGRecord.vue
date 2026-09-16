<template>
	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMUserBGRecord.UserCode')">
				<u-search v-model="form.UserCode" @custom="custom" @search="searchCodeBar" @clear="clear"
					:placeholder="$t('PMUserBGRecord.UserCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMUserBGRecord.date')">
				<u-input v-model="form.date" type="select" border @click="clickSelFun('date')"
					:placeholder="$t('PMUserBGRecord.date_placeholder')" />
			</u-form-item>
		</u-form>
		<view style="margin-top: 20rpx;margin-left: 20rpx;" v-if="form1.SumQty">
			<view style="font-size: 14px;">
				{{$t('PMUserBGRecord.SumQty')}}：{{form1.SumQty}}&#12288{{$t('PMUserBGRecord.SumSalary')}}：{{form1.SumSalary}}
			</view>
		</view>
		<u-table style="margin-top: 20rpx; font-size:5px;table-layout:fixed">
			<u-tr>
				<u-th width="25%">{{$t('PMUserBGRecord.PayrollDate')}}</u-th>
				<u-th width="35%">{{$t('PMUserBGRecord.MaterialName')}}</u-th>
				<u-th width="20%">{{$t('PMUserBGRecord.Qty')}}</u-th>
				<u-th width="20%">{{$t('PMUserBGRecord.Salary')}}</u-th>
			</u-tr>
			<u-tr v-for="(item,index) of list" :key="index">
				<u-td width="25%">{{item.PayrollDate}}</u-td>
				<u-td width="35%" class="u-td">{{item.MaterialName}}</u-td>
				<u-td width="20%">{{item.Qty}}</u-td>
				<u-td width="20%">{{item.Salary}}</u-td>
			</u-tr>
		</u-table>

		<!-- <view style="margin-top: 5px;">
			<view style="border:2px solid white;background-color: Gainsboro;padding: 5px;"
				v-for="(item, index) in list">
				<view>岗位：{{item.PostName}}</view>
				<view>物料编码：{{item.MaterialCode}}</view>
				<view>物料名称：{{item.MaterialName}}</view>
				<view>生产小组：{{item.PTeamCode}}</view>
				<view>价格：{{item.Price}}</view>
				<view>报工数量：{{item.Qty}}</view>
				<view>薪资：{{item.Salary}}</view>
				<view>计薪日期：{{item.PayrollDate}}</view>
			</view>
		</view> -->

		<view>
			<!-- 弹出提示 -->
			<u-top-tips ref="uTips"></u-top-tips>
			<u-toast ref="uToast" />
		</view>
		<view style="height: 205rpx;"></view>
		<view class="" style="display: flex;">
			<u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
				:custom-style="{width: '30%',height: '70rpx',borderRadius: '10rpx'}" @click="searchBGRecord"
				style="position: fixed;bottom: 30rpx;margin-left: 33%;">{{$t('PMUserBGRecord.SumQty')}}
			</u-button>

		</view>
		<!-- 日期范围选择 -->
		<u-calendar v-model="isShowDate" :mode="mode" @change="dateChange"></u-calendar>
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
	import selectPicker from '@/components/select/select.vue'
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			timePicker,
			scanCode,
			selectPicker
		},
		data() {
			return {
				form: {
					date: "",
					UserCode: "",
					StartDate: "",
					EndDate: ""
				},
				form1: {
					SumQty: "",
					SumSalary: ""
				},
				isShowDate: false,
				mode: 'range', //时间多选
				seachTit: "",
				type: "",
				list: [],
				//焦点
				focus1: false,
			}
		},

		onReady() {

		},
		//预加载
		onLoad() {
			_self = this;
			_self.setFocus("focus1");
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("menu.ProduceModel.ProduceModel/PMUserBGRecord")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['GetUserBGRecord']),

			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						this.form.UserCode = res.result; //人员
					}
				});
			},
			//人员
			searchCodeBar(value) {
				this.form.UserCode = value; //人员
			},

			clear() {
				this.form.UserCode = ""; //人员
			},

			searchBGRecord() {
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.list = [];
				this.form1 = {
					SumQty: "",
					SumSalary: "",
				}
				this.GetUserBGRecord(this.form).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form1.SumQty = res.resultData.SumQty;
						this.form1.SumSalary = res.resultData.SumSalary;
						this.list = res.resultData.rows;
					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
			},

			//弹出时间控件
			clickSelFun(val, item) {
				if (val == "date") {
					this.isShowDate = true;
				}
			},
			//日期范围回调事件
			dateChange(e) {
				this.form.date = e.startDate + this.$t("common.To") + e.endDate;
				this.form.StartDate = e.startDate;
				this.form.EndDate = e.endDate;
				console.log(e);
			},
			initFocus() {
				this.focus1 = false
			},
			setFocus(focusName) {
				_self.initFocus();
				setTimeout(() => {
					this[focusName] = true;
				}, 0)
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

	.u-collapse-item {
		background-color: Gainsboro;
		width: 320%;

		view {
			background-color: #F0F3FA;
			font-size: 14px;
		}
	}

	.u-td {
		text-overflow: ellipsis;
		/* //省略号 */
		overflow: hidden;
		/* //超出部分隐藏 */
		white-space: nowrap;
		/* //不换行 */
	}
</style>