using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PowerDivertSlider : SliderComponent
{
    [SerializeField] private PowerBar powerBar;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void GameReset(Task task)
    {
        SetToValue(0.5f);
        powerBar.SetValue(0.5f);
        interactable = false;
        sr.color = new Color(0.8f, 0.8f, 0.8f);
    }

    public override void OnStopInteracting()
    {
        if (CurrentValue == 1) ParentGame.Completed();
    }

    public override void OnValueChange(float newValue)
    {
        powerBar.SetValue(newValue);
        ((DivertPower)ParentGame).SetOthers(powerBar.ComponentName, CurrentValue);
    }

    public void Activate()
    {
        sr.color = Color.white;
        interactable = true;
    }
}
