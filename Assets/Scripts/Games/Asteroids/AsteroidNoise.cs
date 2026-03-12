using UnityEngine;

public class AsteroidNoise : ButtonComponent
{
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private LineRenderer LeftLine;
    [SerializeField] private LineRenderer Rightine;

    public override void GameReset(Task task)
    {
        MoveCrosshair(transform.position);
    }

    public override void OnClick()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoveCrosshair(pos);
    }

    public override void OnRelease()
    {
        
    }

    void MoveCrosshair(Vector3 pos)
    {
        pos = new Vector3(pos.x, pos.y, Crosshair.transform.position.z);
        Crosshair.transform.position = pos;
        LeftLine.SetPosition(1, pos);
        Rightine.SetPosition(1, pos);
    }
}
