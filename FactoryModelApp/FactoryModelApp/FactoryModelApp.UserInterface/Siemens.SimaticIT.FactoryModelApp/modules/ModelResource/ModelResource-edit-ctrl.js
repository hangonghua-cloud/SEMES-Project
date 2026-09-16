(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelResource').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResource.service', '$state',
        '$stateParams', 'common.base', '$filter', '$scope', 'commonService',
        'common.widgets.notificationTile.globalService', 'common.base', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope,
        commonService, notificationService, base, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        var logger, rootstate, messageservice;
        var inputCode = "";

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;
            messageservice = base.widgets.messageOverlay.service;
            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            inputCode = self.currentItem.ModelLeve;
            self.validInputs = false;

            self.LevelCtrl = [];

            GetParentLevel()



            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.LevelChangeHandler = LevelChangeHandler;
        }
        function LevelChangeHandler(oldValue, newValue) {
            //var code = self.LevelCtrl.selectedOption.LevelCode;
            LoadParent(newValue.LevelCode);
            //alert(34);
        }

        function GetParentLevel() {

            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetFormJsonByResourceCode?resourceCode=" + self.currentItem.ParentResource;
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.currentItem.ParentModelLeve = res.data.resultData.ModelLeve;
                    LoadLevel();
                }
            })
        }


        function LoadLevel() {

            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelLevel/GetParentListJson?queryJson={'Level':'" + inputCode + "'}";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    if (jsonData.length == 0) {
                        jsonData = [{
                            CreateDate: "",
                            CreateUser: "admin",
                            Describe: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_2'),
                            EnabledMark: true,
                            Level: 0,
                            LevelCode: "0",
                            LevelName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_3'),
                            ModifyDate: null,
                            ModifyUser: null
                        }];
                    }

                    self.LevelCtrl = jsonData;
                    self.FirstValue = self.LevelCtrl.find(t => t.LevelCode == self.currentItem.ParentModelLeve);
                } else {
                    self.LevelCtrl = [];
                    //console.log(self.LevelCtrl.options);
                }
            }, function (error) {
                // console.log('-----------error------------');
                // console.log(error);
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_4'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_5',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

        }

        function LoadParent(code) {

            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListByLevelJson?queryJson={'Level':'" + code + "'}";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success) && data.data.resultData.length > 0) {
                    if (code != "0") {
                        var jsonData = data.data.resultData;
                        self.ParentResourceCtrl = jsonData;
                    }
                } else {
                    var jsonData = [{
                        CreateDate: "",
                        CreateUser: "",
                        Describe: "",
                        EnabledMark: false,
                        ModelLeve: "1",
                        ModifyDate: null,
                        ModifyUser: null,
                        ParentResource: "(None)",
                        ResourceCode: "0",
                        ResourceName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_6')
                    }];
                    self.ParentResourceCtrl = jsonData;
                }
                self.SecondValue = self.ParentResourceCtrl.find(t => t.ResourceCode == self.currentItem.ParentResource);
            }, function (error) {
                // console.log('-----------error------------');
                // console.log(error);
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_4'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_5',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var username = commonService.getLoginUser().loginName;
            self.currentItem.CreateUser = username;
            self.currentItem.ModelLeve = inputCode;
            var aa = self.SecondValue;
            if (self.SecondValue.ResourceCode == '') {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_7'));
            }
            else {
                if (self.SecondValue.ResourceCode == '(None)') {
                    self.currentItem.ParentResource = "0";
                }
                else {
                    self.currentItem.ParentResource = self.SecondValue.ResourceCode;
                }

                //self.currentItem.ResourceCode = self.currentItem.ResourceCode;
                //self.currentItem.ResourceName = self.currentItem.ResourceName;

                var postData = {
                    KeyValue: self.currentItem.ResourceCode,
                    Entity: self.currentItem
                };

       
                var url = commonService.getMesApiAddress("factory") + 'LevelManage/BsModelWithResource/Save';

                //var req = commonService.callWebApiPost(url, postData);
                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_8'));
        }
        function onSaveSuccess(data) {
            if (data.data.success) {
                sidePanelManager.close();
                $rootScope.$emit('to-addparent', 'parent');
                //notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, { reload: false });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceeditctrl.Tips_8'));
            }
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
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelResource-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
