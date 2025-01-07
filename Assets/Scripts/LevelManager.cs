using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] AudioMixer mixer;

    public AudioClip[] clips;
    AudioSource audioSource;

    public const string MASTER_KEY = "Volume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("do not destroy");
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            print("do destroy");
            Destroy(gameObject);
        }

        LoadVolume();

    }

    public void PlayClip(int clipNumber)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clips[clipNumber], 0.7f); // start clip
    }

    public void StopClip()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop(); //stop currently playing clip
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }


}
