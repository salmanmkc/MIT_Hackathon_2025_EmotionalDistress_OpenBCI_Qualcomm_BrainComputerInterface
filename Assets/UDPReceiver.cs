using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    public int port = 9876; 
    private UdpClient udpClient;
    private Thread receiveThread;

    
    // Fields to store parsed data
    public float tensionLevel;

    void Start()
    {
        udpClient = new UdpClient(port);
        receiveThread = new Thread(ReceiveData) { IsBackground = true };
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        while (true)
        {
            try
            {
                byte[] receivedBytes = udpClient.Receive(ref remoteEndPoint);
                string receivedText = Encoding.UTF8.GetString(receivedBytes);
                Debug.Log($"Received: {receivedText} from {remoteEndPoint}");

                
                // Parse the received JSON data
                ParseJson(receivedText);
            }
            catch (SocketException e)
            {
                Debug.LogError($"Socket Exception: {e.Message}");
            }
            catch (Exception e)
            {
                Debug.LogError($"General Exception: {e.Message}");
            }
        }
    }

    private void ParseJson(string jsonString)
    {
        try
        {
            // Deserialize the JSON string into a UdpData object
            UdpData receivedData = JsonUtility.FromJson<UdpData>(jsonString);

            // Update the fields with the parsed data
            //tensed = receivedData.tensed == 1; // Convert integer to boolean

            tensionLevel = receivedData.tensed;
 

            // Debug logs for the parsed values
            //Debug.Log($"Tensed: {tensed}, Smiling: {smiling}");
        }
        catch (Exception e)
        {
            Debug.LogError($"JSON Parsing Error: {e.Message}");
        }
    }


    void Update()
    {
        // Use the parsed data (e.g., control UI or animations)
        if (tensionLevel > 0.7)
        {
            Debug.Log("Tensed state detected! Triggering response...");
            // Add your logic for tense state
        }

        else if (tensionLevel < 0.3)
        {
            Debug.Log("Smiling state detected! Triggering response...");
            // Add your logic for smiling state
        }
        else
        {
            Debug.Log("Neither tesnsed or smiling, neutral state. Triggering response...");
        }
    }

    void OnApplicationQuit()
    {
        // Clean up the UDP client and thread on application exit
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }

        if (udpClient != null)
        {
            udpClient.Close();
        }
    }

    // Class to map the JSON data structure

    private class UdpData
    {
        public float tensed;  // 1 for tensed, 0 for not tensed 
        //public int smiling; // 1 for smiling, 0 for not smiling
    }
}
