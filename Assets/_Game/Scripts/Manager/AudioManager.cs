using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("--- Audio Source ----------")]
    public AudioSource SoundSource;

    [Header("--- Audio Clip -----")]
    public AudioClip buttonClick;
    public AudioClip chet;
    public AudioClip buaDap;
    public AudioClip riuChem;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySFX(AudioClip clip)
    {
        SoundSource.pitch = 1;
        SoundSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float pitch)
    {
        SoundSource.pitch = pitch;
        SoundSource.PlayOneShot(clip);
    }
    public void PlayButtonClickSound()
    {
        SoundSource.PlayOneShot(buttonClick);
    }
    public void activeSound()
    {
        SoundSource.enabled = !SoundSource.enabled;
    }
    public void SetSound(SoundType soundType) {
        if (!DataRuntimeManager.Instance.dynamicData.GetSoundStatus()) return;
        switch (soundType) { 
            case SoundType.ButtonClick:
                PlayButtonClickSound();
                break;
            case SoundType.Chet:
                PlaySFX(chet);
                break;
            case SoundType.BuaDap:
                PlaySFX(buaDap);
                break;
            case SoundType.RiuChem:
                PlaySFX(riuChem);
                break;
        }
    }
    public void SetSound(SoundType soundType, float pitch)
    {
        if (!DataRuntimeManager.Instance.dynamicData.GetSoundStatus()) return;
        switch (soundType)
        {
            case SoundType.ButtonClick:
                PlayButtonClickSound();
                break;
            case SoundType.Chet:
                PlaySFX(chet, pitch);
                break;
            case SoundType.BuaDap:
                PlaySFX(buaDap, pitch);
                break;
            case SoundType.RiuChem:
                PlaySFX(riuChem, pitch);
                break;
        }
    }
    public enum SoundType { ButtonClick, Chet, BuaDap, RiuChem};
}