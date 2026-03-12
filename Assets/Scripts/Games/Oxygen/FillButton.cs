using UnityEngine;

public class FillButton : ButtonComponent
{
    [SerializeField] private SpriteRenderer RedBulb;
    [SerializeField] private SpriteRenderer GreenBulb;
    [SerializeField] private FillBar EmptyingCanister;
    [SerializeField] private FillBar FillingCanister;
    private float CurrentProgress;
    [SerializeField] private float DefaultProgress = 0;
    [SerializeField] private float FillingDuration = 10;

    Color DarkColor = new Color(0.5f, 0.5f, 0.5f);

    void Start()
    {
        
    }
    public override void GameReset(Task task)
    {
        CurrentProgress = DefaultProgress;
        if (EmptyingCanister) EmptyingCanister.SetValue(1);
        FillingCanister.SetValue(0);

        RedBulb.color = DarkColor;
        GreenBulb.color = DarkColor;
    }

    public override void OnClick()
    {
        RedBulb.color = Color.white;
    }

    public override void OnRelease()
    {
        if (CurrentProgress < 1) RedBulb.color = DarkColor;
    }

    public override void WhileInteracting()
    {
        CurrentProgress += Time.deltaTime / FillingDuration;
        FillingCanister.SetValue(CurrentProgress);
        EmptyingCanister?.SetValue(1 - CurrentProgress);
        if (CurrentProgress >= 1)
        {
            interactable = false;
            RedBulb.color = DarkColor;
            GreenBulb.color = Color.white;
            ParentGame.Completed();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
