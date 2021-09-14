using Common.Scripts.MissionSystem;
using UnityEngine;

public interface ISatelliteFactory
{
    GameObject CreateSatellite(Vector3 position,Quaternion rotation, Vector3 scale);
}