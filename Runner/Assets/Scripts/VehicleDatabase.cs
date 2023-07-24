using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDatabase : MonoBehaviour
{
    public ObjectsDatabaseSO Database => _database;

    [SerializeField] ObjectsDatabaseSO _database;

    public static VehicleDatabase Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
