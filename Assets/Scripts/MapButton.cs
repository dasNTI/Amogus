using UnityEngine;

public class MapButton : MonoBehaviour
{
    [SerializeField] private GameObject map;
    bool visible = false;
    
    public void SwitchVisible()
    {
        visible = !visible;
        map.SetActive(visible);
    }
}
