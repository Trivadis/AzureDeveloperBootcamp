module Trivadis.AzureBootcamp {
    "use strict";

    export class ShellController implements Core.IChatConnector {

        Users: Trivadis.AzureBootcamp.Core.User[];
        UserContext: Trivadis.AzureBootcamp.Core.UserContext;

        TextMessage: string;
        Messages: Array<any>;
        IsLoggedin: boolean;
        IsMessageSending: boolean;
        SelectedFile: any;


        static $inject: string[] = ["$scope", "ChatService", "$mdDialog"];
        constructor(private $scope: angular.IScope, private chatservice: Trivadis.AzureBootcamp.IChatService, private $mdDialog: angular.material.IDialogService) {
            this.Messages = new Array<any>();
            this.IsLoggedin = false;

            var username = angular.element("#username").attr("value");
            if (username) {
                this.doLogin({
                    Name: username
                });
            }
        }

        reloadUsers() {
            this.loadChatAttendees();
        }

        displayLogin($event) {

            this.$mdDialog.show({
                controller: LoginDialogController,
                controllerAs: "ctrl",
                bindToController: true,
                templateUrl: '../App/Templates/logindialog.html',
                targetEvent: $event,
                clickOutsideToClose: true
            }).then(f => this.doLogin(f));
        }

        onConnect() {
            this.loadChatAttendees();
        }

        onConnectionLost() {
            this.TextMessage = null;
            this.SelectedFile = null;
            this.Messages = new Array<any>();
            this.Users = null;
            this.UserContext = null;
            this.IsLoggedin = false;
            this.$scope.$apply();

            alert('signalr connection lost :-(');
        }

        clearChat() {
            this.Messages = new Array<any>();
            this.TextMessage = null;
            this.SelectedFile = null;
            this.IsMessageSending = false;
        }

        sendMessage() {

            this.IsMessageSending = true;
            this.chatservice.sendMessage(this.TextMessage, this.SelectedFile).then(f => {
                console.debug('message sent!');
            }, error => {
                console.error('error while sending!', error);
            }).finally(() => {
                console.debug('message sent completed!');
                this.TextMessage = null;
                this.SelectedFile = null;
                this.IsMessageSending = false;
            });
        }

        onMessageReceived(message: Core.ChatMessage) {
            this.Messages.unshift(message);
            this.$scope.$apply();
        }

        private doLogin(user: any) {
            console.debug("do login...", user);
            this.chatservice.login(this, user).then(f => {
                this.UserContext = f.data;
                this.IsLoggedin = true;
                this.IsMessageSending = false;
            }, error => {
                console.error(error);
            });
        }

        private loadChatAttendees() {
            this.chatservice.loadAllUsers().success(users => {
                this.Users = users;
            });
        }
    }

    angular.module("AzureBootcamp").controller("ShellController", ShellController);
}