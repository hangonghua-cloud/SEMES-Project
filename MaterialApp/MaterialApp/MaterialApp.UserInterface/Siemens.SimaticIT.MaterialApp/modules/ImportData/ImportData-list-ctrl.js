(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.ImportData').config(ListScreenRouteConfig);

    ListScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.ImportData.ImportData.service', '$state', '$stateParams',
        '$rootScope', '$scope', 'common.base', 'common.services.logger.service', 'common.widgets.notificationTile.globalService', 'commonService',
        'common.widgets.busyIndicator.service'];
    function ListScreenController(dataService, $state, $stateParams, $rootScope, $scope, base, loggerService, notificationService,
        commonService, busyIndicatorService) {
        var self = this;
        var logger, rootstate, messageservice, backendService;

        activate();

        // Initialization function
        function activate() {
            logger = loggerService.getModuleLogger('Siemens.SimaticIT.MaterialApp.ImportData.ImportData');

            init();
            // initGridOptions();
            //initGridData();
        }

        function init() {
            logger.logDebug('Initializing controller.......');

            rootstate = 'home.Siemens_SimaticIT_MaterialApp_ImportData_ImportData';
            messageservice = base.widgets.messageOverlay.service;
            backendService = base.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = null;
            self.isButtonVisible = false;
            self.viewerOptions = {};
            self.viewerData = [];

            //1.物料
            self.changeFile1 = changeFile1;
            self.importExcel1 = function () {
                importExcel1(self.file1.contents);
            };
            self.save1ButtonHandler = save1ButtonHandler;

            //3.工厂属性
            self.changeFile13 = changeFile13;
            self.importExcel13 = function () {
                debugger
                importExcel13(self.file13.contents);
            };
            self.save13ButtonHandler = save13ButtonHandler;

            //3.BOM
            self.changeFile2 = changeFile2;
            self.importExcel2 = function () {
                debugger
                importExcel2(self.file2.contents);
            };
            self.save2ButtonHandler = save2ButtonHandler;




        }

        //*****************material start************** */
        function changeFile1(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel1, 500);
            }
        }

        function importExcel1(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_1'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            // self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_3')]);
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["物料主数据"]);
            self.grid1 = canvasDatagrid();
            document.getElementById("sittab1").innerHTML = "";
            document.getElementById("sittab1").appendChild(self.grid1);
            self.grid1.data = self.grid1Json;

            // self.grid2Json = XLSX.utils.sheet_to_json(wb.Sheets[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_4')]);
            // self.grid2 = canvasDatagrid();
            // document.getElementById("sittab2").innerHTML = "";
            // document.getElementById("sittab2").appendChild(self.grid2);
            // self.grid2.data = self.grid2Json;


        }
        function getData1() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                // dataList.push({
                //     MaterialCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_5')],
                //     MaterialName: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_6')],
                //     Spec: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_7')],
                //     MaterialClass: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_8')],
                //     SmallClass: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_9')],
                //     UnitName: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_10')],
                // });
                dataList.push({
                    MaterialCode: item["物料编码"],
                    MaterialName: item["物料名称"],
                    Spec: item["规格型号"],
                    MaterialClass: item["物料分类"],
                    SmallClass: item["物料小类"],
                    UnitName: item["单位"],
                });
            }
            return dataList;
        }
        function getData2() {
            var dataList = [];
            var num = Object.keys(self.grid2Json[0]).length

            // for (var i = 0; i < self.grid2.data.length - 3; i++) {
            //     for (var j = 1; j < Object.keys(self.grid2Json[0]).length; j++) {
            //         var name = commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_11') + (j);
            //         var item = self.grid2.data[i + 3];
            //         dataList.push({
            //             MaterialCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_5')],
            //             AttrCode: self.grid2.data[1][name],
            //             AttrName: self.grid2.data[2][name],
            //             AttrType: self.grid2.data[0][name],
            //             AttrValue: item[name]
            //         });
            //     }
            // }

            for (var i = 0; i < self.grid2.data.length - 3; i++) {
                for (var j = 1; j < Object.keys(self.grid2Json[0]).length; j++) {
                    var name = "属性" + (j);
                    var item = self.grid2.data[i + 3];
                    dataList.push({
                        MaterialCode: item["物料编码"],
                        AttrCode: self.grid2.data[1][name],
                        AttrName: self.grid2.data[2][name],
                        AttrType: self.grid2.data[0][name],
                        AttrValue: item[name]
                    });
                }
            }

            return dataList;
        }

        function save1ButtonHandler() {
            var data1 = getData1();
            //var data2 = getData2();

            if (data1.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_12'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_13') });
            var url = commonService.getMesApiAddress('material') + "/Base_Material/SaveImportExcel";
            commonService.callWebApiPost(url, {
                data1: data1
                // data2: data2,
            }).then(onSaveSuccess, onSaveError);

        }
        //*****************material end************** */

        //*****************factory start************** */
        function changeFile13(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel13, 500);
            }
        }

        function importExcel13(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_1'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });

            // self.grid3Json = XLSX.utils.sheet_to_json(wb.Sheets[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_14')]);
            self.grid3Json = XLSX.utils.sheet_to_json(wb.Sheets["物料工厂主数据"]);
            self.grid3 = canvasDatagrid();
            document.getElementById("sittab13").innerHTML = "";
            document.getElementById("sittab13").appendChild(self.grid3);
            self.grid3.data = self.grid3Json;
            // self.grid2Json = XLSX.utils.sheet_to_json(wb.Sheets[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_15')]);
            self.grid2Json = XLSX.utils.sheet_to_json(wb.Sheets["物料工厂属性数据"]);
            self.grid2 = canvasDatagrid();
            document.getElementById("sittab2").innerHTML = "";
            document.getElementById("sittab2").appendChild(self.grid2);
            self.grid2.data = self.grid2Json;
        }

        function getData3() {
            var dataList = [];
            for (var i = 0; i < self.grid3.data.length; i++) {
                var item = self.grid3.data[i];
                // dataList.push({
                //     FactoryCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_16')],
                //     // FactoryCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_16')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_17') ? "3001" : "2001",
                //     MaterialCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_5')],
                //     Warehouse: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_18')],
                //     ProcureType: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_19')],
                //     ProcessRoute: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_20')],
                //     IsUsed: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_21')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_22') ? true : false,
                //     SafeStock: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_23')],
                // });

                dataList.push({
                    FactoryCode: item["工厂名称"],
                    // FactoryCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_16')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_17') ? "3001" : "2001",
                    MaterialCode: item["物料编码"],
                    Warehouse: item["库存地点"],
                    ProcureType: item["采购类型"],
                    ProcessRoute: item["生产工艺路线"],
                    IsUsed: item["批次管理"] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_22') ? true : false,
                    SafeStock: item["安全库存"],
                });
            }
            return dataList;
        }

        function save13ButtonHandler() {
            var data3 = getData3();
            var data2 = getData2();
            if (data3.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_12'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }
            var post = {
                data: data3,
                data1: data2
            }


            console.log("122222222222222" + JSON.stringify(post));
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_13') });
            var url = commonService.getMesApiAddress('material') + "/Base_MaterialFactory/SaveImportExcel";
            commonService.callWebApiPost(url, {
                data: data3,
                data1: data2
            }).then(onSaveSuccess, onSaveError);

        }


        //*****************factory end************** */


        //****************bom start********************* */
        function changeFile2(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel2, 500);
            }
        }

        function importExcel2(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_1'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid4Json = XLSX.utils.sheet_to_json(wb.Sheets["BOM"]);
            self.grid4 = canvasDatagrid();
            document.getElementById("sittab4").innerHTML = "";
            document.getElementById("sittab4").appendChild(self.grid4);
            self.grid4.data = self.grid4Json;

            // self.grid5Json = XLSX.utils.sheet_to_json(wb.Sheets[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_24')]);
            self.grid5Json = XLSX.utils.sheet_to_json(wb.Sheets["BOM属性"]);
            self.grid5 = canvasDatagrid();
            document.getElementById("sittab5").innerHTML = "";
            document.getElementById("sittab5").appendChild(self.grid5);
            self.grid5.data = self.grid5Json;
        }
        function getData4() {
            var dataList = [];
            for (var i = 0; i < self.grid4.data.length; i++) {
                var item = self.grid4.data[i];
                // dataList.push({
                //     FactoryCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_25')],
                //     FactoryName: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_16')],
                //     OrderType: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_26')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_27') ? "1" : "2",
                //     MaterialCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_28')],
                //     MaterialName: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_29')],
                //     UnitNum: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_30')],
                //     Process: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_31')],
                //     BOMType: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_32')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_33') ? "1" : "2",
                //     isDefault: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_34')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_22') ? true : false
                // });

                dataList.push({
                    FactoryCode: item["工厂编码"],
                    FactoryName: item["工厂名称"],
                    OrderType: item["订单类型"] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_27') ? "1" : "2",
                    MaterialCode: item["产品编码"],
                    MaterialName: item["产品名称"],
                    UnitNum: item["单位数量"],
                    Process: item["工艺路线编码"],
                    BOMType: item["BOM类型"] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_33') ? "1" : "2",
                    isDefault: item["是否默认"] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_22') ? true : false
                });
            }
            return dataList;

        }
        function getData5() {
            var dataList = [];
            for (var i = 0; i < self.grid5.data.length; i++) {
                var item = self.grid5.data[i];
                // dataList.push({
                //     OrderType: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_26')] == commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_27') ? "1" : "2",
                //     ProductCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_28')],
                //     MaterialCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_5')],
                //     MaterialName: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_6')],
                //     BOMCode: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_35')],
                //     Num: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_36')],
                //     Warehouse: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_18')],
                //     ConsumeProcess: item[commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_37')],
                // });

                dataList.push({
                    OrderType: item["订单类型"] == "出口" ? "1" : "2",
                    ProductCode: item["产品编码"],
                    MaterialCode: item["物料编码"],
                    MaterialName: item["物料名称"],
                    BOMCode: item["BOM编码"],
                    Num: item["数量"],
                    Warehouse: item["仓库编码"],
                    ConsumeProcess: item["分配工序编码"],
                });
            }
            return dataList;
        }


        function save2ButtonHandler() {
            var data4 = getData4();
            var data5 = getData5();

            if (data4.length < 1 || data5.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_12'), commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
                return;
            }
            var post = {
                data1: data4,
                data2: data5
            }


            console.log("122222222222222" + JSON.stringify(post));
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_13') });
            var url = commonService.getMesApiAddress('material') + "/BS_BOM/SaveImportExcel";
            commonService.callWebApiPost(url, {
                data1: data4,
                data2: data5
            }).then(onSaveSuccess, onSaveError);
        }
        //*******************bom end******************* */




        function base64ToFile(base64Str) {
            var bstr = atob(base64Str), n = bstr.length, u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], "", { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        }

        function onSaveSuccess(data) {
            busyIndicatorService.hide();
            if (data.data.success == true) {
                commonService.showInfo(data.data.returnMsg);
            }
            else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_2'));
            }
        }
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_38'));
        }
        // Internal function to make item-specific buttons visible
        function setButtonsVisibility(visible) {
            self.isButtonVisible = visible;
        }
    }

    ListScreenRouteConfig.$inject = ['$stateProvider'];
    function ListScreenRouteConfig($stateProvider) {
        var moduleStateName = 'home.Siemens_SimaticIT_MaterialApp_ImportData';
        var moduleStateUrl = 'Siemens.SimaticIT_MaterialApp_ImportData';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/ImportData';

        var state = {
            name: moduleStateName + '_ImportData',
            url: '/' + moduleStateUrl + '_ImportData',
            views: {
                'Canvas@': {
                    templateUrl: moduleFolder + '/ImportData-list.html',
                    controller: ListScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.MaterialApp.ImportData.ImportDatalistctrl.Tips_39'
            }
        };
        $stateProvider.state(state);
    }
}());
