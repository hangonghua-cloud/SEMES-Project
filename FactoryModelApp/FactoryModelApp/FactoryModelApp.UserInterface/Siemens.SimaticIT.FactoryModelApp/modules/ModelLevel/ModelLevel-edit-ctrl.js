(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelLevel').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevel.service', '$state', '$stateParams',
        'common.base', '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = true;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var reg = new RegExp('^[0-9]*$');
            var username = commonService.getLoginUser().loginName;
            self.currentItem.ModifyUser = username;
            self.currentItem.EnabbledMark = true;
            //self.currentItem.CreatDate = '';
            //self.currentItem.ProductionLine = '1'
            //self.currentItem.ProductionLine='1';
            //console.log(self.currentItem)

            if (!reg.test(self.currentItem.Level)) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_2'));
                return;
            }

            if (self.currentItem.Level <= 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_3'));
                return;
            }
            if (self.currentItem.LevelCode == "") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_4'));
            }
            else if (self.currentItem.LevelName == "") {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_5'));
            }
            else {
                var postData = {
                    KeyValue: self.currentItem.LevelCode,
                    //WorkOrderNO: self.currentItem.WorkOrderNO,
                    userCode: "001",
                    userName: "003",
                    //PlanType:'1',
                    //ProductionLine:'1',
                    //PlanedNum:'1',
                    //WorkOderType:'1',
                    Entity: self.currentItem
                };

                var url = commonService.getMesApiAddress("factory") + '/LevelManage/BsModelLevel/Save';
                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_6'));
        }
        function onSaveSuccess(data) {
            if (data.data.success) {
                sidePanelManager.close();
                $rootScope.$emit('to-addparent', 'parent');
                notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, { reload: true });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_6'));
            }
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevel_ModelLevel';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelLevel';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:LevelCode',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelLevel-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveleditctrl.Tips_7'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
