using UnityEngine;

public class MonsterRequestSFX : MonoBehaviour
{
    public AudioClip correctClip;
    public AudioClip wrongClip;

    public float correctVol = 1f;
    public float wrongVol = 1f;
    private AudioSource sfx;
    void Awake()
    {
        sfx = GetComponent<AudioSource>();
        sfx.playOnAwake = false;
        sfx.spatialBlend = 0f;
    }

    public void PlayCorrect()
    {
        if (correctClip)
        {
            sfx.PlayOneShot(correctClip, correctVol);
        }
    }

    public void PlayWrong()
    {
        if (wrongClip)
        {
            sfx.PlayOneShot(wrongClip, wrongVol);
        }
    }
}
