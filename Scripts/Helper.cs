using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera.main, out var result);
        return result;
    }
}
