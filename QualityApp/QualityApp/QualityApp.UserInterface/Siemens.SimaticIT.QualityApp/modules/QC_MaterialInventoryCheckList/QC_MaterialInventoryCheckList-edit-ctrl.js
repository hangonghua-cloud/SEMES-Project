(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList').config(EditScreenStateConfig);

    EditScreenController.$inject = ['Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckList.service', '$state', '$stateParams',
        'common.base', '$filter', '$scope', 'commonService', 'common.services.authentication', 'common.widgets.notificationTile.globalService',
        'common.widgets.busyIndicator.service', '$uibModal', '$interval', '$rootScope', 'FileUploader'];
    function EditScreenController(dataService, $state, $stateParams, common, $filter, $scope, commonService, auth, notificationService, busyIndicatorService, $modal, $interval, $rootScope, FileUploader) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            registerEvents();

            initGridOptions2();
            initGridData2();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_1'));
            sidePanelManager.open({
                mode: "e",
                size: "wide"
            });
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.currentItem = angular.copy($stateParams.selectedItem);

            //前端按钮事件
            self.save = save;
            self.cancel = cancel;
            initDictionary();

            self.testDepartmentChange = testDepartmentChange;

            initUpload();

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
                console.info(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_2'), itemSize, maxSize)
                if (itemSize <= maxSize) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_3') + (maxSize).toFixed(0) + 'MB');
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
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_4'));
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
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_5'));
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
                            TableName: "QC_MaterialInventoryCheckList",
                            Module: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_6')
                        });
                    }
                }
            }
            return ListFiles;
        }


        function initDictionary() {

            self.typeSelect = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" }]
            }

            // self.typeWhsCode = {
            //     value: { ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ResourceCode: "" },
            //     options: [{ ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ResourceCode: "" }]
            // };
            self.typeTestDepartment = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" }]
            };
            self.typeTestResult = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" }]
            };
            self.typeIsFrozen = {
                value: { ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" },
                options: [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" }]
            };
            // commonService.getResourceExtendInfo({ LevelCode: "Warehouse" }).then(function (res) {
            //     if (res && res.data.success) {
            //         self.typeWhsCode.options = res.data.resultData;
            //         self.typeWhsCode.options.splice(0, 0, {
            //             ResourceCode: "",
            //             ResourceName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7')
            //         });
            //         self.typeWhsCode.value = self.typeWhsCode.options.find(t => t.ResourceCode == self.currentItem.WhsCode);
            //     }

            // });
            commonService.getDataItemDuatil("AssayDepartment").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestDepartment.options = res.data.resultData;
                    self.typeTestDepartment.value = self.typeTestDepartment.options.find(t => t.ItemValue == self.currentItem.TestDepartment);
                }
            })
            commonService.getDataItemDuatil("QualityJudgement").then(function (res) {
                if (res && res.data.success) {
                    self.typeTestResult.options = res.data.resultData;
                    self.typeTestResult.value = self.typeTestResult.options.find(t => t.ItemValue == self.currentItem.TestResult);
                }
            })
            commonService.getDataItemDuatil("FrozenStatus").then(function (res) {
                if (res && res.data.success) {
                    self.typeIsFrozen.options = res.data.resultData;
                    self.typeIsFrozen.value = self.typeIsFrozen.options.find(t => t.ItemValue == self.currentItem.IsFrozen);
                }
            })

        }
        function initGridOptions2() {
            self.gridOptionsItem2 = {
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
                    //     name: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_8'), field: 'operation', enableFiltering: false, enableSorting: false, enableColumnMenu: false,
                    //     cellTemplate: '<div style="text-align:center;"><button ng-show="!row.entity.addrow" title="添加" ng-click="grid.appScope.addrow(row.entity)"><span class="glyphicon glyphicon-plus"></span></button>' + ' ' +
                    //         '<button ng-show="!row.entity.editrow" title="删除" ng-click="grid.appScope.delete(row.entity)"><span class="glyphicon glyphicon-trash"></span></button>' + ' ' +
                    //         '</div>', width: 80
                    // },
                    {
                        field: 'TestDepartment',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_9'),
                        width: 120,
                        cellTemplate:
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'1\'"><span ng-cell-text>实验室</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'2\'"><span ng-cell-text>质量部</span></div>' +
                            '<div class="ngCellText" ng-if="row.entity.TestDepartment==\'3\'"><span ng-cell-text>生产部</span></div>'
                    },
                    {
                        field: 'TestItemCoading',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_10'),
                        width: 140
                    },
                    {
                        field: 'TestItemName',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_11'),
                        width: 140
                    },
                    {
                        field: 'TestItemStandard',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_12'),
                        width: 140
                    },
                    {
                        field: 'ItemValue',
                        displayName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_13'),
                        cellTemplate: '<div ng-if="row.entity.DataType==1"><sit-numeric sit-validation="{required: true}" sit-value="row.entity.ItemValue" ></sit-numeric></div>' +
                            '<div ng-if="row.entity.DataType==2"><sit-text sit-validation="{required: true}" sit-value="row.entity.ItemValue" ></sit-text></div>' +
                            '<div ng-if="row.entity.DataType==3"><sit-date-time-picker sit-value="row.entity.ItemValue"' +
                            'sit-format="\'yyyy-MM-dd HH:mm:ss\'"' +
                            'sit-show-button-bar="true"' +
                            'sit-show-weeks="false"' +
                            'sit-validation="{required: true}"></sit-date-time-picker></div>' +
                            '<div ng-if="row.entity.DataType>3"><sit-select sit-value="row.entity.typeResultSelect.value"' +
                            'sit-validation="{required: true}"' +
                            'sit-options="row.entity.typeResultSelect.options"' +

                            'sit-to-display="\'ItemName\'"' +
                            'sit-to-keep="\'ItemValue\'">' +
                            '</sit-select></div>',
                        width: 300
                    },
                ],
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    // gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    //     if (row && row.isSelected == true) {
                    //         self.selectedItem = row.entity;
                    //         //setButtonsVisibility(true);
                    //     } else {
                    //         self.selectedItem = null;
                    //         //setButtonsVisibility(false);
                    //     }
                    // });
                    //防止字段只出现一半
                    $interval(function () {
                        $scope.gridApi.core.handleWindowResize();
                        $scope.gridApi.core.refresh();
                    }, 300, 2)
                },
                data: []
            };
        }

        function initGridData2() {

            var postData = {
                queryJson: {
                    TestDepartment: self.TestDepartment,
                    MaterialInventoryId: self.currentItem.Id

                }
            }
            var url = commonService.getMesApiAddress("quality") + 'QC_MaterialInventoryCheckItem/QC_MaterialInventoryCheckItemPageDataTableList';
            commonService.callWebApiPost(url, postData).then(function (res) {
                if (res && res.data.success) {
                    var data = res.data.resultData.rows;
                    data.forEach(item => {

                        if (item.DataType == "1") {
                            item.ItemValue = parseFloat(item.ItemValue);
                        }

                        if (item.DataType > 3) {
                            self.typeSelect.options = [{ ItemName: commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'), ItemValue: "" }];
                            var arr = item.DataTypeName.split("/");
                            for (var i = 0; i < arr.length; i++) {
                                self.typeSelect.options.push({
                                    ItemName: arr[i], ItemValue: i
                                })
                            }
                            self.typeSelect.value = self.typeSelect.options.find(t => t.ItemName == item.ItemValue);
                            item.typeResultSelect = angular.copy(self.typeSelect);
                        }
                    });
                    self.gridOptionsItem2.data = data;
                } else {
                    self.gridOptionsItem2.data = []
                }
            })

        }

        function testDepartmentChange(oldval, newval) {
            self.TestDepartment = newval.ItemValue;
            if (!!self.selectedItem1) initGridData2();

        }



        function save() {
            debugger
            var itemvalueisnull ="";

            //字典类型 取值参考
            //self.currentItem.InspectionType = self.InspectionType.value.ItemCode;
            var data = self.gridOptionsItem2.data;
            if (data.length < 1) {
                backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_14'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_15'));
                return false;
            }
            self.busy = true;
            var ListFiles = getUploadFile();
         

            if (!!self.typeTestDepartment.value.ItemValue)

                data.filter(function (item, index, array) {
                    return item.TestDepartment == self.typeTestDepartment.value.ItemValue;
                })
debugger
            data.forEach(item => {
                if (item.DataType == "3") {
                    item.ItemValue = commonService.ConvertToLocalTime(item.ItemValue);
                }
                if (item.DataType > 3 && item.typeResultSelect.value) {
                    item.ItemValue = item.typeResultSelect.value.ItemName;
                }

                if(item.ItemValue==undefined)
                {

                    itemvalueisnull=1;
                }else if(item.ItemValue==commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_7'))
                {
                 
                   itemvalueisnull=1;
                }

            });
            if(itemvalueisnull=='1')
            {
            backendService.genericError(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_16'), commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_15'));
            return;
            }

            self.currentItem.TestDepartment = self.typeTestDepartment.value.ItemValue;
            self.currentItem.TestResult = self.typeTestResult.value.ItemValue;

            //提交数据, KeyValue 传主键 是修改, 为空 是新增
            var postData = {
                KeyValue: self.currentItem.Id,
                IsFrozen: self.typeIsFrozen.value.ItemValue,
                Entity: self.currentItem,//检测方法Id
                data: data,
                imgList: ListFiles
            };
            var url = commonService.getMesApiAddress("quality") + 'QC_MaterialInventoryCheck/SaveBatchQC_MaterialInventoryCheck';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
        }

        //取消
        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        //保存成功事件
        function onSaveSuccess(data) {
            if (data.data.success) {
                busyIndicatorService.hide();
                //关闭侧边栏
                sidePanelManager.close();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_17'));
                //刷新局部
                $rootScope.$emit('to-editChildItem', 'parent');
                $state.go('^', {}, { reload: true });
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_15'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_15'));
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }
        function onPropertyGridValidityChange(event, params) {
            if (params.id == "add_form") {
                self.validInputs = params.validity;
            }
        }
    }

    EditScreenStateConfig.$inject = ['$stateProvider'];
    function EditScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_QualityApp_QC_MaterialInventoryCheckList_QC_MaterialInventoryCheckList';
        var moduleFolder = 'Siemens.SimaticIT.QualityApp/modules/QC_MaterialInventoryCheckList';

        var state = {
            name: screenStateName + '.edit',
            url: '/edit:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/QC_MaterialInventoryCheckList-edit.html',
                    controller: EditScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Siemens.SimaticIT.QualityApp.QC_MaterialInventoryCheckList.QC_MaterialInventoryCheckListeditctrl.Tips_18'
            },
            params: {
                selectedItem: null
            }
        };
        $stateProvider.state(state);
    }
}());
