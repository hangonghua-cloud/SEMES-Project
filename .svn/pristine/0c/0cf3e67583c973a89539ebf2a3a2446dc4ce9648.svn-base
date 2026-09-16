(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.WorkOrderWearingLayer.service', '$state', '$stateParams',
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

            initGridData2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.FactoryCode = self.currentItem.data[0].FactoryCode;
            self.FactoryName = self.currentItem.data[0].FactoryName;
            self.validInputs = false;
            self.validInputs1 = false;
            self.isReadOnly = false;
            self.materialRows = [];//物料扣除
            self.list = [];//耐磨层发料

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.edit = edit;
            initDictionary();
            setTimeout(function () {
                //初始化grid数据、查询
                LoadFactory();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000
            self.editSuper = editSuper;//编辑超产品
            self.resetSuper = resetSuper;//超产品重置
            self.saveSuper = saveSuper;//保存超产品
            self.search = search;//搜索
            self.WarehouseChange = WarehouseChange;
            self.WhsNameChange = WhsNameChange;
        }

        function initDictionary() {

            //工厂
            self.WhsName = {
                value: null,
                options: []
            };

            self.typeWarehouse = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeLocation = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2'), ResourceCode: "" }]
            };
            // commonService.get_ResourceExtendByLevelField({ LevelCode: "Warehouse", FieldCode: "CKSX", FieldValue: "2" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWarehouse.options = res.data.resultData;
            //         self.typeWarehouse.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2')
            //         });
            //     }
            // });
            let query = {
                factoryCode: self.FactoryCode,
                fieldCode: "CKSX",
                fieldValue: "2"
            };
            commonService.getWarehouseByFactoryExtendInfo(query).then(function (res) {
                if (res && res.data.success) {
                    self.typeWarehouse.options = res.data.resultData;
                    self.typeWarehouse.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2')
                    });
                }
            });
        }

        function LoadFactory() {
            var url = commonService.getMesApiAddress('factory') + "level/GetWhsNameByFactory";
            var post = { factoryCode: self.FactoryCode }
            commonService.callWebApiPost(url, post).then(function (data) {
                if ((data) && (data.data.success)) {
                    self.WhsName.options = data.data.resultData;;
                }
            });
        }
        function WarehouseChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeLocation.options = res.data.resultData;
                    self.typeLocation.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_2')
                    });
                }
            });

        }
        function WhsNameChange(oldVal, newVal) {
            self.currentItem.WhsCode = "";
            self.currentItem.FactoryName = "";
            newVal.forEach(x => {
                self.currentItem.WhsCode += x.ResourceCode + ",";
                self.currentItem.FactoryName += x.ResourceName + ",";
            });
        };

        function initGridOptions1() {
            self.gridOptionsItem1 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: true,
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
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_3'),
                        width: 130
                    },
                    {
                        field: 'ExeWorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_4'),
                        width: 200
                    },

                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_5'),
                        width: 70
                    },
                    {
                        field: 'SheetsQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_7'),
                        width: 130
                    },
                    {
                        field: 'ShouldNum1',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_8'),
                        width: 110,
                    },
                    // {
                    //     field: 'ActualNum1',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_9'),
                    //     width: 110,
                    // },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_10'),
                        width: 60,

                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_11'),
                        width: 160
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            //setButtonsVisibility(true);

                        } else {
                            self.selectedItem1 = null;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: self.currentItem.data
            };
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
                        field: 'SuperNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_12'),
                        width: 100
                    },
                    {
                        field: 'Qty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_13'),
                        width: 100
                    },
                    {
                        field: 'ActNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_14'),
                        width: 100,

                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_15'),
                        width: 120
                    },

                    {
                        field: 'LocationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_18'),
                        width: 120
                    },

                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_19'),
                        width: 120
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_10'),
                        width: 80
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                            //setButtonsVisibility(true);
                            //self.currentItem.DeductionNum = angular.copy(self.selectedItem2.LockNum);
                        } else {
                            self.selectedItem2 = null;
                            //setButtonsVisibility(false);
                            self.currentItem.DeductionNum = 0;
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

        function initGridData2() {

            var postdata = {
                queryJson: {
                    FactoryCode: self.FactoryCode,
                    MaterialCode: self.currentItem.data[0].WearingLayerCode,
                    IsFrozen: "0",
                    QtyStr: ">0",
                    WhsCode: self.currentItem.WhsCode,
                    ResourceName: self.currentItem.LocationName
                }
            }
            console.log("jpf1234" + JSON.stringify(postdata));
            var url = commonService.getMesApiAddress("material") + "MM_RawMaterialStock/NewMM_RawMaterialStockPageDataTableList";
            commonService.callWebApiPost(url, postdata).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem2.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem2.data = [];
                    commonService.showError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_20') + res.data.Error.Message);
                }
                commonService.hideLoading();
            }, function (error) {
                commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                commonService.hideLoading();
            });
        }

        function search() {
            initGridData2();
            self.isReadOnly = false;
        }
        //编辑备注
        function edit() {
            //单耗
            var rows1 = $scope.gridApi.selection.getSelectedRows();
            if (rows1.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_22'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
                return false
            }
            rows1[0].Remark = self.currentItem.Remark;

        }

        //编辑超产品
        function editSuper() {

            if (self.selectedItem2 == null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_24'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
                return false
            }
            if (self.selectedItem2.Qty < self.currentItem.DeductionNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_25'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
                return false
            }

            self.selectedItem2.ActNum = self.currentItem.DeductionNum;

            // if (data[0].TatolNum<self.currentItem.DeductionNum) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_25'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
            //     return false
            // }
            // var userNum=self.currentItem.DeductionNum;

            // data.forEach((item,index,arr)=>{
            //     if(item.Qty>userNum)item.UserNum=userNum;
            //     else{
            //        item.ActNum=userNum-item.Qty;
            //        userNum=userNum-item.Qty;
            //     }
            // });
        }
        //保存超产品
        function saveSuper() {

            var rows1 = self.gridOptionsItem1.data;
            var rows2 = self.gridOptionsItem2.data;

            var nums = 0;
            rows2.forEach((item, index, arr) => {
                if (item.ActNum > 0) {
                    self.materialRows.push({
                        Id: item.Id,
                        FactoryCode: item.FactoryCode,
                        FactoryName: item.FactoryName,
                        MaterialCode: item.MaterialCode,
                        MaterialName: item.MaterialName,
                        BatchNo: item.BatchNo,
                        SmallClass: item.SmallClass,
                        SmallClassName: item.SmallClassName,
                        SupplierCode: item.SupplierCode,
                        Spec: item.Spec,
                        Unit: item.Unit,
                        Qty: item.Qty,
                        OldWhsCode: item.WhsCode,
                        OldLocationCode: item.LocationCode,
                        IsFrozen: item.IsFrozen,
                        ActNum: item.ActNum
                    });//暂时不做订单与超产品单关联
                    nums += item.ActNum;
                }
            });

            // if (nums < self.currentItem.TotalNum) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_26'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
            //     return false
            // }


            rows1.forEach((item, index, arr) => {

                if (nums > 0) {
                    var obj = {
                        ProductOrder: item.ProductOrder,
                        ExeWorkOrder: item.ExeWorkOrder,
                        WearingLayerCode: item.WearingLayerCode,
                        WearingLayerName: item.WearingLayerName,
                    }
                    if (nums < item.ShouldNum1) {
                        obj.ShouldNum = item.ShouldNum1;
                        obj.ActualNum = nums;
                        obj.ConsumeNum = nums;
                    } else if (rows1.length - 1 == index) {
                        obj.ShouldNum = item.ShouldNum1;
                        obj.ActualNum = nums;
                        obj.SuperNum = nums - item.ShouldNum1;
                        obj.ConsumeNum = nums;
                    } else {
                        obj.ShouldNum = item.ShouldNum1;
                        obj.ActualNum = item.ShouldNum1;
                        obj.ConsumeNum = item.ShouldNum1;
                    }
                    item.IsFlag = true;
                    self.list.push(obj);
                    nums = nums - item.ShouldNum1
                } else {
                    self.list.push({
                        ProductOrder: item.ProductOrder,
                        ExeWorkOrder: item.ExeWorkOrder,
                        WearingLayerCode: item.WearingLayerCode,
                        WearingLayerName: item.WearingLayerName,
                        ShouldNum: item.ShouldNum1,
                        ActualNum: 0,
                        SuperNum: 0,
                        ConsumeNum: 0
                    })
                }
            });
            // console.log(self.list);
            //console.log(self.gridOptionsItem1.data)
            self.isReadOnly = true;
        }
        //重置超产品
        function resetSuper() {
            initGridData2();
            self.isReadOnly = false;
        }

        function save() {

            if (self.list.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_27'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
                return false
            }

            var data = self.gridOptionsItem1.data;
            //备注添加
            self.list.forEach(item => {
                var rowEntity = data.find(t => t.ExeWorkOrder == item.ExeWorkOrder);
                item.FactoryCode = self.FactoryCode;
                item.FactoryName = self.FactoryName;
                item.Remark = rowEntity.Remark;
                item.WhsCode = self.typeWarehouse.value.ResourceCode;
                item.LocationCode = self.typeLocation.value.ResourceCode;
            })

            var data2 = data.filter(function (item) {
                return item.IsFlag == true;
            });

            self.materialRows.forEach(item => {
                item.FactoryCode = self.FactoryCode;
                item.FactoryName = self.FactoryName;
                item.WhsCode = self.typeWarehouse.value.ResourceCode;
                item.LocationCode = self.typeLocation.value.ResourceCode;
            });

            var postData = {
                data1: self.list,
                data2: data2,
                data3: self.materialRows
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_28') });
            var url = commonService.getMesApiAddress("plan") + 'PL_ExeWorkOrderWearingLayer/SaveBatchPL_ExeWorkOrderWearingLayer';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_29'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_23'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }


        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form") {
                self.validInputs = params.validity;
            } else if (params.id == "add_form1") {
                self.validInputs1 = params.validity;
            }
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderWearingLayer_WorkOrderWearingLayer';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderWearingLayer';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderWearingLayer-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderWearingLayer.addJS.Tips_30'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
