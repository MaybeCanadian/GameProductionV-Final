using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnLocations : MonoBehaviour
{
    public List<Transform> differentSpawnLocations;

    private void Start()
    {
        differentSpawnLocations = GetComponents<Transform>().ToList();
        differentSpawnLocations.Add(transform);
    }
}
