/// <reference path="_app.module.ts" />

module Trivadis.AzureBootcamp  {
    "use strict";

    export class HttpConfig {

        WebRoot: string;
        ApiRoot: string;
        ApiHost: string;

        constructor() {

            this.WebRoot = angular.element("#webRoot").attr("href");
            this.ApiRoot = angular.element("#apiRoot").attr("href");
            this.ApiHost = angular.element("#apiHost").attr("href");
        }

        toApiUrl(segment: string): string {
            return this.ApiRoot + segment;
        }

        toWebUrl(segment: string): string {
            return this.WebRoot + segment;
        }

    }

    angular.module("AzureBootcamp").service("HttpConfig", HttpConfig);
}

