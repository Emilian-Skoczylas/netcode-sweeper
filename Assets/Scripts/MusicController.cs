using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggleMute;

    private readonly string _volumeKey = "SETTINGS_VOLUME";
    private readonly string _muteKey = "SETTINGS_MUSIC_MUTE";

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(_muteKey))
        {
            bool mute = PlayerPrefs.GetInt(_muteKey) == 1 ? true : false;
            _audioSource.mute = mute;

            if (_toggleMute != null)
                _toggleMute.isOn = !mute;
        }

        if (PlayerPrefs.HasKey(_volumeKey))
        {
            float value = PlayerPrefs.GetFloat(_volumeKey);
            _audioSource.volume = value;

            if (_slider != null)
                _slider.value = value;
        }
        else
            _audioSource.volume = 0.5f;
    }

    public void MuteUnMuteMusic(bool value)
    {
        if (_audioSource != null)
        {
            _audioSource.mute = !value;
            PlayerPrefs.SetInt(_muteKey, !value ? 1 : 0);
        }
    }

    public void ChangeVolume(float value)
    {
        if (_audioSource != null)
        {
            _audioSource.volume = value;
            PlayerPrefs.SetFloat(_volumeKey, value);
        }
    }
}
