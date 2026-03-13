using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public abstract class Game : MonoBehaviour
{
    const int CameraMoveOffset = 15;

    public Dictionary<string, GameComponent> components = new Dictionary<string, GameComponent>();
    private Action OnComplete;
    [NonSerialized] public bool gameActive;
    void Start()
    {
        InitialSetup(); 
    }

    public void Close()
    {
        Camera.main.transform.DOMoveY(transform.position.y + CameraMoveOffset, 0.5f);
        Lock(); 
    }

    public void Open(Task TaskData, Action onComplete, bool imposter = false)
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y - CameraMoveOffset, Camera.main.transform.position.z);
        Camera.main.transform.DOMoveY(transform.position.y, 0.5f);
        Reset(TaskData);
        if (imposter) return;
        OnComplete = onComplete;
        Unlock();
    }

    public abstract void InitialSetup();
    public virtual void Reset(Task t)
    {
        foreach (var component in components.Values)
        {
            component.GameReset(t);
        }
    }

    public void Completed()
    {
        Lock();
        TaskManager.instance.DoCompletedVisual(() =>
        {
            Close();
            OnComplete();
            OnComplete = null;
        });
    }

    private void Unlock()
    {
        gameActive = true;
        foreach (string c in components.Keys)
            components[c].gameActive = true;
    }
    private void Lock()
    {
        gameActive = false;
        foreach (string c in components.Keys)
        {
            components[c].gameActive = false;
            DOTween.Kill(components[c]);
            components[c].CancelInvoke();
        }
    }

    public void RegisterComponent(string name, GameComponent gc)
    {
        components[name] = gc;
    }

    public void DeleteComponent(string name)
    {
        components.Remove(name);
    }

    public T ReturnAs<T>() where T : Game
    {
        return (T)this;
    }
}
