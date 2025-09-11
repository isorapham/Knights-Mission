using UnityEngine;

public class BossAttackDriver : MonoBehaviour
{
    [Header("Hitboxes")]
    public Collider2D spikeHitbox;
    public Collider2D rollHitbox;
    public Collider2D roarHitbox;

    void Awake()
    {
        DisableAll();
    }

    // Spike
    public void Spike_On() => spikeHitbox.enabled = true;
    public void Spike_Off() => spikeHitbox.enabled = false;

    // Roll
    public void Roll_On() => rollHitbox.enabled = true;
    public void Roll_Off() => rollHitbox.enabled = false;

    // Roar
    public void Roar_On() => roarHitbox.enabled = true;
    public void Roar_Off() => roarHitbox.enabled = false;

    public void DisableAll()
    {
        if (spikeHitbox) spikeHitbox.enabled = false;
        if (rollHitbox) rollHitbox.enabled = false;
        if (roarHitbox) roarHitbox.enabled = false;
    }
}
