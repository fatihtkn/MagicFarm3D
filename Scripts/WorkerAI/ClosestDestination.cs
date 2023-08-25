using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestDestination : MonoBehaviour
{
   
    public Vector3 DestinationOutput(Transform workerPos, List<Transform> targetList)
    {

       
        float[] destinationDelta=new float[targetList.Count];
        Dictionary<Vector3, float> dic=new Dictionary<Vector3, float>();
        Vector3 output=new Vector3();
        for (int i = 0; i < targetList.Count; i++)
        {
            float delta = Vector3.Distance(workerPos.position, targetList[i].position);
            dic.Add(targetList[i].position, delta);
            //print(targetList[i].gameObject.name + " " + targetList[i].position+"Distance:"+delta);
            destinationDelta[i]=delta;
        }
        
        Array.Sort(destinationDelta);
        
        foreach (KeyValuePair<Vector3,float> item in dic)
        {
            if (item.Value == destinationDelta[0])
            {
                output= item.Key;
                
            }
        }
        

        return output;
    }

}
