<!-- 越位通知单管理 -->
<template>
	<view class="bodybackcolor">

		<mescroll-body ref="mescrollRef" :down="downOption" :up="upOption" @init="mescrollInit" :auto="false" @down="downCallback"
		 @up="upCallback">

			<div class="u-m-t-10" style="background: white; ">
				<view class="u-demo-area u-flex u-row-center">
					<u-dropdown ref="uDropdown" :activeColor="activeColor" :borderBottom="borderBottom">
						<!-- <u-dropdown-item @change="SeachChange" v-model="form.Seach" title="所在车间" :options="selectlist0"></u-dropdown-item> -->
						<u-dropdown-item @change="WorkShopCodeCange" v-model="form.WorkShopCode" title="所在工序" :options="selectlist1"></u-dropdown-item>
					</u-dropdown>
					<!-- <view style="width: 20%;text-align: center;" @click="ShowHideSel">
						高级筛选
						<u-icon name="list-dot" size="30" style="margin-left: 3rpx;"></u-icon>
					</view> -->
				</view>
			</div>
			<u-toast ref="uToast"></u-toast>
			
					

			<u-action-sheet :list="selectlist0" v-model="searchlistshow" @click="SeachChange" style=" "></u-action-sheet>


			<u-action-sheet :list="actionList" v-model="actionShow" @click="actionClick" style=" "></u-action-sheet>
			<view class="">
				<view class="" style="width:85%;float:left;margin-top:5px ;padding-left: 10px;">
					<view class="" style="margin-top:10rpx;background-color:RGB(242,242,242);border-radius: 20px;">
						<view class="" style="width:90%;float:left;">
							<u-search v-model="searchmodel" @custom="custom" @search="search" v-bind:placeholder="SeachTit" @clear="clear"
							 :show-action="showAction=false">
							</u-search>
						</view>
						<view class="" style="width:10%;float:left;">
							<!-- <image class="" style="width:35rpx;height:35rpx;" src="../../static/scan.png"></image> -->
							<view class="" style="margin:13rpx 15rpx 0 0">
								<u-icon name="scan" size="40"></u-icon>
							</view>
						</view>
						<view class="" style="clear:both;"></view>
					</view>
				</view>
				<view class="" style="width:15%;float:left; " @click="searchlistshow = true">
					<u-icon name="grid-fill" color="Blue" size="50" style="margin:18rpx 0 0 20rpx;"></u-icon>
				</view>
				<view class="" style="clear:both;"></view>
			</view>
			
			<view style="display: block;float: none; width: 10px; clear: both;"></view>
			<view class="coin-section m-t" style="background-color: #F9F9F9;">
				<view v-for="(item, i) in list" :key="item.id" class="block little-line listitem">
					<view class="s-row" style="padding-left: 0px;">
						<table style="width: 100%;">
							<tr>
								<td style="width: 5%; text-align: center;">
									<u-checkbox v-model="item.checklist" style="transform:scale(0.8);margin: 0px; padding: 0px; line-height: 10px;" >
										
									</u-checkbox>
								</td>
								<!-- <td style="width: 10%;;"></td> -->
								<td>
									<view class="s-row s-rowP" style="" @click="itemshowclick(i)">
										<view class="col">
											<image :src="item.icon" class="coinLogo"></image>
											<text class="coin">{{item.BusSN}}</text>
										</view>
										<view class="col r light">
											<u-icon name="arrow-right" size="30" style="margin-left: 3px;" v-if=" testShow != i"></u-icon>
											<u-icon name="arrow-down" size="30" style="margin-left: 3px;" v-if=" testShow == i"></u-icon>
										</view>
									</view>
<view @click="actionShow=true">
									<view class="s-row">
										<view class=" Itemlable">所在工序：</view>
										<view class=" Itemvalue">{{item.WorkShopName }}</view>
									</view>
									<view class="s-row">
										<view class=" Itemlable">销售订单：</view>
										<view class=" Itemvalue">{{item.SalesDisVoucherProNo }}</view>
									</view>
									<view class="s-row">
										<view class=" Itemlable">生产工单：</view>
										<view class=" Itemvalue">{{item.WONo}}</view>
									</view>
</view>

									<!-- <view v-show=" testShow == i ">

										<view class="s-row">
											<view class=" Itemlable">工序：</view>
											<view class=" Itemvalue">{{item.WorkShopName }}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">销售订单：</view>
											<view class=" Itemvalue">{{item.SalesDisVoucherProNo }}</view>
										</view>
										<view class="s-row">
											<view class=" Itemlable">生产工单：</view>
											<view class=" Itemvalue">{{item.WONo}}</view>
										</view>
									</view> -->
								<!-- 	<view class="s-row bottom">
										<view class="more"></view>
										<view class="evaluate btn" v-if="item.OffsideNoticeFilePath != null" @click="ShowHideUp(false)">查看</view>
									</view> -->
								</td>
							</tr>
						</table>
					</view>



					<!-- <view class="s-row" v-if="i==0">
						<view class="col subtitle row-amount">工序</view>
						<view class="col subtitle row-amount">销售订单</view>
						<view class="col subtitle row-amount">生产工单</view>
					</view>

					<view class="s-row">
						<view class="col subtitle row-title">{{item.WorkShopName }}</view>
						<view class="col subtitle row-title">{{item.SalesDisVoucherProNo}}</view>
						<view class="col subtitle row-title">{{item.WONo}}</view>
					</view> -->

				</view>
			</view>


			<!-- 分页的数据列表 -->
		</mescroll-body>
		<u-popup border-radius="10" v-model="Selshow" @close="Selclose" @open="Selopen" :mode="Selmode" length="70%" :mask="Selmask"
		 :closeable="Selcloseable" :close-icon-pos="SelcloseIconPos">

			<view style="background-color:#F9F9F9 ; height: 100%;;">
				<!-- 	<view class="list-cell b-b" hover-class="cell-hover" :hover-stay-time="50">
				<input v-model="form.SalesDisVoucherProNo" placeholder="销售订单号" />
			</view> -->
				<view style="height: 90%; padding-top: 20px;">
					<view style="text-align: center; height: 30px; ">
						筛选条件
					</view>
					<view class="list-cell b-b" hover-class="cell-hover" :hover-stay-time="50">
						<text class="cell-tit">销售订单</text>
						<input v-model="form.SalesDisVoucherProNo" class="cell-input" placeholder="请输入内容" />
					</view>
					<view class="list-cell b-b" hover-class="cell-hover" :hover-stay-time="50">
						<text class="cell-tit">序列号</text>
						<input v-model="form.BusSN" class="cell-input" placeholder="请输入内容" />
					</view>
					<!-- 	<view class="list-cell b-b" hover-class="cell-hover" :hover-stay-time="50">
					<input v-model="form.BusSN" placeholder="整车序列号" />
				</view> -->

				</view>

				<view class="s-row bottom" style="100%">

					<view class="evaluate btn" @click="ShowHideSel" style="width: 100%;margin: 0px 20rpx;">查询</view>
				</view>
			</view>
		</u-popup>


		<u-popup border-radius="10" v-model="Upshow" @close="Upclose" @open="Upopen" :mode="Upmode" length="80%" :mask="Selmask"
		 :closeable="Upcloseable" :close-icon-pos="UpcloseIconPos">
			<view class="u-demo-wrap">
				<view class="u-demo-area" style="margin-top: 40px;">
					<!-- <u-toast ref="uToast"></u-toast>
					<view class="pre-box" >
						<view class="pre-item" v-for="(item, index) in lists" :key="index">
							<image class="pre-item-image" :src="item.url" mode="aspectFill"></image>
							<view class="u-delete-icon" @tap.stop="deleteItem(index)">
								<u-icon name="close" size="20" color="#ffffff"></u-icon>
							</view>
							<u-line-progress v-if="item.progress > 0 && !item.error" :show-percent="false" height="16" class="u-progress"
							 :percent="item.progress"></u-line-progress>
						</view>
					</view> -->
					
					<u-tag  v-for="(item, i) in selectTagList" :key="item.id" :text="item.WONo" :type="type" :shape="shape" :closeable="closeable" :mode="mode"
					@close="tagclose(item.WONo,item.id)" @click="tagclick(item.WONo,item.id)" :show="true"  v-show="imglistshow" />
					
					<u-upload :before-remove="beforeRemove" ref="uUpload" :custom-btn="customBtn" :show-upload-list="showUploadList"
					 :action="action" :auto-upload="autoUpload" :file-list="fileList" :show-progress="showProgress" :deletable="false"
					 :max-count="maxCount" @on-list-change="onListChange">
						<view v-if="customBtn" slot="addBtn" class="slot-btn" hover-class="slot-btn__hover" hover-stay-time="150">
							<u-icon name="photo" size="60" :color="$u.color['lightColor']"></u-icon>
						</view>
					</u-upload>
					<view class="s-row bottom" style="100%">

						<view class="evaluate btn" @click="clear" style="width: 100%;margin: 0px 20rpx;" v-show="false">清空列表</view>
						<view class="evaluate btn" @click="upload" style="width: 100%;margin: 0px 20rpx;" v-show="imglistshow">上传</view>
					</view>
					<!-- <u-button :custom-style="{marginTop: '40rpx'}" @click="clear">清空列表</u-button>
					<u-button :custom-style="{marginTop: '20rpx'}" @click="upload">上传</u-button> -->
					<!-- <u-button :custom-style="{marginTop: '40rpx'}" @click="reUpload">重新上传</u-button> -->
				</view>
			</view>
		</u-popup>
	</view>
</template>
<link href="main.css" rel="stylesheet" />
<script>
	import MescrollBody from "@/components/mescroll-uni/mescroll-body.vue";
	import MescrollMixin from "@/components/mescroll-uni/mescroll-mixins.js";
	import $ from "@/utils/jquery-3.4.1.js";

	import {
		mapState,
		mapActions,

	} from 'vuex'
	import {
		commonMixin
	} from '@/common/mixin/mixin.js'
	export default {
		mixins: [MescrollMixin, commonMixin], // 使用mixin (在main.js注册全局组件)
		components: {
			MescrollBody
		},
		data() {
			return {
				page: {
					page: 1,
					limit: 10,
				},
				list: [], // 数据列表
				downOption: {
					auto: false // 不自动加载 (mixin已处理第一个tab触发downCallback)
				},
				upOption: {
					auto: true, // 不自动加载
					noMoreSize: 5, //如果列表已无数据,可设置列表的总数量要大于半页才显示无更多数据;避免列表数据过少(比如只有一条数据),显示无更多数据会不好看; 默认5
					page: {
						num: 0, // 当前页码,默认0,回调之前会加1,即callback(page)会从1开始
						size: 10 // 每页数据的数量
					},
					empty: {
						tip: '~ 空空如也 ~'
					}
				},
				tablist: [{
						name: '待投产'
					},
					{
						name: '待下线'
					},
					{
						name: '待质检'
					},
					{
						name: '已完成'
					}
				],
				//标签
type:"primary",
shape:"circle",
closeable:true,

selectTagList:[],

				//查询条件框
				Selshow: false,
				Selmode: 'left',
				Selmask: true, // 是否显示遮罩
				Selcloseable: true,
				SelcloseIconPos: "",
				//操作页面框
				Upshow: false,
				Upmode: 'right',
				Upmask: true, // 是否显示遮罩
				Upcloseable: true,
				UpcloseIconPos: "",
				searchpla: "生产工单",
				form: {
					SalesDisVoucherProNo: "",
					BusSN: "",
					WONo: "",
					WorkShopCode: "",
					WorkShopCodeCN: "--请选择工序--",
					payQrcode: "",
					Seach: "",
				},
				up: {
					Ids: null,
					Name: null,
					OffsideNoticeBy: null,
					Contents: null,
				},
				searchlistshow: false,
				actionShow:false,
				searchmodel: "",
				testShow: -1,
				testShowi: -1,
				selectshow: false,
				selectlist0: [{
						text: '整车序列号',

					},
					{
						text: '销售订单',
					},
					{
						text: '生产工单',
					}
				],
				actionList:[{
					text:'查看图片'
				}],
				SeachTit: "",
				defaultseach: 0,
				selectlist1: [{
						label: '车间1',
						value: '车间1',
					},
					{
						label: '车间2',
						value: '车间2',
					},
					{
						label: '车间3',
						value: '车间3',
					}
				],
				borderBottom: false,
				activeColor: '#138087',
				imglistshow: true,

				hoverClass: 'hover2',
				itemStyle: {

				},
				key: true
			}
		},
		onLoad() {

			this.SeachChange(this.defaultseach);
			this.upCallback({
				num: 1,
				page: 10
			});
		},
		onShow() {
			// uni.$on("refresh", (res) => {
			// 	this.mescroll.resetUpScroll()
			// })
			// uni.$on("filter", (res) => {
			// 	this.page.price = res.price
			// 	this.page.payment = res.payment
			// 	this.mescroll.resetUpScroll()
			// })
			// this.$fire.$emit('refreshCoin')
		},
		onUnload() {
			// uni.$off("refresh", (res) => {})
			// uni.$off("filter", (res) => {})
		},
		onNavigationBarButtonTap(e) {
			//下标从右开始
			const index = e.index;
			if (index === 0) {
				//添加
				this.ShowHideUp(true);
			} else if (index === 1) {
				//查询
				this.ShowHideSel();
			}
		},
		methods: {
			...mapActions('common', ['fiatList']),
			...mapActions('otc', ['advertList']),
			...mapActions('QC', ['CallGetEntity']),
			mescrollInit() {

			},
			Selclose() {
				// console.log('close');
			},
			Selopen() {
				// console.log('open');
			},
			ShowHideSel() {

				if (this.Selshow) {
					this.Selshow = false;
					this.upCallback({
						num: 1,
						page: 10
					});
				} else
					this.Selshow = true;
			},
			ShowHideUp(imglistshow) {
				
				this.selectTagList=[];
				for(var i=0;i<this.list.length;i++){
					if(this.list[i].checklist==true){
					this.selectTagList.push({
						WONo:this.list[i].WONo,
						id:this.list[i].id,
					})
					
				}}
				this.Upshow = true;
				this.imglistshow = imglistshow;
			},
			selectlimitTime() {
				let form = this.form
				let array = [5, 10, 15, 20]
				uni.showActionSheet({
					itemList: array,
					success: function(res) {
						form.limitTime = array[res.tapIndex]
					}
				})
			},
			statusChange(index) {
				this.selectshow = !index;
			},
			// searchlistchange(){
			// 	this.searchlistshow =!this.searchlistshow ;
			// },
			SeachChange(value) {

				this.SeachTit = this.selectlist0[value].text;
				this.search(this.searchmodel);
				// for (var i = 0; i < this.selectlist0.length; i++) {
				// 	if (this.selectlist0[i].value == value)
				// 		this.SeachTit = this.selectlist0[i].label;
				// }
			},
			actionClick(value){
				if(value==0)
					this.Selctimgclick();
			},
			WorkShopCodeCange() {
				this.upCallback({
					num: 1,
					page: 10
				});
			},
			Selctimgclick(){
				this.ShowHideUp(false);
			},
			itemshowclick(id) {
				this.testShow = id;
				if (this.testShowi == id) {
					this.testShow = -1;
					id = -1;
				}
				this.testShowi = id;
			},

			search(value) {

				this.searchmodel = value;
				this.form.WONo = "";
				this.form.BusSN = "";
				this.form.SalesDisVoucherProNo = "";

				switch (this.SeachTit) {
					case "生产工单":
						this.form.WONo = value;
						break;
					case "整车序列号":
						this.form.BusSN = value;
						break;
					case "销售订单":
						this.form.SalesDisVoucherProNo = value;
						break;
				}

				if (this.searchmodel != "") {
					this.upCallback({
						num: 1,
						page: 10
					});
				}


			},
			clear() {
				this.form.WONo = "";
				this.searchmodel = "";
				// console.log(this.value);
			},
			ScanQR() {
				// 允许从相机和相册扫码
				uni.scanCode({
					success: function(res) {

						// this.$api.msg('条码类型' + res.scanType);
						// this.$api.msg('条码内容' + res.result);
						this.search(res.result);
					}
				});
			},
			
			tagclick(WONo,id) {
						
					},
					tagclose(WONo,id) {
						for(var i=0;i<this.selectTagList.length;i++){
							if(WONo==this.selectTagList[i].WONo){
								 this.selectTagList.splice(i, 1);
							}
						}
						
					},
			/*下拉刷新的回调 */
			downCallback() {
				// 这里加载你想下拉刷新的数据, 比如刷新轮播数据
				// loadSwiper();
				// 下拉刷新的回调,默认重置上拉加载列表为第一页 (自动执行 page.num=1, 再触发upCallback方法 )
				this.mescroll.resetUpScroll();
			},
			/*上拉加载的回调: 其中page.num:当前页 从1开始, page.size:每页数据条数,默认10 */
			upCallback(page) {
				if (page.num <= 1) {
					this.list = [];
				}
				this.page.page = page.num;
				console.log(this.page.page)

				// this.$u.post('http://localhost:52521/api/Test/CallGetEntity', {
				// 	TableName: "WorkOrderEntity",
				// 	Current: this.page.page,
				// 	Page: this.page.limit,
				//     OrderBy:"OffsideNoticeFilePath",
				//     Sort:"asc"
				// }).then(res => {
				// 	$this.mescroll.endSuccess(res.result.length);
				// 	$this.list = $this.list.concat(res.result);

				// });
				this.form;

				//说明是查询要清空列表
				if (page.num == 1)
					this.list = [];

				var res = [];
				for (var i = 0; i < 15; i++) {
					res = res.concat({
						"id": i,
						"checklist": false,
						"OffsideNoticeFilePath": "1111",
						"WorkShopName": "测试车间",
						"BusSN": "B571205041B00",
						"SalesDisVoucherProNo": "0020012537",
						"WONo": "0020012537_000" + i,
						icon: "/static/bus8.png",

					});

				}


				this.list = this.list.concat(res);
				this.mescroll.endSuccess(res.length);
				// this.CallGetEntity({
				// 	TableName: "WorkOrderEntity",
				// 	Params:this.form,
				// 	Current: this.page.page,
				// 	Page: this.page.limit,
				// 	OrderBy: "OffsideNoticeFilePath",
				// 	Sort: "asc"
				// }).then(res => {
				// 	this.list = this.list.concat(res.result);
				// 	this.mescroll.endSuccess(res.result.length);
				// });

			},

			handleUpload() {
				this.$upload(this.uploadSuccess, this.uploadProgress)
			},
			uploadSuccess(res) {
				this.form.payQrcode = res.url
			},
			uploadProgress(res) {
				console.log("上传进度:", res)
			},
			//顶部tab点击
			sideTabClick(side, index) {
				this.sideIndex = index
				this.page.side = side
				this.mescroll.resetUpScroll()
			},
			//顶部tab点击
			coinTabClick(index) {
				this.coinIndex = index;
				this.page.coin = this.fiatCoins[index]
				this.mescroll.resetUpScroll()
			},
			filter() {
				uni.getSubNVueById('otcFilterDrawer').show('slide-in-right', 200);
			},
		}
	}
</script>


<style>
	.wrap {
		display: flex;
		flex-direction: column;
		height: calc(100vh - var(--window-top));
		width: 100%;
	}
</style>
