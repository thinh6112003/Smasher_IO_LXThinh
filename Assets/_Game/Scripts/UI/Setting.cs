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
    [SerializeField] private bool vibrationStatus = true;
    [SerializeField] private bool soundStatus = true;
    private void Start()
    {
        soundBtn.onClick.AddListener(OnClickSoundBtn);
        vibarationBtn.onClick.AddListener(OnClickVibrationBtn);
        closeBtn.onClick.AddListener(OnClickCloseBtn);
        SetButtonsOfSetting();
    }

    private void SetButtonsOfSetting()
    {
        // vieet di
        //vibrationStatus = DataManager.Instance.dynamicData.GetVibrationStatus();
        //soundStatus = DataManager.Instance.dynamicData.GetSoundStatus();
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
        //DataManager.Instance.dynamicData.SetVibrationStatus(vibrationStatus);
    }

    private void OnClickSoundBtn()
    {
        soundStatus = !soundStatus;
        soundOn.SetActive(soundStatus);
        soundOff.SetActive(!soundStatus);
        //DataManager.Instance.dynamicData.SetActiveSoundStatus(soundStatus);
    }
}