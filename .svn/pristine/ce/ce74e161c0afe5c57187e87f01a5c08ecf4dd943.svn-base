(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.PrinterWorkOrderBG.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service

            self.currentItem = angular.copy($stateParams.selectedItem);

            self.validInputs = false;

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.processChange = processChange;
            loadProcess(self.currentItem.ProcessRoute);
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {


            self.typeProcessOperation = {
                value: { OperationName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2'), OperationCode: "" },
                options: [{ OperationName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2'), OperationCode: "" }]
            };
            self.typeUserGroup = {
                value: { PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2'), PTeamCode: "" },
                options: [{ PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2'), PTeamCode: "" }]
            }
            self.typeShift = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2'), ItemValue: "" }]
            }

            commonService.getDataItemDuatil("Shift").then(function (res) {
                if (res && res.data.success) {
                    self.typeShift.options = res.data.resultData;
                    self.typeShift.value = self.typeShift.options.find(t => t.ItemValue == self.currentItem.Team);
                }
            })

        }
        function processChange(oldval, newval) {

            if (!!newval) {
                var url2 = commonService.getMesApiAddress("ProduceManage") + "PM_TeamPerson/GetPM_TeamPersonList?ProcessCode=" + newval.OperationCode;
                commonService.callWebApiGet(url2, null).then(function (res) {
                    if (res && res.data.success) {
                        self.typeUserGroup.options = res.data.resultData;
                        self.typeUserGroup.options.splice('0', '0', {
                            PTeamCode: "",
                            PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2')
                        });
                        self.typeUserGroup.value = self.typeUserGroup.options.find(t => t.PTeamCode == self.currentItem.UserGroup);
                    }
                })
            }
        }

        //获取工艺路线下的工序
        function loadProcess(processCode) {

            var postData2 = {
                queryJson: {
                    ProcessCode: processCode
                }
            }
            var url2 = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/BS_ProcessOfOperationsPageDataTableList';
            commonService.callWebApiPost(url2, postData2).then(function (resProcess) {
                if (resProcess && resProcess.data.success) {
                    self.typeProcessOperation.options = resProcess.data.resultData.rows;
                    self.typeProcessOperation.options.splice(0, 0, {
                        OperationCode: "",
                        OperationName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_2')
                    });
                    self.typeProcessOperation.value = self.typeProcessOperation.options.find(t => t.OperationCode == self.currentItem.ProcessCode);
                }
            })
        }




        function save() {
            
            self.currentItem.ProcessCode = self.typeProcessOperation.value.OperationCode;
            self.currentItem.UserGroup = self.typeUserGroup.value.PTeamCode;

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_3') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrderBG/SavePM_PrinterWorkOrderBG';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //取消
        function cancel() {
            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
           
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_4'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_5'));
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterWorkOrderBG_PrinterWorkOrderBG';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterWorkOrderBG';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterWorkOrderBG-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterWorkOrderBG.editJS.Tips_6'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
