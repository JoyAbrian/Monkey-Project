using UnityEngine;
using System;

public enum SoundType
{
    Menu,
    EndingWin,
    EndingLose,
    Anomaly1,
    Anomaly2,
    Anomaly3,
    AmbienceStairs,
    AmbiencePatientRoom,
    AmbienceHallway,
    AmbienceHall,
    AmbienceControlRoom,
    AmbienceCanteen
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private AudioSource ambienceSource;

    private void Awake()
    {
        instance = this;
        ambienceSource = gameObject.AddComponent<AudioSource>();
        ambienceSource.loop = true;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

    public static void PlayAmbience(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.ambienceSource.clip = randomClip;
        instance.ambienceSource.volume = volume;
        instance.ambienceSource.Play();
    }

    public static void StopSound()
    {
        instance.audioSource.Stop();
    }

    public static void StopAmbience()
    {
        instance.ambienceSource.Stop();
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
            soundList[i].name = names[i];
    }
#endif
}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}