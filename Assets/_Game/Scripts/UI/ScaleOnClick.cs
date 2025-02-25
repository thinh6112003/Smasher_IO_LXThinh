using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleOnClick : MonoBehaviour
{
    Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        transform.DOScale(Vector3.one * 1.15f, 0.15f)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                transform.DOScale(Vector3.one, 0.15f)
                .SetEase(Ease.OutSine);
            });
    }
}