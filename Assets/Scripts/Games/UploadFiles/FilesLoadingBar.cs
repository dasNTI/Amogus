using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FilesLoadingBar : AstheticComponent
{
    private SpriteRenderer sr;
    [SerializeField] private TMPro.TextMeshProUGUI ProgressLabel;
    [SerializeField] private Animator FileAnimator;
    [SerializeField] private float LoadingDuration;
    [SerializeField] private string[] ProgressTexts;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public override void GameReset(Task task)
    {
        transform.localScale = new Vector3(0, 1, 1);
        ProgressLabel.text = "";
        sr.material.SetFloat("_Progress", 0);
        FileAnimator.SetBool("Flying", false);
    }

    public void StartProgress()
    {
        transform.DOScaleX(1, 0.5f).OnComplete(() =>
        {
            FileAnimator.SetBool("Flying", true);
            StartCoroutine(ShowProgress());
        });
    }

    IEnumerator ShowProgress()
    {
        float CurrentProgress = 0;
        float CurrentDurationLeft = LoadingDuration;
        for (int i = 0; i < ProgressTexts.Length; i++)
        {
            if (!gameActive) yield break;
            ProgressLabel.text = ProgressTexts[i];

            float duration = Random.Range(0.2f, 0.4f) * CurrentDurationLeft;
            CurrentProgress += Random.Range(0.1f, 0.6f) * (0.9f - CurrentProgress);
            CurrentDurationLeft -= duration;
            sr.material.DOFloat(CurrentProgress, "_Progress", duration * 0.75f);
            yield return new WaitForSeconds(duration);
        }

        sr.material.DOFloat(1, "_Progress", CurrentDurationLeft).OnComplete(() =>
        {
            FileAnimator.SetBool("Flying", false);
            if (gameActive) ParentGame.Completed();
        });
    }
}
