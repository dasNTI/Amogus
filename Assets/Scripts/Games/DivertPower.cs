using UnityEngine;

public class DivertPower : Game
{
    public override void InitialSetup()
    {
        
    }

    public void SetOthers(string ApartFrom, float CurrentUserVolume)
    {
        float OtherVolume = 0.5f - (CurrentUserVolume - 0.5f) * 0.3f;
        foreach (var key in components.Keys)
        {
            if (!key.StartsWith("PowerBar")) continue;
            if (key == ApartFrom) continue;
            ((PowerBar)components[key]).SetValue(OtherVolume);
        }
    }

    public override void Reset(Task t)
    {
        foreach (var component in components.Values)
        {
            component.GameReset(t);
        }

        string n = "Slider" + t.name.Split('_')[1];
        if (components.ContainsKey(n)) ((PowerDivertSlider)components[n]).Activate();
    }
}
