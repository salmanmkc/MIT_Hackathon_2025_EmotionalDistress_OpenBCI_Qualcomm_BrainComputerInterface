using OpenBCI.EyeTracking;
using UnityEngine;
       
namespace OpenBCI.Markers
{
    public class EyeGazeMarker : Marker, IGazeTarget
    {
        [SerializeField] private double StartGazeValue = 1;
        [SerializeField] private double StopGazeValue = 2;
        
        public void StartGaze(EyeGazeManager source) => AddStreamMarker(StartGazeValue);
        public void StopGaze(EyeGazeManager source) => AddStreamMarker(StopGazeValue);
    }
}