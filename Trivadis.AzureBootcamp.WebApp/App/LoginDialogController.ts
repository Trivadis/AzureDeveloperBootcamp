/// <reference path="_app.module.ts" />


module Trivadis.AzureBootcamp {
    "use strict";

    export class LoginDialogController
    {
        User: Trivadis.AzureBootcamp.Core.User;

        static $inject: string[] = ["$scope", "$mdDialog"];
        constructor(private $scope: any, private $mdDialog: angular.material.IDialogService) {

            this.User = new Trivadis.AzureBootcamp.Core.User();
        }

        cancel() {
           this.$mdDialog.cancel();
        }

        dologin(isValid: boolean) {
            if (isValid) {
                this.$mdDialog.hide(this.User);
            }
        };
    }

    angular.module("AzureBootcamp").controller("LoginDialogController", LoginDialogController);
}