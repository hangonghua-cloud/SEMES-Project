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
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_7'));
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
            self.typeIsAdd = {
                value: { ItemName: "新建", ItemValue: "0" },
                options: [
                    { ItemName: "新建", ItemValue: "0" },
                    { ItemName: "追加", ItemValue: "1" },
                ]
            }
        }
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function importExcel(contents) {
            // debugger
            var file = base64ToFile(contents);
            if (!file) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_2'), "提示");
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });
            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["生产计划工单"]);
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
                item["交货日期"] = formatDate(item["交货日期"]);
                if (item["纸盒日期"]) {
                    item["纸盒日期"] = formatDate(item["纸盒日期"]);
                }
            });
            return grid;
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

        function getData() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                dataList.push({
                    FactoryName: item["工厂"],
                    ProductOrder: item["订单号"],
                    CustomerPO: item["客户PO号"],
                    ContainerNO: item["柜号"],
                    MaterialCode: item["客户型号"],
                    OrderPieces: item["总片数"],
                    OrderBox: item["总盒数"],
                    //OrderPallet: item["总托数"],
                    //OrderStartPallet: item["起始托号"],
                    PackPalletNum: item["包装托盘编码"],
                    AvoidProduce: item["是否免产"] == '否' ? false : true,
                    Creator: self.UserCode,
                    GiveTime: item["交货日期"],
                    BoxDate: item["纸盒日期"],
                    Harbour: item["港口"],
                    Remark: item["备注"]
                });
            }
            return dataList;
        }

        /*数据校验逻辑 start*/
        //工单检查
        function CheckWorkOrderRequied(grid) {
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
        //工单类型检验
        function CheckWorderType(grid) {
            if (grid == null || grid == undefined) return "";
            var errorRow = 0;
            var checkCols = new Array("总片数", "总盒数", "总托数", "起始托号");
            var errorRow = 0;
            var errorCol = "";
            var errorTemp = "第errorRow行errorCol列非有效数字!</br>"
            var errorContent = ""
            grid.forEach((item, index, arr) => {
                checkCols.forEach(function (col) {
                    if (!isNumber(item[col])) {
                        errorRow = index + 1;
                        errorCol = col;
                        errorContent += errorTemp.replace(/errorRow/, errorRow).replace(/errorCol/, errorCol);
                        return errorContent;
                    }
                });
            });
            return errorContent;
        }
        /*数据校验逻辑 end*/
        //验证是否为>0 数字
        function isNumber(val) {
            if (typeof (val) == "undefined" || val == "") return false;
            var regPos = /^[0-9]+(.[0-9]{0,2})?$/; //非负浮点数如果带小数 最多2位
            if (regPos.test(val)) {
                return true;
            } else {
                return false;
            }

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
            // var errwOrderRow = CheckWorkOrderRequied(self.grid1Json);
            // if (errwOrderRow > 0) {
            //     backendService.genericError("工单中第" + errwOrderRow + "行数据不完整", "数据校验");
            //     return;
            // }

            //工单日期类型检验
            // var WordertypeError = CheckWorderType(self.grid1Json);
            // if (WordertypeError.length > 0) {
            //     backendService.genericError(WordertypeError);
            //     return;
            // }
            //订单号重复
            var dataList = getData();

            if (dataList.length == 0) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_3'), "提示");
            }
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_5') });
            var url = commonService.getMesApiAddress('plan') + "/PL_WorkOrder/SavePL_WorkOrder";
            commonService.callWebApiPost(url, {
                data1: dataList,
                isAdd: self.typeIsAdd.value.ItemValue
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
            name: screenStateName + '.importWrd',
            url: '/importWrd',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/ProductionOrder-importWrd.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.PlanApp.Plan.importJS.Tips_7'
            }
        };
        $stateProvider.state(state);
    }
}());
