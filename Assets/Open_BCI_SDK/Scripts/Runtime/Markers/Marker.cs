using System;
using System.Net.Sockets;
using UnityEngine;

namespace OpenBCI.Markers
{
    public abstract class Marker : MonoBehaviour
    {
        [SerializeField] private int Port;
        [SerializeField] private string Hostname = "127.0.0.1";
        
        private UdpClient client;

        protected void Start()
        {
            client = new UdpClient();
        }

        protected void AddStreamMarker(double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            
            client?.Send(bytes, bytes.Length, Hostname, Port);
        }
    }
}