(function () {
    'use strict';
    angular.module('CCS.CommonApp')
        .service('commonService', CommonService)
        .filter('alpDatetimeFilter', function () {
            return function (inputdt) {
                if (inputdt != null && inputdt != undefined) {
                    return moment(inputdt).utcOffset('+0000').format('YYYY-MM-DD HH:mm:ss');
                }
                return "";

            }
        }).filter('alpDatetimeFilter2', function () {
            return function (inputdt) {
                if (inputdt != null && inputdt != undefined) {
                    return moment(inputdt).utcOffset('+0000').format('YYYY-MM-DD');
                }
                return "";

            }
        }).filter('alpDatetimeFilterMM', function () {
            return function (inputdt) {
                if (inputdt != null && inputdt != undefined) {
                    return moment(inputdt).utcOffset('+0000').format('YYYY-MM-DD HH:mm');
                }
                return "";
            }
        });

    var locationTypes = {
        CN: "CN",
        VN: "VN",
        TH: "TH",
    };

    CommonService.$inject = ['$q', '$state', 'common.base', 'common.services.logger.service', 'common.services.logger.config', 'common.services.authentication',
        '$http', 'common.widgets.notificationTile.globalService', 'common.widgets.busyIndicator.service', '$uibModal', 'FileUploader', '$translate'];
    function CommonService($q, $state, base, loggerService, loggerConfig, auth, $http, notificationService, busyIndicatorService, $modal, FileUploader, $translate) {
        var self = this;
        var logger, backendService, messageservice;
        var uiGridOptionsConfig;
        var urlForm = {
            baseUrl: 'http://172.168.11.122',
            fileUrl: "http://172.168.11.131/MwebAPI/",
            reportUrl: "http://172.168.11.133:9000/",
            apiAdress_mes_integration: baseUrl + "/mes_integration/api/",
        };
        //本地域名
        var baseHost = window.location.host.toLowerCase();
        //接口访问地址

        var baseUrl = GetSessionUrl("baseUrl");
        var fileUrl = GetSessionUrl("fileUrl");
        //报表服务器
        var reportUrl = GetSessionUrl("reportUrl");
        //MES接口基础地址
        var apiAdress_mes_integration = GetSessionUrl("apiAdress_mes_integration");

        activate();
        self.locationTypes = locationTypes;


        function changeLocation() {
            var locationType = window.localStorage.getItem("locationType") ? window.localStorage.getItem("locationType") : "CN"
            var urlFormStorage = sessionStorage.getItem("urlForm") ? JSON.parse(sessionStorage.getItem("urlForm")) : urlForm;
            switch (locationType) {
                case locationTypes.CN:

                    urlFormStorage.baseUrl = 'http://172.168.11.122';//中国服务器
                    urlFormStorage.fileUrl = "http://172.168.11.131/MwebAPI/";
                    urlFormStorage.reportUrl = "http://172.168.11.133:9000/";//报表服务器
                    urlFormStorage.apiAdress_mes_integration = baseUrl + "/mes_integration/api/";
                    break;
                case locationTypes.VN:
                    urlFormStorage.baseUrl = 'http://192.168.19.102';//越南服务器
                    urlFormStorage.fileUrl = "http://192.168.19.102/MwebAPI/";
                    break;
                case locationTypes.TH:
                    urlFormStorage.baseUrl = 'http://172.16.10.102';//泰国服务器
                    urlFormStorage.fileUrl = "http://172.16.10.102/MwebAPI/";
                    break;
                default:
                    break;
            }
            sessionStorage.setItem("urlForm", JSON.stringify(urlFormStorage));

            baseUrl = GetSessionUrl("baseUrl");
            fileUrl = GetSessionUrl("fileUrl");
            reportUrl = GetSessionUrl("reportUrl");
            apiAdress_mes_integration = GetSessionUrl("apiAdress_mes_integration");
            console.log(locationType, baseUrl, fileUrl);
        }
        function GetSessionUrl(type) {
            var urlFormStorage = sessionStorage.getItem("urlForm") ? JSON.parse(sessionStorage.getItem("urlForm")) : urlForm;
            return urlFormStorage[type];
        }


        /*
       text语言包中的文本结构
       parms有参数传参数，结构参考{test:"TEST"}
       */
        self.$t = (text, parms) => {
            return $translate.instant(text, parms)
        };
        self.$translate = $translate;
        self.UA_Language = localStorage.getItem("language");

        function activate() {

            logger = loggerService.getModuleLogger('CCS.CommonApp.common.service');
            backendService = base.services.runtime.backendService;
            messageservice = base.widgets.messageOverlay.service;
            init();
            changeLocation();
            exposeApi();
        }
        function init() {
            self.departmentCode = '';
            self.departmentName = '';
            self.stationCode = '';
            self.stationName = '';
            uiGridOptionsConfig = {
                fastWatch: true,
                rowHeight: 35,
                minimumColumnSize: 100,
                enableMultiSelection: false,
                enableFiltering: false,
                //基础属性
                enableSorting: true,//是否支持排序(列)
                useExternalSorting: false,//是否支持自定义的排序规则      
                enableGridMenu: false,//是否显示表格 菜单
                showGridFooter: false,//时候显示表格的footer
                enableHorizontalScrollbar: 1,//表格的水平滚动条
                enableVerticalScrollbar: 1,//表格的垂直滚动条 (两个都是 1-显示,0-不显示)
                selectionRowHeaderWidth: 30,
                enableCellEditOnFocus: false,//default为false,true的时候单击即可打开编辑(cellEdit为true的时候,需要引入'ui.grid.cellNav')
                //分页属性
                enablePagination: true, //是否分页,default为true
                enablePaginationControls: true, //使用默认的底部分页
                paginationPageSizes: [20, 25, 30, 50, 75, 100], //每页显示个数选项
                paginationPageSize: 25, //每页显示个数
                paginationCurrentPage: 1, //当前的页码  
                totalItems: 0, // 总数量
                useExternalPagination: true,//是否使用分页按钮          
                //选中
                rowTemplate: "<div ng-dblclick=\"grid.appScope.onDblClick(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>",//双击行事件
                enableFooterTotalSelected: true, // 是否显示选中的总数,default为true,如果显示,showGridFooter 必须为true
                enableFullRowSelection: true, //是否点击行任意位置后选中,default为false,当为true时,checkbox可以显示但是不可选中
                enableRowHeaderSelection: true, //是否显示选中checkbox框 ,default为true
                enableRowSelection: true, // 行选择是否可用,default为true;
                enableSelectAll: false, // 选择所有checkbox是否可用，default为true; 
                enableSelectionBatchEvent: true, //default为true
                modifierKeysToMultiSelect: false,//default为false,为true时只能按ctrl或shift键进行多选,这个时候multiSelect必须为true;
                multiSelect: false,// 是否可以选择多个,默认为true;
                noUnselect: false,//default为false,选中后是否可以取消选中         
                appScopeProvider: self,
                columnDefs: [],
                onRegisterApi: null,
                data: []

            };
        }

        function exposeApi() {
            self.changeLocation = changeLocation;
            self.getMesApiAddress = getMesApiAddress;
            self.getBaseApi = baseUrl;
            self.getLoginUser = getLoginUser;
            self.getLoginUseConfig = getLoginUseConfig;
            self.invokeError = invokeError;
            self.getStateSelectConfig = getStateSelectConfig;
            self.getDataItemDuatil = getDataItemDuatil;//数据字典
            self.getResourceExtendInfo = getResourceExtendInfo;//取指定层级下的所有资源
            self.get_ResourceExtendByLevelField = get_ResourceExtendByLevelField;//取指定层级某个属性值得所有资源
            self.getProcessByFactory = getProcessByFactory;//根据工厂编码获取工序
            self.getProcessByFactoryExtendInfo = getProcessByFactoryExtendInfo;//根据工厂、属性获取工序
            self.getWarehouseByFactory = getWarehouseByFactory;//根据工厂编码获取仓库
            self.getWarehouseByFactoryExtendInfo = getWarehouseByFactoryExtendInfo;//根据工厂、属性获取仓库
            self.getResourceListByParentResource = getResourceListByParentResource;//根据ParentResource获取工厂建模列表
            self.getAllFactoryProcessMachine = getAllFactoryProcessMachine;//获取工厂工序机台列表
            self.getKeyParameterItem = getKeyParameterItem;//获取关键参数项目列表
            self.GetList_Lines = GetList_Lines;
            self.FucntionAuthority = FucntionAuthority;
            self.Authority_ArrayFind = Authority_ArrayFind;
            self.getLoginUserDepartmentAndStation = getLoginUserDepartmentAndStation;
            self.showMessages = showMessages;
            self.$odata = $odata;
            self.$callCommand = $callCommand;
            self.callWebApiPost = callWebApiPost;
            self.callWebApiGet = callWebApiGet;
            self.$callWebApi = $callWebApi;
            self.getShiftBeginEndDate = getShiftBeginEndDate;
            self.formatTreeData = formatTreeData;
            self.$callReadFunction = $callReadFunction;
            self.openModel = openModel;//打开模态框
            self.showInfo = showInfo; //显示普通消息框
            self.showWarning = showWarning; //显示警告消息框
            self.showError = showError; //显示错误消息框
            self.showLoading = showLoading; //显示加载中消息框
            self.hideLoading = hideLoading; //隐藏加载中消息框
            self.httpPost = httpPost;//Post方式调用WebApi接口
            self.httpGet = httpGet;//Get方式调用WebApi接口
            self.SelectMaterialModal = SelectSetMaterialModal;//站台品种弹窗
            self.getPageList = getPageList;//根据参数获取分页数据
            self.uiGridOptionsConfig = uiGridOptionsConfig;
            self.openDownloadDialog = openDownloadDialog;
            self.SelectEP_EquipmentModal = SelectEP_EquipmentModal;//设备弹窗 
            self.Select_SingleChoiceModal = Select_SingleChoiceModal;//公用单选弹窗方法  add: 刘万军 2021-1-22
            self.Select_multiSelectModal = Select_multiSelectModal;//公用多选弹窗方法  add: 刘万军 2021-4-20
            self.Select_SingleChoiceModal_RawReceipt = Select_SingleChoiceModal_RawReceipt;
            self.Select_SingleChoiceModalByValue = Select_SingleChoiceModalByValue;//公用单选弹窗方法 
            self.Select_SingleChoiceModalForBarCode = Select_SingleChoiceModalForBarCode;
            self.apiAdress_mes_integration = apiAdress_mes_integration;//MES接口基础地址
            self.ConvertToLocalTime = ConvertToLocalTime;
            self.ConvertToLocalDate = ConvertToLocalDate;
            self.getUserList = getUserList;
            self.printTemplate = printTemplate;
            self.importExcel = importExcel;//导入Excel
            self.SelectSupplierManage = SelectSupplierManage;//供应商
            self.fileUrl = fileUrl;//文件服务器地址
            self.reportUrl = reportUrl;
            self.FileManageModal = FileManageModal;//附件管理弹窗
            self.InitUploader = InitUploader;//初始化Uploader
            self.getfileUrl = getfileUrl;
        }

        /**
         * 功能描述: 通用的打开下载对话框方法
         * 创    建: 刘万军
         * 创建时间: 2021-4-26 9:50:40
         * @param url 下载地址，也可以是一个blob对象，必选
         * @param saveName 保存文件名，可选
         */
        function openDownloadDialog(url, saveName) {
            if (typeof url == 'object' && url instanceof Blob) {
                url = URL.createObjectURL(url); // 创建blob地址
            }
            var aLink = document.createElement('a');
            aLink.href = url;
            aLink.download = saveName || ''; // HTML5新增的属性，指定保存文件名，可以不要后缀，注意，file:///模式下不会生效
            var event;
            if (window.MouseEvent) event = new MouseEvent('click');
            else {
                event = document.createEvent('MouseEvents');
                event.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
            }
            aLink.dispatchEvent(event);
        }


        //根据参数获取分页数据  
        function getPageList(url, pagination, queryJson) {
            var data = {
                pagination: pagination,
                queryJson: queryJson
            };
            var req = callWebApiPost(url, data);
            return req;
        }
        //获取MES系统WebApi地址
        function getMesApiAddress(hostName) {
            var address = baseUrl + "/sitSrvApi/";
            if (hostName != null) {
                if (hostName == "calendar") {
                    address = baseUrl + "/sit-calendarapi/";
                }
                else if (hostName == "factory") {
                    address = baseUrl + "/sit-factoryapi/";
                }
                else if (hostName == "equipment") {
                    address = baseUrl + "/sit-equipmentapi/";
                }
                else if (hostName == "plan") {
                    address = baseUrl + "/sit-planapi/";
                }
                else if (hostName == "quality") {
                    address = baseUrl + "/sit-qualityapi/";
                }
                else if (hostName == "technology") {
                    address = baseUrl + "/sit-technologyapi/";
                }
                else if (hostName == "material") {
                    address = baseUrl + "/sit-materialapi/";
                }
                else if (hostName == "ProduceManage") {
                    address = baseUrl + "/sit-ProduceManageapi/";
                }
                else if (hostName == "plc") {
                    address = baseUrl + "/sit-plcApi/";
                }
                else if (hostName == "map") {
                    address = baseUrl + "/sit-mapApi/";
                }
            }
            //if (window.location.host.toLowerCase() != "ser1") {
            //    address = "http://192.168.3.12:3030/CCSMESWebApi";
            //}
            return address;
        }

        //获取班次的开始结束时间
        function getShiftBeginEndDate(productDate, beginTime, endTime) {
            var res = {
                beginDate: new Date(productDate + " " + beginTime),
                endDate: new Date(productDate + " " + endTime)
            };
            if ((res.endDate.getTime() - res.beginDate.getTime()) <= 0) {
                res.endDate = addDate(res.endDate.format('yyyy-MM-dd HH:mm:ss'), 1);
            }
            return res;
        }

        //格式化树的数据
        function formatTreeData(jsonData, config) {
            //配置数据源的字段
            var defulet = {
                id: 'id',
                parentId: 'parentId',
                text: 'text',
                attributeValue: 'AttributeValue',
                selectedItem: [],
                selectKey: 'id',
            }
            $.extend(defulet, config);
            var treeData = [];
            for (var i = 0; i < jsonData.length; i++) {
                var opened = true;
                var nodeType = "default";
                if (jsonData[i][defulet.parentId] == "0") {
                    jsonData[i][defulet.parentId] = "#";
                }
                if (jsonData[i][defulet.parentId] == "#") {
                    nodeType = "root";
                }
                var selected = false;

                //默认选中
                if (defulet.selectedItem.length > 0 && defulet.selectKey) {
                    if (_.contains(defulet.selectedItem, jsonData[i][defulet.selectKey])) {
                        selected = true;
                    }
                }
                if (!jsonData.some(item => item[defulet.parentId] === jsonData[i][defulet.id])) {
                    nodeType = "file";
                }
                treeData.push(
                    {
                        "id": jsonData[i][defulet.id],
                        "parent": jsonData[i][defulet.parentId],
                        "text": jsonData[i][defulet.text],
                        "type": nodeType,
                        "isTree": jsonData[i][defulet.attributeValue],
                        'state': { 'opened': opened, 'selected': selected }
                    }
                );
            }
            return treeData;
        }

        //获取登录用户信息
        function getLoginUser() {
            return {
                'loginName': auth.getUser()['unique_name'],
                'fullName': auth.getUser()['urn:fullname'],
                'userID': auth.getUser()['nameid']
            };

        }

        //获取登录用户的部门以及岗位信息
        function getLoginUserDepartmentAndStation() {
            var loginName = auth.getUser()['unique_name'];
            return $http.get("/SitSrvApi/TechnologyManage/Process_ModelTree/GetProcessLoginUserMessages?userId=" + loginName);
        }
        //获取登录用户的部门、隔离级别
        function getLoginUseConfig() {
            var url = getMesApiAddress() + 'System/GetUserInfoConfig';
            var loginName = auth.getUser()['unique_name'];
            //alert(loginName);
            // var loginName = "000156";
            // var result = {};
            // var resultStr = localStorage.getItem(loginName);
            // if (resultStr != null && resultStr.length > 0) {
            //     return JSON.parse(resultStr);
            // }
            var params = { "Code": loginName }

            return callWebApiPost(url, params);
        }

        //调用ua的odata
        function $odata(options) {
            return backendService.findAll({
                'appName': options.appName,
                'entityName': options.entityName,
                'options': options.options
            });
        }

        function $callReadFunction(options) {
            backendService.read({
                appName: options.appName,
                functionName: options.functionName,
                params: options.params
            });
        }


        //调用ua的方法
        function $callCommand(options) {
            return backendService.invoke({
                'appName': options.appName,
                'commandName': options.commandName,
                'params': options.params
            });
        }

        function callWebApiPost(url, data) {
            //  console.log("postData----------------" + JSON.stringify(data));
            var config = {
                headers: {
                    'Content-Type': 'application/json;charset=UTF-8',
                    'Language': self.UA_Language,
                    'UserCode': auth.getUser().unique_name,
                    'UserName': encodeURIComponent(auth.getUser()['urn:fullname'])
                }
            };
            return $http.post(url, data, config);
        };

        function callWebApiGet(url, config) {
            var configHeaders = {
                'Content-Type': 'application/json;charset=UTF-8',
                'Language': self.UA_Language,
                'UserCode': auth.getUser().unique_name,
                'UserName': encodeURIComponent(auth.getUser()['urn:fullname'])
            };
            if (!!config) {
                config.headers = configHeaders;
            }
            else {
                config = {
                    headers: configHeaders
                };
            }
            return $http.get(url, config);
        };

        function httpPost(url, data) {
            var config = {
                headers: {
                    'Content-Type': 'application/json;charset=UTF-8',
                    'Language': self.UA_Language,
                    'UserCode': auth.getUser().unique_name,
                    'FullName': encodeURIComponent(auth.getUser()['urn:fullname'])
                }
            };
            var urlApi = getMesApiAddress() + "/" + url;
            return $http.post(urlApi, data, config);
        }

        function httpGet(url, data) {
            var config = {
                headers: {
                    'Content-Type': 'application/json;charset=UTF-8',
                    'Language': self.UA_Language,
                    'UserCode': auth.getUser().unique_name,
                    'FullName': encodeURIComponent(auth.getUser()['urn:fullname'])
                },
                params: data
            };
            var urlApi = getMesApiAddress() + "/" + url;
            return $http.get(urlApi, config);
        }

        //调用接口
        function $callWebApi(type, url, params) {
            if (type === 'post') {
                return $callWebApiPost(url, params);
            } else if (type === 'get') {
                return $callWebApiGet(url, params);
            } else {
                throw "未识别的请求方式!";
            }
        }

        //请求post接口
        function $callWebApiPost(url, params) {
            return $http.post(url, params);
        }

        //请求get接口
        function $callWebApiGet(url, params) {
            var paramStr = '';
            for (const key in params) {
                var keyValue = params[key];
                if (keyValue && typeof (keyValue) == 'string') {
                    keyValue = keyValue.replace("#", "%23");
                }
                if (paramStr.length == 0) {
                    paramStr += ("?" + key + "=" + keyValue);
                } else {
                    paramStr += ("&" + key + "=" + keyValue);
                }
            }
            return $http.get(url + paramStr);
        }

        //请求异常信息
        function invokeError(error) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: "确定",
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: "错误提示",
                text: '[' + error.status + '] - ' + error.data.Message
            });
            messageservice.show();
        }

        //提示信息弹框
        function showMessages(title, messages) {
            messageservice.set({
                buttons: [{
                    id: 'ok',
                    displayName: "确定",
                    onClickCallback: function () {
                        messageservice.hide();
                    }
                }],
                title: title,
                text: messages
            });
            messageservice.show();
        }

        //显示普通信息弹框
        function showInfo(messages) {
            notificationService.info(messages);
        }

        //显示警告信息弹框
        function showWarning(messages) {
            notificationService.warning(messages);
        }

        //显示错误信息弹框
        function showError(messages) {
            backendService.genericError(messages, "错误信息");
        }

        //显示加载中消息框
        function showLoading(options) {
            busyIndicatorService.show(options);
        }

        //隐藏加载中消息框
        function hideLoading() {
            busyIndicatorService.hide();
        }

        //获取状态下拉框默认配置
        function getStateSelectConfig() {
            return [
                { 'text': '启用', value: true },
                { 'text': '停用', value: false }
            ];
        }

        //获取数据字典
        function getDataItemDuatil(enCode) {
            var url = getMesApiAddress() + 'SystemManage/DataItemDetail/GetDataItemListJson_UA?EnCode=' + enCode;
            return $http.get(url);
        }

        function getResourceExtendInfo(data) {
            var url = getMesApiAddress("factory") + 'level/Get_ModelResourceExtendInfo_ByLevelCode';
            return callWebApiPost(url, data);
        }
        function get_ResourceExtendByLevelField(data) {
            var url = getMesApiAddress("factory") + 'level/Get_ResourceExtendByLevelCode';
            return callWebApiPost(url, data);
        }
        function getProcessByFactory(data) {
            var url = getMesApiAddress("factory") + 'level/GetProcessByFactory';
            return callWebApiPost(url, data);
        }
        function getProcessByFactoryExtendInfo(data) {
            var url = getMesApiAddress("factory") + 'level/GetProcessByFactoryExtendInfo';
            return callWebApiPost(url, data);
        }
        //根据工厂获取仓库
        function getWarehouseByFactory(data) {
            var url = getMesApiAddress("factory") + 'level/GetWarehouseByFactory';
            return callWebApiPost(url, data);
        }
        //根据工厂、属性获取仓库
        function getWarehouseByFactoryExtendInfo(data) {
            var url = getMesApiAddress("factory") + 'level/GetWarehouseByFactoryExtendInfo';
            return callWebApiPost(url, data);
        }
        function getResourceListByParentResource(data) {
            var url = getMesApiAddress("factory") + 'level/GetListByParentResource';
            return callWebApiPost(url, data);
        }
        function getAllFactoryProcessMachine() {
            var url = getMesApiAddress("factory") + 'level/GetAllFactoryProcessMachine';
            return callWebApiGet(url, null);
        }
        //获取关键参数项目列表
        function getKeyParameterItem(data) {
            var url = getMesApiAddress("") + 'Base_KeyParameterItem/GetKeyParameterItemList';
            return callWebApiPost(url, data);
        }
        function getUserList(code) {
            var url = getMesApiAddress() + 'Base/GetUserList?Code=' + code;
            return callWebApiGet(url, null);
        }

        //获取产线
        function GetList_Lines(key) {
            var url = getMesApiAddress() + '/Base/GetList_Lines?key=' + key;
            return $http.get(url);
        }
        //获取按钮权限
        function FucntionAuthority(btnNames) {
            var url = getMesApiAddress() + '/Base/FucntionAuthority';
            var params = {
                userId: getLoginUser().userID,
                BtnNames: btnNames
            };
            //console.log("获取按钮权限------params----" + JSON.stringify(params))
            return $http.post(url, params);
        }
        function Authority_ArrayFind(array, itemName) {
            var obj = array.find(a => { return a.FunctionName == itemName });
            return obj.value;
        }

        //打印服务
        function printTemplate(name, data) {
            let toUrl = "http://localhost:8101/api/print?Template=" + name + "&Action=Print";
            //console.log("条码打印服务器配置..." + JSON.stringify(data));
            $.ajax({
                url: toUrl,
                type: "POST",
                dataType: "application/x-www-form-urlencoded; charset=utf-8",
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                data: data,
                success: function (res) {
                    showWarning('打印成功');
                },
                error: function (res) {
                    //console.log(res);
                    //跨域问题未解决
                    showWarning("打印成功");
                }
            });
        }
        //打开模态框
        function openModel(options) {
            return $modal.open(options);
        }
        //供应商
        function SelectSupplierManage(obj) {
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            //url: "Common/GetSetMaterialPageData",
                            url: getMesApiAddress("material") + "Base_SupplierManage/GetBase_SupplierManageList?checkType=",
                            queryParmeters: {
                                Name: "",
                            },
                            multiple: false,
                            isFilter: "0",
                            method: "Get",
                            sidx: "SupplierCode",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: '序号', minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'SupplierCode',
                                    displayName: '供应商编码',
                                    width: 130
                                },
                                {
                                    field: 'SupplierName',
                                    displayName: '供应商名称',
                                    width: 300
                                },
                                {
                                    field: 'Abbr',
                                    displayName: '供应商简称',
                                    width: 150
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    obj.SupplierCode = data[0].SupplierCode;
                    obj.SupplierName = data[0].SupplierName;
                    obj.Abbr = data[0].Abbr;
                }
            });
        }
        //弹出选择模块 START 
        //物料选择
        function SelectSetMaterialModal(obj) {
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectMaterialModal.html',
                controller: 'CCS.CommonApp.CommonUI.SelectMaterialModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            //url: "Common/GetSetMaterialPageData",
                            url: getMesApiAddress() + "Base/GetList_Materials",
                            queryParmeters: {
                                Name: "",
                                IsSeal: "0"
                            },
                            sidx: "Code",
                            sord: "asc",
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: '序号', minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'SetMaterialCode',
                                    displayName: '物料编码',
                                    width: 400
                                },
                                {
                                    field: 'SetMaterialName',
                                    displayName: '物料名称',
                                    width: 400
                                }
                            ],
                        };
                    }
                }
            });
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    obj.SetMaterialCode = data[0].SetMaterialCode;
                    obj.SetMaterialName = data[0].SetMaterialName;
                }
            });
        }

        //功能描述:设备选择
        //创    建:刘万军
        //创建时间:2021-1-22
        //参    数:设置目标(要写入变量的目标)
        function SelectEP_EquipmentModal(obj, obj2) {

            //配置窗口
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/SelectEP_EquipmentModal.html',//前端显示页面 SelectEP_EquipmentModal.html
                controller: 'CCS.CommonApp.CommonUI.SelectEP_EquipmentModal',//前端显示页面对应的 SelectEP_EquipmentModal.JS
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: getMesApiAddress("equipment") + "Equipment/GetPage_Equipment",//API接口
                            result_obj: obj,
                            queryParmeters: {//查询参数,根据实际情况添加多少个
                                EquipmentName: "",
                                EquipmentCode: "",
                                IsSeal: "0"
                            },
                            sidx: "EquipmentCode",//排序字段
                            sord: "asc",//排序方式
                            columnDefs: [//grid显示字段列表
                                {
                                    name: 'rowNum', displayName: '序号', minWidth: 80, width: 90, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'EquipmentCode',
                                    displayName: '设备编码',
                                    width: 400
                                },
                                {
                                    field: 'EquipmentName',
                                    displayName: '设备名称',
                                    width: 400
                                }
                            ],
                        };
                    }
                }
            });
            //选择回调检测
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    //console.log("obj目标----------" + JSON.stringify(data[0]))
                    //目标赋值
                    obj2.EquipmentCode = data[0].EquipmentCode;
                    obj2.EquipmentName = data[0].EquipmentName;
                    obj.ItemName = data[0].EquipmentName;
                }
            });
        }

        /*
        *功能描述: 单选弹窗方法(精工 ui-grid列表), 自定义查询条件\显示列表字段
        *创    建: 刘万军
        *创建时间:2021-1-22
        *参    数:
        *       title    标题(最后生成如: 选择产品型号)
        *       PostUrl API接口
        *       queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':'ERP到料单号','FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':'订单日期','FiledType':'Date'}]
        *       sidx  排序字段
        *       sord  排序方式
        *       columnDefs   grid显示字段列表
        *       callback  回调方法
        */
        function Select_SingleChoiceModal(title, PostUrl, queryParmeters, sidx, sord, columnDefs, callback, myParameters) {
            //配置窗口
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/Select_SingleChoiceModal.html',//前端显示页面 Select_SingleChoiceModal.html
                controller: 'CCS.CommonApp.CommonUI.Select_SingleChoiceModal',//前端显示页面对应的 Select_SingleChoiceModal.JS
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            title: title,
                            url: PostUrl,//API接口 参考 getMesApiAddress() + "Equipment/GetPage_Equipment",
                            queryParmeters: queryParmeters,//参考[{ 'FieldCode': 'purchaseOrderNo', 'FileldName': 'ERP到料单号', 'FiledType': 'Text' }, { 'FieldCode': 'poDate', 'FileldName': '订单日期', 'FiledType': 'Date' }]
                            sidx: sidx,//"EquipmentCode",//排序字段
                            sord: sord,//"asc",//排序方式
                            columnDefs: columnDefs,  // grid 列表字段
                            myParameters: myParameters
                        };
                    }
                }
            });
            //选择回调检测
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    console.log("obj目标----------" + JSON.stringify(data[0]))
                    //目标赋值
                    if (callback) {
                        callback(data[0]);
                    }

                }
            });
        }

        /*
        * 功能描述: 多选弹窗方法(精工 ui-grid列表), 自定义查询条件\显示列表字段
        * 创    建: 刘万军
        * 创建时间:2021-1-22
        * @param title 标题(最后生成如: 选择产品型号)
        * @param  PostUrl API接口
        * @param queryParmeters  查询条件 参考 [{'FieldCode': 'purchaseOrderNo', 'FileldName':'ERP到料单号','FiledType':'Text'},{'FieldCode': 'poDate', 'FileldName':'订单日期','FiledType':'Date'}]
        * @param sidx  排序字段
        * @param sord  排序方式
        * @param columnDefs   grid显示字段列表
        * @param callback  回调方法
        */
        function Select_multiSelectModal(title, PostUrl, queryParmeters, sidx, sord, columnDefs, callback) {
            //配置窗口
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/Select_multiSelectModal.html',//前端显示页面 Select_multiSelectModal.html
                controller: 'CCS.CommonApp.CommonUI.Select_multiSelectModal',//前端显示页面对应的 Select_multiSelectModal.JS
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            title: title,
                            url: PostUrl,//API接口 参考 getMesApiAddress() + "Equipment/GetPage_Equipment",
                            queryParmeters: queryParmeters,//参考[{ 'FieldCode': 'purchaseOrderNo', 'FileldName': 'ERP到料单号', 'FiledType': 'Text' }, { 'FieldCode': 'poDate', 'FileldName': '订单日期', 'FiledType': 'Date' }]
                            sidx: sidx,//"EquipmentCode",//排序字段
                            sord: sord,//"asc",//排序方式
                            columnDefs: columnDefs  // grid 列表字段
                        };
                    }
                }
            });
            //选择回调检测
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    console.log("obj目标----------" + JSON.stringify(data))
                    //目标赋值
                    if (callback) {
                        callback(data);
                    }

                }
            });
        }

        //导入Excel
        function importExcel(contents) {
            var excelData = {
                data: [],
                columnDefs: []
            };
            if (contents) {
                var wb = XLSX.read(contents, { type: "base64" });
                var a = wb.SheetNames[0];
                var b = wb.Sheets[a];//内容为方式2
                var myData = XLSX.utils.sheet_to_json(b);//内容为方式1 
                excelData.data = myData;
                for (var item in myData[0]) {
                    excelData.columnDefs.push({
                        field: item,
                        displayName: item,
                        width: 180
                    });
                }
            }
            return excelData;
        }

        /*功能描述:根据单筛选条件单选弹窗方法
        *创    建:丁零
        *创建时间:2021-5-18
        *参    数:PostUrl API接口
        *         sidx  排序字段
        *         sord  排序方式
        *         columnDefs   grid显示字段列表
        *         callback  回调方法
        */
        function Select_SingleChoiceModalByValue(PostUrl, sidx, sord, columnDefs, callback, myParameters) {
            // console.log("获取列表API接口----------" + PostUrl)
            // console.log("排序字段-----------------" + sidx);
            // console.log("排序方式-----------------" + sord);
            // console.log("grid显示字段列表---------" + JSON.stringify(columnDefs));
            //参数
            let queryParmeters = {};
            if (myParameters) {
                queryParmeters = myParameters;
            }
            if (queryParmeters.queryCode == null) {
                queryParmeters.queryCode = "";
            }
            if (queryParmeters.queryName == null) {
                queryParmeters.queryName = "";
            }
            //配置窗口
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/Select_SingleChoiceModalByValue.html',//前端显示页面 SelectEP_EquipmentModal.html
                controller: 'CCS.CommonApp.CommonUI.Select_SingleChoiceModalByValue',//前端显示页面对应的 SelectEP_EquipmentModal.JS
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: PostUrl,//getMesApiAddress("equipment") + "Equipment/GetPage_Equipment",//API接口
                            //result_obj: obj,
                            queryParmeters: queryParmeters,
                            sidx: sidx,//"EquipmentCode",//排序字段
                            sord: sord,//"asc",//排序方式
                            columnDefs: columnDefs
                        };
                    }
                }
            });
            //选择回调检测
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    //console.log("obj目标----------" + JSON.stringify(data[0]))
                    //目标赋值
                    if (callback) {
                        callback(data[0]);
                    }

                }
            });
        }

        /*功能描述:根据单筛选条件单选弹窗方法
        *创    建:丁零
        *创建时间:2021-6-7
        *参    数:PostUrl API接口
        *         sidx  排序字段
        *         sord  排序方式
        *         columnDefs   grid显示字段列表
        *         callback  回调方法
        */
        function Select_SingleChoiceModalForBarCode(PostUrl, sidx, sord, columnDefs, callback, myParameters) {
            // console.log("获取列表API接口----------" + PostUrl)
            // console.log("排序字段-----------------" + sidx);
            // console.log("排序方式-----------------" + sord);
            // console.log("grid显示字段列表---------" + JSON.stringify(columnDefs));
            //参数
            let queryParmeters = {};
            if (myParameters) {
                queryParmeters = myParameters;
            }
            if (queryParmeters.queryCode == null) {
                queryParmeters.queryCode = "";
            }
            if (queryParmeters.queryName == null) {
                queryParmeters.queryName = "";
            }
            //配置窗口
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/Select_SingleChoiceModalForBarCode.html',//前端显示页面 SelectEP_EquipmentModal.html
                controller: 'CCS.CommonApp.CommonUI.Select_SingleChoiceModalForBarCode',//前端显示页面对应的 SelectEP_EquipmentModal.JS
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: PostUrl,//getMesApiAddress("equipment") + "Equipment/GetPage_Equipment",//API接口
                            //result_obj: obj,
                            queryParmeters: queryParmeters,
                            sidx: sidx,//"EquipmentCode",//排序字段
                            sord: sord,//"asc",//排序方式
                            columnDefs: columnDefs
                        };
                    }
                }
            });
            //选择回调检测
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    //console.log("obj目标----------" + JSON.stringify(data[0]))
                    //目标赋值
                    if (callback) {
                        callback(data[0]);
                    }

                }
            });
        }

        /*功能描述:单选弹窗方法
        *创    建:刘延彪
        *创建时间:2021-3-24
        *参    数:PostUrl API接口
        *         sidx  排序字段
        *         sord  排序方式
        *         columnDefs   grid显示字段列表
        *         callback  回调方法
        */
        function Select_SingleChoiceModal_RawReceipt(PostUrl, sidx, sord, columnDefs, callback, myParameters) {
            //console.log("获取列表API接口----------" + PostUrl)
            //console.log("排序字段-----------------" + sidx);
            //console.log("排序方式-----------------" + sord);
            //console.log("grid显示字段列表---------" + JSON.stringify(columnDefs));
            //参数
            let queryParmeters = {};
            if (myParameters) {
                queryParmeters = myParameters;
            }
            queryParmeters.queryName = "";
            queryParmeters.queryCode = "";
            //配置窗口
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/Select_SingleChoiceModal_RawReceipt.html',//前端显示页面 SelectEP_EquipmentModal.html
                controller: 'CCS.CommonApp.CommonUI.Select_SingleChoiceModal_RawReceipt',//前端显示页面对应的 SelectEP_EquipmentModal.JS
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: PostUrl,//getMesApiAddress("equipment") + "Equipment/GetPage_Equipment",//API接口
                            //result_obj: obj,
                            queryParmeters: queryParmeters,
                            sidx: sidx,//"EquipmentCode",//排序字段
                            sord: sord,//"asc",//排序方式
                            columnDefs: columnDefs
                        };
                    }
                }
            });
            //选择回调检测
            modalInstance.result.then(function (data) {
                if ((!data || data.length <= 0)) {
                    showWarning('您未选择任何条目');
                } else {
                    console.log("obj目标----------" + JSON.stringify(data[0]))
                    //目标赋值
                    if (callback) {
                        callback(data[0]);
                    }

                }
            });
        }
        //弹出选择模块 END
        //UTC时间转换本地时间
        function ConvertToLocalTime(UTCDateString) {
            if (!UTCDateString) {
                return '1900-01-01 00:00:00';
            }
            function formatFunc(str) {    //格式化显示
                return str > 9 ? str : '0' + str
            }
            var date2 = new Date(UTCDateString);     //这步是关键
            var year = date2.getFullYear();
            var mon = formatFunc(date2.getMonth() + 1);
            var day = formatFunc(date2.getDate());
            var hour = formatFunc(date2.getHours());
            var min = formatFunc(date2.getMinutes());
            var sec = formatFunc(date2.getSeconds());
            var dateStr = year + '-' + mon + '-' + day + ' ' + hour + ':' + min + ':' + sec;
            return dateStr;
        }
        //UTC时间转换本地时间
        function ConvertToLocalDate(UTCDateString) {
            if (!UTCDateString) {
                return '1900-01-01';
            }
            function formatFunc(str) {    //格式化显示
                return str > 9 ? str : '0' + str
            }
            var date2 = new Date(UTCDateString);     //这步是关键
            var year = date2.getFullYear();
            var mon = formatFunc(date2.getMonth() + 1);
            var day = formatFunc(date2.getDate());

            var dateStr = year + '-' + mon + '-' + day;
            return dateStr;
        }

        //附件上传
        function FileManageModal(params, callback) {
            let baseUrl = fileUrl
            params = { pId: '', isSave: 1, module: '', tableName: '', ...params }
            console.log(params)
            if (!params.pId) {
                showWarning('pId为空,请确认！')
            }
            var modalInstance = openModel({
                templateUrl: 'CCS.CommonApp/modules/CommonUI/FileManageModal.html',
                controller: 'CCS.CommonApp.CommonUI.FileManageModal',
                controllerAs: 'vm',
                size: 'lg',
                resolve: {
                    data: function () {
                        return {
                            url: getMesApiAddress() + "Base_Images/Base_ImagesPageDataTableList",
                            queryParmeters: { ParentId: params.pId, module: params.module, tableName: params.tableName },
                            formData: params,
                            sidx: "CreateTime",
                            sord: "asc",
                            multiple: false,
                            columnDefs: [
                                {
                                    name: 'rowNum', displayName: '序号', minWidth: 70, width: 70, enableSorting: false, cellTemplate:
                                        '<div class="ui-grid-cell-contents">{{grid.renderContainers.body.visibleRowCache.indexOf(row) + 1}}</div>'
                                },
                                {
                                    field: 'FileName',
                                    displayName: '文件名称',
                                    width: 200
                                },
                                {
                                    field: 'FileSize',
                                    displayName: '文件大小/m',
                                    width: 120
                                },
                                {
                                    field: 'ImgType',
                                    displayName: '文件类型',
                                    width: 120
                                },
                                {
                                    field: 'CreateTime',
                                    displayName: '上传日期',
                                    width: 120,
                                    type: 'date',
                                    cellFilter: 'alpDatetimeFilter2'//'YYYY-MM-DD HH:mm:ss'//'alpDatetimeFilter'
                                },
                                {
                                    name: 'button',
                                    displayName: '操作',
                                    width: 80,
                                    // pinnedRight: true,
                                    cellTemplate: `<div  style="text-align:center;margin-top:5px;">
                                    <a ng-href="${baseUrl}{{row.entity.FilePath}}" ng-if="!row.entity.IsNew" target="_blank" style="color: #0000cc;">下载</a>
                                    </div>`
                                }
                            ],
                        };
                    }
                }
            });
            // modalInstance.result.then(function (data) {
            //     if ((!data || data.length <= 0)) {
            //         showWarning('您未选择任何条目');
            //     } else {
            //         callback && callback(data[0])
            //     }
            // });
        }

        function InitUploader(formData, callback) {
            formData = { ...formData, isSave: 0 }
            var uploader = new FileUploader({
                queueLimit: 8,
                autoUpload: true,
                formData: [formData],
                url: getfileUrl() + '/UploadFile/Upload2'
            });

            uploader.filters.push({
                name: 'imageFilter',
                fn: function (item, options) {
                    var type = '|' + item.name.substr(item.name.lastIndexOf('.')) + '|';
                    //return '|.jpg|.jepg|.png|.gif|.bmp|.txt|.doc|.docx|.xls|.xlsx|.ppt|.pptx|.pdf|.zip|.rar|.7z|'.indexOf(type) !== -1;
                    return true;//改成不限制附件格式
                }
            });

            uploader.filters.push({
                name: 'syncFilter',
                fn: function (item, options) {
                    if (this.queue.length > 8) {
                        showWarning('文件上传超出上传8个文件限制!');
                    }
                    return this.queue.length < 9;
                }
            });
            uploader.onAfterAddingFile = function (fileItem) {
                var reader = new FileReader();
                reader.addEventListener("load", function (e) {
                    // vm.gridOptions.data.push({
                    //     FileName:fileItem.file.name,
                    //     FileSize:(fileItem.file.size/1024/1024).toFixed(2),
                    //     ImgType:fileItem.file.type,
                    //     IsNew:true
                    // })
                    //文件加载完之后，更新angular绑定
                    // $scope.$apply(function () {
                    //     console.info('lalal', e.target.result);
                    //     // self.images.FilePath = e.target.result;
                    //     $scope.FilePath = e.target.result;
                    // });
                }, false);
                reader.readAsDataURL(fileItem._file);
            };
            //上传成功返回结果 
            uploader.onSuccessItem = function (fileItem, response, status, headers) {
                console.info('onSuccessItem', fileItem, response, status, headers);
                if (response && response.success) {
                    if (response.resultData != null) {
                        callback && callback(response.resultData);
                    }
                } else {
                    showWarning(response.returnMsg);
                }
            };

            return uploader;
        }

        function getfileUrl() {
            var address = fileUrl;

            return address;
        }
    }
}());
