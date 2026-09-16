(function () {
    'use strict';
    angular.module('Siemens.SimaticIT.EquipmentApp.EquipmentAccount').config(AddScreenStateConfig);

    AddScreenController.$inject = ['Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccount.service',
        'commonService', 'common.services.authentication', '$state', '$rootScope', '$stateParams', 'common.base', '$filter', '$scope', 'common.widgets.notificationTile.globalService', 'FileUploader'];
    function AddScreenController(dataService, commonService, auth, $state, $rootScope, $stateParams, common, $filter,
        $scope, notificationService, FileUploader) {
        var self = this;
        var sidePanelManager, backendService, propertyGridHandler;

        activate();
        function activate() {
            init();

            registerEvents();

            sidePanelManager.setTitle(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_1'));
            sidePanelManager.open('e');
        }

        function init() {
            sidePanelManager = common.services.sidePanel.service;
            backendService = common.services.runtime.backendService;

            //Initialize Model Data
            self.selectedItem = angular.copy($stateParams.selectedItem);
            self.currentItem = {};
            self.currentItem.FactoryCode = self.selectedItem.TestMethodCoadin;
            self.currentItem.FactoryName = self.selectedItem.TestMethodCoadinName;
            self.validInputs = false;

            //Expose Model Methods
            self.save = save;
            self.cancel = cancel;

            //状态
            self.StatusConfig = {
                value: null,
                selectedOption: null,
                options: []
            };

            // self.currentItem.EquipCode = self.selectedItem.EquipmentId;
            GetStatusDictionary();
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
                console.info(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_2'), itemSize, maxSize)
                if (itemSize <= maxSize) {
                    commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_3') + (maxSize).toFixed(0) + 'MB');
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
                        commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_4'));
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
                            TableName: "EP_EquipmentManageItem",
                            Module: commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_5')
                        });
                    }
                }
            }
            return ListFiles;
        }


        //状态
        function GetStatusDictionary() {
            var url = commonService.getDataItemDuatil("EquipmentStatus").then(function (res) {
                self.StatusConfig.options = res.data.resultData;;
            });
        }

        function registerEvents() {
            $scope.$on('sit-property-grid.validity-changed', onPropertyGridValidityChange);
        }

        function save(currentItem) {
            debugger
            //self.currentItem.status = self.StateConfig.value.ItemValue;
            if (self.StatusConfig.selectedOption != null) {
                self.currentItem.EquipStatus = self.StatusConfig.selectedOption.ItemValue;
            }

            self.currentItem.ProducedDate = commonService.ConvertToLocalDate(self.ProducedDate);
            self.currentItem.UserDate = commonService.ConvertToLocalDate(self.UserDate);
            self.currentItem.EMId = self.selectedItem.ID;
            self.currentItem.EquipCode = self.selectedItem.EquipmentId;

            self.busy = true;
            var ListFiles = getUploadFile();
            // if (ListFiles.length <= 0) {
            //     commonService.showWarning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_6'));
            //     self.busy = false;
            //     return;
            // }

            var postData = {
                userName: self.UserCode,
                KeyValue: "",
                Entity: self.currentItem,
                imgListEntity: ListFiles
            };

            var url = commonService.getMesApiAddress("equipment") + 'EP_EquipmentManageItem/SaveEP_EquipmentManageItem';

            var req = commonService.callWebApiPost(url, postData).then(onSaveSuccess, onSaveError);

        }

        function cancel() {
            sidePanelManager.close();
            $state.go('^');
        }

        function onSaveSuccess(data) {
            console.log(data.data);
            if (data.data.success) {
                sidePanelManager.close();
                notificationService.warning(commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_7'));
                $rootScope.$emit('to-addChildItem', self.currentItem);
                // $state.go('^', {}, { reload: true });
            } else {
                backendService.genericError(data.data.returnMsg, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_8'));
            }
        }

        function onSaveError(error) {
            backendService.genericError('[' + error.status + '] - ' + error.statusText, commonService.$t('Siemens.SimaticIT.EquipmentApp.EquipmentAccount.EquipmentAccountaddDetailctrl.Tips_8'));
        }

        function onPropertyGridValidityChange(event, params) {
            self.validInputs = params.validity;
        }
    }

    AddScreenStateConfig.$inject = ['$stateProvider'];
    function AddScreenStateConfig($stateProvider) {
        var screenStateName = 'home.Siemens_SimaticIT_EquipmentApp_EquipmentAccount_EquipmentAccount';
        var moduleFolder = 'Siemens.SimaticIT.EquipmentApp/modules/EquipmentAccount';

        var state = {
            name: screenStateName + '.addDetail',
            url: '/addDetail',
            views: {
                'property-area-container@': {
                    templateUrl: moduleFolder + '/EquipmentAccount-addDetail.html',
                    controller: AddScreenController,
                    controllerAs: 'vm'
                }
            },
            data: {
                title: 'Add'
            },
            params: {
                selectedItem: null,
            }
        };
        $stateProvider.state(state);
    }
}());
