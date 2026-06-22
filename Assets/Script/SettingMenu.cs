using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    [Header("Audio")]
    public Slider musicSlider;
    public Toggle musicToggle;
    public TMP_Text musicStatusText;

    void Start()
    {
        // Volume
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        SetMusicVolume(musicSlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);

        // Toggle
        bool musicEnabled =
            PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        musicToggle.isOn = musicEnabled;

        AudioManager.Instance.ToggleMusic(musicEnabled);

        musicStatusText.text =
            musicEnabled ? "Music ON" : "Music OFF";

        musicToggle.onValueChanged.AddListener(ToggleMusic);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void ToggleMusic(bool isOn)
    {
        AudioManager.Instance.ToggleMusic(isOn);

        musicStatusText.text =
            isOn ? "Music ON" : "Music OFF";

        PlayerPrefs.SetInt(
            "MusicEnabled",
            isOn ? 1 : 0
        );

        PlayerPrefs.Save();
    }
}