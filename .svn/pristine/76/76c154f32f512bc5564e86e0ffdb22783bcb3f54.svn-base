/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： 设备保养项目维护
*  2. 创建人员： 王坤
*  3. 创建日期： 2021-08-05
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain').config(EditSelectScreenStateConfig);

    EditSelectScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintain.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$timeout'];
    function EditSelectScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $timeout) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            //传参实体定义赋值
            self.currentItem = angular.copy($stateParams.selectedItem);


            //初始化
            init();
            //注册事件
            registerEvents();

            //获取登录用户信息
            GetUserInfo();

            initGrid();
            initGridData();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_1'));
            //sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
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
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_2'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    }
                    , {
                        field: 'ItemDetailId',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_3'),
                        width: 200,
                        visible: false
                    }
                    , {
                        field: 'EquipmentType',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_4'),
                        width: 200
                    }
                    , {
                        field: 'EquipmentTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_5'),
                        width: 200
                    }
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //行选中事件
                    //$scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                    //    if (row) {
                    //        if (row.isSelected) {
                    //            self.selectedItemDetail = row.entity;
                    //            console.log(self.selectedItemDetail);
                    //            self.isButtonVisible = true;

                    //            $scope.gridApi.selection.toggleRowSelection($scope.gridOptions.data[2]);
                    //        } else {
                    //            self.selectedItemDetail = null;

                    //        }

                    //    }
                    //});

                },
                data: []
            }
        }
        function initGridData() {
            var url = commonService.getMesApiAddress("equipment") + "EP_EquipmentMaintain/GetTypePageList?id=" + self.currentItem.Id;
            commonService.callWebApiGet(url).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptions.data = res.data.resultData;
                    $timeout(function () {
                        _.each(self.gridOptions.data, function (item, index) {

                            if (self.gridOptions.data[index].Id != null) {
                                // console.log(self.gridOptions.data[i]);
                                $scope.gridApi.selection.selectRow(self.gridOptions.data[index]);
                            }
                        })

                    })

                } else {
                    self.gridOptions.data = [];
                }
            }, function (error) {

                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_6'));
            });
        }

        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //编辑保存
        function save() {
            debugger
            let selectRows = $scope.gridApi.selection.getSelectedRows();
            let Id = self.currentItem.Id;
            var rowArr = new Array();
            if (Id == "" || Id.length == 0) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_7'));
                return;
            }
            if (selectRows == null || selectRows.length == 0) {
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_8'));
                return;
            }
            for (var icount = 0; icount < selectRows.length; icount++) {
                //selectRows[icount].EquipmentType = selectRows[icount].ItemDetailId;
                var item = {
                    // FactoryCode: self.currentItem.FactoryCode,
                    // FactoryName: self.currentItem.FactoryName,
                    EquipmentMaintenanceId: selectRows[icount].EquipmentMaintenanceId,
                    EquipmentType: selectRows[icount].ItemDetailId,
                    EquipmentTypeName: selectRows[icount].EquipmentTypeName,
                    Id: selectRows[icount].Id,
                    ItemDetailId: selectRows[icount].ItemDetailId
                }
                rowArr.push(item);
            }
            //console.log('save.......' + JSON.stringify(selectRows));
            var postData = {
                EquipmentMaintenanceId: Id,
                SaveRows: rowArr
            };
            console.log("savesavesavesavesavesavesavesave       " + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentMaintain/SaveChildForm';
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_9'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_10'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EP_EquipmentMaintain.EP_EquipmentMaintaineditselectctrl.Tips_10'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    EditSelectScreenStateConfig.$inject = ['$stateProvider'];
    function EditSelectScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EP_EquipmentMaintain_EP_EquipmentMaintain';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EP_EquipmentMaintain';

        var state = {
            name: screenStateName + '.editselect',
            url: '/editselect/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EP_EquipmentMaintain-editselect.html',
                    controller: EditSelectScreenController,
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
