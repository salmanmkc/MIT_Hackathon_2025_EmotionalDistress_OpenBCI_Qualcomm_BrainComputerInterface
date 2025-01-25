using UnityEngine;
using OpenBCI.Network.Streams; // make sure you import the right thing from the OpenBCI SDK package

public class AlphaPillar : MonoBehaviour
{
    public GameObject pillar;  // make sure you have a GameObject in the scene to represent the pillar
    [SerializeField] private AverageBandPowerStream Stream; // attach the AverageBandPowerStream from the Unity prefab
    private float pillarHeight;

    public GameObject pillarAlpha;
    public float pillarHeightAlpha;
    public GameObject pillarBeta;
    public float pillarHeightBeta;
    public GameObject pillarDelta;
    public float pillarHeightDelta;
    public GameObject pillarGamma;
    public float pillarHeightGamma;
    public GameObject pillarTheta;
    public float pillarHeightTheta;

    
    
    
    

    // Start is called once before the first frame update
    void Start()
    {
        pillarHeight = 0f;
        pillarHeightAlpha = 0f;
        pillarHeightBeta = 0f;
        pillarHeightTheta = 0f;
        pillarHeightDelta = 0f;
        pillarHeightGamma = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // log the alpha band power value to the unity console
        Debug.Log("Alpha: " + Stream.AverageBandPower.Alpha);

        // scale the cylinder height based on the alpha band power
        pillarHeight = Stream.AverageBandPower.Alpha;
        pillar.transform.localScale = new Vector3(1, pillarHeight, 1);

                pillarHeightAlpha = Stream.AverageBandPower.Alpha;
        pillarAlpha.transform.localScale = new Vector3(1, pillarHeightAlpha, 1);
                pillarHeightBeta = Stream.AverageBandPower.Beta;
        pillarBeta.transform.localScale = new Vector3(1, pillarHeightBeta, 1);
                pillarHeightDelta = Stream.AverageBandPower.Delta;
        pillarDelta.transform.localScale = new Vector3(1, pillarHeightDelta, 1);
                pillarHeightGamma = Stream.AverageBandPower.Gamma;
        pillarGamma.transform.localScale = new Vector3(1, pillarHeightGamma, 1);
                pillarHeightTheta = Stream.AverageBandPower.Theta;
        pillarTheta.transform.localScale = new Vector3(1, pillarHeightTheta, 1);
    }
    void Bands()
    {
        pillarHeightAlpha = Stream.AverageBandPower.Alpha;
        pillarHeightBeta = Stream.AverageBandPower.Beta;
        pillarHeightDelta = Stream.AverageBandPower.Delta;
        pillarHeightGamma = Stream.AverageBandPower.Gamma;
        pillarHeightTheta = Stream.AverageBandPower.Theta;
       
    }
}