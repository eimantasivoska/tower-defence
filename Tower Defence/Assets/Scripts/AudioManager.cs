using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
        InitializeSounds();
        LoadVolumeState();
    }
    #endregion


    public float Volume { set; get; }

    #region UI elements
    [SerializeField]
    Slider slider = null;
    [SerializeField]
    Sound[] Sounds = new Sound[1];
    #endregion

    Button optionsButton;
    Button optionsBackButton;



    void Start()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        PlayMusic();
    }

    /// <summary>
    /// Starts playing a sound clip
    /// </summary>
    /// <param name="name">Name of the sound clip</param>
    /// <param name="delay">Delay (if needed)</param>
    public void PlaySound(string name, float delay = 0)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if(s == null)
        {
            Debug.LogError($"Sound clip with the name \"{name}\" was not found!");
            return;
        }
        s.Source.PlayDelayed(delay);
    }

    /// <summary>
    /// Stops the sound if its playing
    /// </summary>
    /// <param name="name">NAme of the sound</param>
    public void StopSound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogError($"Sound clip with the name \"{name}\" was not found!");
            return;
        }
        if(s.Source.isPlaying)
            s.Source.Stop();
    }

    void InitializeSounds()
    {
        foreach(Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = Volume;
            s.Source.loop = s.Loop;
            s.Source.volume = s.MaxVolume;
            s.Source.pitch = s.Pitch;
            s.Source.playOnAwake = false;
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
            s.Source.volume = s.MaxVolume * Volume;
    }

    public void SaveVolumeState()
    {
        if (slider != null)
            PlayerPrefs.SetFloat("volume", slider.value);
    }

    public void LoadVolumeState()
    {
        optionsBackButton = GameObject.Find("ExitButton").GetComponent<Button>();
        optionsBackButton.onClick.AddListener(delegate { SaveVolumeState(); });
        slider.value = PlayerPrefs.GetFloat("volume", 0.3f);
    }

    void SceneChanged(Scene arg0, Scene arg1)
    {
        if (arg1.name.Equals("MainMenu"))
        {
            slider = GameObject.Find("Canvas").GetComponentInChildren<Slider>(true);
            optionsButton = GameObject.Find("OptionsButton").GetComponent<Button>();
            optionsButton.onClick.AddListener(delegate { LoadVolumeState(); });
            slider.onValueChanged.AddListener(delegate { ChangeVolume(slider); });
        }
    }
    public void PlayMusic()
    {
        float delay = Sounds[0].Source.clip.length / Sounds[0].Source.pitch - 0.07f;
        PlaySound("music_start");
        PlaySound("music_loop", delay);
    }
}
