using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Shop : MonoBehaviour
{
    [SerializeField] protected GameObject cameraui;
    [SerializeField] protected GameObject[] shopItemModels;
    [SerializeField] protected ShopUnit shopUnit;
    [SerializeField] protected Transform raceContentTf;
    [SerializeField] protected Transform commonContentTf;
    [SerializeField] protected Transform epicContentTf;
    [SerializeField] protected RectTransform rareContentRectTf;
    [SerializeField] protected RectTransform commonContentRectTf;
    [SerializeField] protected RectTransform epicContentRectTf;
    [SerializeField] protected Button buttonRare;
    [SerializeField] protected Button buttonCommon;
    [SerializeField] protected Button buttonEpic;
    [SerializeField] protected Button buttonEquiep_Equip;
    [SerializeField] protected Button buttonUnlockRandom;
    [SerializeField] protected Image imageMonneyOfRandom;
    [SerializeField] protected GameObject textEquipt;
    [SerializeField] protected GameObject textEquiped;
    [SerializeField] protected TextMeshProUGUI textPrice;

    protected int currentIDSelect = -1;
    protected int currentIDEquip;
    protected int countRaRe = 0;
    protected int countEpic = 0;
    protected int countCommon = 0;
    protected int priceRandomCommon = 50;
    protected int priceRandomRare = 200;
    protected int priceRandomEpic = 1000;
    protected int currentRandomPrice;
    protected float updateTimer = 0;
    protected float updateInterval = 0.2f;
    protected GameObject currentTab;
    protected List<ShopItem> shopItems= new List<ShopItem>();
    protected List<ShopUnit> itemShopUnits = new List<ShopUnit>();
    protected List<int> currentID_ItemLocked = new List<int>();

    protected List<ShopUnit> itemCommons = new List<ShopUnit>();
    protected List<ShopUnit> itemEpics = new List<ShopUnit>();
    protected List<ShopUnit> itemRares = new List<ShopUnit>();
    protected List<ShopUnit> listItemCurrent = new List<ShopUnit>();
    protected DynamicData dynamicData;

    protected void Start()
    {
        buttonUnlockRandom.onClick.AddListener(OnclickButtonUnlockRandom);
        buttonEquiep_Equip.onClick.AddListener(OnclickButtonEquip);
        buttonCommon.onClick.AddListener(OnclickButtonCommon);
        buttonEpic.onClick.AddListener(OnclickButtonEpic);
        buttonRare.onClick.AddListener(OnclickButtonRare);
        Init();
    }
    protected void OnEnable()
    {
        if (currentIDSelect != -1)
        {
            ChangeSelectByID(currentIDSelect);
            UpdateButtonRandom();
        }
        cameraui.SetActive(true);
    }
    protected void OnDisable()
    {
        if(cameraui != null) cameraui.SetActive(false);
    }
    protected void Update()
    {
        if (DynamicActionOnUpdate != null) DynamicActionOnUpdate.Invoke();
    }
    protected virtual void Init()
    {
        dynamicData = DataRuntimeManager.Instance.dynamicData;
        Debug.Log("id :"+dynamicData.GetIdWeapon());
        ChangeTab(shopItems[currentIDSelect].type);
        currentIDEquip = currentIDSelect;
        for (int i = 0; i < shopItems.Count; i++)
        {
            ShopItem newShopItem = shopItems[i];
            switch (newShopItem.type)
            {
                case ShopType.RARE:
                    AddItemShop(rareContentRectTf, raceContentTf, ref countRaRe, i, itemRares);
                    break;
                case ShopType.EPIC:
                    AddItemShop(epicContentRectTf, epicContentTf, ref countEpic, i, itemEpics);
                    break;
                case ShopType.COMMON:
                    AddItemShop(commonContentRectTf, commonContentTf, ref countCommon, i, itemCommons);
                    break;
            }
        }
        ChangeSelectByID(currentIDSelect);
        UpdateButtonRandom();
    }
    protected void OnclickButtonUnlockRandom()
    {
        if (!dynamicData.HasEnoughMonney(currentRandomPrice)) return;

        UIManager.Instance._eventSystem.SetActive(false);
        dynamicData.SubtractMonney(currentRandomPrice);
        countRandom = 0;
        updateTimer = 0;
        updateInterval = 0.1f;
        id = -1;
        DynamicActionOnUpdate += Random;
    }
    protected virtual void OnclickButtonEquip()
    {
        itemShopUnits[currentIDEquip].SetEquiped(false);
        currentIDEquip = currentIDSelect;
        itemShopUnits[currentIDEquip].SetEquiped(true);
        TurnEquiped(true);
    }
    protected void OnclickButtonRare()
    {
        ChangeTab(ShopType.RARE);
    }
    protected void OnclickButtonEpic()
    {
        ChangeTab(ShopType.EPIC);
    }
    protected void OnclickButtonCommon()
    {
        ChangeTab(ShopType.COMMON);
    }
    protected void UpdateButtonRandom()
    {
        currentID_ItemLocked.Clear();
        for (int i = 0; i < listItemCurrent.Count; i++)
        {
            if (listItemCurrent[i].isOpen == false)
            {
                currentID_ItemLocked.Add(i);
            }
        }
        if (currentID_ItemLocked.Count == 0)
        {
            imageMonneyOfRandom.color = new Color(1, 1, 1, 0.6f);
            buttonUnlockRandom.interactable = false;
        }
        else
        {
            imageMonneyOfRandom.color = Color.white;
            buttonUnlockRandom.interactable = true;
        }
    }
    protected void ChangeTab(ShopType type)
    {
        if (currentTab != null) currentTab.SetActive(false);
        switch (type)
        {
            case ShopType.COMMON:
                currentTab = commonContentTf.gameObject;
                listItemCurrent = itemCommons;
                currentRandomPrice = priceRandomCommon;
                textPrice.text = "50";
                break;
            case ShopType.EPIC:
                currentTab = epicContentTf.gameObject;
                listItemCurrent = itemEpics;
                currentRandomPrice = priceRandomEpic;
                textPrice.text = "1000";
                break;
            case ShopType.RARE:
                currentTab = rareContentRectTf.gameObject;
                listItemCurrent = itemRares;
                currentRandomPrice = priceRandomRare;
                textPrice.text = "200";
                break;
        }
        currentTab.SetActive(true);
        UpdateButtonRandom();
    }
    public void AddItemShop(RectTransform contentRectTransform, Transform contenTransform, ref int count, int i, List<ShopUnit> shopItemsOfType)
    {
        count++;
        if (count % 3 == 1 && count > 1)
        {
            contentRectTransform.sizeDelta += new Vector2(0, 54 + 200f);
        }
        ShopUnit newShopItemUnit = Instantiate(shopUnit, contenTransform);
        AddItemShopWithType(i,ref newShopItemUnit);
        itemShopUnits.Add(newShopItemUnit);
        shopItemsOfType.Add(newShopItemUnit);
    }
    public virtual void AddItemShopWithType(int i, ref ShopUnit newShopItemUnit)
    {
    }
    public virtual void ChangeSelectByID(int id)
    {
        if(shopItemModels[currentIDSelect]!= null)
        {
            shopItemModels[currentIDSelect].SetActive(false);
        }
        itemShopUnits[currentIDSelect].SetDeSelect();

        currentIDSelect = id;

        shopItemModels[currentIDSelect].SetActive(true);
        itemShopUnits[currentIDSelect].SetSelect();

        TurnEquiped(currentIDSelect == currentIDEquip);

        // sau base.changeselectbyid cua skin them if skinbase
    }
    public void TurnEquiped(bool isEquiped)
    {
        buttonEquiep_Equip.interactable = !isEquiped;
        textEquipt.SetActive(!isEquiped);
        textEquiped.SetActive(isEquiped);
    }
    #region value for Random()
    Action DynamicActionOnUpdate;
    int id;
    int countRandom;
    int countMaxRandom = 15;
    int idterm;
    # endregion 
    public void Random()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateInterval += 0.0065f * countRandom;
            if (id != -1) listItemCurrent[currentID_ItemLocked[id]].UnsetTermSelect();

            do idterm = UnityEngine.Random.Range(0, currentID_ItemLocked.Count);
            while (idterm == id);

            id = idterm;
            if (countRandom == countMaxRandom || currentID_ItemLocked.Count == 1)
            {
                UIManager.Instance._eventSystem.SetActive(true);
                ShopUnit newUnit = listItemCurrent[currentID_ItemLocked[id]];
                newUnit.SetOpen();

                ChangeSelectByID(newUnit.GetID());
                SetOpen();
                UpdateButtonRandom();
                DynamicActionOnUpdate -= Random;
                return;
            }
            listItemCurrent[currentID_ItemLocked[id]].SetTermSelect();
            countRandom++;
            updateTimer = 0;
        }
    }

    protected virtual void SetOpen()
    {
    }
}


