using UnityEngine;

public class FillBar : AstheticComponent
{
    [SerializeField] private float MaxValue;
    [SerializeField] private float MinValue;
    [SerializeField] private float DefaultValue;
    private float CurrentValue;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = Instantiate(sr.material);
    }

    public override void GameReset(Task task)
    {
        SetValue(DefaultValue);
    }

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        CurrentValue = Mathf.Lerp(MinValue, MaxValue, value);
        sr.material.SetFloat("_Value", CurrentValue);
    }
}
