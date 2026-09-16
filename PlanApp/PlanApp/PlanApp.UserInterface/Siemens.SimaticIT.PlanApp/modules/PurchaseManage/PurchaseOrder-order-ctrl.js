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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {
                Coefficient: 1
            };
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.index = 0;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            //self.addForm = addForm;
            //self.deleteForm = deleteForm;
            self.supplierClick = supplierClick;

            initDictionary();
        }

        function initDictionary() {
            //物料小类
            self.typeSmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_2'), ItemValue: "" }]
            };

            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.typeSmallClass.options = res.data.resultData;
                    self.typeSmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_2'), ItemValue: "" };
                }
            })
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_2')
                    });
                    initGridData1();
                }
            });
        }

        //选择供应商
        function supplierClick() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'Base_SupplierManage/Base_SupplierManagePageDataTableList',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "SupplierCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'SupplierCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_4'),
                                    width: 130
                                },
                                {
                                    field: 'SupplierName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_5'),
                                    width: 300
                                },
                                {
                                    field: 'Abbr',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_6'),
                                    width: 150
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                self.currentItem.SupplierCode = data[0].SupplierCode;
                self.currentItem.SupplierName = data[0].SupplierName;
                self.currentItem.Abbr = data[0].Abbr;
            });
        }


        function initGridOptions1() {
            self.gridOptionsItem1 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: true,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: true,
                enableRowSelection: false,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_9'),
                        width: 140,
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_11'),
                        width: 200
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_12'),
                        width: 120
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_13'),
                        width: 80
                    },
                    {
                        field: 'Amount',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_14'),
                        width: 120
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_15'));
                return;
            }
            self.searchParams.ProcureType = "DD";//采购类型
            self.searchParams.SmallClass = self.typeSmallClass.value.ItemValue;
            if (self.StartPrepay && self.EndPrepay) {
                self.searchParams.StartPrepay = commonService.ConvertToLocalTime(self.StartPrepay);
                self.searchParams.EndPrepay = commonService.ConvertToLocalTime(self.EndPrepay);
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;

            var postData = {
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("plan") + "PL_PrdOrderReqMaterials/GetWorkOrderReqMaterials";
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

        function save() {

            var rows = $scope.gridApi.selection.getSelectedRows();
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_16'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_17'));
                return false;
            }
            var data = []
            rows.forEach(item => {
                data.push({
                    OrderType: "DD",
                    FactoryCode: item.FactoryCode,
                    FactoryName: item.FactoryName,
                    ProductOrder: item.ProductOrder,
                    DeliveryDate: item.DeliveryDate,
                    MaterialCode: item.MaterialCode,
                    MaterialName: item.MaterialName,
                    Spec: item.Spec,
                    SmallClass: item.SmallClass,
                    Unit: item.UnitName,
                    OrderNum: item.Amount,
                    PurchaseNum: (item.Amount / self.currentItem.Coefficient).toFixed(2),
                    Supplier: self.currentItem.SupplierCode,
                    ArrivalStatus: "1",//到货状态
                    Remark: self.currentItem.Remark,
                    ContractNo: self.currentItem.ContractNo,
                    InvoiceNo: self.currentItem.InvoiceNo
                });
            });

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: "",
                data: data
            };

            var url = commonService.getMesApiAddress("plan") + 'PL_PurchaseOrder/SaveBatchPL_PurchaseOrder';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_18') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_19'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_17'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_17'));
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
            name: screenStateName + '.order',
            url: '/order',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PurchaseOrder-order.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.PurchaseManage.orderJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
