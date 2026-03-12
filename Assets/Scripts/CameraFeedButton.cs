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
        btn.targetGraphic.color = Color.white;
        rt.anchoredPosition = position;
        if (CurrentCodeContent == data)
        {
            return;
        }
        CurrentCodeContent = data;

        switch(data[0])
        {
            case 'T':
                btn.image.sprite = UseImage;
                Task t = TaskManager.GetTask(data.Substring(2));
                if (t == null || t.IsDone)
                {
                    btn.interactable = false;
                    return;
                }
                btn.interactable = true;
                CurrentTask = t;
                break;

            case 'P':
                btn.image.sprite = ReportImage;
                break;
        }
    }

    public void HideButton()
    {
        btn.targetGraphic.color = Color.clear;
        btn.interactable = false;
        CurrentCodeContent = "";
    }

    public void OnBtnPress()
    {
        if (CurrentTask != null)
        {
            CurrentTask.game.Open(CurrentTask, () =>
            {
                TaskManager.TickOffTask(CurrentTask);
                feed.CurrentActiveGame = null;
                CurrentTask = null;
            });
            feed.CurrentActiveGame = CurrentTask.game;
            HideButton();
        }
    }
}