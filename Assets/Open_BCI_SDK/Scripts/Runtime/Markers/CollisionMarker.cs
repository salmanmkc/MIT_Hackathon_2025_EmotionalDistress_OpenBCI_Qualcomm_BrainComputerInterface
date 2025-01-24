using UnityEngine;

namespace OpenBCI.Markers
{
    public class CollisionMarker : Marker
    {
        [SerializeField] private double CollisionEnterValue = 1;
        [SerializeField] private double CollisionExitValue = 2;
        [SerializeField] private Collider Collider;

        protected void OnValidate()
        {
            if (Collider == null) Collider = GetComponent<Collider>();
        }

        protected void Awake()
        {
            OnValidate();
        }

        private void OnTriggerEnter(Collider other)
        {
            AddStreamMarker(CollisionEnterValue);
        }

        private void OnTriggerExit(Collider other)
        {
            AddStreamMarker(CollisionExitValue);
        }
    }
}