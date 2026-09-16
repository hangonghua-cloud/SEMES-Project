/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备点检记录
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecord.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            self.currentItem = {};
            //self.currentItem.EquipmentId = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            initGridOptions();
            //getDataItemDetailList();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_2'));
            //sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = {};
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.SelectEquipmentModal = SelectEquipmentModal;
            self.getDataItemDetailList = getDataItemDetailList;
            //self.SelectTaskModal = SelectTaskModal;
            //self.SelectPersonModal = SelectPersonModal;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            initDictionary();
        }

        //点检任务
        function GetTaskList() {
            debugger
            var url = commonService.getMesApiAddress("equipment") + "EP_EquipmentCheckRecord/GetCheckTaskByEquipmentList?equipmentId=" + self.currentItem.EquipmentId;
            //console.log(JSON.stringify(postData));
            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.TaskConfig.options = jsonData;
                    if (jsonData.length > 0)
                        self.TaskConfig.selectedOption = { CheckTaskId: jsonData[0].CheckTaskId, CheckTaskName: jsonData[0].CheckTaskName };
                } else {
                    console.log(self.TaskConfig.options);
                }
                //self.SecondValue = jsonData[0];
                self.TaskConfig.options.splice(0, 0, { CheckTaskId: "", CheckTaskName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_3') })
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_3'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_3')
                    });
                }
            });

            //点检结论
            self.ResultConfig = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_5') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_6') }
                ]
            };
            /*self.ResultConfig2 = {
                value: null,
                selectedOption: {ItemCode: "", ItemName:commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4')},
                options: [
                    {ItemCode: "", ItemName:commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4')},
                    {ItemCode: "1", ItemName:commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_5')},
                    {ItemCode: "2", ItemName:commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_6')}
                ]
            };*/
            self.ResultConfig3 = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_7') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_8') }
                ]
            };
            self.ResultConfig4 = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_9') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_10') }
                ]
            };
            self.ResultConfig2 = {
                value: null,
                selectedOption: { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                options: [
                    { ItemCode: "", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_4') },
                    { ItemCode: "1", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_5') },
                    { ItemCode: "2", ItemName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_6') }
                ]
            };
            self.TaskConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
        }

        function initGridOptions() {
            self.gridOptions = {
                fastWatch: true,
                rowHeight: 35,
                minimumColumnSize: 100,
                enableMultiSelection: false,
                enableFiltering: false,
                //基础属性
                enableSorting: true,//是否支持排序(列)
                useExternalSorting: false,//是否支持自定义的排序规则      
                enableGridMenu: false,//是否显示表格 菜单
                showGridFooter: false,//时候显示表格的footer
                enableHorizontalScrollbar: 1,//表格的水平滚动条
                enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)
                selectionRowHeaderWidth: 30,
                enableCellEditOnFocus: false,//default为false,true的时候单击即可打开编辑(cellEdit为true的时候,需要引入'ui.grid.cellNav')
                //分页属性
                enablePagination: false, //是否分页,default为true
                enablePaginationControls: false, //使用默认的底部分页
                paginationPageSizes: [100, 200, 300], //每页显示个数选项
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中         
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'Id',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_11'),
                        width: 90,
                        visible: false
                    },
                    {
                        field: 'CheckTaskId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_12'),
                        width: 80,
                        visible: false
                    },
                    {
                        field: 'CheckTaskName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_13'),
                        width: 90,
                        visible: false
                    },
                    {
                        field: 'CheckItemId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_14'),
                        width: 80,
                        visible: false
                    },
                    // {
                    //     field: 'FactoryName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_15'),
                    //     width: 120
                    // },
                    {
                        field: 'CheckItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_16'),
                        width: 200
                    },
                    {
                        field: 'CheckItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_17'),
                        width: 200
                    },
                    {
                        field: 'DataType',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_18'),
                        width: 80,
                        visible: false
                    },
                    {
                        field: 'CheckResult',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_19'),
                        width: 200,
                        //enableCellEdit: true, pinnedLeft: true,
                        cellTemplate: '<div class="ui-grid-cell-contents" ng-if="!row.entity.editrow">' +
                            '{{row.entity.CheckResult}}</div>' +
                            '<div ng-show="row.entity.DataType==\'2\'">' +
                            '<sit-text sit-value="row.entity.CheckResult"> ' +
                            '</sit-text>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'1\'">' +
                            '<sit-numeric sit-value="row.entity.CheckResult"> ' +
                            '</sit-numeric>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'4\'">' +
                            '<sit-select sit-value="row.entity.CheckResult.selectedOption"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.CheckResult.options"' +
                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemCode\'">' +
                            '</sit-select>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'5\'">' +
                            '<sit-select sit-value="row.entity.CheckResult.selectedOption"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.CheckResult.options"' +
                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemCode\'">' +
                            '</sit-select>' +
                            '</div>' +
                            '<div ng-show="row.entity.DataType==\'6\'">' +
                            '<sit-select sit-value="row.entity.CheckResult.selectedOption"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.CheckResult.options"' +
                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemCode\'">' +
                            '</sit-select>' +
                            '</div>'
                    }
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row.isSelected) {
                            //self.isDelDisplay=true;
                            self.selectedDetail = row.entity;
                            /*if(deleteArr.indexOf(row.entity.Id) == -1){
                                deleteArr.push(row.entity.Id);
                            }*/
                        }
                        else {
                            //self.isDelDisplay=false;
                            //deleteArr.remove(row.entity.Id);
                        }
                    });
                },
                data: []
            }

        }

        function getDataItemDetailList(oldValue, newValue) {
            debugger
            if (newValue.CheckTaskId == "")
                return;

            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckResult/GetResultPageList';
            var postCode = {
                queryJson: {
                    ParentId: "",
                    CheckTaskId: newValue.CheckTaskId
                }
            };
            console.log(JSON.stringify(postCode));
            commonService.callWebApiPost(url, postCode).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    var arrLeng = 0;
                    self.gridOptions.data = [];
                    for (var icount = 0; icount < resultData.length; icount++) {
                        if (resultData[icount].DataType == "4") {
                            self.ResultConfig.selectedOption = { ItemCode: resultData[icount].CheckResult, ItemName: resultData[icount].CheckResultValue };
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                FactoryName: self.typeFactory.value.ResourceName,
                                CheckTaskId: resultData[icount].CheckTaskId,
                                CheckTaskName: resultData[icount].CheckTaskName,
                                CheckItemId: resultData[icount].CheckItemId,
                                CheckItemName: resultData[icount].CheckItemName,
                                CheckItemStandard: resultData[icount].CheckItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                CheckResult: angular.copy(self.ResultConfig),
                                editrow: true
                            });
                        }
                        else if (resultData[icount].DataType == "5") {
                            self.ResultConfig.selectedOption = { ItemCode: resultData[icount].CheckResult, ItemName: resultData[icount].CheckResultValue };
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                FactoryName: self.typeFactory.value.ResourceName,
                                CheckTaskId: resultData[icount].CheckTaskId,
                                CheckTaskName: resultData[icount].CheckTaskName,
                                CheckItemId: resultData[icount].CheckItemId,
                                CheckItemName: resultData[icount].CheckItemName,
                                CheckItemStandard: resultData[icount].CheckItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                CheckResult: angular.copy(self.ResultConfig3),
                                editrow: true
                            });
                        }
                        else if (resultData[icount].DataType == "6") {
                            self.ResultConfig.selectedOption = { ItemCode: resultData[icount].CheckResult, ItemName: resultData[icount].CheckResultValue };
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                FactoryName: self.typeFactory.value.ResourceName,
                                CheckTaskId: resultData[icount].CheckTaskId,
                                CheckTaskName: resultData[icount].CheckTaskName,
                                CheckItemId: resultData[icount].CheckItemId,
                                CheckItemName: resultData[icount].CheckItemName,
                                CheckItemStandard: resultData[icount].CheckItemStandard,
                                DataType: resultData[icount].DataType,
                                //CheckResult: resultData[icount].CheckResult,
                                CheckResult: angular.copy(self.ResultConfig4),
                                editrow: true
                            });
                        }
                        else {
                            self.gridOptions.data.push({
                                Id: resultData[icount].Id,
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                FactoryName: self.typeFactory.value.ResourceName,
                                CheckTaskId: resultData[icount].CheckTaskId,
                                CheckTaskName: resultData[icount].CheckTaskName,
                                CheckItemId: resultData[icount].CheckItemId,
                                CheckItemName: resultData[icount].CheckItemName,
                                CheckItemStandard: resultData[icount].CheckItemStandard,
                                DataType: resultData[icount].DataType,
                                CheckResult: resultData[icount].CheckResult,
                                //CheckResultValue: angular.copy(self.ResultConfig),
                                editrow: true
                            });
                        }
                        arrLeng++;
                    }

                } else {
                    self.gridOptions.data = [];

                }
            }, function (error) {

            });
        }

        //选择设备  使用公用方法
        function SelectEquipmentModal() {
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_20'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                {
                    field: 'EquipmentId',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_21'),
                    width: 200
                },
                {
                    field: 'EquipmentName',
                    displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_22'),
                    width: 200
                }
            ];
            //queryName: "",
            //queryCode: ""
            /*功能描述:单选弹窗方法
            *创    建:刘万军
            *创建时间:2021-1-22
            *参    数:PostUrl API接口
            *         sidx  排序字段
            *         sord  排序方式
            *         columnDefs   grid显示字段列表
            *         callback  回调方法
            */
            let myParameters = {
                FactoryCode: self.typeFactory.value.ResourceCode
            }
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_21'), commonService.getMesApiAddress("equipment") + "EquipmentManage/GetPage_Equipment", [{ 'FieldCode': 'EquipmentId', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_21'), 'FiledType': 'Text' }, { 'FieldCode': 'EquipmentName', 'FileldName': commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_22'), 'FiledType': 'Text' }], "EquipmentId", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback, myParameters);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalEquipment_callback(res) {
            //alert(JSON.stringify(res));
            debugger
            self.currentItem.EquipmentName = res.EquipmentName;
            self.currentItem.EquipmentId = res.EquipmentId;
            GetTaskList();
        }



        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            var data = self.gridOptions.data;
            if (self.ResultConfig2.selectedOption.ItemCode == "") {
                notificationService.waring(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_23'));
                return;
            }
            else {
                self.currentItem.CheckConclusion = self.ResultConfig2.selectedOption.ItemCode;
            }
            /*if(self.ResultConfig2.selectedOption.ItemCode == ""){
                notificationService.waring(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_24'))
            }*/
            if (self.TaskConfig.selectedOption != null) {
                self.currentItem.CheckTaskId = self.TaskConfig.selectedOption.CheckTaskId;
                self.currentItem.CheckTaskName = self.TaskConfig.selectedOption.CheckTaskName;
            }

            for (var icount = 0; icount < data.length; icount++) {
                data[icount].Creator = self.UserCode;
                if (data[icount].CheckResult != null && data[icount].CheckResult.selectedOption != null) {
                    data[icount].CheckResult = data[icount].CheckResult.selectedOption.ItemCode;
                }
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_25') });
            //字典类型 取值参考
            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;


            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.TypeInPerson = self.UserCode;
            //self.currentItem.CheckConclusion = self.ResultConfig.selectedOption.ItemCode;
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                List: data
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentCheckRecord/SaveEP_EquipmentCheckRecord';
            console.log("url----------------" + url);
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            busyIndicatorService.hide();
        }

        //取消
        function cancel() {
            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_26'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_27'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentCheckRecord.EP_EquipmentCheckRecordaddctrl.Tips_27'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentCheckRecord_EP_EquipmentCheckRecord';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentCheckRecord';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentCheckRecord-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            }
        };
        $stateProvider.state(state);
    }
}());
