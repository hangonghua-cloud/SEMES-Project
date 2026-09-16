(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.ScrapRecord').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.ScrapRecord.ScrapRecord.service', '$state',
        '$stateParams', 'common.base', '$filter', '$scope', 'i18nService', 'FileUploader', 'commonService',
        'common.services.authentication', 'common.widgets.busyIndicator.service'];
    function ViewScreenController(dataService, $state, $stateParams, common, $filter, $scope, i18nService, FileUploader,
        commonService, auth, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
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

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = {}
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            self.changeFile = changeFile;
            self.importExcel = function () {
                importExcel(self.file.contents);
            };

            initDictionary();
        }

        function initDictionary() {
            //工厂建模
            self.typeFactory = {
                value: { ResourceName: "--请选择--", ResourceCode: "" },
                options: [{ ResourceName: "--请选择--", ResourceCode: "" }]
            };
            commonService.getResourceExtendInfo({ LevelCode: "Factory" }).then(function (res) {
                if (res && res.data.success) {
                    self.typeFactory.options = res.data.resultData;
                    if (res.data.resultData.length > 0) {
                        self.typeFactory.value = res.data.resultData[0];
                    }
                    self.typeFactory.options.splice(0, 0, {
                        ResourceCode: "",
                        ResourceName: "--请选择--"
                    });
                }
            })
            //导入类型
            self.typeImportType = {
                value: { ItemName: "--请选择--", ItemValue: "" },
                options: [{ ItemName: "--请选择--", ItemValue: "" },
                { ItemName: "新增", ItemValue: "1" },
                { ItemName: "修改", ItemValue: "2" }]
            };
            //编码
            self.typeScrapCode = {
                value: { ScrapCode: "" },
                options: [{ ScrapCode: "" }]
            };
            var url = commonService.getMesApiAddress('ProduceManage') + "/PMScrapRecord/GetScrapCodeList";
            commonService.callWebApiPost(url, null).then(function (res) {
                if (res && res.data.success) {
                    self.typeScrapCode.options = res.data.resultData;
                }
                self.typeScrapCode.options.splice(0, 0, {
                    ScrapCode: ""
                });
            });
        }

        function importExcel(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError("请选择Excel！", "提示");
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["Sheet1"]);
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
                    SmallClassName: item["物料分类"],
                    BadItemCode: item["报废原因编码"],
                    BadItemName: item["报废原因"],
                    ScrapQty: item["数量"]
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

            if (self.typeImportType.value.ItemValue == "2" && !self.typeScrapCode.value.ScrapCode) {
                commonService.showWarning("请选择编码！");
                return;
            }

            self.currentItem.FactoryCode = self.typeFactory.value.ResourceCode;
            self.currentItem.FactoryName = self.typeFactory.value.ResourceName;
            self.currentItem.StartTime = commonService.ConvertToLocalTime(self.StartTime);
            self.currentItem.EndTime = commonService.ConvertToLocalTime(self.EndTime);
            self.currentItem.ScrapCode = self.typeScrapCode.value.ScrapCode;

            var dataList1 = getData1();
            if (dataList1.length == 0) {
                backendService.genericError("请先导入数据！", "提示");
            }
            busyIndicatorService.show({ message: "保存中，请稍后……" });
            var url = commonService.getMesApiAddress('ProduceManage') + "/PMScrapRecord/Import";
            commonService.callWebApiPost(url, {
                Entity: self.currentItem,
                data: dataList1,
                importType: self.typeImportType.value.ItemValue
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

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_ScrapRecord_ScrapRecord';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/ScrapRecord';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ScrapRecord-select.html',
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
