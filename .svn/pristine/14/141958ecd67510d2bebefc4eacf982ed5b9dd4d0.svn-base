(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.BasicDataManageFBApp.AppModelManage.AppModelScreen.service', '$state', '$stateParams', 'common.base', '$filter',
        '$scope', 'common.services.authentication', 'common.widgets.notificationTile.globalService', '$uibModal'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, auth, notificationService, $modal) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();
            GetUserInfo();
            initddls();
            GetOwnedBtnList();

            sidePanelManager.setTitle('添加PDA功能');
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = {};
            self.validInputs = false;
            setTimeout(() => {
                $('#test-form-input').val('#1c97f5');
            }, 100);

            self.UserId = '';
            self.UserCode = '';
            self.UserName = '';

            self.PDAOwnedPageList = [];
            self.PDAOwnedBtnList = [];
            self.isPage = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.selectIco = selectIco;
            self.ModelTypeChange = ModelTypeChange;
        }

        //获取登录用户信息
        function GetUserInfo() {
            var user = auth.getUser();
            self.UserId = user['nameid'];
            self.UserCode = user['unique_name'];
            self.UserName = user['urn:fullname'];
        }

        function initddls() {
            self.ModelTypeList = [{ 'ItemCode': '页面', 'ItemName': '页面' }, { 'ItemCode': '按钮', 'ItemName': '按钮' }];
            self.PDAOwnedPageList = [{ 'ItemCode': 'ProduceModel', 'ItemName': '生产模块' },
            { 'ItemCode': 'QualityModel', 'ItemName': '质量模块' }, { 'ItemCode': 'EquipmentModel', 'ItemName': '设备模块' },
            { 'ItemCode': 'WMSModel', 'ItemName': '物料模块' }];
        }
        //获取数据字典数据
        //function GetDectionary() {
        //    var obj = ['PDAFunctionType', 'PDAModeList'];//可以一次查询多个
        //    var options = GetDictionaryFilter(obj);
        //    dataService.getDectionary(options).then(function (data) {
        //        if ((data) && (data.succeeded)) {
        //            //如果只查询一项则不需要再进行下面的jinq查询
        //            self.ModelTypeList = new jinqJs()
        //                .from(data.value)
        //                .where(function (item) { return (item.CategoryCode == 'PDAFunctionType'); })
        //                .orderBy({ field: 'SortNum', sort: 'asc' })
        //                .select([{ field: 'ItemCode' }, { field: 'ItemName' }]);
        //            // self.ModelType = self.OwnedPageList[0];//赋默认值
        //            self.PDAOwnedPageList = new jinqJs()
        //                .from(data.value)
        //                .where(function (item) { return (item.CategoryCode == 'PDAModeList'); })
        //                .orderBy({ field: 'SortNum', sort: 'asc' })
        //                .select([{ field: 'ItemCode' }, { field: 'ItemName' }]);
        //        } else {
        //            self.ModelTypeList = [];
        //        }
        //    }, backendService.backendError);
        //}

        //获取按钮所属页面数据
        function GetOwnedBtnList() {
            var options = "$filter=1 eq 1 and ModelType eq '页面' ";
            options += "&$orderby=OrderByNum asc";
            dataService.getAll(options).then(function (data) {
                if ((data) && (data.succeeded)) {
                    self.PDAOwnedBtnList = new jinqJs()
                        .from(data.value)
                        .where()
                        .orderBy({ field: 'OrderByNum', sort: 'asc' })
                        .select([{ field: 'ModelCode', text: 'ItemCode' }, { field: 'ModelName', text: 'ItemName' }]);
                } else {
                    self.PDAOwnedBtnList = [];
                }
            }, backendService.backendError);
        }

        function ModelTypeChange(o, n) {
            if (n.ItemName == '页面') {
                self.OwnedPageList = self.PDAOwnedPageList;
                self.isPage = true;
            } else if (n.ItemName == '按钮') {
                self.OwnedPageList = self.PDAOwnedBtnList;
                self.isPage = false;
            }
        }

        function selectIco() {
            var modalInstance = $modal.open({
                templateUrl: 'Siemens.SimaticIT.BasicDataManageFBApp/modules/AppModelManage/AppModelScreen-select.html',
                controller: 'AppModelScreen-select',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: {
                        selectedItem: {
                            icoClass: $('#icoClass1')[0].className,
                            icoName: $('#icoName1').html(),
                            selectedName: $('#selectedName1').html()
                        }
                    }
                }
            });
            //
            modalInstance.result.then(function (data, equip) {
                self.currentItem.ModelIco = 'customicon ' + data.icoName.substring(1, data.icoName.length);
                self.currentItem.ModelIcoName = data.selectedName;
                self.currentItem.ModelIcoClass = data.icoClass;
                $('#selectedName1').html(data.selectedName);
                $('#icoClass1').attr('class', data.icoClass);
                $('#icoName1').html(data.icoName);
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save() {
            var obj = {
                ModelCode: self.currentItem.ModelCode,
                ModelName: self.currentItem.ModelName,
                ModelType: self.currentItem.ModelType.ItemName,
                OwnedPage: self.currentItem.OwnedPage.ItemCode,
                OwnedPageName: self.currentItem.OwnedPage.ItemName,
                ModelIco: self.currentItem.ModelIco,
                ModelIcoName: self.currentItem.ModelIcoName,
                ModelIcoClass: self.currentItem.ModelIcoClass,
                ModelColor: $('#test-form-input').val(),
                OrderByNum: self.currentItem.OrderByNum,
                CreatedByCode: self.UserCode,
                CreatedByName: self.UserCode + '-' + self.UserName,
            };
            debugger
            dataService.create(obj).then(function (data) {
                if ((data) && (data.succeeded)) {
                    notificationService.info(data.data.Result);
                    onSaveSuccess();
                } else {
                    notificationService.warning(data.data.Result);
                }
            }, backendService.backendError);
        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            sidePanelManager.close();
            $state.go('^', {}, { reload: true });
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_BasicDataManageFBApp_AppModelManage_AppModelScreen';
        var moduleFolder = 'Siemens.SimaticIT.BasicDataManageFBApp/modules/AppModelManage';

        var state = {
            name: screenStateName + '.add',
            url: '/add',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/AppModelScreen-add.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            }
        };
        $stateProvider.state(state);
    }
}());
