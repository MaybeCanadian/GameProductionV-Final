using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Resource Item", menuName = "Items/Resources")]
public class ResourceItem : ScriptableObject
{
    [Tooltip("This is the name shown in game menus")]
    public string DisplayName = "Resource";
    [TextArea, Tooltip("This can be shown when clicking on the item in the menus, a brief description")]
    public string Description = "";
    [Tooltip("The image shown in the menus")]
    public Sprite UIImage;
    [Tooltip("The model used if dropped in game")]
    public GameObject inGameModel;
}
