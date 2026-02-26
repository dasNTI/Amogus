using UnityEngine;

public abstract class AstheticComponent : GameComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnStartInteracting() { }
    public override void OnStopInteracting() { }
    public override void SetDefaultState(Vector3 data) {}
}
