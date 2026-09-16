(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Sys_DataSegregateLabel.Sys_DataSegregateLabel.service', '$state', '$stateParams', 'common.base', '$filter', '$scope'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            sidePanelManager.setTitle('Select');
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
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Sys_DataSegregateLabel_Sys_DataSegregateLabel';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Sys_DataSegregateLabel';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Sys_DataSegregateLabel-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Select'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
