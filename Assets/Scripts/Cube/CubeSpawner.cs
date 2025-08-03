using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    private int _repeatTime = 2;
    public event System.Action<Vector3> CubeDisabeled;

    private void Start()
    {
        StartCoroutine(CountdownForNewCube());
    }

    private IEnumerator CountdownForNewCube()
    {
        var wait = new WaitForSeconds(_repeatTime);

        while (enabled)
        {
            yield return wait;

            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        _objects.Get();
    }

    protected override void ActionOnGet(Cube cube)
    {
        cube.transform.position = new Vector3(Random.Range(-9, 9), 10, Random.Range(-9, 9));
        cube.Initialize(true);
        cube.Renderer.material.color = Color.white;
        cube.Rigidbody.velocity = Vector3.zero;
        cube.Rigidbody.angularVelocity = Vector3.zero;
        cube.gameObject.SetActive(true);
        TellAboutSpawn();
        cube.ReadyForRelease += Release;
    }

    protected override void Release(Cube cube)
    {
        cube.ReadyForRelease -= Release;
        CubeDisabeled?.Invoke(cube.transform.position);
        _objects.Release(cube);
    }
}
