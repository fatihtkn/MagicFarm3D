using System.Collections.Generic;
public interface ILoadable<ObjectType>
{
   
    void Load(string objectSub, string ObjectCountSub, List<ObjectType> objectList);

}
