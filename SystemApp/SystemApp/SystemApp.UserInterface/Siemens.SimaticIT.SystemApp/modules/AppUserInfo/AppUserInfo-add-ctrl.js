(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.AppUserInfo').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfo.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope,commonService,notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_1'));
            sidePanelManager.open('e');
            LoadAppRoles();
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.validInputs = false;
            self.AppRoles = {};
            self.RoleCode = null;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function LoadAppRoles() {
            var url = commonService.getMesApiAddress("") + "/System/GetRoles_PDA";
         
            commonService.callWebApiGet(url, null).then(function (data) {
                if ((data) && (data.data.success) && data.data.resultData.length > 0) {
                    self.AppRoles = data.data.resultData;
                    console.log(self.AppRoles);
                }
              
            }, function (error) {
            
                backendService.genericError(error, commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_2'));
            });
        }
        function save() {
            self.currentItem.CreateUser = commonService.getLoginUser().loginName;
            self.currentItem.APPRole = self.SelRole.RoleCode;
            var postData = {
                KeyValue: "",
                Entity: self.currentItem
            };
           // console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            //console.log("postData---vxcvcx---------------------------------" + JSON.stringify(postData));
            if(self.currentItem.PWD==self.currentItem.PWDAgin){
                var url = commonService.getMesApiAddress() + 'BSAppLogUserInfo/SaveForm';

                var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess,onSaveError);
               
            }
            else{
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_3'));
                self.currentItem.PWD = "";
                self.currentItem.PWDAgin = "";
            }
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_4'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_2'));
            }
        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_2'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_AppUserInfo_AppUserInfo';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/AppUserInfo';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/AppUserInfo-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.AppUserInfo.AppUserInfoaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
