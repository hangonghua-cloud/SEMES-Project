(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.APPRole').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.APPRole.APPRole.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope,commonService,notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleaddctrl.Tips_1'));
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
                    label: commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleaddctrl.Tips_2'),
                    checked: true
                }
            ];
            self.EnabledMark = isEnabledMark;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.CreateUser = commonService.getLoginUser().loginName;
            self.currentItem.EnabledMark = self.EnabledMark[0].checked;
            var postData = {
                KeyValue: "",
                Entity: self.currentItem
            };
           // console.log("postData------------------------------------" + JSON.stringify(self.currentItem));
            console.log("postData---vxcvcx---------------------------------" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress() + 'BSAppRole/SaveForm';

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
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleaddctrl.Tips_3'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleaddctrl.Tips_4'));
            }
        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.APPRole.APPRoleaddctrl.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_APPRole_APPRole';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/APPRole';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/APPRole-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.APPRole.APPRoleaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
