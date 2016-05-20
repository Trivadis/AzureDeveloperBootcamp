module Trivadis.AzureBootcamp {
    "use strict";

    angular.module("AzureBootcamp", [

        "ngMaterial",
        "ngFileUpload"

    ]).config(ConfigureMaterial).run(ConfigureAuthorization);


    function ConfigureAuthorization($http: angular.IHttpService) {
        var bearertoken = angular.element('#bearertoken').attr('value');
        if (bearertoken) {
            console.debug('using bearer token ', bearertoken.length);
            $http.defaults.headers.common['Authorization'] = 'Bearer ' + bearertoken;
        }
    }

    function ConfigureMaterial($mdThemingProvider: angular.material.IThemingProvider, $mdIconProvider: angular.material.IIconProvider) {
        $mdIconProvider.defaultIconSet("../Content/svg/avatars.svg", 128);
    }
}
