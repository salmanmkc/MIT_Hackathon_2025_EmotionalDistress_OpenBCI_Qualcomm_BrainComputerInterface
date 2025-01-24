using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

public class EMGJoystickGizmo : MonoBehaviour
{
    [SerializeField] private EMGJoystickStream Stream;
    
    [SerializeField] private GameObject UpArrow;
    [SerializeField] private GameObject DownArrow;
    [SerializeField] private GameObject LeftArrow;
    [SerializeField] private GameObject RightArrow;

    public float UpThreshold = 1f;
    public float DownThreshold = 1f;
    public float LeftThreshold = 1f;
    public float RightThreshold = 1f;

    private Material upArrowMaterial;
    private Material downArrowMaterial;
    private Material leftArrowMaterial;
    private Material rightArrowMaterial;

    private static readonly int Fill = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        Assert.IsNotNull(Stream);
        
        Assert.IsNotNull(UpArrow);
        Assert.IsNotNull(DownArrow);
        Assert.IsNotNull(LeftArrow);
        Assert.IsNotNull(RightArrow);

        upArrowMaterial = UpArrow.GetComponent<Renderer>()?.materials[0];
        downArrowMaterial = DownArrow.GetComponent<Renderer>()?.materials[0];
        leftArrowMaterial = LeftArrow.GetComponent<Renderer>()?.materials[0];
        rightArrowMaterial = RightArrow.GetComponent<Renderer>()?.materials[0];
        
        Assert.IsNotNull(upArrowMaterial);
        Assert.IsNotNull(downArrowMaterial);
        Assert.IsNotNull(leftArrowMaterial);
        Assert.IsNotNull(rightArrowMaterial);
    }

    private void Update()
    {
        var upValue = Remap(Stream.Joystick.y, 0f, UpThreshold, 0f, 1f);
        var downValue = Remap(-Stream.Joystick.y, 0f, DownThreshold, 0f, 1f);
        var rightValue = Remap(Stream.Joystick.x, 0f, RightThreshold, 0f, 1f);
        var leftValue = Remap(-Stream.Joystick.x, 0f, LeftThreshold, 0f, 1f);
        
        upArrowMaterial.SetFloat(Fill, Mathf.Clamp(upValue, 0f, 1f));
        downArrowMaterial.SetFloat(Fill, Mathf.Clamp(downValue, 0, 1f));
        rightArrowMaterial.SetFloat(Fill, Mathf.Clamp(rightValue, 0f, 1f));
        leftArrowMaterial.SetFloat(Fill, Mathf.Clamp(leftValue, 0f, 1f));
    }
    
    private static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
