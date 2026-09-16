(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.WorkOrderWearingLayer.service', '$state', '$stateParams', 'common.base', '$filter', '$scope'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            sidePanelManager.setTitle('查看');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = $stateParams.selectedItem;

            //Expose Model Methods
            self.cancel = cancel;
        }

        function save() {
            dataService.update(self.currentItem).then(onSaveSuccess, backendService.backendError);
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

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderWearingLayer_WorkOrderWearingLayer';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderWearingLayer';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderWearingLayer-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '查看'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
