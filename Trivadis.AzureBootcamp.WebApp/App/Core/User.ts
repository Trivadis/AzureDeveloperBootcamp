module Trivadis.AzureBootcamp.Core {
    "use strict";

    export class User {
        public Name: string;
        public Avatar: string;
        public Content: string;

        constructor(name?: string, avatar?: string, content?: string) {
            this.Name = name;
            this.Avatar = avatar;
            this.Content = content;
        }
    }
}