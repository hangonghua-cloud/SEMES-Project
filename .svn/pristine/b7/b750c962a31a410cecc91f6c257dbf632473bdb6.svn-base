(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.KeyParameter').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameter.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            self.currentItem = {
                EnCode: angular.copy($stateParams.selectedItem)
            }
            self.validInputs = false;
            self.IsEnabled = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_2'),
                    checked: true
                }
            ];
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_3') });
            self.currentItem.IsEnabled = self.IsEnabled[0].checked ? 1 : 0;
            var postData = {
                KeyValue: '',
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress() + 'Base_KeyParameterItem/SaveBase_KeyParameterItem';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            busyIndicatorService.hide();
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
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_4'));
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_5'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_5'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_KeyParameter_KeyParameter';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/KeyParameter';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/KeyParameter-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameteraddDetailctrl.Tips_6'
            }, params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
