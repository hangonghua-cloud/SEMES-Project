(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.ProductionApp.PrinterOrder').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.ProductionApp.PrinterOrder.PrinterOrder.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.importJS.Tips_1'));
            sidePanelManager.open({
                mode: 'e',
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.OrderDate = new Date();
            self.currentItem = {};
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.changeFile = changeFile;
            self.importExcel = function () {
                importExcel(self.file.contents);
            };
        }


        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function importExcel(contents) {

            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.importJS.Tips_2'), "提示");
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["印刷订单"]);
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
                if (!item["版筒号"]) item["版筒号"] = "";
                if (!item["花色号"]) item["花色号"] = "";
                if (!item["卷#"]) item["卷#"] = "";
                if (!item["托数"]) item["托数"] = "";
                if (!item["打包"]) item["打包"] = "";
                if (!item["备注"]) item["备注"] = "";

            });
            return grid;
        }

        function getData1() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                dataList.push({
                    FactoryName: item["工厂"],
                    PrinterOrder: item["订单号"],
                    PlanOrder: item["生产计划号"],
                    OrderDate: item["下单日期"],
                    DeliveryDate: item["交货日期"],
                    MaterialCode: item["客户型号"],
                    Cylinder: item["版筒号"],
                    DesignColour: item["花色号"],
                    MeterNum: item["米数"],
                    ReelNum: item["卷数"],
                    Reel: item["卷#"],
                    Pallet: item["托数"],
                    Packaging: item["打包"],
                    Remark: item["备注"],
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
            var checkCols = new Array("下单日期", "交货日期");
            var errorRow = 0;
            var errorCol = "";
            var errorTemp = "第errorRow行errorCol列日期格式不对!</br>"
            var errorContent = ""
            grid.forEach((item, index, arr) => {
                checkCols.forEach(function (col) {
                    if (!isDate(item[col])) {
                        errorRow = index + 1;
                        errorCol = col;
                        errorContent += errorTemp.replace(/errorRow/, errorRow).replace(/errorCol/, errorCol);
                        return errorContent;
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
            // debugger;
            var time = new Date((numb - 1) * 24 * 3600000 + 1)
            console.log(time);
            time.setYear(time.getFullYear() - 70)
            function formatFunc(str) {    //格式化显示
                return str > 9 ? str : '0' + str
            }
            time.setHours(-24);//时间会多一天
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
                backendService.genericError(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.importJS.Tips_3'), "提示");
            }
            var PrdtypeError = CheckPrdType(self.grid1Json);
            if (PrdtypeError.length > 0) {
                backendService.genericError(PrdtypeError);
                return;
            }

            var postData = {
                KeyValue: '',
                data: dataList1
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.importJS.Tips_4') });
            var url = commonService.getMesApiAddress("ProduceManage") + 'PM_PrinterWorkOrder/ImportPM_PrinterWorkOrder';
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
            //console.log("保存成功----------------" + JSON.stringify(data));
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.ProductionApp.PrinterOrder.importJS.Tips_5'));
                //刷新局部
                $rootScope.$emit('to-parent', 'parent');
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, "操作出错");
            }
        }

        //保存失败事件
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
        var screenStateName = 'home.Siemens_SimaticIT_ProductionApp_PrinterOrder_PrinterOrder';
        var moduleFolder = 'Siemens.SimaticIT.ProductionApp/modules/PrinterOrder';

        var state = {
            name: screenStateName + '.import',
            url: '/import',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/PrinterOrder-import.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.ProductionApp.PrinterOrder.importJS.Tips_1'
            }
        };
        $stateProvider.state(state);
    }
}());
