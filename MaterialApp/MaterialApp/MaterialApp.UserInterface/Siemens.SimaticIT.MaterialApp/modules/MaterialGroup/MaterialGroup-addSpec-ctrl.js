(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.MaterialGroup').config(AddSpecScreenStateConfig);

    AddSpecScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroup.service', '$state',
        '$stateParams', 'common.base', '$filter', '$rootScope', '$scope', 'common', 'common.widgets.notificationTile.globalService', '$http',
        'common.services.authentication', 'commonService', '$interval', '$timeout', 'common.widgets.busyIndicator.service'];
    function AddSpecScreenController(dataService, $state, $stateParams, common, $filter, $rootScope, $scope, c, notification,
        $http, $auth, commonService, $interval, $timeout, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler, messageservice;

        activate();
        function activate() {
            init();
            initGridOptionsDetail();
            initGridData();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;


            //Initialize Model Data
            self.currentItem = {};
            self.requestparam = {};
            self.validInputs = false;

            self.organizeTreeData = [];
            self.selectedOrganize = [];

            self.data = angular.copy($stateParams.selectedItem);
            self.currentItem.GroupCode = self.data.GroupCode;
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.search = search;
            initDictionary();
        }



        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {


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
                paginationPageSizes: [50, 70, 90, 100], //每页显示个数选项
                paginationPageSize: 50, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: true, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: true,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [

                    {
                        field: 'TraitValue',
                        displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_5'),
                        width: 350
                    },

                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;

                    //行选中事件
                    $scope.gridApi.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);

                        } else {
                            self.selectedItem = null;
                            //setButtonsVisibility(false);
                        }
                    });
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            }
        }

        function search() {
            initGridData();
        }

        //查询方法,数据绑定
        function initGridData() {

            self.requestparam.TraitCode = "SPEC";

            var url = commonService.getMesApiAddress("material") + 'BS_TraitDetails/BS_TraitDetailsPageDataTableList';
            commonService.callWebApiPost(url, { queryJson: self.requestparam }).then(function (res) {
                if ((res) && (res.data.success)) {

                    var param = {
                        queryJson: {
                            GroupCode: self.currentItem.GroupCode
                        }
                    }
                    var url = commonService.getMesApiAddress("material") + "Base_MaterialGroupBindMaterial/GetDataTableWithPage";
                    commonService.callWebApiPost(url, param).then(function (res1) {
                        if (res1 && res1.data.success) {
                            var data = [];
                            res.data.resultData.rows.forEach(item => {
                                if (!res1.data.resultData.rows.find(t => t.MaterialCode == item.TraitValue)) {
                                    data.push(item);
                                }
                            });
                            self.gridOptionsDetail.data = data;
                        }
                    })
                    // $timeout(function () {
                    //     for (var i = 0; i < self.gridOptionsDetail.data.length; i++) {
                    //         if (self.data.GridData.find(t => t.MaterialCode == self.gridOptionsDetail.data[i].MaterialCode) != null) {
                    //             $scope.gridApi.selection.selectRow(self.gridOptionsDetail.data[i]);
                    //         }
                    //     }
                    // }, 500)
                } else {
                    self.gridOptionsDetail.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_9'));
            });
        }

        function save() {

            //dataService.create(self.currentItem).then(onSaveSuccess, backendService.backendError);
            //self.currentItem.IsEnabled = self.IsEnabledMark[0].checked ? 1 : 0;
            //self.currentItem.IsEnabled =1;
            var rowData = [];
            var rows = $scope.gridApi.selection.getSelectedRows();
            console.log(rows);
            if (rows.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_10'), commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_11'));

                return
            }
            rows.forEach((item, index, arr) => {
                rowData.push({
                    GroupCode: self.currentItem.GroupCode,
                    MaterialCode: item.TraitValue,
                    IsVC: 1
                })
            });


            var postData = {
                username: $auth.getUser().unique_name,
                keyValue: "",
                GroupCode: self.currentItem.GroupCode,
                data: rowData
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_12') });
            var url = commonService.getMesApiAddress("material") + "Base_MaterialGroupBindMaterial/SaveFormList";
            commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            busyIndicatorService.hide();
            if (data.data.success) {
                $rootScope.$emit('to-parent', 'parent');
                sidePanelManager.close();
                notification.warning(data.data.returnMsg);
                $state.go('^', { itemId: self.currentItem.ItemId }, { reload: false });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_11'));
            }
        }

        function onSaveError(error) {
            busyIndicatorService.hide();
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_13'),
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_14',
                text: '[' + error.status + '] - ' + error.data.returnMsg
            });
            messageservice.show();
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddSpecScreenStateConfig.$inject = ['$stateProvider'];
    function AddSpecScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_MaterialGroup_MaterialGroup';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/MaterialGroup';

        var state = {
            name: screenStateName + '.addSpec',
            url: '/addSpec/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/MaterialGroup-addSpec.html',
                    controller: AddSpecScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.MaterialGroup.MaterialGroupaddSpecctrl.Tips_15'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
