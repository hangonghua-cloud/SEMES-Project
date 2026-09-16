<template>

    <view class="container">
        <u-form :model="form"  ref="uForm" label-width="auto">
            <u-form-item label="起始日期">
                <u-input v-model="form.Begintime" disabled placeholder="请选择起始日期" type="text" border />
                <date-picker @getTime="getDate"></date-picker>
            </u-form-item>
            <u-form-item label="结束日期">
                <u-input v-model="form.StartTime" disabled placeholder="请选择结束日期" type="text" border />
                <date-picker @getTime="getDate1"></date-picker>
            </u-form-item>
            <u-form-item label="返工单号">
                <u-input v-model="form.fangonghao" type="text" placeholder="" border />
            </u-form-item>
            <u-form-item label="唛头码">
                <u-input v-model="form.mtm" type="text" placeholder="" border />
            </u-form-item>
            <u-form-item label="PO号">
                <u-input v-model="form.PO" type="text" placeholder="" border />
            </u-form-item>
        </u-form>
        <view style="margin-top:10px;">
            <u-divider halfWidth="100%">返工单信息</u-divider>
        </view>
        <view style="height: 630rpx;">
            <scroll-view scroll-y="true" class="scroll-Y" style="height: 600rpx;">
                <u-collapse>
                    <view style="border:1px solid white" v-for="(item, index) in gridList">
                        <u-collapse-item class="u-collapse-item">
                            <template slot="title">
                                <text style="font-size: 14px;">返工单号：{{item.fangonghao}}</text>
                                <text style="font-size: 14px;">订单号：{{item.ProductOrder}}</text>                                
                            </template>
                            <view>订单号：{{item.ProductOrder}}</view>
                            <view>柜号：{{item.ContainerNO}}</view>
                            <view>PO号：{{item.CustomerPO}}</view>
                            <view>返工单状态：{{item.CardName}}</view>
                            <view>总返工片数：{{item.MaterialCode}}</view>
                            <view>已返工片数：{{item.Qty}}</view>
                            <view>待返工片数：{{item.BadQty}}</view>
                            <view>返工部门：{{item.Creator}}</view>
                            <view>返工时间：{{item.CreateTime}}</view>
                        </u-collapse-item>
                    </view>
                </u-collapse>
            </scroll-view>
        </view>
        <view class="" style="display: flex;justify-content: center;">
            <u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
                @click="search" style="position: fixed;bottom: 30rpx;">
                <text>查询</text>
            </u-button>
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
                    fangonghao:"",//返工单号
                    mtm: "",//唛头码
                    PO: "",//PO号
                    Qty: "",
                    BadQty: "",
                    ContainerNO: "",
                    CustomerPO: "",
                    Begintime: "",//结束日期
                    StartTime: "",//起始日期
                   
                },
                showProcess: false,
                processList: [],
                gridList: [],
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
        onLoad() {
            //this.getProcessList();
        },
        onShow() {
            // window.scrollTo(0, 0)
        },
        methods: {
            ...mapActions('Produce', ['PackingBGQuery']),
            ...mapActions('common', ['GetDictionary', 'GetProcessModel']),

            getDate(val) {
                this.form.Begintime = val;
            },
            getDate1(val) {
                this.form.StartTime = val;
            },
            //查询
            search() {
                this.gridList = [];
                //测试
                this.gridList.push({
                    fangonghao: 'FGD0001',
                    ProductOrder: "20-0100#",
                    ContainerNO: "175柜",
                    MaterialCode: "14000",
                    Qty: "9500+500",
                    CustomerPO: " 2956580",
                    CardName: "返工中",
                    BadQty: 4000,
                    CreateTime: "2021-10-12",
                    Creator: "封边",
                    SecondResult: "",
                    SecondUser: "",
                    SecondTime: "",
                });
                return;//测试结束
                var query = {
                    queryJson: {
                        "ProductOrder": this.form.ProductOrder,
                        "ContainerNO": this.form.ContainerNO,
                        "CreateTime": this.form.reworkDate,
                    }
                }
                this.成品返工记录查询事件(query).then(res => {
                    if (res && res.success) {
                        this.gridList = [];
                        if (res.resultData == null || res.resultData.length == 0) {
                            this.gridList = [];
                        } else {
                            console.log(JSON.stringify(res.resultData));
                            res.resultData.forEach((item, index) => {
                                this.gridList.push({
                                    ProductOrder: item.ProductOrder,
                                    ContainerNO: item.ContainerNO,
                                    MaterialCode: item.MaterialCode,
                                    Qty: item.Qty,
                                    CustomerPO: item.CustomerPO,
                                    CardName: item.CardName,
                                    BadQty: item.BadQty,
                                    CreateTime: item.CreateTime,
                                    Creator: item.Creator,
                                    SecondResult: item.SecondResult,
                                    SecondUser: item.SecondUser,
                                    SecondTime: item.SecondTime,
                                });
                            });
                        }
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
                this.form.PTeamCode = "";
                this.form.ProductOrder = "";
                this.form.ContainerNO = "";
                this.form.MMXH = "";
                this.form.Spec = "";
                this.form.MachineCode = "";
                this.firtResultCode = "",
                    this.firtResultNAME = "",
                    this.form.TotalPallet = "";

                this.gridList = [];
            },
            //返回页面
            goBack() {
                uni.switchTab({
                    url: "/pages/index/index",
                });
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
        width: 110%;

        view {
            background-color: #F0F3FA;
            font-size: 14px;
        }
    }
</style>
