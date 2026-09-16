(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions();
            initGridOptionsDetail();
            initGridData();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            self.workOrderList = angular.copy($stateParams.selectedItem);
            self.currentItem = self.workOrderList[0];
            self.isDetailButtonVisible = false;
            self.selectedItemDetail = null
            self.selectedItem = null;

            self.validInputs = false;
            self.isReadNum = true;
            self.isGet = 1;//是否获取最新bom
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeProcessChange = typeProcessChange;
            // self.typeBomChange = typeBomChange;

            self.addItemButtonHandler = addForm;
            self.editItemButtonHandler = editForm;
            self.deleteItemButtonHandler = deleteForm;
            self.selectItemButtonHandler = selectItemButtonHandler;

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function initDictionary() {

            self.typeProcessOperation = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_2'), ProcessCode: "" }]
            };
            self.typeStartProcess = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_2'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("Process").then(function (res) {
                if (res && res.data.success) {
                    self.typeStartProcess.options = res.data.resultData;
                    self.typeStartProcess.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcessOperation.options = res.data.resultData;
                    self.typeProcessOperation.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_2')
                    });
                }
            })
        }

        function initGridOptions() {
            self.gridOptions = {

                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                //minRowsToShow: 23,
                rowHeight: 33,
                useExternalPagination: false,//true:使用外部分页方式；false:使用UI Grid内部分页方式
                useExternalSorting: false,//true:使用外部排序方式，false:使用UI Gird内部排序方式
                multiSelect: false,
                enableRowSelection: true,
                enableRowHeaderSelection: false,
                enableColumnResizing: true,//允许调整列宽
                appScopeProvider: self,
                enableFiltering: false,
                columnDefs: [

                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_3'),
                        width: 140,
                        cellTemplate:
                            '<sit-select sit-value="row.entity.typeProcess.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeProcess.options"' +

                            'sit-change="row.entity.typeProcessChange"' +
                            'sit-to-display="\'ProcessName\'"' +
                            'sit-to-keep="\'ProcessCode\'">' +
                            '</sit-select>'
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_4'),
                        width: 100,
                        cellTemplate:
                            '<sit-select sit-value="row.entity.typeStartOperation.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeStartOperation.options"' +
                            'sit-to-display="\'OperationName\'"' +
                            'sit-to-keep="\'OperationCode\'">' +
                            '</sit-select>'
                    },
                    {
                        field: 'BomCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_5'),
                        width: 140,
                        cellTemplate:
                            '<sit-select sit-value="row.entity.typeBom.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeBom.options"' +
                            // 'sit-change="row.entity.typeBomChange"' +
                            'sit-to-display="\'BOMCode\'"' +
                            'sit-to-keep="\'Id\'">' +
                            '</sit-select>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_8'),
                        width: 100
                    },
                    {
                        field: 'OrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_9'),
                        width: 110
                    },

                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_11'),
                        width: 110
                    },

                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'OrderStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_13'),
                        width: 110,
                        // cellTemplate:
                        //     '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>发布</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已发料</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>生产中</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'6\'"><span ng-cell-text>已完成</span></div>'

                    },

                    // {
                    //     field: 'StartOperationName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_4'),
                    //     width: 110,
                    // },
                    {
                        field: 'UnitNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_20'),
                        width: 150
                    },


                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected === true) {
                            self.selectedOption = row.entity;
                            //setButtonsVisibility(true);
                        } else {
                            self.selectedOption = null;
                            //setButtonsVisibility(false);
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {

            var postData = {
                queryJson: {
                    WorkOrder: self.currentItem.WorkOrder
                }
            }
            var url = commonService.getMesApiAddress("plan") + "PL_BOM/GetWorkOrderBom";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    var data = res.data.resultData.rows;
                    self.selectedItem = angular.copy(data[0]);
                    let workOrder = self.selectedItem.WorkOrder;
                    loadWorkOrderBomItem(workOrder);
                    //工艺路线
                    var url1 = commonService.getMesApiAddress("material") + "BS_BOM/BS_BOMPageDataTableList";
                    commonService.callWebApiPost(url1, {
                        queryJson: {
                            FactoryCode: data[0].FactoryCode,
                            MaterialCode1: data[0].MaterialCode,
                            OrderType: data[0].OrderType
                        }
                    }).then(function (resroute) {

                        let arrnew = resroute.data.resultData.rows.map(item => {
                            return Object.assign({}, { 'ProcessCode': item.Process, 'ProcessName': item.ProcessName });
                        });
                        if (resroute && resroute.data.success) {
                            data[0].typeProcess = {
                                options: arrnew,
                                value: arrnew.find(t => t.ProcessCode == data[0].Process)
                            }
                            // data[0].typeBomChange = typeBomChange;
                            data[0].typeProcessChange = typeProcessChange;
                            self.gridOptions.data = data;
                            typeProcessChange(null, data[0].typeProcess.value);
                            //typeBomChange(null,data[0].typeBom.value);
                        }
                    });
                }
            });
        }

        function typeProcessChange(oldval, newvalue) {
            console.log(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_21'));
            var entity = self.gridOptions.data[0];
            if (!!newvalue) {
                var postData = {
                    queryJson: {
                        OrderType: entity.OrderType,
                        FactoryCode: entity.FactoryCode,
                        MaterialCode1: entity.MaterialCode,
                        Process: newvalue.ProcessCode
                    }
                }
                var url = commonService.getMesApiAddress("material") + "BS_BOM/BS_BOMPageList";
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if (res && res.data.success) {
                        // var ent1 = self.gridOptions.data[0];
                        // var bom = res.data.resultData.rows.find(t => t.BOMCode == ent1.BOMCode);
                        // if (bom != null) {
                        //     bom.Id = ent1.BOMCode;
                        //     self.gridOptions.data[0].typeBom = {
                        //         options: res.data.resultData.rows,
                        //         value: { BOMCode: bom.BOMCode, Id: bom.Id }
                        //     }
                        // } else {
                        //     self.gridOptions.data[0].typeBom = {
                        //         options: [
                        //             { BOMCode: ent1.BOMCode, Id: ent1.BOMCode }],
                        //         value: { BOMCode: ent1.BOMCode, Id: ent1.BOMCode }
                        //     }
                        // }
                        let selectedBomItem = res.data.resultData.rows[0];
                        self.gridOptions.data[0].typeBom = {
                            options: [{ BOMCode: selectedBomItem.BOMCode, Id: selectedBomItem.BOMCode }],
                            value: { BOMCode: selectedBomItem.BOMCode, Id: selectedBomItem.BOMCode }
                        };

                        var bomId = selectedBomItem.Id;
                        loadNeBomItems(bomId);
                    }
                });
                loadProcess(newvalue.ProcessCode);
            }
        }
        //获取工艺路线下的工序
        function loadProcess(processCode) {

            var postData2 = {
                queryJson: {
                    ProcessCode: processCode
                }
            }
            var url2 = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/BS_ProcessOfOperationsPageDataTableList';
            commonService.callWebApiPost(url2, postData2).then(function (resProcess) {

                if (resProcess && resProcess.data.success) {
                    self.gridOptions.data[0].typeStartOperation = {
                        options: resProcess.data.resultData.rows,
                        value: resProcess.data.resultData.rows.find(t => t.OperationCode == self.gridOptions.data[0].StartOperation)
                    }

                }
            })
        }

        function typeBomChange(oldval, newval) {
            console.log(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_22'));
            if (!!newval) {
                var entity = self.gridOptions.data[0];
                if (newval.BOMCode == newval.Id) {
                    //初始化的时候查询绑定工单的bomitems
                    var postData = {
                        queryJson: {
                            WorkOrder: entity.WorkOrder
                        }
                    }
                    var url = commonService.getMesApiAddress("plan") + "PL_BOM/GetWorkOrderBomItem";
                    commonService.callWebApiPost(url, postData).then(function (res) {
                        if (res && res.data.success) {
                            self.gridOptionsDetail.data = res.data.resultData.rows;
                        }
                    })
                } else {
                    //改变的时候后查询新的bom
                    loadNeBomItems(newval.Id);
                }
            }
        }

        function loadWorkOrderBomItem(workOrder) {
            //初始化的时候查询绑定工单的bomitems
            var postData = {
                queryJson: {
                    WorkOrder: workOrder
                }
            }
            var url = commonService.getMesApiAddress("plan") + "PL_BOM/GetWorkOrderBomItem";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                }
            })
        }

        //获取bom
        function loadNeBomItems(bomId) {

            if (self.isGet != 1) {
                var postData = {
                    queryJson: {
                        BOMCode: self.gridOptions.data[0].typeBom.value.BOMCode,
                        BOMId: bomId,
                        OrderType: self.gridOptions.data[0].OrderType
                    }
                }
                var url = commonService.getMesApiAddress("material") + "BS_BOMItems/BS_BOMItemsPageDataTableList";
                commonService.callWebApiPost(url, postData).then(function (res) {
                    if (res && res.data.success) {
                        self.gridOptionsDetail.data = res.data.resultData.rows;
                    }
                })
            }
            self.isGet += 1;
        }

        //获取最新Bom
        function selectItemButtonHandler() {
            loadNeBomItems(null);
        }

        function initGridOptionsDetail() {
            self.gridOptionsDetail = {
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
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_23'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_24'),
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_25'),
                        width: 120
                    },
                    {
                        field: 'BOMCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_26'),
                        width: 120
                    },
                    {
                        field: 'MaterialClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_27'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_28'),
                        width: 100
                    },
                    {
                        field: 'Num',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_29'),
                        width: 100
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_30'),
                        width: 100
                    },
                    {
                        field: 'WarehouseName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_31'),
                        width: 140
                    },
                    {
                        field: 'ProcessCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_32'),
                        width: 130
                    },
                    {
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_33'),
                        width: 130
                    },
                    {
                        field: 'WFMarkName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_40'),
                        width: 130
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;

                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function addForm() {
            var modalInstance = commonService.openModel({
                templateUrl: 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage/AddBomItem.html',
                controller: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.AddBomItem',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {};
                    }
                }
            });
            modalInstance.result.then(function (res) {
                console.log(res);
                var data = self.gridOptionsDetail.data;
                var ent = data.find(t => t.MaterialCode == res.entity.MaterialCode);
                if (ent != null) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_34'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_35'));
                    return false;
                } else {
                    self.gridOptionsDetail.data.push({
                        MaterialCode: res.entity.MaterialCode,
                        MaterialName: res.entity.MaterialName,
                        Spec: res.entity.Spec,
                        BOMCode: res.entity.BOMCode,
                        MaterialCode: res.entity.MaterialCode,
                        MaterialClass: res.entity.MaterialClass,
                        MaterialClassName: res.entity.MaterialClassName,
                        SmallClass: res.entity.SmallClass,
                        SmallClassName: res.entity.SmallClassName,
                        Num: res.entity.Num,
                        Unit: res.entity.Unit,
                        UnitName: res.entity.UnitName,
                        Warehouse: res.entity.Warehouse,
                        WarehouseName: res.entity.WarehouseName,
                        ProcessCode: res.entity.ProcessCode,
                        ProcessName: res.entity.ProcessName,
                    })
                }
            });
        }

        function editForm() {
            if (!self.selectedItemDetail) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_36'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_35'));
                return false;
            }
            var modalInstance = commonService.openModel({
                templateUrl: 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage/EditBomItem.html',
                controller: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.EditBomItem',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            selectedItem: self.selectedItemDetail
                        };
                    }
                }
            });
            modalInstance.result.then(function (res) {

                //var data = self.gridOptionsDetail.data;
                // var ent = data.find(t => t.MaterialCode == res.entity.MaterialCode);
                self.selectedItemDetail.MaterialCode = res.entity.MaterialCode;
                self.selectedItemDetail.MaterialName = res.entity.MaterialName;
                self.selectedItemDetail.MaterialName = res.entity.Spec;
                self.selectedItemDetail.BOMCode = res.entity.BOMCode;
                self.selectedItemDetail.MaterialCode = res.entity.MaterialCode;
                self.selectedItemDetail.MaterialClass = res.entity.MaterialClass;
                self.selectedItemDetail.MaterialClassName = res.entity.MaterialClassName;
                self.selectedItemDetail.SmallClass = res.entity.SmallClass;
                self.selectedItemDetail.SmallClassName = res.entity.SmallClassName;
                self.selectedItemDetail.Num = res.entity.Num;
                self.selectedItemDetail.Unit = res.entity.Unit;
                self.selectedItemDetail.UnitName = res.entity.UnitName;
                self.selectedItemDetail.Warehouse = res.entity.Warehouse;
                self.selectedItemDetail.WarehouseName = res.entity.WarehouseName;
                self.selectedItemDetail.ProcessCode = res.entity.ProcessCode;
                self.selectedItemDetail.ProcessName = res.entity.ProcessName;
                self.selectedItemDetail.WFMark = res.entity.WFMark;
                self.selectedItemDetail.WFMarkName = res.entity.WFMarkName;
            });
        }

        function deleteForm() {
            self.gridOptionsDetail.data = _.filter(self.gridOptionsDetail.data, function (item) {
                return item.MaterialCode != self.selectedItemDetail.MaterialCode;
            })
        }

        function save() {

            //字典类型 取值参考
            var data1 = self.gridOptions.data;
            var data2 = self.gridOptionsDetail.data;

            var entity = {
                WorkOrder: data1[0].WorkOrder,
                Process: data1[0].typeProcess.value.ProcessCode,
                StartOperation: data1[0].typeStartOperation.value.OperationCode,
                BOMCode: data1[0].typeBom.value.BOMCode
            }
            data2.forEach((item, index, arr) => {
                item.BOMId = data1[0].PLBOMId,
                    item.ConsumeProcess = item.ProcessCode
            });

            var postData = {
                IsUpdateProcess: self.selectedItem.Process != entity.Process ? "1" : "0",//判断是否修改工艺
                IsUpdateBom: self.selectedItem.BOMCode != entity.BOMCode ? "1" : "0",//判断是否修改Bom
                BOMCode: entity.BOMCode,
                Entity: entity,
                data: data2,
                workOrderList: self.workOrderList
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_37') });
            var url = commonService.getMesApiAddress("plan") + 'PL_BOM/SaveBatchWorkOrderBomItem';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_38'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_35'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_35'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderManage_WorkOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage';

        var state = {
            name: screenStateName + '.bom',
            url: '/bom',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrder-bom.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.BomJS.Tips_39'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
