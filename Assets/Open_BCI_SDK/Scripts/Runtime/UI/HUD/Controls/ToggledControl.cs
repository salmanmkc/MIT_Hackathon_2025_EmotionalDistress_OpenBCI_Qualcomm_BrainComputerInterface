using UnityEngine;

namespace OpenBCI.UI.HUD
{
    public class ToggledControl : MonoBehaviour
    {
        [SerializeField] private GameObject Active;
        [SerializeField] private GameObject Inactive;

        public void SetActive(bool active)
        {
            Active.SetActive(active);
            Inactive.SetActive(!active);
        }
    }
}