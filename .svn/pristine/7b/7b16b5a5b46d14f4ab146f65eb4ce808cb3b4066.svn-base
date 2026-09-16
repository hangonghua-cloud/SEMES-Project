/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 巡检检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-23
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_IPQCDetail').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetail.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', '$rootScope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, $rootScope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //初始化
            init();
            initGridOptions2();
            initGridData2();

            //注册事件
            registerEvents();


            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }


        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.validInputs = false;

            initDictionary();
            //前端按钮事件
            self.save = save;
            self.cancel = cancel;

        }
        function initDictionary() {

            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" }]
            }

            self.typeTestDepartment = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeTestResult = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" }]
            };
            self.typeIsFrozen = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" }]
            };
            commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestDepartment.options = res.data.resultData;
                    self.typeTestDepartment.value = self.typeTestDepartment.options.find(t => t.ItemValue == self.currentItem.TestDepartment);
                }
            })
            commonService.getDataItemDuatil("QualityJudgement").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestResult.options = res.data.resultData;
                    self.typeTestResult.value = self.typeTestResult.options.find(t => t.ItemValue == self.currentItem.TestResult);
                }
            })
            commonService.getDataItemDuatil("FrozenStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeIsFrozen.options = res.data.resultData;
                    self.typeIsFrozen.value = self.typeIsFrozen.options.find(t => t.ItemValue == self.currentItem.IsFrozen);
                }
            })

        }

        function initGridOptions2() {
            self.gridOptionsItem2 = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
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
                    //     name: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_3'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },

                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_4'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_5'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_6'),
                        width: 140
                    },
                    {
                        field: 'TestItemResult',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_7'),
                        cellTemplate: '<div ng-if="row.entity.DataType==1"><sit-numeric  sit-value="row.entity.TestItemResult" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.DataType==2"><sit-text sit-value="row.entity.TestItemResult" ></sit-text></div>' +
                            '<div ng-if="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.TestItemResult"' +
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

        function initGridData2() {

            var postData = {
                queryJson: {
                    FlowCardId: self.currentItem.Id
                }
            }
            var url = commonService.getMesApiAddress("quality") + 'QC_IPQCDetailResult/GetCheckPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    var data = res.data.resultData.rows;
                    data.forEach(item => {

                        if (item.DataType == "1") {
                            item.TestItemResult = parseFloat(item.TestItemResult);
                        }

                        if (item.DataType > 3) {
                            self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_2'), ItemValue: "" }];
                            var arr = item.DataTypeName.split("/");
                            for (var i = 0; i < arr.length; i++) {
                                self.typeSelect.options.push({
                                    ItemName: arr[i], ItemValue: i
                                })
                            }
                            self.typeSelect.value = self.typeSelect.options.find(t => t.ItemName == item.TestItemResult);
                            item.typeResultSelect = angular.copy(self.typeSelect);
                        }
                    });
                    self.gridOptionsItem2.data = data;
                } else {
                    self.gridOptionsItem2.data = []
                }
            })

        }



        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {

            var data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_8'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_9'));
                busyIndicatorService.hide();
                return false;
            }
            var flag = false;
            data.forEach(item => {

                if (item.DataType == "3") { //时间
                    item.TestItemResult = commonService.ConvertToLocalDate(item.TestItemResult);
                }
                // 下拉框
                if (item.DataType > 3 && item.typeResultSelect.value != undefined) {
                    item.TestItemResult = item.typeResultSelect.value.ItemName;
                }

                if (!item.TestItemResult) flag = true;
            })

            if (flag) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_10'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_9'));
                busyIndicatorService.hide();
                return false;
            }


            var postData = {
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                data: data
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_IPQCDetailResult/SaveBatchQC_IPQCDetailResult';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_11') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_12'));
                //刷新局部
                $rootScope.$emit('to-editItem', self.currentItem);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_9'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IPQCDetail.QC_IPQCDetaileditctrl.Tips_9'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_IPQCDetail_QC_IPQCDetail';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_IPQCDetail';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_IPQCDetail-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Edit'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
