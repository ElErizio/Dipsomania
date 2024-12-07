using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("UI Sliders")] 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;

    [Header("Audio Sources")]
    [SerializeField]private AudioSource currentMusicSource;
    [SerializeField]private AudioSource currentSoundEffectsSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evitar múltiples instancias
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        
        if (currentMusicSource == null)
        {
            currentMusicSource = GameObject.Find("EmisorMusica").GetComponent<AudioSource>();
        }
        
        if (currentSoundEffectsSource == null)
        {
            currentSoundEffectsSource = GameObject.Find("EmisorSFX").GetComponent<AudioSource>();
        }
        
        musicSlider = Resources.FindObjectsOfTypeAll<Slider>().FirstOrDefault(s => s.CompareTag("SliderMusica"));
        soundEffectsSlider = Resources.FindObjectsOfTypeAll<Slider>().FirstOrDefault(s => s.CompareTag("SliderSFX"));

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundEffectsSlider.onValueChanged.AddListener(SetSoundEffectsVolume);
        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = savedMusicVolume;
            SetMusicVolume(savedMusicVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            soundEffectsSlider.value = savedSFXVolume;
            SetSoundEffectsVolume(savedSFXVolume);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetMusicVolume(float volume)
    {
        currentMusicSource.GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); 
    }

    public void SetSoundEffectsVolume(float volume)
    {
        currentSoundEffectsSource.GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); 
    }
}
