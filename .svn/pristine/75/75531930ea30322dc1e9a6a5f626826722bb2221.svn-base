/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 原材料基础检验配置信息
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-31
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance').config(AddMaterialScreenStateConfig);

    AddMaterialScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenance.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$timeout', '$rootScope'];
    function AddMaterialScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $timeout, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                //size: 'wide'
            });
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //初始化前端变量数据
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();

            initGrid();
            initGridData()
        }

        function initGrid() {
            self.gridOptions = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                useExternalPagination: false,//true:使用外部分页方式；false:使用UI Grid内部分页方式
                useExternalSorting: false,//true:使用外部排序方式，false:使用UI Gird内部排序方式
                multiSelect: true,
                enableRowSelection: true,
                enableRowHeaderSelection: true,
                enableColumnResizing: true,//允许调整列宽
                appScopeProvider: self,
                enableFiltering: false,
                multiSelect: false,// 是否可以选择多个,默认为true;
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }
                    , {
                        field: 'id',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_3'),
                        width: 200,
                        visible: false
                    }
                    , {
                        field: 'GroupCode',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_4'),
                        width: 200
                    }
                    , {
                        field: 'GroupName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_5'),
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

      var url = commonService.getMesApiAddress("quality") + "QC_TestMethodMaterial/GetwlZList";
      commonService.callWebApiPost(url, postdata).then(function (res) {
        if (res && res.data.success) {
            self.gridOptions.data = res.data.resultData;


            var postdata = {
                queryJson: {
                    TestMethodId: self.currentItem.Id
                }
            }  
            var url = commonService.getMesApiAddress("quality") + "QC_TestMethodMaterial/QC_TestMethodMaterialPageList";
            commonService.callWebApiPost(url, postdata).then(function (resdata) {
                if (resdata && resdata.data.success) {
                   
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

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_6') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;

            var data = []
            let selectRows = $scope.gridApi.selection.getSelectedRows();

            if (selectRows.length < 1) {

                busyIndicatorService.hide();
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_7'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_8'));
                return false;
            }


            selectRows.forEach(item => {
                data.push({
                    TestMethodId: self.currentItem.Id,
                    SmallClass: item.GroupCode,
                    SmallClassName: item.GroupName
                })
            })


            var postData = {
                Entity: self.currentItem,
                data: data
            };


            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodMaterial/SaveBatchQC_TestMethodMaterial';

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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_9'));
                //刷新局部
                $rootScope.$emit('to-material', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_8'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_TestMethodMaintenance.QC_TestMethodMaintenanceaddmaterialctrl.Tips_8'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddMaterialScreenStateConfig.$inject = ['$stateProvider'];
    function AddMaterialScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_TestMethodMaintenance_QC_TestMethodMaintenance';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_TestMethodMaintenance';

        var state = {
            name: screenStateName + '.addmaterial',
            url: '/addmaterial',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_TestMethodMaintenance-addmaterial.html',
                    controller: AddMaterialScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
