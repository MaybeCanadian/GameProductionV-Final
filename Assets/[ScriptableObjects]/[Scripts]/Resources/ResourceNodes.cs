using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Resource Node", menuName = "Resources/Nodes")]
public class ResourceNodes : ScriptableObject
{
    [Tooltip("The resource drop table this node can produce")]
    public ResourceDropTable drops;
    [Tooltip("The damage required to break the node.")]
    public float maxHealth;
    [Tooltip("How long after breaking the node it respawns")]
    public float respawnTime;
    [Tooltip("If when the resource is depleted, will things be able to move past it")]
    public bool DisableCollisionsWhenDepleted = false;
    [Tooltip("The Sound Effect made when hitting this node")]
    public EffectList soundEffect;
}
