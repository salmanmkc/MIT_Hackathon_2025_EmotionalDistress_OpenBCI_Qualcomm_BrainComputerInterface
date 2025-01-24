using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace OpenBCI.Network
{
    public abstract class Stream : MonoBehaviour
    {
        [SerializeField] protected uint Port;
        
        private UdpClient client;
        private IPEndPoint endpoint;
        private AsyncCallback callback;
        private string message;
        private object obj;
        private bool isDataAvailable;

        protected abstract void ProcessMessage(string message);

        private void Start()
        {
            endpoint = new IPEndPoint(IPAddress.Any, (int)Port);
            client = new UdpClient((int)Port);
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            
            callback = ReceivePacket;
            client?.BeginReceive(callback, obj);
        }

        private void OnDestroy()
        {
            client?.Close();
        }

        public void Update()
        {
            if (message == null || !isDataAvailable) return;
            
            ProcessMessage(message);
            isDataAvailable = false;
        }

        private void ReceivePacket(IAsyncResult result)
        {
            if (client == null) return;

            var data = client.EndReceive(result, ref endpoint);
            message = Encoding.UTF8.GetString(data);
            client?.BeginReceive(callback, obj);

            isDataAvailable = true;
        }
    }
}
