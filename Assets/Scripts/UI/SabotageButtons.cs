using UnityEngine;
using UnityEngine.UI;

public class SabotageButtons : MonoBehaviour
{
    [SerializeField] private Button ReactorButton;
    [SerializeField] private Button O2Button;
    [SerializeField] private Material Cooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateButtons()
    {

    }

    public void SabotageReactor() 
    {
        GameStateManager.CurrentSabotage = SabotageState.Reactor;
    }

    public void SabotageO2()
    {
        GameStateManager.CurrentSabotage = SabotageState.O2;

    }
}
