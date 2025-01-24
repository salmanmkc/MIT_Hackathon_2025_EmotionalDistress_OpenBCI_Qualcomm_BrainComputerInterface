using UnityEngine;

namespace OpenBCI.UI
{
    public abstract class TimeSeriesGraphLine : MonoBehaviour
    {
        [SerializeField] protected float MaxHeight = 100f;
        [SerializeField] protected LineRenderer GraphLine;

        protected Vector3[] Positions;
        private RectTransform transform2D;

        public abstract void UpdateData(float[] data);
        private void Awake() => OnValidate();

        private void OnValidate()
        {
            transform2D = GetComponent<RectTransform>();
            if (GraphLine == null) GraphLine = GetComponentInChildren<LineRenderer>(true);
        }

        protected void UpdateHorizontalSpacing(int size)
        {
            if (transform2D == null)
            {
                transform2D = GetComponent<RectTransform>();
            }
            
            var spacing = transform2D.rect.width / size;
            GraphLine.positionCount = size;

            if (Positions == null || Positions.Length != size)
            {
                Positions = new Vector3[GraphLine.positionCount];
            }
            
            for (var i = 0; i < size; i++) Positions[i].x = spacing * i;
        }
    }
}