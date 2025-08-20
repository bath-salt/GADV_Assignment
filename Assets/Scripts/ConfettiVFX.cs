using UnityEngine;

public class ConfettiVFX : MonoBehaviour
{
    ParticleSystem ps;

    private void OnEnable()
    {
        if (!ps)
        {
            ps = GetComponent<ParticleSystem>();
            ps.Clear(true);
            ps.Play(true);
        }
    }
}
