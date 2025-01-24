using System;
using Newtonsoft.Json;
using UnityEngine;

namespace OpenBCI.Network.Streams
{
    public abstract class TwoDimensionalStream : Stream
    {
        [Serializable]
        public class Packet
        {
            // Names must be exactly as written below for JSON deserialization
            public string type;
            public float[,] data;
        }
        
        private bool isPortValid = true;
        protected abstract void ProcessData(float[,] data);
        
        protected override void ProcessMessage(string message)
        {
            Packet packet = null;
            try
            {
                packet = JsonConvert.DeserializeObject<Packet>(message);
            }
            catch (JsonException)
            {
                if (isPortValid)
                {
                    Debug.LogError(
                        $"Received invalid input from port {Port}. Verify that the correct stream is being sent from the GUI on this port.");
                    isPortValid = false;
                }
            }
            if (packet == null) return;
            
            isPortValid = true;
            ProcessData(packet.data);
        }
    }
}