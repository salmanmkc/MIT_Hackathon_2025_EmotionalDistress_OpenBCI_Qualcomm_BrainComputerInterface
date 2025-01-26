using OpenBCI.Network.Streams;
using OpenBCI.UI;
using UnityEngine;

public class EMGAvererageGraph : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private BarGraphBar[] Bars;
    private AverageExg averageExg;

    private void OnValidate()
    {
        if (Bars == null || Bars.Length == 0) Bars = GetComponentsInChildren<BarGraphBar>();
    }


    //private void Update()
    //{
    //    for (var i = 0; i < averageExg.averages.Length; i++)
    //    {
    //        //Bars.SetValue(i, averageExg.averages[i] * 100);
    //    }
    //}
   
}
