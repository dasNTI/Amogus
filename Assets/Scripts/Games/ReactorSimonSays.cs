using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ReactorSimonSays : Game
{
    int[] Sequence = new int[5];
    int CurrentCheckLength = 0;
    int CurrentCheckIndex = 0;
    bool RegisterButtons = true;
    public override void InitialSetup()
    {
        NewSequence();
    }

    public void ButtonPressed(int index)
    {
        if (!RegisterButtons) return;
        if (index == Sequence[CurrentCheckIndex])
        {
            CurrentCheckIndex++;
            if (CurrentCheckIndex >= CurrentCheckLength)
            {
                CurrentCheckLength++;
                if (CurrentCheckLength > 5)
                {
                    Completed();
                    return;
                }
                SetLeds(CurrentCheckLength);
                CurrentCheckIndex = 0;
                StartCoroutine(ShowSequence());
            }
        }else
        {
            SetLeds(0);
            StartCoroutine(ButtonsWrong());
        }
    }

    public override void Reset(Task t)
    {
        CurrentCheckIndex = 0;
        CurrentCheckLength = 1;
        foreach (var component in components.Values)
        {
            component.GameReset(t);
        }
        SetLeds(CurrentCheckLength);
        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        RegisterButtons = false;
        yield return new WaitForSeconds(1);
        SetButtonsActive(false);
        for (int i = 0; i < CurrentCheckLength; i++)
        {
            components["BluePad" + Sequence[i]].ReturnAs<SimonBluePad>().SetVisible(true);
            yield return new WaitForSeconds(0.2f);
            components["BluePad" + Sequence[i]].ReturnAs<SimonBluePad>().SetVisible(false);
            if (i != CurrentCheckLength - 1) yield return new WaitForSeconds(0.3f);
        }
        SetButtonsActive(true);
        RegisterButtons = true;
    }

    void NewSequence()
    {
        for (int i = 0; i < Sequence.Length; i++)
        {
            Sequence[i] = Mathf.FloorToInt(Random.Range(0, 9));
            if (i != 0) while (Sequence[i] == Sequence[i - 1]) Sequence[i] = Mathf.FloorToInt(Random.Range(0, 9));
        }
    }

    IEnumerator ButtonsWrong()
    {
        SetButtonsActive(false);
        for (int i = 0; i < 3; i++) 
        {
            SetButtonsRed(true);
            yield return new WaitForSeconds(0.25f);
            SetButtonsRed(false);
            yield return new WaitForSeconds(0.25f);
        }

        NewSequence();
        CurrentCheckIndex = 0;
        CurrentCheckLength = 1;
        SetLeds(1);
        StartCoroutine(ShowSequence());
    }

    void SetButtonsActive(bool v)
    {
        for (int i = 0; i < 9; i++)
        {
            components["Button" + i].interactable = v;
        }
    }


    void SetButtonsRed(bool v)
    {
        for (int i = 0; i < 9; i++)
        {
            components["Button" + i].ReturnAs<SimonBtn>().SetRed(v);
        }
    }

    void SetLeds(int stagesOn)
    {
        for (int i = 0; i < 5; i++)
        {
            components["Led" + i].ReturnAs<SimonLed>().SetLit(i < stagesOn);
        }
    }
}
