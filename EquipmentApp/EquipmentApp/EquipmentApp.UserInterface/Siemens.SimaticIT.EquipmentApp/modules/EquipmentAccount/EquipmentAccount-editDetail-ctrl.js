(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EquipmentAccount').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount.service',
        '$state', '$stateParams', 'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccounteditDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;


            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.ProducedDate = new Date(self.currentItem.ProducedDate);
            self.UserDate = new Date(self.currentItem.UserDate);

            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.StatusConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            var url = commonService.getDataItemDuatil("EquipmentStatus").then(function (res) {
                self.StatusConfig.options = res.data.resultData;
                self.StatusConfig.selectedOption = self.StatusConfig.options.find(t => t.ItemValue == self.currentItem.EquipStatus)
            });
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        var username = commonService.getLoginUser().loginName;
        //username = username.replace('\\', '\\\\');
        self.userName = username;


        function save(currentItem) {
            if (self.StatusConfig.selectedOption != null) {
                self.currentItem.EquipStatus = self.StatusConfig.selectedOption.ItemValue;
            }

            self.currentItem.ProducedDate = commonService.ConvertToLocalDate(self.ProducedDate);
            self.currentItem.UserDate = commonService.ConvertToLocalDate(self.UserDate);


            var postData = {
                userName: self.UserCode,
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentManageItem/SaveEP_EquipmentManageItem';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccounteditDetailctrl.Tips_2'));
                $rootScope.$emit('to-addChildItem', self.currentItem);
                //$state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccounteditDetailctrl.Tips_3'));
            }

        }
        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccounteditDetailctrl.Tips_3'));
        }
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount_EquipmentAccount';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EquipmentAccount';

        var state = {
            name: screenStateName + '.editDetail',
            url: '/editDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EquipmentAccount-editDetail.html',
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
