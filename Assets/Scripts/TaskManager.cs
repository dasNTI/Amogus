using UnityEngine;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static List<Task> Tasklist;
    [SerializeField] private List<Task> TaskCatalog;
    void Start()
    {
        Tasklist = TaskCatalog;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Task GetTask(string name)
    {
        Task result = null;
        foreach (Task task in Tasklist)
        {
            if (task.name == name) result = task;
        }
        return result;
    }

    public static void TickOffTask(Task t)
    {
        Tasklist.Remove(t);
        if (!t.TwoPart) return;

        Task followup = new Task();
        followup.name = t.followUpName;
        followup.followUpLabel = t.followUpLabel;
        followup.game = t.followUpGame;
        Tasklist.Add(followup);
    }
}

[System.Serializable]
public class Task
{
    public string name;
    public string label;
    public Game game;
    public bool TwoPart = false;
    public string followUpName;
    public string followUpLabel;
    public Game followUpGame = null;
}