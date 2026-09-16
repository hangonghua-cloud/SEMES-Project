/**
*  0. 代码生成： UA主子表一键生成前后端html、JS、API接口代码生成器 Ver 1.11 更新日期：2021-07-14  设计者：刘万军
*  1. 功能描述： 物料属性模板维护
*  2. 创建人员： liyongguo
*  3. 创建日期： 2021-07-21
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialBindTemp').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemp.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemp');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
            setTimeout(function () {
                //初始化grid数据、查询
                initGridData();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000
        }

        //初始化
        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_MaterialBindTemp_MaterialBindTemp';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};
            //子表明细
            self.selectedItemDetail = null;
            self.isButtonVisible2 = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
        }
        $rootScope.$on('to-parent', function (event, OnData) {
            initGridData();
            initGridDataDetail();
        });
        $rootScope.$on('to-parentdetail', function (event, OnData) {
            initGridDataDetail();
        });
        //初始化grid选项
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
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TempCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_2'),
                        width: 140
                    },
                    {
                        field: 'TempName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_3'),
                        width: 140
                    },
                    {
                        field: 'Sort',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'Remark',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_5'),
                        width: 200
                    },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_6'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsEnabled==1">生效</div>' +
                            '<div class="ngCellText" style="padding-left:5px;height:30px;line-height:30px;" ng-if="row.entity.IsEnabled==0">失效</div>'
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_7'),
                        width: 140
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_8'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_9'),
                        width: 180
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_10'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridData();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItem = row.entity;
                                self.isButtonVisible = true;
                                //子表明细关联
                                initGridDataDetail();
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initGridData() {
            self.selectedItem = null;
            self.isButtonVisible = false;
            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'TempCode',//模板编码
                sord: 'asc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/Base_MaterialBindTempPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];

                }
                self.gridOptionsDetail.data = [];
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_11'));
            });
        }

        //查询
        function searchButtonHandler() {
            self.gridOptionsDetail.data = [];
            initGridData();
            //initGridDataDetail();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        //编辑
        function editButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            debugger
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_12');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_13');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/RemoveBase_MaterialBindTemp';

                var user = commonService.getLoginUser();

                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.UpdateByCode = self.UserCode;
                self.selectedItem.UpdateByName = self.UserCode + '-' + self.UserName;
                if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                    self.selectedItem.UpdateByName = self.UserCode;
                }
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                self.gridOptionsDetail.data = [];
                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        commonService.showInfo(res.data.returnMsg);
                        initGridData();

                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_14'));
                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_11'));
                });
            }, title);
        }

        //初始化子表grid选项
        function initGridOptionsDetail() {
            self.gridOptionsDetail = {
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
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 30, 50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 20, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_1'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    // {
                    //     field: 'MateriaBindTempId', 
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_15'),
                    //     width: 200
                    // },
                    // {
                    //     field: 'TempName', 
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_3'),
                    //     width: 200
                    // },
                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_17'),
                        width: 120
                    },
                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_18'),
                        width: 120
                    },
                    // {
                    //     field: 'UpperLimit',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_19'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'LowerLimit',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_20'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'Length',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_21'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'DefaultValue',
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_22'),
                    //     width: 100
                    // },
                    // {
                    //     field: 'IsEnabled', 
                    //     displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_23'),
                    //     width: 80,
                    //     cellTemplate: '<div><input type="checkbox" ng-disabled="true" ng-model="row.entity.IsEnabled"/></div>'
                    // },
                    {
                        field: 'Sort',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_4'),
                        width: 120
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_8'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_9'),
                        width: 180
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_10'),
                        width: 180,
                        type: 'date',
                        cellFilter: 'date:"yyyy-MM-dd HH:mm:ss"'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {

                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;

                                //子表明细关联
                                // initGridDataDetail();
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

        //子表查询方法,数据绑定
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'Sort',//属性编码
                sord: 'asc'
            };

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.MateriaBindTempId = self.selectedItem.Id;
            }
            else {
                // backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_24'), commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_25'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/Base_MaterialBindTempFacetPageDataTableList';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {

                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptionsDetail.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptionsDetail.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_11'));
            });
        }

        //查询
        function search2ButtonHandler() {
            initGridDataDetail();
        }

        //新增
        function add2ButtonHandler(clickedCommand) {
            $state.go(rootstate + '.addDetail', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        }

        //编辑
        function edit2ButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_12');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_13');
            backendService.confirm(text, function () {

                var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/RemoveBase_MaterialBindTempFacet';

                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItemDetail.UpdateByCode = self.UserCode;
                self.selectedItemDetail.UpdateByName = self.UserCode + '-' + self.UserName;
                if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                    self.selectedItemDetail.UpdateByName = self.UserCode;
                }
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItemDetail
                };

                commonService.callWebApiPost(url, postData).then(function (res) {

                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);

                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_11'));
                });
            }, title);
        }

        function onGridItemSelectionChanged(items, item) {
            if (item && item.selected == true) {
                self.selectedItem = item;
                setButtonsVisibility(true);
            } else {
                self.selectedItem = null;
                setButtonsVisibility(false);
            }
        }
        // Internal function to make item-specific buttons visible
        function setButtonsVisibility(visible) {
            self.isButtonVisible = visible;
        }
    }

    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialBindTemp';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_MaterialBindTemp';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialBindTemp';

        var state = {
            name: moduleStateName + '_MaterialBindTemp',
            url: '/' + moduleStateUrl + '_MaterialBindTemp',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/MaterialBindTemp-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialBindTemp.MaterialBindTemplistctrl.Tips_26'
            }
        };
        $stateProvider.state(state);
    }
}());
