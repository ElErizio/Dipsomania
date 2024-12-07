using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public GameObject musicSource;
    public GameObject musicSourceMenu;
    public GameObject soundEffectsSource;

    [Header("UI Sliders")]
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider soundEffectsSlider;


    private float volumeMusic;
    private float volumeSFX;
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundEffectsVolumeKey = "SoundEffectsVolume";

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
        Debug.Log(mode);

        if (GameObject.FindWithTag("SliderMusica"))
        {
            Debug.Log("Chinga tu madre un Slider de musica OMG");
        }
        else
        {
            return;
        }

        musicSlider = GameObject.FindGameObjectWithTag("SliderMusica").GetComponent<Slider>();
       
        // Attach listeners to the sliders if they exist
        if (scene.name == "MainMenu")
        {
            volumeMusic = musicSlider.value;
            AudioSource musicMenu = Instantiate(musicSourceMenu).GetComponent<AudioSource>();
            musicMenu.volume = volumeMusic;
            Debug.Log("Se cargaron los sliders de la musica");
        }

        if (scene.name == "Rafa Test")
        {
            volumeMusic = musicSlider.value;
            AudioSource music = Instantiate(musicSource).GetComponent<AudioSource>();
            music.volume = volumeMusic;
            Debug.Log("Se cargaron los sliders de la musica");
        }

        soundEffectsSlider = GameObject.FindGameObjectWithTag("SliderSFX").GetComponent<Slider>();
        AudioSource sfx = Instantiate(soundEffectsSource).GetComponent<AudioSource>();
        volumeSFX = soundEffectsSlider.value;
        sfx.volume = soundEffectsSlider.value;
        Debug.Log("Se cargaron los sliders de los efectos");

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /* public void SetMusicVolume(float volume)
    {
        musicSource.GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
        

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsSource.GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat(SoundEffectsVolumeKey, volume);
        PlayerPrefs.Save();
    }*/
}
