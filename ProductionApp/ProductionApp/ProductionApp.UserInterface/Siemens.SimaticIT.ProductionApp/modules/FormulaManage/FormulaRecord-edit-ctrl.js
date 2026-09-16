(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.FormulaManage').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.FormulaManage.FormulaRecord.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initGridDataDetail();
            initGridData();

            registerEvents();

            sidePanelManager.setTitle(self.currentItem.FormulaTypeName + commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;


            //Initialize Model Data
            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);

            self.currentItem.FormulaTime = new Date(self.currentItem.FormulaTime);

            if (self.currentItem.FormulaType == "XLPF") {
                self.currentItem.ProcessCode = "FHXL"
            } else {
                self.currentItem.ProcessCode = "FHWL"
            }

            self.validInputs = false;
            self.isDetailButtonVisible = false;
            self.selectedItemDetail = null;
            self.isreadonly = false;

            self.validInputs2 = false;
            self.is2DetailButtonVisible = false;
            self.selectedItem2Detail = null;

            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.userGroupChange = userGroupChange;
            // self.add=addForm;
            // self.delete=deleteForm;
            self.formulachange = formulachange;
        }

        function initDictionary() {

            self.typeMachine = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2'), ResourceCode: "" }]
            };
            self.typeUserGroup = {
                value: { PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2'), PTeamCode: "" },
                options: [{ PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2'), PTeamCode: "" }]
            }
            // self.typeShift = {
            //     value: { ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2'), ItemValue: "" },
            //     options: [{ ItemName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2'), ItemValue: "" }]
            // }
            // commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.Factory.options = res.data.resultData;
            //         self.Factory.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2')
            //         });
            //     }
            // });
            // commonService.getDataItemDuatil("Shift").then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeShift.options = res.data.resultData;
            //         self.typeShift.value = self.typeShift.options.find(t => t.ItemValue == self.currentItem.Team);
            //     }
            // })
            //FHWL：喂料， FHXL：小料

            var url2 = commonService.getMesApiAddress("ProduceManage") + "PM_TeamPerson/GetPM_TeamPersonList?ProcessCode=" + self.currentItem.ProcessCode;
            commonService.callWebApiGet(url2, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeUserGroup.options = res.data.resultData;
                    self.typeUserGroup.options.splice('0', '0', {
                        PTeamCode: "",
                        PTeamName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2')
                    });
                    self.typeUserGroup.value = self.typeUserGroup.options.find(t => t.PTeamCode == self.currentItem.UserGroup);
                }
            })
            commonService.getResourceListByParentResource({ ParentResource: self.currentItem.ProcessCode }).then(function (res) {
                if (res && res.data.success) {
                    self.typeMachine.options = res.data.resultData;
                    self.typeMachine.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_2')
                    });
                    self.typeMachine.value = self.typeMachine.options.find(t => t.ResourceCode == self.currentItem.FormulaMachine);
                }
            });
        }

        //组管理人员
        function userGroupChange(oldval, newval) {
            self.currentItem.UserName = "";
            if (!!newval && newval.PTeamCode != "") {
                let queryParmeters = {
                    queryJson: {
                        PTeamCode: newval.PTeamCode
                    }
                };
                var url = commonService.getMesApiAddress("ProduceManage") + 'PM_TeamPerson_Items/PM_TeamPerson_ItemsPageDataTableList';
                commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var rows = res.data.resultData.rows;
                        var str = "";
                        rows.forEach((item, index) => {
                            str += item.UserName + ","
                        })
                        self.currentItem.UserName = str;
                    } else {
                        self.currentItem.UserName = "";
                    }
                }, function (error) {
                    backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_3'));
                });
            }
        }

        function initGridDataDetail() {
            self.gridOptionsItem1 = {
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
                paginationPageSizes: [100, 300, 500, 1000], //每页显示个数选项
                paginationPageSize: 300, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮
                //选中
                rowTemplate: " <div ng-dblclick =\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: false, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中
                appScopeProvider: self,
                columnDefs: [
                    {
                        name: 'rowNum', displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_4'), width: 80, enableSorting: false, cellTemplate:
                            '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                    },

                    {
                        field: 'MaterialName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_5'),
                        width: 160
                    },
                    {
                        field: 'UnitConsome',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_6'),
                        width: 120
                    },
                    {
                        field: 'UnitName',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_7'),
                        width: 120
                    },
                    {
                        field: 'TheoryQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_8'),
                        width: 120
                    },
                    {
                        field: 'ActQty',
                        displayName: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_9'),
                        width: 160,
                        cellTemplate: '<sit-numeric sit-value="row.entity.ActQty" ></sit-numeric></div>',
                        width: 300
                    },
                ],
                //---------------api---------------------
                onRegisterApi: function (gridApi) {
                    $scope.gridApiDetail = gridApi;

                    //行选中事件
                    $scope.gridApiDetail.selection.on.rowSelectionChanged($scope, function (row, event) {
                        if (row) {
                            if (row.isSelected) {
                                self.selectedItemDetail = row.entity;
                                self.isDetailButtonVisible = true;
                                //console.log (self.selectedItemDetail);
                                //子表明细关联
                                //initGridDataDetail();
                            } else {
                                self.selectedItemDetail = null;
                                self.isDetailButtonVisible = false;
                            }
                        }
                    });
                },
                data: []
            }
        }

        function initGridData() {
            let queryParmeters = {
                queryJson: {
                    FormulaId: self.currentItem.Id
                }
            };
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_FormulaRecordDetail/PM_FormulaRecordDetailPageDataTableList';
            commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                if ((res) && (res.data.success)) {
                    self.gridOptionsItem1.data = res.data.resultData.rows;
                } else {
                    self.gridOptionsItem1.data = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_3'));
            });
        }

        function formulachange(oldval, newval) {
            self.gridOptionsItem1.data.forEach(item => {
                item.TheoryQty = item.UnitConsome * newval;
            })
        }

        // function addForm(){
        //     if(!self.currentItem.FormulaNum){
        //         backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_10'),commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_11'));
        //         return
        //     }
        //     self.is2DetailButtonVisible=false;
        //     self.isreadonly=true;

        // }



        // function deleteForm(){
        //     if(!!self.selectedItem2Detail){
        //         self.gridOptionsItem2.data = _.filter(self.gridOptionsItem2.data, function (item) {
        //             return item.BatchNo != self.selectedItem2Detail.BatchNo;
        //         })
        //     }
        //     if(self.selectedItem2Detail.length==0){
        //         self.isreadonly=false;
        //     }
        // }


        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_12') });
            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            //self.currentItem.FormulaCode = self.currentItem.MaterialCode;
            // self.currentItem.FormulaName = self.currentItem.MaterialName;
            // self.currentItem.FormulaType= self.currentItem.SmallClass;
            self.currentItem.FormulaMachine = self.typeMachine.value.ResourceCode;
            self.currentItem.UserGroup = self.typeUserGroup.value.PTeamCode;


            var data1 = self.gridOptionsItem1.data;
            if (data1.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_13'), commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_11'));
                busyIndicatorService.hide();
                return
            }

            var postData = {
                KeyValue: self.currentItem.Id,
                Entity: self.currentItem,
                data: data1
            };

            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_FormulaRecord/SavePM_FormulaRecord';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_14'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_15'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_15'));
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form2") {
                self.validInputs2 = params.validity;
            } else {
                self.validInputs = params.validity;
            }
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_FormulaManage_FormulaRecord';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/FormulaManage';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/FormulaRecord-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.FormulaRecord.editJS.Tips_16'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());