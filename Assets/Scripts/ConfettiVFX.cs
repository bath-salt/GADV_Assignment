using UnityEngine;

public class ConfettiVFX : MonoBehaviour
{
    ParticleSystem ps;

    private void OnEnable()
    {
        // Only fetch the ParticleSystem once (the first time it is enabled)
        // This avoids repeated GetComponent calls every time the object is re enabled
        // I clear it first to enable no leftover particles remain from a previous play session 
        if (!ps)
        {
            ps = GetComponent<ParticleSystem>();
            ps.Clear(true);
            ps.Play(true);
        }
    }
}
