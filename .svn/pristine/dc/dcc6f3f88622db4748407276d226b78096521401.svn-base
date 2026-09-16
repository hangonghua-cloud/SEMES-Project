(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.isReadOnly2 = false;
            self.isReadOnly3 = false;
            self.isReadOnly4 = true;
            loadReadOnly();


            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function loadReadOnly() {
            /**
         * 编辑工单时：1、“工单状态”是创建、审核和发布状态时，下单总片数、下单总盒数、下单总托数、下单起始托可编辑，并且其修改的数量关联影响发货总片数、发货总盒数、发货总托数、发货起始托，此时“发货”相关的四个字段不可编辑；
            2、“工单状态”是已发料、生产中状态时，下单总片数、下单总盒数、下单总托数、下单起始托不可编辑，发货总片数、发货总盒数、发货总托数、发货起始托，可编辑；
            3、“工单状态”是已完成状态，且PO号状态≠已发货时，下单总片数、下单总盒数、下单总托数、下单起始托不可编辑，发货总片数、发货总盒数、发货总托数、发货起始托，可编辑；
            4、“工单状态”是已完成状态，且PO号状态=已发货时，下单总片数、下单总盒数、下单总托数、下单起始托，发货总片数、发货总盒数、发货总托数、发货起始托，均不可编辑；
         */
            //OrderStatus 工单  POStatus 状态
            // if (self.currentItem.OrderStatus == "1" || self.currentItem.OrderStatus == "2" || self.currentItem.OrderStatus == "3") {
            //     self.isReadOnly1 = false;
            //     self.isReadOnly2 = true;
            // } else if (self.currentItem.OrderStatus == "4" || self.currentItem.OrderStatus == "5") {
            //     self.isReadOnly1 = true;
            //     self.isReadOnly2 = false;
            // } else if (self.currentItem.OrderStatus == "6" && self.currentItem.POStatus != "5") {
            //     self.isReadOnly1 = true;
            //     self.isReadOnly2 = false;
            // } else if (self.currentItem.OrderStatus == "6" && self.currentItem.POStatus == "5") {
            //     self.isReadOnly1 = true;
            //     self.isReadOnly2 = true;
            // }  

            // if (self.currentItem.OrderStatus == "1" || self.currentItem.OrderStatus == "2" || self.currentItem.OrderStatus == "3" || self.currentItem.OrderStatus == "4") {
            //     self.isReadOnly3 = false;
            // }


            if (self.currentItem.POStatus == "5") {
                self.isReadOnly2 = true;
            }
            if (self.currentItem.OrderStatus == "1" || self.currentItem.OrderStatus == "2" || self.currentItem.OrderStatus == "3" || self.currentItem.OrderStatus == "4") {
                self.isReadOnly4 = false;
            }
        }

        function initDictionary() {

            self.Factory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ResourceCode: "" }]
            };
            //工艺路线
            self.ProcessRoute = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ProcessCode: "" }]
            };
            //工单状态
            self.typeWO = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ItemValue: "" }]
            };
            //PO号状态
            self.POStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ItemValue: "" }]
            };
            //免产标志
            self.AvoidProduce = {
                value: { ItemName: self.currentItem.AvoidProduce == true ? commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_3') : commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_4'), ItemValue: self.currentItem.AvoidProduce },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_3'), ItemValue: true },
                    { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_4'), ItemValue: false }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2')
                    });
                    self.Factory.value = self.Factory.options.find(t => t.ResourceCode == self.currentItem.FactoryCode);
                }
            });

            var url1 = commonService.getMesApiAddress("material") + "BS_Process/BS_ProcessPageDataTableList";
            var postData = {
                queryJson: {
                    MaterialCode: self.currentItem.MaterialCode,
                    FactoryCode: self.currentItem.FactoryCode
                }
            };
            commonService.callWebApiPost(url1, postData).then(function (res) {

                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData.rows;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2')
                    });
                    self.ProcessRoute.value = self.ProcessRoute.options.find(t => t.ProcessCode == self.currentItem.Process);
                }
            });
            commonService.getDataItemDuatil("WorkOrderStatus").then(function (res) {
                if (res && res.data.success) {
                    let options = res.data.resultData.filter(t => {
                        return t.ItemValue >= self.currentItem.OrderStatus;
                    })
                    self.typeWO.options = options;
                    self.typeWO.options.splice('0', '0', {
                        ItemValue: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2')
                    });
                    self.typeWO.value = self.typeWO.options.find(t => t.ItemValue == self.currentItem.OrderStatus);
                }
            })
            //PO号状态
            commonService.getDataItemDuatil("PoStatus").then(function (res) {
                if (res && res.data.success) {
                    let options = res.data.resultData.filter(t => {
                        return t.ItemValue >= self.currentItem.POStatus;
                    })
                    self.POStatus.options = options;
                    self.POStatus.options.splice('0', '0', {
                        ItemValue: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2')
                    });
                    self.POStatus.value = self.POStatus.options.find(t => t.ItemValue == self.currentItem.POStatus);
                }
            })

            //港口
            self.typePort = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ItemCode: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2'), ItemCode: "" }]
            };
            commonService.getKeyParameterItem({ EnCode: "PORTINFOS" }).then(function (res) {
                if (res && res.data.success) {
                    self.typePort.options = res.data.resultData;
                    self.typePort.options.splice(0, 0, {
                        ItemCode: "",
                        ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_2')
                    });
                    if (self.currentItem.Harbour)
                        self.typePort.value = self.typePort.options.find(t => t.ItemCode == self.currentItem.Harbour);
                }
            });
        }

        //编辑保存
        function save() {

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            if (self.currentItem.OrderStatus == "1" || self.currentItem.OrderStatus == "2" || self.currentItem.OrderStatus == "3") {
                self.currentItem.DeliveryPieces = self.currentItem.OrderPieces;
                self.currentItem.DeliveryBox = self.currentItem.OrderBox;
                self.currentItem.DeliveryPallet = self.currentItem.OrderPallet;
                self.currentItem.DeliveryStartPallet = self.currentItem.OrderStartPallet;
                self.currentItem.DeliveryWholePallet = self.currentItem.OrderWholePallet;
            }

            self.currentItem.Id = angular.copy(self.currentItem.WorkOrderId);
            self.currentItem.Process = self.ProcessRoute.value.ProcessCode;
            self.currentItem.OrderStatus = self.typeWO.value.ItemValue;
            self.currentItem.POStatus = self.POStatus.value.ItemValue;
            self.currentItem.AvoidProduce = self.AvoidProduce.value.ItemValue;
            self.currentItem.Harbour = self.typePort.value.ItemCode;
            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem,
                DXZH: self.currentItem.DXZH
            };

            var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/SavePL_WorkOrderForm';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_5') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_7'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_7'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.editOrder',
            url: '/editOrder/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-editOrder.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.editOrderJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
