(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.TeamPerson').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.TeamPerson.TeamPerson.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;
        i18nService.setCurrentLang('zh-cn');
        var langFirst = 'Siemens.SimaticIT.ProductionApp.TeamPerson.';
        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.TeamPerson.TeamPerson');

            init();
            initGridOptions();
            //初始化子表grid选项
            initGridOptionsDetail();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_TeamPerson_TeamPerson';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //子表明细
            self.selectedItemDetail = {};
            self.currentItemDetail = {};
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};

            initDictionary();
            initUserConfig();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            // self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除
            //self.select2ButtonHandler = select2ButtonHandler;//查看

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            self.typeFactoryChange = typeFactoryChange;

        }

        function initDictionary() {

            self.Process = {
                value: { ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" }]
            };
            self.Post = {
                value: { Col2: commonService.$t('customCommon.SelectTips'), Col1: "" },
                options: [{ Col2: commonService.$t('customCommon.SelectTips'), Col1: "" }]
            };
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('customCommon.SelectTips')
                    });
                    initGridData();
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.Process.options = res.data.resultData;
                        self.Process.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('customCommon.SelectTips')
                        });
                    }
                });
            } else {
                self.Process = {
                    value: { ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('customCommon.SelectTips'), ResourceCode: "" }]
                };
            }
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
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
                        name: 'rowNum', displayName: commonService.$t('customCommon.rowNum'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'FactoryName',
                        displayName: commonService.$t(langFirst + 'FactoryName'),//'工厂名称',
                        width: 120
                    },

                    {
                        field: 'ProcessName',
                        displayName: commonService.$t(langFirst + 'ProcessName'),//'工序名称',
                        width: 200
                    },
                    {
                        field: 'PTeamCode',
                        displayName: commonService.$t(langFirst + 'PTeamCode_2'),//'生产小组编码',
                        width: 200
                    },
                    {
                        field: 'PTeamName',
                        displayName: commonService.$t(langFirst + 'PTeamName_2'),//'生产小组名称',
                        width: 200
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
                                //console.log (self.selectedItem);
                                //子表明细关联
                                initGridDataDetail();
                                //岗位
                                commonService.getKeyParameterItem({ ItemCode: self.selectedItem.ProcessCode }).then(function (res) {
                                    if (res && res.data.success) {
                                        self.Post.options = res.data.resultData;
                                        self.Post.options.splice(0, 0, {
                                            Col1: "",
                                            Col2: commonService.$t('customCommon.SelectTips')
                                        });
                                    }
                                });
                            } else {
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                                self.Post = {
                                    value: { Col2: commonService.$t('customCommon.SelectTips'), Col1: "" },
                                    options: [{ Col2: commonService.$t('customCommon.SelectTips'), Col1: "" }]
                                };
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
                sord: 'desc'
            };

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('customCommon.Select.SelectFactory'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.ProcessCode = self.Process.value.ResourceCode;
            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson/PM_TeamPersonPageDataTableList';
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
                backendService.genericError(commonService.$t('customCommon.ErrorGetData'), commonService.$t('customCommon.ErrorGetData'));
            });
        }

        //查询
        function searchButtonHandler() {
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
            var title = commonService.$t('customCommon.Confirm.Title');
            var text = commonService.$t('customCommon.Confirm.Context');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson/RemovePM_TeamPerson';
                console.log("删除----------------" + url);
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
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
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePM_TeamPerson----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, "删除数据出错");
                    }
                }, function (error) {
                    console.log("RemovePM_TeamPerson--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('customCommon.ErrorGetData'));
                });
            }, title);
        }

        //子表操作区
        function initUserConfig() {
            self.UserConfig = {
                id: "userId",
                disableEP: false,
                datasource: [],
                selectedObject: {
                    Code: '',
                    Name: '',
                    ShowName: ''
                },
                limit: 5,
                waitTime: 500,
                placeholder: commonService.$t(langFirst + 'Person_placeholder'),
                attributetodisplay: "ShowName",
                editable: true,
                required: true,
                changeEvent: function (oldValue, newValue) {
                    if (self.UserConfig.selectedObject && self.UserConfig.selectedObject.Code) {
                        self.UserConfig.selectedObject.Code = null;
                    }
                    getUserData(newValue, self.UserConfig);
                }
            };
        }

        //获取用户名称
        function getUserData(name, obj) {

            console.log(name);
            if (!name) {
                obj.datasource = [];
            } else {
                commonService.getUserList(name).then(function (res) {
                    if (res && res.data.success) {
                        obj.datasource = res.data.resultData;

                    }
                }, function (error) {
                    commonService.showError('[' + error.status + '] - ' + commonService.$t('customCommon.ErrorGetData') + error.statusText);
                });
            }

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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
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
                        name: 'rowNum', displayName: commonService.$t('customCommon.rowNum'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'PostCode',
                        displayName: commonService.$t(langFirst + 'PostCode'),
                        width: 200
                    },
                    {
                        field: 'PostName',
                        displayName: commonService.$t(langFirst + 'PostName'),//'岗位名称',
                        width: 200
                    },
                    {
                        field: 'UserCode',
                        displayName: commonService.$t(langFirst + 'UserCode'),//'人员编码',
                        width: 200
                    },
                    {
                        field: 'UserName',
                        displayName: commonService.$t(langFirst + 'UserName'),//'人员名称',
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //分页按钮事件
                    gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                        //调用查询方法
                        initGridDataDetail();
                    });
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {

                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);

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
                sidx: 'CreateTime',//创建时间
                sord: 'desc'
            };


            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.TeamId = self.selectedItem.Id;
            }
            else {
                backendService.genericError(commonService.$t('customCommon.Select.SelectMainData'), commonService.$t('customCommon.Error'));
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson_Items/PM_TeamPerson_ItemsPageDataTableList';
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
                backendService.genericError(commonService.$t('customCommon.ErrorGetData'), commonService.$t('customCommon.ErrorGetData'));
            });
        }

        function add2ButtonHandler() {

            if (!self.selectedItem) {
                backendService.genericError(commonService.$t('customCommon.Select.SelectProdTeam'), commonService.$t('customCommon.ErrorGetData'));
                busyIndicatorService.hide();
                return;
            }

            self.currentItemDetail.TeamId = self.selectedItem.Id;
            self.currentItemDetail.FactoryCode = self.selectedItem.FactoryCode;
            self.currentItemDetail.FactoryName = self.selectedItem.FactoryName;
            self.currentItemDetail.PTeamCode = self.selectedItem.PTeamCode;
            self.currentItemDetail.PostCode = self.Post.value.Col1;
            self.currentItemDetail.PostName = self.Post.value.Col2;
            self.currentItemDetail.UserCode = self.UserConfig.selectedObject.Code;
            self.currentItemDetail.UserName = self.UserConfig.selectedObject.Name;

            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItemDetail
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson_Items/SavePM_TeamPerson_Items';
            busyIndicatorService.show({ message: commonService.$t('customCommon.Saveing') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.selectDetail', { id: self.selectedItemDetail.ID, selectedItem: self.selectedItemDetail });
        }

        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('customCommon.Confirm.Title');
            var text = commonService.$t('customCommon.Confirm.Context');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson_Items/RemovePM_TeamPerson_Items';
                //var url = 'http://localhost:49849/' + 'PM_TeamPerson_Items' + '/RemovePM_TeamPerson_Items'; 
                console.log("删除----------------" + url);
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
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePM_TeamPerson_Items----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        // commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridDataDetail();
                        self.selectedItemDetail = null;
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, "删除数据出错");
                    }
                }, function (error) {

                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('customCommon.ErrorGetData'));
                });
            }, title);
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                self.Post.value = { Col2: commonService.$t('customCommon.SelectTips'), Col1: "" };
                self.UserConfig.datasource = [];
                self.UserConfig.selectedObject = {
                    Code: '',
                    Name: '',
                    ShowName: ''
                };
                //关闭侧边栏
                // sidePanelManager.close();
                // commonService.showInfo('保存成功！');
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                initGridDataDetail();
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('customCommon.ErrorOperate'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('customCommon.ErrorOperate'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_TeamPerson';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_TeamPerson';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/TeamPerson';

        var state = {
            name: moduleStateName + '_TeamPerson',
            url: '/' + moduleStateUrl + '_TeamPerson',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/TeamPerson-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.TeamPerson.Title'
            }
        };
        $stateProvider.state(state);
    }
}());
