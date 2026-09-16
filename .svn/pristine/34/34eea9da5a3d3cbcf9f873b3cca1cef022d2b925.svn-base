(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.DataDictionary').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary.service', '$state',
        '$stateParams', 'common.base', '$filter', '$rootScope', '$scope', 'common', 'common.widgets.notificationTile.globalService', '$http',
        'common.services.authentication', 'commonService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, c, notification, $http, $auth, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler, messageservice;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataItemaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {

            sidePanelManager = common.services.sidePanel.service;
            messageservice = common.widgets.messageOverlay.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;

            self.organizeTreeData = [];
            self.selectedOrganize = [];

            self.currentItem.ParentId = $stateParams.parentId;

            self.currentItem.ItemId = $stateParams.itemId;

            initCheckbox();
            // console.log('-----------$stateParams------------');
            // console.log($stateParams);

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function initCheckbox() {
            var isEnabledMark = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataItemaddctrl.Tips_2'),
                    checked: true
                }
            ];
            self.IsEnabledMark = isEnabledMark;
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            //dataService.create(self.currentItem).then(onSaveSuccess, backendService.backendError);
            self.currentItem.EnabledMark = self.IsEnabledMark[0].checked ? 1 : 0;
            // console.log('-----------self.currentItem------------');
            // console.log(self.currentItem);
            var postData = {
                username: $auth.getUser().unique_name,
                keyValue: "",
                DataItemDetailEntity: self.currentItem
            };

            var url = commonService.getMesApiAddress() + "SystemManage/DataItemDetail/SaveForm";
            commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            if (data.data.success) {
                $rootScope.$emit('to-parent', 'parent');
                sidePanelManager.close();
                notification.warning(data.data.returnMsg);
                $state.go('^', { itemId: self.currentItem.ItemId }, { reload: false });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataItemaddctrl.Tips_3'));
            }
        }

        function onSaveError(error) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataItemaddctrl.Tips_4'),
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: 'Siemens.SimaticIT.BaseDataApp.DataDictionary.DataItemaddctrl.Tips_5',
                text: '[' + error.status + '] - ' + error.data.returnMsg
            });
            messageservice.show();
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_DataDictionary_DataDictionary';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/DataDictionary';

        var state = {
            name: screenStateName + '.dataItemAdd',
            url: '/dataItemAdd',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/DataItem-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            },
            params: {
                parentId: null,
                itemId: null
            }
        };
        $stateProvider.state(state);
    }
}());
