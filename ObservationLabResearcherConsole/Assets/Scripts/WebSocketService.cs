using UnityEngine;
using NativeWebSocket;
//using System.Net.WebSockets;
using TMPro;

public class WebSocketService : MonoBehaviour
{
    private WebSocket websocket;
    private string webSocketDns = "wss://s09mwt8241.execute-api.us-east-2.amazonaws.com/production";
    [SerializeField] private TextMeshProUGUI connectionStatusText;
    void Start()
    {
        Debug.Log("Websocket start");
 
        websocket = new WebSocket(webSocketDns);
        SetupWebsocketCallbacks();

        Connect();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }




    // Connects to the websocket
    async public void Connect()
    {
        // waiting for messages
        await websocket.Connect();
    }

    // Establishes the connection's lifecycle callbacks.
    private void SetupWebsocketCallbacks()
    {
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
            connectionStatusText.text = "Connection Open";
            Data msg = new Data("Hello from Unity");
            SendWebSocketMessage(JsonUtility.ToJson(msg));
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
            connectionStatusText.text = "Error" + e;

        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
            connectionStatusText.text = "Connection closed";

        };

        //when data is received
        websocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            string message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log(message.ToString());

            ProcessReceivedMessage(message);
        };
    }

    private void ProcessReceivedMessage(string message)
    {
        /*GameMessage gameMessage = JsonUtility.FromJson<GameMessage>(message);

        if (gameMessage.opcode == PlayingOp)
        {
            _statusController.SetText(StatusController.Playing);
        }
        else if (gameMessage.opcode == ThrowOp)
        {
            Debug.Log(gameMessage.message);
        }
        else if (gameMessage.opcode == YouWonOp)
        {
            _statusController.SetText(StatusController.YouWon);
            QuitGame();
        }
        else if (gameMessage.opcode == YouLostOp)
        {
            _statusController.SetText(StatusController.YouLost);
            QuitGame();
        }*/
    }

    async void SendWebSocketMessage(string msg)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText(msg);
            Debug.Log("Message sent");
        }
    }
}
