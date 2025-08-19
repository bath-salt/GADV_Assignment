using UnityEngine;

public class WordSFX : MonoBehaviour
{
    public AudioClip strokeDing;
    public AudioClip wordComplete;

    public float strokeVol = 0.8f;
    public float wordVol = 1.0f;

    private AudioSource sfx;
    void Awake()
    {
        sfx = GetComponent<AudioSource>();
        sfx.playOnAwake = false;
        sfx.spatialBlend = 0f;
    }

    public void PlayStrokeComplete()
    {
        Play(strokeDing, strokeVol);
    }
    
    public void PlayWordComplete()
    {
        Play(wordComplete, wordVol);
    }

    private void Play(AudioClip clip, float volume)
    {
        if(!clip || !sfx)
        {
            return;
        }
        sfx.PlayOneShot(clip, volume);
    }
}
