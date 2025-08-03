using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _position;
    public event System.Action ObjectSpawned;

    private void OnEnable()
    {
        _cubeSpawner.CubeDisabeled += SpawnObject;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeDisabeled -= SpawnObject;
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.transform.position = _position;
        bomb.Renderer.material.color = Color.black;
        bomb.Rigidbody.velocity = Vector3.zero;
        bomb.Rigidbody.angularVelocity = Vector3.zero;
        bomb.gameObject.SetActive(true);
        bomb.StartTimer();
        ObjectSpawned?.Invoke();
        bomb.ReadyForRelease += Release;
    }
    protected override void Release(Bomb bomb)
    {
        bomb.ReadyForRelease -= Release;
        _objects.Release(bomb);
    }

    private void SpawnObject(Vector3 position)
    {
        _position = position;
        _objects.Get();
    }
}
