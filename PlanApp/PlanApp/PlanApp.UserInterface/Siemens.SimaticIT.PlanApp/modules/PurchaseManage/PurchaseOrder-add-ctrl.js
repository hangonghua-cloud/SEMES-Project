(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.PurchaseManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.PurchaseManage.PurchaseOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_1'));
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

            initDictionary()
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
            self.supplierClick = supplierClick;
        }

        function initDictionary() {
            self.typeMaterialSmall = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeMaterialSmall.options = res.data.resultData;
                    self.typeMaterialSmall.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_2')
                    });
                    initGridData1();
                }
            });
        }

        function supplierClick() {
            commonService.SelectSupplierManage(self.currentItem);
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
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_4'),
                        width: 140
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_5'),
                        width: 140
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_6'),
                        width: 80
                    },
                    {
                        field: 'OrderQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_7'),
                        width: 140
                    }, {
                        field: 'TransitQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'BuyQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_10'),
                        width: 110
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
                        } else {
                            self.selectedItem1 = null;
                            self.IsShowButten = false;
                            //setButtonsVisibility(false);
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_11'));
                return;
            }

            self.searchParams.ProcureType = "DDKC";//采购类型
            self.searchParams.SmallClass = self.typeMaterialSmall.value.ItemValue;
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;

            var postData = {
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + "MM_RawMaterialStock/GetMaterialOrderStockTable";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem1.data = res.data.resultData;
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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_12'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_13'));
                return false;
            }
            var data = self.gridOptionsItem2.data;
            var ent = data.find(t => t.ProductOrder == self.selectedItem1.ProductOrder && t.MaterialCode == self.selectedItem1.MaterialCode);
            if (ent != null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_14'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_13'));
                return false;
            }

            //self.currentItem.PrepayDate = commonService.ConvertToLocalDate(self.PrepayDate);
            self.index = self.index + 1;
            data.push({
                index: self.index,
                OrderType: "DDKC",
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                MaterialCode: self.selectedItem1.MaterialCode,
                MaterialName: self.selectedItem1.MaterialName,
                SmallClass: self.selectedItem1.SmallClass,
                Spec: self.selectedItem1.Spec,
                Unit: self.selectedItem1.UnitName,
                PurchaseNum: self.currentItem.PurchaseNum,
                Supplier: self.currentItem.SupplierCode,
                Abbr: self.currentItem.Abbr,
                //PurchaseDeliveryDate: self.currentItem.PrepayDate,
                ArrivalStatus: "1",//到货状态
                Remark: self.currentItem.Remark,
                ContractNo: self.currentItem.ContractNo,
                InvoiceNo: self.currentItem.InvoiceNo
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
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_3'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_4'),
                        width: 140
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_5'),
                        width: 140
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_6'),
                        width: 80
                    },

                    {
                        field: 'PurchaseNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_15'),
                        width: 140
                    },
                    {
                        field: 'Abbr',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_16'),
                        width: 140
                    },
                    {
                        field: 'ContractNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_17'),
                        width: 140
                    },
                    {
                        field: 'InvoiceNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_18'),
                        width: 140
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_19'),
                        width: 140
                    },
                    // {
                    //     field: 'PurchaseDeliveryDate',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_20'),
                    //     width: 120,
                    // },

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

            var data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_21'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_13'));
                return false;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: "",
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_22') });
            var url = commonService.getMesApiAddress("plan") + 'PL_PurchaseOrder/SaveBatchPL_PurchaseOrder';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_23'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_13'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_13'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form1") {
                self.validInputs = params.validity;
            }
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_PurchaseManage_PurchaseOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/PurchaseManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PurchaseOrder-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.PurchaseManage.addJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
