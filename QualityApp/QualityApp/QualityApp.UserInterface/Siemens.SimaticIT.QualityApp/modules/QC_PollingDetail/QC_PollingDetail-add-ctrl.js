/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 巡检检验记录表
*  2. 创建人员： dragon
*  3. 创建日期： 2022-03-21
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_PollingDetail').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetail.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();
            initGridOptions1();
            initGridOptions2();


            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_1'));
            //sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }


        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.searchParams1 = {};
            self.TestMaintenance = {};

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.add = add;
            self.typeFactoryChange = typeFactoryChange;

            initDictionary();

        }

        function initDictionary() {
            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ItemValue: "" }]
            }
            // debugger
            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2')
                    });
                    initGridData1();
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.typeProcess.options = res.data.resultData;
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2')
                        });
                    }
                });
            } else {
                self.typeProcess = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }
        // //获取工序
        // function getProcess() {
        //     // debugger
        //     self.typeProcess = {
        //         value: { OperationName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), OperationCode: "" },
        //         options: [{ OperationName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), OperationCode: "" }]
        //     };
        //     var url = commonService.getMesApiAddress("quality") + "QC_PollingDetail/GetWorkOrderOperation";
        //     commonService.callWebApiPost(url, { workOrder: self.selectedItem1.WorkOrder }).then(function (res) {
        //         if (res && res.data.success) {
        //             self.typeProcess.options = res.data.resultData;
        //             self.typeProcess.options.splice(0, 0, {
        //                 OperationCode: "",
        //                 OperationName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2')
        //             });
        //         }
        //     });
        // }

        function search() {
            initGridData1();
        }


        function initGridOptions1() {
            self.gridOptionsItem1 = {
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_5'),
                        width: 80
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_7'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_8'),
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData1();
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                        } else {
                            self.selectedItem1 = null;
                            // self.typeProcess = {
                            //     value: { OperationName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), OperationCode: "" },
                            //     options: [{ OperationName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), OperationCode: "" }]
                            // };
                            self.gridOptionsItem2.data = [];
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData1() {
            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//单据类型
                sord: 'desc'
            };
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_9'));
                return;
            }
            self.searchParams1.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams1.OrderStatus = "5";
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams1
            };
            var url = commonService.getMesApiAddress("plan") + "PL_WorkOrder/PL_WorkOrderPageDataTableList";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    //总条数
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }

        function initGridOptions2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_12'),
                        width: 200
                    },
                    // {
                    //     field: 'DataTypeName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_13'),
                    //     width: 140
                    // },
                    {
                        field: 'TestItemResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_14'),
                        cellTemplate: '<div ng-show="row.entity.DataType==1"><sit-numeric  sit-value="row.entity.TestItemResult" ></sit-numeric></div>' +
                            '<div ng-show="row.entity.DataType==2"><sit-text sit-value="row.entity.TestItemResult" ></sit-text></div>' +
                            '<div ng-show="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.TestItemResult"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>' +
                            '<div ng-show="row.entity.DataType>3"><sit-select sit-value="row.entity.typeResultSelect.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeResultSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                            self.IsShowDeleteButten = true;
                            //setButtonsVisibility(true);

                        } else {
                            self.selectedItem2 = null;
                            self.IsShowDeleteButten = false;
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi2.core.handleWindowResize();
                        $scope.gridApi2.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function add() {
            initGridData2();
        }

        function initGridData2() {

            var postData = {
                processCode: self.typeProcess.value.ResourceCode,
                materialCode: self.selectedItem1.MaterialCode
            }
            let url = commonService.getMesApiAddress("quality") + 'QC_PollingDetail/GetTestItemMaintenance';
            commonService.callWebApiPost(url, postData).then(function (result) {
                debugger;
                if (result && result.data.success) {
                    self.TestMaintenance = result.data.resultData.main;
                    let data = result.data.resultData.detail;
                    data.forEach(item => {
                        if (item.DataType > 3) {
                            self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2'), ItemValue: "" }];
                            var arr = item.DataTypeName.split("/");
                            for (var i = 0; i < arr.length; i++) {
                                self.typeSelect.options.push({
                                    ItemName: arr[i], ItemValue: i
                                })
                            }
                            item.typeResultSelect = angular.copy(self.typeSelect);
                        }
                    });
                    self.gridOptionsItem2.data = data;
                }
            })
        }

        //保存
        function save() {
            if (!self.selectedItem1) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_15'));
                return;
            }
            var itemvalueisnull = "";
            debugger
            var data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_16'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_17'));
                busyIndicatorService.hide();
                return false;
            }

            var flag = false;

            data.forEach(item => {
                item.FactoryCode = self.selectedItem1.FactoryCode;
                item.FactoryName = self.selectedItem1.FactoryName;
                if (item.DataType == "3") { //时间
                    item.TestItemResult = commonService.ConvertToLocalTime(item.TestItemResult);
                }
                // 下拉框
                if (item.DataType > 3 && item.typeResultSelect.value != undefined) {
                    item.TestItemResult = item.typeResultSelect.value.ItemName;
                }

                if (!item.TestDepartment) flag = true;

                // if (item.TestItemResult == undefined) {

                //     backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_18'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_17'));
                //     itemvalueisnull = 1;
                // } else if (item.TestItemResult == commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_2')) {
                //     backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_18'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_17'));
                //     itemvalueisnull = 1;
                // }
            });

            // if (itemvalueisnull == '1') {
            //     return false;
            // }

            // if (flag) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_19'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_17'));
            //     busyIndicatorService.hide();
            //     return false;
            // }

            self.selectedItem1.TestProcess = self.typeProcess.value.ResourceCode;
            self.selectedItem1.CalibrationMethod = self.TestMaintenance.TestMethodCoading;
            self.selectedItem1.Remark = self.currentItem.Remark;
            var postData = {
                Entity: self.selectedItem1,
                data: data
            };

            var url = commonService.getMesApiAddress("quality") + 'QC_PollingDetail/PollingDetailAndResultSave';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_20'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_17'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_17'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_PollingDetail_QC_PollingDetail';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_PollingDetail';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_PollingDetail-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_PollingDetail.QC_PollingDetailaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
