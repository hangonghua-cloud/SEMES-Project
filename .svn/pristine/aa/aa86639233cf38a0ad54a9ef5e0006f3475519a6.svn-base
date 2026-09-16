<template>
	<view class="select">
			<u-input class="input" :select-open="show" border  @click="showOption" disabled="" :placeholder="'请选择'+label" v-model="data.label"  type="select"  />
		<u-select v-model="show" @confirm="confirm" mode="single-column" :defaultValue="defaultVal" :list="list"></u-select>
	</view>
</template>

<script>
	export default {
		props:{
			//文本
			label:{
				default:'',
				type:String
			},
			//默认值
			defaultVal:{
				default:()=>{
					return [0];
				},
				type:Array
			},
			//绑定唯一标识
			id:{
				default:'',
				type:String
			},
			//数据列表
			list:{
				default:[],
				type:Array
			}
		},
		
		data() {
			return {
				show:false,
				data:{
					value:"",
					label:''
				},
			};
		},
		created(){
			this.data = this.list[this.defaultVal[0]]
			this.id?this.$emit('getval',this.data,this.id):this.$emit('getval',this.data)
		},
		methods:{
			showOption(){
				this.show = true;
			},
			confirm(val){
				this.data = val[0]
				this.id?this.$emit('getval',val[0],this.id):this.$emit('getval',val[0])
				
			}
		}
	}
</script>

<style lang="scss" scoped>

</style>
