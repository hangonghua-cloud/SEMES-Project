(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterOrder').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            //initFormJson();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentOrder = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.validInputs = false;
            self.isShowBatchDate = false;

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.processChange = processChange;
            self.IsMergeBatchChange = IsMergeBatchChange;
            self.MergeBatchChange = MergeBatchChange;
            self.BatchDateChange = BatchDateChange;
            loadProcess(self.currentOrder.ProcessRoute);
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {
            // self.ProcessRoute = {
            //     value: { ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ProcessCode: "" },
            //     options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ProcessCode: "" }]
            // };
            self.typeProcessOperation = {
                value: { OperationName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), OperationCode: "" },
                options: [{ OperationName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), OperationCode: "" }]
            };
            self.typeUserGroup = {
                value: { PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), PTeamCode: "" },
                options: [{ PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), PTeamCode: "" }]
            }
            self.typeShift = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" }]
            }
            self.IsMergeBatch = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_3'), ItemValue: "0" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" },
                { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_4'), ItemValue: "1" },
                { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_3'), ItemValue: "0" }]
            };
            //合并批次
            self.MergeBatch = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" }]
            };


            commonService.getDataItemDuatil("Shift").then(function (res) {
                if (res && res.data.success) {
                    self.typeShift.options = res.data.resultData;
                    self.typeShift.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
        }


        //是否合批change事件
        function IsMergeBatchChange(oldItem, newItem) {

            if (newItem.ItemValue == "1") {
                self.isShowBatchDate = true;
                let queryParmeters = {
                    MaterialCode: self.currentOrder.MaterialCode,
                    //supplierCode: self.currentItem.SupplierCode
                };
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrderBG/GetMaterialBatch';
                commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                    if ((res) && (res.data.success) && res.data.resultData) {
                        res.data.resultData.forEach(item => {
                            self.MergeBatch.options.push({
                                ItemValue: item.BatchNo,
                                ItemName: item.BatchNo,
                                BatchDate: item.BatchDate
                            });
                        });
                    } else {
                        self.MergeBatch = {
                            value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" },
                            options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" }]
                        };
                    }
                });
            }
            else {
                self.isShowBatchDate = false;
                self.MergeBatch = {
                    value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" },
                    options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2'), ItemValue: "" }]
                };
            }
        }
        //合并批次改变事件
        function MergeBatchChange(oldItem, newItem) {

            if (self.IsMergeBatch.value.ItemValue == "1") {
                self.currentItem.BatchNo = newItem.ItemName;
                self.BatchDate = new Date(newItem.BatchDate)
            } else {
                self.currentItem.BatchNo = "";
            }
        }
        //批次日期改变事件
        function BatchDateChange(oldItem, newItem) {

            if (self.IsMergeBatch.value.ItemValue == "0") { //不合批
                let queryParmeters = {
                    MaterialCode: self.currentOrder.MaterialCode,
                    BatchDate: commonService.ConvertToLocalDate(newItem)
                };
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrderBG/GetNewBatch';
                commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                    if ((res) && (res.data.success) && res.data.resultData) {
                        self.currentItem.BatchNo = res.data.resultData;
                    } else {

                    }
                });
            }
        }
        function processChange(oldval, newval) {
            if (!!newval) {
                var url2 = commonService.getMesApiAddress("ProduceManage") + "PM_TeamPerson/GetPM_TeamPersonList?ProcessCode=" + newval.OperationCode;
                commonService.callWebApiGet(url2, null).then(function (res) {
                    if (res && res.data.success) {
                        self.typeUserGroup.options = res.data.resultData;
                        self.typeUserGroup.options.splice('0', '0', {
                            PTeamCode: "",
                            PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2')
                        });
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
                        OperationName: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_2')
                    });
                }
            })
        }


        function save() {

            self.currentItem.FactoryCode = self.currentOrder.FactoryCode;
            self.currentItem.FactoryName = self.currentOrder.FactoryName;
            self.currentItem.WorkOrder = self.currentOrder.WorkOrder;
            self.currentItem.ProcessCode = self.typeProcessOperation.value.OperationCode;
            self.currentItem.UserGroup = self.typeUserGroup.value.PTeamCode;
            self.currentItem.BatchDate = commonService.ConvertToLocalDate(self.BatchDate);
            self.currentItem.MaterialCode = self.currentOrder.MaterialCode;

            //self.currentItem.Team = self.typeShift.value.ItemValue;

            if (self.currentOrder.MeterNum < self.currentItem.MeterNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_5'), commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_6'));
                return false;
            }
            if (self.currentOrder.ReelNum < self.currentItem.ReelNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_7'), commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_6'));
                return false;
            }

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                WorkOrderId: self.currentOrder.Id,
                Entity: self.currentItem
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_8') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_9'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', { gridId: self.currentOrder.OrderId, GridDetailId: self.currentOrder.Id });
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_6'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_6'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder_PrinterOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterOrder';

        var state = {
            name: screenStateName + '.editBG',
            url: '/editBG/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterOrder-editBG.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.editBGJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
