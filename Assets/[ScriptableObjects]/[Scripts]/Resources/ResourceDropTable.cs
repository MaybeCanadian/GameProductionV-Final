using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Drop Table", menuName = "Items/Tables")]
public class ResourceDropTable : ScriptableObject
{
    public string TableName = "";
    public List<Drops> Drops;
}

[System.Serializable]
public class Drops
{
    [Tooltip("Resource object to drop")]
    public ResourceItem item;
    [Min(0), Tooltip("Amount of the resource to drop per drop cluster")]
    public Vector2Int dropAmount =  new Vector2Int(0, 0);
    [Range(100.0f, 0.0f), Tooltip("Percent chance of dropping, between 0 and 100")]
    public float dropChance = 100.0f;

    public int GetDropAmount()
    {
        return Random.Range(dropAmount.x, dropAmount.y + 1);
    }
}
