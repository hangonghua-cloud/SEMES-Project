(function () {
    'use strict';
    angular.module('CCS.CommonApp.CommonUI').controller('CCS.CommonApp.CommonUI.Select_SingleChoiceModalForBarCode',
        ['common.base', '$filter', '$scope', '$uibModalInstance','data', 'commonService',
            function (common, $filter, $scope, $modalInstance, data, commonService) {
                var vm = this;
                var sidePanelManager, backendService, propertyGridHandler;
                vm.data = angular.copy(data);
                vm.lang = 'zh-cn';
                vm.currentItem = null;
                // //分页变量
                var pagination = {
                    rows: 20,//每页显示条数
                    page: 1,//页码
                    sidx: vm.data.sidx,//排序字段
                    sord: vm.data.sord//排序方式
                };
                //查询参数
                vm.queryParmeters = vm.data.queryParmeters;
                activate();

                function activate() {
                    init();
                    vm.loadData = loadData;
                }
                vm.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
                    if (col.filters[0].term) {
                        return 'header-filtered';
                    } else {
                        return '';
                    }
                };

                //grid列表初始化
                vm.gridOptions = {
                    enableFullRowSelection: true,
                    enableRowSelection: true,
                    enableSelectAll: false,
                    enableMultiSelection: false,
                    selectionRowHeaderWidth: 35,
                    enableRowHeaderSelection: true,
                    //Added for custom paging      
                    paginationPageSizes: [pagination.rows, pagination.rows * 2, pagination.rows * 4, pagination.rows * 10, pagination.rows * 20],
                    paginationPageSize: pagination.rows,
                    useExternalPagination: true, // custom      
                    useExternalSorting: true, // custom      
                    useExternalFiltering: true, // custom 
                    totalItems: null,
                    multiSelect: false,
                    enableSorting: true,
                    enableFiltering: false,
                    columnDefs: vm.data.columnDefs,
                    onRegisterApi: function (gridApi) {
                        $scope.gridApi = gridApi;
                        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                            // var msg = 'row selected ' + row.isSelected;
                            // console.log(msg);
                            if (row && row.isSelected === true) {
                                vm.selectedItem = row.entity;
                            } else {
                                vm.selectedItem = null;
                            }
                        });
                        gridApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {
                            var msg = 'rows changed ' + rows.length;
                            console.log(msg);
                        });
                        //Added for custom paging      
                        gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                            pagination.page = newPage;
                            pagination.rows = pageSize;
                            vm.loadData();
                        });
                        //custom sort      
                        $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                            if (sortColumns.length === 0) {//不排序
                                pagination.sidx = "";
                                pagination.sord = "";
                            } else {//按指定的一个字段排序
                                if (sortColumns.length === 1) {
                                    pagination.sidx = sortColumns[0].field;
                                    pagination.sord = sortColumns[0].sort.direction;
                                }
                                else {//组合排序
                                    var sortname = "";
                                    for (var i = 0, len = sortColumns.length; i < len; i++) {
                                        if (i > 0) {
                                            sortname += ", ";
                                        }
                                        sortname += sortColumns[i].field;
                                        if (i !== len - 1) {
                                            sortname += " " + sortColumns[i].sort.direction;
                                        }
                                    }
                                    //console.log('-----------------sortname------------------');
                                    //console.log(sortname);
                                    pagination.sidx = sortname;
                                    pagination.sord = sortColumns[len - 1].sort.direction;
                                }
                            }
                            loadData();
                        });
                    },
                    data: []
                };
                //初始化
                function init() {
                    sidePanelManager = common.services.sidePanel.service;
                    backendService = common.services.runtime.backendService;
                    loadData();
                }
                //加载数据
                function loadData() {
                    commonService.showLoading({ message: commonService.$t('customCommon.SelectModal.JS.Tips_6') });
                    var queryParmeters = {
                        pagination: pagination,
                        queryJson: vm.queryParmeters
                    };
                    console.log('Select_SingleChoiceModalForBarCode:queryParmeters');   console.log(queryParmeters);
                    //调用API 传递翻页\参数 获取数据
                    commonService.callWebApiPost(vm.data.url, queryParmeters).then(function (res) {
                        if ((res) && (res.data.success)) {
                            //console.log("res.data----------------" + JSON.stringify(res));
                            //总条数
                            vm.gridOptions.totalItems = res.data.resultData.records;
                            //数据  
                            vm.gridOptions.data = res.data.resultData.rows;
                        } else {
                            vm.gridOptions.data = [];
                            console.log("获取数据出错---------error--------"+JSON.stringify(res));
                            commonService.showError("获取数据出错:" + res.data.returnMsg);
                        }
                        commonService.hideLoading();
                    }, function (error) {
                        console.log("error--------"+JSON.stringify(error));
                        commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                        commonService.hideLoading();
                    });                   
                }

                //选择
                vm.save = function () {
                    //获取行数据
                    let selectionRows = $scope.gridApi.selection.getSelectedRows();
                    //console.log('selectionRows-----------------' + JSON.stringify(selectionRows));
                    //vm.data.result_obj.SetMaterialCode = selectionRows.SetMaterialCode
                    //vm.data.result_obj.SetMaterialName = selectionRows.EquipmentName;
                    //关闭弹窗并返回选择的行数据
                    $modalInstance.close(selectionRows);
                };
                //取消
                vm.cancel = function () {
                    //释放弹窗
                    $modalInstance.dismiss();
                };
            }
        ]);
}());