using UnityEngine;

public class FilesButton : ButtonComponent
{

    [SerializeField] private Sprite UploadSprite;
    [SerializeField] private Sprite DownloadSprite;
    [SerializeField] private FilesLoadingBar loadingBar;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = Instantiate(sr.material);
    }
    public override void OnClick()
    {
        sr.color = Color.clear;
        loadingBar.StartProgress();
    }

    public override void GameReset(Task task)
    {   
        interactable = true;
        sr.color = Color.white;
        if (task.name == "UploadFiles")
        {
            sr.sprite = UploadSprite;
        }else
        {
            sr.sprite = DownloadSprite;
        }
    }


    public override void OnRelease()
    {}
}
