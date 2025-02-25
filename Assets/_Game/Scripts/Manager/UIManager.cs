using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using UnityEditor;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public GameObject _eventSystem;
    [SerializeField] private Image killProgress;
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button rePlayButton;
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private Button noAdsButton;
    [SerializeField] private Button slotButton;
    [SerializeField] private Button biggerButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backHomeButton1; // skin
    [SerializeField] private Button backHomeButton2; // weapon
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
    [SerializeField] private GameObject tutorialUI;

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
        backHomeButtonSetting.onClick.AddListener(OnClickBackHomeSetting);
        backHomeButtonSlot.onClick.AddListener(OnClickBackHomeSlot);
        Observer.AddListener(constr.WINGAME, SetWinLevel);
        Observer.AddListener(constr.LOSEGAME, SetLoseLevel);
        Observer.AddListener(constr.UPDATEUI, UpdateTxtUI);
        Observer.AddListener(constr.DONELOADLEVEL, () => {
            UpdateTxtUI(); 
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
        settingPopup.GetComponent<Setting>().Hide() ;
    }
    private void OnClickWeaponButton()
    {
        HomeUI.Instance.CloseHomeUITo(SceneUIType.ShopWeapon);
    }
    private void OnClickSkinButton()
    {
        HomeUI.Instance.CloseHomeUITo(SceneUIType.ShopSkin);
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
    public void UpdateTxtUI()
    {
        DynamicData dynamicdata = DataRuntimeManager.Instance.dynamicData;
        // level winlevel, gameplay
        // monney dung chung
        // so luong enemy con lai

        monney_Txt.text = dynamicdata.GetCurrentMonney().ToString();
        level_WinGame_Txt.text = "LEVEL "+dynamicdata.GetCurrentIDLevel().ToString()+"\nCOMPLETE";
        level_Current_GP_Txt.text = dynamicdata.GetCurrentIDLevel().ToString();
        level_Next_GP_Txt.text = (dynamicdata.GetCurrentIDLevel()+1).ToString();
        countEnemy.text = GameManager.Instance.GetNumberOfEnemy().ToString()+ " PLAYER LEFT";
    }
    public void UpdateProcess()
    {
        killProgress
            .DOFillAmount((20f - GameManager.Instance.GetNumberOfEnemy()) / 20f, 0.15f)
            .SetEase(Ease.OutBack)
            .SetDelay(0.3f);
        countEnemy.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.075f).OnComplete(() =>
        {
            countEnemy.transform.DOScale(new Vector3(1f, 1f, 1f), 0.075f);
        });
        UpdateTxtUI();
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
        Observer.Noti(constr.CHANGE_CAMERA_PLAY);
        HomeUI.Instance.CloseHomeUITo(SceneUIType.GamePlay);
        UpdateProcess();
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
