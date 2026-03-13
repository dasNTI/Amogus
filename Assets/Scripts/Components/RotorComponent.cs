using UnityEngine;

public abstract class RotorComponent : GameComponent
{
    [SerializeField] private float MinAngle;
    [SerializeField] private float MaxAngle;
    private Vector3 initMousePosition;
    private float initMouseAngle = Mathf.Infinity;
    private float initTransformAngle;
    private float InteractDistanceThreshold = 0.25f;
    private float CurrentChange;
    private Vector3 TransformScreenPosition = Vector3.zero;
    public float CurrentValue;

    public override void GameReset(Task task)
    {
        
    }

    public override void OnStartInteracting()
    {
        initMousePosition = Input.mousePosition;
        initTransformAngle = transform.eulerAngles.z;
    }

    public override void OnStopInteracting()
    {
        initMouseAngle = Mathf.Infinity;
    }
    public override void WhileInteracting()
    {
        if (TransformScreenPosition == Vector3.zero) TransformScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dif = Input.mousePosition - TransformScreenPosition;
        if (dif.magnitude > InteractDistanceThreshold && initMouseAngle == Mathf.Infinity) initMouseAngle = GetCurrentAngle();
        if (initMouseAngle == Mathf.Infinity) return;

        float change = GetCurrentAngle() - initMouseAngle;
        if (change == CurrentChange) return;

        CurrentChange = change;
        float newAngle = Mathf.Clamp(initTransformAngle + change, MinAngle, MaxAngle);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newAngle);
        CurrentValue = (newAngle - MinAngle) / (MaxAngle - MinAngle);
        OnValueChange(CurrentValue);
    }

    float GetCurrentAngle()
    {
        Vector3 delta = Input.mousePosition - TransformScreenPosition;
        return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
    }

    public void SetValue(float v)
    {
        CurrentValue = v;
        float newAngle = Mathf.Lerp(MinAngle, MaxAngle, CurrentValue);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newAngle);
    }

    public abstract void OnValueChange(float newValue);
}
