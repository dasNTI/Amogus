using UnityEngine;

public class FilesFolderCover : AstheticComponent
{
    [SerializeField] private Sprite TabletSprite;
    [SerializeField] private Sprite OtherSprite;
    [SerializeField] private bool IsRightFolder;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void GameReset(Task task)
    {
        if (task.name == "UploadFiles")
        {
            if (IsRightFolder)
            {
                sr.sprite = OtherSprite;
            }else
            {
                sr.sprite = TabletSprite;
            }
        }else
        {
            if (IsRightFolder)
            {
                sr.sprite = TabletSprite;
            }
            else
            {
                sr.sprite = OtherSprite;
            }
        }
    }
}
