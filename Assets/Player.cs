using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Нормальная скорость")]
    [SerializeField] private float _speedNormal;
    [Header("Скорость при ускорении")]
    [SerializeField] private float _speedBoost;
    [SerializeField] private Rigidbody _rb;
    [Header("Чувствительность мыши")]
    [SerializeField] private float _mouseSens;
    [Header("Хвосты для корабля")]
    [SerializeField] private List<TrailRenderer> _trails = new List<TrailRenderer>();
    [Header("Луч для выстрела")]
    [SerializeField] private List<LineRenderer> _guns = new List<LineRenderer>();
    [Header("Дальность выстрела")]
    [SerializeField] private float _shootRange;
    [Header("Источник звука")]
    [SerializeField] private AudioSource _audioSource;
    private bool _isCanShoot = true;
    private float _speed;
    private Vector2 _moveDirection;
    private Vector2 _mouseDirection;
    private bool _isBoost;
    private bool _isShoot;




    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Application.targetFrameRate = 200;
        foreach (var gun in _guns)
        {
            gun.enabled = false;
        }
        
    }

    private void Update()
    {
        PlayerInput();
        OnMove();
        OnRotation();
        DrawTrails();
        OnShoot();
    }

    private void OnShoot()
    {
        if(_guns.Count == 0) return;

        if (_isShoot && _isCanShoot)
        {
            StartCoroutine(OnShootRoutine());
        }
    }

    private IEnumerator OnShootRoutine()
    {
        _isCanShoot = false;
        foreach (var gun in _guns)
        {
            gun.SetPosition(1, Vector3.forward * _shootRange);
            gun.enabled = true;
        }
        _audioSource.PlayOneShot(_audioSource.clip);
        yield return new WaitForSeconds(0.1f);
        foreach (var gun in _guns)
        {
            gun.enabled = false;
            ShootRaycast(gun.transform.position);
        }
        _isCanShoot = true;
    }

    private void ShootRaycast(Vector3 pointStartRay)
    {

        RaycastHit hit;
        if(Physics.Raycast(pointStartRay, transform.forward, out hit ,_shootRange))
        {
            print(hit.collider.name);
            Health health = hit.collider.GetComponent<Health>();
            if (health)
            {
                health.GetDamage();
            }
        }
    }

    private void DrawTrails()
    {
        if (_trails.Count == 0) return;

        if (_moveDirection.y > 0)
        {
            foreach (var item in _trails)
            {
                item.enabled = true;
            }
        }
        else
        {
            foreach (var item in _trails)
            {
                item.enabled = false;
            }
        }
    }

    private void PlayerInput()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");
        _moveDirection = _moveDirection.normalized;
        

        _mouseDirection.x = Input.GetAxis("Mouse X");
        _mouseDirection.y = Input.GetAxis("Mouse Y");
        _mouseDirection *= Time.deltaTime * _mouseSens;

        _isBoost = Input.GetKey(KeyCode.LeftShift);
        _isShoot = Input.GetKeyDown(KeyCode.Mouse0);
    }

    private void OnMove()
    {
        if (_isBoost)
        {
            _speed = _speedBoost;
        }
        else
        {
            _speed = _speedNormal;
        }

        Vector3 move = transform.TransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.y));
        if(_moveDirection.y != 0)
        {
            _rb.velocity = move * _speed;
        }

        _rb.AddRelativeTorque(Vector3.back * 500 * _moveDirection.x * Time.deltaTime);
    }

    private void OnRotation()
    {
        //Vector3 move = transform.TransformDirection(new Vector3(_moveDirection.x, 0, _moveDirection.y));
        transform.Rotate(new Vector3(-_mouseDirection.y, _mouseDirection.x, 0));
    }
}
