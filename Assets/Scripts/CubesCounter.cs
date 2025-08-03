using TMPro;
using UnityEngine;

public class CubesCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cubesText;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private int _spawnedCount = 0;

    private void OnEnable()
    {
        _cubeSpawner.ObjectSpawned += CubeSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.ObjectSpawned += CubeSpawned;
    }

    private void CubeSpawned()
    {
        _spawnedCount++;

        _cubesText.text = $"—чет кубов:\n{_spawnedCount}\n{_cubeSpawner.PoolCapacity}\n{_cubeSpawner.Actives}";
    }
}
