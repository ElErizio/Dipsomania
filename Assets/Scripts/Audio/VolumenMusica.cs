using UnityEngine;
using UnityEngine.UI;

public class VolumenMusica : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    public AudioSource musica;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenMusica", 1.0f);
        musica.volume = slider.value;
        RevisarMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenMusica", sliderValue);
        musica.volume = slider.value;
        RevisarMute();
    }

    public void RevisarMute()
    {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true; 
        }
        else
        {
            imagenMute.enabled = false; 
        }
    }
}
