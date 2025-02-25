using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    [SerializeField] private Button vibarationBtn;
    [SerializeField] private Button soundBtn;
    [SerializeField] private Button closeBtn;
    [SerializeField] private GameObject vibrationOn;
    [SerializeField] private GameObject vibrationOff;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private Transform settingPopup;
    [SerializeField] private bool vibrationStatus = true;
    [SerializeField] private bool soundStatus = true;
    
    private void Start()
    {
        soundBtn.onClick.AddListener(OnClickSoundBtn);
        vibarationBtn.onClick.AddListener(OnClickVibrationBtn);
        SetButtonsOfSetting();
    }
    private void OnEnable()
    {
        settingPopup.localScale = Vector3.zero;
        settingPopup.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    private void OnDisable()
    {
        Image img = gameObject.GetComponent<Image>();
        img.color = Color.clear;
        img.DOFade(0.4f, 0.5f);
        closeBtn.interactable = true;
    }
    private void SetButtonsOfSetting()
    {
        // vieet di
        //vibrationStatus = DataRuntimeManager.Instance.dynamicData.GetVibrationStatus();
        //soundStatus = DataRuntimeManager.Instance.dynamicData.GetSoundStatus();
        vibrationOn.SetActive(vibrationStatus);
        vibrationOff.SetActive(!vibrationStatus);
        soundOn.SetActive(soundStatus);
        soundOff.SetActive(!soundStatus);
    }

    private void OnClickCloseBtn()
    {
        this.gameObject.SetActive(false);
    }

    private void OnClickVibrationBtn()
    {
        vibrationStatus = !vibrationStatus;
        vibrationOn.SetActive(vibrationStatus);
        vibrationOff.SetActive(!vibrationStatus);
        //DataRuntimeManager.Instance.dynamicData.SetVibrationStatus(vibrationStatus);
    }

    private void OnClickSoundBtn()
    {
        soundStatus = !soundStatus;
        soundOn.SetActive(soundStatus);
        soundOff.SetActive(!soundStatus);
        //DataRuntimeManager.Instance.dynamicData.SetActiveSoundStatus(soundStatus);

    }
    public void Hide()
    {
        UIManager.Instance._eventSystem.SetActive(false);
        closeBtn.interactable = false;
        gameObject.GetComponent<Image>().DOFade(0f, 0.5f);
        settingPopup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>{
            gameObject.SetActive(false);
            UIManager.Instance._eventSystem.SetActive(true);
        });
    }
}