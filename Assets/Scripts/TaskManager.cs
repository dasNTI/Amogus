using UnityEngine;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Rendering;
using Unity.VisualScripting;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public static List<Task> Tasklist = new List<Task>();
    public static TaskManager instance = null;

    public static int DoneTasks = 0;        // Die beiden Sachen wären im Multiplayer gesynct
    public static int TotalTaskCount = 0;

    [SerializeField] private List<Task> TaskCatalog;
    [SerializeField] private VisibleTasklist VisibleList;
    [SerializeField] private TaskMap taskMap;

    [Header("Complete Animation")]
    [SerializeField] private RectTransform CompletedText;
    [SerializeField] private float CompletedTextOffset;
    [SerializeField] private Image ProgressBarSr;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
        if (TotalTaskCount == 0) DistributeTasks();
    }

    void Start()
    {
        ProgressBarSr.material = Instantiate(ProgressBarSr.material);
        VisibleList.UpdateList();
        taskMap.UpdateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DistributeTasks() // Müsste man im Multiplayer überarbeiten
    {
        for (int i = 0; i < TaskCatalog.Count; i++)
        {
            Tasklist.Add(TaskCatalog[i]);
        }
        TotalTaskCount = Tasklist.Count;
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

    public static void TickOffTask(Task task)
    {
        Task t = Tasklist[Tasklist.FindIndex(e => e.name == task.name)];
        if (t.TwoPart)
        {
            Task followup = new Task
            {
                name = t.followUpName,
                label = t.followUpLabel,
                game = t.followUpGame,
                MapPosition = t.followUpPosition,
                IsFollowUpItself = true
            };
            Tasklist[Tasklist.IndexOf(t)] = followup;
        }
        else
        {
            int index = Tasklist.IndexOf(t);
            t.IsDone = true;
            Tasklist[index] = t;

            DoneTasks++;
            instance.ProgressBarSr.material.DOFloat(DoneTasks * 1f / TotalTaskCount, "_Progress", 0.5f);
            if (DoneTasks == TotalTaskCount) GameStateManager.CurrentGameState = GameState.CrewmatesWon;
        }
        
        instance.VisibleList.UpdateList();
        instance.taskMap.UpdateMap();
    }

    public void DoCompletedVisual(Action callback)
    {
        CompletedText.localPosition = new Vector2(CompletedText.localPosition.x, CompletedTextOffset);  
        CompletedText.DOLocalMoveY(0, 1).OnComplete(() =>
        {
            CompletedText.DOLocalMoveY(-CompletedTextOffset, 1).OnComplete(() =>
            {
                callback();
            });
        });
    }
}

[System.Serializable]
public class Task
{
    public string name;
    public string label;
    public Game game;
    public Vector2 MapPosition;
    public bool IsFollowUpItself = false;
    public bool IsDone = false;

    [Header("Follow Up")]
    public bool TwoPart = false;
    public string followUpName;
    public string followUpLabel;
    public Game followUpGame = null;
    public Vector2 followUpPosition;
}