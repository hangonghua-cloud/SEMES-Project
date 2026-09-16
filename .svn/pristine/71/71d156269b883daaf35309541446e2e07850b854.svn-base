(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ProcessBad').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ProcessBad.ProcessBad.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            initGridOptions();
            initGridData();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            //Expose Model Methods
            self.cancel = cancel;
            self.save = save;
        }

        function initGridOptions() {
            self.gridOptions = {
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
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 1000, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    //{
                    //    field: 'ProcessCode',
                    //    displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_3'),
                    //    width: 200
                    //},
                    {
                        field: 'ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_4'),
                        width: 200
                    },
                    {
                        field: 'ItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_5'),
                        width: 200
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridDetailApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridDetailApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            var url1 = commonService.getMesApiAddress() + 'SystemManage/DataItemDetail/GetList_DataItemByFather_PDA';
            let postData = {
                EnCode: "PoorWorkReport",
                Remark1: self.currentItem.FactoryCode
            };
            commonService.callWebApiPost(url1, postData).then(function (res1) {
                if ((res1) && (res1.data.success)) {
                    // res1.data.resultData.splice(0, 1);
                    self.gridOptions.data = res1.data.resultData;

                    let queryParmeters = {
                        queryJson: {
                            ProcessCode: self.currentItem.ProcessCode
                        }
                    };
                    var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProcessBadItem/PM_ProcessBadItemPageDataTableList';
                    commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                        if ((res) && (res.data.success)) {
                            var data2 = res.data.resultData.rows;
                            self.gridOptions.data.forEach(item => {
                                if (data2.find(t => t.BadItemCode == item.ItemValue)) {
                                    $scope.gridDetailApi.selection.selectRow(item);
                                }
                            })
                        } else {
                            self.gridOptions.data = res1.data.resultData;
                        }
                    }, function (error) {
                        backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_6'));
                    });

                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_6'));
            });




        }

        function save() {

            var data = $scope.gridDetailApi.selection.getSelectedRows();
            data.forEach((item, index, arr) => {
                item.BadItemCode = item.ItemValue
                item.ProcessCode = self.currentItem.ProcessCode;
            });

            var postData = {
                FactoryCode: self.currentItem.FactoryCode,
                FactoryName: self.currentItem.FactoryName,
                Entity: data
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProcessBadItem/SaveBatchPM_ProcessBadItem';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_7') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_8'));
                //刷新局部
                $rootScope.$emit('to-parentDetail', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_9'));
        }

        //取消
        function cancel() {
            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ProcessBad_ProcessBad';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ProcessBad';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProcessBad-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ProcessBad.selectJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
