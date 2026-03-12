using UnityEngine;

public abstract class SliderComponent : GameComponent
{
    [SerializeField] private Vector3 StartPosition;
    [SerializeField] private Vector3 EndPosition;
    private float SliderLength = 0;
    private float InitialValue;
    public float CurrentValue;
    private Vector3 InitialMousePosition;

    public override void OnStartInteracting()
    {
        if (SliderLength == 0) SliderLength = Vector3.Distance(StartPosition, EndPosition);
        InitialMousePosition = Input.mousePosition;
        InitialValue = CurrentValue;
    }

    public override void WhileInteracting()
    {
        Vector3 delta = InitialMousePosition - Input.mousePosition;
        float ValueChange = ProjectChangeOntoSlider(delta * UnitsPerPixel);
        if (ValueChange == 0) return;
        CurrentValue = Mathf.Clamp01(InitialValue + ValueChange);
        transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, CurrentValue);
        OnValueChange(CurrentValue);
    }

    public abstract void OnValueChange(float newValue);

    float ProjectChangeOntoSlider(Vector3 v)
    {
        return Vector3.Dot(StartPosition - EndPosition, v) / SliderLength;
    }

    public void SetToValue(float v)
    {
        CurrentValue = v;
        transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, CurrentValue);
    }
}
