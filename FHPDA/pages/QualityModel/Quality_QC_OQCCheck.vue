<template>
    <view class="Quality_QC_OQCCheck">
        <view class="top">
            <u-form :model="form" ref="uForm">
                <u-form-item label="唛头码:">
                    <u-input v-model="form.mtm" type="text" disabled="" />
                </u-form-item>
                <u-form-item label="OQC检验单">
                    <u-input v-model="form.jyd" type="text" disabled="" />
                </u-form-item>
                <u-form-item label="订单号:">
                    <u-input v-model="form.ddh" type="text" disabled="" />
                </u-form-item>
                <u-form-item label="柜号:">
                    <u-input v-model="form.gh" type="text" disabled="" />
                </u-form-item>
                <u-form-item label="客户型号:">
                    <u-input v-model="form.khxh" type="text" disabled="" />
                </u-form-item>
                <u-form-item label="建单时间:">
                    <u-input v-model="form.jdsj" type="text" disabled="" />
                </u-form-item>
            </u-form>
        </view>

        <!-- <scroll-view scroll-y="true" class="scroll-Y" style="height: 760rpx;"> -->
        <u-table style="margin-top: 20rpx;" font-size="16">
            <u-tr class="u-tr">
                <u-th>检验大类</u-th>
                <u-th>检验项目</u-th>
                <u-th>一等品指标</u-th>
                <u-th>合格指标</u-th>
                <u-th width="36%">检验结果</u-th>
                <u-th width="36%">判别</u-th>
            </u-tr>
            <u-tr v-for="(item,index) of EP_EquipmentMaintainDetailList" :key="index">
                <u-th>{{item.a}}</u-th>
                <u-th>{{item.b}}</u-th>
                <u-th>{{item.c}}</u-th>
                <u-th>{{item.d}}</u-th>
                <u-th width="36%" v-if="item.DataTypeName == '数值'">
                    <u-input v-model="item.TestItemResult" placeholder="输入数值" type="number" border />
                </u-th>
                <u-th width="36%" v-else-if="item.DataTypeName == '文本'">
                    <u-input v-model="item.TestItemResult" placeholder="输入内容" type="text" border />
                </u-th>
                <u-th width="36%" v-else-if="item.DataTypeName == '日期'">
                    <u-input v-model="item.TestItemResult" placeholder="输入日期"
                        @click="ShowactionDATE(item.TestItemCoading,item.DataTypeName)" type="select" border />
                </u-th>
                <u-th width="36%" v-else>
                    <!-- ="item.DataTypeName.index('/') > 0" -->
                    <u-input v-model="item.TestItemResult" placeholder="下拉选择" type="select"
                        @click="ShowactionSheetLis(item.TestItemCoading,item.DataTypeName,item.Options)" border />
                    <u-action-sheet :list="actionSheetList" v-model="IsShowactionSheetList"
                        @click="actionSheetCallback"></u-action-sheet>
                </u-th>
                <u-th width="36%">
                    <!-- ="item.DataTypeName.index('/') > 0" -->
                    <u-input v-model="item.TestItemResult2" placeholder="下拉选择" type="select"
                        @click="ShowactionSheetLis2(item.TestItemCoading,item.DataTypeName,item.Options)" border />
                    <u-action-sheet :list="actionSheetList2" v-model="IsShowactionSheetList2"
                        @click="actionSheetCallback2"></u-action-sheet>
                </u-th>
            </u-tr>
        </u-table>
        <view class="header">
            <!--   <view style="margin-top:10rpx;margin-bottom: 10rpx;" class="header">
                <u-divider halfWidth="100%">最终判定</u-divider>
            </view> -->
            <u-form :model="form" ref="uForm">
                <u-form-item label="检验数量:" style="height: auto;margin-bottom: -5px;margin-top: 1px;">
                    <u-input v-model="form.CkcNum" type="number" placeholder="" border />
                </u-form-item>
                <u-form-item label="检验结论:" required>
                    <u-input v-model="form.Determination" type="text" disabled="" @click="clickSelFun('Determination')"
                        border placeholder="请选择检验结论" />
                </u-form-item>
                <u-form-item label="备注:" style="height: auto;margin-bottom: -5px;margin-top: 1px;">
                    <u-input v-model="form.Remark" type="textarea" placeholder="" border />
                </u-form-item>
            </u-form>
        </view>
        <!-- </scroll-view> -->
        <view style="height: 205rpx;"></view>
        <view class="" style="display: flex;justify-content: center;">
            <u-button :type="'primary'" :custom-style="{width: '50%',height: '70rpx',borderRadius: '10rpx'}"
                @click="SaveQCTestItemFormPost" style="position: fixed;bottom: 30rpx;">
                <text>提交</text>
            </u-button>
        </view>
        <!-- <view class="" style="display: flex;">
            <u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087"
                :custom-style="{width: '43%',height: '70rpx',borderRadius: '10rpx'}" @click="SaveQCTestItemFormPost"
                style="position: fixed;bottom: 30rpx;margin-left: 2%;">生成过程检验记录
            </u-button>
            <u-button :type="'success'" :ripple="true" ripple-bg-color="#00aa00"
                :custom-style="{width: '43%',height: '78rpx',borderRadius: '10rpx'}" @click="search"
                style="position: fixed;bottom: 30rpx;margin-left: 50%;">记录查询
            </u-button>
        </view> -->
        <!-- <view class="btn">
            <u-button :type="'primary'" :ripple="true" ripple-bg-color="#138087" style="width: '40%';margin-left: auto;"
                @click="SaveQCTestItemForm">生成过程检验记录
            </u-button>
            <u-button :type="'success'" :ripple="true" ripple-bg-color="#138087" style="width: '40%';margin-left: auto;"
                @click="search">记录查询
            </u-button>
        </view> -->
        <view style="height: 15upx;"></view>
        <!-- 工序选择 -->
        <u-select v-model="showProcess" @confirm="changeProcess" :list="processList"></u-select>
        <!-- 检验机台选择 -->
        <u-select v-model="isShowResult" @confirm="changeProductionMachine" :list="ProductionMachineList"></u-select>
        <!-- 日期范围选择 -->
        <u-calendar v-model="isShowDate" :mode="mode" @change="dateChange"></u-calendar>
        <!-- 检验项目单日期选择 -->
        <u-calendar v-model="isShowDate2" mode="date" @change="dateChange2"></u-calendar>
        <!-- 工序选择 查询 -->
        <u-select v-model="showProcess2" @confirm="changeProcess2" :list="processList2"></u-select>
        <!-- 检验机台选择 -->
        <u-select v-model="isShowResult2" @confirm="changeProductionMachine2" :list="ProductionMachineList2"></u-select>
        <!-- 检验方法选择 -->
        <u-select v-model="IsShowTestMethodCoading" @confirm="changeTestMethodCoading" :list="TestMethodCoadingList">
        </u-select>
        <!-- 判定选择 -->
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
        formatDate
    } from "@/utils/date.js"; //转换日期格式
    import {
        commonMixin
    } from '@/common/mixin/mixin.js'
    import timePicker from '@/components/timePicker/timePicker.vue'
    import scanCode from '@/components/scanCode/scanCode.vue'
    import selectPicker from '@/components/select/select.vue'
    import global from '@/utils/global'
    export default {
        data() {
            return {
                action: global.FileHandler, //图片上传地址
                fileList: [], //文件上传列表
                mtm: "", //唛头码
                jyd: "", //OQC检验单号
                form: {
                    mtm: "", //唛头码
                    jyd: "", //OQC检验单
                    ddh: "", //订单号
                    gh: "", //柜号
                    khxh: "", //客户型号
                    jdsj: "", //建单时间
                    CkcNum: "1", //检验数量
                    Determination: "合格",
                    DeterminationCode: "1",
                    Remark: "" //备注
                },
                //工序列表
                processList: [],
                //工序是否显示弹窗
                showProcess: false,
                //工序列表
                processList2: [],
                //工序是否显示弹窗
                showProcess2: false,
                ProductionMachineList: [], //检验机台列表
                ProductionMachineList2: [], //检验机台列表
                isShowResult: false, //检验机台 下拉显示面板
                isShowResult2: false, //检验机台 下拉显示面板
                showDetermination: false, //检验合格 下拉显示面板 是否显示
                DeterminationList: [{
                        label: '合格',
                        value: '1'
                    },
                    {
                        label: '不合格',
                        value: '2'
                    }
                ], //检验判定 1合格, 2 不合格
                list: 15,
                page: 0,
                isShowDate: false, //显示日期范围选择面板
                mode: 'range',
                gridList: [], //设备保养任务列表
                //isShowCheck: false, //设备类别 下拉显示面板               
                selectCheck: [], //设备类别数组
                SparePartsItemDetailList: [], //检测结果数组
                SparePartsItemDetailList2: [], //检测明细结果数组
                show_shd: false, //记录查询页面
                show_shd2: false, //记录查询明细页面
                isShowDate2: false, //显示日期
                IsShowTestMethodCoading: false, //显示检验方法
                TestMethodCoadingList: [], //检验方法列表
                Upmode: 'right', //显示弹窗从右出到左
                Upmask: true, // 是否显示遮罩
                Upcloseable: false, //是否显示弹窗关闭按钮
                UpcloseIconPos: 'top-left', //显示弹窗关闭按钮 显示位置
                UserList: [],
                EP_EquipmentMaintainDetailList: [], //检测项目列表
                actionSheetList: [{
                        text: '男'
                    },
                    {
                        text: '女'
                    },
                ], //检验项目 数据类型 下拉
                actionSheetList2: [{
                        text: '合格'
                    },
                    {
                        text: '不合格'
                    },
                ], //检验项目 数据类型 下拉
                IsShowactionSheetList: false, //是否显示保养任务项目选择下拉框
                IsShowactionSheetList2: false, //是否显示保养任务项目选择下拉框
                TestItemCoading: '', //检验任务项目编码
                isShowUser: false,
                selectUser: [],
                isShowSpare: false,
                SpareList: [],
                radioResult: ""
            };
        },
        filters: {

        },
        components: {
            timePicker,
            scanCode,
            selectPicker
        },
        mixins: [commonMixin],
        onLoad: function(option) {
            //当前登录的用户信息
            //console.info('当前登录人信息', JSON.stringify(this.loginInfo));
            //当前登录的用户信息
            console.info('传参数据', JSON.stringify(this.option));
            this.mtm = option.mtm; //唛头码
            this.jyd = option.jyd; //OQC检验单号
            console.info('传参数据.mtm', this.mtm);
            if (!this.mtm) {
                this.$refs.uToast.show({
                    title: '无唛头码传参!',
                    type: 'warning',
                    icon: true
                });
                return;
            }
            this.LoadList();
        },
        computed: {
            ...mapState('user', ['loginInfo'])
        },
        methods: {
            ...mapActions('Quality', ['GetQCTransferCardEntity', 'GetQCTestItemList', 'GetQCTestMethodList',
                'SaveQCTestItemForm', 'GetQCTestRecordList', 'GetQCTestResultRecordList', 'SavePollingDetailForm'
            ]),
            ...mapActions('common', ['GetDictionary', 'GetModelResourceExtendInfoByLevelCode', 'GetUserList',
                'GetBaseMaterialList', 'GetProcessModel', 'GetListByParentResource'
            ]),
            //初始根据 唛头码和检验单号 获取OQC检验项目
            LoadList() {
                //从api 接口 获取 OQC检验记录和检验项目
                this.form = {
                    mtm: this.mtm, //唛头码
                    jyd: this.jyd, //OQC检验单
                    ddh: "20-0100#", //订单号
                    gh: "175柜", //柜号
                    khxh: "V097707033", //客户型号
                    jdsj: "2021-9-18  10:35" ,//建单时间
                    CkcNum:"1",//检验数量
                    Determination:"合格",//检验结论
                    DeterminationCode:"1",//检验结论 编码
                    Remark:""//备注
                };
                this.EP_EquipmentMaintainDetailList.push({
                    TestItemCoading: "1",
                    a: "外观",
                    b: "分层",
                    c: "1.0",
                    d: "2.0",
                    DataTypeName: "数值",
                    Options: "",
                    TestItemResult: "",
                    TestItemResult2: ""
                }, {
                    TestItemCoading: "2",
                    a: "尺寸",
                    b: "厚度",
                    c: "±0.15mm",
                    d: "±0.15mm",
                    DataTypeName: "文本",
                    Options: "",
                    TestItemResult: "",
                    TestItemResult2: ""
                }, {
                    TestItemCoading: "3",
                    a: "下拉",
                    b: "选择",
                    c: "±0.15mm",
                    d: "±0.15mm",
                    DataTypeName: "下拉选择",
                    Options: [{
                        text: '是'
                    }, {
                        text: '否'
                    }],
                    TestItemResult: "",
                    TestItemResult2: ""
                }, {
                    TestItemCoading: "4",
                    a: "尺寸",
                    b: "垂直度",
                    c: "公差≤0.25mm",
                    d: "公差≤0.25mm",
                    DataTypeName: "日期",
                    Options: "",
                    TestItemResult: "",
                    TestItemResult2: ""
                })
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
                this.form.CardCode = value; //流转卡
                if (this.form.CardCode != "") {
                    this.getCodes(value);
                }
            },
            clear() {
                this.form.CardCode = ""; //流转卡
            },
            custom(val) {
                console.info('', val);
            },
            getCodes(val) {
                //巡检/过程检验根据流转卡获取信息
                console.log(val)
                let queryJson = {
                    "CardCode": val
                };
                console.info('流转卡查询参数', JSON.stringify(queryJson));
                this.GetQCTransferCardEntity(queryJson).then(res => {
                    if (res && res.success) {
                        console.info('流转卡获取信息', JSON.stringify(res.resultData));
                        this.form = res.resultData;
                        //工序
                        this.getProcessList();
                    } else {
                        this.$refs.uToast.show({
                            title: '' + res.returnMsg,
                            type: 'warning',
                            icon: true
                        });
                    }
                });
            },
            //初始化工序列表
            getProcessList() {
                this.form.ProcessCode = "";
                this.form.ProcessName = "";
                var data = {
                    FactoryCode: this.form.FactoryCode
                }
                this.GetProcessModel(data).then(res => {
                    this.processList = [];
                    if (res.success) {
                        if (res.resultData == null || res.resultData.length == 0) {
                            this.processList = [];
                        } else {
                            console.log(JSON.stringify(res.resultData));
                            res.resultData.forEach((item, index) => {
                                this.processList.push({
                                    value: item.ResourceCode,
                                    label: item.ResourceName
                                });
                            });
                        }
                    } else {
                        this.processList = [{
                            value: '',
                            label: '无'
                        }];
                    }
                });
            },
            //选择工序
            changeProcess(val) {
                this.form.ProcessCode = val[0].value; //val[0].label;				
                this.form.ProcessName = val[0].label;
                //初始化检验机台列表
                this.searchSpareParts();
                //检验方法选择列表
                this.searchTestMethodCoading();
            },
            //保养项目下拉点击初始下拉内容与显示下拉面板
            ShowactionSheetLis(TestItemCoading, DataTypeName, Options) {
                this.TestItemCoading = TestItemCoading;
                console.info('检验项目编码与内容', TestItemCoading, DataTypeName);
                this.actionSheetList = Options; //[];
                // let ep_item = DataTypeName.split("/"); //字符分割
                // ep_item.forEach((item, index) => {
                //     this.actionSheetList.push({
                //         text: item
                //     })
                // });

                this.IsShowactionSheetList = true; //显示检验项目下拉框
            },
            //检验项目下拉回调事件
            actionSheetCallback(index) {
                console.info('检验项目下拉回调内容', this.actionSheetList[index].text);
                let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.TestItemCoading == this
                    .TestItemCoading);
                console.info('当前选择检验项目', JSON.stringify(filterList));
                if (filterList.length > 0) {
                    filterList[0].TestItemResult = this.actionSheetList[index].text;
                }
            },
            //保养项目下拉点击初始下拉内容与显示下拉面板
            ShowactionSheetLis2(TestItemCoading, DataTypeName, Options) {
                this.TestItemCoading = TestItemCoading;
                console.info('检验项目编码与内容', TestItemCoading, DataTypeName);
                //this.actionSheetList2 = Options; //[];
                // let ep_item = DataTypeName.split("/"); //字符分割
                // ep_item.forEach((item, index) => {
                //     this.actionSheetList.push({
                //         text: item
                //     })
                // });

                this.IsShowactionSheetList2 = true; //显示检验项目下拉框
            },
            //检验项目下拉回调事件
            actionSheetCallback2(index) {
                console.info('检验项目下拉回调内容', this.actionSheetList2[index].text);
                let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.TestItemCoading == this
                    .TestItemCoading);
                console.info('当前选择检验项目', JSON.stringify(filterList));
                if (filterList.length > 0) {
                    filterList[0].TestItemResult2 = this.actionSheetList2[index].text;
                }
            },
            //检验项目日期点击初始下拉内容与显示下拉面板
            ShowactionDATE(TestItemCoading, DataTypeName) {
                this.TestItemCoading = TestItemCoading;
                console.info('检验项目编码与内容', TestItemCoading, DataTypeName);

                this.isShowDate2 = true; //显示日期选择
            },
            //检验项目日期点击初始下拉内容与显示下拉面板  回调
            dateChange2(e) {
                console.info('检验项目日期回调内容', e.result);
                let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.TestItemCoading == this
                    .TestItemCoading);
                console.info('当前选择检验项目', JSON.stringify(filterList));
                if (filterList.length > 0) {
                    filterList[0].TestItemResult = e.result;
                }
            },
            getDate(val) {
                console.info('检验项目日期回调内容', val);
                let filterList = this.EP_EquipmentMaintainDetailList.filter(item => item.EquipmentMaintainId == this
                    .EquipmentMaintainId);
                console.info('当前选择检验项目', JSON.stringify(filterList));
                if (filterList.length > 0) {
                    filterList[0].TestItemResult = val;
                }
            },
            //start 弹窗选择*****************            
            //选择保养人
            SearchUser() {
                this.isShowUser = true;

                var queryJson = {
                    "UserCode": this.form2.RepairingPersonName
                };
                this.GetUserList(queryJson).then(res => {
                    this.UserList = [];
                    if (res && res.success) {
                        res.resultData.forEach((item, index) => {
                            this.UserList.push({
                                value: item.Code,
                                label: item.Name //item.Code + '-' + 
                            });
                        });
                    } else {
                        this.$refs.uToast.show({
                            title: '' + res.returnMsg,
                            type: 'warning',
                            icon: true
                        });
                    }
                })
            },
            //保养人选择回调事件
            changeUserFun(val) {
                this.form2.RepairingPerson = val[0].value;
                this.form2.RepairingPersonName = val[0].label;
            },
            //检验机台选择列表
            searchSpareParts() {
                this.form.ProductionMachineCode = "";
                this.form.ProductionMachine = "";
                var data = {
                    ParentResource: this.form.ProcessCode //工序编码
                }
                this.GetListByParentResource(data).then(res => {
                    this.ProductionMachineList = [];
                    if (res.success) {
                        if (res.resultData == null || res.resultData.length == 0) {
                            this.ProductionMachineList = [];
                        } else {
                            console.log(JSON.stringify(res.resultData));
                            res.resultData.forEach((item, index) => {
                                this.ProductionMachineList.push({
                                    value: item.ResourceCode,
                                    label: item.ResourceName
                                });
                            });
                        }
                    } else {
                        this.ProductionMachineList = [{
                            value: '',
                            label: '无'
                        }];
                    }
                });
            },
            //检验机台回调事件
            changeProductionMachine(val) {
                console.info('选择回调备件', JSON.stringify(val));
                this.form.ProductionMachineCode = val[0].value;
                this.form.ProductionMachine = val[0].label;
            },
            //检验方法选择列表
            searchTestMethodCoading() {
                this.form.TestMethodCoadingCode = "";
                this.form.TestMethodCoading = "";
                var data = {
                    FactoryCode: this.form.FactoryCode, //工厂
                    ProcessCode: this.form.ProcessCode, //工序编码
                    SmallClass: this.form.SmallClass, //物料小类
                    TestType: "2" //巡检:1 过程检验:2
                }
                console.info('检验方法查询参数', JSON.stringify(data));
                this.GetQCTestMethodList(data).then(res => {
                    this.TestMethodCoadingList = [];
                    if (res.success) {
                        if (res.resultData == null || res.resultData.length == 0) {
                            this.TestMethodCoadingList = [];
                        } else {
                            console.log(JSON.stringify(res.resultData));
                            res.resultData.forEach((item, index) => {
                                this.TestMethodCoadingList.push({
                                    value: item.TestMethodCoading,
                                    label: item.TestMethodName
                                });
                            });
                        }
                    } else {
                        this.TestMethodCoadingList = [{
                            value: '',
                            label: '无'
                        }];
                    }
                });
            },
            //检验方法回调事件
            changeTestMethodCoading(val) {
                console.info('选择回调备件', JSON.stringify(val));
                this.form.TestMethodCoadingCode = val[0].value;
                this.form.TestMethodCoading = val[0].label;
                //根据物料小类,检验工序和检验方法 获取检测项目
                this.searchGetEP_EquipmentMaintainDetailList();
            },
            //根据物料小类,检验工序获取检测项目
            searchGetEP_EquipmentMaintainDetailList() {
                let postdata = {
                    FactoryCode: this.form.FactoryCode, //工厂
                    SmallClass: this.form.SmallClass, //物料小类
                    ProcessCode: this.form.ProcessCode, //工序
                    TestMethodCoading: this.form.TestMethodCoadingCode, //检验方法
                    TestType: "2" //巡检:1 过程检验:2
                }
                console.info('根据物料小类,检验工序获取检测项目-参数', JSON.stringify(postdata));
                this.GetQCTestItemList(postdata).then(res => {
                    this.EP_EquipmentMaintainDetailList = [];
                    console.info('根据物料小类,检验工序获取检测项目-返回结果', JSON.stringify(res));
                    if (res && res.success) {
                        res.resultData.forEach((item, index) => {
                            this.EP_EquipmentMaintainDetailList.push({
                                "TestMethodCoading": item.TestMethodCoading, //检测方法
                                "TestItemCoading": item.TestItemCoading, //检测项目编码
                                "TestItemName": item.TestItemName, //检测项目名称
                                "TestItemStandard": item.TestItemStandard, //检测标准
                                "TestDepartment": item.TestDepartment, //检测部门
                                "DataType": item.DataType, //数据类型
                                "DataTypeName": item.DataTypeName, //数据类型名称
                                "TestItemResult": item.TestItemResult, //检测结果
                                "Options": item.Options //下拉数值
                            });
                        });
                    } else {
                        this.$refs.uToast.show({
                            title: '' + res.returnMsg,
                            type: 'warning',
                            icon: true
                        });
                    }
                })
            },
            //返回按钮 关闭弹窗
            exit() {
                this.show_shd = !this.show_shd;
            },
            //返回按钮 关闭弹窗
            exit2() {
                this.show_shd2 = !this.show_shd2;
            },
            Upclose() {},
            //end 弹窗选择*******************
            //点击事件 判断调用那个下拉框
            clickSelFun(val, item) {
                if (val == "process") {
                    if (!this.form.CardCode) {
                        this.$refs.uToast.show({
                            title: '请先扫描流转卡',
                            type: 'warning',
                            icon: true
                        });
                        return;
                    }
                    //工序
                    this.showProcess = true;
                } else if (val == "ProductionMachine") {
                    if (!this.form.CardCode) {
                        this.$refs.uToast.show({
                            title: '请先扫描流转卡',
                            type: 'warning',
                            icon: true
                        });
                        return;
                    }
                    if (!this.form.ProcessCode) {
                        this.$refs.uToast.show({
                            title: '请先选择检验工序',
                            type: 'warning',
                            icon: true
                        });
                        return;
                    }
                    //检验机台  选择显示 面板
                    this.isShowResult = true;
                } else if (val == "ProductionMachine2") {
                    //检验机台  选择显示 面板
                    this.isShowResult2 = true;
                } else if (val == "process2") {
                    //初始化工序列表
                    //this.getProcessList2();
                    //工序
                    this.showProcess2 = true;
                } else if (val == "TestMethodCoading") {
                    if (!this.form.CardCode) {
                        this.$refs.uToast.show({
                            title: '请先扫描流转卡',
                            type: 'warning',
                            icon: true
                        });
                        return;
                    }
                    if (!this.form.ProcessCode) {
                        this.$refs.uToast.show({
                            title: '请先选择检验工序',
                            type: 'warning',
                            icon: true
                        });
                        return;
                    }
                    //初始化检验方法列表
                    this.IsShowTestMethodCoading = true;
                } else if (val == "date") {
                    this.isShowDate = true;
                } else if (val == "date2") {
                    this.isShowDate2 = true;
                } else if (val == 'Determination') {
                    //判定显示
                    this.showDetermination = true;
                }
            },
            //选择判定
            changeDetermination(val) {
                this.form.DeterminationCode = val[0].value;
                this.form.Determination = val[0].label;
            },
            //设备下拉框选择事件回调
            changeCheckFun(val) {
                this.gridList = [];
                console.log(JSON.stringify(val))
                this.form.RepairingType = val[0].value; //val[0].label;
                this.form.RepairingTypeName = val[0].label;
            },

            //日期范围回调事件
            dateChange(e) {
                this.form2.date = e.startDate + " 至 " + e.endDate;
                this.form2.StartDate = e.startDate;
                this.form2.EndDate = e.endDate;
                console.log(e);
            },
            //记录查询
            search() {
                //清空记录查询表单
                this.form2 = {};
                //清空检验结果
                this.SparePartsItemDetailList = [];
                //清空记录查询明细表单
                this.form3 = {};
                //清空检验明细结果
                this.SparePartsItemDetailList2 = [];
                //隐藏保养执行弹窗
                this.show_shd = !this.show_shd;
                //初始化工序列表
                this.getProcessList2();
            },
            //初始化工序列表
            getProcessList2() {
                this.form2.ProcessCode = "";
                this.form2.ProcessName = "";
                var data = {
                    FactoryCode: this.$FactoryCode
                }
                this.GetProcessModel(data).then(res => {
                    this.processList2 = [];
                    if (res.success) {
                        if (res.resultData == null || res.resultData.length == 0) {
                            this.processList2 = [];
                        } else {
                            console.log(JSON.stringify(res.resultData));
                            res.resultData.forEach((item, index) => {
                                this.processList2.push({
                                    value: item.ResourceCode,
                                    label: item.ResourceName
                                });
                            });
                            //检验机台选择列表
                            this.searchSpareParts2()
                        }
                    } else {
                        this.processList2 = [{
                            value: '',
                            label: '无'
                        }];
                    }
                });
            },
            //选择工序
            changeProcess2(val) {
                this.form2.ProcessCode = val[0].value;
                this.form2.ProcessName = val[0].label;
                //初始化检验机台列表
                this.searchSpareParts2();
            },
            //检验机台选择列表
            searchSpareParts2() {
                this.form2.ProductionMachineCode = "";
                this.form2.ProductionMachine = "";
                var data = {
                    ParentResource: this.form2.ProcessCode //工序编码
                }
                this.GetListByParentResource(data).then(res => {
                    this.ProductionMachineList2 = [];
                    if (res.success) {
                        if (res.resultData == null || res.resultData.length == 0) {
                            this.ProductionMachineList2 = [];
                        } else {
                            console.log(JSON.stringify(res.resultData));
                            res.resultData.forEach((item, index) => {
                                this.ProductionMachineList2.push({
                                    value: item.ResourceCode,
                                    label: item.ResourceName
                                });
                            });
                        }
                    } else {
                        this.ProductionMachineList2 = [{
                            value: '',
                            label: '无'
                        }];
                    }
                });
            },
            //检验机台回调事件
            changeProductionMachine2(val) {
                console.info('选择回调备件', JSON.stringify(val));
                this.form2.ProductionMachineCode = val[0].value;
                this.form2.ProductionMachine = val[0].label;
            },
            //记录查询 执行
            jiluQuery() {
                if (!this.form2.ProductOrder) {
                    this.$refs.uToast.show({
                        title: '请输入订单号！',
                        type: 'warning',
                        icon: true
                    });
                    return;
                }

                if (!this.form2.ContainerNO) {
                    this.$refs.uToast.show({
                        title: '请输入柜号！',
                        type: 'warning',
                        icon: true
                    });
                    return;
                }

                // if (!this.form2.ProcessName) {
                //     this.$refs.uToast.show({
                //         title: '请选择工序！',
                //         type: 'warning',
                //         icon: true
                //     });
                //     return;
                // }

                if (!this.form2.StartDate) {
                    this.$refs.uToast.show({
                        title: '请输入检验日期范围！',
                        type: 'warning',
                        icon: true
                    });
                    return;
                }

                let postData = {
                    TestType: "2", //巡检:1 过程检验:2
                    "queryJson": {
                        ProductOrder: this.form2.ProductOrder, //订单号
                        ContainerNO: this.form2.ContainerNO, //柜号 
                        ProductionWorkshop: this.form2.ProcessCode, //工序
                        TestMachine: this.form2.ProductionMachineCode, //检验机台                    
                        StartTime: this.form2.StartDate, //开始日期
                        EndTime: this.form2.EndDate //结束日期
                    },
                }
                console.info('记录查询提交参数', JSON.stringify(postData));
                this.GetQCTestRecordList(postData).then(res => {
                    if (res && res.success) {
                        this.SparePartsItemDetailList = res.resultData;
                        console.info('检测结果列表', JSON.stringify(res.resultData));
                    } else {
                        this.$refs.uToast.show({
                            title: '' + res.returnMsg,
                            type: 'warning',
                            icon: true
                        });
                    }
                });
            },
            //查询检测明细结果
            showQCTestResultRecordList(item) {
                console.info('查询检测明细结果入参', JSON.stringify(item));
                this.form3 = item;
                let postData = {
                    TestType: "2", //巡检:1 过程检验:2
                    "queryJson": {
                        ParentId: item.Id //父ID
                    },
                }
                console.info('记录查询明细提交参数', JSON.stringify(postData));
                this.GetQCTestResultRecordList(postData).then(res => {
                    if (res && res.success) {
                        this.SparePartsItemDetailList2 = res.resultData;
                        console.info('检测明细结果列表', JSON.stringify(res.resultData));
                        this.show_shd2 = !this.show_shd2;
                    } else {
                        this.$refs.uToast.show({
                            title: '' + res.returnMsg,
                            type: 'warning',
                            icon: true
                        });
                    }
                });
            },
            //生成OQC检验记录
            SaveQCTestItemFormPost() {
                // if (!this.form.ProcessName) {
                //     this.$refs.uToast.show({
                //         title: '工序不能为空',
                //         type: 'warning',
                //         icon: true
                //     });
                //     return;
                // }
                // if (!this.form.ProductionMachine) {
                //     this.$refs.uToast.show({
                //         title: '检验机台不能为空',
                //         type: 'warning',
                //         icon: true
                //     });
                //     return;
                // }
                console.info('', JSON.stringify(this.EP_EquipmentMaintainDetailList));
                //检测保养项目是否都填写
                if (this.EP_EquipmentMaintainDetailList.length > 0) {
                    this.EP_EquipmentMaintainDetailList.forEach((item, index) => {
                        if (item.TestItemResult == '') {
                            this.$refs.uToast.show({
                                title: '请填写检验结果',
                                type: 'warning',
                                icon: true
                            });
                            return;
                        }
                        if (item.TestItemResult2 == '') {
                            this.$refs.uToast.show({
                                title: '请选择类别',
                                type: 'warning',
                                icon: true
                            });
                            return;
                        }
                    });
                }
                console.info('任务表单', JSON.stringify(this.form));
                console.info('检验项目', JSON.stringify(this.EP_EquipmentMaintainDetailList));
                // //提交数据
                // let posdata = {
                //     TestType: "2", //巡检:1  过程检验:2
                //     entity: {
                //         FlowCardId: this.form.CardCode, //流转卡
                //         CalibrationMethod: this.form.TestMethodCoadingCode, //检测方法
                //         TestProcess: this.form.ProcessCode, //检测工序 巡检:1 
                //         ProductionMachine: this.form.ProductionMachineCode, //检测机台 巡检:2                        
                //         ProductionWorkshop: this.form.ProcessCode, //检测工序 过程检验:2
                //         TestMachine: this.form.ProductionMachineCode, //检测机台 过程检验:2 
                //         Remark: this.form.Remark, //备注
                //         Inspector: this.loginInfo.result ? this.loginInfo.result.UserCode : 'App' //检测人
                //     },
                //     data: this.EP_EquipmentMaintainDetailList
                // };
                //console.info('提交保存内容', JSON.stringify(posdata));

                this.$refs.uToast.show({
                    title: '创建成功!',
                    type: 'success',
                    icon: true
                });
                //检验结论合格，提交后检验作业结束，可继续进行发货作业
                if (this.form.Determination == "合格") {
                    //返回首页
                    uni.switchTab({
                        url: "/pages/index/index",
                    });
                } else {
                    //检验结论不合格，提交后直接跳转成品返工作业界面，可继续创建成品返工单。当前托盘禁止进行发货作业。
                    //页面这间跳转
                    uni.navigateTo({
                        url: '/pages/WMSModel/WMS_FinishedPorductReturn?mtm=' + this.form.mtm + '&jyd=' +
                            this.form.jyd
                    })
                }

                return;
                this.OQC检验提交方法(posdata).then(res => {
                    console.log(JSON.stringify(res));
                    if (res && res.success) {
                        this.$refs.uToast.show({
                            title: '保存成功！',
                            type: 'success',
                            icon: true
                        });
                        //清空
                        this.form = {};
                        console.info('this.form', JSON.stringify(this.form));
                        this.ProductionMachineList = []; //检验机台列表
                        console.info('this.ProductionMachineList', JSON.stringify(this.ProductionMachineList));
                        this.processList = []; //工序列表
                        console.info('this.processList', JSON.stringify(this.processList));
                        this.actionSheetList = []; //检验项目 数据类型 下拉  
                        console.info('this.actionSheetList', JSON.stringify(this.actionSheetList));
                        this.EP_EquipmentMaintainDetailList = []; //检验项目 TABLE  
                        console.info('this.EP_EquipmentMaintainDetailList', JSON.stringify(this
                            .EP_EquipmentMaintainDetailList));

                        //检验结论合格，提交后检验作业结束，可继续进行发货作业
                        if (this.form.Determination == "合格") {
                            //返回首页
                            uni.switchTab({
                                url: "/pages/index/index",
                            });
                        } else {
                            //检验结论不合格，提交后直接跳转成品返工作业界面，可继续创建成品返工单。当前托盘禁止进行发货作业。
                            //页面这间跳转
                            uni.navigateTo({
                               url: '/pages/WMSModel/WMS_FinishedPorductReturn?mtm=' + this.form.mtm + '&jyd=' +
                                   this.form.jyd
                            })
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
            //返回页面
            goBack() {
                uni.switchTab({
                    url: "/pages/index/index",
                });
            }
        }
    }
</script>

<style lang="scss" scoped>
    Quality_QC_OQCCheck {
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
