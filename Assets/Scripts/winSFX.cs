using UnityEngine;

public class winSFX : MonoBehaviour
{
    public AudioClip winClip;
    public float vol = 1f;

    private AudioSource sfx;

    void Awake()
    {
        sfx = GetComponent<AudioSource>();
        sfx.playOnAwake = false;
    }

    private void OnEnable()
    {
        if(!winClip)
        {
            return;
        }

        sfx.PlayOneShot(winClip, vol);  
    }
}
