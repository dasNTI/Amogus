using UnityEngine;
using DG.Tweening;

public class PowerButton : ButtonComponent
{
    
    [SerializeField] private Material UpperGridMaterial;
    [SerializeField] private Material LowerGridMaterial;

    public override void OnClick()
    {
        UpperGridMaterial.SetInt("_On", 1);
        LowerGridMaterial.SetInt("_On", 1);
        transform.DORotate(new Vector3(0, 0, -90), 0.5f).OnComplete(() =>
        {
            ParentGame.Completed();
        });
    }

    public override void OnRelease()
    {
        
    }

    public override void GameReset(Task task)
    {
        transform.eulerAngles = Vector3.zero;
        UpperGridMaterial.SetInt("_On", 0);
        LowerGridMaterial.SetInt("_On", 0);
    }
}
