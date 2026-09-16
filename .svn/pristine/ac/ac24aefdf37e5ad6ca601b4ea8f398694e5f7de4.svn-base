(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.TransferCardResume').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.TransferCardResume.RawMaterialComplete.service', '$state', '$stateParams', 'common.base', '$filter', '$scope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle('添加');
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
            dataService.create(self.currentItem).then(onSaveSuccess, backendService.backendError);
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
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_TransferCardResume_RawMaterialComplete';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/TransferCardResume';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialComplete-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '添加'
            }
        };
        $stateProvider.state(state);
    }
}());
