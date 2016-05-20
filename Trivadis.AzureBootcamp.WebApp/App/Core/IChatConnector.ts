module Trivadis.AzureBootcamp.Core {
    "use strict";

    export interface IChatConnector {
        onConnect: () => void;
        onConnectionLost: () => void;
        reloadUsers: () => void;
        onMessageReceived: (message: any) => void;
    }
}