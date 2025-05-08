using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum OptionState
{
    None,
    Pause
}

[Serializable]
public struct Resolution
{
    public int Item1;
    public int Item2;
}

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance { get; private set; }
    private OptionState state = OptionState.None;

    [Header("참조")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject pause_Panel;

    [Space, SerializeField] private Dropdown resolution_Dropdown;
    [SerializeField] private Dropdown screenMode_Dropdown;

    [Space, SerializeField] private Slider masterVolume_Slider;
    [SerializeField] private Slider bgmVolume_Slider;
    [SerializeField] private Slider sfxVolume_Slider;

    [Space, SerializeField] private Button exit_Button;
    [SerializeField] private Button goToTitle_Button;

    [Space, Header("설정")]
    [SerializeField] private string titleSceneName;
    [SerializeField] private float minVolume = -80;
    [SerializeField] private float maxVolume = 0;
    [SerializeField] private Resolution[] resolutions;
    [SerializeField] private FullScreenMode[] screenMode;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        pause_Panel.SetActive(false);

        // 볼륨 슬라이더 최대 최소 값 설정 
        masterVolume_Slider.maxValue = maxVolume;
        masterVolume_Slider.minValue = minVolume;
        bgmVolume_Slider.maxValue = maxVolume;
        bgmVolume_Slider.minValue = minVolume;
        sfxVolume_Slider.maxValue = maxVolume;
        sfxVolume_Slider.minValue = minVolume;

        // 드롭다운에 값 등록
        List<Dropdown.OptionData> resolutionData = new List<Dropdown.OptionData>();
        foreach (Resolution value in resolutions)
        {
            resolutionData.Add(new Dropdown.OptionData(value.Item1 + " X " + value.Item2));
        }
        resolution_Dropdown.AddOptions(resolutionData);

        List<Dropdown.OptionData> screenModeData = new List<Dropdown.OptionData>();
        foreach (FullScreenMode mode in screenMode)
        {
            screenModeData.Add(new Dropdown.OptionData(mode.ToString()));
        }
        screenMode_Dropdown.AddOptions(screenModeData);

        // 볼륨 슬라이더 값 변경 이벤트 등록
        resolution_Dropdown.onValueChanged.AddListener(OnChange_Resolution);
        screenMode_Dropdown.onValueChanged.AddListener(OnChange_ScreenMode);
        masterVolume_Slider.onValueChanged.AddListener(OnChange_MasterVolume);
        bgmVolume_Slider.onValueChanged.AddListener(OnChange_BGMVolume);
        sfxVolume_Slider.onValueChanged.AddListener(OnChange_SFXVolume);

        // 버튼들에 이벤트 등록
        exit_Button.onClick.AddListener(DeactivePausePanel);
        goToTitle_Button.onClick.AddListener(GoToTitle);

        // 값 초기화
        if (PlayerPrefs.HasKey("Resolution") == false) { PlayerPrefs.SetInt("Resolution", 0); }
        if (PlayerPrefs.HasKey("ScreenMode") == false) { PlayerPrefs.SetInt("ScreenMode", 0); }
        if (PlayerPrefs.HasKey("MasterVolume") == false) { PlayerPrefs.SetFloat("MasterVolume", -20); }
        if (PlayerPrefs.HasKey("BGMVolume") == false) { PlayerPrefs.SetFloat("BGMVolume", -20); }
        if (PlayerPrefs.HasKey("SFXVolume") == false) { PlayerPrefs.SetFloat("SFXVolume", -20); }


        masterVolume_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
        bgmVolume_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        sfxVolume_Slider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == OptionState.None)
            {
                ActivePausePanel();
            }
            else if (state == OptionState.Pause)
            {
                DeactivePausePanel();
            }
        }
    }

    public void ActivePausePanel()
    {
        state = OptionState.Pause;
        Time.timeScale = 0;
        if (TitleManager.instance != null) // 타이틀 화면에 있으면
        {
            goToTitle_Button.gameObject.SetActive(false); // 타이틀 화면으로 가는 버튼 비활성화
        }
        else { goToTitle_Button.gameObject.SetActive(true); }

        // 옵션창 열면 설정 값들로 UI값 변경
        resolution_Dropdown.value = PlayerPrefs.GetInt("Resolution");
        screenMode_Dropdown.value = PlayerPrefs.GetInt("ScreenMode");
        masterVolume_Slider.value = PlayerPrefs.GetFloat("MasterVolume");
        bgmVolume_Slider.value = PlayerPrefs.GetFloat("BGMVolume");
        sfxVolume_Slider.value = PlayerPrefs.GetFloat("SFXVolume");

        // UI창 활성화
        pause_Panel.SetActive(true);
    }

    void DeactivePausePanel()
    {
        state = OptionState.None;
        Time.timeScale = 1;
        pause_Panel.SetActive(false);
    }

    void GoToTitle()
    {
        DeactivePausePanel();
        SceneManager.LoadScene(titleSceneName);
    }

    void OnChange_Resolution(int solution)
    {
        Screen.SetResolution(resolutions[solution].Item1, resolutions[solution].Item2, true);
        PlayerPrefs.SetInt("Resolution", solution);
    }

    void OnChange_ScreenMode(int mode)
    {
        Screen.fullScreenMode = screenMode[mode];
        PlayerPrefs.SetInt("ScreenMode", mode);
    }

    void OnChange_MasterVolume(float _volume)
    {
        float volume = Mathf.Clamp(_volume, minVolume, maxVolume);
        mixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    void OnChange_BGMVolume(float _volume)
    {
        float volume = Mathf.Clamp(_volume, minVolume, maxVolume);
        mixer.SetFloat("BGM", volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    void OnChange_SFXVolume(float _volume)
    {
        float volume = Mathf.Clamp(_volume, minVolume, maxVolume);
        mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
