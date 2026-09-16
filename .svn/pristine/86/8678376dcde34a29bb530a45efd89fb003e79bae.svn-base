<template>
	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMInMachineCard.MachineCode')">
				<u-search v-model="form.MachineCode" @custom="custom" @search="search" @clear="clear"
					:placeholder="$t('PMInMachineCard.MachineCode_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
			<u-form-item :label="$t('PMInMachineCard.MachineName')">
				<u-input v-model="form.MachineName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
			<u-form-item :label="$t('PMInMachineCard.ProcessName')">
				<u-input v-model="form.ProcessName" disabled type="text" placeholder="" border class="readonly" />
			</u-form-item>
		</u-form>
		<view style="margin-top:10px;">
			<u-divider halfWidth="100%">{{$t('PMInMachineCard.CardCodeInfo')}}</u-divider>
		</view>
		<view style="height: auto;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 1100rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in list">
						<u-collapse-item class="u-collapse-item" :style="{'background-color':item.ClassColor}">
							<template slot="title">
								<text style="font-size: 14px;">
									{{item.CardCode}}&#12288{{item.SheetQty}}/{{item.PieceQty}}
								</text>
							</template>
							<view>{{$t('PMInMachineCard.MMXH')}}：{{item.MMXH}}</view>
							<view>{{$t('PMInMachineCard.Spec')}}：{{item.Spec}}</view>
							<view>{{$t('PMInMachineCard.ProductOrder')}}：{{item.ProductOrder}}</view>
							<view>{{$t('PMInMachineCard.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view>{{$t('PMInMachineCard.StartTime')}}：{{formatTime(item.StartTime)}}</view>
						</u-collapse-item>
					</view>
				</u-collapse>
			</scroll-view>
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
	var _self;
	export default {
		mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			scanCode
		},
		data() {
			return {
				form: {
					MachineCode: "", //机台编码
					MachineName: "", //机台名称
					ProcessCode: "", //工序编码
					ProcessName: "" //工序名称
				},
				seachTit: "",
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
				title: this.$t("menu.ProduceModel.ProduceModel/PMInMachineCard")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['InMachineCardQuery']),

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
				this.form.MachineCode = value; //流转卡
				if (this.form.MachineCode) {
					this.codeBarScan();
				}
			},

			clear() {
				this.form.MachineCode = ""; //流转卡
			},

			codeBarScan() {
				this.list = [];
				let query = {
					machineCode: this.form.MachineCode
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.InMachineCardQuery(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.form.MachineName = res.resultData.MachineName;
						this.form.ProcessCode = res.resultData.ProcessCode;
						this.form.ProcessName = res.resultData.ProcessName;
						// res.resultData.List.forEach(t => {
						// 	t.ClassColor = "Gainsboro";
						// 	// if (t.BusinessType == "2" && t.CardStatus != '3') {
						// 	// 	t.ClassColor = "#83ff67"; //完工
						// 	// }
						// })
						console.log(JSON.stringify(res.resultData.List));
						this.list = res.resultData.List;

					} else {
						this.$refs.uToast.show({
							title: '' + res.returnMsg,
							type: 'warning',
							icon: true
						});
					}
				});
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
		/* background-color: Gainsboro; */
		width: 320%;

		view {
			background-color: #F0F3FA;
			font-size: 14px;
		}
	}
</style>