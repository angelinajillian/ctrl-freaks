using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // singleton

    // Sound FX settings struct
    private struct SoundSettings
    {
        public AudioClip clip;
        public float volume;
        public float spatialBlend;
        public bool enableSpatial;
        public float minDistance;
        public float maxDistance;
        public AudioRolloffMode rolloffMode;

        public SoundSettings(AudioClip clip, float volume, float spatialBlend, bool enableSpatial, float minDistance, float maxDistance, AudioRolloffMode rolloffMode)
        {
            this.clip = clip;
            this.volume = volume;
            this.spatialBlend = spatialBlend;
            this.enableSpatial = enableSpatial;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            this.rolloffMode = rolloffMode;
        }
    }

    [SerializeField] private AudioMixerGroup ambienceMixerGroup;

    // FX sounds
    [SerializeField] private List<AudioClip> walkingSFX;
    [SerializeField] private List<AudioClip> runningSFX;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip punchSound;
    [SerializeField] private AudioClip punchWhooshSound;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip runningSound;
    [SerializeField] private AudioClip cannonFireSound;
    [SerializeField] private AudioClip fire1Sound;

    // Ambience
    [SerializeField] private AudioClip caveAmbience;

    // Music

    private void Awake()
    {
        // singleton instance of our soundManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameObject ambientObject = new GameObject("AudioSource");
        AudioSource ambientSource = ambientObject.AddComponent<AudioSource>();

        ambientSource.clip = caveAmbience;
        ambientSource.loop = true;
        ambientSource.Play();

    }

    public void PlayPunchWooshSound(Vector3 position)
    {
        SoundSettings soundSettings = new SoundSettings(punchWhooshSound, 1.0f, 1.0f, false, 1.0f, 75.0f, AudioRolloffMode.Linear);
        PlaySound(soundSettings, position);
    }
    
    public void PlayWalkSound(Vector3 position)
    {
        //PlaySound(throwSoundClip, position);
    }
    
    public void PlayFire1Sound(Vector3 position)
    {
        SoundSettings soundSettings = new SoundSettings(fire1Sound, 1.0f, 1.0f, false, 1.0f, 75.0f, AudioRolloffMode.Linear);
        PlaySound(soundSettings, position);
    }

    public void PlayCannonSound(Vector3 position)
    {
        SoundSettings soundSettings = new SoundSettings(cannonFireSound, 1.0f, 1.0f, true, 1.0f, 75.0f, AudioRolloffMode.Linear);
        PlaySound(soundSettings, position);
    }

    public void PlayExplosionSound(Vector3 position)
    {
        SoundSettings soundSettings = new SoundSettings(explosionSound, 1.0f, 1.0f, true, 1.0f, 30.0f, AudioRolloffMode.Linear);
        PlaySound(soundSettings, position);
    }

    public void PlayPunchSound(Vector3 position)
    {
        SoundSettings soundSettings = new SoundSettings(punchSound, 0.9f, 1.0f, true, 1.0f, 10.0f, AudioRolloffMode.Logarithmic);
        PlaySound(soundSettings, position);

    }

    private void PlaySound(SoundSettings settings, Vector3 position)
    {
        // create audio source gameobject and move it to the location of where sound should be
        GameObject audioSourceObject = new GameObject("AudioSource");
        audioSourceObject.transform.position = position;

        // now add the actual audiosource to our gameobject
        AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();

        // set the audio clip settings
        audioSource.clip = settings.clip; 
        audioSource.volume = settings.volume;
        audioSource.spatialBlend = settings.spatialBlend; // 3d spatial sound since we 3d out here
        audioSource.spatialize = settings.enableSpatial;
        audioSource.minDistance = settings.minDistance;
        audioSource.maxDistance = settings.maxDistance;
        audioSource.rolloffMode = settings.rolloffMode;

        audioSource.Play();

        // destroy audioSourceObject now that we are done with it
        Destroy(audioSourceObject, audioSource.clip.length);
    }
}
