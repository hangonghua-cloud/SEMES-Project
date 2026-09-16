(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.MaterialApp.BSTraitManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.MaterialApp.BSTraitManage.BS_TraitManage.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$rootScope', 'i18nService'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $rootScope, i18nService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            sidePanelManager.setTitle('导入特征值');
            sidePanelManager.open({
                mode: 'e',
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.TraitManageID = self.currentItem.Id;
            self.TraitName = self.currentItem.TraitName;
            //Initialize Model Data
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
                backendService.genericError("请选择Excel！", "提示");
                return;
            }
            var wb = XLSX.read(contents, { type: "base64" });

            self.grid1Json = XLSX.utils.sheet_to_json(wb.Sheets["特征值"]);
            self.grid2Json = XLSX.utils.sheet_to_json(wb.Sheets[self.TraitName]);
            self.grid1 = canvasDatagrid();
            document.getElementById("sittab1").innerHTML = "";
            document.getElementById("sittab1").appendChild(self.grid1);
            // self.grid1.data = gridFormatDate(self.grid1Json);
            self.grid1.data = self.grid1Json;

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
        // function gridFormatDate(grid) {
        //     grid.forEach((item, index, arr) => {
        //         //item["下单日期"] = formatDate(item["下单日期"]);
        //         //item["交货日期"] = formatDate(item["交货日期"]);
        //         //item["纸盒日期"] = formatDate(item["纸盒日期"]);
        //         item["特征值"] = formatDate(item["特征值"]);
        //     });
        // return grid;
        // }


        function formatDate(numb) {
            var time = new Date((numb - 1) * 24 * 3600000 + 1)
            console.log(time);
            time.setYear(time.getFullYear() - 70)
            function formatFunc(str) {    //格式化显示
                return str > 9 ? str : '0' + str
            }
            var year = time.getFullYear();
            var mon = formatFunc(time.getMonth() + 1);
            var day = formatFunc(time.getDate() - 1);
            //var hour = formatFunc(time.getHours());
            //var min = formatFunc(time.getMinutes());
            // var sec = formatFunc(time.getSeconds());
            var dateStr = year + '-' + mon + '-' + day;
            return dateStr;
        }

        function getTraitValue() {
            var dataList = [];
            for (var i = 0; i < self.grid1.data.length; i++) {
                var item = self.grid1.data[i];
                //  CheckRequied(item);
                dataList.push({
                    TraitValue: item["特征值"],
                    AttrModleCode: item["特征值属性模板"]
                }
                );
            }
            return dataList;
        }

        function getTraitArrtValue() {
            var dataList = [];
            var num = Object.keys(self.grid2Json[0]).length

            for (var i = 0; i < self.grid2.data.length - 3; i++) {
                for (var j = 1; j < Object.keys(self.grid2Json[0]).length; j++) {
                    var name = "属性" + (j);
                    var item = self.grid2.data[i + 3];
                    dataList.push({
                        TraitValue: item["特征值"],
                        AttrCode: self.grid2.data[1][name],
                        AttrName: self.grid2.data[2][name],
                        AttrType: self.grid2.data[0][name],
                        AttrValue: item[name]
                    });
                }
            }

            return dataList;
        }




        /*数据校验逻辑 start*/
        //1.校验数据存在
        // function CheckRequied(grid) {
        //     var errorRow = 0;
        //     grid.forEach((item, index, arr) => {
        //         var len = Object.keys(item).length;

        //         if (len < 9) {
        //             errorRow = index + 1;
        //             return errorRow;
        //         }
        //     });
        //     return errorRow;
        // }
        /*数据校验逻辑 end*/

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {

            //校验
            // var errRow = CheckRequied(self.grid1Json);
            // if (errRow > 0) {
            //     backendService.genericError("生产订单中第" + errRow + "行数据不完整", "数据校验");
            //     return;
            // }
            //校验
            //self.uploader.uploadAll();

            self.currentItem.CreateName = self.UserName;
            self.currentItem.Creator = self.UserCode;
            if (self.UserName == null || self.UserName == '' || self.UserName == undefined) {
                self.currentItem.CreateName = self.UserCode;
            }
            var dataList1 = getTraitValue();
            if (dataList1.length == 0) {
                backendService.genericError("请先导入数据！", "提示");
            }
            var dataListArrt = getTraitArrtValue();
            let post = {
                data1: dataList1,
                TraitManageID: self.TraitManageID,
                dataArrt: dataListArrt,
                TraitCode: self.currentItem.TraitCode

            };
            console.log("pf11111111111111111" + JSON.stringify(post));
            debugger;
            var url = commonService.getMesApiAddress('material') + "/BS_TraitDetails/SaveImportExcel";
            commonService.callWebApiPost(url, {
                dataArrt: dataListArrt,
                data1: dataList1,
                TraitManageID: self.TraitManageID,
                CreateName: self.currentItem.CreateName,
                Creator: self.currentItem.Creator,
                TraitCode: self.currentItem.TraitCode
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


        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_MaterialApp_BSTraitManage_BS_TraitManage';
        var moduleFolder = 'Siemens.SimaticIT.MaterialApp/modules/BSTraitManage';

        var state = {
            name: screenStateName + '.import',
            url: '/import/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/BSTraitManage-import.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: '导入特征值'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
