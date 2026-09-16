(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW').config(SeqScreenStateConfig);

    SeqScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.ExeWorkOrderSW.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function SeqScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            // debugger;
            init();
            initDictionary();
            initSeqGridOptions();
            registerEvents();

            sidePanelManager.setTitle("派工顺序调整");
            //sidePanelManager.open('e');
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            self.selectedSeqItem = {};
            self.validInputs = false;

            //Expose Model Methods
            self.cancel = cancel;
            self.search = search;
            self.moveUp = moveUp;
            self.moveDown = moveDown;
            self.typeFactoryChange = typeFactoryChange;
            self.typeProcessChange = typeProcessChange;
        }

        function initDictionary() {
            //工厂
            self.typeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1'), ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.AddJS.Tips_1')
                    });
                }
            });

            self.typeProcess = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeMachine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2'), ResourceCode: "" }]
            };
        }

        //工厂变更
        function typeFactoryChange(oldItem, newItem) {

            let data = {
                factoryCode: newItem.ResourceCode,
                fieldCode: "PGGX",
                fieldValue: "JC"
            };

            commonService.getProcessByFactoryExtendInfo(data).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcess.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeProcess.value = res.data.resultData[0];
                        self.typeProcess.options.splice(0, 0, {
                            ResourceCode: "",
                            ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2')
                        });
                    }
                    else {
                        self.typeProcess = {
                            value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2'), ResourceCode: "" },
                            options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2'), ResourceCode: "" }]
                        };
                    }
                }
            });
        }

        //工序改变
        function typeProcessChange(oldItem, newItem) {
            commonService.getResourceListByParentResource({ ParentResource: newItem.ResourceCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeMachine.options = res.data.resultData;
                    self.typeMachine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_2')
                    });
                }
            });
        }

        function initSeqGridOptions() {
            self.gridSeqOptions = {
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
                enablePaginationControls: false, //使用默认的底部分页
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: false,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: false, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
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
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_5'), width: 70, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },
                    {
                        field: 'CreateTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_6'),
                        width: 160,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'PlanProductTime',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_7'),
                        width: 140,
                        type: 'date',
                        cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                    },
                    {
                        field: 'WorkOrderTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_8'),
                        width: 100
                    },
                    {
                        field: 'ProductOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_9'),
                        width: 120
                    },
                    {
                        field: 'WorkOrder',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_10'),
                        width: 150
                    },
                    {
                        field: 'ContainerNO',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_11'),
                        width: 100
                    },
                    {
                        field: 'SWStatus',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_12'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.SWStatus==\'1\'"><span ng-cell-text>未开工</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.SWStatus==\'2\'"><span ng-cell-text>正在生产</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.SWStatus==\'3\'"><span ng-cell-text>已完成</span></div>'
                    },
                    {
                        field: 'MMXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_16'),
                        width: 120
                    },
                    {
                        field: 'MMCJ',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_17'),
                        width: 100
                    },
                    {
                        field: 'SmallClassName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_18'),
                        width: 100
                    },
                    {
                        field: 'Spec',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_19'),
                        width: 150
                    },
                    {
                        field: 'BWXH',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_20'),
                        width: 100
                    },
                    {
                        field: 'UV',
                        displayName: 'UV',
                        width: 100
                    },
                    {
                        field: 'KCKX',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_21'),
                        width: 100
                    },
                    {
                        field: 'OrderPieces',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_22'),
                        width: 100
                    },
                    {
                        field: 'ActualSheets',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_23'),
                        width: 120
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedSeqItem = row.entity;
                                self.isButtonVisible = true;
                                //console.log (self.selectedItem);
                            } else {
                                self.selectedSeqItem = null;
                                self.isButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        //查询方法,数据绑定
        function initSeqGridData() {
            if (!self.typeMachine.value.ResourceCode) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_24'));
                return;
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_25') });
            let queryParmeters = {
                queryJson: {
                    ProcessCode: self.typeProcess.value.ResourceCode,
                    EquipCode: self.typeMachine.value.ResourceCode
                }
            };
            console.log('queryParmeters-----' + JSON.stringify(queryParmeters));
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/PM_ExeWorkOrderSWPageDataTableList';
            //var url = 'http://localhost:49849/' + 'ProductRule' + '/PL_ProductRulePageDataTableList'; 
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                //console.log ('-self.Post_ResultData----------------------' + JSON.stringify(res));
                busyIndicatorService.hide();
                if ((res) && (res.data.success)) {
                    //数据
                    self.gridSeqOptions.data = res.data.resultData.rows;
                } else {
                    self.gridSeqOptions.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_26'));
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //查询
        function search() {
            initSeqGridData();
        }

        //上移
        function moveUp() {

            //排序
            self.gridSeqOptions.data.sort(function (a, b) {
                a.SWSeq - b.SWSeq;
            })

            let selectedIndex = self.gridSeqOptions.data.indexOf(self.selectedSeqItem);
            if (selectedIndex <= 0) {
                return;
            }
            let selectedSeq = self.selectedSeqItem.SWSeq;
            let upSeq = self.gridSeqOptions.data[selectedIndex - 1].SWSeq;

            self.gridSeqOptions.data[selectedIndex].SWSeq = upSeq;
            self.gridSeqOptions.data[selectedIndex - 1].SWSeq = selectedSeq;


            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                Entity: self.gridSeqOptions.data
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/UpdateBatchPM_ExeWorkOrderSW';
            console.log("url----------------" + url);
            //提交数据
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_27') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("UpdateBatchPM_ExeWorkOrderSW----------------------" + JSON.stringify(req));
            busyIndicatorService.hide();
        }

        //下移
        function moveDown() {

            //排序
            self.gridSeqOptions.data.sort(function (a, b) {
                a.SWSeq - b.SWSeq;
            })

            let selectedIndex = self.gridSeqOptions.data.indexOf(self.selectedSeqItem);
            if (selectedIndex == self.gridSeqOptions.data.length - 1) {
                return;
            }
            let selectedSeq = self.selectedSeqItem.SWSeq;
            let downSeq = self.gridSeqOptions.data[selectedIndex + 1].SWSeq;

            self.gridSeqOptions.data[selectedIndex].SWSeq = downSeq;
            self.gridSeqOptions.data[selectedIndex + 1].SWSeq = selectedSeq;


            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                Entity: self.gridSeqOptions.data
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ExeWorkOrderSW/UpdateBatchPM_ExeWorkOrderSW';
            console.log("url----------------" + url);
            //提交数据
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_27') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("UpdateBatchPM_ExeWorkOrderSW----------------------" + JSON.stringify(req));
            busyIndicatorService.hide();
        }

        //下移
        function cancel() {

            //关闭侧边栏
            sidePanelManager.close();
            //返回列表(父页面)
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {

            if (data.data.success) {
                busyIndicatorService.hide();
                // commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_28'));
                //刷新局部
                initSeqGridData();
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_29'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_29'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    SeqScreenStateConfig.$inject = ['$stateProvider'];
    function SeqScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ExeWorkOrderSW_ExeWorkOrderSW';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ExeWorkOrderSW';

        var state = {
            name: screenStateName + '.seq',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ExeWorkOrderSW-seq.html',
                    controller: SeqScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ExeWorkOrderSW.seqJS.Tips_1'
            },
            params: {

            }
        };
        $stateProvider.state(state);
    }
}());
