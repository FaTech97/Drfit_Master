using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.LevelServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DriftCarMove : MonoBehaviour
{
    public ParticleSystem damageEffect;

    [SerializeField] private GameObject moveEducation;
    [SerializeField] private Button LeftButton;

    [SerializeField] private Button RightButton;

    // Настройки
    public float MoveSpeed = 50; // Скорость движения
    public float MaxSpeed = 15; // Максимальная скорость
    public float Drag = 0.98f; // Сопротивление движению
    public float SteerAngle = 20; // Угол поворота
    public float Traction = 1; // Сцепление
    private Vector3 _moveForce; // Сила движения
    private bool _isStarted = false;
    private LevelEvents _levelEvents;
    private WindowService _windowService;
    private IPersistanseDataService _persistanseDataService;

    [Inject]
    private void Construct(LevelEvents levelEvents, WindowService windowService,
        IPersistanseDataService persistanseDataService)
    {
        _levelEvents = levelEvents;
        _windowService = windowService;
        _persistanseDataService = persistanseDataService;
        _levelEvents.OnAllItemsGets += StopCar;
    }

    public void StartCar()
    {
        _isStarted = true;
    }

    public void DestroyCar(Vector3 damagePosition)
    {
        MoveSpeed = 0;
        MaxSpeed = 0;
        _moveForce = Vector3.zero;
        PlayDamageEffect(damagePosition);
        _persistanseDataService.SubscructHP();
        _windowService.Open(WindowId.Fail);
    }

    private void PlayDamageEffect(Vector3 damagePosition)
    {
        Instantiate<ParticleSystem>(damageEffect, damagePosition, Quaternion.Euler(new Vector3(-90, 0, 0)),
            this.transform);
    }

    private void StopCar()
    {
        MoveSpeed = 0;
    }

    private void Update()
    {
        if (!_isStarted && Input.anyKey)
        {
            moveEducation.SetActive(false);
            _windowService.Close(WindowId.Main);
            StartCar();
        }

        if (_isStarted)
        {
            Move();
            Drift();
        }
    }

    public float GetSpeed()
    {
        return _moveForce.magnitude;
    }

    private void Move()
    {
        // Движение
        _moveForce += transform.forward * MoveSpeed * 1 * Time.deltaTime;
        transform.position += _moveForce * Time.deltaTime;
    }

    private void Drift()
    {
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * _moveForce.magnitude * SteerAngle * Time.deltaTime);

        // Сопротивление и ограничение максимальной скорости
        _moveForce *= Drag;
        _moveForce = Vector3.ClampMagnitude(_moveForce, MaxSpeed);

        _moveForce = Vector3.Lerp(_moveForce.normalized, transform.forward, Traction * Time.deltaTime) *
                     _moveForce.magnitude;
    }
}