(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.AppUserInfo').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfo.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            LoadAppRoles();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.currentItem.PWDAgin = self.currentItem.PWD;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.AppRoles = {};
            self.RoleCode = null;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.CreateUser = commonService.getLoginUser().loginName;
            self.currentItem.APPRole = self.SelRole.RoleCode;
            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };
            // console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            // console.log("postData------------------------------------" + JSON.stringify(postData));
            if (self.currentItem.PWD == self.currentItem.PWDAgin) {
                var url = commonService.getMesApiAddress() + 'BSAppLogUserInfo/SaveForm';

                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
                //console.log("----------------------------------11111" + req);
            }
            else {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_2'));
                self.currentItem.PWD = "";
                self.currentItem.PWDAgin = "";
            }
        }
        function LoadAppRoles() {
            var url = commonService.getMesApiAddress("") + "/System/GetRoles_PDA";

            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success) && data.data.resultData.length > 0) {
                    self.AppRoles = data.data.resultData;
                    for (var i = 0; i < self.AppRoles.length; i++) {
                        debugger
                        if (self.AppRoles[i].RoleCode == self.currentItem.APPRole) {
                            self.SelRole = self.AppRoles[i];
                            return;
                        }
                    }

                }

            }, function (error) {

                backendService.genericError(error, commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_3'));
            });
        }
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_4'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_3'));
            }
        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_3'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_AppUserInfo_AppUserInfo';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/AppUserInfo';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/AppUserInfo-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoeditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
