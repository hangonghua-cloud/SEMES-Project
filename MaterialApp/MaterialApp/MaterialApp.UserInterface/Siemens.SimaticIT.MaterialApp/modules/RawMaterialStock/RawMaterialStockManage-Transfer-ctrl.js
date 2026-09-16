(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initDictionary();
            initGridOptions();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_1'));
            //sidePanelManager.open('e');
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;
            //初始化 工厂名称
            self.FactoryName = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_2'), ResourceCode: "" }]
            };
            //Initialize Model Data
            self.currentItem = {};
            self.queryItem = {};
            self.queryItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.geridData = [];
            self.queryItem.forEach((item, index, arr) => {
                self.geridData.push({
                    Id: item.Id,
                    FactoryName: item.FactoryName,
                    FactoryCode: item.FactoryCode,
                    MaterialCode: item.MaterialCode,
                    MaterialName: item.MaterialName,
                    SmallClass: item.SmallClass,
                    Spec: item.Spec,
                    SupplierName: item.SupplierName,
                    SupplierCode: item.SupplierCode,
                    BatchNo: item.BatchNo,
                    WhsName: item.WhsName,
                    WhsCode: item.WhsCode,
                    LocationName: item.LocationName,
                    LocationCode: item.LocationCode,
                    Unit: item.Unit,
                    Qty: item.Qty

                });
            });
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeFactoryChange = typeFactoryChange;

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        //获取数据字典数据
        function initDictionary() {

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.FactoryName.options = res.data.resultData;
                    // if (res.data.resultData.length > 0) {
                    //     self.FactoryName.value = res.data.resultData[0];
                    // }
                    self.FactoryName.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_2')
                    });
                }
            });
            // commonService.getDataItemDuatil("Unit").then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeUnit.options = res.data.resultData;
            //         self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            //     }
            // })
        }
        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                self.currentItem.FactoryCode = newItem.ResourceCode;

                var queryParmeters = { FactoryCode: newItem.ResourceCode };
                var url = commonService.getMesApiAddress("factory") + 'level/GetWhoseLocationByFac';
                commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                    if ((res) && (res.data.success)) {
                        //总条数

                        self.currentItem.WhsName = res.data.resultData[0].ckResourceName;
                        self.currentItem.WhsCode = res.data.resultData[0].ckResourceCode;
                        self.currentItem.LocationName = res.data.resultData[0].kwResourceName;
                        self.currentItem.LocationCode = res.data.resultData[0].kwResourceCode;
                    }
                }, function (error) {
                    backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_3'));
                });
            }
        }
        function initGridOptions() {
            self.gridOptionsDetail = {

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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_4'), width: 60, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'Id',
                        displayName: 'Id',
                        width: 100,
                        visible: false
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_5'),
                        width: 100
                    },
                    {
                        field: 'FactoryCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_5'),
                        width: 100

                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_6'),
                        width: 100
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_8'),
                        width: 100
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_9'),
                        width: 100
                    }, {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_11'),
                        width: 80
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_12'),
                        width: 80
                    }, {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_13'),
                        width: 60
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_14'),
                        width: 60
                    },
                    {
                        field: 'transferNumber',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_15'),
                        width: 120,
                        cellTemplate: '<sit-numeric sit-value="row.entity.transferNumber" ></sit-numeric>'
                    }

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
                    // //防止字段只出现一半
                    // $interval(function () {
                    //     $scope.gridApi.core.handleWindowResize();
                    //     $scope.gridApi.core.refresh();
                    // }, 300, 2)

                },

                data: self.geridData

            }
        }

        //编辑保存
        function save() {

            //判断是否选择工厂
            if (self.FactoryName.value.ResourceCode == null || self.FactoryName.value.ResourceCode == "") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_16'))
                return;
            }
            //判断虚拟仓位是否维护
            if (self.currentItem.WhsName == null || self.currentItem.WhsName == "") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_17'))
                return;
            }

            //判断调拨数量是否大于库存数量
            var data2 = self.gridOptionsDetail.data;
            //判断调拨工厂与物料工厂是否相同
            if (self.FactoryName.value.ResourceCode == data2[0].FactoryCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_18'))
                return;
            }
            var numisEnpty = 0;
            var numMore = 0;
            data2.forEach((item, index, arr) => {
                //判断调拨数量是否为空
                if (item.transferNumber == null || item.transferNumber == "") {
                    numisEnpty = numisEnpty + 1;
                }
                //判断调拨数量是否大于库存数量
                if (item.transferNumber > item.Qty) {
                    numMore = numMore + 1;
                }
            });
            if (numisEnpty > 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_19'))
                return;
            }
            if (numMore > 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_20'))
                return;
            }
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                receiveFactoryCode: self.currentItem.FactoryCode,
                receiveFactoryName: self.FactoryName.value.ResourceName,
                receiveWhsName: self.currentItem.WhsName,
                receiveWhsCode: self.currentItem.WhsCode,
                receiveLocationName: self.currentItem.LocationName,
                receiveLocationCode: self.currentItem.LocationCode,
                remak: self.currentItem.Remark,
                Entity: data2
            };
            console.log("jpf1234" + JSON.stringify(postData));

            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStocktransfer';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_21') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_22'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_23'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_23'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: screenStateName + '.Transfer',
            url: '/Transfer',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialStockManage-Transfer.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageTransferctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
