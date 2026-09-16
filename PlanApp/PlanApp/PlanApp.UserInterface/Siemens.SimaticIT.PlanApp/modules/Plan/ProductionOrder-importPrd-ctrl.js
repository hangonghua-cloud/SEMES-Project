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
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_4'));
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
            self.updateCustomer = updateCustomer;
            GetUserInfo();

            self.changeFile = changeFile;
            self.importExcel = function () {
                importExcel(self.file.contents);
            };
        }

        function updateCustomer() {
            window.location.href = "Browser://";
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
        }

        function changeFile(oldFile, newFile) {
            if (newFile) {
                setTimeout(self.importExcel, 500);
            }
        }
        function gridFormatDate(grid) {
            grid.forEach((item, index, arr) => {
                item["下单日期"] = formatDate(item["下单日期"]);
                item["交货日期"] = formatDate(item["交货日期"]);
                if (item["纸盒日期"] != 'NaN-0NaN-0NaN') {
                    item["纸盒日期"] = formatDate(item["纸盒日期"]);
                }

            });
            return grid;
        }

        function getData1() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                dataList.push({
                    ProductOrder: item["订单号"],
                    Customer: item["客户"],
                    OrderType: item["订单类型"] == "出口" ? 1 : 2,
                    ProductPlanNo: item["生产计划号"],
                    OrderDate: item["下单日期"],
                    DeliveryDate: item["交货日期"],
                    BoxDate: item["纸盒日期"] == "NaN-0NaN-0NaN" ? '' : item["纸盒日期"],
                    Salesman: item["业务员"],
                    Technology: item["工艺要求"],
                    Remark: item["备注"],
                    CreateBy: self.UserCode,
                });
            }
            return dataList;
        }

        /*数据校验逻辑 start*/
        //1.校验生产订单数据存在
        function CheckPrdOrderRequied(grid) {
            if (grid == null || grid == undefined) return "";
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
        //生产订单类型检验
        function CheckPrdType(grid) {
            if (grid == null || grid == undefined) return "";
            var checkCols = new Array("下单日期", "交货日期", "纸盒日期");
            var errorRow = 0;
            var errorCol = "";
            var errorTemp = "第errorRow行errorCol列日期格式不对!</br>"
            var errorContent = ""
            grid.forEach((item, index, arr) => {
                checkCols.forEach(function (col) {
                    if (item[col] != 'NaN-0NaN-0NaN') {

                        if (!isDate(item[col])) {
                            errorRow = index + 1;
                            errorCol = col;
                            errorContent += errorTemp.replace(/errorRow/, errorRow).replace(/errorCol/, errorCol);
                            return errorContent;
                        }
                    }
                });
            });
            return errorContent;
        }
        //grid判断列数据是否重复
        function isUniqueCol(grid, col) {
            var array = [];
            var errorContent = "";
            if (grid == null || (col.length == 0)) return null;
            grid.forEach((item, index, arr) => {
                var val = item[col];
                if (array.indexOf(val) < 0) {
                    array.push(val);
                }
                else {
                    errorContent = "第" + (index + 1) + "行" + col + "数据重复!";
                    return errorContent;
                }
            });
            return errorContent;
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
            const START_TIME = '1900/01/01';  
            let duration = numb - 1
            // 1900/2/29的num为60
            if (numb > 60) {
            // 对于num大于60的需解析日期,要减去多的1900/2/29日的那一天
            duration = numb - 2
            }
            return moment(START_TIME).add(moment.duration({ 'days': duration })).format('YYYY-MM-DD');
        }
        
        function base64ToFile(base64Str) {
            var bstr = atob(base64Str), n = bstr.length, u8arr = new Uint8Array(n);
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], "", { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
        }

        function save() {

            //校验
            // var errPrdRow = CheckPrdOrderRequied(self.grid1Json);
            // if (errPrdRow > 0) {
            //     backendService.genericError("生产订单中第" + errPrdRow + "行数据不完整", "数据校验");
            //     return;
            // }
            //生产订单日期类型检验
            var PrdtypeError = CheckPrdType(self.grid1Json);
            if (PrdtypeError.length > 0) {
                backendService.genericError(PrdtypeError);
                return;
            }
            //订单号重复
            var isUniqueerror = isUniqueCol(self.grid1Json, "订单号");
            if (isUniqueerror.length > 0) {
                backendService.genericError(isUniqueerror);
                return;
            }

            var dataList1 = getData1();
            if (dataList1.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_3'), "提示");
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_5') });
            var url = commonService.getMesApiAddress('plan') + "/PL_ProductionOrder/SaveBatchPL_ProductionOrder";
            commonService.callWebApiPost(url, {
                data1: dataList1
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

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_PlanApp_Plan_ProductionOrder';
        var moduleFolder = 'Siemens.SimaticIT.PlanApp/modules/Plan';

        var state = {
            name: screenStateName + '.importPrd',
            url: '/importPrd',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-importPrd.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_4'
            }
        };
        $stateProvider.state(state);
    }
}());
