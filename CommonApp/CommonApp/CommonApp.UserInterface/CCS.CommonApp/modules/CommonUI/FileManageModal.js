(function () {
    'use strict';
    angular.module('CCS.CommonApp.CommonUI').controller('CCS.CommonApp.CommonUI.FileManageModal',
        ['common.base', '$filter', '$scope', '$uibModalInstance', 'data', 'commonService', 'FileUploader',
            function (common, $filter, $scope, $modalInstance, data, commonService, FileUploader) {
                var vm = this;
                var sidePanelManager, backendService, propertyGridHandler;
                vm.data = angular.copy(data);
                vm.lang = 'zh-cn';
                vm.currentItem = null;
                console.log(vm.data);
                // //分页变量
                var pagination = {
                    rows: 20,//每页显示条数
                    page: 1,//页码
                    sidx: vm.data.sidx,//排序字段
                    sord: vm.data.sord//排序方式
                };
                //查询参数
                vm.queryParmeters = vm.data.queryParmeters;
                //vm.searchObj = vm.data.searchObj;

                activate();

                function activate() {
                    init();
                    vm.loadData = loadData;
                    vm.deleteData = deleteData;
                }
                vm.highlightFilteredHeader = function (row, rowRenderIndex, col, colRenderIndex) {
                    if (col.filters[0].term) {
                        return 'header-filtered';
                    } else {
                        return '';
                    }
                };


                function urlEncode(param, key, encode) {
                    if (param == null) return '';
                    var paramStr = '';
                    var t = typeof (param);
                    if (t == 'string' || t == 'number' || t == 'boolean') {
                        paramStr += '&' + key + '=' + ((encode == null || encode) ? encodeURIComponent(param) : param);
                    } else {
                        for (var i in param) {
                            var k = key == null ? i : key + (param instanceof Array ? '[' + i + ']' : '.' + i);
                            paramStr += urlEncode(param[i], k, encode);
                        }
                    }
                    return paramStr;
                }
                vm.gridOptions = {
                    enableFullRowSelection: true,
                    enableRowSelection: true,
                    enableSelectAll: vm.data.multiple,//是否多选
                    enableMultiSelection: vm.data.multiple,//是否多选
                    selectionRowHeaderWidth: 35,
                    enableRowHeaderSelection: true,
                    //Added for custom paging      
                    paginationPageSizes: [pagination.rows, pagination.rows * 2, pagination.rows * 4, pagination.rows * 10, pagination.rows * 20],
                    paginationPageSize: pagination.rows,
                    useExternalPagination: true, // custom      
                    useExternalSorting: true, // custom      
                    useExternalFiltering: true, // custom 
                    totalItems: null,
                    multiSelect: vm.data.multiple,//是否多选
                    enableSorting: true,
                    enableFiltering: false,
                    enablePinning: true,
                    columnDefs: vm.data.columnDefs,
                    onRegisterApi: function (gridApi) {
                        $scope.gridApi = gridApi;
                        gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                            // var msg = 'row selected ' + row.isSelected;
                            // console.log(msg);
                            if (row && row.isSelected) {
                                vm.selectedItem = row.entity;
                            } else {
                                vm.selectedItem = null;
                            }
                        });
                        gridApi.selection.on.rowSelectionChangedBatch($scope, function (rows) {
                            var msg = 'rows changed ' + rows.length;
                            console.log(msg);
                        });
                        //Added for custom paging      
                        gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                            pagination.page = newPage;
                            pagination.rows = pageSize;
                            vm.loadData();
                        });
                    },
                    data: []
                };

                function init() {
                    sidePanelManager = common.services.sidePanel.service;
                    backendService = common.services.runtime.backendService;
                    initUpload();
                    vm.doUpload = () => {
                        var _content = document.getElementById("content");
                        //设置content的宽度（动态变化）
                        _content.style.width = `${0}px`;
                        _content.innerText = "";
                        $('#upload')[0].click()
                    }
                    vm.closeClick = () => {
                        $modalInstance.dismiss();
                    }
                    loadData();

                }
                function deleteData() {
                    var title = commonService.$t('customCommon.SelectModal.JS.Tips_1');
                    var text = commonService.$t('customCommon.SelectModal.JS.Tips_2');
                    backendService.confirm(text, function () {
                        var url = commonService.getMesApiAddress() + 'Base_Images/RemoveBase_Images';
                        //提交删除当前选择数据实体
                        var postData = {
                            Entity: vm.selectedItem
                        };
                        commonService.callWebApiPost(url, postData).then(function (res) {
                            if ((res) && (res.data.success)) {
                                var resultData = res.data.resultData;
                                //成功
                                commonService.showInfo(res.data.returnMsg);
                                //重新刷新列表
                                loadData();
                                self.selectedItem = null;
                                self.isButtonVisible = false;
                            } else {
                                //失败
                                commonService.showWarning(res.data.returnMsg);
                                console.log('删除数据出错: [' + res.status + '] - ' + res.statusText + '-' + res.data.returnMsg);
                            }
                        }, function (error) {
                            console.log("RemoveQua_Sample--error--------------------------" + JSON.stringify(error));
                            backendService.genericError('[' + error.status + '] - ' + error.statusText + '-' + error.data.returnMsg, "获取数据出错");
                        });
                    }, title);
                }
                function loadData() {
                    //let url = vm.data.url + urlEncode(vm.queryParmeters).replace('&','?')
                    let url = vm.data.url
                    commonService.callWebApiPost(url, { queryJson: vm.queryParmeters, pagination: pagination }).then(function (res) {
                        if (res && res.data.success) {
                            vm.gridOptions.totalItems = res.data.resultData.total;
                            vm.gridOptions.data = res.data.resultData.rows;
                        } else {
                            vm.gridOptions.data = [];
                            commonService.showError("获取数据出错:" + res.data.Error.Message);
                        }
                        commonService.hideLoading();
                    }, function (error) {
                        commonService.showError('[' + error.status + '] - ' + '获取数据时出现错误 ' + error.statusText);
                        commonService.hideLoading();
                    });

                }
                function initUpload() {
                    //上传  
                    //commonService.getMesApiAddress() = '/sitSrvApi/'
                    //上传文件接口 '/SitSrvApi/FileManage/UploadFile/UploadPS_SAP_TuZhi_UploadFile'
                    var uploader = $scope.uploader = new FileUploader({
                        queueLimit: 10,
                        autoUpload: true,
                        formData: [{ pId: vm.queryParmeters.ParentId, isSave: 1, module: vm.queryParmeters.module, tableName: vm.queryParmeters.tableName }],
                        url: commonService.getfileUrl() + '/UploadFile/Upload2'
                    });

                    // FILTERS  |jpg|png|jpeg|bmp|gif|doc|docx|xls|xlsx|ppt|pptx
                    // uploader.filters.push({
                    //     name: 'imageFilter',
                    //     fn: function (item /*{File|FileLikeObject}*/, options) {
                    //         var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';                     
                    //         return '.jpg|.jpeg|.png|.gif|.bmp|.txt|.doc|.docx|.xls|.xlsx|.ppt|.pptx|.pdf|.zip|.rar|.7z'.indexOf(type) !== -1;
                    //     }
                    // });

                    // a sync filter
                    uploader.filters.push({
                        name: 'syncFilter',
                        fn: function (item /*{File|FileLikeObject}*/, options) {
                            if (this.queue.length > 10) {
                                commonService.showWarning(commonService.$t('customCommon.SelectModal.JS.Tips_5'));
                            }
                            return this.queue.length < 11;
                        }
                    });
                    // // an async filter
                    // uploader.filters.push({
                    //     name: 'asyncFilter',
                    //     fn: function (item /*{File|FileLikeObject}*/, options, deferred) {
                    //         setTimeout(deferred.resolve, 1e3);
                    //     }
                    // });

                    uploader.onAfterAddingFile = function (fileItem) {
                        console.info('onAfterAddingFile', fileItem);
                        var reader = new FileReader();
                        reader.addEventListener("load", function (e) {
                            // vm.gridOptions.data.push({
                            //     FileName:fileItem.file.name,
                            //     FileSize:(fileItem.file.size/1024/1024).toFixed(2),
                            //     ImgType:fileItem.file.type,
                            //     IsNew:true
                            // })
                            //文件加载完之后，更新angular绑定
                            $scope.$apply(function () {
                                console.info('lalal', e.target.result);
                                // self.images.FilePath = e.target.result;
                                $scope.FilePath = e.target.result;
                            });
                        }, false);
                        reader.readAsDataURL(fileItem._file);
                    };
                    //上传成功返回结果 
                    uploader.onSuccessItem = function (fileItem, response, status, headers) {
                        console.info('onSuccessItem', fileItem, response, status, headers);
                        if (response && response.success) {
                            if (response.resultData != null) {
                                loadData();
                            }
                        } else {
                            commonService.showWarning(response.returnMsg);
                        }
                    };
                    // CALLBACKS
                    uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
                        console.info('onWhenAddingFileFailed', item, filter, options);
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
                        var _content = document.getElementById("content");
                        var progressWidth = progress / 100 * 300;
                        //设置content的宽度（动态变化）
                        _content.style.width = `${progressWidth}px`;
                        _content.innerText = progress + "%";
                        console.info('onProgressAll', progress);
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

                vm.fileManage = (item) => {

                }
                vm.save = function () {
                    $scope.uploader.uploadAll();
                    $modalInstance.dismiss();
                    // let selectionRows = $scope.gridApi.selection.getSelectedRows();
                    // $modalInstance.close(selectionRows);
                };

                vm.cancel = function () {
                    $modalInstance.dismiss();
                };
            }
        ]);
}());