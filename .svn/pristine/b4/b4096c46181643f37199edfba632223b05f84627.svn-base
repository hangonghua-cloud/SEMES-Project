(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.TraitProcess').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperation.service', '$state',
        '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.busyIndicator.service'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.MaterialApp.TraitProcess.TraitProcessOperationselectctrl.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            // self.currentItem = $stateParams.selectedItem;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.changeFile = changeFile;
            self.importExcel = function () {
                importExcel(self.file.contents);
            };
        }

        function base64ToFile(base64Str) {
            var bstr = atob(base64Str), n = bstr.length, u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], "", { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        }

        function changeFile(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel, 500);
            }
        }

        function importExcel(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_1'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["工艺路线"]);
            self.grid1 = canvasDatagrid();
            document.getElementById("sittab1").innerHTML = "";
            document.getElementById("sittab1").appendChild(self.grid1);
            self.grid1.data = self.grid1Json;

            self.grid2Json = XLSX.utils.sheet_to_json(wb.Sheets["工序"]);
            self.grid2 = canvasDatagrid();
            document.getElementById("sittab2").innerHTML = "";
            document.getElementById("sittab2").appendChild(self.grid2);
            self.grid2.data = self.grid2Json;

            self.grid3Json = XLSX.utils.sheet_to_json(wb.Sheets["属性"]);
            self.grid3 = canvasDatagrid();
            document.getElementById("sittab3").innerHTML = "";
            document.getElementById("sittab3").appendChild(self.grid3);
            self.grid3.data = self.grid3Json;
        }

        function getData1() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                dataList.push({
                    FactoryCode: item["工厂编码"],
                    FactoryName: item["工厂名称"],
                    ProcessCode: item["工艺编码"],
                    ProcessName: item["工艺名称"],
                    MaterialClass: item["特征名称"],
                    SmallClass: item["特征值"],
                    IsDefault: item["是否默认"] == "是" ? 1 : 0,
                });
            }
            return dataList;
        }

        function getData2() {
            var dataList = [];
            for (var i = 0; i < self.grid2.data.length; i++) {
                var item = self.grid2.data[i];

                dataList.push({
                    ProcessCode: item["工艺编码"],
                    OperationCode: item["工序编码"],
                    OperationName: item["工序名称"],
                    CuringCycle: item["养生周期"],
                    SN: item["顺序号"],
                });
            }
            return dataList;
        }

        function getData3() {
            var dataList = [];
            for (var i = 0; i < self.grid3.data.length; i++) {
                var item = self.grid3.data[i];

                dataList.push({
                    ProcessCode: item["工艺编码"],
                    OperationCode: item["工序编码"],
                    AttrCode: item["属性编码"],
                    AttrName: item["属性名称"],
                    AttrType: item["属性类型编码"],
                    AttrTypeName: item["属性类型名称"],
                    AttrValue: item["属性值"],
                });
            }
            return dataList;
        }

        function save() {

            var data1 = getData1();
            var data2 = getData2();
            var data3 = getData3();

            if (data1.length < 1 || data2.length < 1 || data3.length < 1) {
                commonService.showWarning("数据不完整,请重新导入");
                return;
            }
            var data = {
                data1: data1,
                data2: data2,
                data3: data3,
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_5') });
            var url = commonService.getMesApiAddress('material') + "/BS_Process/TraitProcessImport";
            commonService.callWebApiPost(url, data).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            busyIndicatorService.hide();
            if (data.data.success == true) {
                sidePanelManager.close();
                $state.go('^', {}, { reload: true });
            }
            else {
                backendService.genericError(data.data.returnMsg, "提示");
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, "操作出错");
        }
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_TraitProcess_TraitProcessOperation';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/TraitProcess';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/TraitProcessOperation-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '导入'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
