using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;
public class LoadSceneUI : MonoBehaviour
{
    [SerializeField] private Image loadingProgress;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image dim1;
    [SerializeField] private Image dim2;
    [SerializeField] private TextMeshProUGUI textProcess;
    bool complete = false;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Observer.AddListener(constr.DONELOADSCENEASYNC, ToCompleteProgress);
        Observer.AddListener(constr.DONELOADLEVEL, ToCompleteProgress);
        Observer.AddListener(constr.NEXTLEVEL,ChangeToHome);
        Observer.AddListener(constr.RELOADLEVEL,ChangeToHome);
        InitLoad();
    }
    private void ChangeToHome()
    {
        gameObject.SetActive(true);
        loadingProgress.fillAmount = 0;
        dim1.DOFade(1, 0.3f).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                canvasGroup.alpha = 1f;   
                InitLoad();
            });
    }
    private void InitLoad()
    {
        complete = false;
        canvasGroup.alpha = 1f;
        dim1.DOFade(0,0.4f).SetEase(Ease.Linear);
        Invoke(nameof(BeginProgress), 0.4f);
    }
    private void BeginProgress()
    {
        if (complete) return;
        loadingProgress.DOFillAmount(0.3f, 7f)
            .OnUpdate(() =>
            {
                textProcess.text = ((int)(loadingProgress.fillAmount * 100f)).ToString()+"%";
            })
            .SetEase(Ease.OutQuad);
        Invoke(nameof(WaitDoneProgress), 5f);
    }
    private void WaitDoneProgress()
    {
        if (complete) return;
        loadingProgress.DOFillAmount(1f, 5f)
            .OnUpdate(() =>
            {
                textProcess.text = ((int)(loadingProgress.fillAmount * 100f)).ToString()+"%";
            })
            .SetEase(Ease.Linear);
    }
    private void ToCompleteProgress()
    {
        complete = true;
        loadingProgress.DOFillAmount(1f, 3f).SetEase(Ease.InOutQuint)
            .OnUpdate(() =>
            {
                textProcess.text = ((int)(loadingProgress.fillAmount * 100f)).ToString() + "%";
            })
            .OnComplete(() =>
            {
                dim1.DOFade(1, 0.4f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    canvasGroup.alpha = 0f;
                    dim1.DOFade(0, 0.3f).SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    }).SetEase(Ease.Linear);
                }).SetEase(Ease.Linear);
            });
    }
}
