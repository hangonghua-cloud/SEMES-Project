(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle('添加列表');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.currentItem = angular.copy($stateParams);
            self.currentItem.CategoryCode = $stateParams.CategoryCode;
            var IsValid = [
                {
                    label: "启用",
                    checked: true
                }
            ];
            self.IsValid = IsValid;

            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            
            self.currentItem.IsValid = self.IsValid[0].checked;
            dataService.create(self.currentItem).then(onSaveSuccess, backendService.backendError);
            
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: false });
            $rootScope.$emit('to-parent', 'parent');
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_BaseManage_BaseManageScreen';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/BaseManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BaseManageScreen-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '添加列表'
            },
            params: {
                CategoryCode: null,
            }
        };
        $stateProvider.state(state);
    }
}());
