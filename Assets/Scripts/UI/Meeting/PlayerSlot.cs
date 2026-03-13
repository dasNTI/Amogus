using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField] private Image PlayerIcon;
    [SerializeField] private TMPro.TextMeshProUGUI label;
    [SerializeField] private GameObject StarterIcon;
    [SerializeField] private Button VoteButton;

    public Meeting ParentMeeting;
    private int SlotPlayerId;

    bool VoteButtonVisible = false;
    bool VoteButtonAvailable = true;

    void Start()
    {
        PlayerIcon.material = Instantiate(PlayerIcon.material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupPlayer(Player p)
    {
        if (!p.startedMeeting) Destroy(StarterIcon);
        PlayerIcon.material.SetFloat("_Color", p.color);
        label.text = p.name;
        if (!p.alive)
        {
            PlayerIcon.material.SetInt("_Dimmed", 1);
            label.text += "(X)";
        }
    }

    public void VoteFor()
    {
        ParentMeeting.CastVote(PlayerManager.Playerlist[PlayerManager.OwnIndex].id, SlotPlayerId);
        VoteButtonAvailable = false;
    }

    public void SwitchVoteButtonVisible()
    {
        VoteButtonVisible = !VoteButtonVisible;
        SetVoteButtonVisible(VoteButtonVisible);
    }

    public void SetVoteButtonVisible(bool v)
    {
        VoteButton.gameObject.SetActive(v);
    }
}
