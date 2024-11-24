using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopUnit : MonoBehaviour
{
    ShopItem shopItem;
    [SerializeField] private GameObject equipIcon;
    [SerializeField] private GameObject termSelectIcon;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;
    public bool isOpen;
    public bool isEquiped;
    Shop myShop;
    int id;

    public void Init(ShopItem _shopItem, bool status, int _id, Shop _myshop, bool _isEquiped = false)
    {
        isEquiped = _isEquiped;
        id = _id;
        myShop = _myshop;
        shopItem = _shopItem;
        if (status)
        {
            iconImage.sprite = shopItem.spriteOpen;
            isOpen = true;
        }
        else
        {
            iconImage.sprite = shopItem.spriteLock;
            isOpen = false;
        }
        button.onClick.AddListener(() =>
        {
            if (isOpen) myShop.ChangeSelectByID(_id);
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
        iconImage.sprite = shopItem.spriteOpen;
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
