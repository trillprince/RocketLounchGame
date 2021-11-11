using Common.Scripts.Rocket;
using UnityEngine;

public class HologramEffect : RocketEffect
{
    public HologramEffect(RocketHealth rocketHealth, IEffectAudio effectAudio)
        : base(rocketHealth, effectAudio)
    {
        rocketHealth.AddHealth(1);
        rocketHealth.OnDamage += HealOnDamage;
    }

    private void HealOnDamage()
    {
        Health.AddHealth(1);
    }
}