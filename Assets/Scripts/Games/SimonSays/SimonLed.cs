using UnityEngine;

public class SimonLed : AestheticComponent
{
    [SerializeField] private Color DarkColor = new Color(0.7f, 0.7f, 0.7f);
    private SpriteRenderer sr;
    public override void GameReset(Task task)
    {
        SetLit(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void SetLit(bool lit)
    {
        if (lit)
        {
            sr.color = Color.white;
        }else
        {
            sr.color = DarkColor;
        }
    }
}
