using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using Shop;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class OnGuiChangedHandler : MonoBehaviour
{
    [SerializeField] private GameObject whileEffectPrefab;
    [SerializeField] private Transform GUI;
    private IPersistanseDataService _persistanseDataService;
    private ItemId _currentId;
    private StaticDataService _staticDataService;
    private SizeAligner _sizeAligner;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService, StaticDataService staticDataService)
    {
        _persistanseDataService = persistanseDataService;
        _staticDataService = staticDataService;
    }

    private void Awake()
    {
        _sizeAligner = GetComponent<SizeAligner>();
    }

    private void Start()
    {
        SpawnCar(_persistanseDataService.Data.Player.CurrectItemId);
        _persistanseDataService.OnDataChanged += CheckNewValue;
    }

    private void OnDestroy()
    {
        _persistanseDataService.OnDataChanged -= CheckNewValue;
    }

    private void CheckNewValue()
    {
        if (_currentId != _persistanseDataService.Data.Player.CurrectItemId)
        {
            SpawnCar(_persistanseDataService.Data.Player.CurrectItemId);
        }
    }

    private void SpawnCar(ItemId playerCurrectItemId)
    {
        if (GUI.childCount != 0)
        {
            Destroy(GUI.GetChild(0).gameObject);
        }

        _currentId = playerCurrectItemId;
        ShopItemConfig newItem = _staticDataService.Items.Get(playerCurrectItemId);
        GameObject gameObject = Instantiate(newItem.model, GUI);
        _sizeAligner.AlignColliderSizeToObjectSize(gameObject);
        SpawnWheelsShadow();
    }

    private void SpawnWheelsShadow()
    {
        var _collider = GetComponent<BoxCollider>();
        var leftSpawn = new Vector3(_collider.center.x - _collider.size.x / 2, _collider.center.y - _collider.size.y / 2,
            _collider.center.z + _collider.size.z / 2);
        var rightSpawn = new Vector3(_collider.center.x + _collider.size.x / 2, _collider.center.y - _collider.size.y / 2,
            _collider.center.z + _collider.size.z / 2);
        var leftBackSpawn = new Vector3(_collider.center.x - _collider.size.x / 2, _collider.center.y - _collider.size.y / 2,
            _collider.center.z - _collider.size.z / 2);
        var rightBackSpawn = new Vector3(_collider.center.x + _collider.size.x / 2, _collider.center.y - _collider.size.y / 2,
            _collider.center.z - _collider.size.z / 2);
        SpawnOneShadow(leftSpawn, "left");
        SpawnOneShadow(rightSpawn, "right");
        SpawnOneShadow(leftBackSpawn, "leftBack");
        SpawnOneShadow(rightBackSpawn, "rightBack");
    }

    private void SpawnOneShadow(Vector3 leftSpawn, string name)
    {
        GameObject leftObject =
            Instantiate(whileEffectPrefab, Vector3.zero, Quaternion.identity);
        leftObject.name = name;
        leftObject.transform.parent = transform;
        leftObject.transform.localPosition = leftSpawn;
        leftObject.transform.parent = GUI.GetChild(0);
    }
}