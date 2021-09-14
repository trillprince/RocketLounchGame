using Common.Scripts.MissionSystem;
using UnityEngine;

public class BasicSatelliteFactory : MonoBehaviour,ISatelliteFactory
{
    [SerializeField] private GameObject _basicSatellitePrefab;
    
    public GameObject CreateSatellite(Vector3 position,Quaternion rotation, Vector3 scale)
    {
        var satellite = Instantiate(_basicSatellitePrefab, transform);
        satellite.transform.position = position;
        satellite.transform.rotation = rotation;
        satellite.transform.localScale = scale;
        return satellite;
    }
}