(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EquipmentAccount').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount.service', 'commonService', 'common.services.authentication', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'common.widgets.notificationTile.globalService'];
    function AddScreenController(dataService, commonService, auth, $state, $stateParams, common, $filter, $scope, notificationService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_1'));
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
            self.selectCodad = selectCodad;
            self.selectSite = selectSite;
            self.selectProcess = selectProcess;

            //工厂
            self.CodadConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //状态
            self.StatusConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //设备类型
            self.TypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //安装位置
            self.SiteConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //所属工序
            self.ProcessConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            //所属工序
            self.LineConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            GetUserInfo();
            GetCodadList();
            GetStatusDictionary();
            GetTypeDictionary();

        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }
        //工厂
        function GetCodadList() {
            var postData = {
                "queryJson": {
                    "ResourceCode": "",
                    "ResourceName": "",
                    "ModelLevel": "Factory"
                }
            };
            var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
            console.log(JSON.stringify(postData));
            commonService.callWebApiPost(url, postData).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.CodadConfig.options = jsonData;
                } else {
                    console.log(self.ParentResourceCtrl.options);
                }
                //self.SecondValue = jsonData[0];
                self.CodadConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_2') })
            }, function (error) {
                // console.log('-----------error------------');
                console.log(error);
            });
        }

        //状态
        function GetStatusDictionary() {
            var url = commonService.getDataItemDuatil("EquipmentStatus").then(function (res) {
                self.StatusConfig.options = res.data.resultData;;
            });
        }
        //设备类型
        function GetTypeDictionary() {
            var url = commonService.getDataItemDuatil("EquipmentTypes").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
            });
        }
        function selectCodad(oldValue, newValue) {
            if (newValue != null) {
                var code = newValue.ResourceCode;
                var postData = {
                    "queryJson": {
                        "ResourceCode": "",
                        "ParentResource": code,
                        "ResourceName": "",
                        "ModelLevel": "WorkShop"
                    }
                };
                var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
                console.log(JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (data) {
                    if ((data) && (data.data.success)) {
                        var jsonData = data.data.resultData;
                        self.SiteConfig.options = jsonData;
                    } else {
                        console.log(self.SiteConfig.options);
                    }
                    //self.SecondValue = jsonData[0];
                    self.SiteConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_2') })
                }, function (error) {
                    // console.log('-----------error------------');
                    console.log(error);
                });
            }
        };

        function selectSite(oldValue, newValue) {
            if (newValue != null) {
                var code = newValue.ResourceCode;
                var postData = {
                    "queryJson": {
                        "ResourceCode": "",
                        "ParentResource": code,
                        "ResourceName": "",
                        "ModelLevel": "Process"
                    }
                };
                var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
                console.log(JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (data) {
                    if ((data) && (data.data.success)) {
                        var jsonData = data.data.resultData;
                        self.ProcessConfig.options = jsonData;
                    } else {
                        console.log(self.ProcessConfig.options);
                    }
                    //self.SecondValue = jsonData[0];
                    self.ProcessConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_2') })
                }, function (error) {
                    // console.log('-----------error------------');
                    console.log(error);
                });
            }
        };

        function selectProcess(oldValue, newValue) {
            if (newValue != null) {
                var code = newValue.ResourceCode;
                var postData = {
                    "queryJson": {
                        "ResourceCode": "",
                        "ParentResource": code,
                        "ResourceName": "",
                        "ModelLevel": "Machine"
                    }
                };
                var url = commonService.getMesApiAddress("factory") + "LevelManage/BsModelWithResource/GetListJson";
                console.log(JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (data) {
                    if ((data) && (data.data.success)) {
                        var jsonData = data.data.resultData;
                        self.LineConfig.options = jsonData;
                    } else {
                        console.log(self.LineConfig.options);
                    }
                    //self.SecondValue = jsonData[0];
                    self.LineConfig.options.splice(0, 0, { ResourceCode: "", ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_2') })
                }, function (error) {
                    // console.log('-----------error------------');
                    console.log(error);
                });
            }
        };

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        /*var username = commonService.getLoginUser().loginName;
        //username = username.replace('\\', '\\\\');
        self.userName = username;*/


        function save(currentItem) {
            //self.currentItem.status = self.StateConfig.value.ItemValue;
            if (self.CodadConfig.selectedOption != null) {
                self.currentItem.TestMethodCoadin = self.CodadConfig.selectedOption.ResourceCode;
            }
            if (self.TypeConfig.selectedOption != null) {
                self.currentItem.EquipmentType = self.TypeConfig.selectedOption.ItemValue;
            }
            // if(self.StatusConfig.selectedOption != null){
            //     self.currentItem.EquipmentStatus = self.StatusConfig.selectedOption.ItemValue;
            // }
            if (self.SiteConfig.selectedOption != null) {
                self.currentItem.InstallationSite = self.SiteConfig.selectedOption.ResourceCode;
            }
            if (self.ProcessConfig.selectedOption != null) {
                self.currentItem.ProcessBelong = self.ProcessConfig.selectedOption.ResourceCode;
            }
            if (self.LineConfig.selectedOption != null) {
                self.currentItem.LineCode = self.LineConfig.selectedOption.ResourceCode;
            }
            // if(self.currentItem.DateOfProduction != null && self.currentItem.DateOfProduction != "" && self.currentItem.DateOfProduction != "Invalid date"){
            //     self.currentItem.DateOfProduction = moment(self.currentItem.DateOfProduction).format("YYYY-MM-DD");
            // }
            // else{
            //     self.currentItem.DateOfProduction = null;
            // }
            // if(self.currentItem.DateOfService != null && self.currentItem.DateOfService != "" && self.currentItem.DateOfService != "Invalid date"){
            //     self.currentItem.DateOfService = moment(self.currentItem.DateOfService).format("YYYY-MM-DD");
            // }
            // else{
            //     self.currentItem.DateOfService = null;
            // }
            self.currentItem.Creator = self.UserCode;
            var postData = {
                userName: self.UserCode,
                KeyValue: "",
                Entity: self.currentItem
            };

            var url = commonService.getMesApiAddress("equipment") + 'EquipmentManage/SaveForm';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_3'));
                $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_4'));
            }
        }

        function onSaveError(error) {
            console.log(123);
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddctrl.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount_EquipmentAccount';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EquipmentAccount';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EquipmentAccount-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            }
        };
        $stateProvider.state(state);
    }
}());
