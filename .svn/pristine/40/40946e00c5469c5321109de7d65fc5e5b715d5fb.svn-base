(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(Out2ScreenStateConfig);

    Out2ScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function Out2ScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.index = 0;
            self.PostDate = new Date();

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
            self.selectWorkShopModal = selectWorkShopModal; //车间
        }

        function initDictionary() {
            //出库方式
            self.typeOutMode = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("OutMode").then(function (res) {
                if (res && res.data.success) {
                    self.typeOutMode.options = res.data.resultData;
                    // self.typeOutMode.options.splice(0, 0, { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ItemValue: "" });
                    self.typeOutMode.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ItemValue: "" };
                }
            })

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2')
                    });
                    initGridData1();
                }
            });

            //车间
            self.typeWorkShop = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ResourceCode: "" }]
            };

        }

        //选择部门
        function selectWorkShopModal() {
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_32'));
                return;
            }
            if (!self.typeOutMode.value.ItemValue) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_33'));
                return;
            }
            if (self.typeOutMode.value.ItemValue == "201") //成本中心领料  数据源加载车间信息
            {
                var modalInstance = commonService.openModel({
                    templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                    controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                    controllerAs: 'vm',
                    size: 'lg',
                    resolve: {
                        data: function () {
                            return {
                                url: commonService.getMesApiAddress("factory") + 'level/GetListByParentResource',
                                queryParmeters: {
                                    Name: "",
                                    ParentResource: self.typeFactory.value.ResourceCode
                                },
                                multiple: false,
                                method: "Post",
                                //分页需要添加此对象
                                // pagination: {
                                //     rows: 20,//每页显示条数
                                //     page: 1,//页码
                                // },
                                sidx: "ResourceCode",
                                sord: "asc",
                                columnDefs: [
                                    {
                                        name: 'rowNum', displayName: "行号", minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                    },
                                    {
                                        field: 'ResourceCode',
                                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_34'),
                                        width: 150
                                    },
                                    {
                                        field: 'ResourceName',
                                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_35'),
                                        width: 150
                                    }
                                ],
                            };
                        }
                    }
                });
                modalInstance.result.then(function (data) {
                    if ((!data || data.length <= 0)) {
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addJS.Tips_8'));
                    } else {
                        self.currentItem.WorkShopCode = data[0].ResourceCode;
                        self.currentItem.WorkShopName = data[0].ResourceName;
                    }
                });
            }
            else {
                var modalInstance = commonService.openModel({
                    templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                    controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                    controllerAs: 'vm',
                    size: 'lg',
                    resolve: {
                        data: function () {
                            return {
                                url: commonService.getMesApiAddress("plan") + 'PL_InternalOrder/PL_InternalOrderPageDataTableList',
                                queryParmeters: {
                                    Name: "",
                                    Status: "B"
                                },
                                multiple: false,
                                method: "Post",
                                //分页需要添加此对象
                                pagination: {
                                    rows: 20,//每页显示条数
                                    page: 1,//页码
                                },
                                sidx: "OrderDescription",
                                sord: "asc",
                                columnDefs: [
                                    {
                                        name: 'rowNum', displayName: "行号", minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                    },
                                    {
                                        field: 'OrderDescription',
                                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_38'),
                                        width: 200
                                    },
                                    {
                                        field: 'InternalOrder',
                                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_37'),
                                        width: 200
                                    },
                                ],
                            };
                        }
                    }
                });
                modalInstance.result.then(function (data) {
                    if ((!data || data.length <= 0)) {
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.addJS.Tips_8'));
                    } else {
                        self.currentItem.WorkShopCode = data[0].InternalOrder;
                        self.currentItem.WorkShopName = data[0].OrderDescription;
                    }
                });
            }
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
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
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_6'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_13'),
                        width: 200
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_9'),
                        width: 80
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_12'),
                        width: 100
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            //setButtonsVisibility(true);
                            //GetWorkOrderBomUnitConsome();
                            self.IsShowButten = true;
                            //车间
                            commonService.getResourceListByParentResource({ ParentResource: self.selectedItem1.FactoryCode }).then(function (res) {
                                if (res && res.data.success) {
                                    self.typeWorkShop.options = res.data.resultData;
                                    self.typeWorkShop.options.splice(0, 0, {
                                        ResourceCode: "",
                                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2')
                                    });
                                }
                            });
                        } else {
                            self.selectedItem1 = null;
                            self.IsShowButten = false;
                            //setButtonsVisibility(false);
                            //车间
                            self.typeWorkShop = {
                                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ResourceCode: "" },
                                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_2'), ResourceCode: "" }]
                            };
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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_16'))
                return;
            }

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            var postData = {
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/MM_RawMaterialStockPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }
        function search() {
            initGridData1();
        }

        function addForm() {
            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_17'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_18'));
                return false;
            }
            var data = self.gridOptionsItem2.data;
            var ent = data.find(t => t.MaterialCode == self.selectedItem1.MaterialCode
                && t.WhsCode == self.selectedItem1.WhsCode && t.LocationCode == self.selectedItem1.LocationCode
                && t.BatchNo == self.selectedItem1.BatchNo && t.WorkShopCode == self.currentItem.WorkShopCode);
            if (ent != null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_19'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_18'));
                return false;
            }
            if (!self.currentItem.Qty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_20'));
                return;
            }
            if (!self.typeOutMode.value.ItemValue) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_21'));
                return;
            }
            if (self.currentItem.Qty > self.selectedItem1.Qty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_22'));
                return;
            }
            if (!self.PostDate) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_36'));
                return;
            }

            self.index = self.index + 1;
            data.push({
                index: self.index,
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                WhsCode: self.selectedItem1.WhsCode,
                WhsName: self.selectedItem1.WhsName,
                LocationCode: self.selectedItem1.LocationCode,
                LocationName: self.selectedItem1.LocationName,
                MaterialCode: self.selectedItem1.MaterialCode,
                MaterialName: self.selectedItem1.MaterialName,
                Spec: self.selectedItem1.Spec,
                SmallClass: self.selectedItem1.SmallClass,
                SmallClassName: self.selectedItem1.SmallClassName,
                SupplierCode: self.selectedItem1.SupplierCode,
                SupplierName: self.selectedItem1.SupplierName,
                BatchNo: self.selectedItem1.BatchNo,
                StockQty: self.selectedItem1.Qty,
                Unit: self.selectedItem1.Unit,
                Qty: self.currentItem.Qty,
                MoveType: self.typeOutMode.value.ItemValue,
                OutType: "10", //出库
                OutTypeName: self.typeOutMode.value.ItemName,
                WorkShopCode: self.currentItem.WorkShopCode,
                WorkShopName: self.currentItem.WorkShopName,
                Remark: self.currentItem.Remark,
                PostDate: commonService.ConvertToLocalDate(self.PostDate),
                BusinessId: self.selectedItem1.Id, //库存表Id
                BusinessTable: "MM_RawMaterialStock"
            });
            self.gridOptionsItem2.data = data;
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
                        field: 'WorkShopName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_30'),
                        width: 200
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_12'),
                        width: 200
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_5'),
                        width: 200
                    },

                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_6'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_13'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_23'),
                        width: 120
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_15'),
                        width: 120
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_7'),
                        width: 150
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_9'),
                        width: 80
                    },
                    {
                        field: 'StockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_24'),
                        width: 110
                    },
                    {
                        field: 'OutTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_25'),
                        width: 110
                    },
                    {
                        field: 'PostDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_31'),
                        width: 110
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_26'),
                        width: 110
                    }
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


        function deleteForm() {
            self.gridOptionsItem2.data = _.filter(self.gridOptionsItem2.data, function (item) {
                return item.index != self.selectedItem2.index;
            })
        }

        function save() {

            //字典类型 取值参考
            let data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_27'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_18'));
                return false;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                Entity: data
            };
            // debugger
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStockOut';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_28') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_29'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_18'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_18'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    Out2ScreenStateConfig.$inject = ['$stateProvider'];
    function Out2ScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: screenStateName + '.out2',
            url: '/out2/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialStockManage-out2.html',
                    controller: Out2ScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
