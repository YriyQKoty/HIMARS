using UnityEngine;

namespace Scriptables.Cameras
{
    [CreateAssetMenu(fileName = "Outline", menuName = "Outline Data", order = 0)]
    public class OutlineCameraData : ScriptableObject
    {
        [Range(1.0f, 6.0f)]
        [SerializeField]private float lineThickness = 1.25f;
        [Range(0, 10)]
        [SerializeField]private float lineIntensity = .5f;
        [Range(0, 1)]
        [SerializeField]private float fillAmount = 0.2f;

        [SerializeField]private Color lineColor0 = Color.red;
        [SerializeField]private Color lineColor1 = Color.green;
        [SerializeField]private Color lineColor2 = Color.blue;

        [SerializeField]private bool additiveRendering = false;

        [SerializeField]private bool backfaceCulling = true;

        [SerializeField]private Color fillColor = Color.blue;
        [SerializeField]private bool useFillColor = false;


        public Color LineColor0 => lineColor0;

        public Color LineColor1 => lineColor1;

        public Color LineColor2 => lineColor2;

        public float LineThickness => lineThickness;

        public float LineIntensity => lineIntensity;

        public float FillAmount => fillAmount;

        public bool AdditiveRendering => additiveRendering;

        public bool BackfaceCulling => backfaceCulling;

        public Color FillColor => fillColor;

        public bool UseFillColor => useFillColor;
    }
}