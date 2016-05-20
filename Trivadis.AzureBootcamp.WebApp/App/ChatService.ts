module Trivadis.AzureBootcamp {
    "use strict";

    export interface IChatService {
        loadAllUsers: () => ng.IHttpPromise<Trivadis.AzureBootcamp.Core.User[]>;
        login: (connector: Core.IChatConnector, user: any) => ng.IHttpPromise<Trivadis.AzureBootcamp.Core.UserContext>;
        sendMessage: (textmessage?: string, file?: any) => ng.IPromise<any>;
    }

    class ChatService implements IChatService {

        UserContext: Trivadis.AzureBootcamp.Core.UserContext;

        static $inject: string[] = ["$http", "HttpConfig", "SignalrProxy", "Upload", "$q"];

        constructor(private $http: ng.IHttpService, private config: Trivadis.AzureBootcamp.HttpConfig, private signalr: Trivadis.AzureBootcamp.ISignalrProxy, private upload: ng.angularFileUpload.IUploadService, private $q : ng.IQService) {
        }

        loadAllUsers(): ng.IHttpPromise<Trivadis.AzureBootcamp.Core.User[]> {
            return this.$http.get(this.config.toApiUrl('chat/users'));
        }

        login(connector: Core.IChatConnector, user: any): ng.IHttpPromise<Trivadis.AzureBootcamp.Core.UserContext> {
            return this.joinChat(user).then(f => {
                this.UserContext = f.data;
                this.signalr.connect(this.UserContext.UserId, connector);
                return f;
            });
        }

        sendMessage(textmessage?: string, file?: any): ng.IPromise<any> {
            var deferred = this.$q.defer<any>();

            if (file != null) {
                var api = this.config.toApiUrl('file/upload');
                this.upload.upload<Trivadis.AzureBootcamp.Core.FileUploadResult>({
                    data: [
                        file
                    ],
                    url: api,
                    method: 'post'
                }).then(f => {

                    this.sendChatMessage(textmessage, f.data.Fileuri).then(f => {
                        deferred.resolve();
                    }, error => {
                        deferred.reject(error);
                    });

                    }, error => {
                        deferred.reject(error);
                    });
            }
            else if (textmessage != null) {
                return this.sendChatMessage(textmessage);
            }

            else if (textmessage == null && file == null) {
                deferred.reject( 'nix data' );
            }

            return deferred.promise;
        }

        private joinChat(user: any): ng.IHttpPromise<Trivadis.AzureBootcamp.Core.UserContext> {
            return this.$http.post(this.config.toApiUrl('chat/join'), user);
        }

        private sendChatMessage(textmessage?: string, imageurl?: string): ng.IHttpPromise<any> {

            var message = new Trivadis.AzureBootcamp.Core.ChatMessage();
            message.Message = textmessage;
            message.ImageUrl = imageurl;
            message.SenderUserId = this.UserContext.UserId;
            message.SenderUserName = this.UserContext.Name;

            return this.$http.post(this.config.toApiUrl('chat/send'), message);
        }
    }

    angular.module("AzureBootcamp").service("ChatService", ChatService);
}