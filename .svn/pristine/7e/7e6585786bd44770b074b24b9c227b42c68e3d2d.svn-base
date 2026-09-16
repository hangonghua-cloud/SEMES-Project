<template>
	<view class="timePicker">
		<image class="timeImg" @click="changeShow" :src="imgUrl[0]"
		 mode=""></image>
		<u-picker mode="time" @confirm="getTime" v-model="showTime" :params="params"></u-picker>
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
	export default {
		computed: {
			...mapState('common', ['theme'])
		},

		data() {
			return {
				//日期选择范围
				params: {
					year: true,
					month: true,
					day: true,
					hour: false,
					minute: false,
				},
				showTime: false,
				//图标图片
				imgUrl: [
					require('@/static/images/calendar-theme1.png'),
					require('@/static/images/calendar-theme2.png'),
					require('@/static/images/calendar-theme3.png')]
			};
		},
		methods: {
			//切换展示
			changeShow() {
				this.showTime = true
			},
			//获取结果
			getTime(val) {
				let str = `${val.year}-${val.month}-${val.day}`//处理返回的日期格式
				this.$emit('getTime', str)//返回给父组件
			}
		}
	}
</script>

<style lang="scss" scoped>
	.timePicker{
		height: 100%;
		display: flex;
		align-items: center;
		margin-left:20upx;
	}
	.timeImg {
		width: 60upx;
		height: 60upx;
	}
</style>
