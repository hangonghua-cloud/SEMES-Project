(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.APPRoleFunction').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunction.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,commonService,notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_1'));
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

            var isEnabledMark = [
                {
                    label: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_2'),
                    checked: true
                }
            ];
            self.EnabledMark = isEnabledMark;

            self.AppModuleOptions = {
                value: {},
                options: [{
                    ModuleCode: 'MaterialModel',
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_3')
                },
                {
                    ModuleCode: 'QualityModel',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_4')
                },
                {
                    ModuleCode: 'EquipmentModel',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_5')
                },
                {
                    ModuleCode: 'TechnologyModel',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_6')
                },
                {
                    ModuleCode: 'select',//2
                    ModuleName: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_7')
                }],
                selectedOption: {ModuleCode: self.currentItem.ModuleCode,ModuleName:self.currentItem.ModuleName}
            }  
            //self.AppModuleOptions.selectedOption={ModuleCode: self.currentItem.ModuleCode,ModuleName:self.currentItem.ModuleName};     

            self.TypeOptions = {
                value:{},
                options: [{
                    Code: '1',
                    Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_8')
                },
                {
                    Code: '2',//2
                    Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_9')
                },
                {
                    Code: 'select',//2
                    Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_7')
                }],
                selectedOption: {Code: 'select',Name: commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_7')}
            }       
        }
        self.TypeOptions.selectedOption={Code:self.currentItem.Type,Name:self.currentItem.TypeName};

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.ModifyUser = commonService.getLoginUser().loginName;
            self.currentItem.ModuleCode=self.AppModuleOptions.selectedOption.ModuleCode;
            self.currentItem.ModuleName=self.AppModuleOptions.selectedOption.ModuleName;
            self.currentItem.Type=self.TypeOptions.selectedOption.Code;
            self.currentItem.EnabledMark = self.EnabledMark[0].checked;
            var postData = {
                KeyValue: self.currentItem.Id,
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
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_10'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_11'));
            }
        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_11'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_APPRoleFunction_APPRoleFunction';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/APPRoleFunction';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/APPRoleFunction-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.APPRoleFunction.APPRoleFunctioneditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
