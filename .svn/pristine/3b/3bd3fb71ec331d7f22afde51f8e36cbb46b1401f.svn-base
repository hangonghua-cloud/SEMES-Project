(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelLevel').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLevel.service', '$state', '$stateParams', 'common.base',
        '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService', 'common.widgets.busyIndicator.service'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_1'));
            sidePanelManager.open('e');
            /*sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });*/
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {
                Describe: "",
                Level: 0,
                LevelCode: "",
                LevelName: "",
            };
            self.validInputs = false;
            /*self.saveDialog = saveDialog;
            self.cancleDialog = cancleDialog;
            self.dialogShow = false;*/
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            busyIndicatorService.show();
            var reg=new RegExp('^[0-9]*$');
            var username = commonService.getLoginUser().loginName;
            
            self.currentItem.CreateUser = username;
            self.currentItem.EnabbledMark = true;
            //console.log(self.currentItem)

            if(!reg.test(self.currentItem.Level)){
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_2'));
                return;
            }
            
            if(self.currentItem.Level <= 0){
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_3'));
                return;
            }
            if(self.currentItem.LevelCode==""){
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_4'));
            }
            else if(self.currentItem.LevelName==""){
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_5'));
            }
            else{
                var postData = {
                    KeyValue: "",
                    userCode: "001",
                    userName: "003",
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
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_6'));
        }

        function onSaveSuccess(data) {
            if(data.data.success){
                sidePanelManager.close();
                $rootScope.$emit('to-addparent', 'parent');
                notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, { reload: true });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_6'));
            }  
            busyIndicatorService.hide();
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevel_ModelLevel';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelLevel';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelLevel-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevel.ModelLeveladdctrl.Tips_7'
            }
        };
        $stateProvider.state(state);
    }
}());
