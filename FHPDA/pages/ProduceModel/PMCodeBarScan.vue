<template>
	<view class="container">
		<u-form :model="form" ref="uForm" label-width="auto">
			<u-form-item :label="$t('PMCodeBarScan.CodeBar')">
				<u-search v-model="form.CodeBar" @custom="custom" @search="searchCodeBar" @clear="clear"
					:placeholder="$t('PMCodeBarScan.CodeBar_placeholder')" shape="square" border
					:show-action="showAction=false" :focus="focus1">
				</u-search>
				<u-icon name="scan" size="70" @click="searchQR"></u-icon>
			</u-form-item>
		</u-form>
		<view style="height: auto;">
			<scroll-view scroll-y="true" class="scroll-Y" style="height: 1100rpx;">
				<u-collapse>
					<view style="border:1px solid white" v-for="(item, index) in list">
						<u-collapse-item v-show="type=='1'" class="u-collapse-item"
							:style="{'background-color':item.ClassColor}">
							<template slot="title">
								<text style="font-size: 14px;">
									{{item.CardName}}&#12288{{item.ProcessName}}&#12288{{item.CardStatusName}}&#12288{{item.BusinessTypeName}}&#12288{{item.BGQty}}
								</text>
							</template>
							<view>{{$t('PMCodeBarScan.MMXH')}}：{{item.MMXH}}</view>
							<view>{{$t('PMCodeBarScan.Spec')}}：{{item.Spec}}</view>
							<view>{{$t('PMCodeBarScan.ProductOrder')}}：{{item.ProductOrder}}</view>
							<view>{{$t('PMCodeBarScan.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view style="border-top: 1px solid black;" v-for="(itemD, indexD) in item.bgList">
								<view>
									{{itemD.ProcessName}}&#12288{{itemD.MachineCode}}&#12288{{itemD.Qty}}{{itemD.Unit}}
								</view>
								<view>{{itemD.UserNames}}</view>
								<view>{{formatTime(itemD.CreateTime)}}</view>
							</view>
						</u-collapse-item>
						<u-collapse-item v-show="type=='2'" class="u-collapse-item"
							:style="{'background-color':item.ClassColor}">
							<template slot="title">
								<text style="font-size: 14px;">
									{{item.MarkName}}&#12288{{item.StatusName}}&#12288{{item.WhsName}}&#12288{{item.LocationCode}}&#12288{{item.PieceQty}}/{{item.BoxQty}}
								</text>
							</template>
							<view>{{$t('PMCodeBarScan.ProductOrder')}}：{{item.ProductOrder}}</view>
							<view>{{$t('PMCodeBarScan.ContainerNO')}}：{{item.ContainerNO}}</view>
							<view>{{$t('PMCodeBarScan.MaterialCode')}}：{{item.MaterialCode}}</view>
						</u-collapse-item>
						<u-collapse-item v-show="type=='3'" class="u-collapse-item"
							:style="{'background-color':item.ClassColor}">

							<template slot="title">
								<text style="font-size: 14px;">
									{{item.TransferName}}&#12288{{item.CardStatusName}}&#12288{{item.BGQty}}
								</text>
							</template>
							<view>{{$t('PMCodeBarScan.MaterialCode')}}：{{item.MaterialCode}}</view>
							<view>{{$t('PMCodeBarScan.MaterialName')}}：{{item.MaterialName}}</view>
							<view style="border-top: 1px solid black;" v-for="(itemD, indexD) in item.bgList">
								<view>
									{{itemD.BGProcess}}&#12288{{itemD.BGMachine}}&#12288{{itemD.BGQty}}{{itemD.Unit}}&#12288{{formatTime(itemD.CreateTime)}}
								</view>
								<view>{{itemD.UserGroup}}</view>
							</view>
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
					CodeBar: ""
				},
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
				title: this.$t("menu.ProduceModel.ProduceModel/PMCodeBarScan")
			});
		},
		methods: {
			//参数1 store/modules目录下 文件名, 参数2 文件里方法名
			...mapActions('Produce', ['CodeBarScan']),

			//条码扫描事件
			searchQR() {
				var self = this;
				//允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {
						self.searchCodeBar(res.result);
					}
				});
			},

			//条码查询
			searchCodeBar(value) {
				this.form.CodeBar = value; //流转卡
				if (this.form.CodeBar != "") {
					this.codeBarScan();
				}
			},

			clear() {
				this.form.CodeBar = ""; //流转卡
			},

			codeBarScan() {
				this.list = [];
				let query = {
					codeBar: this.form.CodeBar
				};
				uni.showLoading({
					title: this.$t("common.loading")
				});
				this.CodeBarScan(query).then(res => {
					uni.hideLoading();
					if (res.success) {
						this.type = res.resultData.Type;
						res.resultData.List.forEach(t => {
							t.ClassColor = "Gainsboro";
							if (t.BusinessType == "2" && t.CardStatus != '3') {
								t.ClassColor = "#83ff67"; //完工
							} else if (t.BusinessType == "8" && t.CardStatus != '3') {
								t.ClassColor = "#ff74bc"; //开工
							}
						})
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