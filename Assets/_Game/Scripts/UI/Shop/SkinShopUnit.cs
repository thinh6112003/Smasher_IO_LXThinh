using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopUnit : MonoBehaviour
{
    SkinShopItem skinShopItem;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private GameObject termSelectIcon;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;
    public bool isOpen;
    public bool isEquiped;
    ShopSkin myShopSkin;
    int id;
    public void Init(SkinShopItem _shopItem, bool status, int _id, ShopSkin _shopSkin, bool _isEquiped= false)
    {
        isEquiped = _isEquiped;
        id = _id;
        myShopSkin = _shopSkin;
        skinShopItem = _shopItem;
        if (status)
        {
            iconImage.sprite = skinShopItem.spriteOpen;
            isOpen = true;
        }
        else
        {
            iconImage.sprite = skinShopItem.spriteLock;
            isOpen = false;
        }
        button.onClick.AddListener(() =>
        {
            if(isOpen) myShopSkin.ChangeSkinSelectByID(_id);
        });
    }
    public void SetEquiped(bool _isEquiped)
    {
        isEquiped = _isEquiped;
    }
    public void SetTermSelect()
    {
        ScaleEffect(1.08f, 0.075f);
        termSelectIcon.SetActive(true);
    }
    public void UnsetTermSelect()
    {
        termSelectIcon.SetActive(false);
    }
    public void SetSelect()
    {
        equipIcon.SetActive(true);
        ScaleEffect(1.35f, 0.15f);
    }
    public void SetDeSelect()
    {
        equipIcon.SetActive(false);
    }
    public void SetOpen()
    {
        isOpen = true;
        DataManager.Instance.shopData.SetOpenSkin(id);
        iconImage.sprite = skinShopItem.spriteOpen;
        ScaleEffect(1.6f, 0.25f);
    }
    public void ScaleEffect(float scale, float time)
    {
        transform.DOScale(Vector3.one * scale, time)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                transform.DOScale(Vector3.one, time);
            });
    }
    public int GetID()
    {
        return id;
    }
}
