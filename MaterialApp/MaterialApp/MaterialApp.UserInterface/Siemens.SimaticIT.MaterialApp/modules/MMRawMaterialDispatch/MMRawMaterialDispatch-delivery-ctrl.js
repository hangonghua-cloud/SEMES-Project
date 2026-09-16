(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch').config(DeliveryScreenStateConfig);

    DeliveryScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatch.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function DeliveryScreenController(dataService, $state, $stateParams,
        common, $filter, $scope, commonService, auth, notificationService,
        busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();
            initGridOptionsItem();
            initGridDataItem();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_1'));
            //sidePanelManager.open('e');
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItemMain = angular.copy($stateParams.selectedItem.main);
            self.currentItem = angular.copy($stateParams.selectedItem.detail);
            self.validInputs = false;
            self.searchParams = {};
            self.selectedItemDetail = null;
            self.PostDate = new Date();

            //Expose Model Methods
            self.cancel = cancel;
            self.addForm = addForm;
            // self.deleteForm = deleteForm;
            self.save = save;

        }

        function initGridOptionsItem() {
            self.gridOptionsItem = {
                //分页属性
                enablePagination: false, //是否分页,default为true
                enablePaginationControls: false, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 50, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: false,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: true,
                enableRowSelection: true,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'DeliveryQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_2'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_3'),
                        width: 150
                    },
                    // {
                    //     field: 'Spec',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_4'),
                    //     width: 150
                    // },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_5'),
                        width: 150
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_6'),
                        width: 150
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_7'),
                        width: 150
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_8'),
                        width: 150
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_9'),
                        width: 150
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    // $scope.gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    //     //调用查询方法
                    //     initGridDataItem();
                    // });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {

                        if (row && row.isSelected == true) {
                            self.selectedItemDetail = row.entity;
                            self.validInputs = true;
                        } else {
                            self.selectedItemDetail = null;
                            self.validInputs = false;
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

        function initGridDataItem() {

            self.searchParams.RawDispatchMaterialCode = self.currentItem.MaterialCode;
            self.searchParams.CKLX = "3";
            var postData = {
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/MM_RawMaterialStockPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    //总条数
                    self.gridOptionsItem.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem.data = []
                }
            })
        }

        //添加
        function addForm() {

            if (!self.selectedItemDetail) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_11'));
                return;
            }
            if (!self.selectedItemDetail.DeliveryQty) {
                self.selectedItemDetail.DeliveryQty = 0;
            }

            self.selectedItemDetail.DeliveryQty += self.currentItem.DeliveryQty;
            self.selectedItemDetail.UnitName = self.selectedItemDetail.Unit;
            self.selectedItemDetail.DispatchSubId = self.currentItem.Id;
            self.selectedItemDetail.DeliveryNo = self.currentItem.DeliveryNo;
        }

        function save() {
            // debugger;
            var data = self.gridOptionsItem.data.filter(t => !!t.DeliveryQty);
            if (data.length == 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_12'));
                return;
            }
            var filter1 = data.find(t => t.DeliveryQty > t.Qty);
            if (filter1) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_13'));
                return;
            }

            const totalValue = data.reduce((sum, item) => sum + item.DeliveryQty, 0);
            if (totalValue != self.currentItem.Qty) {
                //发货数量必须等于总发货数量
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_14'));
                return;
            }

            if (!self.PostDate) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_36'));
                return;
            }
            self.currentItemMain.PostDate = commonService.ConvertToLocalDate(self.PostDate);

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                entity: self.currentItemMain,
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_15') });
            var url = commonService.getMesApiAddress("material") + 'MMRawMaterialDispatchDetail/Delivery';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_16'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_17'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_17'));
        }

        //取消
        function cancel() {
            sidePanelManager.close();//关闭侧边栏
            $state.go('^');//返回列表(父页面)
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }
    DeliveryScreenStateConfig.$inject = ['$stateProvider'];
    function DeliveryScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MMRawMaterialDispatch_MMRawMaterialDispatch';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMRawMaterialDispatch';

        var state = {
            name: moduleStateName + '.delivery',
            url: '/delivery/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MMRawMaterialDispatch-delivery.html',
                    controller: DeliveryScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMRawMaterialDispatch.MMRawMaterialDispatchdeliveryctrl.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
