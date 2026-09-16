(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PMPerformanceManage').config(ImportScreenStateConfig);

    ImportScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PMPerformanceManage.PMPerformanceManage.service', '$state',
        '$stateParams', 'common.base', '$filter', '$scope', 'i18nService', 'FileUploader', 'commonService',
        'common.services.authentication', 'common.widgets.busyIndicator.service'];
    function ImportScreenController(dataService, $state, $stateParams, common, $filter, $scope, i18nService, FileUploader,
        commonService, auth, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();

        // Initialization function
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle('导入');
            //sidePanelManager.open('e');
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

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
                backendService.genericError("请选择Excel！", "提示");
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["绩效管理"]);
            self.grid1 = canvasDatagrid();
            document.getElementById("sittab1").innerHTML = "";
            document.getElementById("sittab1").appendChild(self.grid1);
            // self.grid1.data = gridFormatDate(self.grid1Json);
            self.grid1.data = self.grid1Json;
            console.log("导入的数据", self.grid1.data);
        }

        function changeFile(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel, 500);
            }
        }
        function gridFormatDate(grid) {
            grid.forEach((item, index, arr) => {
                item["计薪日期"] = formatDate(item["计薪日期"]);
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
                    WorkshopCode: item["车间编码"],
                    WorkshopName: item["车间名称"],
                    ProcessCode: item["工序编码"],
                    ProcessName: item["工序名称"],
                    PostCode: item["岗位编码"],
                    PostName: item["岗位名称"],
                    UserCode: item["员工工号"],
                    UserName: item["员工姓名"],
                    PayrollDate: item["计薪日期"],
                    Salary: item["薪资"],
                    Description: item["事项说明"],
                    Remark: item["备注"],
                    InfoSource: 0
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
                backendService.genericError("请先导入数据！", "提示");
            }
            busyIndicatorService.show({ message: "保存中，请稍后……" });
            var url = commonService.getMesApiAddress('ProduceManage') + "/PMPerformanceManage/BatchImport";
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
    ImportScreenStateConfig.$inject = ['$stateProvider'];
    function ImportScreenStateConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_ProductionApp_PMPerformanceManage_PMPerformanceManage';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PMPerformanceManage';

        var state = {
            name: moduleStateName + '.import',
            url: '/import',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PMPerformanceManage-import.html',
                    controller: ImportScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '导入'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
