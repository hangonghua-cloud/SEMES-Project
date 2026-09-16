(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(MergeScreenStateConfig);

    MergeScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function MergeScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.mainData = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.arrQty = self.mainData.map(item => { return item.Qty; });
            self.currentItem.Qty = eval(self.arrQty.join("+"));

            self.selectedItem1 = null;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function initDictionary() {
            //库位
            self.typeLocation = {
                value: { LocationCode: "" },
                options: null
            }
            debugger;
            const uniqueArr = Array.from(new Set(self.mainData.map((item) => JSON.stringify([item.LocationCode]))));
            // 还原成对象格式
            const locationOptions = uniqueArr.map((str) => JSON.parse(str)).map(([LocationCode]) => ({ LocationCode }));
            self.typeLocation.options = [{ LocationCode: "" }, ...locationOptions];

            //批次
            self.typeBatch = {
                value: { BatchNo: "" },
                options: null
            }
            const batchOptions = self.mainData.map(({ BatchNo }) => ({ BatchNo }));
            self.typeBatch.options = [{ BatchNo: "" }, ...batchOptions];
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
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_2'),
                        width: 110
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_3'),
                        width: 130
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_4'),
                        width: 150
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_5'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_6'),
                        width: 200
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_7'),
                        width: 200
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_8'),
                        width: 140
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_9'),
                        width: 130
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_10'),
                        width: 110
                    },
                    // {
                    //     field: 'Unit',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_11'),
                    //     width: 80
                    // },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;

                        } else {
                            self.selectedItem1 = null;

                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: self.mainData
            };
        }

        function save() {

            //字典类型 取值参考
            let data = self.gridOptionsItem1.data;
            if (data.length < 1) {
                //没有要操作的数据行
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_12'));
                return false;
            }
            if (!self.typeLocation.value.LocationCode) {
                //请选择库位
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_13'));
                return;
            }
            if (!self.typeBatch.value.BatchNo) {
                //请选择批次
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_14'));
                return;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                Qty: self.currentItem.Qty,
                LocationCode: self.typeLocation.value.LocationCode,
                BatchNo: self.typeBatch.value.BatchNo,
                data: data
            };
            // debugger
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/RawMaterialStockMerge';
            //保存中，请稍后...
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_15') });
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
                //保存成功
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_16'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                //保存失败
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_17'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            //保存异常
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_18'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    MergeScreenStateConfig.$inject = ['$stateProvider'];
    function MergeScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: screenStateName + '.merge',
            url: '/merge/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialStockManage-merge.html',
                    controller: MergeScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManagemergectrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
