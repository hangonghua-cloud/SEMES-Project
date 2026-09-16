(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.PlanApp.Plan').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.PlanApp.Plan.ProductionOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'i18nService', 'FileUploader', 'commonService', 'common.services.authentication', 'common.widgets.busyIndicator.service'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, i18nService, FileUploader,
        commonService, auth, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = null;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            GetUserInfo();

            self.changeFile = changeFile;
            self.importExcel = function () {
                importExcel(self.file.contents);
            };
        }
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function importExcel(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_2'), "提示");
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["生产订单"]);

            self.grid1 = canvasDatagrid();
            document.getElementById("sittab1").innerHTML = "";
            document.getElementById("sittab1").appendChild(self.grid1);
            self.grid1.data = gridFormatDate(self.grid1Json);

            self.grid2Json = XLSX.utils.sheet_to_json(wb.Sheets["生产计划工单"]);
            self.grid2 = canvasDatagrid();
            document.getElementById("sittab2").innerHTML = "";
            document.getElementById("sittab2").appendChild(self.grid2);
            self.grid2.data = self.grid2Json;
        }


        function changeFile(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel, 500);
            }
        }
        function base64ToFile(base64Str) {
            var bstr = atob(base64Str), n = bstr.length, u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], "", { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        }
        function gridFormatDate(grid) {
            grid.forEach((item, index, arr) => {
                item["下单日期"] = formatDate(item["下单日期"]);
                item["交货日期"] = formatDate(item["交货日期"]);
                item["纸盒日期"] = formatDate(item["纸盒日期"]);
            });
            return grid;
        }


        function formatDate(numb) {
            var time = new Date((numb - 1) * 24 * 3600000 + 1);
            console.log(time);
            time.setYear(time.getFullYear() - 70)
            function formatFunc(str) {    //格式化显示
                return str > 9 ? str : '0' + str
            }
            // time.setHours(-24);//时间会多一天
            var year = time.getFullYear();
            var mon = formatFunc(time.getMonth() + 1);
            var day = formatFunc(time.getDate());
            //閏年
            if (year % 4 == 0 && year % 100 != 0 | year % 400 == 0) {
                if (parseInt(mon) > 2) {
                    time.setHours(-24);
                    mon = formatFunc(time.getMonth() + 1);
                    day = formatFunc(time.getDate());
                }
            }
            var dateStr = year + '-' + mon + '-' + day;
            return dateStr;
        }


        function getData1() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                CheckRequied(item);
                dataList.push({
                    ProductOrder: item["订单号"],
                    Customer: item["客户"],
                    OrderType: item["订单类型"] == "内销" ? 1 : 2,
                    ProductPlanNo: item["生产计划号"],
                    OrderDate: item["下单日期"],
                    DeliveryDate: item["交货日期"],
                    InsStBoxDateandard: item["纸盒日期"],
                    Technology: item["工艺要求"]
                });
            }
            return dataList;
        }

        function getData2() {
            var dataList = [];
            for (var i = 0; i < self.grid2.data.length; i++) {
                var item = self.grid2.data[i];
                dataList.push({
                    FactoryCode: item["工厂"],
                    ProductOrder: item["订单号"],
                    CustomerPO: item["客户PO号"],
                    ContainerNO: item["柜号"],
                    MaterialCode: item["客户型号"],
                    OrderPieces: item["总片数"],
                    OrderBox: item["总盒数"],
                    OrderPallet: item["总托数"],
                    OrderStartPallet: item["起始托号"],
                    AvoidProduce: item["是否免产"] == '否' ? false : true,
                });
            }
            return dataList;
        }

        /*数据校验逻辑 start*/
        //1.校验数据存在
        function CheckRequied(grid) {
            var errorRow = 0;
            grid.forEach((item, index, arr) => {
                var len = Object.keys(item).length;

                if (len < 9) {
                    errorRow = index + 1;
                    return errorRow;
                }
            });
            return errorRow;
        }
        /*数据校验逻辑 end*/

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            //校验
            var errRow = CheckRequied(self.grid1Json);
            if (errRow > 0) {
                backendService.genericError("生产订单中第" + errRow + "行数据不完整", "数据校验");
                return;
            }
            //校验
            //self.uploader.uploadAll();
            var dataList1 = getData1();
            var dataList2 = getData2();
            if (dataList1.length == 0 || dataList2.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_3'), "提示");
            }
            //var url = commonService.getMesApiAddress('plan') + "/PL_ProductionOrder/SaveBatchPL_ProductionOrder";
            //commonService.callWebApiPost(url, {
            //    data1: dataList1,
            //    data2: dataList2
            //}).then(onSaveSuccess, onSaveError);
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


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.import',
            url: '/import',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-import.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
