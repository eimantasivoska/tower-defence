using UnityEngine.UI;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    #region Singleton
    public static AudioManager Instance { set; get; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Volume = 1;
        InitializeSounds();
        LoadVolumeState();
    }
    #endregion


    public float Volume { set; get; }

    #region UI elements
    [SerializeField]
    Slider slider = null;
    [SerializeField]
    Sound[] Sounds = new Sound[0];
    #endregion


    void Start()
    {
        float delay = Sounds[0].Clip.length - 0.07f; // For playing the music loop use this delay
    }

    void PlaySound(string name, float delay = 0)
    {
        AudioSource s = Array.Find(Sounds, sound => sound.Name == name).Source;
        s.PlayDelayed(delay);
    }

    void InitializeSounds()
    {
        foreach(Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = Volume;
            s.Source.loop = s.Loop;
            s.Source.volume = s.Volume;
        }
    }

    public void ChangeVolume(Slider slider)
    {
        Volume = slider.value;
        ChangeVolume();
    }

    void ChangeVolume()
    {
        foreach (Sound s in Sounds)
            s.Source.volume = Volume;
    }

    public void SaveVolumeState()
    {
        if (slider != null)
            PlayerPrefs.SetFloat("volume", slider.value);
    }

    void LoadVolumeState()
    {
        slider.value = PlayerPrefs.GetFloat("volume", 0.3f);
    }
}
