<template>
    <view class="WMS_FinishedPorductReturn">
        <view class="top">
           <u-form-item label="唛头码:" required>
               <!-- <u-input v-model="form.EquipmentId"  clearable type="text"  border placeholder="设备编码扫描" /> -->
               <u-search v-model="form.MarkCode" @custom="custom" @search="searchCardCode" @clear="clear"
                   placeholder="唛头码扫描" shape="square" border :show-action="showAction=false">
               </u-search>
               <u-icon name="scan" size="70" @click="searchQR"></u-icon>
           </u-form-item>
        </view>
        <view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
            <!-- <u-divider halfWidth="100%">记录查询</u-divider> -->
            <u-line color="blue" />
        </view>
        <u-form :model="form" ref="uForm">
            <u-form-item label="订单号:">
                <u-input v-model="form.ProductOrder" type="text" disabled="" />
            </u-form-item>
            <u-form-item label="柜号:">
                <u-input v-model="form.ContainerNO" type="text" disabled="" />
            </u-form-item>
            <u-form-item label="PO号:">
                <u-input v-model="form.CustomerPO" type="text" disabled="" />
            </u-form-item>
            <u-form-item label="总盒数:">
                <u-input v-model="form.TotalBoxQty" type="text" disabled="" />
            </u-form-item>
            <u-form-item label="总托数:">
                <u-input v-model="form.TotalPalletQty" type="text" disabled="" />
            </u-form-item>
        </u-form>
        <view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
            <u-divider halfWidth="100%">
                <u-checkbox @change="checkboxChange" v-model="allchecked" :name="allname">成品返工唛头清单</u-checkbox>
            </u-divider>
            <!-- <u-line color="blue" /> -->
        </view>
        <!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;"> -->
        <u-table style="margin-top: 20rpx;" align="left">
            <u-tr class="u-tr">
                <u-th width="12%" align="center">选择</u-th>
                <u-th align="center">成品发货详情</u-th>
            </u-tr>
            <u-tr v-for="(item,index) of SparePartsItemDetailList" :key="index">
                <u-th width="12%" align="center">
                    <u-checkbox v-model="item.Checked">
                        <!-- <view class="label u-line-1"></view> -->
                    </u-checkbox>
                </u-th>
                <u-th align="left">唛头号:{{item.mth}}<br>客户型号:{{item.khxh}}<br>库存位置:{{item.kcwz}}</u-th>
            </u-tr>
        </u-table>
        <view class="header">
            <u-form :model="form" ref="uForm">
                <u-form-item label="责任部门:" required>
                    <u-input v-model="form.ProcessName" type="select" disabled="" @click="clickSelFun('process')" border
                        placeholder="请选择返工责任部门" />
                </u-form-item>
            </u-form>
        </view>
        <!-- </scroll-view> -->
        <view style="height: 205rpx;"></view>
        <view class="" style="display: flex;justify-content: center;">
            <u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
                @click="SaveQCTestItemFormPost" style="position: fixed;bottom: 30rpx;">
                <text>生成返工单</text>
            </u-button>
        </view>
        <view style="height: 15upx;"></view>
        <!-- 工序选择 -->
        <u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
        <!-- 责任部门(工序) -->
        <u-select v-model="showDetermination" @confirm="changeDetermination" :list="DeterminationList"></u-select>
        <view>
            <!-- 弹出提示 -->
            <u-top-tips ref="uTips"></u-top-tips>
            <u-toast ref="uToast" />
        </view>
        <homeBtn></homeBtn>
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
		export default {
			mixins: [commonMixin], // 使用mixin (在main.js注册全局组件)
			components: {
				scanCode
			},
			data() {
				return {
					form: {
						
					MarkCode :"",				
					ProductOrder :"",
					ContainerNO :"",
					MaterialCode :"",
					CustomerPO:"",
					TotalPalletQty:"",
					TotalBoxQty:"",
					
					lstLoc :"",
					
					WhsCode:"",
					WhsName :"",
					locationCode:"",
					LocationCode:"",
					LocationName:"",
				    },
					chkAll: false,
			 SparePartsItemDetailList:[],
					//工序是否显示弹窗
					showdutyProcess: false,
					showreworkProcess: false,
					CardList: [],
					rules: {
						code: [{
							required: true,
							message: '请输入姓名',
							trigger: 'blur,change'
						}],
						intro: [{
							min: 5,
							message: '简介不能少于5个字',
							trigger: 'change'
						}]
					},
				}
			},
	
			onReady() {
				// this.$refs.uForm.setRules(this.rules);
				// this.mescroll.resetUpScroll()
				// this.mescroll.showNoMore()
			},
			//预加载
			onLoad() {
			
			},
			onNavigationBarButtonTap: function(option) {
			    //点击事件
			    console.info('点击查询按钮事件', JSON.stringify(option));
			    uni.navigateTo({
			        url: '/pages/WMSModel/WMS_SemiFinishProductChangeSel'
			    })
			},
			
			onNavigationBarButtonTap: function(option) {
			    //点击事件
			    console.info('点击查询按钮事件', JSON.stringify(option));
			    uni.navigateTo({
			        url: '/pages/WMSModel/WMS_FinishProductChangeSel'
			    })
			},
			
			onShow() {
				// window.scrollTo(0, 0)
			},
			methods: {
				//参数1 store/modules目录下 文件名, 参数2 文件里方法名
				//查询入库单主表列表(过滤掉状态为已入库) pagination 分页json; queryJson 查询JSO
				...mapActions('WMS', ['ProductReWorkMarkScan','ProductMoveLocationScan','ProductMoveSave']),
				...mapActions('common', ['GetProcessModel']),
				
				
				
				
				//显示下拉框
				showSel(val, item) {
					if (val == "dutyprocess")
						this.showdutyProcess = true;
						else if (val=="reworkprocess")
						this.showreworkProcess=true;
				},
				//选择责任工序
				changedutyProcess(val) {
					this.form.dutyProcessCode = val[0].value; //val[0].label;				
					this.form.dutyProcessName = val[0].label;
				},
	
	           //选择返工工序
				changereworkProcess(val) {
					this.form.reworkProcessCode = val[0].value; //val[0].label;				
					this.form.reworkProcessName = val[0].label;
				},
				
				//条码扫描事件
				searchQR() {
				    var self = this;
				    //允许从相机和相册扫码
				    uni.scanCode({
				        success: function(res) {
				            self.searchCardCode(res.result);
				        }
				    });
				},
				
				//条码查询
				searchCardCode(value) {
				    this.form.MarkCode = value; //唛头码
				    if (this.form.MarkCode != "") {
				        this.productReWorkMarkScan();
				    }
				},
				
				clear() {
				    this.form.MarkCode = ""; //唛头码
				},
				
				
	         //唛头码扫描
				productReWorkMarkScan() {
					this.SparePartsItemDetailList = [];
					let query = {
						markCode: this.form.MarkCode
					};
					this.ProductReWorkMarkScan(query).then(res => {
						console.log(JSON.stringify(res));
						if (res.success) {
							console.log(JSON.stringify(res.resultData));
							this.form.ProductOrder = res.resultData.ProductOrder;
						    this.form.ContainerNO =res.resultData.ContainerNO;
							this.form.CustomerPO=res.resultData.CustomerPO;
							this.form.MaterialCode=res.resultData.MaterialCode;
						    this.form.TotalPalletQty=res.resultData.TotalPalletQty;
							this.form.TotalBoxQty=res.resultData.TotalBoxQty;
					if(res.resultData.detail==null || res.resultData.detail.length==0)	
						{
							this.SparePartsItemDetailList=[];
							
						}
						else{
							
							this.SparePartsItemDetailList=res.resultData.detail;
							this.SparePartsItemDetailList.forEach(item => {
								this.$set(item, "Checked", false);
							})
						  }
						}
					 else {
							this.$refs.uToast.show({
								title: '' + res.returnMsg,
								type: 'warning',
								icon: true
							});
						}
						//console.log(JSON.stringify(this.form.MarkingManageId))
					});
				},
	
				
				//库位扫描
			     	productMoveLocationScan() {
			     		this.CardList = [];
			     		let query = {
			     			locationCode: this.form.locationCode
			     		};
			     		this.ProductMoveLocationScan(query).then(res => {
			     			console.log(JSON.stringify(res));
			     			if (res.success) {
			     				console.log(JSON.stringify(res.resultData));
			     				this.form.WhsCode = res.resultData.WhsCode;
			     				this.form.WhsName = res.resultData.WhsName;
			     			    this.form.LocationCode =res.resultData.LocationCode;
			     				this.form.LocationName=res.resultData.LocationName;
			     				
			     			} else {
			     				this.$refs.uToast.show({
			     					title: '' + res.returnMsg,
			     					type: 'warning',
			     					icon: true
			     				});
			     			}
			     			//console.log(JSON.stringify(this.form.MarkingManageId))
			     		});
			     	},
					
				
				//确认
				save() {
					if (this.form.MarkCode == "") {
						this.$refs.uToast.show({
							title: '唛头码不能为空',
							type: 'warning',
							icon: true
						});
						return;
					}
					if (this.form.locationCode == "") {
						this.$refs.uToast.show({
							title: '移库库位不能为空！',
							type: 'warning',
							icon: true
						});
						return;
					}
					
					let data = {
						markCode: this.form.MarkCode,
						whsCode: this.form.WhsCode,
						locationCode: this.form.LocationCode,
						userCode: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App',
						userName: this.loginInfo.result ? this.loginInfo.result.UserName : 'MesApp'
					};
					
					
					this.SemiProductMoveSave(data).then(res => {
						console.log(JSON.stringify(res));
						if (res.success) {
							this.$refs.uToast.show({
								title: '保存成功！',
								type: 'success',
								icon: true
							});
							this.reset();
						} else {
							this.$refs.uToast.show({
								title: '' + res.returnMsg,
								type: 'warning',
								icon: true
							});
						}
					});
				},
				reset() {
					this.form.MarkCode = "";
					this.form.ProductOrder = "";
					this.form.ContainerNO = "";
					this.form.MaterialCode = "";
					this.form.SourceWhsCode = "";
					this.form.SourceWhsName = "";
					this.form.SourceLocationCode = "";
					this.form.SourceLocationName = "";
					this.form.lstLoc = "";
					this.form.WhsCode = "";
				    this.form.WhsName = "";
				    this.form.locationCode = "";
				    this.form.LocationCode = "";
					this.form.LocationName = "";
					this.CardList = [];
				},
					
				//查询
				sel(){	
					uni.navigateTo({
							url: '/pages/WMSModel/WMS_FinishProductStoreSel',
						});
					
				},
				//待入库清单
				search(){
					uni.navigateTo({
							url: '/pages/WMSModel/WMS_FinishProductVoucher',
						});
				},	
			}
		}
</script>
<style lang="scss" scoped>
    .WMS_FinishedPorductReturn {
        padding: 20upx;
        //background: #f9f9f9;
        font-size: 32upx;
        height: 100vh;
    }

    .readonly {
        background-color: Gainsboro;
    }

    .u-form-item {
        height: auto;
    }

    .u-form {
        background: #fff;
        padding: 0 20upx 20upx 20upx;
    }

    .btn {
        display: flex;

        uni-button {
            width: 48%;
        }
    }

    .wrap {
        margin-top: 24rpx;
    }

    .item {
        display: flex;
        background: #fff;
        padding: 20upx;
        flex-direction: column;
        margin-bottom: 8upx !important;
        align-items: center;
        //box-shadow: 0px 3px 3px #7b7b7b;

        .left {
            width: 160upx;
        }

        .top {
            display: flex;
            width: 100%;
            padding: 0 20upx 20upx 0;
            align-items: center;
            border-bottom: 2px solid #138087;

            .name {
                font-size: 30upx;
                font-weight: 600;
            }
        }

        .bottom {
            width: 100%;

            .center {
                flex: 1;

                .deviedeItem {
                    display: flex;
                    font-size: 30upx;

                    .nr {
                        color: #999999;
                    }

                    view {
                        padding: 10upx;
                    }
                }
            }
        }
    }
</style>
