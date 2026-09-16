(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MMSaleDomestic').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomestic.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_1'));
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
            self.OldData = [];
            self.validInputs = false;
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.IsShowButten = false;
            self.IsShowDeleteButten = false;
            self.searchParams = {};
            self.index = 0;



            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            self.addForm = addForm;
            self.deleteForm = deleteForm;
            self.typeFactoryChange = typeFactoryChange;
            self.WarehouseChange = WarehouseChange;

            initDictionary();
        }
        function initDictionary() {
            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" }]
            };

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2')
                    });
                    initGridData1();
                }
            });
            //  //仓库
            //  commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKLX", FieldValue: "3" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2')
            //         });
            //     }
            // });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                let query = {
                    factoryCode: newItem.ResourceCode,
                    fieldCode: "CKLX",
                    fieldValue: "3"
                };
                commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                    if (res && res.data.success) {
                        self.typeWarehouse.options = res.data.resultData;
                        self.typeWarehouse.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2')
                        });
                    }
                });
            } else {
                self.typeWarehouse = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2'), ResourceCode: "" }]
                };
            }
        }

        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_2')
                    });
                }
            });
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
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
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_4'),
                        width: 130
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_5'),
                        width: 130
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_6'),
                        width: 130
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'PieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_8'),
                        width: 130
                    },
                    {
                        field: 'BoxQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_9'),
                        width: 130
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_10'))
                return;
            }

            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'CreateTime',//订单号、柜号
                sord: 'desc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.Attr = "0";
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_SaleDomestic/MM_SaleDomesticPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    //总条数
                    self.gridOptionsItem1.totalItems = res.data.resultData.records;
                    self.gridOptionsItem1.data = res.data.resultData.rows;
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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_11'), commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_12'));
                return false;
            }
            var data = self.gridOptionsItem2.data;
            var ent = data.find(t => t.ProductOrder == self.selectedItem1.ProductOrder
                && t.MaterialCode == self.selectedItem1.MaterialCode
            );
            if (ent != null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_13'), commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_12'));
                return false;
            }
            //if (!self.DeliveryType.value.ItemValue) {
            //    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_14'));
            //    return;
            //}

            var pieceQty = Math.round(self.selectedItem1.PieceQty / self.selectedItem1.BoxQty * self.currentItem.BoxQty)

            self.index = self.index + 1;
            data.push({
                index: self.index,
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                ProductOrder: self.selectedItem1.ProductOrder,
                MaterialCode: self.selectedItem1.MaterialCode,
                WhsName: self.typeWarehouse.value.ResourceName,
                WhsCode: self.typeWarehouse.value.ResourceCode,
                LocationName: self.typeLocation.value.ResourceName,
                LocationCode: self.typeLocation.value.ResourceCode,
                PieceQty: pieceQty,
                BoxQty: self.currentItem.BoxQty,
                Remark: self.currentItem.Remark,
                IsEnabled: true
            });

            self.OldData.push({
                Id: self.selectedItem1.Id,
                PieceQty: self.selectedItem1.PieceQty - pieceQty,
                BoxQty: self.selectedItem1.BoxQty - self.currentItem.BoxQty,
                IsEnabled: true
            })

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
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'WhsName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'PieceQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'BoxQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_15'),
                        width: 160
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

            //字典类型 取值参考
            let data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_16'), commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_12'));
                return false;
            }


            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                OldData: self.OldData,
                Entity: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_17') });
            var url = commonService.getMesApiAddress("material") + 'MM_SaleDomestic/SaveBatchMM_SaleDomestic';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            self.OldData = [];
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_18'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_12'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_12'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form1") {
                self.validInputs = params.validity;
            }
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MMSaleDomestic_SaleDomestic';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MMSaleDomestic';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/SaleDomestic-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MMSaleDomestic.SaleDomesticaddctrl.Tips_19'
            }
        };
        $stateProvider.state(state);
    }
}());
