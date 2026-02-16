using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public abstract class Game : MonoBehaviour
{
    const int CameraMoveOffset = 10;

    private Dictionary<string, GameComponent> components = new Dictionary<string, GameComponent>();
    private Action OnComplete;
    void Start()
    {
        InitialSetup(); 
    }

    public void Close()
    {
        Camera.main.transform.DOMoveY(transform.position.y, 0.5f);
        Lock();
    }

    public void Open(Action onComplete, bool imposter = false)
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y - CameraMoveOffset, transform.position.z);
        Camera.main.transform.DOMoveY(transform.position.y, 0.5f);
        Reset();
        if (imposter) return;
        OnComplete = onComplete;
        Unlock();
    }

    public abstract void InitialSetup();
    public abstract void Reset();

    private void Completed()
    {
        OnComplete();
        OnComplete = null;
    }

    private void Unlock()
    {
        foreach (string c in components.Keys)
            components[c].gameActive = false;
    }
    private void Lock()
    {
        foreach (string c in components.Keys)
            components[c].gameActive = false;
    }

    public void RegisterComponent(string name, GameComponent gc)
    {
        components[name] = gc;
    }
}
