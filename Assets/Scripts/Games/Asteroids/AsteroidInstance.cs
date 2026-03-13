using DG.Tweening;
using UnityEngine;

public class AsteroidInstance : ButtonComponent
{
    [SerializeField] private Sprite[] AsteroidSprites;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private float DistanceFromCenter = 7.5f;
    [SerializeField] private Vector2 MaxGoalDivergence = new Vector2(1.8f, 2.8f);
    public PolygonCollider2D GameCollider;
    private Vector3 TrajectoryDelta;
    private float RotationSpeed;
    private float Speed = 8f;
    private float TrajectoryTimeElapsed = 0;

    public override void Awake() { }
    public void Start()
    {
        ParentGame.RegisterComponent(gameObject.name, this);
        col = GetComponent<PolygonCollider2D>();
        sr.sprite = AsteroidSprites[Mathf.FloorToInt(Random.value * AsteroidSprites.Length)];
        SetupTrajectory();
        gameActive = true;
    }

    public override void GameReset(Task task) { }

    public override void Update()
    {
        if (!gameActive) return;
        if (!interactable) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!col.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) return;
            if (!GameCollider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))) return;
            OnClick();
        }

        if (TrajectoryTimeElapsed > 1 && transform.localPosition.magnitude > DistanceFromCenter)
        {
            Destroy(gameObject);
            return;
        }

        transform.localPosition += TrajectoryDelta * Time.deltaTime;
        transform.eulerAngles = transform.eulerAngles + Vector3.forward * RotationSpeed;
        TrajectoryTimeElapsed += Time.deltaTime;
    }

    void SetupTrajectory()
    {
        float a = Random.value * 2 * Mathf.PI;
        Vector2 startOffset = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
        transform.localPosition = startOffset * DistanceFromCenter;

        Vector2 TrajectoryCenter = new Vector2(Random.Range(-1, 1) * MaxGoalDivergence.x, Random.Range(-1, 1) * MaxGoalDivergence.y);
        Vector2 GoalTraj = TrajectoryCenter - startOffset * DistanceFromCenter;

        TrajectoryDelta = GoalTraj.normalized * Speed;
        RotationSpeed = Random.Range(-1, 1) * 0.0005f / Time.deltaTime;
    }

    public override void OnClick()
    {
        ((ClearAsteroids)ParentGame).AsteroidDestroyed();
        ParentGame.DeleteComponent(gameObject.name);
        Destroy(gameObject);
    }

    public override void OnRelease() {}
}
