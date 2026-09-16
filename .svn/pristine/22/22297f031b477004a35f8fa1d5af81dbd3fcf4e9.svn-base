(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFileds.service', '$state',
        '$stateParams', 'common.base', '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        var inputCode = "";

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            inputCode = angular.copy($stateParams.code);

            self.FieldTypeCtrl = {
                options: [
                    {
                        TypeKey: 'string',
                        TypeValue: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_2')
                    }, {
                        TypeKey: 'int',
                        TypeValue: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_3')
                    }, {
                        TypeKey: 'datetime',
                        TypeValue: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_4')
                    }
                ],
                selectedOption: {
                    Id: 'string',
                    Name: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_2')
                }
            }

            self.typeRadio = {
                options: [{ label: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_9'), value: true }, { label: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_10'), value: false }]
            }

            //Initialize Model Data
            self.currentItem = {
                CreateUser: "",
                LevelCode: "",
                EnabledMark: true,
                FieldType: "",
                FieldCode: "",
                FieldName: ""
            };
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var username = commonService.getLoginUser().loginName;
            username = username.replace('\\', '\\\\');
            self.currentItem.CreateUser = username;
            self.currentItem.LevelCode = inputCode;
            self.currentItem.EnabledMark = true;
            self.currentItem.FieldType = self.FieldTypeCtrl.selectedOption.TypeKey;

            var postData = {
                KeyValue: "",
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("factory") + 'LevelManage/BsModelLevelExtendFields/Save';


            if (self.currentItem.FieldCode == "") {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_5'));
            }
            else if (self.currentItem.FieldName == "") {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_6'));
            }
            else if (self.currentItem.FieldType == "") {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_7'));
            }
            else {
                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveError(error) {

            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_8'));
        }
        function onSaveSuccess(data) {
            if (data.data.success) {
                sidePanelManager.close();
                $rootScope.$emit('to-parent', 'parent');
                notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, {});//reload: false
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_8'));
            }
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevelExtendFileds_ModelLevelExtendFileds';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelLevelExtendFileds';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelLevelExtendFileds-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            params: {
                code: null,
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
