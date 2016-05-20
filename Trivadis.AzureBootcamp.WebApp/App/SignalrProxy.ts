/// <reference path="_app.module.ts" />

module Trivadis.AzureBootcamp {
    "use strict";

    export interface ISignalrProxy {
        connect: (userid: string, connector: Core.IChatConnector) => void;
    }

    SignalrProxy.$inject = ["HttpConfig"];

    function SignalrProxy(config : Trivadis.AzureBootcamp.HttpConfig ): ISignalrProxy {

        var chatProxy: SignalR.Hub.Proxy;
        var connector: Core.IChatConnector; 

        var service: ISignalrProxy = {
            connect: connect
        };

        return service;

        function connect(userid: string, connector: Core.IChatConnector)  {

            this.connector = connector;
            var connection = $.hubConnection(config.ApiHost);
            connection.logging = true;
            connection.qs = { userid: userid };

            chatProxy = connection.createHubProxy('ChatHub');
            chatProxy.on('SendMessageToClient', function (message) {
                connector.onMessageReceived(message);
            });

            chatProxy.on('UsersChanged', function () {
                connector.reloadUsers();
            });

            connection.error(f => {
                console.error(f);
            });

            connection.disconnected(() => {
                connector.onConnectionLost();
            });


            connection.start().done(function (data) {
                connector.onConnect();
            }).fail(function (error) {
                console.error(error);
            });
        }
    }


    angular.module("AzureBootcamp").factory("SignalrProxy", SignalrProxy);
}