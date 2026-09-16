(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage').config(AddParentScreenStateConfig);

    AddParentScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service', '$state', '$stateParams', 'common.base', '$filter', '$scope','$rootScope'];
    function AddParentScreenController(dataService, $state, $stateParams, common, $filter, $scope,$rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle('添加类别');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.currentItem = angular.copy($stateParams);
            self.currentItem.ParentId = $stateParams.ParentId;
            self.validInputs = false;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.ParentId = $stateParams.ParentId;
            dataService.createParent(self.currentItem).then(onSaveSuccess, backendService.backendError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: false });
            $rootScope.$emit('to-parent1', 'parent');
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddParentScreenStateConfig.$inject = ['$stateProvider'];
    function AddParentScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_BaseManage_BaseManageScreen';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/BaseManage';

        var state = {
            name: screenStateName + '.addParent',
            url: '/addParent',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BaseManageScreen-addParent.html',
                    controller: AddParentScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '添加菜单'
            },
            params: {
                ParentId: null,
            }
        };
        $stateProvider.state(state);
    }
}());
