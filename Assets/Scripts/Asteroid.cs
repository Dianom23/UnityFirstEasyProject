using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 _randomScale;
    private Vector3 _randomRotation;
    private float _speed;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_randomScale.x = Random.Range(0.5f, 7f);
       // _randomScale.y = Random.Range(0.5f, 7f);
        //_randomScale.z = Random.Range(0.5f, 7f);
        transform.localScale = Vector3.one * Random.Range(0.5f, 7f);

        _randomRotation.x = Random.Range(0f, 360f);
        _randomRotation.y = Random.Range(0f, 360f);
        _randomRotation.z = Random.Range(0f, 360f);
        transform.eulerAngles = _randomRotation;

        _speed = Random.Range(1, 10);
       
    }
    private void Update()
    {
        _rb.velocity = transform.forward * _speed;
    }


}
