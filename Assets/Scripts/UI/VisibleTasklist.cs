using System.Collections.Generic;
using UnityEngine;

public class VisibleTasklist : MonoBehaviour
{
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private Color HalfDoneColor;
    [SerializeField] private Color DoneColor;

    List<GameObject> labels = new List<GameObject>();

    bool visible = true;

    void Start()
    {
        
    }

    public void UpdateList()
    {
        labels = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
        foreach (Task t in TaskManager.Tasklist)
        {
            GameObject label = Instantiate(ItemPrefab);
            labels.Add(label);
            TMPro.TextMeshProUGUI text = label.GetComponent<TMPro.TextMeshProUGUI>();
            text.text = t.label;    
            if (t.IsDone)
            {
                text.color = DoneColor;
            }else if (t.IsFollowUpItself)
            {
                text.color = HalfDoneColor;
            }
            label.transform.SetParent(transform);
            label.transform.localScale = Vector3.one;
        }
    }

    public void SwitchVisible()
    {
        Debug.Log("yeet");
        visible = !visible;
        foreach (var lbl in labels)
        {
            lbl.SetActive(visible);
        }
    }
}
