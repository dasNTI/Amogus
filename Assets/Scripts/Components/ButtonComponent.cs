using UnityEngine;

public abstract class ButtonComponent : GameComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetDefaultState(Vector3 data)
    {
        
    }

    public override void HandleInteracting() {
        OnClick();
    }
    public override void OnStopInteracting() {
        OnRelease();
    }

    public abstract void OnClick();
    public abstract void OnRelease();
}
