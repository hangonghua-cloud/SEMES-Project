(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            GetUserInfo();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_1'));
            sidePanelManager.open('e');
        }
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;
            self.currentItem = {};
            self.queryItem = {};
            self.queryItem = angular.copy($stateParams.selectedItem);
            var OrderPiecesNum = 0;
            self.queryItem.forEach((item, index, arr) => {
                OrderPiecesNum = OrderPiecesNum + item.OrderPiecesNum;
            });
            //获取登录用户信息

            self.currentItem.MaterialName = OrderPiecesNum;
            //Initialize Model Data
            self.currentItem.Labelvalue = commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_2');
            self.validInputs = false;

            initDictionary();
            GetOperation();
            // self.typeFactoryChange = typeFactoryChange;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }
        function initDictionary() {
            //初始化 物料分类

            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), ResourceCode: "" }]
            }
            //初始化
            self.typeOperation = {
                value: { OperationName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), OperationCode: "" },
                options: [{ OperationName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), OperationCode: "" }]
            }
            self.TypeOrder = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("TransferType").then(function (res) {
                if (res && res.data.success) {
                    self.TypeOrder.options = res.data.resultData;
                    self.TypeOrder.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            commonService.getResourceExtendInfo({ LevelCode: "Factory", role: "admin" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3')
                    });
                }
            });


        }

        function GetOperation() {
            var url = commonService.getMesApiAddress("plan") + 'PL_BOM/GetWorkOrderOperationsItem';

            commonService.callWebApiPost(url, { queryJson: { WorkOrder: self.queryItem[0].WorkOrder } }).then(function (res) {
                if (res && res.data.success) {

                    var Positions = [{ OperationName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3'), OperationCode: "" }]
                    var retData = res.data.resultData.rows
                    for (var i = 0; i < retData.length; i++) {
                        Positions.push({ OperationName: retData[i].OperationName, OperationCode: retData[i].OperationCode })
                    }
                    self.typeOperation.options = Positions

                }
            })
        }
        // function typeFactoryChange(oldItem, newItem) {
        //     commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
        //         if (res && res.data.success) {
        //             self.typeOperation.options = res.data.resultData;
        //             self.typeOperation.options.splice(0, 0, {
        //                 ResourceCode: "",
        //                 ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_3')
        //             });
        //         }
        //     });
        // }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            //字典类型 取值参考
            var TransferData = [];
            self.currentItem.UpdateByName = self.UserName;
            self.currentItem.UpdateByCode = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.UpdateByName = self.UserCode;
            }
            self.currentItem.AcceptFactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.AcceptFactoryName = self.typeFactory.value.ResourceName;

            if (!!self.TypeOrder.value) {
                self.currentItem.TransTypeCode = self.TypeOrder.value.ItemValue;
                self.currentItem.TransTypeName = self.TypeOrder.value.ItemName;
            }
            if (self.currentItem.TransTypeCode == "OtherProcessTransfer") {
                //部门调拨查看工艺路线是否相同
                var processd = self.queryItem[0].Process;
                var intmum = 0;
                self.queryItem.forEach((item, index, arr) => {

                    if (processd != item.Process) {
                        intmum = intmum + 1;
                    }
                });
                if (intmum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_4'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_5'));
                    return false;
                }
            }

            //判断是否正常工单VC工单
            if (self.currentItem.TransTypeCode == "OtherProcessTransfer") {
                var MMHC = self.queryItem[0].MMHC;
                var Spec = self.queryItem[0].Spec;
                var UV = self.queryItem[0].UV;
                var KCKX = self.queryItem[0].KCKX;
                var vcnum = 0;
                self.queryItem.forEach((item, index, arr) => {

                    if (MMHC != item.MMHC || Spec != item.Spec || UV != item.UV || KCKX != item.KCKX) {
                        vcnum = vcnum + 1;
                    }
                });
                if (vcnum > 0) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_6'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_5'));
                    return false;
                }
            }

            if (self.currentItem.TransTypeCode == "OtherProcessTransfer") {

                if (self.typeOperation.value.OperationCode == null || self.typeOperation.value.OperationCode == "") {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_7'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_5'));
                    return false;
                }

            }

            if (self.typeOperation.value.OperationCode != null && self.typeOperation.value.OperationCode != "") {
                self.currentItem.TransProcessCode = self.typeOperation.value.OperationCode;
                self.currentItem.TransProcessName = self.typeOperation.value.OperationName;
            } else {
                self.currentItem.TransProcessCode = "";
                self.currentItem.TransProcessName = "";
            }
            var oldFactoryCode = self.queryItem[0].FactoryCode;
            if (oldFactoryCode == self.currentItem.AcceptFactoryCode) {

                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_8'), commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_5'));
                return false;
            }
            self.queryItem.forEach((item, index, arr) => {
                // OrderPiecesNum=OrderPiecesNum+item.OrderPiecesNum;
                TransferData.push({
                    SendFactoryCode: item.FactoryCode,
                    SendFactoryName: item.FactoryName,
                    AcceptFactoryCode: self.currentItem.AcceptFactoryCode,
                    AcceptFactoryName: self.currentItem.AcceptFactoryName,
                    ProductOrder: item.ProductOrder,
                    WorkOrder: item.WorkOrder,
                    MaterialCode: item.MaterialCode,
                    TransTypeCode: self.currentItem.TransTypeCode,
                    TransTypeName: self.currentItem.TransTypeName,
                    TransProcessCode: self.currentItem.TransProcessCode,
                    TransProcessName: self.currentItem.TransProcessName,
                    Remark: self.currentItem.Remark,
                    Creator: self.currentItem.UpdateByCode,
                    CreateName: self.currentItem.UpdateByName
                });
            });

            var postData = {
                //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: TransferData
            };
            console.log("kjpf132345" + JSON.stringify(postData));

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_9') });
            var url = commonService.getMesApiAddress('plan') + 'PL_TransfersRecord/SavePL_TransfersRecord';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_10'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_5'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_5'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.Transfer',
            url: '/Transfer',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-Transfer.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.TransferJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
