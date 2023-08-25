using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeEfectManager : MonoBehaviour
{
    private LineRenderer lineRend;
   private Transform wandPosition;
   private Transform curvePos;
   public Transform targetPosition;
    public int vertexCount = 12;
    private void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        curvePos = transform.GetChild(0).transform;
        wandPosition = GameObject.FindGameObjectWithTag("Wand").transform;
       

    }
    private void Update()
    {
        
      
        lineRend.SetPosition(1, wandPosition.position);
        var pointList = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
        {
            var tangentLineVertex1 = Vector3.Lerp(wandPosition.position, curvePos.position, ratio);
            var tangentLineVertex2 = Vector3.Lerp(curvePos.position, targetPosition.position, ratio);
            var bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            pointList.Add(bezierpoint);
        }
        lineRend.positionCount = pointList.Count;
        lineRend.SetPositions(pointList.ToArray());
    }

}
