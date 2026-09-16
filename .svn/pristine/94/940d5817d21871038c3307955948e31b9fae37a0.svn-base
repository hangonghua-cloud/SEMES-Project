(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.BaseManage').config(EditParentScreenStateConfig);

    EditParentScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.BaseManage.BaseManageScreen.service', '$state', '$stateParams', 'common.base', '$filter', '$scope','$rootScope'];
    function EditParentScreenController(dataService, $state, $stateParams, common, $filter, $scope,$rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            sidePanelManager.setTitle('编辑类别');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.currentItem.Id = $stateParams.selectedItem.Id;
            self.currentItem.CategoryCode = $stateParams.selectedItem.CategoryCode;
            self.currentItem.CategoryName = $stateParams.selectedItem.CategoryName;
            self.currentItem.SortNum = $stateParams.selectedItem.SortNum;
            self.validInputs = false;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.IsValid = true;
            dataService.updateParent(self.currentItem).then(onSaveSuccess, backendService.backendError);
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

    EditParentScreenStateConfig.$inject = ['$stateProvider'];
    function EditParentScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_BaseManage_BaseManageScreen';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/BaseManage';

        var state = {
            name: screenStateName + '.editParent',
            url: '/editParent/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BaseManageScreen-editParent.html',
                    controller: EditParentScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '编辑菜单'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
