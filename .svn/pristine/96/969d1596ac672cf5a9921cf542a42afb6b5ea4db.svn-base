(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.PurchaseManage').config(InScreenStateConfig);

    InScreenController.$inject = ['Siemens.SimaticIT.PlanApp.PurchaseManage.PurchaseOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function InScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_1'));
            // sidePanelManager.open('e');
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.currentItem.Remark = "";
            self.currentItem.BatchCount = 1; //批次数量默认为1
            self.currentItem.ManufacturerCode = ""; //厂家编码
            self.currentItem.ManufacturerName = ""; //厂家名称
            self.currentItem.SupplierCode2 = "";
            self.currentItem.SupplierName2 = "";
            self.currentItem.SupplierCode3 = "";
            self.currentItem.SupplierName3 = "";
            self.currentItem.SupplierCode4 = "";
            self.currentItem.SupplierName4 = "";
            self.currentItem.SupplierCode5 = "";
            self.currentItem.SupplierName5 = "";
            self.currentItem.SupplierCode6 = "";
            self.currentItem.SupplierName6 = "";
            self.validInputs = false;
            self.isVisible = self.currentItem.IsUsed;
            self.PostDate = new Date();

            initdictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.WarehouseChange = WarehouseChange;
            self.IsMergeBatchChange = IsMergeBatchChange;
            self.MergeBatchChange = MergeBatchChange;
            self.BatchDateChange = BatchDateChange;

            self.selectClick1 = selectClick1;
            self.selectClick2 = selectClick2;
            self.manufacturerModal = manufacturerModal;//制造厂家模态框
            self.supplierClick = supplierClick;

            //附件
            self.fileList = [];
            self.uploader = commonService.InitUploader({}, (res) => {
                self.fileList.push({
                    path: res[0].path,
                    size: res[0].size,
                    name: res[0].path.substr(res[0].path.lastIndexOf('/') + 1)
                })
            });
            self.uploader.onProgressAll = function (progress) {
                var _content = document.getElementById("content");
                var progressWidth = progress / 100 * 300;
                //设置content的宽度（动态变化）
                _content.style.width = `${progressWidth}px`;
                _content.innerText = progress + "%";
                console.info('onProgressAll', progress);
            };
            self.doUpload = () => {
                var _content = document.getElementById("content");
                //设置content的宽度（动态变化）
                _content.style.width = `${0}px`;
                _content.innerText = "";
                $('#upload')[0].click()
            }
            self.deleteFile = (path) => {
                console.log(path)
                self.fileList = self.fileList.filter(m => m.path != path)
            }
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initdictionary() {

            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ResourceCode: "" }]
            };
            self.Location = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ResourceCode: "" }]
            };

            //是否合批
            self.IsMergeBatch = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_3'), ItemValue: "1" },
                { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_4'), ItemValue: "0" }]
            };
            //启用批次管理，默认为否
            if (self.currentItem.IsUsed) {
                self.IsMergeBatch.value = { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_4'), ItemValue: "0" };
            }

            //合并批次
            self.MergeBatch = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
            };
            self.currentItem.WhsCode = self.currentItem.Warehouse;
            self.currentItem.WhsName = self.currentItem.ResourceName;

            // self.typeWarehouse = {
            //     value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ResourceCode: "" },
            //     options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ResourceCode: "" }]
            // };

            if (self.currentItem.SmallClass == "BC") {
                self.IsMergeBatch.value.ItemValue = "1";
                self.IsMergeBatch.value.ItemName = commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_3');

                if (self.IsMergeBatch.value.ItemValue == "1") {
                    let queryParmeters = {
                        materialCode: self.currentItem.MaterialCode,
                        supplierCode: self.currentItem.Supplier,
                        factoryCode: self.currentItem.FactoryCode
                    };
                    var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/GetMaterialStockBatch';
                    commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                        if ((res) && (res.data.success) && res.data.resultData) {
                            //合并批次
                            self.MergeBatch = {
                                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
                            };
                            res.data.resultData.forEach(item => {
                                self.MergeBatch.options.push({
                                    ItemValue: item,
                                    ItemName: item
                                });
                            });
                        } else {
                            self.MergeBatch = {
                                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
                            };
                        }
                    });
                }
                else {
                    self.MergeBatch = {
                        value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                        options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
                    };
                }

            }


            //仓库         
            commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2')
                    });
                    self.typeWarehouse.value = self.typeWarehouse.options.find(t => t.ResourceCode == self.currentItem.Warehouse);
                }
            });

            //单位
            self.typeUnit = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("Unit").then(function (res) {
                if (res && res.data.success) {
                    self.typeUnit.options = res.data.resultData;
                    self.typeUnit.value = res.data.resultData.find(t => t.ItemValue == self.currentItem.Unit);
                }
            })

        }
        //选择仓库
        function selectClick1() {
            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("factory") + 'level/GetWarehouseByFactory',
                            queryParmeters: {
                                Name: "",
                                factoryCode: self.currentItem.FactoryCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                // {
                                //     field: 'ResourceCode',
                                //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_6'),
                                //     width: 200
                                // },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_7'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_8'));
                } else {
                    self.currentItem.WhsCode = data[0].ResourceCode;
                    self.currentItem.WhsName = data[0].ResourceName;
                }
            });
        }
        //选择库位
        function selectClick2() {
            if (!self.currentItem.WhsCode) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_9'), commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_10'));
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
                                ParentResource: self.currentItem.WhsCode
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Post",
                            sidx: "ResourceCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                // {
                                //     field: 'ResourceCode',
                                //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_6'),
                                //     width: 200
                                // },
                                {
                                    field: 'ResourceName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_11'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_8'));
                } else {
                    self.currentItem.LocationCode = data[0].ResourceCode;
                    self.currentItem.LocationName = data[0].ResourceName;
                }
            });
        }

        //选择制造厂家
        function manufacturerModal() {

            var modalInstance = commonService.openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: commonService.getMesApiAddress("") + "SystemManage/DataItemDetail/GetDataItemListJson_UA?EnCode=10&modal=1",
                            queryParmeters: {
                                Name: ""
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Get",
                            sidx: "ItemValue",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_5'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ItemValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_12'),
                                    width: 200
                                },
                                {
                                    field: 'ItemName',
                                    displayName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_13'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_8'));
                } else {
                    self.currentItem.ManufacturerCode = data[0].ItemValue;
                    self.currentItem.ManufacturerName = data[0].ItemName;
                }
            });
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

        //仓库改变事件
        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.Location.options = res.data.resultData;
                    self.Location.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2')
                    });
                }
            });
        }
        //是否合批change事件
        function IsMergeBatchChange(oldItem, newItem) {
            // debugger;
            if (newItem.ItemValue == "1") {
                let queryParmeters = {
                    materialCode: self.currentItem.MaterialCode,
                    supplierCode: self.currentItem.Supplier,
                    factoryCode: self.currentItem.FactoryCode
                };
                var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/GetMaterialStockBatch';
                commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                    if ((res) && (res.data.success) && res.data.resultData) {
                        res.data.resultData.forEach(item => {
                            self.MergeBatch.options.push({
                                ItemValue: item,
                                ItemName: item
                            });
                        });
                    } else {
                        self.MergeBatch = {
                            value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                            options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
                        };
                    }
                });
            }
            else {
                self.MergeBatch = {
                    value: { ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" },
                    options: [{ ItemName: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_2'), ItemValue: "" }]
                };
            }
        }
        //合并批次改变事件
        function MergeBatchChange(oldItem, newItem) {
            // debugger;
            if (self.IsMergeBatch.value.ItemValue == "1") {
                self.currentItem.BatchNo = newItem.ItemValue;
            } else {
                self.currentItem.BatchNo = "";
            }
        }
        //批次日期改变事件
        function BatchDateChange(oldItem, newItem) {
            // debugger;
            if (self.IsMergeBatch.value.ItemValue == "0") { //不合批
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
        }


        function save() {

            // if (self.currentItem.Qty > self.currentItem.NoArrivalQty) {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_14'));
            //     return;
            // }

            self.currentItem.BusinessTable = "PL_PurchaseOrder";
            self.currentItem.SupplierCode = self.currentItem.Supplier;
            if (!!self.BatchDate) {
                self.currentItem.BatchDate = commonService.ConvertToLocalDate(self.BatchDate);
            }
            if (!self.PostDate) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.RawMaterialStock.RawMaterialStockManageoutctrl.Tips_36'));
                return;
            }

            self.currentItem.Unit = self.typeUnit.value.ItemValue;
            self.currentItem.UnitName = self.typeUnit.value.ItemName;
            self.currentItem.PostDate = commonService.ConvertToLocalDate(self.PostDate);

            var postData = {
                KeyValue: '',
                Entity: self.currentItem,
                ArrivalQty: self.currentItem.ArrivalQty ? self.currentItem.ArrivalQty : 0,
                PurchaseId: self.currentItem.Id,//采购订单Id
                // manufacturerCode: self.currentItem.ManufacturerCode,
                // manufacturerName: self.currentItem.ManufacturerName,
                SupplierCode2: self.currentItem.SupplierCode2,
                SupplierName2: self.currentItem.SupplierName2,
                SupplierCode3: self.currentItem.SupplierCode3,
                SupplierName3: self.currentItem.SupplierName3,
                SupplierCode4: self.currentItem.SupplierCode4,
                SupplierName4: self.currentItem.SupplierName4,
                SupplierCode5: self.currentItem.SupplierCode5,
                SupplierName5: self.currentItem.SupplierName5,
                SupplierCode6: self.currentItem.SupplierCode6,
                SupplierName6: self.currentItem.SupplierName6,
                batchCount: self.currentItem.BatchCount,
                isHePi: self.IsMergeBatch.value.ItemValue
            };
            var url = commonService.getMesApiAddress("material") + 'MM_RawMaterialIn/SaveMM_RawMaterialIn';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_15') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function saveFiles(uId) {
            // debugger;
            var url = commonService.getfileUrl() + '/UploadFile/Save';
            var postData = {
                parentId: uId,
                tableName: 'MM_ReceiptNotice',
                module: commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_16'),
                factoryCode: self.currentItem.FactoryCode,
                factoryName: self.currentItem.FactoryName,
                files: self.fileList,
            }
            commonService.callWebApiPost(url, postData).then((res) => {
                console.log(res)
            })
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
                //data.data.resultData && saveFiles(data.data.resultData[0]?.Id)；
                data.data.resultData && saveFiles(data.data.resultData[0]?.ReceiptCode);
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_17'));
                //刷新局部
                $rootScope.$emit('to-parent', self.currentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_10'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_10'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    InScreenStateConfig.$inject = ['$stateProvider'];
    function InScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_PurchaseManage_PurchaseOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/PurchaseManage';

        var state = {
            name: screenStateName + '.in',
            url: '/in/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PurchaseOrder-in.html',
                    controller: InScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.PurchaseManage.inJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
