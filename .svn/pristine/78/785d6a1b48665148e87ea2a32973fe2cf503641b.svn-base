(function () {
    'use strict';

    var app = angular.module('CCS.CommonApp');

    /**
    * @usage
    * As an element:
    * ```
    * <sit-text-picker sit-value="value" sit-validation="validation" ng-readonly="ngReadonly" ng-blur="ngBlur" sit-change="sitChange"
    *       ng-disabled="ngDisabled" ng-focus="ngFocus">
    * </sit-text-picker>
    * ```
    * @restrict E
	*
    * @param {string} sit-value Value of the text widget.
    * @param {ValidationModel} sit-validation See {@link ValidationModel}.
    * @param {string} [ng-blur] _(Optional)_ An expression to evaluate on blur event.
    * @param {string} [sit-change] _(Optional)_ An expression to evaluate on change of value.
    * @param {string} [ng-disabled] _(Optional)_ If this expression is truthy, the element will be disabled.
    * @param {string} [ng-focus] _(Optional)_ An expression to evaluate on focus event.
    * @param {string} [ng-readonly] _(Optional)_ If this expression is truthy, the element will be set as read-only.
    *
    */
    function AlpTextPickerController() {}

    app.controller('AlpTextPickerController', AlpTextPickerController);

    app.directive('sitAlpTextPicker', function () {

        return {
            bindToController: {
                'readOnly': '=?sitReadOnly',
                'value': '=?sitValue',
                'validation': '=?sitValidation',
                'ngBlur': '&?',
                'sitChange': '=?',
                'ngDisabled': '=?',
                'ngFocus': '&?',
                'ngReadonly': '=?',
                'placeholder': '=?sitPlaceholder'
            },

            scope: {},

            restrict: 'E',

            controller: 'AlpTextPickerController',

            controllerAs: 'alpTextPickerCtrl',

            templateUrl: 'CCS.CommonApp/widgets/sitAlpTextPicker/sit-alp-text-picker.html'
        };
    });
})();