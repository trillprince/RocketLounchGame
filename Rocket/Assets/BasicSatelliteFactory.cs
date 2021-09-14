using Common.Scripts.MissionSystem;
using UnityEngine;

public class BasicSatelliteFactory : MonoBehaviour,ISatelliteFactory
{
    [SerializeField] private GameObject _basicSatellitePrefab;
    
    public GameObject CreateSatellite()
    {
        var satellite = Instantiate(_basicSatellitePrefab, transform);
        return satellite;
    }
}