(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service', '$state', '$stateParams', 'common.base', '$filter', '$scope','$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,$rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            sidePanelManager.setTitle('编辑列表');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            if (self.currentItem.IsValid == true) {
                var IsValid = [
                    {
                        label: "启用",
                        checked: true
                    }
                ];
                self.IsValid = IsValid;
            } else {
                var IsValid = [
                    {
                        label: "不启用",
                        checked: false
                    }
                ];
                self.IsValid = IsValid;
            }
            self.validInputs = false;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            $rootScope.$emit('to-parent', 'parent');
            self.currentItem.IsValid = self.IsValid[0].checked;
            dataService.update(self.currentItem).then(onSaveSuccess, backendService.backendError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^');
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_BaseManage_BaseManageScreen';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/BaseManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BaseManageScreen-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '编辑列表'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
