using UnityEngine;

public class GetFrequency : Game
{
    public override void InitialSetup()
    {
        float goalValue = Random.Range(0.1f, 0.9f);
        float minDistance = 0.2f;
        float startValue = goalValue;
        do
        {
            startValue += (Random.value - 0.5f) * 1.5f;
        } while (Mathf.Abs(goalValue - startValue) < minDistance || startValue > 0.85f || startValue < 0.15f);

        components["Frequency Dial"].ReturnAs<FrequencyDial>().SetDefaultState(new float[] { goalValue, startValue});
    }
}
