using UnityEngine;

public class SimonBluePad : AestheticComponent
{
    private SpriteRenderer sr;
    public override void GameReset(Task task)
    {
        SetVisible(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetVisible(bool v)
    {
        if (v)
        {
            sr.color = Color.white;
        }else
        {
            sr.color = Color.clear;
        }
    }
}
