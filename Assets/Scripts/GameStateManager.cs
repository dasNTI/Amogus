using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance = null;
    /* All diese Variablen wären auch im Multiplayer gesynct */
    private static GameState _currentGameState = GameState.Mainloop;
    public static GameState CurrentGameState
    {
        get
        {
            return _currentGameState;
        }
        set
        {
            _currentGameState = value;
            if (value == GameState.MeetingPending) instance.NotifyMeeting();
            if (value == GameState.ImpostorsWon || value == GameState.CrewmatesWon) SceneManager.LoadScene("WinScreen", LoadSceneMode.Single);
        }
    }
    public static int _playersInMeeting = 0;
    public static int PlayersInMeeting
    {
        get
        {
            return _playersInMeeting;
        }
        set
        {
            _playersInMeeting = value;
            if (value == PlayerManager.Playerlist.Count)
            {
                CurrentGameState = GameState.MeetingActive;
                instance.StartMeeting();
            }
        }
    }
    private static SabotageState _currentSabotage = SabotageState.None;
    public static SabotageState CurrentSabotage
    {
        get
        {
            return _currentSabotage;
        }
        set
        {
            _currentSabotage = value;
            if (value == SabotageState.None)
            {
                instance.NotifySabotage(false);
            }else
            {
                instance.NotifySabotage(true);
                SabotageSolutionStepsLeft = 2;
                SabotageTimer = SabotageTime;
            }
        }
    }
    const int SabotageTime = 60;
    static float SabotageTimer = -1;
    public static int SabotageSolutionStepsLeft = 0;
    public static float SabotageSolvedAt;


    private void Start()
    {
        instance = this;
    }

    public static void JoinMeeting(int playerId)
    {
        CurrentGameState = GameState.MeetingPending;
        if (PlayersInMeeting == 0)
        {
            PlayerManager.SetPlayerAsMeetingStarter(playerId);
        }
        PlayerManager.SetPlayerInMeeting(playerId, true);
        PlayersInMeeting++;
        PlayersInMeeting = PlayerManager.Playerlist.Count; // Die NPCs können ja schlecht akzeptieren, das is hier provisorisch
        SceneManager.LoadScene("Meeting", LoadSceneMode.Single);
    }

    public void StartMeeting()
    {
        Meeting.instance.OpenMeeting();
    }

    public void NotifyMeeting()
    {

    }

    public void NotifySabotage(bool active)
    {

    }

    private void Update()
    {
        if (CurrentSabotage != SabotageState.None)
        {
            if (SabotageTimer != -1)
            {
                SabotageTimer -= Time.deltaTime;
            }
        }
    }
}

public enum GameState
{
    Mainloop = 0,
    MeetingPending = 1,
    MeetingActive = 2,
    CrewmatesWon = 3,
    ImpostorsWon = 4
}

public enum SabotageState
{
    None = 0,
    Reactor = 1,
    O2 = 2
}