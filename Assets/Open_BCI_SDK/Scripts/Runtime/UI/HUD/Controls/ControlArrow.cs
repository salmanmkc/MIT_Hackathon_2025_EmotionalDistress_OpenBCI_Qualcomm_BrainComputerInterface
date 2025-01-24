using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OpenBCI.UI.HUD
{
    public class ControlArrow : MonoBehaviour
    {
        [SerializeField] private Color ActiveColor;
        [SerializeField] private Color InactiveColor;

        [Space]
        [SerializeField] private Image BaseArrow;
        [SerializeField] private Image GlowArrow;
        [SerializeField] private Image BaseNode;
        [SerializeField] private Image GlowNode;

        [Space]
        [SerializeField] private TMP_Text Value;
        [SerializeField] private SpriteRenderer Bumper;

        private Color activeGlow;

        private void OnValidate()
        {
            BaseArrow ??= transform.GetChild(0).GetComponent<Image>();
            GlowArrow ??= transform.GetChild(1).GetComponent<Image>();
            BaseNode ??= transform.GetChild(2).GetComponent<Image>();
            GlowNode ??= transform.GetChild(3).GetComponent<Image>();
        }

        private void Awake()
        {
            OnValidate();
            activeGlow = ActiveColor;
        }

        public void SetActivation(float activation)
        {
            //transform.localScale = Vector3.one + Vector3.one * (activation * 0.25f);
            activeGlow.a = activation;

            BaseArrow.color = activation > 0f ? ActiveColor : InactiveColor;
            BaseNode.color = activation > 0f ? ActiveColor : InactiveColor;
            GlowArrow.color = activation > 0f ? activeGlow : InactiveColor;
            GlowNode.color = activation > 0f ? activeGlow : InactiveColor;

            Value.color = activation > 0f ? ActiveColor : InactiveColor;
            Value.text = Mathf.Clamp((int)(activation * 100), 0, 100).ToString();

            Bumper.color = new Color(1f, 1f, 1f, activation);
        }
    }
}