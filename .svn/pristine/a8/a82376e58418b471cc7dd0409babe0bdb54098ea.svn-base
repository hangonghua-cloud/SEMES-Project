(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.ProductionFirstInspection.service', '$state', '$stateParams',
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
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.ProductionFirstInspection');

            init();
            initGridOptions();
            initGridOptionsDetail();

        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_ProductionApp_ProductionFirstInspection_ProductionFirstInspection';
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
            self.isDetailButtonVisible = false;
            self.viewerOptions2 = {};
            self.viewerData2 = [];
            self.searchParams2 = {};
            initDictionary();

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;//新增

            self.selectButtonHandler = selectButtonHandler;//查看//子表/明细//关联
            self.deleteButtonHandler = deleteButtonHandler;//删除
            self.searchButtonHandler = searchButtonHandler;//查询
            self.labButtonHandler = labButtonHandler;//实验室
            self.qualityButtonHandler = qualityButtonHandler;//质量
            self.confirmButtonHandler = confirmButtonHandler;//车间主任确认


            //子明细
            self.add2ButtonHandler = add2ButtonHandler;//新增
            self.edit2ButtonHandler = edit2ButtonHandler;//编辑
            self.delete2ButtonHandler = delete2ButtonHandler;//删除

            self.typeFactoryChange = typeFactoryChange;
            self.ProcessChange = ProcessChange;
        }
        function initDictionary() {

            //实验室状态
            self.LaboratoryConfig = {
                value: null,
                selectedOption: null,
                options: []
            };
            //判定状态
            self.DeterminationConfig = {
                value: null,
                selectedOption: null,
                options: []
            };


            //工序
            self.ProcessConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" }]
            };


            self.TestMachineConfig = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" }]
            };

            self.typeFirstTest = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_2'), ItemValue: "2" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_3'), ItemValue: "1" },
                ]
            };

            // commonService.getResourceExtendInfo({ LevelCode: "Process" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.ProcessConfig.options = res.data.resultData;
            //         self.ProcessConfig.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1')
            //         });
            //     }
            // });


            commonService.getDataItemDuatil("LaboratoryStatus").then(function (res) {
                self.LaboratoryConfig.options = res.data.resultData;;
            });

            commonService.getDataItemDuatil("ComprehensiveJudgement").then(function (res) {
                self.DeterminationConfig.options = res.data.resultData;;
            });

            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.Factory.options = res.data.resultData;
                    self.Factory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1')
                    });
                }
            });
            //车间确认状态
            self.typeWorkShop = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ItemValue: "" },
                options: [
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ItemValue: "" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_4'), ItemValue: "0" },
                    { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_5'), ItemValue: "1" },
                ]
            }

            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1')
                    });
                    initGridData();
                }
            });
        }

        function typeFactoryChange(oldItem, newItem) {
            if (newItem.ResourceCode) {
                commonService.getProcessByFactory({ LevelCode: newItem.ResourceCode }).then(function (res) {
                    if (res && res.data.success) {
                        self.ProcessConfig.options = res.data.resultData;
                        self.ProcessConfig.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1')
                        });
                    }
                });
            } else {
                self.ProcessConfig = {
                    value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" },
                    options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1'), ResourceCode: "" }]
                };
            }
        }

        function ProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.TestMachineConfig.options = res.data.resultData;
                    self.TestMachineConfig.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_1')
                    });
                }
            });
        }

        $rootScope.$on("to-parent", function (event, data) {
            initGridData();
        })
        $rootScope.$on("to-parentDetail", function (event, data) {
            initGridDataDetail();
        })

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
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'InspectClass',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_8'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.InspectClass==\'2\'"><span ng-cell-text>质量首检</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.InspectClass==\'1\'"><span ng-cell-text>车间首检</span></div>'
                    },

                    {
                        field: 'FirstProcessName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_11'),
                        width: 100
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_12'),
                        width: 110
                    },
                    // {
                    //     field: 'WorkOrder',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_13'),
                    //     width: 200
                    // },
                    {
                        field: 'ExeWorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_14'),
                        width: 110
                    },
                    {
                        field: 'OrderType',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_15'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'1\'"><span ng-cell-text>正常工单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'2\'"><span ng-cell-text>补料单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'3\'"><span ng-cell-text>拣余单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'4\'"><span ng-cell-text>免产单</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.OrderType==\'5\'"><span ng-cell-text>超产品单</span></div>'
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_21'),
                        width: 100
                    },
                    {
                        field: 'FirstMachineName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_22'),
                        width: 110
                    },
                    {
                        field: 'MaterialCode',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_23'),
                        width: 120
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_24'),
                        width: 130
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_25'),
                        width: 110
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_26'),
                        width: 110
                    },

                    {
                        field: 'LaboratoryTestStatusName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_27'),
                        width: 110
                    },
                    {
                        field: 'DeterminationName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_28'),
                        width: 110
                    },
                    // {
                    //     field: 'Creator',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_29'),
                    //     width: 120
                    // },
                    {
                        field: 'FirstUser',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_30'),
                        width: 100
                    },
                    {
                        field: 'FirstTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_31'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'SecondMarkName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_32'),
                        width: 110
                    },
                    {
                        field: 'SecondUser',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_33'),
                        width: 110
                    },
                    {
                        field: 'SecondTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_34'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'Remarks',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_35'),
                        width: 160,
                    },

                    {
                        field: 'Attachment',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_36'),
                        width: 160,
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
                sidx: 'FirstTime',//车间首检时间
                sord: 'DESC'
            };
            if (!self.typeFactory.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_37'));
                return;
            }
            self.searchParams.FactoryCode = self.typeFactory.value.ResourceCode;
            self.searchParams.InspectClass = self.typeFirstTest.value.ItemValue;

            if (self.ProcessConfig.value != null) {
                self.searchParams.FirstProcessCode = self.ProcessConfig.value.ResourceCode;
            }
            else {
                self.searchParams.FirstProcessCode = "";
            }

            if (self.TestMachineConfig.value != null) {
                self.searchParams.FirstMachine = self.TestMachineConfig.value.ResourceCode;
            }
            else {
                self.searchParams.FirstMachine = "";
            }

            if (self.DeterminationConfig.selectedOption != null) {
                self.searchParams.Determination = self.DeterminationConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.Determination = "";
            }


            if (self.LaboratoryConfig.selectedOption != null) {
                self.searchParams.LaboratoryTestStatus = self.LaboratoryConfig.selectedOption.ItemValue;
            }
            else {
                self.searchParams.LaboratoryTestStatus = "";
            }

            if (self.StartTime && self.EndTime) {
                self.searchParams.StartTime = commonService.ConvertToLocalTime(self.StartTime);
                self.searchParams.EndTime = commonService.ConvertToLocalTime(self.EndTime);
            } else {
                self.searchParams.StartTime = "";
                self.searchParams.EndTime = "";
            }

            self.searchParams.SecondMark = self.typeWorkShop.value.ItemValue;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspection/PM_ProductionFirstInspectionPageDataTableList';

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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
            });
        }

        //查询
        function searchButtonHandler() {
            initGridData();
            // initGridDataDetail();
        }

        //新增
        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }

        // //编辑
        // function editButtonHandler(clickedCommand) {
        //     // TODO: Put here the properties of the entity managed by the service
        //     $state.go(rootstate + '.edit', { id: self.selectedItem.ID, selectedItem: self.selectedItem });
        // }

        //查看/明细/子表//绑定
        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }


        //删除 事件
        function deleteButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_39');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_40');
            backendService.confirm(text, function () {
                if (self.selectedItem.SecondMark == "1") {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_41'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_42'));
                    return;
                }
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspection/RemovePM_ProductionFirstInspection';
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
                console.log("new postData------------------------------------" + JSON.stringify(postData));
                commonService.callWebApiPost(url, postData).then(function (res) {
                    console.log("RemovePM_ProductionFirstInspection----------------------------" + JSON.stringify(res));
                    if ((res) && (res.data.success)) {
                        var resultData = res.data.resultData;
                        //成功
                        commonService.showInfo(res.data.returnMsg);
                        //重新刷新列表
                        initGridData();
                        self.selectedItem = null;
                        self.isButtonVisible = false;
                        self.gridApiDetail.data = [];
                        self.isDetailButtonVisible = false;
                    } else {
                        //失败
                        commonService.showWarning(res.data.returnMsg);
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_44'));
                    }
                }, function (error) {
                    console.log("RemovePM_ProductionFirstInspection--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
                });
            }, title);
        }

        //车间主任确认
        function confirmButtonHandler(clickedCommand) {

            if (self.selectedItem.SecondMark == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_45'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_46'));
                return;
            }
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspection/WorkShopConfirm';
            //提交当前选择数据实体
            var postData = {
                KeyValue: self.selectedItem.Id,
                secondResult: "1"
            };
            commonService.callWebApiPost(url, postData).then(function (res) {
                if ((res) && (res.data.success)) {
                    //成功
                    commonService.showInfo(res.data.returnMsg);
                    //重新刷新列表
                    initGridData();
                    self.selectedItem = null;
                    self.isButtonVisible = false;
                    self.gridOptionsDetail.data = [];
                    self.isDetailButtonVisible = false;
                } else {
                    //失败
                    commonService.showWarning(res.data.returnMsg);
                }
            }, function (error) {
                backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
            });
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_6'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'TestDepartmentName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_47'),
                        width: 200,
                        // cellTemplate:
                        //     '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                        //     '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_51'),
                        width: 160
                    },
                    {
                        field: 'DataTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_52'),
                        width: 160
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_53'),
                        width: 160
                    },
                    {
                        field: 'QualityResult',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_54'),
                        width: 160,
                    },
                    {
                        field: 'CreatorName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_55'),
                        width: 200
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_56'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilterMM'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
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
                                //console.log (self.selectedItemDetail);
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

        //实验室录入
        function labButtonHandler(clickedCommand) {
            if (self.selectedItem.LaboratoryTestStatus == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_57'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
                return;
            }

            if (self.selectedItem.LaboratoryTestStatus == "") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_57'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
                return;
            }
            if (self.selectedItem.SecondMark == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_41'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_58'));
                return;
            }

            $state.go(rootstate + '.add', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //质量判定
        function qualityButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            if (self.selectedItem.LaboratoryTestStatus == "2") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_59'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
                return;
            }
            if (self.selectedItem.SecondMark == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_41'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_58'));
                return;
            }

            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        //子表查询方法,数据绑定
        function initGridDataDetail() {
            self.selectedItemDetail = null;
            self.isDetailButtonVisible = false;
            let Pagination = {
                rows: self.gridOptionsDetail.paginationPageSize,
                page: self.gridOptionsDetail.paginationCurrentPage,
                sidx: 'TestItemCoading',//首检项目编码
                sord: 'asc'
            };

            if (self.selectedItem != null) {
                //关联字段
                self.searchParams2.FirstInspectionId = self.selectedItem.Id;
            }
            else {
                self.gridApiDetail.data = [];
                return;
            }

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams2
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspectionDetail/PM_ProductionFirstInspectionDetailPageDataTableList';
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
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
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

        //编辑项目
        function edit2ButtonHandler(clickedCommand) {
            if (self.selectedItem.SecondMark == "1") {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_41'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_58'));
                return;
            }
            $state.go(rootstate + '.editDetail', { id: self.selectedItemDetail.Id, selectedItem: self.selectedItemDetail });
        }



        //删除 事件
        function delete2ButtonHandler(clickedCommand) {
            var title = commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_39');
            var text = commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_40');
            backendService.confirm(text, function () {
                if (self.selectedItem.SecondMark == "1") {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_60'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_42'));
                    return;
                }
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspectionDetail/RemovePM_ProductionFirstInspectionDetail';
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
                    console.log("RemovePM_ProductionFirstInspectionDetail----------------------------" + JSON.stringify(res));
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
                        console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                        //backendService.genericError('[' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_44'));
                    }
                }, function (error) {
                    console.log("RemovePM_ProductionFirstInspectionDetail--error--------------------------" + JSON.stringify(error));
                    backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_38'));
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
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_ProductionFirstInspection';
        var moduleStateUrl = 'Siemens.SimaticIT_ProductionApp_ProductionFirstInspection';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ProductionFirstInspection';

        var state = {
            name: moduleStateName + '_ProductionFirstInspection',
            url: '/' + moduleStateUrl + '_ProductionFirstInspection',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ProductionFirstInspection-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.JS.Tips_61'
            }
        };
        $stateProvider.state(state);
    }
}());
