(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelResource').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResource.service', '$state', '$stateParams', 
    'common.base', '$filter', '$rootScope', '$scope', 'commonService','$compile'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, $compile) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;
           
            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
          
            if (self.currentItem) {
                var StrForm = '';
                if (self.currentItem.FieldType == "string") {
                    StrForm = '<sit-property sit-widget="sit-text" sit-value="vm.currentItem.FieldValue" sit-validation="{required: true}">' + self.currentItem.FieldName + ':</sit-property>';
                } else if (self.currentItem.FieldType == "int") {
                    
                    self.currentItem.FieldValue=parseFloat(self.currentItem.FieldValue);
                    StrForm = '<sit-property sit-widget="sit-numeric" sit-value="vm.currentItem.FieldValue"  sit-validation="{required: true, max:99999999}" >' + self.currentItem.FieldName + ':</sit-property>';
                }
                else if (self.currentItem.FieldType == "datetime") {
                    StrForm = '<sit-property sit-widget="sit-date-time-picker" sit-value="vm.currentItem.FieldValue" sit-format="\'yyyy-MM-dd HH:mm\'" sit-validation="{required: true}" sit-read-only="false">' + self.currentItem.FieldName + ':</sit-property>';
                }
                $("#myDivForm").html($compile(StrForm)($scope));
            }


            self.validInputs =true;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {        
            var username = commonService.getLoginUser().loginName;
            //username = username.replace('\\', '\\\\');
            console.log(self.currentItem)
            var postData = {
                ResourceCode: self.currentItem.ResourceCode,
                FieldCode: self.currentItem.FieldCode,
                FieldValue: self.currentItem.FieldValue,
                userName: username
            };


            var url = commonService.getMesApiAddress("factory") + 'level/Save_FieldData';

            commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    onSaveSuccess();

                } else {

                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditDetailctrl.Tips_2'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditDetailctrl.Tips_3',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            $rootScope.$emit('to-parent', 'parent');   
            sidePanelManager.close();
            //$state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelResource_ModelResource';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelResource';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelResource-editDetail.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditDetailctrl.Tips_4'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
