using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    [SerializeField] private GameObject map;
    [SerializeField] private Image MapImage;
    bool visible = false;
    [SerializeField] private GameObject ImposterButtons;
    [SerializeField] private Sprite ImposterMapSprite;
    [SerializeField] private Sprite CrewmateMapSprite;
    [SerializeField] private SabotageButtons sb;
    
    public void SwitchVisible()
    {
        visible = !visible;
        map.SetActive(visible);
        if (PlayerManager.PlayerIsImpostor)
        {
            sb.UpdateButtons(); 
            ImposterButtons.SetActive(true);
            MapImage.sprite = ImposterMapSprite;
        }else
        {
            ImposterButtons.SetActive(false);
            MapImage.sprite = CrewmateMapSprite;
        }
    }
}
