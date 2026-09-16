(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.ProductionFirstInspection.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGridOptions();
            initGridData();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_1'));
            sidePanelManager.open(
                {
                    mode: 'e',
                    size: 'wide'
                }
            );
        }


        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;
            self.isQualityButtonVisible = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;

            initDictionary();
        }

        function initDictionary() {
            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_2'), ItemValue: "" }]
            }
            self.DeterminationConfig = {
                selectedOption: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_2'), ItemValue: "" }]
            }

            commonService.getDataItemDuatil("ComprehensiveJudgement").then(function (res) {
                self.DeterminationConfig.options = res.data.resultData;
                self.DeterminationConfig.selectedOption = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
            });

        }


        function initGridOptions() {
            self.gridOptionsItem = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                paginationCurrentPage: 1,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    // {
                    //     name: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_3'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_4'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_5'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_6'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_7'),
                        width: 200
                    },
                    // {
                    //     field: 'DataTypeName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_8'),
                    //     width: 140
                    // },
                    {
                        field: 'QualityResult',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_9'),
                        cellTemplate: '<div ng-if="row.entity.DataType==1"><sit-numeric  sit-value="row.entity.QualityResult" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.DataType==2"><sit-text sit-value="row.entity.QualityResult" ></sit-text></div>' +
                            '<div ng-if="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.QualityResult"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>' +
                            '<div ng-if="row.entity.DataType>3"><sit-select sit-value="row.entity.typeResultSelect.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeResultSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                },
                data: []
            };
        }

        function initGridData() {

            var postdata2 = {

                pagination: {
                    rows: self.gridOptionsItem.paginationPageSize,
                    page: self.gridOptionsItem.paginationCurrentPage,

                    sidx: 'TestItemCoading',//首检项目编码
                    sord: 'asc'
                },

                queryJson: {
                    FirstInspectionId: self.currentItem.Id,
                    TestDepartment: "2"
                }
            }
            var url1 = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspectionDetail/PM_ProductionFirstInspectionDetailPageDataTableList';

            commonService.callWebApiPost(url1, postdata2).then(function (result) {
                var data = result.data.resultData.rows;
                if (result && result.data.success) {
                    data.forEach(item => {

                        if (item.DataType == "1") item.QualityResult = parseFloat(item.QualityResult);
                        else if (item.DataType == "3") item.QualityResult = new Date(item.QualityResult);
                        else if (item.DataType > 3) {
                            self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_2'), ItemValue: "" }];
                            var arr = item.DataTypeName.split("/");
                            for (var i = 0; i < arr.length; i++) {
                                self.typeSelect.options.push({
                                    ItemName: arr[i], ItemValue: i
                                })
                            }
                            if (!!item.QualityResult) {
                                self.typeSelect.value = self.typeSelect.options.find(t => t.ItemName == item.QualityResult);
                            }
                            item.typeResultSelect = angular.copy(self.typeSelect);
                        }

                    });
                    self.gridOptionsItem.data = data;
                }
            })
        }


        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            var itemvalueisnull = "";
            var data = self.gridOptionsItem.data;


            if (self.DeterminationConfig.selectedOption != null) {
                self.currentItem.Determination = self.DeterminationConfig.selectedOption.ItemValue;
            }
            else {
                self.currentItem.Determination = "";
            }
            var flag = false;
            data.forEach(item => {

                if (item.DataType == "3") { //时间
                    item.QualityResult = commonService.ConvertToLocalTime(item.QualityResult);
                }

                // if(item.DataType=="2")
                // {
                //   item.QualityResult = commonService.commonService

                // }
                // 下拉框
                if (item.DataType > 3 && item.typeResultSelect.value != undefined) {
                    item.QualityResult = item.typeResultSelect.value.ItemName;
                }
                if (!item.QualityResult) flag = true;

                if (item.QualityResult == undefined) {

                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_10'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_11'));
                    itemvalueisnull = 1;
                } else if (item.QualityResult == commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_2')) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_10'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_11'));
                    itemvalueisnull = 1;
                }

            });

            if (itemvalueisnull == '1') {
                return false;
            }

            // if (flag) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_12'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_11'));
            //     busyIndicatorService.hide();
            //     return false;
            // }

            // busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_13') });

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                List: data
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspectionDetail/SavePM_ProductionFirstInspectionDetail2';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            busyIndicatorService.hide();
        }

        //取消
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
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_14'));

                $rootScope.$emit('to-parentDetail', self.currentItem);
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_11'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ProductionFirstInspection_ProductionFirstInspection';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ProductionFirstInspection';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionFirstInspection-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.editeJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
