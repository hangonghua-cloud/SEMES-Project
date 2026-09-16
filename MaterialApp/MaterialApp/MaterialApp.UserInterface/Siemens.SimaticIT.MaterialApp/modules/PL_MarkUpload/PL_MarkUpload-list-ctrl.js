/**
*  0. 代码生成： UADM单表一键生成前后端html、JS、API接口代码生成器 Ver 2.01 发布日期：2021-04-18  设计师：刘万军
*  1. 功能描述： 唛头配置
*  2. 创建人员： jpf
*  3. 创建日期： 2022-12-03
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.PL_MarkUpload').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUpload.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUpload');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_PL_MarkUpload_PL_MarkUpload';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];



            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//搜索
            //自定义添加快速查询框
            AddSortSearch_txt(self);
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
        }

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
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        field: 'MarkCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_1'),
                        width: 170
                    },
                    {
                        field: 'MarkName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_2'),
                        width: 170
                    },
                    {
                        field: 'IsEnabled',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_3'),
                        width: 170,
                        cellTemplate: '<div><input type="checkbox" ng-disabled="true" ng-model="row.entity.IsEnabled"/></div>'
                    },
                    {
                        field: 'Creator',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_4'),
                        width: 170
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_5'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    },
                    {
                        field: 'ModifyBy',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_6'),
                        width: 170
                    },
                    {
                        field: 'ModifyTime',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_7'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
                    }
                ],
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
                                //console.log (self.selectedItem);
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
                sidx: 'CreateTime',//创建时间
                sord: 'asc'
            };



            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress("material") + 'PL_MarkUpload/PL_MarkUploadPageDataTableList';
            //var url = 'http://localhost:49849/' + 'BS_TraitManage' + '/BS_TraitManagePageDataTableList'; 
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_8'));
            });
        }
        function searchButtonHandler(clickedCommand) {
            initGridData();
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
            var title = commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_9');
            var text = commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_10');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("material") + 'PL_MarkUpload/DeletePL_MarkUpload';
                console.log("删除----------------" + url);
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];

                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePL_MarkUpload----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        initGridData();
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_11'));
                    }
                }, function (error) {
                    console.log("RemovePL_MarkUpload--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_8'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_PL_MarkUpload';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_PL_MarkUpload';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/PL_MarkUpload';

        var state = {
            name: moduleStateName + '_PL_MarkUpload',
            url: '/' + moduleStateUrl + '_PL_MarkUpload',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/PL_MarkUpload-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.PL_MarkUpload.PL_MarkUploadlistctrl.Tips_12'
            }
        };
        $stateProvider.state(state);
    }
}());
