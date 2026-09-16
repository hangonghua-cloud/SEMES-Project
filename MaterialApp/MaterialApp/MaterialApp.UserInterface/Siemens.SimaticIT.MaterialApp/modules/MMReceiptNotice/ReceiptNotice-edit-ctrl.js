(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMReceiptNotice').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNotice.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.ArrivalTime = new Date(self.currentItem.ArrivalTime);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.manufacturerModal = manufacturerModal;//制造厂家
            self.supplierClick = supplierClick;

            initDictronary();
        }

        function initDictronary() {

            //制造厂家
            self.typeManufacturer = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("10").then(function (res) {
                if (res && res.data.success) {
                    self.typeManufacturer.options = res.data.resultData;
                    self.typeManufacturer.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.ManufacturerCode);
                }
            })
        }

        //选择制造厂家
        function manufacturerModal() {

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("") + "SystemManage/DataItemDetail/GetDataItemListJson_UA?EnCode=10&modal=1",
                            queryParmeters: {
                                Name: ""
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Get",
                            sidx: "ItemValue",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ItemValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_4'),
                                    width: 200
                                },
                                {
                                    field: 'ItemName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_5'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_6'));
                } else {
                    self.currentItem.ManufacturerCode = data[0].ItemValue;
                    self.currentItem.ManufacturerName = data[0].ItemName;
                }
            });
        }

        //选择供应商
        function supplierClick(levelCode) {
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
                                SupplierLevel: levelCode
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "SupplierCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_34'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'SupplierCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_35'),
                                    width: 130
                                },
                                {
                                    field: 'SupplierName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_36'),
                                    width: 300
                                },
                                {
                                    field: 'Abbr',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_37'),
                                    width: 150
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                self.currentItem["SupplierCode" + levelCode] = data[0].SupplierCode;
                self.currentItem["SupplierName" + levelCode] = data[0].SupplierName;
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            // self.currentItem.ManufacturerCode = self.typeManufacturer.value.ItemValue;
            // if (self.typeManufacturer.value.ItemValue) {
            //     self.currentItem.ManufacturerName = self.typeManufacturer.value.ItemName;
            // }
            // else {
            //     self.currentItem.ManufacturerName = "";
            // }

            self.currentItem.ArrivalTime = commonService.ConvertToLocalDate(self.ArrivalTime);

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_7') });
            var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/SaveMM_ReceiptNotice';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_8'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_9'));
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_9'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MMReceiptNotice_ReceiptNotice';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMReceiptNotice';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ReceiptNotice-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeeditctrl.Tips_10'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
