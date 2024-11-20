using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class UIManager : Singleton<UIManager>
{
    public GameObject _eventSystem;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button rePlayButton;
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private Button noAdsButton;
    [SerializeField] private Button slotButton;
    [SerializeField] private Button biggerButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backHomeButton1;
    [SerializeField] private Button backHomeButton2;
    [SerializeField] private Button backHomeButtonSetting;
    [SerializeField] private Button backHomeButtonSlot;
    [SerializeField] private GameObject homeSceneUI;
    [SerializeField] private GameObject gamePlaySceneUI;
    [SerializeField] private GameObject winSceneUI;
    [SerializeField] private GameObject loseSceneUI;
    [SerializeField] private GameObject currentSceneUI;
    [SerializeField] private GameObject settingPopup;
    [SerializeField] private GameObject weaponSceneUI;
    [SerializeField] private GameObject skinSceneUI;
    [SerializeField] private GameObject slotSceneUI;

    [SerializeField] private TMP_Text monney_Txt;
    [SerializeField] private TMP_Text level_WinGame_Txt;
    [SerializeField] private TMP_Text level_Current_GP_Txt;
    [SerializeField] private TMP_Text level_Next_GP_Txt;
    [SerializeField] private TMP_Text countEnemy;

    private void Start()
    {
        currentSceneUI = homeSceneUI;
        // remove after fix
        playButton.onClick.AddListener(OnClickPlayButton);
        rePlayButton.onClick.AddListener(OnClickReplayButton);
        nextButton.onClick.AddListener(OnClickNextButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        noAdsButton.onClick.AddListener(OnClickNoAdsButton);
        slotButton.onClick.AddListener(OnClickSlotButton);
        biggerButton.onClick.AddListener(OnClickBiggerButton);
        skinButton.onClick.AddListener(OnClickSkinButton);
        weaponButton.onClick.AddListener(OnClickWeaponButton);
        backHomeButton1.onClick.AddListener(OnClickBackHomeButton);
        backHomeButton2.onClick.AddListener(OnClickBackHomeButton);
        backHomeButtonSetting.onClick.AddListener(OnClickBackHomeSetting);
        backHomeButtonSlot.onClick.AddListener(OnClickBackHomeSlot);
        Observer.AddListener(constr.WINGAME, SetWinLevel);
        Observer.AddListener(constr.LOSEGAME, SetLoseLevel);
        Observer.AddListener(constr.UPDATEUI, UpdateTxtUI);
        Observer.AddListener(constr.DONELOADLEVEL, () => {
            SetUIScene(SceneUIType.Home);
        });
        UpdateTxtUI();
    }

    private void OnClickBackHomeSlot()
    {
        slotSceneUI.SetActive(false);
    }

    private void OnClickBackHomeSetting()
    {
        settingPopup.SetActive(false);
    }

    private void OnClickBackHomeButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        SetUIScene(SceneUIType.Home);
    }
    private void OnClickWeaponButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        SetUIScene(SceneUIType.ShopWeapon);
    }
    private void OnClickSkinButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        SetUIScene(SceneUIType.ShopSkin);
    }
    private void OnClickBiggerButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Debug.Log("OnClickBiggerButton");
    }
    private void OnClickSlotButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        slotSceneUI.SetActive(true);
    }
    private void OnClickNoAdsButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Debug.Log("OnClickNoAdsButton");
    }
    private void OnClickSettingButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        settingPopup.SetActive(true);
    }
    private void UpdateTxtUI()
    {
        DynamicData dynamicdata =  DataManager.Instance.dynamicData;
        // level winlevel, gameplay
        // monney dung chung
        // so luong enemy con lai
    }
    private void OnClickReplayButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Observer.Noti(constr.RELOADLEVEL);
    }
    private void OnClickNextButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Observer.Noti(constr.NEXTLEVEL);
    }
    private void OnClickPlayButton()
    {
        //AudioManager.Instance.SetSound(AudioManager.SoundType.ButtonClick);
        Observer.Noti(constr.CHANGE_CAMERA_PLAY);
        SetUIScene(SceneUIType.GamePlay);
    }
    private async void SetWinLevel()
    {
        await Task.Delay(2500);
        SetUIScene(SceneUIType.Win);
    }
    private void SetLoseLevel()
    {
        SetUIScene(SceneUIType.Lose);
    }
    public void ChangeSceneUI(GameObject sceneUISet)
    {
        if (currentSceneUI != sceneUISet)
        {
            currentSceneUI.SetActive(false);
            currentSceneUI = sceneUISet;
            currentSceneUI.SetActive(true);
        }
    }
    public void SetUIScene(SceneUIType sceneUIType)
    {
        switch (sceneUIType)
        {
            case SceneUIType.Home:
                ChangeSceneUI(homeSceneUI);
                break;
            case SceneUIType.GamePlay:
                ChangeSceneUI(gamePlaySceneUI);
                break;
            case SceneUIType.Win:
                ChangeSceneUI(winSceneUI);
                break;
            case SceneUIType.Lose:
                ChangeSceneUI(loseSceneUI);
                break;
            case SceneUIType.ShopWeapon:
                ChangeSceneUI(weaponSceneUI);
                break;
            case SceneUIType.ShopSkin:
                ChangeSceneUI(skinSceneUI);
                break;
            case SceneUIType.Slot:
                ChangeSceneUI(slotSceneUI);
                break;
        }
    }
    public enum SceneUIType
    {
        Home,
        GamePlay,
        Win,
        Lose,
        ShopWeapon,
        ShopSkin,
        Slot
    }
    public enum PopupUIType
    {
        Setting,
    }
}
