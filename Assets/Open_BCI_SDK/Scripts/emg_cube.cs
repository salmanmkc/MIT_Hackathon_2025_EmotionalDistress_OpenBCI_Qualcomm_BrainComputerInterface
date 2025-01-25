using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// must include this to access the streams
using OpenBCI.Network.Streams;

public class emg_cube : MonoBehaviour
{
    //must include this to access the streams
    //can grab any of the data streams you want
    // (e.g. ExGStream, EMGJoystickStream, AverageBandPowerStream, FFTStream, etc.)
    [SerializeField] private EMGStream Stream;
    public GameObject Cube;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("channel info" + Stream.Channels[0]);

        //extracted only the data from the first ExG channel, 
        // which was configured to EMG
        if (Stream.Channels[0] > 0.8)
        {
            //move the cube closer to the camera
            transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            //move the cube further from the camera
            transform.position = new Vector3(0, 0, 1);
        }
    }
}
