(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroup.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroupeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.ModifyUser = commonService.getLoginUser().loginName;
            console.log(self.currentItem)

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };

            //console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            //console.log("postData------------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'SystemManage/Sys_DataSegregateGroup/SaveForm';
            console.log("----------------" + url);

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, backendService.backendError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateGroup_Sys_DataSegregateGroup';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateGroup';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateGroup-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroupeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
