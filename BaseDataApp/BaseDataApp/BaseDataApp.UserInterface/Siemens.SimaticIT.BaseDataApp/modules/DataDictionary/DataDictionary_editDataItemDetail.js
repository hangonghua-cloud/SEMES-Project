(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BaseDataApp.DataDictionary').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary.service'
        , '$state', '$stateParams', 'common.base', '$filter', '$scope', 'common', 'common.widgets.notificationTile.globalService', '$http',
        'common.services.authentication', 'commonService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, c, notification, $http, $auth, commonService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler, messageservice;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary_editDataItemDetail.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            messageservice = common.widgets.messageOverlay.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            initCheckbox();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function initCheckbox() {
            var isEnabledMark = [
                {
                    label: commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary_editDataItemDetail.Tips_2'),
                    checked: (self.currentItem.EnabledMark == 1)
                }
            ];
            self.IsEnabledMark = isEnabledMark;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            //dataService.update(self.currentItem).then(onSaveSuccess, backendService.backendError); 
            self.currentItem.EnabledMark = self.IsEnabledMark[0].checked? 1 : 0;
            if(self.currentItem.ParentId == ""){
                self.currentItem.ParentId = "0";
            }
            // console.log('-----------self.currentItem------------');
            // console.log(self.currentItem);
            var postData = {
                username: $auth.getUser().unique_name,
                keyValue: self.currentItem.ItemDetailId,
                DataItemDetailEntity: self.currentItem
            };
            console.log("postData------------"+JSON.stringify(postData));
            var url=commonService. getMesApiAddress()+"SystemManage/DataItemDetail/SaveForm";
            $http.post(url,postData).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }


        function onSaveSuccess(data) {
            if(data.data.success){
                sidePanelManager.close();
                //$state.go('^', {}, { reload: true });
                $state.go('^', {itemId:self.currentItem.ItemId}, { reload: true });
            }
            else{
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary_editDataItemDetail.Tips_3'));
            }
        }

        function onSaveError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.BaseDataApp.DataDictionary.DataDictionary_editDataItemDetail.Tips_3'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BaseDataApp_DataDictionary_DataDictionary';
        var moduleFolder = 'Siemens.SimaticIT.BaseDataApp/modules/DataDictionary';

        var state = {
            name: screenStateName + '.editDataItemDetail',
            url: '/editDataItemDetail/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/DataDictionary_editDataItemDetail.html',
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
