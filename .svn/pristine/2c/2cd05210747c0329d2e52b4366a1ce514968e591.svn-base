/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备备件使用情况
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSpareParts.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service', 'i18nService'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService, i18nService) {
        //国际化 
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSpareParts');

            //初始化
            init();
            //初始化grid选项
            initGridOptions();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentSpareParts_EP_EquipmentSpareParts';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增
            self.editButtonHandler = editButtonHandler;//编辑
            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询

            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();



            initDictionary();
            GetStatusDictionary();
        }

        function initDictionary() {
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_1')
                    });
                    initGridData();
                }
            });

            //更换类型
            self.TypeConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_3'),
                        width: 110
                    },
                    {
                        field: 'UseType',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_4'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.UseType==\'1\'"><span ng-cell-text>设备维修</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.UseType==\'2\'"><span ng-cell-text>设备保养</span></div>'
                    },

                    {
                        field: 'EquipmentId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_5'),
                        width: 110
                    },
                    {
                        field: 'EquipmentName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_6'),
                        width: 110
                    },
                    {
                        field: 'SparePartsId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_7'),
                        width: 110
                    },
                    {
                        field: 'SparePartsName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_8'),
                        width: 110
                    },
                    {
                        field: 'MaterialClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_9'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_10'),
                        width: 200
                    },

                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_11'),
                        width: 110
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_12'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },

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

            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_13'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            if (self.TypeConfig.selectedOption != null) {
                self.searchParams.ReplaceType = self.TypeConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.ReplaceType = "";
            }
            if (self.StartDate != null && self.StartDate != "") {
                self.searchParams.StartTime = moment(self.StartDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.StartTime = "";
            }
            if (self.EndDate != null && self.EndDate != "") {
                self.searchParams.EndTime = moment(self.EndDate).format("YYYY-MM-DD");
            }
            else {
                self.searchParams.EndTime = "";
            }

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'CreateTime',//更换类型
                sord: 'desc'
            };

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentSpareParts/GetListWithPage';

            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    //总条数
                    self.gridOptions.totalItems = res.data.resultData.records;
                    //数据
                    self.gridOptions.data = res.data.resultData.rows;
                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_14'));
            });
        }



        //更换类型
        function GetStatusDictionary() {
            var url = commonService.getDataItemDuatil("ReplacementType").then(function (res) {
                self.TypeConfig.options = res.data.resultData;;
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
            var title = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_15');
            var text = commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_16');
            backendService.confirm(text, function () {
                //commonService.getMesApiAddress() = '/sitSrvApi/'
                var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentSpareParts/RemoveEP_EquipmentSpareParts';
                console.log("删除----------------" + url);
                var user = commonService.getLoginUser();
                //self.UserId = user['nameid'];
                self.UserCode = user.loginName;
                self.UserName = user.fullName;
                self.selectedItem.ModifyBy = self.UserCode;
                //提交删除当前选择数据实体
                var postData = {
                    Entity: self.selectedItem
                };
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemoveEP_EquipmentSpareParts----------------------------" + JSON.stringify(res));
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
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_17'));
                    }
                }, function (error) {
                    console.log("RemoveEP_EquipmentSpareParts--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_14'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentSpareParts';
        var moduleStateUrl = 'Siemens.SimaticIT_EquipmentApp_EP_EquipmentSpareParts';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentSpareParts';

        var state = {
            name: moduleStateName + '_EP_EquipmentSpareParts',
            url: '/' + moduleStateUrl + '_EP_EquipmentSpareParts',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/EP_EquipmentSpareParts-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.EquipmentApp.EP_EquipmentSpareParts.EP_EquipmentSparePartslistctrl.Tips_18'
            }
        };
        $stateProvider.state(state);
    }
}());
