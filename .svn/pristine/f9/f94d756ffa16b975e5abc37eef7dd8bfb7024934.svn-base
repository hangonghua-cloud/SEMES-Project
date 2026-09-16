(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.ExeWorkOrderSW.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initAddGridOptions();
            registerEvents();

            sidePanelManager.setTitle($stateParams.title);
            //sidePanelManager.open('e');
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.processType = angular.copy($stateParams.processType);
            self.process = "";//挤出工序编码
            //Initialize Model Data
            self.currentItem = null;
            self.validInputs = false;
            self.searchParams = {};
            self.factoryData = [];

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeFactoryChange = typeFactoryChange;
            self.searchButtonHandler = initAddGridData;
            initDictionary();
        }

        function initDictionary() {

            self.typeMachine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" }]
            };
            self.typeAssignStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_2'), ItemValue: "1" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ItemValue: "" }]
            };
            self.typeStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_3'), ItemValue: "1" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_3'), ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_4'), ItemValue: "2" }
                ]
            };
            commonService.getDataItemDuatil("AssignStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeAssignStatus.options = res.data.resultData;
                    //self.typeAssignStatus.value = { ItemValue: res.data.resultData[1].ItemValue, ItemName: res.data.resultData[1].ItemName };
                }
            })

            // commonService.getAllFactoryProcessMachine().then(function (res) {
            //     if (res && res.data.success) {
            //         self.factoryData = res.data.resultData;
            //         let machineList = self.factoryData.filter(item => {
            //             return item.ProcessCode == self.process
            //         }).map(item => { return Object.assign({}, { 'ResourceCode': item.MachineCode, 'ResourceName': item.MachineName }) });
            //         self.typeMachine.options = machineList;
            //         self.typeMachine.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1')
            //         });
            //     }
            //     else {
            //         self.factoryData = [];
            //     }
            // });
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1')
                    });
                }
            });
        }

        //工厂变更
        function typeFactoryChange(oldItem, newItem) {

            let data = {
                factoryCode: newItem.ResourceCode,
                fieldCode: "PGGX",
                fieldValue: self.processType
            };

            commonService.getProcessByFactoryExtendInfo(data).then(function (res) {
                if (res && res.data.success) {
                    if (res.data.resultData.length > 0) {
                        self.process = res.data.resultData[0].ResourceCode;
                        commonService.getResourceListByParentResource({ ParentResource: self.process }).then(function (res) {
                            if (res && res.data.success) {
                                self.typeMachine.options = res.data.resultData;
                                self.typeMachine.options.splice(0, 0, {
                                    ResourceCode: "",
                                    ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1')
                                });
                            }
                        });
                        if (self.process) {
                            initAddGridData();
                        } else {
                            self.gridOptionsAdd.data = [];
                        }
                    } else {
                        self.typeMachine = {
                            value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" },
                            options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" }]
                        };
                        self.gridOptionsAdd.data = [];
                    }
                }
            });
        }

        function initAddGridOptions() {
            self.gridOptionsAdd = {
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
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_5'), width: 60, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_7'),
                        width: 110
                    },
                    // {
                    //     field: 'Status',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_8'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.Status==\'1\'"><span ng-cell-text>未开始</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.Status==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.Status==\'3\'"><span ng-cell-text>已完成</span></div>'
                    // },
                    {
                        field: 'AssignStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_12'),
                        width: 110,
                    },
                    // {
                    //     field: 'ProductOrder',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_15'),
                    //     width: 150
                    // },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_16'),
                        width: 150
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_18'),
                        width: 150
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_19'),
                        width: 150
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_20'),
                        width: 150
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_21'),
                        width: 150
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_22'),
                        width: 150
                    },
                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 150
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_23'),
                        width: 150
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_24'),
                        width: 100
                    },
                    {
                        field: 'ActualSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_25'),
                        width: 120
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initAddGridData();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItem = row.entity;
                                self.isButtonVisible = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initAddGridData() {

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_26'));
                return;
            }

            let Pagination = {
                rows: self.gridOptionsAdd.paginationPageSize,
                page: self.gridOptionsAdd.paginationCurrentPage,
                sidx: 'ProductOrder Desc,CAST(ContainerNO as int)',//排序字段
                sord: 'asc'
            };

            //  self.searchParams.Process = self.processType;
            self.searchParams.ProcessCode = self.process;
            self.searchParams.AssignStatus = self.typeAssignStatus.value.ItemValue;
            // self.searchParams.Status = self.typeStatus.value.ItemValue;
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/GetExeWorkOrderDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {

                    self.gridOptionsAdd.data = res.data.resultData.rows;
                    self.gridOptionsAdd.totalItems = res.data.resultData.records;
                } else {
                    self.gridOptionsAdd.data = [];
                }
            }, function (error) {
                backendService.genericError('数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_27'));
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //新增保存
        function save() {


            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length < 1) {
                busyIndicatorService.hide();
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_28'), commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_29'));
                return
            }

            var AssignStatus = ""
            var msg = "";
            rows.forEach((item, index, arr) => {
                if (AssignStatus == "") AssignStatus = item.AssignStatus;
                else if (AssignStatus != "" && AssignStatus != item.AssignStatus) msg = commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_30');
                if (item.AssignStatus == "2") msg = commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_31')
            });
            if (!!msg) {
                busyIndicatorService.hide();
                backendService.genericError(msg, commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_29'));
                return;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                MachineCode: self.typeMachine.value.ResourceCode,
                PlanProductTime: commonService.ConvertToLocalDate(self.PlanProductTime),
                data: rows
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_32') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/InsertBatchPM_ExeWorkOrderSW';
            //提交数据
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
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_33'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_29'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_29'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form1") {
                self.validInputs = params.validity;
            }
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ExeWorkOrderSW_ExeWorkOrderSW';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ExeWorkOrderSW';

        var state = {
            name: screenStateName + '.add',
            url: '/add/:processType',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ExeWorkOrderSW-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_34'
            },
            params: {
                title: null,
                process: null,
            }
        };
        $stateProvider.state(state);
    }
}());
