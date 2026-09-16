(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $interval, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            initGridOptions1();
            initGridOptions2();

            initGridData1();
            initGridData2();


            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_1'));
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


            self.currentItem.DXZH = 0
            self.selectedItem1 = null;
            self.selectedItem2 = null;
            self.validInputs = false;
            self.isVisbale = false;
            self.materialItem = [];//面膜物料扣除
            self.superList = [];//超产品工单
            self.superMateiral = [];//超产品扣除

            initDictionary();

            //Expose Model Methods
            self.save = save;//保存面膜发料
            self.cancel = cancel;
            self.edit = edit;//编辑备注
            self.editMask = editMask;//面膜发料
            self.editSuper = editSuper;//编辑超产品
            self.resetSuper = resetSuper;//超产品重置
            self.saveSuper = saveSuper;//保存超产品

        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function initDictionary() {

            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_2'), ResourceCode: "" }]
            };

            // commonService.getProcessByFactory({ LevelCode: self.currentItem.FactoryCode }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.Process.options = res.data.resultData;
            //         self.Process.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_2')
            //         });
            //     }
            // });
        }





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
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_4'),
                        width: 150
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_5'),
                        width: 120
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_7'),
                        width: 70
                    },
                    {
                        field: 'OrderNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'ProductNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'UnProductNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'ActNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'SuperProdunction',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_12'),
                        width: 110
                    },

                    {
                        field: 'DeductionNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'ShouldNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_14'),
                        width: 110,
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_15'),
                        width: 60,
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_16'),
                        width: 110
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            //setButtonsVisibility(true);
                            //GetWorkOrderBomUnitConsome();
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
                data: []
            };
        }

        function initGridData1() {

            var url = commonService.getMesApiAddress("plan") + "PL_PlanStoreIssue/GetStoreIssueWorkOrder";
            commonService.callWebApiPost(url, { data: self.currentItem.array }).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem1.data = res.data.resultData;
                    var allQty = 0;
                    _.each(self.gridOptionsItem1.data, function (item, index) {
                        allQty += item.ShouldNum
                    })
                    self.currentItem.AllQty = allQty
                    initMarkQty(self.gridOptionsItem1.data[0].MMXH);
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }

        function initMarkQty(MMXH) {
            var postdata = {
                queryJson: {
                    FactoryCode: self.currentItem.FactoryCode,
                    MaterialCode: MMXH,
                    IsFrozen: "0",
                    QtyStr: ">0",
                    FieldValue: "1",
                }
            }
            var url = commonService.getMesApiAddress("material") + "MM_RawMaterialStock/GetRawMaterialStock";
            commonService.callWebApiPost(url, postdata).then(function (res) {
                if (res && res.data.success) {
                    var MarkQty = 0;
                    _.each(res.data.resultData.rows, function (item, index) {
                        MarkQty += item.Qty
                    })
                    self.currentItem.MarkQty = MarkQty
                } else {

                }
            })
        }


        //编辑备注
        function edit() {
            //单耗
            if (!self.selectedItem1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_17'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            self.selectedItem1.Remark = self.currentItem.Remark;

        }
        //面膜发料
        function editMask() {
            var rows = $scope.gridApi.selection.getSelectedRows();
            //var rows=self.gridOptionsItem1.data;
            if (rows.length <= 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_19'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            var TotalNum = 0;
            rows.forEach((item, index, arr) => {
                TotalNum += parseFloat(item.ShouldNum)
            });

            var modalInstance = commonService.openModel({
                templateUrl: 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle/MaskSending.html',
                controller: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.MaskSending',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            queryParmeters: {
                                FactoryCode: self.currentItem.FactoryCode,
                                FactoryName: self.currentItem.FactoryName,
                                TotalNum: TotalNum.toFixed(2),
                                WorkOrder: rows[0].WorkOrder,
                                MaterialCode: rows[0].MaterialCode,
                                MMXH: rows[0].MMXH
                            }
                        };
                    }
                }
            });
            modalInstance.result.then(function (res) {
                var UserNum = res.UseNum;//面膜(米)
                self.materialItem = res.data;
                //返回对象数组中的某一个属性
                let arrWorkOrder = rows.map(item => {
                    return item.WorkOrder;
                })
                let workOrders = arrWorkOrder.join(',');
                self.materialItem.forEach(item => {
                    item.WorkOrder = workOrders;
                })
                rows.forEach((item, index, arr) => {
                    var DeductionNum = item.DeductionNum;//超产品(张)
                    //米=张*大小张转换*单耗
                    var ActNum = item.ActNum;
                    if (!DeductionNum) DeductionNum = 0;//超产品张数
                    //执行工单张数
                    item.PSheetsQty = item.ProductNum - DeductionNum;

                    if (UserNum > 0) {
                        if (item.MaskStatus == "2" && rows.length == 1) {
                            //已发料的工单
                            //1.已经发到0了
                            //2.未发到0
                            if (UserNum >= item.ShouldNum) {
                                item.SuperNum = UserNum - item.ShouldNum;
                                item.ActualNum = UserNum;
                                item.ConsumeNum = UserNum;
                            } else {
                                item.ActualNum = UserNum;
                                item.ConsumeNum = UserNum;
                                item.ShouldNum = UserNum;
                                item.ActNum = Math.ceil(item.ShouldNum / item.MaskConsume / item.DXZH) //发料张数

                            }
                        } else if (index == rows.length - 1) {
                            if (UserNum >= item.ShouldNum) {
                                // item.ShouldNum = ((item.ActNum - DeductionNum) * 1.0 * item.DXZH * item.MaskConsume).toFixed(2)//应发米数
                                item.ShouldNum = ((item.ActNum) * 1.0 * item.DXZH * item.MaskConsume).toFixed(2)//应发米数
                                item.SuperNum = (UserNum - item.ShouldNum);//超发米数
                            } else {
                                item.ShouldNum = UserNum//应发米数
                                item.ActNum = Math.ceil(item.ShouldNum / item.MaskConsume / item.DXZH) //发料张数
                            }
                            item.ActualNum = UserNum;//实发米数
                            item.ConsumeNum = UserNum;//耗用米数

                        } else {
                            //应发数量
                            if (UserNum >= item.ShouldNum) {
                                // item.ShouldNum = ((item.ActNum - DeductionNum) * 1.0 * item.DXZH * item.MaskConsume).toFixed(2)//应发米数
                                item.ShouldNum = ((item.ActNum) * 1.0 * item.DXZH * item.MaskConsume).toFixed(2)//应发米数
                                item.ActualNum = item.ShouldNum;//实发米数
                                UserNum = UserNum - item.ShouldNum;
                                item.ConsumeNum = item.ActualNum;
                            } else {
                                item.ShouldNum = UserNum
                                item.ActualNum = UserNum;
                                item.ConsumeNum = UserNum;
                                item.ActNum = Math.ceil(item.ShouldNum / item.MaskConsume / item.DXZH) //发料张数
                                UserNum = 0;
                            }

                        }

                        item.UnProductNum = item.UnProductNum - item.ActNum - DeductionNum;
                        if (item.UnProductNum < 0) {
                            item.UnProductNum = 0;
                        }
                        item.WhsCode = res.WhsCode;
                        item.LocationCode = res.LocationCode;
                        item.IsFilter = "1";
                    } else {
                        item.ShouldNum = 0;
                    }

                });
            });
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
                        field: 'ProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_20'),
                        width: 100
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_21'),
                        width: 100
                    },

                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_5'),
                        width: 100,
                    },
                    {
                        field: 'StockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_22'),
                        width: 110
                    },

                    {
                        field: 'SheetStockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_23'),
                        width: 110
                    },
                    {
                        field: 'BatchNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_24'),
                        width: 100
                    },
                    {
                        field: 'NoLockQty',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_25'),
                        width: 110
                    },
                    {
                        field: 'UseNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_26'),
                        width: 110
                    },

                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                            self.isVisbale = true;
                            //setButtonsVisibility(true);
                            //self.currentItem.DeductionNum = angular.copy(self.selectedItem2.LockNum);
                        } else {
                            self.selectedItem2 = null;
                            //setButtonsVisibility(false);
                            self.currentItem.DeductionNum = 0;
                            self.isVisbale = false;
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

            var postData = {
                queryJson: {
                    FactoryCode: self.currentItem.FactoryCode,
                    MaterialCode: self.currentItem.MaterialCode
                }
            };
            var url = commonService.getMesApiAddress("material") + "MM_SuperProductStock/MM_SuperProductStockPageDataTableList";
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem2.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = []
                }
            })
        }


        //编辑超产品
        function editSuper() {

            var data = $scope.gridApi.selection.getSelectedRows();
            if (data.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_27'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            if (data[0].ActNum == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_28'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }

            if (self.selectedItem2 == null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_29'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            if (self.selectedItem2.UseNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_30'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            if (!self.currentItem.DeductionNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_31'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            if (self.currentItem.DeductionNum > self.selectedItem2.NoLockQty) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_32'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            // self.selectedItem2.LockNum = self.selectedItem2.Inventorysheet - self.currentItem.DeductionNum;
            self.selectedItem2.UseNum = self.currentItem.DeductionNum;
            self.selectedItem2.LockedQty = self.selectedItem2.LockedQty + self.currentItem.DeductionNum;
            self.currentItem.DeductionNum = 0;
            console.log(self.selectedItem2.LockedQty);
            //if(!self.selectedItem1.DeductionNum) self.selectedItem1.DeductionNum=0;
            //self.selectedItem1.DeductionNum=self.selectedItem1.DeductionNum+self.currentItem.DeductionNum;
            //self.selectedItem2.StartOperationName = self.Process.value.ResourceName;
            //self.selectedItem2.StartOperation = self.Process.value.ResourceCode;

        }
        //保存超产品
        function saveSuper() {

            if (self.materialItem.length > 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_33'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false
            }
            var rows1 = $scope.gridApi.selection.getSelectedRows();
            if (rows1.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_34'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            var data = self.gridOptionsItem2.data;
            var materialRows = [];
            var nums = 0;
            var pieceQty = 0;
            var startOperation = "";
            let batchNo = "";
            let supId = "";
            data.forEach((item, index, arr) => {
                if (item.UseNum > 0) {
                    //超产品库存
                    materialRows.push(item);//暂时不做订单与超产品单关联
                    //超产品订单
                    // superRows.push({
                    //     // ProductOrder:rows1[0].ProductOrder,
                    //     // WorkOrder: rows1[0].WorkOrder,
                    //     // WorkOrderType:"5",//超产品执行工单
                    //     //MaterialCode: item.MaterialCode,
                    //     StartOperation: item.StartOperation,
                    //     SheetsQty: item.UseNum,//超产品张数
                    //     PiecesQty: Math.ceil(item.UseNum * rows1[0].DXZH),//超产品片数
                    //     Yield: rows1[0].Yield,
                    //     Process: rows1[0].Process,
                    //     TransferBy: rows1[0].TransferBy,
                    //     IsEnabled: 1
                    // });
                    startOperation = item.ProcessCode;
                    batchNo = item.BatchNo;
                    supId = item.Id;
                    nums += Math.ceil(item.UseNum / item.DXZH);
                    pieceQty += item.UseNum;
                }
            });


            if (nums > rows1[0].SuperProdunction) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_35'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            //超产品抵扣张数
            let actum = rows1[0].UnProductNum - nums;
            if (actum < 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_36'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            rows1[0].DeductionNum = nums;
            //发料数量
            rows1[0].ActNum = actum;
            rows1[0].ShouldNum = ((rows1[0].UnProductNum - rows1[0].DeductionNum) * 1.0 * rows1[0].DXZH * rows1[0].MaskConsume).toFixed(2);
            //未发数量 未放量
            //rows1[0].UnProductNum = Math.ceil(rows1[0].ProductNum - nums);
            // //汇总未发数量
            // self.currentItem.TotalNum = self.currentItem.TotalNum - nums;
            // var postData = {
            //     KeyValue: rows1[0].Id,
            //     UnProductNum: rows1[0].UnProductNum,
            //     WorkOrder: rows1[0].WorkOrder,
            //     data: rows
            // }

            var super1 = self.superList.find(t => t.WorkOrder == rows1[0].WorkOrder);
            if (super1 != null) {
                var index = self.superList.indexOf(super1);
                self.superList.splice(index, 1);
            }
            self.superList.push({
                FactoryCode: rows1[0].FactoryCode,
                FactoryName: rows1[0].FactoryName,
                ProductOrder: rows1[0].ProductOrder,
                WorkOrder: rows1[0].WorkOrder,
                WorkOrderType: "5",//超产品执行工单
                //MaterialCode: item.MaterialCode,
                Process: rows1[0].Process,
                StartOperation: startOperation,
                ActNum: nums,//超产品张数
                PSheetsQty: nums,//执行工单生产放量张数
                PieceQty: pieceQty,//选定片数之和
                Yield: rows1[0].Yield,
                Process: rows1[0].Process,
                TransferBy: rows1[0].TransferBy,
                IsEnabled: 1,
                BatchNo: batchNo,
                SupId: supId
            });

            materialRows.forEach(item => {
                // var super1=self.superMateiral.find(t=>t.WorkOrder==item.WorkOrder);
                // if(super1==null){
                //     self.superMateiral.push(item);
                // }else{
                //     var index=self.superMateiral.indexOf(super1);
                //     self.superMateiral.splice(index,1);
                //     self.superMateiral.push(item);
                // }

                //测试数据暂不校验重复
                self.superMateiral.push(item);
            })

        }
        //重置超产品
        function resetSuper() {
            self.superList = [];
            self.superMateiral = [];
            initGridData1();
            initGridData2();
            self.isVisbale = false;
            self.selectedItem2 = null;
            self.currentItem.DeductionNum = 0;
        }

        //保存分配的执行工单
        function save() {
            var rows1 = $scope.gridApi.selection.getSelectedRows();
            if (rows1.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_37'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }
            // if (self.materialItem.length < 1) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_38'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
            //     return false;
            // }
            //发料不足时，剔除没有发料的工单
            rows1 = rows1.filter(function (item) {
                return item.IsFilter == "1" && item.ActNum > 0;
            })
            rows1 = rows1.concat(self.superList);

            if (rows1.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_39'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
                return false;
            }

            var postData = {
                rows1: rows1,//拆解工单信息
                rows2: self.materialItem,//面膜物料扣除
                rows3: self.superMateiral,//超产品扣除
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_40') });
            var url = commonService.getMesApiAddress("plan") + 'PL_PlanStoreIssue/SaveWorkOrderDismantle';
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
            self.materialItem = []
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_42'));
                //刷新局部
                $rootScope.$emit('to-parent', "parent");
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_18'));
        }


        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form") {
                self.validInputs = params.validity;
            }

        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle_WorkOrderDismantle';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle';

        var state = {
            name: screenStateName + '.split',
            url: '/split',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderDismantle-split.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.splitJS.Tips_43'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
