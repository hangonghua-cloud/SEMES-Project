(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductStock').config(EditDetailScreenStateConfig);

    EditDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductStock.ProductStock.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.selectOrderModal = selectOrderModal;//选择目标订单
            self.selectContainerNOrModal = selectContainerNOrModal;//选择柜号
            self.WarehouseChange = WarehouseChange;

        }

        function initDictionary() {
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2'), ResourceCode: "" }]
            };

            //仓库
            // commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKLX" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2')
            //         });
            //         self.typeWarehouse.value = { ResourceCode: self.currentItem.WhsCode, ResourceName: self.currentItem.WhsName };
            //     }
            // });
            let query = {
                factoryCode: self.currentItem.FactoryCode,
                fieldCode: "CPDG",
                fieldValue: "1"
            }
            commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2')
                    });
                    self.typeWarehouse.value = { ResourceCode: self.currentItem.WhsCode, ResourceName: self.currentItem.WhsName };
                }
            });
        }

        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_2')
                    });
                    self.typeLocation.value = { ResourceCode: self.currentItem.LocationCode, ResourceName: self.currentItem.LocationName };
                }
            });
        }

        function selectOrderModal() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'MM_ProductStock/GetProductOrderPageDataTableList',
                            queryParmeters: {
                                Name: ""
                            },
                            pagination: {
                                rows: 20,//每页显示条数
                                page: 1//页码
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ProductOrder",
                            sord: "desc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ProductOrder',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_4'),
                                    width: 200
                                },

                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_5'));
                } else {
                    self.currentItem.TargetProductOrder = data[0].ProductOrder;
                }
            });
        }
        function selectContainerNOrModal() {
            if (!self.currentItem.TargetProductOrder) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_6'));
                return;
            }

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'MM_ProductStock/GetContainerNOPageDataTableList',
                            queryParmeters: {
                                Name: "",
                                ProductOrder: self.currentItem.TargetProductOrder,
                                MaterialCode: self.currentItem.MaterialCode
                            },
                            pagination: {
                                rows: 20,//每页显示条数
                                page: 1//页码
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ContainerNO",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ContainerNO',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_7'),
                                    width: 130
                                },
                                {
                                    field: 'OrderPieces',
                                    displayName: "订单片数",
                                    width: 130
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_5'));
                } else {
                    self.currentItem.TargetWorkOrder = data[0].WorkOrder;
                    self.currentItem.TargetContainerNO = data[0].ContainerNO;
                }
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            if (self.currentItem.TargetPieceQty > self.currentItem.PieceQty) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_8'));
                return;
            }
            self.currentItem.TargetWhsCode = self.typeWarehouse.value.ResourceCode;
            self.currentItem.TargetLocationCode = self.typeLocation.value.ResourceCode;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ProductStock/ProductStockMove2';
            //提交数据
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_9') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_10'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_11'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditDetailScreenStateConfig.$inject = ['$stateProvider'];
    function EditDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductStock_ProductStock';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductStock';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductStock-editDetail.html',
                    controller: EditDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductStock.ProductStockeditDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
