(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderDismantle').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderDismantle.WorkOrderDismantle.service', '$state', '$stateParams',
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
            initGridData2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_1'));
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
            self.selectedItem1 = {};
            self.selectedItem2 = {};
            self.validInputs = false;
            self.xuyaoNum = 0;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.edit = edit;
            self.blurActNum = blurActNum;
        }

        function initDictionary() {

            self.Process = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_2'), ResourceCode: "" }]
            };

            commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
                if (res && res.data.success) {
                    self.Process.options = res.data.resultData;
                    self.Process.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_2')
                    });
                }
            });
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initGridOptions1() {
            self.gridOptionsItem1 = {
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
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_4'),
                        width: 110
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_7'),
                        width: 70
                    },
                    {
                        field: 'ProductNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'UnProductNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_9'),
                        width: 110
                    },

                    {
                        field: 'SuperProdunction',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_10'),
                        width: 110
                    },
                    {
                        field: 'ActNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'DeductionNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_12'),
                        width: 110
                    },
                    {
                        field: 'StartOperationName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_13'),
                        width: 110
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_14'),
                        width: 60,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.Unit==1"><span ng-cell-text >米</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.Unit==2"><span ng-cell-text >张</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.Unit!=3"><span ng-cell-text >片</span></div>'
                    },
                    {
                        field: 'WFNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_18'),
                        width: 110
                    },
                    {
                        field: 'ShouldNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_19'),
                        width: 110
                    },
                    // {
                    //     field: 'DeductionAfterNum',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_20'),
                    //     width: 110
                    // },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_21'),
                        width: 110
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem1 = row.entity;
                            self.currentItem.ActNum = angular.copy(row.entity.UnProductNum);
                            //setButtonsVisibility(true);
                            GetWorkOrderBomUnitConsome();
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

        function GetWorkOrderBomUnitConsome() {
            let queryParmeters = {
                queryJson: {
                    WorkOrder: self.selectedItem1.WorkOrder
                }
            };
            var url = commonService.getMesApiAddress("plan") + 'PL_BOM/GetWorkOrderBomUnitConsome';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    console.log(res);
                    self.currentItem.DanHao = res.data.resultData.DanHao;
                } else {
                    backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_22'));
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_22'));
            });
        }

        function edit() {
            //单耗
            debugger;
            if (!self.currentItem.DanHao) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_23'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false;
            }
            //var danhao=0.2;
            var copyData = $scope.gridApi.selection.getSelectedRows();
            if (copyData.length != 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_25'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false;
            }

            if (self.currentItem.DeductionNum && self.currentItem.DeductionNum > 0 && self.Process.value.ResourceCode == "") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_26'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false;
            }

            // debugger
            if (self.currentItem.ActNum > self.selectedItem1.ProductNum || self.selectedItem1.SuperProdunction < self.currentItem.DeductionNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_27'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false;
            }

            if (self.currentItem.DeductionNum) {
                self.currentItem.DeductionNumcopy = self.currentItem.DeductionNum;
                //超产品抵扣
                copyData[0].DeductionNum = angular.copy(self.currentItem.DeductionNum);
            } else {
                self.currentItem.DeductionNumcopy = 0;
            }
            if (self.currentItem.ActNum + self.currentItem.DeductionNumcopy != copyData[0].ActNum) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_28'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false;
            }


            //生成执行工单数量
            copyData[0].ActNum = angular.copy(self.currentItem.ActNum);
            //未生成
            if (copyData[0].UnProductNum == copyData[0].ProductNum) {
                copyData[0].UnProductNum = angular.copy(copyData[0].ProductNum - self.currentItem.ActNum - self.currentItem.DeductionNumcopy);
            } else {
                copyData[0].UnProductNum = angular.copy(copyData[0].UnProductNum - self.currentItem.ActNum - self.currentItem.DeductionNumcopy);
            }


            //应发数量
            copyData[0].ShouldNum = angular.copy(Math.ceil((self.currentItem.ActNum - self.currentItem.DeductionNumcopy) * self.currentItem.DanHao));

            //开始工序
            if (self.Process.value.ResourceCode != "") {
                copyData[0].StartOperationName = angular.copy(self.Process.value.ResourceName);
                copyData[0].SuperStartOperation = angular.copy(self.Process.value.ResourceCode);
            }
            //备注
            self.xuyaoNum = 0;
            copyData[0].Remark = angular.copy(self.currentItem.Remark);
            self.gridOptionsItem1.data.forEach((item, index, arr) => {
                if (item.ShouldNum) self.xuyaoNum = self.xuyaoNum + item.ShouldNum;
            })
        }


        function initGridOptions2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: true,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: true,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    // {
                    //     name: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_29'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_30'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'SuperNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_31'),
                        width: 100
                    },
                    {
                        field: 'Nums',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_32'),
                        width: 100
                    },
                    {
                        field: 'ActNum',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_33'),
                        width: 100,
                        cellTemplate: '<sit-numeric sit-value="row.entity.ActNum" ng-blur="row.entity.blurActNum(row.entity)"></sit-numeric>'
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_34'),
                        width: 100,
                    },
                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_5'),
                        width: 100
                    },

                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_35'),
                        width: 80
                    },
                    {
                        field: 'SupplierName',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_36'),
                        width: 100
                    },
                    {
                        field: 'LotNo',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_37'),
                        width: 100
                    },
                    {
                        field: 'Unit',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_14'),
                        width: 80
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_38'),
                        width: 100
                    },

                    // {
                    //     field: 'MixTime',
                    //     displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_39'),
                    //     enableCellEdit: false,
                    //     cellFilter: 'date:\'yyyy-MM-dd HH:mm:ss\'',
                    //     width: 150,
                    //     cellTemplate:
                    //         '<div ng-show="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.MixTime"' +
                    //         'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                    //         'sit-show-button-bar="true"' +
                    //         'sit-show-weeks="false"' +
                    //         'sit-validation="{required: false}"></sit-date-time-picker></div>',
                    // },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi2 = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem2 = row.entity;
                            //setButtonsVisibility(true);
                        } else {
                            self.selectedItem2 = null;
                            //setButtonsVisibility(false);
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
            var rows = [
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
                { MaterialCode: "ABC", MaterialName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_40'), Nums: 1000, blurActNum: self.blurActNum },
            ]
            self.gridOptionsItem2.data = rows;
        }

        function blurActNum(row) {
            //发料数量
            if (row.ActNum > row.Nums || row.ActNum < 0) {
                row.ActNum = 0;
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_41'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
            }
            //   if(row.ActNum>0 && row.ActNum<=row.Nums){
            //       row.Nums=row.Nums-row.ActNum;
            //   }  
        }

        function save() {
  
            var rows1 = self.gridOptionsItem1.data;
            var rows2 = $scope.gridApi2.selection.getSelectedRows();
            if (rows2.length < 1) {
                busyIndicatorService.hide();
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_42'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false
            }
            var num = 0;
            rows2.forEach((item, index, arr) => {
                num = num + item.ActNum;
            });
            if (self.xuyaoNum > num) {
                //busyIndicatorService.hide();
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_43'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
                return false
            }
            rows1.forEach((item, index, arr) => {
                if (index == rows1.length - 1) {
                    item.SuperNum = (num - item.ShouldNum);
                    item.ActualNum = num;
                    item.ConsumeNum = item.ShouldNum;//耗用
                } else {
                    item.ActualNum = item.ShouldNum;
                    num = num - item.ShouldNum;
                    item.ConsumeNum = item.ActualNum;
                }
            })

            rows2.forEach((item, index, arr) => {
                item.Nums = item.Nums - item.ActNum;
            });

            var postData = {
                rows1: rows1,
                rows2: rows2
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_44') });
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
  
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_45'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_24'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderDismantle_WorkOrderDismantle';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderDismantle';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrderDismantle-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderDismantle.addJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
