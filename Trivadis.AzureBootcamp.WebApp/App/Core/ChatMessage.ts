module Trivadis.AzureBootcamp.Core {
    "use strict";

    export class ChatMessage {

        public SenderUserId: string;
        public SenderUserName: string;
        public Message: string;
        public ImageUrl: string;
        public SenderUserAvatar: string
        public Timestamp: Date
    }
}