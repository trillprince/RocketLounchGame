using UnityEngine;

[CreateAssetMenu(fileName = "RocketSpeedStats", menuName = "ScriptableObjects/Stats/Rocket")]
public class RocketSpeedStats: ScriptableObject
{
    [SerializeField] private float _rocketStartSpeed = 40f;
    [SerializeField] private float _rocketMaxSpeed = 100f;
    [SerializeField] private float _rocketSpeedAcceleration = 15f;

    public float RocketStartSpeed
    {
        get => _rocketStartSpeed;
    }

    public float RocketMaxSpeed
    {
        get => _rocketMaxSpeed;
    }

    public float RocketSpeedAcceleration
    {
        get => _rocketSpeedAcceleration;
    }
}