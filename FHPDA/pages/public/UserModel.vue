<template>
	<view class="container">
		<u-form :model="form" ref="uForm">
				<u-form-item label="人员信息">
					<u-input v-model="form.Name" type="text" placeholder="请输入人员工号或者姓名" />
				</u-form-item>
			</u-form>
			<view style="display: flex;">
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='return' @click="exit" size="return">返回</u-button>
				<u-button type="primary" :ripple="true" ripple-bg-color="#138087" class='submits' @click="Search" size="default">查询
				</u-button>
			</view>
			<view class="bottom" style="margin-top: 10px;">
				<scroll-view scroll-y="true" style="height: 800rpx;">
					<view class="item" v-for="(item,index) of UserList" @click="SelectChange(item)" :key='index'>
					
			<!-- <u-collapse-item class="u-collapse-item">
				<template slot="title">
					<text style="font-size: 14px;">人员编号：{{item.value}}&#12288人员名称：{{item.label}}</text>
				</template>
			</u-collapse-item> -->
			
			<view style="border:1px solid white;">
				<view class="name" style="background-color: Gainsboro;height: 50px;padding: 10rpx;">人员编号：{{item.value}}&#12288人员名称：{{item.label}}</view>
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
					ProcessBelongName: '',
                     Name:'',
					reworkDate: "",
					processList: [],
				},
				showProcess: false,
				gridList: [],
				UserList:[],
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
		onLoad(options) {
			console.log(options)
			this.SearchUser();
		},
		onShow() {
			// window.scrollTo(0, 0)
		},


		methods: {
			...mapActions('common', ['GetUserList']),
			...mapActions('common', ['GetDictionary', 'GetProcessModel']),

		SearchUser() {
			var queryJson = {
				"UserCode": "",
			};
			this.GetUserList(queryJson).then(res => {
				this.UserList = [];
				if (res && res.success) {
					res.resultData.forEach((item, index) => {
						this.UserList.push({
							value: item.Code,
							label: item.Name
						});
		                this.isShowUser = true;
					});
				} else {
						this.UserList = [];
				}
			})
		},
		
              Search() {
                  var name =this.form.Name;
				this.UserList = this.UserList.filter(t => {
					return t.label.indexOf(this.form.Name) >= 0
				})
			},
			SelectChange(item) {			 
				uni.$emit("to-parent", {
					result: {
						UserCode:item.value,
						UserName:item.label
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
