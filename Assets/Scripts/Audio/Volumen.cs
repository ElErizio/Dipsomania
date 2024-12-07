using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;
    
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume =  slider.value;
        RevisarMute();
    }

    public void ChangeSlider(float valor)
    { 
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    public void RevisarMute()
    {
        if (sliderValue == 0)
        {

        }
        else
        {
        
        }
    }
}
