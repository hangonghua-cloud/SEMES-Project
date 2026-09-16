(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.OQCManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfig.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth,
        notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGrid();
            initGridData();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = null;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

        }

        function initGrid() {
            self.gridOptions = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                useExternalPagination: false,//true:使用外部分页方式；false:使用UI Grid内部分页方式
                useExternalSorting: false,//true:使用外部排序方式，false:使用UI Gird内部排序方式
                multiSelect: true,
                enableRowSelection: false,
                enableRowHeaderSelection: true,
                enableColumnResizing: true,//允许调整列宽
                appScopeProvider: self,
                enableFiltering: false,
                multiSelect: false,// 是否可以选择多个,默认为true;
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }
                    , {
                        field: 'id',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_3'),
                        width: 200,
                        visible: false
                    }
                    , {
                        field: 'GroupCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_4'),
                        width: 200
                    }
                    , {
                        field: 'GroupName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_5'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;

                },
                data: []
            }
        }

        function initGridData() {

            let Pagination = {
                rows: self.gridOptions.paginationPageSize,
                page: self.gridOptions.paginationCurrentPage,
                sidx: 'GroupCode',//检验类型
                sord: 'asc'
            };
            var postdata = {
                pagination: Pagination,

            }

            var url = commonService.getMesApiAddress("quality") + "QC_OQCCheckConfigMaterial/GetwlZList";
            commonService.callWebApiPost(url, postdata).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptions.data = res.data.resultData;
                    var postdata = {
                        queryJson: {
                            OQCCheckConfigId: self.selectedItem.Id
                        }
                    }
                    // debugger
                    var url = commonService.getMesApiAddress("quality") + "QC_OQCCheckConfigMaterial/QC_OQCCheckConfigMaterialPageDataTableList";
                    commonService.callWebApiPost(url, postdata).then(function (resdata) {
                        if (resdata && resdata.data.success) {
                            // debugger
                            self.gridOptions.data.forEach((item) => {
                                if (resdata.data.resultData.rows.find(t => t.SmallClass == item.GroupCode)) {
                                    $scope.gridApi.selection.selectRow(item);
                                }
                            });

                        }
                    });
                }
            })
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            var data = []
            let selectRows = $scope.gridApi.selection.getSelectedRows();

            if (selectRows.length < 1) {

                busyIndicatorService.hide();
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_6'), commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_7'));
                return false;
            }

            selectRows.forEach(item => {
                data.push({
                    OQCCheckConfigId: self.selectedItem.Id,
                    SmallClass: item.GroupCode,
                    SmallClassName: item.GroupName
                })
            })

            var postData = {
                KeyValue: self.selectedItem.Id,
                data: data
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_8') });
            var url = commonService.getMesApiAddress("quality") + 'QC_OQCCheckConfigMaterial/SaveBatchQC_OQCCheckConfigMaterial';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_9'));
                //刷新局部
                $rootScope.$emit('to-parentMaterialGroup', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_7'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_7'));
        }
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_OQCManage_OQCCheckConfig';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/OQCManage';

        var state = {
            name: screenStateName + '.addMaterial',
            url: '/addMaterial',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OQCCheckConfig-addMaterial.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.OQCManage.OQCCheckConfigaddMaterialctrl.Tips_1'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
