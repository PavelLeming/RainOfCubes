using TMPro;
using UnityEngine;

public class Counter<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private TextMeshProUGUI _objectsText;
    [SerializeField] protected Spawner<T> _objectSpawner;



    private void OnEnable()
    {
        _objectSpawner.ObjectSpawned += ObjectSpawned;
    }

    private void OnDisable()
    {
        _objectSpawner.ObjectSpawned += ObjectSpawned;
    }

    protected void ObjectSpawned()
    {
        _objectsText.text = $"—чет:\n{_objectSpawner.SpawnedCount}\n{_objectSpawner.PoolCapacity}\n{_objectSpawner.Actives}";
    }
}
