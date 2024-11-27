using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.InputService;
using _GAME.scripts.Architecture.Architecture.Services.LevelServices;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
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
	private bool _iSFinished = false;
	private SoundsService _soundsService;
	private bool _timerStarted = false;

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.TryGetComponent(out DestroierItem driftCarMove))
		{
			this.DestroyCar(other.GetContact(0).point);
		}
	}

	[Inject]
	private void Construct(LevelEvents levelEvents, WindowService windowService,
		IPersistanseDataService persistanseDataService, SoundsService soundsService)
	{
		_levelEvents = levelEvents;
		_soundsService = soundsService;
		_windowService = windowService;
		_persistanseDataService = persistanseDataService;
		_levelEvents.OnAllItemsGets += OnCollectAllItemsHandler;
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
		if (_iSFinished)
		{
			return;
		}

		PlayDamageEffect(damagePosition);
		if (!_isDestoyed)
		{
			_isDestoyed = true;
			MoveSpeed = 0;
			MaxSpeed = 0;
			_moveForce = Vector3.zero;
			_persistanseDataService.SubscructHP();
			_windowService.Open(WindowId.Fail);
			StopAllAudio();
		}
	}

	private void PlayDamageEffect(Vector3 damagePosition)
	{
		Instantiate<ParticleSystem>(damageEffect, damagePosition, Quaternion.Euler(new Vector3(-90, 0, 0)),
			this.transform);
	}

	private void OnCollectAllItemsHandler()
	{
		_iSFinished = true;
		MoveSpeed = 0;
		StopAllAudio();
	}

	private void OnDestroy()
	{
		StopAllAudio();
	}

	private void StopAllAudio()
	{
		_soundsService.Stop(SoundID.Move);
		_soundsService.Stop(SoundID.Drift);
	}

	private void Update()
	{
		if (!_isStarted && !_timerStarted && _inputService.MoveDirection != 0)
		{
			_timerStarted = true;
			rsgText.Show();
			moveEducation.SetActive(false);
		}

		if (_isStarted)
		{
			MoveForward();
			Drift();
		}
	}

	public float GetSpeed()
	{
		return _moveForce.magnitude;
	}

	private void MoveForward()
	{
		if (_iSFinished || _isDestoyed)
			return;
		// TechDept переписать логику дрифта с падением машинки
		_moveForce += transform.forward * carConfig.speed * MoveSpeed;
		transform.position += _moveForce * Time.deltaTime;
		_soundsService.Play(SoundID.Move);
	}

	private void Drift()
	{
		if (_iSFinished || _isDestoyed)
			return;
		float steerInput = _inputService.MoveDirection;

		_moveForce *= Drag;
		if (Mathf.Abs(steerInput) > 0)
		{
			_soundsService.Play(SoundID.Drift);
			transform.Rotate(Vector3.up * steerInput * _moveForce.magnitude * SteerAngle * Time.deltaTime);
		}
		else
		{
			_soundsService.Stop(SoundID.Drift);
		}

		// Сопротивление и ограничение максимальной скорости
		_moveForce = Vector3.Lerp(_moveForce.normalized, transform.forward, Traction * Time.deltaTime) *
		             _moveForce.magnitude;
		_moveForce = Vector3.ClampMagnitude(_moveForce, MaxSpeed);
	}

	public void SetNewConfig(ShopItemConfig newItem)
	{
		carConfig = newItem;
	}
}
