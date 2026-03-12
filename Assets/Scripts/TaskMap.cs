using UnityEngine;

public class TaskMap : MonoBehaviour
{
    [SerializeField] private GameObject TaskPrefab;

    public void UpdateMap()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (Task i in TaskManager.Tasklist)
        {
            if (i.IsDone) continue;
            GameObject t = Instantiate(TaskPrefab);
            t.transform.SetParent(transform, false);
            t.transform.localScale = Vector3.one;
            t.transform.localPosition = i.MapPosition;
        }
    }
}
