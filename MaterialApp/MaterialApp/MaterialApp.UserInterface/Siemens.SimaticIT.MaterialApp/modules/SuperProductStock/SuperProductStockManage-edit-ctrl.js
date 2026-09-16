(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SuperProductStock').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_1'));
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
            self.selectClick2 = selectClick2;
            self.OutTypeChange = OutTypeChange;
        }

        function initDictionary() {

            //初始化 
            self.OutType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_2'), ItemValue: "0" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_2'), ItemValue: "0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_3'), ItemValue: "1" }]
            };
        }

        function OutTypeChange(oldItem, newItem) {
            self.currentItem.ProductOrder = "";
            self.currentItem.ContainerNO = "";
            self.currentItem.WorkOrder = "";
            self.currentItem.ExeWorkOrder = "";
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function selectClick2() {
            if (!self.currentItem.MaterialCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_4'), commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_5'));
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
                            url: commonService.getMesApiAddress("material") + 'MM_SuperProductStock/GetExeWorkOrderList',
                            queryParmeters: {
                                Name: "",
                                materialCode: self.currentItem.MaterialCode,
                                batchNo: self.OutType.value.ItemValue == "1" ? self.currentItem.BatchNo : "",
                                exeWorkOrderType: self.OutType.value.ItemValue == "1" ? "5" : "1",
                                processCode: self.OutType.value.ItemValue == "1" ? self.currentItem.ProcessCode : ""
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "WorkOrder",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_6'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ProductOrder',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_7'),
                                    width: 120
                                },
                                {
                                    field: 'ContainerNO',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_8'),
                                    width: 80
                                },
                                {
                                    field: 'ExeWorkOrder',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_9'),
                                    width: 200
                                },
                                {
                                    field: 'OrderPieces',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_10'),
                                    width: 200
                                },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_11'));
                } else {

                    self.currentItem.ProductOrder = data[0].ProductOrder;
                    self.currentItem.ContainerNO = data[0].ContainerNO;
                    self.currentItem.WorkOrder = data[0].WorkOrder;
                    self.currentItem.ExeWorkOrder = data[0].ExeWorkOrder;
                }
            });
        }

        //编辑保存
        function save() {
            if (self.currentItem.Qty <= 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_12'));
                return;
            }
            if (self.OutType.value.ItemValue == "0") {
                if (self.currentItem.Qty > self.currentItem.NoLockQty) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_13'));
                    return;
                }
            }
            else {
                if (self.currentItem.Qty > self.currentItem.LockedQty) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_14'));
                    return;
                }
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_15') });

            self.currentItem.BusinessType = "2";//转出
            self.currentItem.OutType = self.OutType.value.ItemValue;//转出类型
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SuperProductStock/SuperProductStockOut';
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
                // sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_16'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_5'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_SuperProductStock_SuperProductStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SuperProductStock';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SuperProductStockManage-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SuperProductStock.SuperProductStockManageeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
