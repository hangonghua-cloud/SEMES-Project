(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductDispatch').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatch.service', '$state', '$stateParams',
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

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_1'));
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
        }

        function initDictionary() {
            //产品状态
            self.AvoidProduce = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_2'), ItemValue: "" },
                { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_3'), ItemValue: "0" },
                { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_4'), ItemValue: "1" }]
            }
            //发货类型
            self.DeliveryType = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_5'), ItemValue: "1" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_5'), ItemValue: "1" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_6'), ItemValue: "2" }]
            }
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_2'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_2')
                    });
                    initGridData1();
                }
            });
        }

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 50, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: true,
                enableRowSelection: false,
                enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_7'),
                        width: 100
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_8'),
                        width: 150
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_9'),
                        width: 150
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'AvoidProduce',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_11'),
                        width: 150,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce==true"><span ng-cell-text >免产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce!=true"><span ng-cell-text >正常</span></div>'
                    },
                    {
                        field: 'DeliveryPallet',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_12'),
                        width: 150
                    },
                    {
                        field: 'DeliveryPieces',
                        displayName: '发货总片数',
                        width: 150
                    }
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
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_13'))
                return;
            }

            let Pagination = {
                rows: self.gridOptionsItem1.paginationPageSize,
                page: self.gridOptionsItem1.paginationCurrentPage,
                sidx: 'ProductOrder desc,CAST(ContainerNo as int) ',//订单号、柜号
                sord: 'asc'
            };

            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            var postData = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchBill/GetWorkOrderDataTable';
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

            // var data1 = $scope.gridApi.selection.getSelectedRows();
            // if (data1.length < 1) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_14'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_15'));
            //     return false;
            // }
            // var data2 = self.gridOptionsItem2.data;
            // var msg = "";

            // data1.forEach((item) => {
            //     if (data2.length > 0 && data2.find(t => t.ProductOrder == item.ProductOrder && t.ContainerNO == item.ContainerNO)) {
            //         msg += item.ContainerNO + ";";
            //     }
            // })

            // if (!!msg) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_16') + msg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_15'));
            //     return false;
            // }
            //if (!self.DeliveryType.value.ItemValue) {
            //    commonService.showWarning(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_17'));
            //    return;
            //}

            // var deliveryDate = commonService.ConvertToLocalDate(self.DeliveryDate)

            // data1.forEach(item => {

            //     data2.push({
            //         WoId: item.WoId,
            //         FactoryCode: item.FactoryCode,
            //         FactoryName: item.FactoryName,
            //         ProductOrder: item.ProductOrder,
            //         WorkOrder: item.WorkOrder,
            //         ContainerNO: item.ContainerNO,
            //         CustomerPO: item.CustomerPO,
            //         AvoidProduce: item.AvoidProduce,
            //         DeliveryType: self.DeliveryType.value.ItemValue,
            //         DeliveryDate: deliveryDate,
            //         PalletQty: item.DeliveryPallet,
            //         PieceQty: item.DeliveryPieces,
            //         BoxNum: item.DeliveryBox,
            //         GrossWeight: item.GrossWeight * item.DeliveryPallet,
            //         Volume: self.currentItem.Volume,
            //         LoadingBill: self.currentItem.LoadingBill,
            //         InvoiceNO: self.currentItem.InvoiceNO,
            //         Remark: self.currentItem.Remark
            //     });
            // })
            // debugger;

            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_14'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_15'));
                return false;
            }
            var data2 = self.gridOptionsItem2.data;
            if (data2.find(t => t.WorkOrder == self.selectedItem1.WorkOrder)) {
                commonService.showWarning("数据重复！");
                return;
            }
            var deliveryDate = commonService.ConvertToLocalDate(self.DeliveryDate);

            data2.push({
                WoId: self.selectedItem1.WoId,
                FactoryCode: self.selectedItem1.FactoryCode,
                FactoryName: self.selectedItem1.FactoryName,
                ProductOrder: self.selectedItem1.ProductOrder,
                WorkOrder: self.selectedItem1.WorkOrder,
                ContainerNO: self.selectedItem1.ContainerNO,
                CustomerPO: self.selectedItem1.CustomerPO,
                AvoidProduce: self.selectedItem1.AvoidProduce,
                DeliveryType: self.DeliveryType.value.ItemValue,
                DeliveryDate: deliveryDate,
                PalletQty: self.selectedItem1.DeliveryPallet,
                Qty: self.selectedItem1.DeliveryPieces,
                BoxNum: self.selectedItem1.DeliveryBox,
                GrossWeight: self.selectedItem1.GrossWeight * self.selectedItem1.DeliveryPallet,
                Volume: self.currentItem.Volume,
                LoadingBill: self.currentItem.LoadingBill,
                InvoiceNO: self.currentItem.InvoiceNO,
                Remark: self.currentItem.Remark
            });

            self.gridOptionsItem2.data = data2;
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
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_9'),
                        width: 80
                    },
                    {
                        field: 'CustomerPO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_10'),
                        width: 120
                    },
                    {
                        field: 'AvoidProduce',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_11'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce==true"><span ng-cell-text >免产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.AvoidProduce!=true"><span ng-cell-text >正常</span></div>'
                    },
                    {
                        field: 'PalletQty',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_12'),
                        width: 130
                    },
                    {
                        field: 'Qty',
                        displayName: '片数',
                        width: 130
                    },
                    {
                        field: 'BoxNum',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_18'),
                        width: 110
                    },
                    {
                        field: 'GrossWeight',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_19'),
                        width: 110
                    },
                    {
                        field: 'Volume',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_20'),
                        width: 110
                    },
                    {
                        field: 'DeliveryType',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_21'),
                        width: 130,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.DeliveryType==\'1\'"><span ng-cell-text >装柜发货</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.DeliveryType==\'2\'"><span ng-cell-text >非装柜发货</span></div>'
                    },
                    {
                        field: 'DeliveryDate',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_22'),
                        width: 130
                    },
                    {
                        field: 'InvoiceNO',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_23'),
                        width: 130
                    },
                    {
                        field: 'LoadingBill',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_24'),
                        width: 130
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_25'),
                        width: 130
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
                return item.WoId != self.selectedItem2.WoId;
            })
        }

        function save() {

            //字典类型 取值参考
            let data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_26'), commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_15'));
                return false;
            }


            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_27') });
            var url = commonService.getMesApiAddress("material") + 'MM_ProductDispatchBill/SaveDispatchBillItem';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //取消
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_28'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_15'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_15'));
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
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductDispatch_ProductDispatch';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductDispatch';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductDispatch-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductDispatch.ProductDispatchaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
