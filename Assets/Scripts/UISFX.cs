using UnityEngine;

public class UISFX : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip press;

    public AudioClip back;

    public void PlayPress()
    {
        if(sfx && press)
        {
            sfx.PlayOneShot(press);
        }
    }

    public void PlayBack()
    {
        if (sfx && back)
        {
            sfx.PlayOneShot(back);
        }
    }
}
