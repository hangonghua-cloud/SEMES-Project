(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.PurchaseManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.PlanApp.PurchaseManage.PurchaseOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_1'));
            // sidePanelManager.open('e');
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
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
            self.selectClick2 = SelectEquipmentModal;
            self.supplierClick = supplierClick;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {
            self.typeOrderType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), ItemValue: "" }]
            };
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), ItemValue: "" }]
            };
            self.typeArrivalStatus = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), ItemValue: "" }]
            };
            // self.typeSupplier = {
            //     value: { SupplierName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), SupplierCode: "" },
            //     options: [{ SupplierName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2'), SupplierCode: "" }]
            // };


            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.typeOrderType.options = res.data.resultData;
                    self.typeOrderType.value = self.typeOrderType.options.find(t => t.ItemValue == self.currentItem.OrderType);
                }
            })
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == self.currentItem.Unit);
                }
            })
            commonService.getDataItemDuatil("OrderArrivalStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeArrivalStatus.options = res.data.resultData;
                    self.typeArrivalStatus.value = self.typeArrivalStatus.options.find(t => t.ItemValue == self.currentItem.ArrivalStatus);
                }
            })
            // var url = commonService.getMesApiAddress("material") + "Base_SupplierBindMaterialGroup/GetSupplierBindMaterialSelect?materialCode=" + self.currentItem.MaterialCode;
            // commonService.callWebApiGet(url, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeSupplier.options = res.data.resultData;
            //         self.typeSupplier.options.splice(0, 0, {
            //             SupplierCode: "",
            //             SupplierName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2')
            //         });
            //         self.typeSupplier.value = self.typeSupplier.options.find(t => t.SupplierCode == self.currentItem.Supplier);
            //     }
            // });
            // var url = commonService.getMesApiAddress("material") + "Base_SupplierManage/GetBase_SupplierManageList?checkType=";
            // commonService.callWebApiGet(url, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeSupplier.options = res.data.resultData;
            //         self.typeSupplier.options.splice(0, 0, {
            //             SupplierCode: "",
            //             SupplierName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_2')
            //         });
            //         self.typeSupplier.value = self.typeSupplier.options.find(t => t.SupplierCode == self.currentItem.Supplier);
            //     }
            // });
        }



        //选择设备  使用公用方法
        function SelectEquipmentModal() {
            debugger
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                // {
                //     field: 'EquipmentId',
                //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_4'),
                //     width: 200
                // },
                {
                    field: 'Abbr',
                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_5'),
                    width: 200
                }
            ];
            //queryName: "",
            //queryCode: ""
            /*功能描述:单选弹窗方法
            *创    建:刘万军
            *创建时间:2021-1-22
            *参    数:PostUrl API接口
            *         sidx  排序字段
            *         sord  排序方式
            *         columnDefs   grid显示字段列表
            *         callback  回调方法
            */
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_6'), commonService.getMesApiAddress("material") + "Base_SupplierManage/Base_SupplierManagePageList", [{ 'FieldCode': 'Abbr', 'FileldName': commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_5'), 'FiledType': 'Text' }], "SupplierCode", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalEquipment_callback(res) {
            //alert(JSON.stringify(res));
            debugger
            self.currentItem.Abbr = res.Abbr;
            self.currentItem.Supplier = res.SupplierCode;
        }

        //选择供应商
        function supplierClick(levelCode) {

            //debugger;
            let parentSupplierCodes = "";
            if (levelCode == '2') {
                parentSupplierCodes = self.currentItem.Supplier;
            }
            else {
                parentSupplierCodes = self.currentItem["SupplierCode" + (levelCode - 1)];
            }

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'Base_SupplierManage/Base_SupplierManagePageDataTableList',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                SupplierLevel: levelCode,
                                parentSupplierCodes: parentSupplierCodes
                            },
                            pagination: {},
                            multiple: true,
                            sidx: "SupplierCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_34'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'SupplierCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_35'),
                                    width: 130
                                },
                                {
                                    field: 'SupplierName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_36'),
                                    width: 300
                                },
                                {
                                    field: 'Abbr',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_37'),
                                    width: 150
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                self.currentItem["SupplierCode" + levelCode] = data.map(t => { return t.SupplierCode; }).join();
                self.currentItem["SupplierName" + levelCode] = data.map(t => { return t.Abbr; }).join();
            });
        }


        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_7') });

            //self.currentItem.Unit = self.typeUnit.value.ItemValue;
            self.currentItem.OrderType = self.typeOrderType.value.ItemValue;
            //self.currentItem.ArrivalStatus = self.typeArrivalStatus.value.ItemValue;
            // self.currentItem.Supplier = self.typeSupplier.value.SupplierCode;
            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("plan") + 'PL_PurchaseOrder/SavePL_PurchaseOrder';
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
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_9'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_10'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_10'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_PurchaseManage_PurchaseOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/PurchaseManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PurchaseOrder-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.PurchaseManage.editJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
