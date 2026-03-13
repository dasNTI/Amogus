using UnityEngine;

public abstract class ButtonComponent : GameComponent
{
    public override void OnStartInteracting() {
        OnClick();
    }
    public override void OnStopInteracting() {
        OnRelease();
    }

    public abstract void OnClick();
    public abstract void OnRelease();
}
