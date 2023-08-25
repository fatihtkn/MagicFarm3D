using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pool",menuName ="Pool/ObjectList")]
public class CollectableListData : ScriptableObject
{
    public List<GameObject> leafList;
    public List<GameObject> orbList;
    public List<GameObject> potionList;
}
