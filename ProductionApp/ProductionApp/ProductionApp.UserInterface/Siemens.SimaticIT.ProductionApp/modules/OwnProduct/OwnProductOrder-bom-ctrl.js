(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions();
            initGridOptionsDetail();
            initGridData();
            sidePanelManager.setTitle('工单BOM修改');
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.isDetailButtonVisible = false;
            self.selectedItemDetail = null
            self.selectedItem = null;

            self.validInputs = false;
            self.isReadNum = true;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeProcessChange = typeProcessChange;
            self.typeBomChange = typeBomChange;

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
                value: { ProcessName: "--请选择--", ProcessCode: "" },
                options: [{ ProcessName: "--请选择--", ProcessCode: "" }]
            };
            self.typeStartProcess = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" }]
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
                        ProcessName: "--请选择--"
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
                        displayName: '工艺路线',
                        width: 100,
                        cellTemplate:
                            '<sit-select sit-value="row.entity.typeProcess.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeProcess.options"' +

                            'sit-change="row.entity.typeProcessChange"' +
                            'sit-to-display="\'ProcessName\'"' +
                            'sit-to-keep="\'ProcessCode\'">' +
                            '</sit-select>'
                    },
                    // {
                    //     field: 'StartOperationName',
                    //     displayName: '起始工序',
                    //     width: 100,
                    //     cellTemplate:
                    //         '<sit-select sit-value="row.entity.typeStartOperation.value"' +
                    //         'sit-validation="{required: false}"' +
                    //         'sit-options="row.entity.typeStartOperation.options"' +
                    //         'sit-to-display="\'OperationName\'"' +
                    //         'sit-to-keep="\'OperationCode\'">' +
                    //         '</sit-select>'
                    // },
                    {
                        field: 'BomCode',
                        displayName: 'Bom编码',
                        width: 150,
                        cellTemplate:
                            '<sit-select sit-value="row.entity.typeBom.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeBom.options"' +
                            'sit-change="row.entity.typeBomChange"' +
                            'sit-to-display="\'BOMCode\'"' +
                            'sit-to-keep="\'Id\'">' +
                            '</sit-select>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: '工厂名称',
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.FactoryCode==\'3001\'"><span ng-cell-text>富华工厂</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.FactoryCode==\'3002\'"><span ng-cell-text>华丽二厂</span></div>'
                    },
                    {
                        field: 'WorkOrder',
                        displayName: '工单号',
                        width: 120
                    },
                    // {
                    //     field: 'ContainerNO',
                    //     displayName: '柜号',
                    //     width: 100
                    // },
                    // {
                    //     field: 'OrderType',
                    //     displayName: '订单类型',
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>出口</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>内销</span></div>'
                    // },

                    {
                        field: 'MaterialCode',
                        displayName: '客户型号',
                        width: 160
                    },
                    {
                        field: 'Spec',
                        displayName: '规格型号',
                        width: 200
                    },

                    // {
                    //     field: 'OrderPieces',
                    //     displayName: '下单总片数',
                    //     width: 120
                    // },
                    // {
                    //     field: 'OrderStatus',
                    //     displayName: '工单状态',
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'1\'"><span ng-cell-text>创建</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'2\'"><span ng-cell-text>审核</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'3\'"><span ng-cell-text>发布</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'4\'"><span ng-cell-text>已发料</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'5\'"><span ng-cell-text>生产中</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.OrderStatus==\'6\'"><span ng-cell-text>已完成</span></div>'

                    // },

                    // {
                    //     field: 'StartOperationName',
                    //     displayName: '起始工序',
                    //     width: 110,
                    // },
                    // {
                    //     field: 'UnitNum',
                    //     displayName: '单位数量',
                    //     width: 150
                    // },


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
            self.currentItem;
            var postData = {
                queryJson: {
                    WorkOrder: self.currentItem.WorkOrder
                }
            }
            var url = commonService.getMesApiAddress("ProduceManage") + "PL_BOM/GetOwnProductOrderBom";

            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    var data = res.data.resultData.rows;
                    self.selectedItem = angular.copy(data[0]);
                    //工艺路线
                    var url1 = commonService.getMesApiAddress("material") + "Base_MaterialFactory/GetBase_MaterialFactorySelect";
                    commonService.callWebApiPost(url1, {
                        queryJson: {
                            FactoryCode: data[0].FactoryCode,
                            MaterialCode: data[0].MaterialCode
                        }
                    }).then(function (resroute) {
                        if (resroute && resroute.data.success) {
                            var route = resroute.data.resultData.find(t => t.ProcessCode == data[0].ProcessRoute);
                            if (route != null) {
                                data[0].typeProcess = {
                                    options: resroute.data.resultData,
                                    value: route
                                }
                            } else {
                                resroute.data.resultData.splice('0', '0', {
                                    ProcessCode: data[0].Process,
                                    ProcessName: data[0].ProcessName
                                });
                                data[0].typeProcess = {
                                    options: resroute.data.resultData,
                                    value: {
                                        ProcessCode: data[0].Process,
                                        ProcessName: data[0].ProcessName
                                    }
                                }
                            }


                            data[0].typeBomChange = typeBomChange;
                            data[0].typeProcessChange = typeProcessChange;
                            //Bom

                            self.gridOptions.data = data;
                            typeProcessChange(null, data[0].typeProcess.value);
                            //typeBomChange(null,data[0].typeBom.value);
                        }
                    });
                }
            });
        }

        function typeProcessChange(oldval, newvalue) {
            console.log("工艺路线改变");
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
                        var ent1 = self.gridOptions.data[0];
                        var bom = res.data.resultData.rows.find(t => t.BOMCode == ent1.BOMCode);
                        if (bom != null) {
                            bom.Id = ent1.BOMCode;
                            self.gridOptions.data[0].typeBom = {
                                options: res.data.resultData.rows,
                                value: { BOMCode: bom.BOMCode, Id: bom.Id }
                            }
                        } else {
                            self.gridOptions.data[0].typeBom = {
                                options: [
                                    { BOMCode: ent1.BOMCode, Id: ent1.BOMCode }],
                                value: { BOMCode: ent1.BOMCode, Id: ent1.BOMCode }
                            }
                        }

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
            console.log("bom改变");
            if (!!newval) {
                var entity = self.gridOptions.data[0];
                if (newval.BOMCode == newval.Id) {
                    //初始化的时候查询绑定工单的bomitems
                    var postData = {
                        queryJson: {
                            WorkOrder: entity.WorkOrder
                        }
                    }
                    var url = commonService.getMesApiAddress("ProduceManage") + "PL_BOM/GetWorkOrderBomItem";
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

        //获取bom
        function loadNeBomItems(bomId) {
            var postData = {
                queryJson: {
                    BOMCode: self.gridOptions.data[0].typeBom.value.BOMCode,
                    BOMId: bomId
                }
            }
            var url = commonService.getMesApiAddress("material") + "BS_BOMItems/BS_BOMItemsPageDataTableList";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                }
            })
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
                        name: 'rowNum', displayName: '序号', width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: '物料编码',
                        width: 120
                    },
                    {
                        field: 'MaterialName',
                        displayName: '物料名称',
                        width: 120
                    },
                    {
                        field: 'BOMCode',
                        displayName: 'BOM编码',
                        width: 120
                    },
                    {
                        field: 'MaterialClassName',
                        displayName: '物料分类',
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: '物料小类',
                        width: 100
                    },
                    {
                        field: 'Num',
                        displayName: '数量',
                        width: 100
                    },
                    {
                        field: 'UnitName',
                        displayName: '单位',
                        width: 100
                    },
                    {
                        field: 'WarehouseName',
                        displayName: '库存地点',
                        width: 140
                    },
                    {
                        field: 'ProcessCode',
                        displayName: '分配工序代码',
                        width: 130
                    },
                    {
                        field: 'ProcessName',
                        displayName: '分配工序名称',
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
                templateUrl: 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct/AddBomItem.html',
                controller: 'Siemens.SimaticIT.ProductionApp.OwnProduct.AddBomItem',
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
                    backendService.genericError("物料重复", "操作出错");
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
                backendService.genericError("请选择行数据", "操作出错");
                return false;
            }
            var modalInstance = commonService.openModel({
                templateUrl: 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct/EditBomItem.html',
                controller: 'Siemens.SimaticIT.ProductionApp.OwnProduct.EditBomItem',
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
                FactoryCode: self.currentItem.FactoryCode,
                FactoryName: self.currentItem.FactoryName,
                WorkOrder: data1[0].WorkOrder,
                ProcessRoute: data1[0].typeProcess.value.ProcessCode,
                BOMCode: data1[0].typeBom.value.BOMCode
            }
            data2.forEach((item, index, arr) => {
                item.BOMId = data1[0].PLBOMId,
                    item.ConsumeProcess = item.ProcessCode
            });

            var postData = {
                BOMId: data1[0].PLBOMId,//PL_Bom的Id
                BOMCode: entity.BOMCode,
                Entity: entity,
                data: data2
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PL_BOM/SaveBatchWorkOrderBomItem';
            busyIndicatorService.show({ message: "保存中，请稍后……" });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo('保存成功！');
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, "操作出错");
            }
        }



        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, "操作出错");
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }




    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: screenStateName + '.bom',
            url: '/.bom/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OwnProductOrder-bom.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '工单BOM修改'
            },
            params: {
                selectedItem: null,
            }

        };
        $stateProvider.state(state);
    }
}());
