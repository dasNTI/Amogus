using UnityEngine;

public class PowerBar : AestheticComponent
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = Instantiate(sr.material);
        sr.material.SetFloat("_Seed", Random.Range(1000, 10000));
    }

    public override void GameReset(Task task)
    {
        SetValue(0.6f);
    }

    public void SetValue(float v)
    {
        sr.material.SetFloat("_Value", v);
    }
}
