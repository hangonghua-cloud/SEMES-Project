(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.RawMaterialStock').config(AddDetailScreenStateConfig);

    AddDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;

            self.isButtonVisible = true;
            self.isButtonVisible1 = true;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.selectClick1 = selectClick1;
            self.selectClick2 = selectClick2;
            // self.materialChange = materialChange;
            self.materialClick = materialClick;
            self.selectClick3 = SelectEquipmentModal;
            self.BatchDateChange = BatchDateChange;


        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }



        function initDictionary() {
            //初始化 物料分类
            self.MaterialClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" }]
            };

            //初始化 物料小类
            self.SmallClass = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeIsExemption = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_3'), ItemValue: "0" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_3'), ItemValue: "0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_4'), ItemValue: "1" },
                ]
            };


            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" }]
            };


            self.Warehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ResourceCode: "" }]
            };

            self.ProcureType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" }]
            };


            //初始化 物料小类
            self.IsEnabled = {
                value: { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_4') },
                options: [
                    { ItemValue: true, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_4') },
                    { ItemValue: false, ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_3') }
                ]
            };
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ResourceCode: "" }]
            };
            self.ProcessRoute = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ProcessCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ProcessCode: "" }]
            };
            commonService.getDataItemDuatil("MaterialType").then(function (res) {
                if (res && res.data.success) {
                    self.MaterialClass.options = res.data.resultData;
                    self.MaterialClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("MaterialSmall").then(function (res) {
                if (res && res.data.success) {
                    self.SmallClass.options = res.data.resultData;
                    self.SmallClass.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
                if (res && res.data.success) {
                    self.Warehouse.options = res.data.resultData;
                    self.Warehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2')
                    });
                }
            });
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2')
                    });
                }
            });
            commonService.getDataItemDuatil("ProcureType").then(function (res) {
                if (res && res.data.success) {
                    self.ProcureType.options = res.data.resultData;
                    self.ProcureType.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })
            var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.ProcessRoute.options = res.data.resultData;
                    self.ProcessRoute.options.splice('0', '0', {
                        ProcessCode: "",
                        ProcessName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2')
                    });
                }
            })

        }

        //批次日期改变事件
        function BatchDateChange(oldItem, newItem) {
            // debugger;

            let queryParmeters = {
                batchDate: commonService.ConvertToLocalDate(newItem)
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/GetNewBatch';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success) && res.data.resultData) {
                    self.currentItem.BatchNo = res.data.resultData;
                } else {

                }
            });

        }
        //选择物料
        function materialClick() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("material") + 'Base_MaterialFactory/Base_MaterialFactoryPageDataTableList',
                            method: "Post",
                            queryParmeters: {
                                Name: "",
                                FactoryCode: self.typeFactory.value.ResourceCode,
                                QueryFilter1: "CHPN"
                            },
                            pagination: {},
                            multiple: false,
                            sidx: "MaterialCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'MaterialCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_6'),
                                    width: 110
                                },
                                {
                                    field: 'MaterialName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_7'),
                                    width: 120
                                },
                                {
                                    field: 'Spec',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_8'),
                                    width: 120
                                },
                                {
                                    field: 'MaterialClassName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_9'),
                                    width: 120
                                },
                                {
                                    field: 'ProcessRouteName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_10'),
                                    width: 120
                                },
                                // {
                                //     field: 'IsUsed',
                                //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_11'),
                                //     width: 120
                                // },
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                console.log(data);
                // self.currentItem.MaterialCode = data[0].MaterialCode;
                // self.currentItem.MaterialName = data[0].MaterialName;
                // self.currentItem.Spec = data[0].Spec;
                // self.currentItem.MaterialClass = data[0].MaterialClass;
                // self.currentItem.MaterialClassName = data[0].MaterialClassName;
                //self.currentItem.MaterialClassName = res.data.resultData[0].MaterialClassName;
                //self.currentItem.MaterialClass = res.data.resultData[0].MaterialClass;
                self.currentItem.MaterialCode = data[0].MaterialCode;
                self.currentItem.MaterialName = data[0].MaterialName;
                self.currentItem.Spec = data[0].Spec;
                self.MaterialClass.value = self.MaterialClass.options.find(t => t.ItemValue == data[0].MaterialClass);
                self.SmallClass.value = self.SmallClass.options.find(t => t.ItemValue == data[0].SmallClass);
                self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == data[0].Unit);

                if (data[0].IsUsed == false)  //是否启用批次管理
                {
                    self.isButtonVisible = false;
                    self.isButtonVisible1 = false;
                }
                else {
                    self.isButtonVisible = true;
                    self.isButtonVisible1 = true;
                }
            });
        }
        // function materialChange(oldval, newval) {
        //     var url = commonService.getMesApiAddress("material") + 'Base_Material/GetDataTable_TestOtherEntity?checkType=' + newval;
        //     commonService.callWebApiGet(url, null).then(function (res) {
        //         if (res && res.data.success && res.data.resultData.length > 0) {
        //             var item = res.data.resultData[0];
        //             //self.currentItem.MaterialClassName = res.data.resultData[0].MaterialClassName;
        //             //self.currentItem.MaterialClass = res.data.resultData[0].MaterialClass;
        //             self.currentItem.MaterialName = item.MaterialName;
        //             self.currentItem.Spec = item.Spec;
        //             self.MaterialClass.value = self.MaterialClass.options.find(t => t.ItemValue == item.MaterialClass);
        //             self.SmallClass.value = self.SmallClass.options.find(t => t.ItemValue == item.SmallClass);
        //             self.typeUnit.value = self.typeUnit.options.find(t => t.ItemValue == item.Unit);
        //             if (item.IsEnabled == "0")  //是否启用批次管理
        //             {
        //                 self.isButtonVisible = false;
        //                 self.isButtonVisible1 = false;

        //             }


        //         } else {
        //             self.currentItem.MaterialName = "";
        //             self.currentItem.Spec = "";
        //             self.MaterialClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" };
        //             self.SmallClass.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" };
        //             self.typeUnit.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_2'), ItemValue: "" };
        //         }
        //     });
        // }
        //选择供应商  使用公用方法
        function SelectEquipmentModal() {
            // debugger
            //alert('SelectEP_EquipmentModal');
            console.log(commonService);
            //grid显示字段列表
            let columnDefs = [
                {
                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                },
                // {
                //     field: 'EquipmentId',
                //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_12'),
                //     width: 200
                // },
                {
                    field: 'Abbr',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_13'),
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
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_14'), commonService.getMesApiAddress("material") + "Base_SupplierManage/Base_SupplierManagePageList", [{ 'FieldCode': 'Abbr', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_13'), 'FiledType': 'Text' }], "SupplierCode", "asc", columnDefs, Select_SingleChoiceModalEquipment_callback);
        }

        //选择弹窗回调方法  返回 选择实体
        function Select_SingleChoiceModalEquipment_callback(res) {
            //alert(JSON.stringify(res));
            // debugger
            self.currentItem.Abbr = res.Abbr;
            self.currentItem.Supplier = res.SupplierCode;
        }

        function selectClick1() {
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_15'));
                return;
            }
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("factory") + 'level/GetWarehouseByFactoryExtendInfo',
                            queryParmeters: {
                                factoryCode: self.typeFactory.value.ResourceCode,
                                fieldCode: "CKLX",
                                fieldValue: "1"
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_16'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_17'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_18'));
                } else {
                    // debugger
                    self.currentItem.TargetWhsCode = data[0].ResourceCode;
                    self.currentItem.TargetWhsName = data[0].ResourceName;
                }
            });

        }
        function selectClick2() {
            if (!self.currentItem.TargetWhsCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_19'), commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_20'));
                return;
            }
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("factory") + 'level/GetListByParentResource',
                            queryParmeters: {
                                Name: "",
                                ParentResource: self.currentItem.TargetWhsCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ResourceCode',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_21'),
                                    width: 200
                                },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_22'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_18'));
                } else {
                    self.currentItem.TargetLocationCode = data[0].ResourceCode;
                    self.currentItem.TargetLocationName = data[0].ResourceName;
                    // debugger
                    //获取管理方式
                    var postData = {
                        KeyValue: self.currentItem.TargetLocationCode

                    };

                    //   var url = commonService.getMesApiAddress("LevelManage/BsModelResourceExtendInfo") + 'GetManageMode';

                    var url = commonService.getMesApiAddress("factory") + 'LevelManage/BsModelResourceExtendInfo/GetManageMode';
                    commonService.callWebApiPost(url, postData).then(function (res) {
                        if ((res) && (res.data.success) && res.data.resultData) {

                            var a = res.data.resultData.FieldValue;
                            if (a == "1") {
                                self.currentItem.ManageMode = commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_23');
                            }
                            else {

                                self.currentItem.ManageMode = commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_24');
                            }


                        } else {

                        }
                    });


                }
            });
        }
        //编辑保存
        function save() {
            // debugger

            if (self.currentItem.MaterialName == null) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_25'));
                return;
            }
            if (self.currentItem.TargetLocationName == null) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_26'));
                return;
            }
            if (self.isButtonVisible == true && !self.BatchDate) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_27'));
                return;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: "",
                Entity: self.currentItem,
                FactoryCode: self.typeFactory.value.ResourceCode,
                FactoryName: self.typeFactory.value.ResourceName,
                MaterialCode: self.currentItem.MaterialCode,
                MaterialName: self.currentItem.MaterialName,
                Spec: self.currentItem.Spec,
                SmallClass: self.SmallClass.value.ItemValue,
                BatchNo: self.currentItem.BatchNo,
                BatchDate: self.BatchDate,
                Qty: self.currentItem.TargetQty,
                Unit: self.typeUnit.value.ItemName,
                SupplierCode: self.currentItem.Supplier,
                WhsCode: self.currentItem.TargetWhsCode,
                LocationCode: self.currentItem.TargetLocationCode
            };

            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialStock/SaveData';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_28') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_29'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_20'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_20'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_RawMaterialStock_RawMaterialStockManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/RawMaterialStock';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/RawMaterialStockManage-addDetail.html',
                    controller: AddDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageaddDetailctrl.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
