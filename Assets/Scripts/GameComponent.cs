using UnityEngine;

public abstract class GameComponent : MonoBehaviour
{
    public static float UnitsPerPixel = 0;

    [SerializeField] public string ComponentName;
    public PolygonCollider2D col;
    public Game ParentGame;

    public bool gameActive = false;
    public bool interactable = true;
    private bool interacting = false;

    public virtual void Awake()
    {
        if (!ParentGame) ParentGame = transform.parent.GetComponent<Game>();
        ParentGame.RegisterComponent(ComponentName, this);
        col = GetComponent<PolygonCollider2D>();
        if (UnitsPerPixel == 0)
        {
            UnitsPerPixel = 1f / ((Screen.height / 2.0f) / Camera.main.orthographicSize);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!gameActive) return;
        if (!interactable) return;
        if (Input.GetMouseButtonDown(0)) {
            if (!col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) return;
            interacting = true;
            OnStartInteracting();
        }else if (interacting && Input.GetMouseButtonUp(0) ) {
            interacting = false;
            OnStopInteracting();
        }

        if (interacting) WhileInteracting();
    }

    public virtual void SetDefaultState(float data) { }

    public abstract void OnStartInteracting();
    public virtual void WhileInteracting() { }

    public abstract void OnStopInteracting();
    public abstract void GameReset(Task task);
}
