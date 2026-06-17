using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [Header("Audio")]
    public Slider musicSlider;

    void Start()
    {
        // Ambil volume yang tersimpan
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        // Terapkan volume saat game dimulai
        SetMusicVolume(musicSlider.value);

        // Event saat slider digeser
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMusicVolume(float volume)
    {
        // Ubah volume musik
        AudioManager.Instance.SetMusicVolume(volume);

        // Simpan volume
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
}