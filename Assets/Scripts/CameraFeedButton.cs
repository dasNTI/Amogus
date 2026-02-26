using UnityEngine;
using UnityEngine.UI;

public class CameraFeedButton : MonoBehaviour
{
    private RectTransform rt;
    private Button btn;
    private string CurrentCodeContent;
    private Task CurrentTask;

    [SerializeField] private CameraFeed feed;
    [SerializeField] private Sprite UseImage;
    [SerializeField] private Sprite ReportImage;
    [SerializeField] private Sprite KillImage;

    void Start()
    {   
        rt = GetComponent<RectTransform>();
        btn = GetComponent<Button>();
    }   

    public void CurrentButton(Vector2 position, string data)
    {
        btn.interactable = true;
        rt.anchoredPosition = position;
        if (CurrentCodeContent == data)
        {
            return;
        }
        CurrentCodeContent = data;

        string[] args = data.Split('_');
        switch(args[0])
        {
            case "T":
                btn.image.sprite = UseImage;
                Task t = TaskManager.GetTask(args[1]);
                if (t == null)
                {
                    btn.interactable = false;
                    return;
                }
                btn.interactable = true;
                CurrentTask = t;
                break;

            case "P":
                btn.image.sprite = ReportImage;
                break;
        }
    }

    public void HideButton()
    {
        btn.interactable = false;
        CurrentCodeContent = "";
    }

    public void OnBtnPress()
    {
        if (CurrentTask != null)
        {
            CurrentTask.game.Open(() =>
            {
                TaskManager.TickOffTask(CurrentTask);
                feed.CurrentActiveGame = null;
            });
            feed.CurrentActiveGame = CurrentTask.game;
            HideButton();
        }
    }
}