using UnityEngine;

public class SimonBtn : ButtonComponent
{
    [SerializeField] private Color ClickedColor;
    [SerializeField] private Color WrongColor;
    [SerializeField] private int index;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        interactable = false;
    }

    public override void GameReset(Task task)
    {
        sr.color = Color.white;
    }

    public override void OnClick()
    {
        sr.color = ClickedColor;
        ParentGame.ReturnAs<ReactorSimonSays>().ButtonPressed(index);
    }

    public override void OnRelease()
    {
        sr.color = Color.white;
    }

    public void SetRed(bool v)
    {
        if (v)
        {
            sr.color = WrongColor;
        }else
        {
            sr.color = Color.white;
        }
    }
}
