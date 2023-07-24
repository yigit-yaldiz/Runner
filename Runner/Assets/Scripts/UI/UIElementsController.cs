using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIElementsController : MonoBehaviour
{
    public TextMeshProUGUI CountdownText => _countdownText;

    [SerializeField] GameObject _raceButton;
    [SerializeField] TextMeshProUGUI _countdownText;
    public static UIElementsController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CarSelecting.CarSelected += ActivateRaceElements;
    }

    private void OnDisable()
    {
        CarSelecting.CarSelected -= ActivateRaceElements;
    }

    void ActivateRaceElements(Collider other)
    {
        _raceButton.SetActive(true);
    }
}
