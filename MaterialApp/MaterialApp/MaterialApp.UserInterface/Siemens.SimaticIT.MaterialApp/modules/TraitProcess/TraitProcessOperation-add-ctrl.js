(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.TraitProcess').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperation.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            //初始化 是否启用
            self.IsEnabled = {
                value: '1',
                options: [{
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_1'),
                    value: '1'
                }, {
                    label: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_2'),
                    value: '0'
                }]
            };
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_3'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.queryItem = {};
            self.validInputs = false;
            self.queryItem = angular.copy($stateParams.selectedItem);

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.materialClick = materialClick;
            initDictionary();
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function initDictionary() {
            //初始化 物料分类

            self.TypeFactory = {
                value: { ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_4'), ResourceCode: "" },
                options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_4'), ResourceCode: "" }]
            }


            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.TypeFactory.options = res.data.resultData;
                    self.TypeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_4')
                    });
                }
            });


        }

        function materialClick() {
            //自定义查询字段
            let queryParmeters = [
                { 'FieldCode': 'TraitCode', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_5'), 'FiledType': 'Text' },
                { 'FieldCode': 'TraitName', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_6'), 'FiledType': 'Text' },
                { 'FieldCode': 'TraitValue', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_7'), 'FiledType': 'Text' }
            ];

            //grid显示字段列表
            let columnDefs = [
                {
                    field: 'TraitCode',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_5'),
                    width: 200
                },
                {
                    field: 'TraitName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_6'),
                    width: 200
                },
                {
                    field: 'TraitValue',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_7'),
                    width: 200
                }
            ];

            /*
            * 功能描述: 单选弹窗方法(), 自定义查询条件,显示列表字段
            * 创    建: jpf
            * 创建时间: 2022-11-15
            * 参    数:
            *       title    标题(最后生成如: 选择产品型号)
            *       PostUrl API接口
            *       queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_8'),'FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_9'),'FiledType':'Date'}]
            *       sidx  排序字段
            *       sord  排序方式
            *       columnDefs   grid显示字段列表
            *       callback  回调方法
            */
            let query = {
                Factory: self.queryItem
            }
            commonService.Select_SingleChoiceModal(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_10'), commonService.getMesApiAddress("material") + 'BS_TraitManage/BS_TraitManagerDetailDataTableList', queryParmeters, 'TraitCode', 'asc', columnDefs, Select_SingleChoiceModal_MaterialName_callback, query);
        }
        //公共弹窗回调方法
        function Select_SingleChoiceModal_MaterialName_callback(result_data) {
            console.info('Select_SingleChoiceModal_MaterialName_callback_data', result_data);
            self.currentItem.MaterialClass = result_data.TraitName;
            self.currentItem.SmallClass = result_data.TraitValue

        }
        //保存
        function save() {
            // 下拉取值
            self.currentItem.FactoryCode = self.TypeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.TypeFactory.value.ResourceName;
            self.currentItem.ProcessType = "2";
            if (self.IsEnabled.value == "1") {
                self.currentItem.IsDefault = true;

            } else {
                self.currentItem.IsDefault = false;
            }
            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: '',      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem
            };
            console.log("jpf1111111" + JSON.stringify(postData));
            var url = commonService.getMesApiAddress("material") + 'BS_Process/SaveBS_Process';
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_11') });
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
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_12'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_13'));
            }
        }
        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_13'));
        }


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_TraitProcess_TraitProcessOperation';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/TraitProcess';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TraitProcessOperation-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationaddctrl.Tips_3'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
