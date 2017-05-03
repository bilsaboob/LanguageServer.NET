﻿using System;
using System.Threading.Tasks;
using JsonRpc.Standard.Contracts;
using JsonRpc.Standard.Server;
using LanguageServer.VsCode.Contracts;
using LanguageServer.VsCode.Contracts.Client;
using Newtonsoft.Json.Linq;

namespace DemoLanguageServer.Services
{
    public class InitializaionService : DemoLanguageServiceBase
    {

        [JsonRpcMethod(AllowExtensionData = true)]
        public InitializeResult Initialize(int processId, Uri rootUri, ClientCapabilities capabilities,
            JToken initializationOptions = null, string trace = null)
        {
            return new InitializeResult(new ServerCapabilities
            {
                HoverProvider = true,
                TextDocumentSync = new TextDocumentSyncOptions
                {
                    OpenClose = true,
                    WillSave = true,
                    Change = TextDocumentSyncKind.Full
                }
            });
        }

        [JsonRpcMethod]
        public async Task Initialized()
        {
            await Client.Window.ShowMessage(MessageType.Info, "Hello from language server.");
            var choice = await Client.Window.ShowMessage(MessageType.Warning, "Wanna drink?", "Yes", "No");
            await Client.Window.ShowMessage(MessageType.Info, $"You chose {(string) choice ?? "Nothing"}.");
        }

        [JsonRpcMethod]
        public void Shutdown()
        {

        }

        [JsonRpcMethod]
        public void Exit()
        {
            Session.StopServer();
        }
    }
}
