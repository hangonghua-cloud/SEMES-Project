<template>
	<view class="homeBtn" @click="goHome">
		<u-icon name="home"></u-icon>
	</view>
</template>

<script>
	export default {
		name:"homeBtn",
		data() {
			return {
			};
		},
		methods:{
			goHome(){
				uni.switchTab({
					url: '/pages/index/index'
				})
			}
		}
	}
</script>

<style lang="scss">
	.homeBtn{
		position: fixed;
		top:82%;
		width: 100upx;
		height: 100upx;
		border-radius: 50upx;
		right:20upx;
		background: rgba(0,0,0,0.5);
		display: flex;
		justify-content: center;
		align-items: center;
		img{
			width: 80%;
			height: 80%;
		}
	}
</style>
