/** 
*  1. 任务功能： 公共通用单选择弹窗(精工UI-GRID列表)
*  2. 创建人员： 刘万军
*  3. 创建日期： 2021-2-20
*  4. 修改日期： 2021-4-15
**/
(function () {
    'use strict';
    angular.module('CCS.CommonApp.CommonUI').controller('CCS.CommonApp.CommonUI.Select_SingleChoiceModal',
        ['common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'commonService', '$sce', 'common.services.authentication',
            function (common, $filter, $scope, $modalInstance, data, commonService, $sce, auth) {
                var self = this;
                var sidePanelManager, backendService, propertyGridHandler;
                self.data = angular.copy(data);

                self.lang = 'zh-cn';
                self.currentItem = null;
                // //分页变量
                var pagination = {
                    rows: 20,//每页显示条数
                    page: 1,//页码
                    sidx: self.data.sidx,//排序字段
                    sord: self.data.sord//排序方式
                };

                //查询参数
                self.queryParmeters = {};//self.data.queryParmeters;

                activate();

                function activate() {
                    init();



                    self.loadData = loadData;
                }

                self.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
                    if (col.filters[0].term) {
                        return 'header-filtered';
                    } else {
                        return '';
                    }
                };

                //grid列表初始化
                self.gridOptions = {
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
                    multiSelect: false, //单选 /多选
                    enableSorting: true,
                    enableFiltering: false,
                    columnDefs: self.data.columnDefs,
                    onRegisterApi: function (gridApi) {
                        $scope.gridApi = gridApi;
                        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                            // var msg = 'row selected ' + row.isSelected;
                            // console.log(msg);
                            if (row && row.isSelected === true) {
                                self.selectedItem = row.entity;
                            } else {
                                self.selectedItem = null;
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
                            self.loadData();
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
                    //let QueryAry =[{'FieldCode': 'purchaseOrderNo', 'FileldName':'ERP到料单号','FiledType':'Text'},{'FieldCode': 'srName', 'FileldName':'供应商名称','FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':'订单日期','FiledType':'Date'}]
                    let QueryAry = self.data.queryParmeters;
                    self.html_template = BuildQueryHtml(QueryAry);
                    console.log(self.html_template);
                }

                //根据定义字段生成查询条件
                function BuildQueryHtml(QueryAry) {
                    let Text_html = "<sit-property sit-widget=\"sit-text\" sit-value=\"vm.queryParmeters.{0}\" sit-read-only=\"false\" style=\"margin-left: 10px;\">{1}:</sit-property>";
                    let Date_html = "<sit-property sit-widget=\"sit-datepicker\" sit-value=\"vm.queryParmeters.{0}\" sit-read-only=\"false\" sit-widget-attributes=\"{'sit-format':'yyyy-MM-dd','sit-show-meridian':'false'}\">{1}:</sit-property>";
                    let QueryHtml = "<sit-property-grid sit-id=\"add_form\" sit-layout=\"Horizontal\" sit-type=\"Fluid\" sit-mode=\"edit\" sit-columns=\"54\">";
                    QueryAry.forEach(element => {
                        switch (element.FiledType) {
                            case 'Text':
                                QueryHtml += Text_html.replace('{0}', element.FieldCode).replace('{1}', element.FileldName);
                                break;
                            case 'Date':
                                QueryHtml += Date_html.replace('{0}', element.FieldCode).replace('{1}', element.FileldName);
                                break;
                        }
                    });
                    QueryHtml += "<button ng-click=\"vm.loadData()\" class=\"btn btn-primary\" style=\"height: 34px;margin-top: 20px;\"><i class=\"fa fa-search\"></i><span>&nbsp;{{'customCommon.SelectModal.Tips_3'|translate}}</span></button>";
                    QueryHtml += "</sit-property-grid>";
                    return QueryHtml;
                }

                //加载数据
                function loadData() {
                    console.log(self.queryParmeters);
                    commonService.showLoading({ message: commonService.$t('customCommon.SelectModal.JS.Tips_6') });
                    let queryJson = Object.assign(self.queryParmeters, self.data.myParameters);
                    var queryParmeters = {
                        pagination: pagination,
                        queryJson: queryJson
                    };
                    console.log('Select_SingleChoiceModal:queryJson------' + JSON.stringify(queryJson));
                    //调用API 传递翻页\参数 获取数据
                    commonService.callWebApiPost(self.data.url, queryParmeters).then(function (res) {
                        //console.log("res.data----------------" + JSON.stringify(res));
                        if ((res) && (res.data.success)) {
                            //总条数
                            self.gridOptions.totalItems = res.data.resultData.records;
                            //数据  
                            self.gridOptions.data = res.data.resultData.rows;
                        } else {
                            self.gridOptions.data = [];
                            commonService.showErrorMessage("获取数据出错:" + res.data.Error.Message);
                        }
                        commonService.hideLoading();
                    }, function (error) {
                        commonService.showErrorMessage('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                        commonService.hideLoading();
                    });
                }

                //选择
                self.save = function () {
                    //获取行数据
                    let selectionRows = $scope.gridApi.selection.getSelectedRows();
                    //console.log('selectionRows-----------------' + JSON.stringify(selectionRows));
                    //关闭弹窗并返回选择的行数据
                    $modalInstance.close(selectionRows);
                };
                //取消
                self.cancel = function () {
                    //释放弹窗
                    $modalInstance.dismiss();
                };
            }
        ]);
}());