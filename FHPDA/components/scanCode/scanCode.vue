<template>
	<view class="scanCode">
		<u-input class="input"  :focus='show'  :placeholder-style='placeholderStyle' placeholder="请扫描" v-model="code"  type="serach" @confirm="onenter" border />
		<u-icon name="scan" size="50upx" color="#138087" @click="getCodes"></u-icon>
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
		focus:function(){
		    // 并不能完全禁用软键盘
		},
		
		data() {
			return {
				code:'',
				placeholderStyle:"fontSize:32upx",
				show:false
			};
		},
		mounted() {
			this.show = true;
			setTimeout(function(){
			    uni.hideKeyboard();//隐藏软键盘
			    // plus.key.hideSoftKeybord();
			},250);
		},
		methods: {
			//回车事件
			onenter(){
				this.$emit('getCode', this.code)
				console.log(789)
			},
			//获取结果
			getCodes(val) {
				uni.scanCode({
				    //scanType: ['barCode'],
				    success: function (res) {
						this.code = res.result
				    }
				});
				this.$emit('getCode', res.result)//返回给父组件
			},
		}
	}
</script>

<style lang="scss" scoped>
	.scanCode{
		width: 100%;
		height: 110upx;
		display: flex;
		font-size: 32upx;
		padding:20upx;
		.input{
			margin-right: 10upx;
		}
	}
	
</style>
