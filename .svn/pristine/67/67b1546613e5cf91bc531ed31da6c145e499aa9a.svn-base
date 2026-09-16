(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.OwnProduct').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.OwnProduct.OwnProductOrder.service', '$state',
        '$stateParams', 'common.base', '$filter', '$scope', 'commonService', 'common.widgets.busyIndicator.service'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            sidePanelManager.setTitle('导入');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = $stateParams.selectedItem;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.changeFile = changeFile;
            self.importExcel = function () {
                importExcel(self.file.contents);
            };
        }

        function importExcel(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_2'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_3'));
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["Sheet1"]);
            self.grid1 = canvasDatagrid();
            document.getElementById("sittab1").innerHTML = "";
            document.getElementById("sittab1").appendChild(self.grid1);
            // self.grid1.data = gridFormatDate(self.grid1Json);
            self.grid1.data = self.grid1Json;
        }

        function changeFile(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel, 500);
            }
        }
        function gridFormatDate(grid) {
            grid.forEach((item, index, arr) => {
                item[commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_4')] = formatDate(item[commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_4')]);
                item[commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_5')] = formatDate(item[commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_5')]);
                item[commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_6')] = formatDate(item[commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_6')]);
            });
            return grid;
        }

        function getData1() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                dataList.push({
                    FactoryCode: item["工厂编码"],
                    FactoryName: item["工厂名称"],
                    ProcessCode: item["工序编码"],
                    ProcessName: item["工序名称"],
                    WorkOrder: item["工单号"],
                    LineNum: item["行号"],
                    BOMCode: item["BOM编码"],
                    ProcessRoute: item["工艺路线编码"],
                    MaterialCode: item["物料编码"],
                    MaterialName: item["物料名称"],
                    PlanQty: item["计划数量"],
                    UnitName: item["单位"],
                });
            }
            return dataList;
        }

        /*数据校验逻辑 end*/
        function isDate(val) {
            if (typeof (val) == "undefined" || val == "") return false;
            //返回为false则是日期格式;isNaN(data)排除data为纯数字的情况（此处不考虑只有年份的日期，如'2018'
            if (isNaN(val) && !isNaN(Date.parse(val))) {
                return true;
            }
            return false;
        }
        function formatDate(numb) {
            // debugger;
            var time = new Date((numb - 1) * 24 * 3600000 + 1)
            console.log(time);
            time.setYear(time.getFullYear() - 70)
            function formatFunc(str) {    //格式化显示
                return str > 9 ? str : '0' + str
            }
            // time.setHours(-24);//时间会多一天
            var year = time.getFullYear();
            var mon = formatFunc(time.getMonth() + 1);
            var day = formatFunc(time.getDate());
            var dateStr = year + '-' + mon + '-' + day;
            return dateStr;
        }
        function base64ToFile(base64Str) {
            var bstr = atob(base64Str), n = bstr.length, u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], "", { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        }

        function save() {

            var dataList1 = getData1();
            if (dataList1.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_18'), commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_3'));
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_19') });
            var url = commonService.getMesApiAddress('ProduceManage') + "/PM_OwnProductOrder/WorkOrderImport";
            commonService.callWebApiPost(url, {
                data: dataList1
            }).then(onSaveSuccess, onSaveError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            busyIndicatorService.hide();
            if (data.data.success == true) {
                sidePanelManager.close();//关闭侧边栏
                $state.go('^', {}, { reload: true });
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_3'));
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.ProductionApp.PMOperationPalletNum.importJS.Tips_20'));
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
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_OwnProduct_OwnProductOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/OwnProduct';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/OwnProductOrder-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '查看'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
