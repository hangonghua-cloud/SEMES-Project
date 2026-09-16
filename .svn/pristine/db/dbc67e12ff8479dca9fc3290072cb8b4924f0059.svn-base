(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EquipmentAccount').config(ViewScreenStateConfig);

    ViewScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount.service',
        'commonService', 'common.services.authentication', '$state', '$stateParams', 'common.base', '$filter', '$scope',
        'common.widgets.notificationTile.globalService', 'FileUploader', 'common.widgets.busyIndicator.service'];
    function ViewScreenController(dataService, commonService, auth, $state, $stateParams, common, $filter,
        $scope, notificationService, FileUploader, busyIndicatorService) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();
            initImages();
            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data

            // TODO: Put here the properties of the entity managed by the service
            self.currentItem = angular.copy($stateParams.selectedItem);
            self.images = [];

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;
            self.delete = deleteform;

            initUpload();

        }
        //文件上传配置
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
                console.info(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_2'), itemSize, maxSize)
                if (itemSize <= maxSize) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_3') + (maxSize).toFixed(0) + 'MB');
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
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_4'));
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
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_5'));
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
                            ParentId: self.currentItem.Id,//关联表Id
                            TableName: "EP_EquipmentManageItem",//关联表名称
                            Module: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_6')//所属模块
                        });
                    }
                }
            }
            return ListFiles;
        }

        //页面展示图片
        function initImages() {
            var post = {
                queryJson: {
                    ParentId: self.currentItem.Id
                }
            }

            var url = commonService.getMesApiAddress() + 'Base_Images/Base_ImagesPageDataTableList';
            commonService.callWebApiPost(url, post).then(function (res) {

                if ((res) && (res.data.success)) {
                    var resultData = res.data.resultData;
                    self.images = resultData.rows;
                } else {
                    self.images = [];
                }
            }, function (error) {
                backendService.genericError('获取数据出错', commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_7'));
            });
        }

        function save() {
            busyIndicatorService.show({ message: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_8') });
            self.busy = true;
            var ListFiles = getUploadFile();

            if (ListFiles.length <= 0) {
                commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_9'));
                self.busy = false;
                return;
            }

            var postData = {
                KeyValue: "",
                data: ListFiles
            };

            var url = commonService.getMesApiAddress() + 'Base_Images/SaveBatchBase_Images';
            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

            busyIndicatorService.hide();
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
                initImages();
                busyIndicatorService.hide();
                commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_10'));
            } else {
                busyIndicatorService.hide();
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_11'));
            }
        }

        //保存失败事件
        function onSaveError(error) {
            busyIndicatorService.hide();
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_11'));
        }

        function deleteform(img) {
            debugger
            backendService.confirm(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_12'), function (res) {
                var url = commonService.getMesApiAddress() + 'Base_Images/DeleteBase_Images';
                commonService.callWebApiPost(url, { Entity: img }).then(function (res) {
                    if (res && res.data.success) {
                        initImages();
                        commonService.showInfo(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_10'));;
                    } else {
                        backendService.genericError(res.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_11'));
                    }
                });

            }, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountselectctrl.Tips_13'));

        }



        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    ViewScreenStateConfig.$inject = ['$stateProvider'];
    function ViewScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount_EquipmentAccount';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EquipmentAccount';

        var state = {
            name: screenStateName + '.select',
            url: '/select/:id',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EquipmentAccount-select.html',
                    controller: ViewScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Select'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
