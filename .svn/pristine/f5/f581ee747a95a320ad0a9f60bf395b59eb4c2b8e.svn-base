(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.SupplierManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.IsEnabled = [
                {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_2'),
                    checked: self.currentItem.IsEnabled
                }
            ];
            self.supplierClick = supplierClick;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            GetUserInfo();
            initDictionary();
        }

        function initDictionary() {
            //供应商等级
            self.typeSupplierLevel = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageaddctrl.Tips_6'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageaddctrl.Tips_6'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("SupplierLevel").then(function (res) {
                if (res && res.data.success) {
                    self.typeSupplierLevel.options = res.data.resultData;
                    self.typeSupplierLevel.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.SupplierLevel);
                }
            })

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
                                Name: ""
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
                self.currentItem.ParentSupplierCode = data[0].SupplierCode;
                self.currentItem.ParentSupplierName = data[0].SupplierName;
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }


        //编辑保存
        function save() {

            self.currentItem.IsEnabled = self.IsEnabled[0].checked ? 1 : 0;
            self.currentItem.ModifyBy = self.UserCode;
            self.currentItem.SupplierLevel = self.typeSupplierLevel.value.ItemValue;

            var postData = {
                KeyValue: self.currentItem.Id ? self.currentItem.Id : self.currentItem.ID,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            var url = commonService.getMesApiAddress("material") + 'Base_SupplierManage/SaveBase_SupplierManage';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_3') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_4'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_5'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_SupplierManage_SupplierManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/SupplierManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SupplierManage-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.SupplierManage.SupplierManageeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
