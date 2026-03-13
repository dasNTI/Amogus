using UnityEngine;

public abstract class AestheticComponent : GameComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Awake()
    {
        if (!ParentGame) ParentGame = transform.parent.GetComponent<Game>();
        ParentGame.RegisterComponent(ComponentName, this);
    }

    public override void Update() { }

    public override void OnStartInteracting() { }
    public override void WhileInteracting() { }
    public override void OnStopInteracting() { }
}
