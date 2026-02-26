using UnityEngine;
using DG.Tweening;

public class PowerButton : ButtonComponent
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnClick()
    {
        transform.DORotate(new Vector3(0, 0, -90), 0.5f).OnComplete(() =>
        {
            ParentGame.Completed();
        });
    }

    public override void OnRelease()
    {
        
    }

    public override void GameReset()
    {
        transform.eulerAngles = Vector3.zero;
    }
}
