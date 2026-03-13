using System.Collections.Generic;
using UnityEngine;

public class Meeting : MonoBehaviour
{
    public static Meeting instance = null;

    [SerializeField] private GameObject PlayerSlotPrefab;
    [SerializeField] private GameObject MeetingCover;

    public Dictionary<int, int[]> Votes = new Dictionary<int, int[]>();
    public int VotesLeft;

    void Start()
    {
        if (instance == null) instance = this;
        VotesLeft = PlayerManager.Playerlist.Count;
    }

    public void OpenMeeting()
    {
        Destroy(MeetingCover);
    }

    public void CastVote(int ownPlayerId, int votedPlayerId)
    {
        
    }

    public void CloseOtherButtons()
    {

    }
}
