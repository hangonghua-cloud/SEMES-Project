/**
*  0. 代码生成： UA主子表一键生成前后端html、JS、API接口代码生成器 Ver 1.13 更新日期：2021-08-16  设计者：刘万军
*  1. 功能描述： 特征维护
*  2. 创建人员： jpf
*  3. 创建日期： 2022-11-02
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BSTraitManage').config(AddDetailScreenStateConfig);

    AddDetailScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BSTraitManage.BS_TraitManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope', 'i18nService'];
    function AddDetailScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope, i18nService) {
        // 国际化；
        i18nService.setCurrentLang('zh-cn');
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {

            //初始化
            init();
            initGridOptions();
            //注册事件
            registerEvents();

            //self.currentItem.TraitManageID = commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_1');//如果新增有系统自动生成编号 可以在这写一个初始值, 这个控件要只读状态,后台代码生成编号+流水号

            //获取登录用户信息
            GetUserInfo();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_2'));
            // sidePanelManager.open('e');//使用窄弹窗
            //使用宽右侧弹窗
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
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
            self.MainselectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.currentItem.TraitManageID = self.MainselectedItem.Id;//关联字段
            self.validInputs = false;
            initDictionary();

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            //屏蔽弹出框点击空白区域关闭的方法
            //commonService.shieldModalClose();
            self.AttrChange = AttrChange;
        }

        function initDictionary() {

            //物料属性模板
            self.Attr = {
                value: null,
                options: []
            }
            //获取属性模板

            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTemp/GetBase_MaterialBindTempList?checkType=';
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.Attr.options = res.data.resultData;
                    self.Attr.options.splice(0, 0, {
                        TempCode: "",
                        TempName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_3')
                    });
                    self.Attr.value = { TempCode: res.data.resultData[0].TempCode, TempName: res.data.resultData[0].TempName };
                } else {
                    self.Attr.options = [];
                }
            });
        }
        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        //下拉框改变事件
        function AttrChange(oldval, newval) {
            if (newval) initGridData(newval.Id);
        }
        //加载table
        function initGridData(id) {
            var url = commonService.getMesApiAddress("material") + 'Base_MaterialBindTempFacet/GetBase_MaterialBindTempFacetList?checkType=' + id;
            var req = commonService.callWebApiGet(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.gridOptionsItem.data = res.data.resultData;
                } else {
                    self.gridOptionsItem.data = []
                }
            });
        }
        function initGridOptions() {
            self.gridOptionsItem = {
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
                excessRows: 100,
                columnDefs: [

                    {
                        field: 'AttrCode',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_4'),
                        width: 200,
                    },
                    {
                        field: 'AttrName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_5'),
                        width: 200
                    },

                    {
                        field: 'AttrTypeName',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_6'),
                        width: 200
                    },

                    {
                        field: 'AttrValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_7'),
                        cellTemplate: '<div ng-show="row.entity.AttrType==1"><sit-numeric  sit-value="row.entity.AttrValue" ></sit-numeric></div>' +
                            '<div ng-show="row.entity.AttrType==2"><sit-text sit-value="row.entity.AttrValue" ></sit-text></div>' +
                            '<div ng-show="row.entity.AttrType==3"><sit-date-time-picker sit-value="row.entity.AttrValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>',
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                        } else {
                            self.selectedItem = null;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        //保存
        function save() {

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;


            console.log("postData------------------------------------" + JSON.stringify(self.currentItem));

            //获取登录用户信息 要存在此函数 GetUserInfo()  方法    
            self.currentItem.CreateName = self.UserName;
            self.currentItem.Creator = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.CreateName = self.UserCode;
            }
            if (self.Attr.value.TempCode == "" || self.Attr.value.TempCode == null) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_8'), commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_9'));
                return;
            }

            self.currentItem.AttrModleCode = self.Attr.value.TempCode;
            let itemData = angular.copy(self.gridOptionsItem.data);
            itemData.forEach((item, index, arr) => {
                if (item.AttrType == "3" && item.AttrValue) {//处理时间
                    item.AttrValue = $filter('date')(new Date(item.AttrValue), 'yyyy-MM-dd HH:mm:ss');
                }
                //item.MaterialId = self.currentItem.MaterialId;
            });
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                data: itemData,
                TraitCode: self.MainselectedItem.TraitCode
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("material") + 'BS_TraitDetails/SaveBS_TraitDetails';
            //var url = 'http://localhost:49849/' + 'BS_TraitDetails' + '/SaveBS_TraitDetails'; 
            console.log("url----------------" + url);
            //提交数据
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_10') });
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
            console.log("SaveBS_TraitManage----------------------" + JSON.stringify(req));
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
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_11'));
                //刷新局部 主界面中子明细表   'parent' 回传参数 或 实体 都可以
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^');//不加这个主列表页面原按钮不能再次点击
                //$state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_12'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_12'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddDetailScreenStateConfig.$inject = ['$stateProvider'];
    function AddDetailScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BSTraitManage_BS_TraitManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BSTraitManage';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BSTraitManage-addDetail.html',
                    controller: AddDetailScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.BSTraitManage.BSTraitManageaddDetailctrl.Tips_2'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
