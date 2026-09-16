(function () {
    'use strict';
    angular.module('CCS.CommonApp.CommonUI').controller('CCS.CommonApp.CommonUI.SelectMaterialModal',
        ['common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'commonService',
            function (common, $filter, $scope, $modalInstance, data, commonService) {
                var vm = this;
                var sidePanelManager, backendService, propertyGridHandler;
                vm.data = angular.copy(data);
                vm.lang = 'zh-cn';
                vm.currentItem = null;
                console.log(vm.data);
                // //分页变量
                var pagination = {
                    rows: 20,//每页显示条数
                    page: 1,//页码
                    sidx: vm.data.sidx,//排序字段
                    sord: vm.data.sord//排序方式
                };
                //查询参数
                vm.queryParmeters = vm.data.queryParmeters;
                // vm.queryParmeters = {
                //     Type: 1,//物料类型（1-产品；2-包装物料；3-原料；）
                //     Name: null//物料名称（模糊查询）
                // };
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

                vm.gridOptions = {
                    enableFullRowSelection: true,
                    enableRowSelection: true,
                    enableSelectAll: vm.data.multiple,//是否多选
                    enableMultiSelection: vm.data.multiple,//是否多选
                    selectionRowHeaderWidth: 35,
                    enableRowHeaderSelection: true,
                    //Added for custom paging      
                    paginationPageSizes: [pagination.rows, pagination.rows * 2, pagination.rows * 4, pagination.rows * 10, pagination.rows * 20],
                    paginationPageSize: pagination.rows,
                    useExternalPagination: true, // custom      
                    useExternalSorting: true, // custom      
                    useExternalFiltering: true, // custom 
                    totalItems: null,
                    multiSelect: vm.data.multiple,//是否多选
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

                function init() {
                    sidePanelManager = common.services.sidePanel.service;
                    backendService = common.services.runtime.backendService;
                    loadData();
                }
                function loadData() {
                    // debugger;
                    // commonService.showLoading({ message: "加载中,请稍后..." });
                    if (vm.data.method == "Get") {
                        let requestUrl = "";
                        if (!!vm.queryParmeters.Name) {
                            if (vm.data.url.indexOf('=') > -1)
                                requestUrl = vm.data.url + '&Name=' + vm.queryParmeters.Name;
                            else
                                requestUrl = vm.data.url + vm.queryParmeters.Name;
                        }
                        else {
                            requestUrl = vm.data.url;
                        }
                        commonService.callWebApiGet(requestUrl, null).then(function (res) {
                            if (res && res.data.success) {
                                if (vm.data.isFilter == "0") vm.gridOptions.data = res.data.resultData;
                                else {
                                    vm.gridOptions.data = res.data.resultData.filter((item) => { return item.ItemValue && item.ItemValue != "" });
                                    vm.gridOptions.totalItems = res.data.resultData.records;
                                }
                            } else {
                                vm.gridOptions.data = [];
                                commonService.showError("获取数据出错:" + res.data.Error.Message);
                            }
                            commonService.hideLoading();
                        }, function (error) {
                            commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                            commonService.hideLoading();
                        });
                    } else if (!vm.data.pagination || vm.data.pagination == null) {//不分页
                        commonService.callWebApiPost(vm.data.url, vm.queryParmeters).then(function (res) {
                            if (res && res.data.success) {
                                vm.gridOptions.totalItems = res.data.resultData.length;
                                vm.gridOptions.data = res.data.resultData;
                                vm.gridOptions.paginationPageSize = res.data.resultData.length;
                                if (vm.gridOptions.paginationPageSizes.indexOf(res.data.resultData.length) < 0)
                                    vm.gridOptions.paginationPageSizes.push(res.data.resultData.length);
                                pagination.rows = res.data.resultData.length;
                            } else {
                                vm.gridOptions.data = [];
                                commonService.showError("获取数据出错:" + res.data.Error.Message);
                            }
                            commonService.hideLoading();
                        }, function (error) {
                            commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                            commonService.hideLoading();
                        });
                    } else {

                        //分页
                        var postData = {
                            pagination: pagination,
                            queryJson: vm.queryParmeters
                        }
                        commonService.callWebApiPost(vm.data.url, postData).then(function (res) {

                            if (res && res.data.success) {
                                vm.gridOptions.totalItems = res.data.resultData.records;
                                vm.gridOptions.data = res.data.resultData.rows;
                            } else {
                                vm.gridOptions.data = [];
                                commonService.showError("获取数据出错:" + res.data.Error.Message);
                            }
                            commonService.hideLoading();
                        }, function (error) {
                            commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                            commonService.hideLoading();
                        });

                    }

                }

                vm.save = function () {
                    let selectionRows = $scope.gridApi.selection.getSelectedRows();
                    $modalInstance.close(selectionRows);
                };

                vm.cancel = function () {
                    $modalInstance.dismiss();
                };
            }
        ]);
}());