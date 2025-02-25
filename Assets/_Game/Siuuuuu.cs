using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class Siuuuuu : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 50,0);
        GetComponent<RectTransform>().DOAnchorPosY(400, 0.5f).SetEase(Ease.OutBack);
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).SetDelay(0.3f).OnComplete(() => {
                gameObject.SetActive(false);
            });
        });
    }
}
