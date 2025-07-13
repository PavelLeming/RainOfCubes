using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    private int _repeatTime = 2;
    private int _poolCapacity = 5;
    private int _poolMaxSize = 5;

    private ObjectPool<Cube> _cubes;

    private void Awake()
    {
        _cubes = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = new Vector3(Random.Range(-9, 9), 10, Random.Range(-9, 9));
        cube.Initialize(true, cube.GetComponent<Renderer>(), cube.GetComponent<Rigidbody>());
        cube.Renderer.material.color = Color.white;
        cube.Rigidbody.velocity = Vector3.zero;
        cube.Rigidbody.angularVelocity = Vector3.zero;
        cube.gameObject.SetActive(true);
        cube.ReadyForRelease += GetReleasde;
    }

    private void Start()
    {
        StartCoroutine(CountdownForNewCube());
    }

    private IEnumerator CountdownForNewCube()
    {
        var wait = new WaitForSeconds(1);

        while (true)
        {
            for (int i = _repeatTime; i > 0; i--)
            {
                yield return wait;
            }

            GetCube();
        }
    }

    private void GetCube()
    {
        _cubes.Get();
    }

    private void GetReleasde(Cube cube)
    {
        cube.ReadyForRelease -= GetReleasde;
        _cubes.Release(cube);
    }
}
