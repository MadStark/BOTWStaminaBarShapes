using UnityEngine;
using Shapes;

[ExecuteInEditMode]
public class StaminaUI : MonoBehaviour
{
    [Header("Transform")]
    public float scale = 1;
    public Vector3 localPosition;
    
    [Header("Visuals")]
    public float mainRadius;
    public float mainThickness;
    public float secondaryRadius;
    public float secondaryThickness;
    public float tertiaryRadius;
    
    [Header("Stats")]
    public float maxFill;
    public float fillAmount;
    public float consumption;
    public bool recovering;

    [Header("Colours")]
    [ColorUsage(true, true)]
    public Color backgroundColor;
    [ColorUsage(true, true)]
    public Color fillColor;
    [ColorUsage(true, true)]
    public Color consumptionColor;
    [ColorUsage(true, true)]
    public Color recoveringColor;
    
    private static readonly Quaternion roll = Quaternion.AngleAxis(90, Vector3.forward);

    
    private void OnPostRender()
    {
        Draw.Matrix = transform.localToWorldMatrix;
        Draw.BlendMode = ShapesBlendMode.Transparent;

        float maxFill = Mathf.Clamp(this.maxFill, 1, 3);
        float fillAmount = Mathf.Clamp(this.fillAmount, 0, maxFill);
        
        DrawWheel(mainRadius * scale, mainThickness * scale, 1, Mathf.Clamp01(fillAmount));
        DrawWheel(secondaryRadius * scale, secondaryThickness * scale, Mathf.Clamp01(maxFill - 1), Mathf.Clamp01(fillAmount - 1));
        DrawWheel(tertiaryRadius * scale, secondaryThickness * scale, Mathf.Clamp01(maxFill - 2), Mathf.Clamp01(fillAmount - 2));
    }
    
    private void DrawWheel(float radius, float thickness, float maxFill, float fillAmount)
    {
        bool consuming = consumption > 0f && fillAmount > 0f && fillAmount < 1f;

        // Draw background
        float maxAngleEnd = maxFill * ShapesMath.TAU;
        Draw.Arc(localPosition, roll, radius, thickness * 0.95f, 0, maxAngleEnd, backgroundColor);
        
        if (consuming && !recovering)
        {
            float fillAngleEnd = Mathf.Clamp01(fillAmount - consumption) * ShapesMath.TAU;
            // Draw fill
            Draw.Arc(localPosition, roll, radius, thickness, 0, fillAngleEnd, fillColor);
            // Draw consumption
            Draw.Arc(localPosition, roll, radius, thickness, fillAngleEnd, fillAmount * ShapesMath.TAU, consumptionColor);
        }
        else
        {
            // Draw fill
            Draw.Arc(localPosition, roll, radius, thickness, 0, fillAmount * ShapesMath.TAU, recovering ? recoveringColor : fillColor);
        }
    }
}
