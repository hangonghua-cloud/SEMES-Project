(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.ProductionFirstInspection.service',
        '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication',
        'common.widgets.notificationTile.globalService', 'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGridOptions();
            initGridData();
            registerEvents();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            initDictionary();
            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
        }
        function initDictionary() {
            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_2'), ItemValue: "" }]
            }
        }

        function initGridOptions() {
            self.gridOptionsItem = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                paginationCurrentPage: 1, //当前的页码  
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
                    //     name: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_3'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_4'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_5'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_6'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_7'),
                        width: 200
                    },
                    // {
                    //     field: 'DataTypeName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_8'),
                    //     width: 140
                    // },
                    {
                        field: 'TestItemResult',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_9'),
                        cellTemplate: '<div ng-show="row.entity.DataType==1"><sit-numeric  sit-value="row.entity.QualityResult" ></sit-numeric></div>' +
                            '<div ng-show="row.entity.DataType==2"><sit-text sit-value="row.entity.QualityResult" ></sit-text></div>' +
                            '<div ng-show="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.QualityResult"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>' +
                            '<div ng-show="row.entity.DataType>3"><sit-select sit-value="row.entity.typeResultSelect.value"' +
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

            let Pagination = {
                sidx: 'TestItemCoading',//首检项目编码
                sord: 'asc'
            };

            var postdata2 = {
                pagination: {
                    rows: self.gridOptionsItem.paginationPageSize,
                    page: self.gridOptionsItem.paginationCurrentPage,
                    sidx: 'TestItemCoading',//首检项目编码
                    sord: 'asc'
                },

                queryJson: {
                    FirstInspectionId: self.currentItem.Id,
                    TestDepartment: "1"
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
                            self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_2'), ItemValue: "" }];
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

        //编辑保存
        function save() {

            var itemvalueisnull = "";
            var data = self.gridOptionsItem.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_10'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
                busyIndicatorService.hide();
                return false;
            }

            var flag = false;

            data.forEach(item => {

                if (item.DataType == "3") { //时间
                    item.QualityResult = commonService.ConvertToLocalTime(item.QualityResult);
                }
                // 下拉框
                if (item.DataType > 3 && item.typeResultSelect.value != undefined) {
                    item.QualityResult = item.typeResultSelect.value.ItemName;
                }

                // if (!item.TestDepartment) flag = true;

                if (item.QualityResult == undefined) {

                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_12'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
                    itemvalueisnull = 1;
                } else if (item.QualityResult == commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_2')) {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_12'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
                    itemvalueisnull = 1;
                }
                else if (item.QualityResult == '') {
                    backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_12'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
                    itemvalueisnull = 1;
                }
            });

            if (itemvalueisnull == '1') {
                return false;
            }

            // if (flag) {
            //     backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_13'), commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
            //     busyIndicatorService.hide();
            //     return false;
            // }

            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                data: data
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_ProductionFirstInspectionDetail/SavePM_ProductionFirstInspectionDetail1';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            busyIndicatorService.hide();
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
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
            // console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_15'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_11'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ProductionFirstInspection_ProductionFirstInspection';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ProductionFirstInspection';

        var state = {
            name: screenStateName + '.add',
            url: '/add/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionFirstInspection-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.ProductionFirstInspection.addJS.Tips_1'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
