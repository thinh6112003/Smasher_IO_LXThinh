using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Transform progressGamePlay;
    [SerializeField] private GameObject tutorial;
    private void OnEnable()
    {
        tutorial.SetActive(true);
        progressGamePlay.localScale = new Vector3(0, 0, 1);
        progressGamePlay.DOScale(new Vector3(1, 1, 1), 0.6f).SetEase(Ease.Linear);
    }
}
