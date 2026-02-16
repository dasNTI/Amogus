using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    private PolygonCollider2D collider;

    public bool gameActive = false;
    private bool interactable = true;
    private bool interacting = false;
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
        if (!gameActive) return;
        if (Input.GetMouseButtonDown(2) && !interacting) {

        }else if (Input.GetMouseButtonUp(2)) {
            interacting = false;
            OnStopInteracting();
        }
    }

    public abstract void SetDefaultState(Vector3 data);

    public abstract void HandleInteracting();

    public abstract void OnStopInteracting();
}
