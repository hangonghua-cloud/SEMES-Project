(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.Process').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.Process.ProcessAttr.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = angular.copy($stateParams.selectedItem)
            self.currentItem = {
                ProcessAttrId: self.selectedItem.Id
            }
            self.validInputs = false;
            self.typeAttrTypeSelect = {
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_2'), ItemValue: "" }],
                selectedOption: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_2'), ItemValue: "" }
            }
            commonService.getDataItemDuatil("DataType").then(function (res) {
                if (res && res.data.success) {
                    self.typeAttrTypeSelect.options = res.data.resultData;
                    self.typeAttrTypeSelect.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_2'), ItemValue: "" };
                }
            })


            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_3') });

            self.currentItem.AttrType = self.typeAttrTypeSelect.selectedOption.ItemValue;
            self.currentItem.AttrTypeName = self.typeAttrTypeSelect.selectedOption.ItemName;

            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("material") + 'Base_ProcessAttr/SaveProcessAttrItemForm';

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
                //sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_4'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                //$state.go('^', {}, { reload: false });
                self.currentItem = {
                    ProcessAttrId: self.selectedItem.Id
                }
                self.typeAttrTypeSelect.selectedOption = angular.copy(self.typeAttrTypeSelect.options[0]);
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_Process_ProcessAttr';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/Process';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProcessAttr-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.Process.ProcessAttraddDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
