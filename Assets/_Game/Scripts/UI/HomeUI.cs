using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

public class HomeUI : Singleton<HomeUI>
{
    [SerializeField] private Button playBtn;
    [SerializeField] private GameObject Logo;
    [SerializeField] private GameObject playIcon;
    [SerializeField] private GameObject levelIcon;
    [SerializeField] private RectTransform skinShopBtn;
    [SerializeField] private RectTransform settingBtn;
    [SerializeField] private RectTransform weaponShopBtn;


    private Vector2 originalAnchorMin;  // Lưu lại giá trị anchorMin ban đầu
    private Vector2 originalAnchorMax;  // Lưu lại giá trị anchorMax ban đầu
    private Vector2 originalPosition;  // Lưu lại vị trí ban đầu của shopPanel

    private Vector3 originalSkinShopPos;
    private Vector3 originalSettingPos;
    private Vector3 originalWeaponShopPos;
    private void Awake()
    {
        originalSettingPos = settingBtn.anchoredPosition;
        originalSkinShopPos = skinShopBtn.anchoredPosition;
        originalWeaponShopPos = weaponShopBtn.anchoredPosition;
    }
    private void OnEnable()
    {
        ScaleIn(Logo);
        ScaleIn(levelIcon);
        ScaleIn(playIcon);
        playIcon.GetComponent<Animator>().enabled = false;

        SlideIn(skinShopBtn, originalSkinShopPos);
        SlideIn(settingBtn, originalSettingPos);
        SlideIn(weaponShopBtn, originalWeaponShopPos);

    }
    private void ScaleIn(GameObject _gameObject)
    {
        _gameObject.transform.localScale = Vector3.zero;
        _gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack)
            .OnComplete(() => { 
                if(_gameObject==playIcon) playIcon.GetComponent<Animator>().enabled = true;
            });
    }
    private void SlideIn(RectTransform rectTransform, Vector3 originPos)
    {
        Vector3 posNew = originPos;
        Vector3 posTarget = originPos;
        posNew.x = -posNew.x;
        rectTransform.anchoredPosition = posNew;
        rectTransform.DOAnchorPos(posTarget, 0.5f).SetEase(Ease.OutBack).SetDelay(0.3f);
    }

    private void ScaleOut(GameObject _gameObject)
    {
        _gameObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
    }
    private void SlideOut(RectTransform rectTransform, Vector3 originPos)
    {
        Vector3 posTarget = originPos;
        posTarget.x = -posTarget.x;
        rectTransform.DOAnchorPos(posTarget, 0.3f).SetEase(Ease.InBack).SetDelay(0.5f);
    }
    public async void CloseHomeUITo(UIManager.SceneUIType sceneUIType)
    {
        ScaleOut(Logo);
        ScaleOut(levelIcon);
        ScaleOut(playIcon);
        playIcon.GetComponent<Animator>().enabled = false;

        SlideOut(skinShopBtn, originalSkinShopPos);
        SlideOut(settingBtn, originalSettingPos);
        SlideOut(weaponShopBtn, originalWeaponShopPos);
        await Task.Delay(800);
        UIManager.Instance.SetUIScene(sceneUIType);
    }
}
