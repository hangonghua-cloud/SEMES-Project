(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMReceiptNotice').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNotice.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.index = 0;

            initDictionary();

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
            self.manufacturerModal = manufacturerModal;//制造厂家
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


        function initDictionary() {

            self.typeSupplier = {
                value: { SupplierName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), SupplierCode: "" },
                options: [{ SupplierName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), SupplierCode: "" }]
            };
            var url = commonService.getMesApiAddress("material") + "Base_SupplierManage/GetBase_SupplierManageList?checkType=";
            commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeSupplier.options = res.data.resultData;
                    self.typeSupplier.options.splice(0, 0, {
                        SupplierCode: "",
                        SupplierName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2')
                    });
                }
            });
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2')
                    });
                    initGridData1();
                }
            });

            //制造厂家
            self.typeManufacturer = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("10").then(function (res) {
                if (res && res.data.success) {
                    self.typeManufacturer.options = res.data.resultData;
                    self.typeManufacturer.value = { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_2'), ItemValue: "" };
                }
            })
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
                                    name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_3'), minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'ItemValue',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_4'),
                                    width: 200
                                },
                                {
                                    field: 'ItemName',
                                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_5'),
                                    width: 350
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_6'));
                } else {
                    self.currentItem.ManufacturerCode = data[0].ItemValue;
                    self.currentItem.ManufacturerName = data[0].ItemName;
                }
            });
        }

        //选择供应商
        function supplierClick(levelCode) {

            let parentSupplierCodes = "";
            if (levelCode == '2') {
                parentSupplierCodes = self.selectedItem1.Supplier;
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

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 100, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'PurchaseOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_12'),
                        width: 80
                    },
                    {
                        field: 'FirstQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_13'),
                        width: 140
                    },
                    {
                        field: 'PurchaseNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_14'),
                        width: 120
                    },
                    {
                        field: 'NoArrivalQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_15'),
                        width: 120
                    },
                    // {
                    //     field: 'Amount',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_16'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'ArrivalStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_17'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'1\'"><span ng-cell-text>未到货</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'2\'"><span ng-cell-text>部分到货</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'3\'"><span ng-cell-text>已到货</span></div>'
                    // },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_18'),
                        width: 250
                    },
                    {
                        field: 'ContractNo',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_19'),
                        width: 120
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData1();
                    });
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            //setButtonsVisibility(true);
                            //GetWorkOrderBomUnitConsome();
                            self.IsShowButten = true;
                        } else {
                            self.selectedItem1 = null;
                            self.IsShowButten = false;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData1() {

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_20'));
                return;
            }

            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//订单号、柜号
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            // self.searchParams.ProcureType = "DDKC";//采购类型
            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalDate(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalDate(self.EndTime);
            } else {
                self.searchParams.StartTime = ""
                self.searchParams.EndTime = ""
            }

            var postData = {
                // pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("plan") + "PL_PurchaseOrder/GetPageDataTableListByReceiptNotice";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                    //总条数(分页)
                    // self.gridOptionsItem1.totalItems = res.data.resultData.records;
                    // self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }
        function search() {
            initGridData1();
        }

        function addForm() {

            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_21'), commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_22'));
                return false;
            }

            var data = self.gridOptionsItem2.data;
            var ent = data.find(t => t.MaterialCode == self.selectedItem1.MaterialCode && t.PurchaseOrder == self.selectedItem1.PurchaseOrder);
            if (ent != null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_23'), commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_22'));
                return false;
            }

            self.currentItem.ArrivalTime = commonService.ConvertToLocalDate(self.ArrivalTime);
            self.index = self.index + 1;
            data.push({
                index: self.index,
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                ArrivalQty: self.currentItem.ArrivalQty,
                ArrivalTime: self.currentItem.ArrivalTime,
                MaterialCode: self.selectedItem1.MaterialCode,
                MaterialName: self.selectedItem1.MaterialName,
                Spec: self.selectedItem1.Spec,
                SmallClass: self.selectedItem1.SmallClass,
                MaterialClass: self.selectedItem1.MaterialClass,
                Unit: self.selectedItem1.Unit,
                PurchaseNum: self.selectedItem1.PurchaseNum,
                ArrivalStatus: self.selectedItem1.ArrivalStatus,
                PurchaseOrder: self.selectedItem1.PurchaseOrder,
                ProductOrder: self.selectedItem1.ProductOrder,
                SupplierCode: self.selectedItem1.Supplier,
                SupplierName: self.selectedItem1.SupplierName,
                PurchaseId: self.selectedItem1.Id,
                ReceiptStatus: "1",
                ManufacturerCode: self.currentItem.ManufacturerCode,
                ManufacturerName: self.currentItem.ManufacturerName,
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
                Remark: self.currentItem.Remark
            });
            self.gridOptionsItem2.data = data;
        }

        function initGridOptions2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'ArrivalQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_24'),
                        width: 100
                    },
                    {
                        field: 'ArrivalTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_25'),
                        width: 120,
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_26'),
                        width: 150
                    },
                    {
                        field: 'SmallClass',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_27'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_12'),
                        width: 100
                    },
                    {
                        field: 'PurchaseNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_14'),
                        width: 120
                    },
                    // {
                    //     field: 'Amount',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_16'),
                    //     width: 120
                    // },
                    // {
                    //     field: 'ArrivalStatus',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_17'),
                    //     width: 110,
                    //     cellTemplate:
                    //         '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'1\'"><span ng-cell-text>未到货</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'2\'"><span ng-cell-text>部分到货</span></div>' +
                    //         '<div class="ngCellText" ng-if="row.entity.ArrivalStatus==\'3\'"><span ng-cell-text>已到货</span></div>'
                    // },

                    {
                        field: 'PurchaseOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_9'),
                        width: 120
                    },
                    //  {
                    //     field: 'ManufacturerName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_28'),
                    //     width: 120
                    // },
                    {
                        field: 'SupplierName2',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_38'),
                        width: 120
                    },
                    {
                        field: 'SupplierName3',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_39'),
                        width: 120
                    },
                    {
                        field: 'SupplierName4',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_40'),
                        width: 120
                    },
                    {
                        field: 'SupplierName5',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_41'),
                        width: 120
                    },
                    {
                        field: 'SupplierName6',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_42'),
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_29'),
                        width: 120
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                            self.IsShowDeleteButten = true;
                            //setButtonsVisibility(true);

                        } else {
                            self.selectedItem2 = null;
                            self.IsShowDeleteButten = false;
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi2.core.handleWindowResize();
                        $scope.gridApi2.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }


        function deleteForm() {
            self.gridOptionsItem2.data = _.filter(self.gridOptionsItem2.data, function (item) {
                return item.index != self.selectedItem2.index;
            })
        }

        function save() {

            var data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_30'), commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_22'));
                return false;
            }

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: "",
                data: data
            };
            console.log("jpf1234" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("material") + 'MM_ReceiptNotice/SaveBatchMM_ReceiptNotice';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_31') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function saveFiles(uId) {
            debugger;
            var url = commonService.getfileUrl() + '/UploadFile/Save';
            var postData = {
                parentId: uId,
                tableName: 'MM_ReceiptNotice',
                module: "物料模块",
                factoryCode: self.typeFactory.value.ResourceCode,
                factoryName: self.typeFactory.value.ResourceName,
                files: self.fileList,
            }
            commonService.callWebApiPost(url, postData).then((res) => {
                console.log(res)
            })
        }

        //保存成功事件
        function onSaveSuccess(data) {

            if (data.data.success) {
                busyIndicatorService.hide();
                saveFiles(data.data.resultData[0] ?.ReceiptCode);
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_32'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_22'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_22'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MMReceiptNotice_ReceiptNotice';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMReceiptNotice';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ReceiptNotice-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMReceiptNotice.ReceiptNoticeaddctrl.Tips_33'
            }
        };
        $stateProvider.state(state);
    }
}());
