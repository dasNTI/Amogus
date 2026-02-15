using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static Task[] Tasklist;
    void Start()
    {
        
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
}

[System.Serializable]
public class Task
{
    public string name;
    public string label;
    public Game game;
    public bool TwoPart = false;
    public string followUpLabel;
    public Game followUpGame = null;
}