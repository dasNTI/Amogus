using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    public bool Locked = false;
    [SerializeField] private string ComponentName;
    private void Awake()
    {
        transform.parent.GetComponent<Game>().RegisterComponent(ComponentName, this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void SetDefaultState(Vector3 data);
}
