using SimpleControlDesktop.Models;
using SimpleControlDesktop.ViewModels;
using SimpleControlDesktop.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleControlDesktop.Server
{
    public class WebSocketServer
    {
        public static async Task StartServerAsync(string uri)
        {
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(uri);
                listener.Start();
                Debug.WriteLine($"WebSocket server started at {uri}");

                while (true)
                {
                    HttpListenerContext context = await listener.GetContextAsync();

                    if (context.Request.IsWebSocketRequest)
                    {
                        HttpListenerWebSocketContext wsContext = await context.AcceptWebSocketAsync(null);
                        Debug.WriteLine("Client connected");

                        await HandleConnection(wsContext.WebSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
            }
        }


        private static async Task HandleConnection(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Debug.WriteLine($"Message received: {receivedMessage}");

                try
                {
                    // Deserialize the received message into a CommandMessage object
                    var commandMessage = JsonSerializer.Deserialize<CommandMessage>(receivedMessage);

                    if (commandMessage?.Command == null)
                    {
                        Debug.WriteLine("Invalid command received.");
                    }
                    else
                    {
                        switch (commandMessage.Command.ToLower())
                        {
                            case "shutdown":
                                SystemController.Shutdown();
                                break;
                            case "restart":
                                SystemController.Restart();
                                break;
                            case "sleep":
                                SystemController.Sleep();
                                break;
                            case "shutdownwithtimer":
                                SystemController.ShutdownWithTimer(commandMessage.Value.GetInt32());
                                break;
                            case "cancelshutdown":
                                SystemController.CancelShutdown();
                                break;
                            case "setvolume":
                                SystemController.SetVolume(commandMessage.Value.GetInt32());
                                break;
                            case "filetransfer":
                                await FileHandler.HandleFileTransferAsync(commandMessage.Value);
                                break;
                            default:
                                Debug.WriteLine("Unknown command received.");
                                break;
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Debug.WriteLine($"JSON Parsing Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}");
                }

                string responseMessage = $"Echo: {receivedMessage}";
                byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);

                await webSocket.SendAsync(
                    new ArraySegment<byte>(responseBytes),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
            }

            Debug.WriteLine("Connection closed!");
            webSocket.Dispose();
        }


    }
}
