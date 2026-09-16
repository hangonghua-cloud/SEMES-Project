(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Departments').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Departments.Departments.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', "commonService", '$rootScope', 'common.widgets.busyIndicator.service'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, $rootScope,
        busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentseditctrl.Tips_1'));
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

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentseditctrl.Tips_2') });
            var url = commonService.getMesApiAddress() + 'Base/SaveDepartmentForm';
            commonService.callWebApiPost(url, self.currentItem).then(onSaveSuccess, onSaveError);
        }

        function onSaveSuccess(data) {
            busyIndicatorService.hide();//关闭遮罩层
            if (data.data.success) {
                sidePanelManager.close();
                $rootScope.$emit('to-parent', 'parent');
                //notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, {});//reload: false
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentseditctrl.Tips_3'));
            }
        }

        function onSaveError(error) {
            busyIndicatorService.hide();//关闭遮罩层
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentseditctrl.Tips_3'));
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Departments_Departments';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Departments';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Departments-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Departments.Departmentseditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
