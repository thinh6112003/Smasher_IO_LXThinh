using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;

public class Tutorial : MonoBehaviour
{
    public RectTransform hintHandTransform;
    float timer = 0;
    Action action;
    Vector3 scaleHint = new Vector3 (0.65f, 0.65f, 1f);
    private void OnEnable()
    {
        hintHandTransform.localScale = Vector3.zero;
        hintHandTransform.DOScale(scaleHint, 0.6f);
        timer = 0;
        action += ToDisable;
    }
    private void Update()
    {
        if(action != null)
        {
            action.Invoke();
        }
    }
    private void ToDisable()
    {
        timer += Time.deltaTime;
        if(timer > 4f)
        {
            gameObject.SetActive(false);
            action -= ToDisable;
        }
    }
}
