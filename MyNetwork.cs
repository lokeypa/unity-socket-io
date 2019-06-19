using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
//Connection

public class MyNetwork : MonoBehaviour
{

  private Socket socket;
  private bool isConnected = false;
  void Awake (){
    socket = IO.Socket("http://192.168.0.8:8080");
  }


    public void Connect() {
      socket.Open();
      if(isConnected) {
        socket.Emit("join", SystemInfo.deviceUniqueIdentifier);
      }
    }
    // Start is called before the first frame update
    void Start()
    {
      socket.On(Socket.EVENT_CONNECT, () => {
        Debug.Log("connection made");
        isConnected = true;
      });

      socket.On("ready", (data) => {
        Debug.Log(data);
        socket.Emit("players:get");
      });

      socket.On("players:list", (data) => {
        Debug.Log("players:list response");
        Debug.Log(data);
      });
      socket.On("serverMessage", (data) => {
        Debug.LogWarning(data);
      });
      
      socket.On("serverError", (data) => {
        Debug.LogError(data);
      });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
