using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.InputService;
using _GAME.scripts.Architecture.Architecture.Services.LevelServices;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DriftCarMove : MonoBehaviour
{
	public ParticleSystem damageEffect;

	// TechDept удалить не используемое в проекте
	// TechDept добавить линт
	[SerializeField] private GameObject moveEducation;
	[SerializeField] private Button LeftButton;
	[SerializeField] private Button RightButton;
	[SerializeField] private ReadyStadyGoText rsgText;

	// Настройки
	private float MoveSpeed = 1; // Скорость движения
	private float MaxSpeed = 30; // Максимальная скорость
	public float Drag = 0.98f; // Сопротивление движению
	public float SteerAngle = 20; // Угол поворота
	public float Traction = 1; // Сцепление
	private Vector3 _moveForce; // Сила движения
	private bool _isStarted = false;
	private LevelEvents _levelEvents;
	private WindowService _windowService;
	private IPersistanseDataService _persistanseDataService;
	private bool _isDestoyed = false;
	private IInputService _inputService;
	private ShopItemConfig carConfig;

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.TryGetComponent(out DestroierItem driftCarMove))
		{
			this.DestroyCar(other.GetContact(0).point);
		}
	}

	[Inject]
	private void Construct(LevelEvents levelEvents, WindowService windowService,
		IPersistanseDataService persistanseDataService)
	{
		_levelEvents = levelEvents;
		_windowService = windowService;
		_persistanseDataService = persistanseDataService;
		_levelEvents.OnAllItemsGets += StopCar;
	}

	private void Start()
	{
		_inputService = GetComponent<PCInput>();
	}


	public void StartCar()
	{
		_isStarted = true;
	}

	public void DestroyCar(Vector3 damagePosition)
	{
		PlayDamageEffect(damagePosition);
		if (!_isDestoyed)
		{
			_isDestoyed = true;
			MoveSpeed = 0;
			MaxSpeed = 0;
			_moveForce = Vector3.zero;
			_persistanseDataService.SubscructHP();
			_windowService.Open(WindowId.Fail);
		}
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
		if (!_isStarted && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)))
		{
			moveEducation.SetActive(false);
			CloseMain();
			StartCar();
		}

		if (_isStarted)
		{
			Move();
			Drift();
		}
	}

	public void CloseMain()
	{
		_windowService.Close(WindowId.Main);
	}

	public float GetSpeed()
	{
		return _moveForce.magnitude;
	}

	private void Move()
	{
		// TechDept переписать логику дрифта с падением машинки
		_moveForce += transform.forward * carConfig.speed * MoveSpeed;
		transform.position += _moveForce * Time.deltaTime;
	}

	private void Drift()
	{
		float steerInput = _inputService.MoveDirection;
		transform.Rotate(Vector3.up * steerInput * _moveForce.magnitude * SteerAngle * Time.deltaTime);

		// Сопротивление и ограничение максимальной скорости
		_moveForce *= Drag;
		_moveForce = Vector3.ClampMagnitude(_moveForce, MaxSpeed);

		_moveForce = Vector3.Lerp(_moveForce.normalized, transform.forward, Traction * Time.deltaTime) *
					 _moveForce.magnitude;
	}

	public void SetNewConfig(ShopItemConfig newItem)
	{
		carConfig = newItem;
	}
}
