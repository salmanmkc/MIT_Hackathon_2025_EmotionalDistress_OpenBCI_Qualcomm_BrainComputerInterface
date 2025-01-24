namespace OpenBCI.EyeTracking
{
    public interface IGazeTarget
    {
        public void StartGaze(EyeGazeManager source);
        public void StopGaze(EyeGazeManager source);
    }
}