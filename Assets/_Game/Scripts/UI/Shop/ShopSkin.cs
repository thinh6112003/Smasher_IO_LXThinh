using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public class ShopSkin : MonoBehaviour
{
    [SerializeField] private GameObject cameraui;
    [SerializeField] private GameObject[] skins;
    [SerializeField] private GameObject skinBase;
    [SerializeField] private SkinShopSO skinShopSO;
    [SerializeField] private SkinShopUnit skinShopUnit;
    [SerializeField] private Transform raceContentTf;
    [SerializeField] private Transform commonContentTf;
    [SerializeField] private Transform epicContentTf;
    [SerializeField] private RectTransform rareContentRectTf;
    [SerializeField] private RectTransform commonContentRectTf;
    [SerializeField] private RectTransform epicContentRectTf;
    [SerializeField] private Button buttonRare;
    [SerializeField] private Button buttonCommon;
    [SerializeField] private Button buttonEpic;
    [SerializeField] private Button buttonEquiep_Equip;
    [SerializeField] private Button buttonUnlockRandom;
    [SerializeField] private Image imageMonneyOfRandom;
    [SerializeField] private GameObject textEquipt;
    [SerializeField] private GameObject textEquiped;
    [SerializeField] private TextMeshProUGUI textPrice;

    int currentIDSkinSelect=-1;
    int currentIDSkinEquip;
    int countRaRe = 0;
    int countEpic = 0;
    int countCommon = 0;
    int priceRandomCommon = 50;
    int priceRandomRare = 200;
    int priceRandomEpic = 1000;
    int currentRandomPrice;
    private float updateTimer=0;
    private float updateInterval = 0.2f;
    private GameObject currentTab;
    private List<SkinShopUnit> skinShopUnits = new List<SkinShopUnit>();
    private List<int> currentIDSkinLocked = new List<int>();

    private List<SkinShopUnit> skinCommons = new List<SkinShopUnit>();
    private List<SkinShopUnit> skinEpics = new List<SkinShopUnit>();
    private List<SkinShopUnit> skinRares = new List<SkinShopUnit>();

    private List<SkinShopUnit> listSkinsCurrent = new List<SkinShopUnit>();
    DynamicData dynamicData;

    private void Awake()
    {
        //buttonEquiep_Equip.onClick.AddListener(OnclickButtonEquip);
        buttonUnlockRandom.onClick.AddListener(OnclickButtonUnlockRandom);
        buttonEquiep_Equip.onClick.AddListener(OnclickButtonEquip);
        buttonCommon.onClick.AddListener(OnclickButtonCommon);
        buttonEpic.onClick.AddListener(OnclickButtonEpic);
        buttonRare.onClick.AddListener(OnclickButtonRare);
    }
    private void OnEnable()
    {
        if (currentIDSkinSelect!= -1)
        {
            Debug.Log("zxxx");
            ChangeSkinSelectByID(currentIDSkinSelect);
            UpdateButtonRandom();
        }
        cameraui.SetActive(true);
    }
    private void OnDisable()
    {
        cameraui.SetActive(false);
    }
    private void Start()
    {
        dynamicData = DataManager.Instance.dynamicData;
        currentIDSkinSelect = dynamicData.GetIdSkin();
        currentIDSkinEquip = currentIDSkinSelect;
        Debug.Log(dynamicData.GetIdSkin() + "   in start");
        ChangeTab(skinShopSO.skinShopItems[currentIDSkinSelect].type);
        for (int i = 0; i < skinShopSO.skinShopItems.Count; i++)
        {
            SkinShopItem newSkinShopItem = skinShopSO.skinShopItems[i];
            switch (newSkinShopItem.type)
            {
                case Type.RARE:
                    AddItemShop(rareContentRectTf, raceContentTf, ref countRaRe, i, skinRares);
                    break;
                case Type.EPIC:
                    AddItemShop(epicContentRectTf, epicContentTf, ref countEpic, i, skinEpics);
                    break;
                case Type.COMMON:
                    AddItemShop(commonContentRectTf, commonContentTf, ref countCommon, i, skinCommons);
                    break;
            }
        }
        ChangeSkinSelectByID(currentIDSkinSelect);
        UpdateButtonRandom();
    }
    private void Update()
    {
        if(DynamicActionOnUpdate != null) DynamicActionOnUpdate.Invoke();
    }
    private void OnclickButtonUnlockRandom()
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
    private void OnclickButtonEquip()
    {
        dynamicData.SetIdSkin(currentIDSkinSelect);
        skinShopUnits[currentIDSkinEquip].SetEquiped(false);
        currentIDSkinEquip = currentIDSkinSelect;
        skinShopUnits[currentIDSkinEquip].SetEquiped(true);
        TurnEquiped(true);
        Observer.Noti(constr.CHANGESKIN);
    }
    private void OnclickButtonRare()
    {
        ChangeTab(Type.RARE);
    }
    private void OnclickButtonEpic()
    {
        ChangeTab(Type.EPIC);
    }
    private void OnclickButtonCommon()
    {
        ChangeTab(Type.COMMON);
    }
    private void UpdateButtonRandom()
    {
        currentIDSkinLocked.Clear();
        for (int i = 0; i < listSkinsCurrent.Count; i++)
        {
            if (listSkinsCurrent[i].isOpen == false)
            {
                currentIDSkinLocked.Add(i);
            }
        }
        if (currentIDSkinLocked.Count == 0)
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
    private void ChangeTab(Type type)
    {
        if (currentTab != null) currentTab.SetActive(false);
        switch (type)
        {
            case Type.COMMON:
                currentTab = commonContentTf.gameObject;
                listSkinsCurrent = skinCommons;
                currentRandomPrice = priceRandomCommon;
                break;
            case Type.EPIC:
                currentTab = epicContentTf.gameObject;
                listSkinsCurrent = skinEpics;
                currentRandomPrice = priceRandomEpic;
                break;
            case Type.RARE:
                currentTab = rareContentRectTf.gameObject;
                listSkinsCurrent = skinRares;
                currentRandomPrice = priceRandomRare;
                break;
        }
        currentTab.SetActive(true);
        UpdateButtonRandom();
    }
    public void AddItemShop(RectTransform contentRectTransform,Transform contenTransform, ref int count,int i, List<SkinShopUnit> listSkinShopType)
    {
        count++;
        if(count%3==1&& count> 1)
        {
            contentRectTransform.sizeDelta += new Vector2(0, 54+200f);
        }
        SkinShopUnit newSkinSUnit = Instantiate(skinShopUnit, contenTransform);
        newSkinSUnit.Init(skinShopSO.skinShopItems[i], DataManager.Instance.shopData.GetSkinStatus(i), i, this,currentIDSkinEquip == i );
        skinShopUnits.Add(newSkinSUnit);
        listSkinShopType.Add(newSkinSUnit);
    }
    public void ChangeSkinSelectByID(int id)
    {
        Debug.Log(id);
        Debug.Log(currentIDSkinSelect + "   in change");
        skins[currentIDSkinSelect].SetActive(false);
        skinShopUnits[currentIDSkinSelect].SetDeSelect();

        currentIDSkinSelect = id;

        if (skinShopSO.skinShopItems[id].style== Style.HEROA) 
            skinBase.SetActive(true);
        else 
            skinBase.SetActive(false);
        skins[currentIDSkinSelect].SetActive(true);
        skinShopUnits[currentIDSkinSelect].SetSelect();

        TurnEquiped(currentIDSkinSelect == currentIDSkinEquip);
    }
    public void TurnEquiped(bool isEquiped)
    {
        buttonEquiep_Equip.interactable = !isEquiped;
        textEquipt.SetActive(!isEquiped);
        textEquiped.SetActive(isEquiped);
    }

    Action DynamicActionOnUpdate;
    int id;
    int countRandom;
    int countMaxRandom = 15;
    int idterm;
    public void Random()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer >= updateInterval)
        {
            updateInterval += 0.0065f*countRandom;
            if (id!=-1)listSkinsCurrent[currentIDSkinLocked[id]].UnsetTermSelect();

            do idterm = UnityEngine.Random.Range(0, currentIDSkinLocked.Count);
            while (idterm == id);
            id = idterm;
            if(countRandom == countMaxRandom|| currentIDSkinLocked.Count ==1)
            {
                UIManager.Instance._eventSystem.SetActive(true);
                SkinShopUnit newUnit =  listSkinsCurrent[currentIDSkinLocked[id]];
                newUnit.SetOpen();
                ChangeSkinSelectByID(newUnit.GetID());
                UpdateButtonRandom();
                DynamicActionOnUpdate -= Random;
                return;
            }
            listSkinsCurrent[currentIDSkinLocked[id]].SetTermSelect();
            countRandom++;
            updateTimer = 0;
        }
    }
}
