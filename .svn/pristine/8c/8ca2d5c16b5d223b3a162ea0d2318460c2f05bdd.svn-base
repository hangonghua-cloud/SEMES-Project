(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Persons').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Persons.Persons.service', '$state', '$stateParams',
        'common.base', '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            self.IsEnabled = [
                {
                    label: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_2'),
                    checked: self.currentItem.IsEnabled
                }
            ];
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.typeFactoryChange = typeFactoryChange;

            initDictionary();

            setTimeout(function () {
                //初始化grid数据、查询
                LoadFactory();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000
        }
        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: null,
                options: []
            };
            self.typeSexSelect = {
                options: [
                    { ItemValue: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_3'), ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_3') },
                    { ItemValue: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_4'), ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_4') },
                ],
                selectedOption: { ItemValue: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_3'), ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_3') }
            }
            self.typeDeptSelect = {
                selectedOption: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_5') },
                options: [{ ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_5') }]
            };

            self.typePositionSelect = {
                selectedOption: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_5') },
                options: [{ ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_5') }]
            };
            commonService.getDataItemDuatil("Position").then(function (res) {
                if (res && res.data.success) {
                    self.typePositionSelect.options = res.data.resultData;
                    self.typePositionSelect.selectedOption =
                        self.typePositionSelect.options.find(t => t.ItemValue == self.currentItem.Position_ID);
                }
            })

            var url = commonService.getMesApiAddress() + "Base/GetList_Department_Control";
            commonService.callWebApiGet(url).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.typeDeptSelect.options = jsonData;
                    self.typeDeptSelect.selectedOption =
                        self.typeDeptSelect.options.find(t => t.ItemValue == self.currentItem.Department_ID);
                } else {
                    var jsonData = [{
                        Id: "",
                        Server: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_6')
                    }];
                    self.typeDeptSelect = jsonData;
                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_7'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_8',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
        }

        function typeFactoryChange(oldVal, newVal) {
            self.currentItem.FactoryCode = "";
            self.currentItem.FactoryName = "";
            newVal.forEach(x => {
                self.currentItem.FactoryCode += x.ResourceCode + ",";
                self.currentItem.FactoryName += x.ResourceName + ",";
            });
        };


        function LoadFactory() {
            //工厂
            commonService.getResourceExtendInfo({ LevelCode: "Factory", role: "admin" }).then(function (res) {
                if (res && res.data.success) {
                    let list = res.data.resultData;
                    for (let index = 0; index < list.length; index++) {
                        let entity = list[index];
                        if (self.currentItem.FactoryName && self.currentItem.FactoryName.indexOf(entity.ResourceName) != -1) {
                            entity.selected = true//下拉框默认选中
                        }
                    }
                    self.typeFactory.options = list;
                    console.log(self.typeFactory.options);
                }
            });
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            self.currentItem.IsEnabled = self.IsEnabled[0].checked ? 1 : 0;
            self.currentItem.Sex = self.typeSexSelect.selectedOption.ItemValue;
            if (self.typeDeptSelect.selectedOption && self.typeDeptSelect.selectedOption.ItemValue != "") {
                self.currentItem.Department_ID = self.typeDeptSelect.selectedOption.ItemValue;
            }
            self.currentItem.Position_ID = self.typePositionSelect.selectedOption.ItemValue;
            self.currentItem.ModifyBy = commonService.getLoginUser().loginName;

            var url = commonService.getMesApiAddress() + 'Base/SavePeopleForm';
            var req = commonService.callWebApiPost(url, self.currentItem).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();

                $rootScope.$emit('to-parent', data);
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_9'));
                $state.go('^', {}, { reload: false });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_10'));
            }
        }

        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_10'));
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
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Persons_Persons';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Persons';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Persons-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Persons.Personseditctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
