(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroup.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroupaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.CreateUser = commonService.getLoginUser().loginName;
            console.log(self.currentItem)

            var postData = {
                KeyValue: "",
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

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateGroup_Sys_DataSegregateGroup';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateGroup';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateGroup-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Sys_DataSegregateGroup.Sys_DataSegregateGroupaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
