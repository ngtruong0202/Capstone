using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private const string PlayerPrefsKey = "BackgroundMusicEnabled";
    private const string PlayerPrefsKeyVolum = "BackgroundMusicEnabled";

    public TextMeshProUGUI musicTxt;

    public AudioSource audioBackgroundMusic;
    private bool isPlayingBGSound = true;

    public Slider sliderBackgroundMusic;
    public Button onOffMusic;
    public GameObject checkSoundIMG;

    private void Start()
    {
        sliderBackgroundMusic.onValueChanged.AddListener(SetVolumeBackgroundMusic);
        onOffMusic.onClick.AddListener(TurnOnOffBGSoundButton);

        isPlayingBGSound = PlayerPrefs.GetInt(PlayerPrefsKey, 1) == 1;
        audioBackgroundMusic.volume = PlayerPrefs.GetFloat(PlayerPrefsKeyVolum, 1);

        if (isPlayingBGSound)
        {
            audioBackgroundMusic.Play();
        }
        else
        {
            audioBackgroundMusic.Stop();
        }
        sliderBackgroundMusic.value = audioBackgroundMusic.volume;
        //musicTxt.text = audioBackgroundMusic.volume.ToString();
        checkSoundIMG.SetActive(isPlayingBGSound);
        var index = audioBackgroundMusic.volume * 100;
        musicTxt.text = index.ToString("0");
    }

    public void TurnOnOffBGSoundButton()
    {
        isPlayingBGSound = !isPlayingBGSound;
        PlayerPrefs.SetInt(PlayerPrefsKey, isPlayingBGSound ? 1 : 0);

        if (isPlayingBGSound)
        {
            audioBackgroundMusic.Play();
        }
        else
        {
            audioBackgroundMusic.Pause();
        }
        checkSoundIMG.SetActive(isPlayingBGSound);

    }

    public void TurnOnOffBGSound()
    {
        isPlayingBGSound = !isPlayingBGSound;
        PlayerPrefs.SetInt(PlayerPrefsKey, isPlayingBGSound ? 1 : 0);

        if (isPlayingBGSound)
        {
            audioBackgroundMusic.Play();
        }
        else
        {
            audioBackgroundMusic.Pause();
        }
    }
    public void SetVolumeBackgroundMusic(float volume)
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeyVolum, volume);
        audioBackgroundMusic.volume = volume;
        var index = audioBackgroundMusic.volume * 100;
        musicTxt.text = index.ToString("0");

    }


}
