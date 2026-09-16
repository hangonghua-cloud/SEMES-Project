(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Departments').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Departments.Departments.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', "commonService", '$rootScope', 'common.widgets.busyIndicator.service'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, $rootScope,
        busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_1'));
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

            //数据字典
            initDictionary();
        }

        function initDictionary() {

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_2')
                    });
                }
            });

        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_3') });
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
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_4'));
            }
        }

        function onSaveError(error) {
            busyIndicatorService.hide();//关闭遮罩层
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_4'));
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }



        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Departments_Departments';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Departments';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Departments-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Departments.Departmentsaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
