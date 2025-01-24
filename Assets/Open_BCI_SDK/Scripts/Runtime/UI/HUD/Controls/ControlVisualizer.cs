using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public class ControlVisualizer : MonoBehaviour
    {
        [SerializeField] protected ControlArrow UpArrow;
        [SerializeField] protected ControlArrow DownArrow;
        [SerializeField] protected ControlArrow LeftArrow;
        [SerializeField] protected ControlArrow RightArrow;
        [SerializeField] protected ControlArrow ForwardArrow;
        [SerializeField] protected ControlArrow BackwardArrow;
        [SerializeField] protected ControlArrow ClockwiseArrow;
        [SerializeField] protected ControlArrow CounterclockwiseArrow;

        [Space]
        [SerializeField] protected Controller Controller;

        protected virtual void Update()
        {
            UpArrow.SetActivation(Controller.Up);
            DownArrow.SetActivation(Controller.Down);
            LeftArrow.SetActivation(Controller.Left);
            RightArrow.SetActivation(Controller.Right);
            ForwardArrow.SetActivation(Controller.Forward);
            BackwardArrow.SetActivation(Controller.Backward);
            ClockwiseArrow.SetActivation(Controller.YawRight);
            CounterclockwiseArrow.SetActivation(Controller.YawLeft);
        }
    }
}