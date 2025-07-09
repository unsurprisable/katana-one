using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource soundObject;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource audioSource = Instantiate(soundObject, position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volumeMultiplier;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlaySoundClip(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource audioSource = Instantiate(soundObject, position, Quaternion.identity);

        audioSource.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        audioSource.volume = volumeMultiplier;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
