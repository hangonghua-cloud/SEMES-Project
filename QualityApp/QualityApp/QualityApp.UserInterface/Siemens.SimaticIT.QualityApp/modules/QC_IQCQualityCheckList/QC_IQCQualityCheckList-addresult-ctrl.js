/**
*  0. 代码生成： UA单表一键生成前后端html、JS、API接口代码生成器 Ver 2.13 更新日期：2021-07-12  设计者：刘万军
*  1. 功能描述： IQC品质检验记录表
*  2. 创建人员： 丁零
*  3. 创建日期： 2021-08-27
*  4. 修改人员： 
*  5. 修改日期： 
**/
(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckList.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope', 'FileUploader'];
    function AddScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope, FileUploader) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {


            init();
            initGridOptions()
            initGridData();
            registerEvents();

            sidePanelManager.setTitle(self.currentItem.TitleName);
            sidePanelManager.open({
                mode: 'e',
                size: 'wide'
            });
        }

        //初始化
        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            self.currentItem = angular.copy($stateParams.selectedItem);
            self.validInputs = false;

            initDictionary();



            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            initUpload()

        }
        function initUpload() {

            var uploader = $scope.uploader = new FileUploader({
                url: commonService.fileUrl + 'UploadFile/Upload',
                headers: {
                    'UserCode': encodeURIComponent(auth.getUser()['unique_name']),
                    'UserName': encodeURIComponent(auth.getUser()['urn:fullname']),
                },
            });


            //限制上传的文件数量
            uploader.queueLimit = 10;
            //添加文件自动上传
            uploader.autoUpload = true;
            //文件限制提示语
            var showMsg = function (itemSize, maxSize) {
                console.info(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_1'), itemSize, maxSize)
                if (itemSize <= maxSize) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_2') + (maxSize).toFixed(0) + 'MB');
                    return false;
                }
                $scope.size = itemSize;
                return true;
            }
            // FILTERS  |jpg|png|jpeg|bmp|gif|doc|docx|xls|xlsx|ppt|pptx
            uploader.filters.push({
                name: 'imageFilter',
                fn: function (item /*{File|FileLikeObject}*/, options) {
                    if (!showMsg(item.size, 4096)) {
                        return false;
                    }
                    var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                    if ('|jpg|jpeg|png|'.indexOf(type) == -1) {
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_3'));
                    }
                    return '|jpg|jpeg|png|'.indexOf(type) !== -1;
                }
            });
            // 自定义过滤器
            uploader.filters.push({
                name: 'syncFilter',
                fn: function (item /*{File|FileLikeObject}*/, options) {
                    if (!showMsg(item.size, 4096)) {
                        return false;
                    }
                    console.log('syncFilter');
                    if (this.queue.length > 10) {
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_4'));
                    }
                    return this.queue.length < 11;
                }
            });

            // 自定义过滤器
            uploader.filters.push({
                name: 'asyncFilter',
                fn: function (item /*{File|FileLikeObject}*/, options, deferred) {
                    if (!showMsg(item.size, 4096)) {
                        return false;
                    }
                    console.log('asyncFilter');
                    setTimeout(deferred.resolve, 1e3);
                }
            });

            // CALLBACKS
            uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
                console.info('onWhenAddingFileFailed', item, filter, options);
            };
            uploader.onAfterAddingFile = function (fileItem) {
                console.info('onAfterAddingFile', fileItem);
            };
            uploader.onAfterAddingAll = function (addedFileItems) {
                console.info('onAfterAddingAll', addedFileItems);
            };
            uploader.onBeforeUploadItem = function (item) {
                console.info('onBeforeUploadItem', item);
            };
            uploader.onProgressItem = function (fileItem, progress) {
                console.info('onProgressItem', fileItem, progress);
            };
            uploader.onProgressAll = function (progress) {
                console.info('onProgressAll', progress);
            };
            uploader.onSuccessItem = function (fileItem, response, status, headers) {
                console.info('onSuccessItem', fileItem, response, status, headers);
                if (response && response.success) {
                    fileItem._file.filePath = response.resultData;
                }
            };
            uploader.onErrorItem = function (fileItem, response, status, headers) {
                console.info('onErrorItem', fileItem, response, status, headers);
            };
            uploader.onCancelItem = function (fileItem, response, status, headers) {
                console.info('onCancelItem', fileItem, response, status, headers);
            };
            uploader.onCompleteItem = function (fileItem, response, status, headers) {
                console.info('onCompleteItem', fileItem, response, status, headers);
            };
            uploader.onCompleteAll = function () {
                console.info('onCompleteAll');
            };

            console.info('uploader', uploader);
        }

        //获取上传文件
        function getUploadFile() {
            var ListFiles = [];
            if ($scope.uploader.queue.length > 0) {
                for (var i = 0; i < $scope.uploader.queue.length; i++) {
                    var uploadFile = $scope.uploader.queue[i];
                    if (uploadFile.isSuccess) {
                        ListFiles.push({
                            FileName: uploadFile._file.name,
                            FilePath: commonService.fileUrl + uploadFile._file.filePath,
                            ImgType: uploadFile._file.type,
                            TableName: "QC_IQCQualityCheck",
                            Module: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_5')
                        });
                    }
                }
            }
            return ListFiles;
        }



        function initDictionary() {
            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_6'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_6'), ItemValue: "" }]
            }
            self.typeQuality = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_6'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_6'), ItemValue: "" }]
            }
            commonService.getDataItemDuatil("QualityJudgement").then(function (res) {
                if (res && res.data.success) {
                    self.typeQuality.options = res.data.resultData;
                    self.typeQuality.value = { ItemValue: res.data.resultData[0].ItemValue, ItemName: res.data.resultData[0].ItemName };
                }
                if (!!self.currentItem.TestResult) {
                    self.typeQuality.value = self.typeQuality.options.find(t => t.ItemValue == self.currentItem.TestResult);
                }

            })
        }

        function initGridOptions() {
            self.gridOptionsItem = {
                enablePagination: false,
                enablePaginationControls: false,   //是否显示分页
                paginationPageSizes: [10, 20, 50, 100, 200, 500],
                paginationPageSize: 50,
                rowHeight: 35,
                multiSelect: false,
                enableFiltering: false,
                enableCellEditOnFocus: false,
                enableSelectAll: false,
                enableRowSelection: false,
                //enableFullRowSelection: true,
                enableMultiSelection: false,
                minimumColumnSize: 100,
                appScopeProvider: self,
                columnDefs: [
                    // {
                    //     name: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_7'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    // {
                    //     field: 'MaterialName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_8'),
                    //     enableCellEdit: false,
                    //     cellTemplate: '<div><div ng-click="grid.appScope.cellClicked(row.entity,col)" class="ui-grid-cell-contents" style="height:35px" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>',
                    //     width: 90
                    // },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_9'),
                        width: 110,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_11'),
                        width: 140
                    },
                    // {
                    //     field: 'DataTypeName',
                    //     displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_12'),
                    //     width: 140
                    // },
                    {
                        field: 'BadNum',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_13'),
                        width: 140,
                        cellTemplate:
                            //'<div ng-show="row.entity.TestDepartment==2"><sit-text sit-value="row.entity.BadNum" ></sit-text></div>'
                            '<div ng-if="row.entity.TestDepartment!=2">{{row.entity.BadNum}}</div><div ng-if="row.entity.TestDepartment==2"><sit-text sit-value="row.entity.BadNum"></sit-text></div>'
                    },
                    {
                        field: 'ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_14'),
                        cellTemplate:
                            '<div class="ui-grid-cell-contents" ng-if="row.entity.TestDepartment!=2">{{row.entity.ItemValue}}</div>' +
                            '<div ng-if="row.entity.DataType==1 && row.entity.TestDepartment==2"><sit-numeric sit-value="row.entity.ItemValue" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.DataType==2 && row.entity.TestDepartment==2"><sit-text sit-value="row.entity.ItemValue" ></sit-text></div>' +
                            '<div ng-if="row.entity.DataType==3 && row.entity.TestDepartment==2"><sit-date-time-picker sit-value="row.entity.ItemValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: false}"></sit-date-time-picker></div>' +
                            '<div ng-if="row.entity.DataType>3 && row.entity.TestDepartment==2" ><sit-select sit-value="row.entity.typeResultSelect.value"' +
                            'sit-validation="{required: false}"' +
                            'sit-options="row.entity.typeResultSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                        if (row && row.isSelected == true) {
                            self.selectedItem = row.entity;
                            //setButtonsVisibility(true);
                        } else {
                            self.selectedItem = null;
                            //setButtonsVisibility(false);
                        }
                    });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData() {
            if (!self.currentItem.TestMethodIdId) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_15'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_16'));
                return
            }
            var postdata = {
                queryJson: {
                    TestMethodId: self.currentItem.TestMethodIdId,
                    //TestDepartment: self.currentItem.TestDepartment
                }
            }

            var url = commonService.getMesApiAddress("quality") + 'QC_TestMethodItemMaintenance/QC_TestMethodItemMaintenancePageDataTableList';
            var req = commonService.callWebApiPost(url, postdata).then(function (res) {
                if (res && res.data.success) {

                    var data = res.data.resultData.rows;

                    var postdata2 = {
                        queryJson: {
                            IQCId: self.currentItem.Id
                        }
                    }
                    var url1 = commonService.getMesApiAddress("quality") + 'QC_IQCQualityCheckItem/QC_TestDetailPageDataTableList';

                    commonService.callWebApiPost(url1, postdata2).then(function (result) {

                        if (result && result.data.success) {

                            data.forEach(item => {
                                //实验室赋值
                                var ent = result.data.resultData.rows.find(t => t.TestItemCoading == item.TestItemCoading);
                                if (ent != null) {
                                    item.BadNum = ent.BadNum;
                                    if (item.DataType == 1) {
                                        item.ItemValue = parseFloat(ent.ItemValue);
                                    } else {
                                        item.ItemValue = ent.ItemValue;
                                    }
                                }
                                //实验室的不能录入
                                if (item.DataType > 3 && item.TestDepartment == 2) {
                                    self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_6'), ItemValue: "" }];
                                    var arr = item.DataTypeName.split("/");
                                    for (var i = 0; i < arr.length; i++) {
                                        self.typeSelect.options.push({
                                            ItemName: arr[i], ItemValue: i
                                        })
                                    }
                                    if (!!item.ItemValue) {
                                        self.typeSelect.value = self.typeSelect.options.find(t => t.ItemName == item.ItemValue);
                                    }
                                    item.typeResultSelect = angular.copy(self.typeSelect);
                                }
                            });
                        } else {
                            data.forEach(item => {
                                if (item.DataType > 3 && item.TestDepartment == 2) {
                                    self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_6'), ItemValue: "" }];
                                    var arr = item.DataTypeName.split("/");
                                    for (var i = 0; i < arr.length; i++) {
                                        self.typeSelect.options.push({
                                            ItemName: arr[i], ItemValue: i
                                        })
                                    }
                                    item.typeResultSelect = angular.copy(self.typeSelect);
                                }
                            });
                        }
                        self.gridOptionsItem.data = data;
                    })
                } else {
                    self.gridOptionsItem.data = []
                }
            });
        }


        //注册控件事件(输入框改变触发事件)
        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        //保存
        function save() {

            var data = self.gridOptionsItem.data;
            //过滤质量的
            data = data.filter(function (item, index, array) {
                return item.TestDepartment == "2";
            })

            if (data.length < 1) {

                commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_17'));
                return;
            }
            if (self.currentItem.TestDepartment == "2") {
                self.currentItem.TestResult = self.typeQuality.value.ItemValue;
            }

            data.forEach(item => {

                if (item.DataType == "3") { //时间
                    item.ItemValue = commonService.ConvertToLocalTime(item.ItemValue);
                }
                // 下拉框
                if (item.DataType > 3 && item.typeResultSelect.value.ItemValue >= 0) {
                    item.ItemValue = item.typeResultSelect.value.ItemName;
                }
                item.FactoryCode = self.currentItem.FactoryCode;
                item.FactoryName = self.currentItem.FactoryName;
            })
            self.busy = true;
            var ListFiles = getUploadFile();
            // if (ListFiles.length <= 0) {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_18'));
            //     self.busy = false;
            //     return;
            // }

            var postData = {
                TestDepartment: self.currentItem.TestDepartment,
                KeyValue: self.currentItem.Id,      //self.currentItem.Id 编辑要传主键, 如果是大写ID 改成Id 
                Entity: self.currentItem,
                data: data,
                imgList: ListFiles
            };

            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_19') });
            var url = commonService.getMesApiAddress("quality") + 'QC_IQCQualityCheckItem/SaveBatchQC_TestDetailList';
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
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_20'));
                //刷新局部
                let callData = {
                    dept: self.currentItem.TestDepartment,
                    testResult: self.currentItem.TestResult
                };
                $rootScope.$emit('to-detail', callData);
                $state.go('^', {}, { reload: false });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_16'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_IQCQualityCheckList.QC_IQCQualityCheckListaddresultctrl.Tips_16'));
        }

        //控件内容改变事件
        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_IQCQualityCheckList_QC_IQCQualityCheckList';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_IQCQualityCheckList';

        var state = {
            name: screenStateName + '.addresult',
            url: '/addresult',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_IQCQualityCheckList-addresult.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
