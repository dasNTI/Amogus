using UnityEngine;

public class RotorComponent : GameComponent
{
    [SerializeField] private float MinAngle;
    [SerializeField] private float MaxAngle;
    private Vector3 initMousePosition;
    private float initMouseAngle;
    private float initTransformAngle;
    private float InteractDistanceThreshold = 0.25f;
    private float CurrentChange;
    public float CurrentValue;

    public override void GameReset(Task task)
    {
        throw new System.NotImplementedException();
    }

    public override void OnStartInteracting()
    {
        initMousePosition = Input.mousePosition;
        initTransformAngle = transform.eulerAngles.z;
    }

    public override void OnStopInteracting() { }
    public override void WhileInteracting()
    {
        Vector3 dif = Input.mousePosition - initMousePosition;
        if (dif.magnitude > InteractDistanceThreshold && initMouseAngle != Mathf.Infinity) initMouseAngle = GetCurrentAngle();

        float change = GetCurrentAngle() - initMouseAngle;
        if (change != CurrentChange)
        {
            CurrentChange = change;
        }
    }

    float GetCurrentAngle()
    {
        Vector3 delta = Input.mousePosition - initMousePosition;
        return Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;


    }
}
