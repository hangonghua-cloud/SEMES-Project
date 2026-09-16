(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').controller('Siemens.SimaticIT.PlanApp.WorkOrderManage.AddProcess',
        ['common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'commonService', 'common.widgets.busyIndicator.service', '$state', '$rootScope',
            function (common, $filter, $scope, $modalInstance, data, commonService, busyIndicatorService, $state, $rootScope) {
                var vm = this;
                var sidePanelManager, backendService, propertyGridHandler;
                vm.data = angular.copy(data);
                vm.currentItem = vm.data.selectedItem;
                vm.lang = 'zh-cn';

                vm.validInputs = false;

                activate();

                function activate() {
                    init();
                    registerEvents();

                }


                function init() {
                    sidePanelManager = common.services.sidePanel.service;
                    backendService = common.services.runtime.backendService;
                    initGridOptionsDetail();
                    initGridData();
                }

                function initGridOptionsDetail() {
                    vm.gridOptions = {
                        fastWatch: true,
                        rowHeight: 35,
                        minimumColumnSize: 100,
                        enableMultiSelection: false,
                        enableFiltering: false,
                        //基础属性
                        enableSorting: true,//是否支持排序(列)
                        useExternalSorting: false,//是否支持自定义的排序规则
                        enableGridMenu: false,//是否显示表格 菜单
                        showGridFooter: false,//时候显示表格的footer
                        enableHorizontalScrollbar: 1,//表格的水平滚动条
                        enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)
                        selectionRowHeaderWidth: 30,
                        enableCellEditOnFocus: false,//default为false,true的时候单击即可打开编辑(cellEdit为true的时候,需要引入'ui.grid.cellNav')
                        //分页属性
                        enablePagination: false, //是否分页,default为true
                        enablePaginationControls: false, //使用默认的底部分页
                        paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                        paginationPageSize: 100, //每页显示个数
                        paginationCurrentPage: 1, //当前的页码  
                        totalItems: 0, // 总数量
                        useExternalPagination: true,//是否使用分页按钮
                        //选中
                        rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                        enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                        enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                        enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                        enableRowSelection: true, // 行选择是否可用,default为true;
                        enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                        enableSelectionBatchEvent: true, //default为true
                        modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                        multiSelect: true,// 是否可以选择多个,默认为true;
                        noUnselect: false,//default为false,选中后是否可以取消选中
                        appScopeProvider: self,
                        columnDefs: [
                            {
                                name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                                    '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                            },
                            {
                                field: 'MaterialClass',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_2'),
                                width: 150
                            },
                            {
                                field: 'SmallClass',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_3'),
                                cellTooltip: true,
                                width: 250
                            },
                            {
                                field: 'ProcessCode',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_4'),
                                cellTooltip: true,
                                width: 200
                            },
                            {
                                field: 'ProcessName',
                                displayName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_5'),
                                cellTooltip: true,
                                width: 250
                            }
                        ],
                        //---------------api---------------------
                        onRegisterApi: function (gridApi) {
                            $scope.gridOptions = gridApi;

                            //行选中事件
                            $scope.gridOptions.selection.on.rowSelectionChanged($scope, function (row, event) {
                                if (row) {
                                    if (row.isSelected) {
                                        vm.selectedItemDetail = row.entity;

                                    } else {
                                        vm.selectedItemDetail = null;

                                    }
                                }
                            });
                            //全选事件enableSelectAll（在grid上选中全选时触发）
                            $scope.gridOptions.selection.on.rowSelectionChangedBatch($scope, function (allRow, event) {
                                let len = $scope.gridOptions.selection.getSelectedRows().length;
                                if (len > 0) {
                                }
                                else {
                                }
                            });
                        },
                        data: []
                    }
                }

                function initGridData() {

                    var postData = {
                        queryJson: {
                            ProcessType: '2',
                            WorkOrder: vm.currentItem.WorkOrder
                        }
                    }
                    var url = commonService.getMesApiAddress('plan') + 'PL_WorkOrder/PL_WorkOrderProcessTable';
                    commonService.callWebApiPost(url, postData).then(function (res) {
                        if ((res) && (res.data.success)) {
                            //数据
                            vm.gridOptions.data = res.data.resultData.rows;
                        } else {
                            vm.gridOptions.data = [];
                        }
                    }, function (error) {
                        backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_6'));
                    });

                }
                vm.save = function () {
                    var copyData = $scope.gridOptions.selection.getSelectedRows();
                    if (copyData.length == 0) {
                        backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_7'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_6'));
                        return;
                    }
                    if (copyData.length > 1) {
                        var num = 0;
                        var TraitName = copyData[0].MaterialClass;
                        copyData.forEach((item, index, arr) => {

                            if (TraitName == item.MaterialClass) {
                                num = num + 1;
                            }

                        });
                        if (num > 1) {
                            backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_8'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_6'));
                            return;
                        }
                    }

                    var postData = {
                        entity: copyData,
                        WorkOrder: vm.currentItem.WorkOrder
                    }
                    var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/Save_VCworkProcess';
                    var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
                };
                //保存成功事件
                function onSaveSuccess(data) {

                    if (data.data.success) {
                        busyIndicatorService.hide();
                        //关闭侧边栏
                        //sidePanelManager.close();
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_9'));
                        //刷新局部
                        //$rootScope.$emit('to-parent', 'parent');
                        //$state.go('^', {}, { reload: false });
                        $modalInstance.close();
                    } else {
                        busyIndicatorService.hide();
                        backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_10'));
                    }
                }

                //保存失败事件
                function onSaveError(error) {
                    busyIndicatorService.hide();
                    backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addprocessJS.Tips_10'));
                }
                vm.cancel = function () {
                    $modalInstance.dismiss();
                };

                function registerEvents() {
                    $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
                }
                function onPropertyGridValidityChange(event, params) {
                    vm.validInputs = params.validity;
                }
            }
        ]);
}());