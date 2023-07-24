using System;
using System.Collections;
using UnityEngine;
using PathCreation.Examples;

public class CarSelecting : MonoBehaviour
{
    public GameObject MergedVehicle;
    public Transform RaceCars;

    public static CarSelecting Instance { get; private set; }

    public static Action<Collider> CarSelected;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CarSelected += LerpTheCarToRacePos;
        CarSelected += RacePrepairing;
    }

    private void OnDisable()
    {
        CarSelected -= LerpTheCarToRacePos;
        CarSelected -= RacePrepairing;
    }

    private void OnTriggerEnter(Collider other)
    {
        DragDrop dragDrop = other.GetComponentInChildren<DragDrop>();

        if (dragDrop == null)
        {
            Debug.LogWarning($"This vehicle do not have {dragDrop} component");
            return;
        }

        if (other.CompareTag("Vehicle") && dragDrop.IsThatMerged)
        {
            CarSelected(other);    
        }
    }

    void LerpTheCarToRacePos(Collider other)
    {
        StartCoroutine(KickBackLerp(1f));

        IEnumerator KickBackLerp(float animTime)
        {
            float elapsedTime = 0;

            Vector3 currentPos = other.transform.parent.position + Vector3.right;
            Vector3 targetPos = transform.position + new Vector3(1, 0, 0.1829f);

            while (elapsedTime < animTime)
            {
                elapsedTime += Time.deltaTime;
                other.transform.parent.position = Vector3.Lerp(currentPos, targetPos, elapsedTime / animTime);
                yield return null;
            }

            //other.transform.parent.position = targetPos
        }
    }

    void RacePrepairing(Collider other)
    {
        Transform vehicleTransform = other.transform;
        vehicleTransform.tag = "Player";
        MergedVehicle = vehicleTransform.gameObject;
        vehicleTransform.parent.SetParent(RaceCars);

        vehicleTransform.position += new Vector3(-1, 0, 0);
        other.GetComponentInParent<PlayerVehicle>().SetRacePathToVehicle();
    }
}
