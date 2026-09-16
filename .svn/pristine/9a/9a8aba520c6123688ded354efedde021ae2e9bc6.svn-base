(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFileds.service', '$state', 
        '$stateParams', 'common.base', '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.FieldTypeCtrl = {
                options: [
                    {
                        TypeKey: 'string',
                        TypeValue: '字符类型'
                    }, {
                        TypeKey: 'int',
                        TypeValue: '数字类型'
                    }, {
                        TypeKey: 'datetime',
                        TypeValue: '日期类型'
                    }
                ],
                selectedOption: {
                    TypeKey: 'string',
                    TypeValue: '字符类型'
                }
            }
            self.typeRadio = {
                options: [{ label: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_9'), value: true }, { label: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledsaddctrl.Tips_10'), value: false }]
            }

            self.validInputs = false;
            var id=self.currentItem.FieldType;
            var value="";
            if(id == "string") value=commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_2');
            if(id == "int") value=commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_3');
            if(id == "datetime") value=commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_4');
            self.FieldTypeCtrl.selectedOption = {TypeKey: id, TypeValue: value};

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var username = commonService.getLoginUser().loginName;
            
            self.currentItem.ModifyUser = username;
            self.currentItem.FieldType = self.FieldTypeCtrl.selectedOption.TypeKey;

       
            var postData = {
                KeyValue: self.currentItem.id,
                Entity: self.currentItem
            };

       
            var url = commonService.getMesApiAddress("factory") + 'LevelManage/BsModelLevelExtendFields/Save';

            if(self.currentItem.FieldCode==""){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_5'));
            }
            else if(self.currentItem.FieldName==""){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_6'));
            }
            else if(self.currentItem.FieldType==""){
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_7'));
            }
            else{
                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
              
                //onSaveSuccess();
            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }


        function onSaveError(error){
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_8'));
        }
        function onSaveSuccess(data) {
            if(data.data.success){
                sidePanelManager.close();
                $rootScope.$emit('to-parent', 'parent'); 
                notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, {  });//reload: false
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_8'));
            }            
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelLevelExtendFileds_ModelLevelExtendFileds';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelLevelExtendFileds';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelLevelExtendFileds-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelLevelExtendFileds.ModelLevelExtendFiledseditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
