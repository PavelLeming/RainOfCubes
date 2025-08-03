using TMPro;
using UnityEngine;

public class BombCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bombsText;
    [SerializeField] private BombSpawner _bombSpawner;

    private int _spawnedCount = 0;

    private void OnEnable()
    {
        _bombSpawner.ObjectSpawned += BombSpawned;
    }

    private void OnDisable()
    {
        _bombSpawner.ObjectSpawned += BombSpawned;
    }

    private void BombSpawned()
    {
        _spawnedCount++;

        _bombsText.text = $"Счет бомб:\n{_spawnedCount}\n{_bombSpawner.PoolCapacity}\n{_bombSpawner.Actives}";
    }
}
