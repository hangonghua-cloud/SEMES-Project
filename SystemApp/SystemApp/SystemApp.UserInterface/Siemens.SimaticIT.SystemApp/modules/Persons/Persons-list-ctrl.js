(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.SystemApp.Persons').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.SystemApp.Persons.Persons.service', '$state', '$stateParams', '$rootScope',
        '$scope', 'common.base', 'common.services.logger.service', 'commonService', '$timeout'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, commonService, $timeout) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.SystemApp.Persons.Persons');

            init();
            initGridOptions();
            initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_SystemApp_Persons_Persons';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];
            self.searchParams = {};

            //Expose Model Methods
            self.addButtonHandler = addButtonHandler;
            self.editButtonHandler = editButtonHandler;
            self.deleteButtonHandler = deleteButtonHandler;
            self.searchButtonHandler = searchButtonHandler;
            initDictionary();
            setTimeout(function () {
                //初始化grid数据、查询
                LoadFactory();
            }, 100);//如果查询条件有下拉参数，请调整此值到1000

            // self.SetPermissions = SetPermissions;
            // self.selectButtonHandler = selectButtonHandler;
            // self.doneButtonHandler = doneButtonHandler;
            $rootScope.$on('to-parent', function (event, OnData) {
                // self.selectedItem.PrintId = OnData.PrintId;
                // self.selectedItem.Server = OnData.Server;
                $timeout(function () {
                    initGridData();
                }, 1500);
            });
        }

        function initDictionary() {
            self.typeDeptSelect = {
                selectedOption: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1') },
                options: [{ ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1') }]
            };

            self.typePositionSelect = {
                selectedOption: { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1') },
                options: [{ ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1') }]
            };
            commonService.getDataItemDuatil("Position").then(function (res) {
                if (res && res.data.success) {
                    self.typePositionSelect.options = res.data.resultData;
                    self.typePositionSelect.selectedOption = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
            })

            var url = commonService.getMesApiAddress() + "Base/GetList_Department_Control";
            commonService.callWebApiGet(url).then(function (data) {
                if ((data) && (data.data.success)) {
                    var jsonData = data.data.resultData;
                    self.typeDeptSelect.options = jsonData;
                    self.typeDeptSelect.options.splice(0, 0, { ItemValue: "", ItemName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1') });
                } else {
                    var jsonData = [{
                        Id: "",
                        Server: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_2')
                    }];
                    self.typeDeptSelect = jsonData;
                }
            }, function (error) {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_3'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_4',
                    text: '[' + error.status + '] - ' + error.data.returnMsg
                });
                messageservice.show();
            });
            // //工厂
            // self.typeFactory = {
            //     value: { ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1'), ResourceCode: "" },
            //     options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1'), ResourceCode: "" }]
            // };
            // commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeFactory.options = res.data.resultData;
            //         self.typeFactory.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_1')
            //         });
            //     }
            // });
            //工厂多选
            self.multiSelectFactory = {
                value: "",
                options: []
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    res.data.resultData.forEach(item => {
                        if (item.ResourceCode) {
                            self.multiSelectFactory.options.push({
                                id: item.ResourceCode,
                                name: item.ResourceName
                            });
                        }
                    });
                }
            });
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
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_5'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }, {
                        field: 'Code',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_6'),
                        width: 150
                    }
                    , {
                        field: 'Name',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_7'),
                        width: 150
                    }, {
                        field: 'Sex',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_8'),
                        width: 100
                    }, {
                        field: 'DepartName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_9'),
                        width: 300
                    },
                    // {
                    //     field: 'JobName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_10'),
                    //     width: 200
                    // } , 
                    {
                        field: 'PositionName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_11'),
                        width: 120
                    },
                    {
                        field: 'MobilePhone',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_12'),
                        width: 200
                    },
                    {
                        field: 'FactoryName',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_13'),
                        width: 200
                    },
                    //  {
                    //     field: 'CertificateCode',
                    //     displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_14'),
                    //     width: 200
                    // }, 
                    {
                        field: 'IsEnabled', displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_15'),
                        width: 100,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled==true"><span ng-cell-text class="green">生效</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.IsEnabled!=true"><span ng-cell-text class="red">失效</span></div>'
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_16'),
                        width: 200,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'
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
                                //initGridOperationOptions();
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
            var Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'Name',
                sord: 'asc'
            };
            // if (self.typeDeptSelect.selectedOption && self.typeDeptSelect.selectedOption.ItemValue != "") {
            //     self.searchParams.Department_ID = self.typeDeptSelect.selectedOption.ItemValue;
            // }
            self.searchParams.Position_ID = self.typePositionSelect.selectedOption.ItemValue;
            self.searchParams.FactoryCode = self.multiSelectFactory.value;

            let queryParmeters = {
                pagination: Pagination,
                queryJson: self.searchParams
            };

            var url = commonService.getMesApiAddress() + 'Base/GetList_Person';
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

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_17'));
            });
        }
        //查询
        function searchButtonHandler() {
            initGridData();
        }
        //U9同步
        function doneButtonHandler() {
            commonService.showLoading();
            let url = commonService.apiAdress_mes_integration + "PersonCall/do";
            // let url = commonService.apiAdress_mes_integration + "PersonCall/test";
            commonService.callWebApiGet(url, null).then(function (res) {
                commonService.hideLoading();
                if (res.data == "success") {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_18'));
                }
                else {
                    commonService.showError(res.data);
                }
            }, function (error) {
                commonService.hideLoading();
                commonService.showError('[' + error.status + '] - ' + error.statusText + '-' + error.data);
            });
        }

        function addButtonHandler(clickedCommand) {
            $state.go(rootstate + '.add');
        }
        function SetPermissions(clickedCommand) {
            // console.log("self.selectedItem---------------------------------"+JSON.stringify(self.selectedItem));

            $state.go(rootstate + '.ModifyPermissions', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }
        function editButtonHandler(clickedCommand) {
            if (!self.selectedItem) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_19'));
                return;
            }
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.edit', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function selectButtonHandler(clickedCommand) {
            // TODO: Put here the properties of the entity managed by the service
            $state.go(rootstate + '.select', { id: self.selectedItem.Id, selectedItem: self.selectedItem });
        }

        function deleteButtonHandler(clickedCommand) {

            if (!self.selectedItem) {
                notification.warning(commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_20'));
                return;
            }
            debugger

            var title = commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_21');
            // TODO: Put here the properties of the entity managed by the service
            var text = commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_22');

            backendService.confirm(text, function () {
                var url = commonService.getMesApiAddress() + "Base/DeletePeopleForm?ID=" + self.selectedItem.ID;
                var res = commonService.callWebApiGet(url).then(onDeleteSuccess, onDeleteError);
                self.isButtonVisible = false;
            }, title);
        }

        function onDeleteSuccess(data) {
            if (data.data.success) {

                $timeout(function () {
                    initGridData();
                }, 1500);
            }
            else {
                messageservice.set({
                    buttons: [{
                        id: 'ok',
                        displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_3'),
                        onClickCallback: function () {
                            messageservice.hide();
                        }
                    }],
                    title: 'Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_23',
                    text: '操作失败 - ' + data.data.returnMsg
                });
                messageservice.show();
            }
        }

        function onDeleteError(error) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: commonService.$t('Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_3'),
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: 'Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_23',
                text: '[' + error.status + '] - ' + error.data.returnMsg
            });
            messageservice.show();
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
        var moduleStateName = 'home.Siemens_SimaticIT_SystemApp_Persons';
        var moduleStateUrl = 'Siemens.SimaticIT_SystemApp_Persons';
        var moduleFolder = 'Siemens.SimaticIT.SystemApp/modules/Persons';

        var state = {
            name: moduleStateName + '_Persons',
            url: '/' + moduleStateUrl + '_Persons',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/Persons-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.SystemApp.Persons.Personslistctrl.Tips_25'
            }
        };
        $stateProvider.state(state);
    }
}());
