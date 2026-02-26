using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    private PolygonCollider2D col;
    public Game ParentGame;

    public bool gameActive = false;
    public bool interactable = true;
    private bool interacting = false;
    [SerializeField] private string ComponentName;

    private void Awake()
    {
        ParentGame = transform.parent.GetComponent<Game>();
        ParentGame.RegisterComponent(ComponentName, this);
        col = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameActive) return;
        if (!interactable) return;
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("yeet");
            if (!col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) return;
            interacting = true;
            OnStartInteracting();
        }else if (interacting && Input.GetMouseButtonUp(0) ) {
            interacting = false;
            OnStopInteracting();
        }
    }

    public abstract void SetDefaultState(Vector3 data);

    public abstract void OnStartInteracting();

    public abstract void OnStopInteracting();
    public abstract void GameReset();
}
