(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.APPRoleFunction').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunction.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope,commonService,notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_1'));
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

            var isEnabledMark = [
                {
                    label: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_2'),
                    checked: true
                }
            ];
            self.EnabledMark = isEnabledMark;

            self.AppModuleOptions = {
                options: [{
                    ModuleCode: 'MaterialModel',
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_3')
                },
                {
                    ModuleCode: 'QualityModel',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_4')
                },
                {
                    ModuleCode: 'EquipmentModel',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_5')
                },
                {
                    ModuleCode: 'TechnologyModel',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_6')
                },
                {
                    ModuleCode: 'select',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_7')
                }],
                selectedOption: {ModuleCode: 'select',ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_7')}
            }       

            self.TypeOptions = {
                options: [{
                    Code: '1',
                    Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_8')
                },
                {
                    Code: '2',//2
                    Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_9')
                },
                {
                    Code: 'select',//2
                    Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_7')
                }],
                selectedOption: {Code: 'select',Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_7')}
            }       
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.CreateUser = commonService.getLoginUser().loginName;
            self.currentItem.ModuleCode=self.AppModuleOptions.selectedOption.ModuleCode;
            self.currentItem.ModuleName=self.AppModuleOptions.selectedOption.ModuleName;
            self.currentItem.Type=self.TypeOptions.selectedOption.Code;
            self.currentItem.EnabledMark = self.EnabledMark[0].checked;
            var postData = {
                KeyValue: "",
                Entity: self.currentItem
            };
           // console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            //console.log("postData---vxcvcx---------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'BSAppRole/SaveRoleFunctionForm';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess,onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_10'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_11'));
            }
        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_11'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_APPRoleFunction_APPRoleFunction';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/APPRoleFunction';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/APPRoleFunction-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctionaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
