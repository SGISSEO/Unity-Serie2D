using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'une instance de AudioManager dans la scène");
            return;
        }

        Instance = this;
    }

    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }


    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = pos;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
        return audioSource;
    }
}
