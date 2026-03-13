using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    /* Wäre im Multiplayer Synchronisiert */
    public static List<Player> Playerlist = new List<Player>();
    public static int DeadPlayers = 0;
    /* Wäre rein Client Side */
    public static int OwnIndex = 0;
    public static bool PlayerIsImpostor = false;

    [SerializeField] private List<Player> PlayerTestingCatalogue = new List<Player>();

    private void Awake()
    {
        Playerlist = PlayerTestingCatalogue;
    }

    void Start()
    {
        if (instance == null) instance = this;
    }

    public static bool IsPlayerAlive(int id)
    {
        Player p = GetPlayer(id);
        return p.alive; 
    }

    public static Player GetPlayer(int id)
    {
        return Playerlist.Find(p => p.id == id);
    }

    public static void SwitchImposter(bool IsImposter)
    {
        PlayerIsImpostor = IsImposter;
        Playerlist[OwnIndex].impostor = IsImposter;
        Debug.Log("imposter switch");
    }

    public static void KillPlayer(int id)
    {
        int index = Playerlist.FindIndex(p => p.id == id);
        Playerlist[index].alive = false;
        // Im Multiplayer wäre hier jetzt noch ne beidseitige Animation oder so und der getötete Spieler müsste sich hinsetzen
    }

    public static void SetPlayerInMeeting(int id, bool inMeeting)
    {
        int index = Playerlist.FindIndex(p => p.id == id);
        Playerlist[index].inMeeting = inMeeting;
    }
    public static void SetPlayerAsMeetingStarter(int id)
    {
        int index = Playerlist.FindIndex(p => p.id == id);
        Playerlist[index].startedMeeting = true;
    }

    public static void ResetAfterMeeting()
    {
        foreach (Player p in Playerlist)
        {
            p.inMeeting = false;
            p.startedMeeting = false;
        }
    }
} 

[System.Serializable]
public class Player
{
    public string name;
    public int id;
    public float color;
    public bool alive = true;
    public bool impostor = false;
    public bool inMeeting = false;
    public bool startedMeeting = false;
}
