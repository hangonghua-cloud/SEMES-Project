(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Persons').config(PersonScreenStateConfig);

    PersonScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Persons.Persons.service', '$state', '$stateParams', 'common.base', 
    '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function PersonScreenController(dataService, $state, $stateParams, common, $filter, $rootScope,$scope,commonService,notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            LoadServer();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem =  angular.copy($stateParams.selectedItem);

            console.log("--------------------------------"+JSON.stringify(self.currentItem ));

            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.ServerCtrl = {
                value: {},
                selectedOption: {},
                options: []
            };
            self.searchParams={};
            self.searchParams.isAllow = {
                value: {},
                selectedOption: {},
                options: [{ID: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_2'), Name: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_2')}, {ID: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_3'), Name: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_3')}]
            };
            self.searchParams.isAllow.value={
                ID: self.currentItem.IsDeptLimit
            };
            
        }

       function LoadServer() {
            var queryJson={"Server":"","IP":""};
            var url = commonService.getMesApiAddress() + "BsPrintServer/GetListJson";

            commonService.callWebApiPost(url, queryJson).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.ServerCtrl.options = jsonData;
                    if(self.currentItem.PrintId!=""){
                        self.ServerCtrl.selectedOption={Id: self.currentItem.PrintId, Server: self.currentItem.Server};
                    }
                } else {
                    var jsonData = [{
                        Id: "",
                        Server: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_4')
                    }];
                    self.ServerCtrl = jsonData;
                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_5'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_6',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            // var config=commonService.getLoginUseConfig();
            // console.log("commonService.getLoginUseConfig()commonService.getLoginUseConfig()------------------------------------" + JSON.stringify(config));
            var isAllows =  self.searchParams.isAllow.value.ID;
            if(isAllows == commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_2')){
                isAllows = 'true';
            }else{
                isAllows = 'false';
            }
            var postData = {

                    
                    queryJson: {
                        ID: self.currentItem.ID,
                        Islimit: isAllows
                    }
            };
            //console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            console.log("postData------------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'BsPeopleByPrintServer/UpdateIsDeptLimit';
            console.log("----------------" + url);

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess,onSaveError);
            console.log("----------------------------------11111" + req);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                var returnValue={
                    PrintId:self.ServerCtrl.selectedOption.Id,
                    Server:self.ServerCtrl.selectedOption.Server
                };
                $rootScope.$emit('to-parent', returnValue);
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_7'));
                $state.go('^', {}, { reload: false });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_8'));
            }
        }

        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_8'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    PersonScreenStateConfig.$inject = ['$stateProvider'];
    function PersonScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Persons_Persons';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Persons';

        var state = {
            name: screenStateName + '.ModifyPermissions',
            url: '/ModifyPermissions',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Persons-ModifyPermissions.html',
                    controller: PersonScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Persons.PersonsModifyPermissions.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
