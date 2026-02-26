using UnityEngine;

public class AcceptPower : Game
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitialSetup()
    {
        
    }
    public override void Reset()
    {
        components["PowerButton"].GameReset();
    }
}
