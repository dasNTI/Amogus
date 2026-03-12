using UnityEngine;

public class ClearAsteroids : Game
{
    [SerializeField] private GameObject AsteroidPrefab;
    private int Destroyed = 0;
    [SerializeField] private float AsteroidGap = 0.5f;
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private PolygonCollider2D gameCollider;
    public override void InitialSetup()
    {
        
    }

    public override void Reset(Task t)
    {
        Destroyed = 0;
        text.text = "Destroyed: " + Destroyed;
        foreach (GameComponent i in components.Values)
        {
            i.GameReset(t);
        }
        InvokeRepeating("NewAsteroid", 0, AsteroidGap);
    }

    void NewAsteroid()
    {
        if (!gameActive)
        {
            CancelInvoke();
            return;
        }

        GameObject ast = Instantiate(AsteroidPrefab);
        AsteroidInstance instance = ast.GetComponent<AsteroidInstance>();
        instance.ParentGame = this;
        instance.GameCollider = gameCollider;
        ast.transform.SetParent(transform, false);
    }

    public void AsteroidDestroyed()
    {
        Destroyed++;
        text.text = "Destroyed: " + Destroyed;
        if (Destroyed >= 20) Completed();
    }
}
