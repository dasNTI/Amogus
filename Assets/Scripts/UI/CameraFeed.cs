using UnityEngine;
using ZXing;
using UnityEngine.UI;

public class CameraFeed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Material mat;
    WebCamTexture CamTexture;
    [SerializeField] private CameraFeedButton button;
    [SerializeField] private int ScaleFactor = 4;
    [SerializeField] private int GameResetThreshold = 5;
    private int emptyChecks = 0;

    public Vector2 QrCodePosition;
    public Game CurrentActiveGame = null;

    private Rect FeedRect;
    private void Awake()
    {
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
    }
    void Start()
    {
        FeedRect = GetComponent<RectTransform>().rect;
        WebCamDevice[] devices = WebCamTexture.devices;
        CamTexture = new WebCamTexture(devices[0].name, Screen.width / ScaleFactor, Screen.height / ScaleFactor);
# if UNITY_EDITOR
        CamTexture = new WebCamTexture(devices[1].name, Screen.width / ScaleFactor, Screen.height / ScaleFactor); // An meinem Rechner sind die devices komisch
# endif


        mat.SetTexture("_MainTex", CamTexture);
        CamTexture.Play();
        InvokeRepeating("Scan", 1, 0.5f);
    }

    void Scan()
    {
        IBarcodeReader reader = new BarcodeReader();
        Result result = reader.Decode(CamTexture.GetPixels32(), CamTexture.width, CamTexture.height);
        if (result != null)
        {
            if (CurrentActiveGame != null) return;
            float x = (result.ResultPoints[0].X + result.ResultPoints[2].X) / 2 / CamTexture.width;
            float y = (result.ResultPoints[0].Y + result.ResultPoints[2].Y) / 2 / CamTexture.height;

            QrCodePosition = new Vector2((1 - y) * FeedRect.width, -x * FeedRect.height);

            button.CurrentButton(QrCodePosition, result.Text);
            emptyChecks = 0;
        } else
        {
            button.HideButton();
            if (CurrentActiveGame == null) return;
            emptyChecks++;
            if (emptyChecks > GameResetThreshold)
            {
                CurrentActiveGame.Close();
                CurrentActiveGame = null;
            }
        }
    }
}
