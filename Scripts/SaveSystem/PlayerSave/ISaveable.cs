using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable<ObjectType>
{
    void Save(string objectSub,string ObjectCountSub,List<ObjectType>objectList);
      
}
