using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    private AudioSource bgmAudio;

    private const string MusicVolumeKey = "MusicVolume";

    private void Start()
    {
        bgmAudio = BGMPlayer.Instance.GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        musicSlider.value = savedVolume;
        ApplyVolume(savedVolume);

        musicSlider.onValueChanged.AddListener(ApplyVolume);
    }

    private void ApplyVolume(float value)
    {
        if (bgmAudio != null)
            bgmAudio.volume = value;

        PlayerPrefs.SetFloat(MusicVolumeKey, value);
    }
}
