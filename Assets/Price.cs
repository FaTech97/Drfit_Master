using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Price : MonoBehaviour, IPointerClickHandler
{
    private IPersistanseDataService _persistanseDataService;
    private readonly int _priceCount = 30;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService)
    {
        _persistanseDataService = persistanseDataService;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _persistanseDataService.AddMoney(_priceCount);
        Destroy(this.gameObject);
    }
}
