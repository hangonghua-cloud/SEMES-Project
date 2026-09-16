(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ProductDatafile').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafile.service', '$state', '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
    'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;
        
        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            self.selectClick = selectClick;//选择库位
            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
        }
        function selectClick() {
            //自定义查询字段
            let queryParmeters = [
                { 'FieldCode': 'module', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_2'), 'FiledType': 'Text' },
                { 'FieldCode': 'tableName', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_3'), 'FiledType': 'Text' },
                { 'FieldCode': 'TableEntity', 'FileldName': commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_4'), 'FiledType': 'Text' },
            ];

            //grid显示字段列表
            let columnDefs = [
                {
                    field: 'module',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_2'),
                    width: 170
                },
                {
                    field: 'tableName',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_3'),
                    width: 200
                },
                {
                    field: 'TableEntity',
                    displayName: commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_4'),
                    width: 250
                }

            ];
            let query = {
                isFile: '1'
            }
            /*
            * 功能描述: 单选弹窗方法(精工 ui-grid列表), 自定义查询条件,显示列表字段
            * 创    建: 刘万军
            * 创建时间: 2021-1-22
            * 参    数:
            *       title    标题(最后生成如: 选择产品型号)
            *       PostUrl API接口
            *       queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_5'),'FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_6'),'FiledType':'Date'}]
            *       sidx  排序字段
            *       sord  排序方式
            *       columnDefs   grid显示字段列表
            *       callback  回调方法
            */

            commonService.Select_multiSelectModal(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_2'), commonService.getMesApiAddress("material") + 'Base_DataFileCon/Base_DataFileConPageDataTableList', queryParmeters, 'module', 'asc', columnDefs, Select_SingleChoiceModal_MaterialName_callback, query);
        }

        //公共弹窗回调方法
        function Select_SingleChoiceModal_MaterialName_callback(result_data) {
            console.info('Select_SingleChoiceModal_MaterialCode_callback_data', result_data);
            //物料编码编码
            //self.currentItem.MaterialCode = result_data.Materiel_Code;
            //物料编码
            var TableEntity="";
            result_data.forEach((item, index, arr) => {
                TableEntity=TableEntity+','+item.TableEntity;
            })
            self.currentItem.TableEntity=TableEntity;
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var postData = {
                   //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                TableEntity: self.currentItem.TableEntity,
                StartTime:commonService.ConvertToLocalTime(self.CreateTime),
                EndTime:commonService.ConvertToLocalTime(self.EndCreateTime)
            };

            console.log("postData------------------------------------" + JSON.stringify(postData));
            // commonService.getMesApiAddress() = '/sitSrvApi/'
            var url = commonService.getMesApiAddress("material") + 'Base_DataFileCon/Save_DateTimeDataFileCon';
         
            //提交数据
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);
         
            busyIndicatorService.hide();
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_7'));
                //刷新局部
                //$rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_8'));
            }
        }
   //保存失败事件
   function onSaveError(error) {
    busyIndicatorService.hide();
    backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_8'));
}
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_ProductDatafile_ProductDatafile';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ProductDatafile';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductDatafile-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ProductDatafile.ProductDatafileaddctrl.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
