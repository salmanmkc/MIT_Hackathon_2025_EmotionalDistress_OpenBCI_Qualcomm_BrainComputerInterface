using OpenBCI.Network.Streams;
using UnityEngine;
using UnityEngine.Assertions;

public class GesturePadGizmo : MonoBehaviour
{
    [SerializeField] private GesturePadStream Stream;
    
    [SerializeField] private GameObject UpArrow;
    [SerializeField] private GameObject DownArrow;
    [SerializeField] private GameObject LeftArrow;
    [SerializeField] private GameObject RightArrow;
    [SerializeField] private GameObject UpLeftArrow;
    [SerializeField] private GameObject UpRightArrow;
    [SerializeField] private GameObject DownLeftArrow;
    [SerializeField] private GameObject DownRightArrow;
    [SerializeField] private GameObject CenterArrow;
    
    private Material upArrowMaterial;
    private Material downArrowMaterial;
    private Material leftArrowMaterial;
    private Material rightArrowMaterial;
    private Material upLeftArrowMaterial;
    private Material upRightArrowMaterial;
    private Material downLeftArrowMaterial;
    private Material downRightArrowMaterial;
    private Material centerArrowMaterial;

    private static readonly int Fill = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        Assert.IsNotNull(Stream);
        
        Assert.IsNotNull(UpArrow);
        Assert.IsNotNull(DownArrow);
        Assert.IsNotNull(LeftArrow);
        Assert.IsNotNull(RightArrow);
        Assert.IsNotNull(UpLeftArrow);
        Assert.IsNotNull(UpRightArrow);
        Assert.IsNotNull(DownLeftArrow);
        Assert.IsNotNull(DownRightArrow);
        Assert.IsNotNull(CenterArrow);

        upArrowMaterial = UpArrow.GetComponent<Renderer>()?.materials[0];
        downArrowMaterial = DownArrow.GetComponent<Renderer>()?.materials[0];
        leftArrowMaterial = LeftArrow.GetComponent<Renderer>()?.materials[0];
        rightArrowMaterial = RightArrow.GetComponent<Renderer>()?.materials[0];
        upLeftArrowMaterial = UpLeftArrow.GetComponent<Renderer>()?.materials[0];
        upRightArrowMaterial = UpRightArrow.GetComponent<Renderer>()?.materials[0];
        downLeftArrowMaterial = DownLeftArrow.GetComponent<Renderer>()?.materials[0];
        downRightArrowMaterial = DownRightArrow.GetComponent<Renderer>()?.materials[0];
        centerArrowMaterial = CenterArrow.GetComponent<Renderer>()?.materials[0];
        
        Assert.IsNotNull(upArrowMaterial);
        Assert.IsNotNull(downArrowMaterial);
        Assert.IsNotNull(leftArrowMaterial);
        Assert.IsNotNull(rightArrowMaterial);
        Assert.IsNotNull(upLeftArrowMaterial);
        Assert.IsNotNull(upRightArrowMaterial);
        Assert.IsNotNull(downLeftArrowMaterial);
        Assert.IsNotNull(downRightArrowMaterial);
        Assert.IsNotNull(centerArrowMaterial);
    }

    private void Update()
    {
        upArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.Up, 0f, 1f));
        downArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.Down, 0f, 1f));
        leftArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.Left, 0f, 1f));
        rightArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.Right, 0f, 1f));
        upLeftArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.UpLeft, 0f, 1f));
        upRightArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.UpRight, 0f, 1f));
        downLeftArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.DownLeft, 0f, 1f));
        downRightArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.DownRight, 0f, 1f));
        centerArrowMaterial.SetFloat(Fill, Mathf.Clamp(Stream.GesturePad.Center, 0f, 1f));
    }
}
