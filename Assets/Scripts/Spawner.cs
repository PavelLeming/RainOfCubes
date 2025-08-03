using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private T _object;
    private int _poolCapacity = 5;
    private int _poolMaxSize = 5;

    protected ObjectPool<T> _objects;

    private void Awake()
    {
        _objects = new ObjectPool<T>(
            createFunc: () => Instantiate(_object),
            actionOnGet: (poolableObject) => ActionOnGet(poolableObject),
            actionOnRelease: (poolableObject) => poolableObject.gameObject.SetActive(false),
            actionOnDestroy: (poolableObject) => Destroy(poolableObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    protected virtual void ActionOnGet(T poolableObject)
    {

    }

    protected virtual void Release(T poolableObject)
    {

    }
}
