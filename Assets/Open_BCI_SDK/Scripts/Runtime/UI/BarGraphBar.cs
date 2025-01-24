using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace OpenBCI.UI
{
    public class BarGraphBar : MonoBehaviour
    {
        [SerializeField] private RectTransform Bar;
        [SerializeField] private TMP_Text Value;

        private void OnValidate()
        {
            if (Bar == null) Bar = transform.Find("Bar")?.GetComponent<RectTransform>();
            if (Value == null) Value = transform.Find("Value")?.GetComponent<TMP_Text>();
        }

        private void Awake()
        {
            OnValidate();
            
            Assert.IsNotNull(Bar);
            Assert.IsNotNull(Value);
        }

        public void UpdateBar(float height, float value)
        {
            var size = Bar.sizeDelta;
            size.y = height;
            Bar.sizeDelta = size;

            Value.text = Mathf.RoundToInt(value).ToString();
        }
    }
}