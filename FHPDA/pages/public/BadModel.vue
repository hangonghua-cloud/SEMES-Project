<template>
	<view class="container">
		<u-form :model="form" ref="uForm">
			<u-form-item :label="$t('BadModel.BadModelName')">
				<u-input v-model="form.Name" type="text" :placeholder="$t('BadModel.BadModelName_placeholder')" />
			</u-form-item>
		</u-form>
		<view style="display: flex;">
			<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='return' @click="exit"
				size="return">{{$t('BadModel.cancel')}}</u-button>
			<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='submits' @click="Search"
				size="default">{{$t('BadModel.sreach')}}
			</u-button>
		</view>
		<view class="bottom" style="margin-top: 10px;">
			<scroll-view scroll-y="true" style="height: 800rpx;">
				<view class="item" v-for="(item,index) of badItemList" @click="SelectChange(item)" :key='index'>
					<view style="border:1px solid white;">
						<view class="name" style="background-color: Gainsboro;height: 50px;padding: 10rpx;">
							{{item.value}}:{{item.label}}
						</view>
					</view>
				</view>
			</scroll-view>

		</view>
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
					ProcessCode: '',
					BadItemCode: "",
					BadItemName: "",
				},
				badItemList: [],
			};
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
		onLoad(options) {
			console.log(options)
			//this.Search();
			this.form.ProcessCode = options.ProcessCode;
			if (!this.form.ProcessCode)
				return;

			let data = {
				processCode: options.ProcessCode
			}
			this.GetPMProcessBadItem(data).then(res => {

				if (res.success) {
					this.badItemList = res.resultData;
				} else {
					this.badItemList = []
				}
			});
		},
		onShow() {
			// window.scrollTo(0, 0)
			uni.setNavigationBarTitle({ // 修改头部标题
				title: this.$t("BadModel.BadModelTitle")
			});
		},

		methods: {
			...mapActions('Produce', ['GetPMProcessBadItem']),

			Search() {

				this.badItemList = this.badItemList.filter(t => {
					return t.label.indexOf(this.form.Name) >= 0
				})
			},
			SelectChange(item) {

				uni.$emit("to-parent", {
					result: {
						BadItemCode: item.value,
						BadItemName: item.label
					}
				});
				uni.navigateBack()
				//exit();
			},

			//返回页面
			exit() {
				uni.navigateBack()
				// uni.switchTab({
				// 	url: "/pages/index/index",
				// });
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
</style>