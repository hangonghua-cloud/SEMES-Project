(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.WorkOrderManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.WorkOrderManage.WorkOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;


        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(self.currentItem.Title);
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            self.currentItem = angular.copy($stateParams.selectedItem);

            self.validInputs = false;
            self.isReadNum = true;
            initDictionary();
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            //self.materialChange = materialChange;
            self.numChange = numChange;
            self.processRouteChange = processRouteChange;
            materialChange();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {

            self.typeProcessOperation = {
                value: { ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1'), ProcessCode: "" },
                options: [{ ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1'), ProcessCode: "" }]
            };
            self.typeStartProcess = {
                value: { OperationName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1'), OperationCode: "" },
                options: [{ OperationName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1'), OperationCode: "" }]
            };
            if (self.currentItem.IsVC) {
                self.typeStartProcess.options.push(
                    { OperationName: self.currentItem.StartOperationName, OperationCode: self.currentItem.StartOperation }
                );
                self.typeStartProcess.value = { OperationName: self.currentItem.StartOperationName, OperationCode: self.currentItem.StartOperation };
            }
            //工序
            // commonService.getProcessByFactory({ LevelCode: self.currentItem.FactoryCode }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeStartProcess.options = res.data.resultData;
            //         self.typeStartProcess.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1')
            //         });
            //     }
            // });

            //工艺路线
            // var url = commonService.getMesApiAddress("material") + "BS_Process/GetBS_ProcessList?checkType=";
            // commonService.callWebApiGet(url, null).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeProcessOperation.options = res.data.resultData;
            //         self.typeProcessOperation.options.splice('0', '0', {
            //             ProcessCode: "",
            //             ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1')
            //         });
            //     }
            // })

            var url1 = commonService.getMesApiAddress("material") + "Base_MaterialFactory/GetBase_MaterialFactorySelect";
            commonService.callWebApiPost(url1, {
                queryJson: {
                    FactoryCode: self.currentItem.FactoryCode,
                    MaterialCode: self.currentItem.MaterialCode
                }
            }).then(function (res) {
                if (res && res.data.success) {
                    self.typeProcessOperation.options = res.data.resultData;
                    // self.typeProcessOperation.options.splice('0', '0', {
                    //     ProcessCode: "",
                    //     ProcessName: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_1')
                    // });
                    self.typeProcessOperation.value = res.data.resultData[0];
                }
            })
        }
        function materialChange() {
            debugger;
            if (self.currentItem.IsVC) {
                self.isReadNum = false;
            } else {
                let queryParmeters = {
                    queryJson: {
                        MaterialCode: self.currentItem.MaterialCode
                    }
                };
                var url = commonService.getMesApiAddress("material") + 'Base_MaterialFacet/Base_MaterialFacetPageDataTableList';
                //DXZH
                commonService.callWebApiPost(url, queryParmeters).then(function (res) {
                    if ((res) && (res.data.success)) {
                        var rows = res.data.resultData.rows;
                        self.currentItem.Spec = rows[0].Spec;
                        self.currentItem.DXZH = rows.find(t => t.AttrCode == "DXZH").AttrValue;
                        self.isReadNum = false;
                    } else {
                        self.currentItem.Spec = "";
                    }
                }, function (error) {
                    backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_2'));
                });
            }
        }
        function numChange(old, newval) {
            if (self.currentItem.DXZH && self.currentItem.DXZH != 0) {
                self.currentItem.OrderPieces = Math.ceil(newval * 1.0 * self.currentItem.DXZH);
                self.currentItem.OrderPieces = self.currentItem.OrderPieces;
            }
        }

        //获取工艺路线下的工序
        function processRouteChange(oldval, newval) {

            var postData2 = {
                queryJson: {
                    ProcessCode: newval.ProcessCode
                }
            }
            var url2 = commonService.getMesApiAddress("material") + 'BS_ProcessOfOperations/BS_ProcessOfOperationsPageDataTableList';
            commonService.callWebApiPost(url2, postData2).then(function (resProcess) {
                if (resProcess && resProcess.data.success) {
                    self.typeStartProcess.options = resProcess.data.resultData.rows;
                    self.typeStartProcess.value = resProcess.data.resultData.rows[0];
                }
            })
        }
        function save() {

            if (self.currentItem.WorkOrderType == "3" && !self.currentItem.JY_Remark) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_3'), commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_4'));
                return false;
            }
            //字典类型 取值参考
            if (!self.currentItem.IsVC) {
                self.currentItem.Process = self.typeProcessOperation.value.ProcessCode == "" ? null : self.typeProcessOperation.value.ProcessCode;
            }
            self.currentItem.StartOperation = self.typeStartProcess.value.OperationCode == "" ? null : self.typeStartProcess.value.OperationCode;

            var postData = {
                KeyValue: '',
                ProcessRoute: self.currentItem.Process,
                OldWorkOrder: self.currentItem.OldWorkOrder,
                OrderType: self.currentItem.OrderType,
                DXZH: self.currentItem.DXZH,
                Remark: self.currentItem.JY_Remark,
                Entity: self.currentItem,
            };
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_5') });
            var url = commonService.getMesApiAddress("plan") + 'PL_WorkOrder/SavePL_AddWorkOrderForm';
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_6'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_4'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_4'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_WorkOrderManage_WorkOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/WorkOrderManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/WorkOrder-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.WorkOrderManage.addJS.Tips_7'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
