using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class FrequencyDial : RotorComponent
{
    [SerializeField] private GameObject GreenGlow;
    [SerializeField] private GameObject RedGlow;
    [SerializeField] private float CorrectValueThreshold = 0.025f;
    [SerializeField] private SpriteRenderer SignalRenderer;
    public float GoalValue;
    public float DefaultValue;
    private float CorrectValueTimer = 0;
    private float CorrectValueTime = 3f;
    private bool CurrentlyCorrect;

    private void Start()
    {
        SignalRenderer.material = Instantiate(SignalRenderer.material);
    }

    public override void Update()
    {
        if (!gameActive) return;
        if (!interactable) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) return;
            interacting = true;
            OnStartInteracting();
        }
        else if (interacting && Input.GetMouseButtonUp(0))
        {
            interacting = false;
            OnStopInteracting();
        }

        if (interacting) WhileInteracting();

        if (CurrentlyCorrect)
        {
            CorrectValueTimer -= Time.deltaTime;
            if (CorrectValueTimer < 0)
            {
                GreenGlow.SetActive(true);
                RedGlow.SetActive(false);
                ParentGame.Completed();
            }
        }
    }

    public override void SetDefaultState(float[] data)
    {
        GoalValue = data[0];
        DefaultValue = data[1];
    }

    public override void GameReset(Task task)
    {
        GreenGlow.SetActive(false);
        RedGlow.SetActive(true);
        SetValue(DefaultValue);
        CorrectValueTimer = CorrectValueTime;
        SignalRenderer.material.SetFloat("_Noise", 1 - Mathf.Abs(CurrentValue - GoalValue));
    }

    public override void OnValueChange(float newValue)
    {
        CurrentValue = newValue;
        float dif = Mathf.Abs(CurrentValue - GoalValue);
        if (dif < CorrectValueThreshold)
        {
            CurrentlyCorrect = true;
        }else
        {
            CorrectValueTimer = CorrectValueTime;
            CurrentlyCorrect = false;
        }

        SignalRenderer.material.SetFloat("_Noise", 1 - dif);
    }
}
