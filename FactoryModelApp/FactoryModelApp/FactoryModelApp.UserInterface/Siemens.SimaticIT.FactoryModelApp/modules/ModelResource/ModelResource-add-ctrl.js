(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.FactoryModelApp.ModelResource').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResource.service', '$state',
        '$stateParams', 'common.base', '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        var inputCode = "";
        var inputLevel = "";

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            inputCode = angular.copy($stateParams.code);
            self.LevelCtrl = [];
            LoadLevel();
            //Initialize Model Data
            self.currentItem = {
                ResourceCode: null,
                ResourceName: null,
                ParentResourceCode: null
            };
            self.validInputs = false;
            //self.currentItem.ResourceCode = null;

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
        function LoadLevel() {
            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelLevel/GetParentListByCodeJson?queryJson={'Level':'" + inputCode + "'}";
            console.log("url------------------------ " + url);

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    if (jsonData.length == 0) {
                        jsonData = [{
                            CreateDate: "",
                            CreateUser: "admin",
                            Describe: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_2'),
                            EnabledMark: true,
                            Level: 0,
                            LevelCode: "0",
                            LevelName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_3'),
                            ModifyDate: null,
                            ModifyUser: null
                        }];
                    }
                    self.LevelCtrl = jsonData;
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
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_4'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_5',
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
                        ResourceName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_6')
                    }];
                    self.ParentResourceCtrl = jsonData;
                    //console.log(self.ParentResourceCtrl.options);
                }
            }, function (error) {
                // console.log('-----------error------------');
                // console.log(error);
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_4'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_5',
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
            //username = username.replace('\\', '\\\\');
            self.currentItem.CreateUser = username;
            //self.currentItem.ParentResource = self.ParentResourceCtrl.selectedOption.ResourceCode;
            self.currentItem.ResourceCode = self.currentItem.MainResourceCode;
            self.currentItem.ResourceName = self.currentItem.MainResourceName;
            /*if(self.currentItem.ParentResourceCode.ParentResource=='(None)'){
                self.currentItem.ModelLeve=1;
            }
            else{
                self.currentItem.ModelLeve = self.currentItem.ModelLeve.LevelCode;
            }*/
            self.currentItem.ModelLeve = inputCode;
            if (self.currentItem.ParentResourceCode.ParentResource == '') {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_7'));
            }
            else {
                if (self.currentItem.ParentResourceCode.ParentResource == '(None)') {
                    self.currentItem.ParentResource = "0";
                }
                else {
                    self.currentItem.ParentResource = self.currentItem.ParentResourceCode.ResourceCode;
                }

                console.log(self.currentItem)
                var postData = {
                    KeyValue: "",
                    Entity: self.currentItem
                };
                var url = commonService.getMesApiAddress("factory") + 'LevelManage/BsModelWithResource/Save';
                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_8'));
        }

        function onSaveSuccess(data) {
            if (data.data.success) {
                sidePanelManager.close();
                $rootScope.$emit('to-addparent', 'parent');
                //notificationService.warning(data.data.returnMsg);
                $state.go('^', {}, { reload: false });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_8'));
            }
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_FactoryModelApp_ModelResource_ModelResource';
        var moduleFolder = 'Siemens.SimaticIT.FactoryModelApp/modules/ModelResource';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ModelResource-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            params: {
                code: null
            },
            data: {
                title: 'Siemens.SimaticIT.FactoryModelApp.ModelResource.ModelResourceaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
