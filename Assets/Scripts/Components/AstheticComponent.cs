using UnityEngine;

public abstract class AstheticComponent : GameComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Update()
    {

    }

    public override void OnStartInteracting() { }
    public override void WhileInteracting() { }
    public override void OnStopInteracting() { }
}
