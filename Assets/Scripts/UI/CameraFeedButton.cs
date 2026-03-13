using UnityEngine;
using UnityEngine.UI;

public class CameraFeedButton : MonoBehaviour
{
    private RectTransform rt;
    private Button btn;
    private string CurrentCodeContent;
    private Task CurrentTask;
    private int CurrentlySelectedPlayer = 0;
    private string CurrentlySelectedUtility = "";
    private CodeType CurrentCodeType = CodeType.None;

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
                if (GameStateManager.CurrentGameState != GameState.Mainloop) break;

                CurrentCodeType = CodeType.Task;
                btn.image.sprite = UseImage;
                Task t = TaskManager.GetTask(data.Substring(2));
                if ((t == null || t.IsDone) && !PlayerManager.PlayerIsImpostor)
                {
                    btn.interactable = false;
                    return;
                }
                btn.interactable = true;
                CurrentTask = t;
                break;

            case 'P':
                if (GameStateManager.CurrentGameState != GameState.Mainloop) break;

                CurrentCodeType = CodeType.Player;
                int id = int.Parse(data.Substring(2));
                CurrentlySelectedPlayer = id;

                btn.image.sprite = ReportImage;
                if (PlayerManager.IsPlayerAlive(id))
                {
                    if (PlayerManager.PlayerIsImpostor)
                    {
                        btn.image.sprite = KillImage;
                        btn.interactable = true;
                    }
                    else
                    {
                        btn.interactable = false;
                    }
                }
                else
                {
                    btn.interactable = true;
                }
                break;

            case 'D':
                CurrentCodeType = CodeType.Debug;
                btn.image.sprite = UseImage;   
                btn.interactable = true;
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
        switch (CurrentCodeType)
        {
            case CodeType.Task:
                if (CurrentTask != null)
                {
                    CurrentTask.game.Open(CurrentTask, () =>
                    {
                        TaskManager.TickOffTask(CurrentTask);
                        feed.CurrentActiveGame = null;
                        CurrentTask = null;
                    }, PlayerManager.PlayerIsImpostor);
                    feed.CurrentActiveGame = CurrentTask.game;
                    HideButton();
                }
                break;
            case CodeType.Player:
                if (PlayerManager.IsPlayerAlive(CurrentlySelectedPlayer))
                {
                    PlayerManager.KillPlayer(CurrentlySelectedPlayer);
                    CurrentCodeContent = "";
                }else
                {
                    GameStateManager.JoinMeeting(PlayerManager.Playerlist[PlayerManager.OwnIndex].id);
                }
                break;

            case CodeType.Debug:
                if (CurrentCodeContent.EndsWith("SwitchImposter"))
                {
                    PlayerManager.SwitchImposter(!PlayerManager.PlayerIsImpostor);
                }
                break;
        }
    }
}

public enum CodeType
{
    None = 0,
    Task = 1,
    Player = 2,
    Utility = 3,
    Debug = 4
}