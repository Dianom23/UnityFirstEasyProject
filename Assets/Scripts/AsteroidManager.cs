using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


public class AsteroidManager : MonoBehaviour
{
    [Header("Расстояние между астероидами")]
    [SerializeField] private float _gridSpacing;

    [Header("Количество астероидов на оси")]
    [SerializeField, Range(0,15)] private int _countAstetoidsOnAxisGrid;

    [Header("Создаваемые астероиды")]
    [SerializeField] private List<GameObject> _asteroids = new List<GameObject>();

    void Start()
    {
        PlaceAsteroids();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Spawn Asteroids")]
    private void PlaceAsteroids()
    {
        for (int x = 0; x < _countAstetoidsOnAxisGrid; x++)
        {
            for (int y = 0; y < _countAstetoidsOnAxisGrid; y++)
            {
                for (int z = 0; z < _countAstetoidsOnAxisGrid; z++)
                {
                    InstantiateAsteroid(x, y, z);
                }
            }
        }
    }

    [ContextMenu("Clear Asteroids")]
    private void ClearAsteroids()
    {
        foreach (Transform item in transform)
        {
            //Destroy(item);
            DestroyImmediate(item.gameObject);
        }
    }

    private void InstantiateAsteroid(int x, int y, int z)
    {
        GameObject randomAsteroid = _asteroids[Random.Range(0, _asteroids.Count)];
        GameObject createdAsteroid = Instantiate(randomAsteroid,
            new Vector3(transform.position.x + (x * _gridSpacing) + AsteroidOffset(),
            transform.position.y + (y * _gridSpacing) + AsteroidOffset(),
            transform.position.z + (z * _gridSpacing) + AsteroidOffset()
            ), Quaternion.identity, transform);
        createdAsteroid.AddComponent<Asteroid>();
    }

    private float AsteroidOffset()
    {
        return Random.Range(-_gridSpacing / 2f, _gridSpacing / 2f);
    }
}
