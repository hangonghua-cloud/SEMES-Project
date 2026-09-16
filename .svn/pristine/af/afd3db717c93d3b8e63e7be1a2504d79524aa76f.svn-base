(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.KeyParameter').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParameter.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_1'));
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
        }

        function initDictionary() {

            debugger
            self.IsEnabledMark = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_2'),
                    checked: true
                }
            ];
            self.IsEnabledMark[0].checked = self.currentItem.IsEnabled;

            self.IsRepetition = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_3'),
                    checked: false
                }
            ];
            self.IsRepetition[0].checked = self.currentItem.IsRepetition;

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_4') });

            self.currentItem.IsEnabled = self.IsEnabledMark[0].checked ? 1 : 0;
            self.currentItem.IsRepetition = self.IsRepetition[0].checked ? 1 : 0;

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress() + 'Base_KeyParameter/SaveBase_KeyParameter';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_5'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_6'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_KeyParameter_KeyParameter';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/KeyParameter';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/KeyParameter-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.BaseDataApp.KeyParameter.KeyParametereditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
