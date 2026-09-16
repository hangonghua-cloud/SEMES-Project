(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Persons').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Persons.Persons.service', '$state', '$stateParams', 'common.base',
        '$filter', '$rootScope', '$scope', 'commonService', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, commonService, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.IsEnabled = [
                {
                    label: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_2'),
                    checked: true
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
                    { ItemValue: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_3'), ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_3') },
                    { ItemValue: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_4'), ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_4') },
                ],
                selectedOption: { ItemValue: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_3'), ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_3') }
            }
            self.typeDeptSelect = {
                selectedOption: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_5') },
                options: [{ ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_5') }]
            };

            self.typePositionSelect = {
                selectedOption: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_5') },
                options: [{ ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_5') }]
            };
            commonService.getDataItemDuatil("Position").then(function (res) {
                if (res && res.data.success) {
                    self.typePositionSelect.options = res.data.resultData;
                    self.typePositionSelect.selectedOption = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            var url = commonService.getMesApiAddress() + "Base/GetList_Department_Control";
            commonService.callWebApiGet(url).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.typeDeptSelect.options = jsonData;
                    self.typeDeptSelect.selectedOption = { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_5') };
                } else {
                    var jsonData = [{
                        Id: "",
                        Server: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_6')
                    }];
                    self.typeDeptSelect = jsonData;
                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_7'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_8',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });

            // commonService.getDataItemDuatil("MaterialCheckinType").then(function (res) {
            //     if (res && res.data.success) {

            //         res.data.resultData.forEach(element => {
            //             if (element.ItemCode != "") {
            //                 self.typeFactory.options.push({ id: element.ItemName, name: element.ItemName });
            //             }
            //         });
            //         // self.MaterialCheckinType.options = res.data.resultData;
            //     }
            // });

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
                    // for (let index = 0; index < list.length; index++) {
                    //     let entity = list[index];
                    //     if (self.currentItem.FactoryName && self.currentItem.FactoryName.indexOf(entity.ResourceName) != -1) {
                    //         entity.selected = true//下拉框默认选中
                    //     }
                    // }
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

            self.currentItem.Creator = commonService.getLoginUser().loginName;

            var url = commonService.getMesApiAddress() + 'Base/SavePeopleForm';
            //console.log("----------------" + url);

            var req = commonService.callWebApiPost(url, self.currentItem).then(onSaveSuccess, onSaveError);
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

                $rootScope.$emit('to-parent', data);
                notificationService.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_9'));
                $state.go('^', {}, { reload: false });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_10'));
            }
        }

        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_10'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_SystemApp_Persons_Persons';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Persons';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/Persons-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Persons.Personsaddctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
