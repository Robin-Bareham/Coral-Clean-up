//using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AiObjects
{
    public string AIGroupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate { get { return m_spawnRate; } }
    public int spawnAmount { get { return m_maxSpawnAmount; } }
    public bool randomizeStats { get { return m_randomizeStats; } }

    //Header groups serialized fields in the inspector
    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_spawnRate;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_maxSpawnAmount;
    [SerializeField]
    private bool m_randomizeStats;
}

public class AiSpawner : MonoBehaviour
{
    //Variable Declaration

    //Using list as unsure of number of waypoints
    [SerializeField]
    List<Transform> waypoints = new List<Transform>();

    //Create array from AiObject class
    [Header("AI Group Settings")]
    public AiObjects[] AIObject = new AiObjects[5];

    private void Start()
    {
        GetWaypoints();
    }

    void GetWaypoints()
    {
        //Look through nested children
        Transform[] wpList = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++)
        {
            if (wpList[i].tag == "waypoint")
            {
                waypoints.Add(wpList[i]);
            }
        }
    }
}
