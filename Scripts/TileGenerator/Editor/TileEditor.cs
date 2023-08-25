using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(TileGenerator))]
public class TileEditor : Editor
{
    
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        TileGenerator generator = (TileGenerator)target;
        if (generator.representativePosition != null) generator.representativePosition.transform.position = generator.pos;
        GUILayout.BeginHorizontal();
                 if (GUILayout.Button("GenerateTile"))
                 {
                     generator.GenerateTile();
                     
                 }
                 if (GUILayout.Button("GenerateBuyArea"))
                 {
                     generator.GenerateBuyArea();
                 }
                 if (GUILayout.Button("GenerateLockedBridge"))
                 {
                     generator.GenerateLockedBridge();
                 }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("AddValueToX"))
        {
            generator.AddValueX();
        }
        if (GUILayout.Button("ReduceValueToX"))
        {
            generator.ReduceValueX();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("AddValueToZ"))
        {
            generator.AddTileValueZ();
        }
        if (GUILayout.Button("ReduceValueToZ"))
        {
            generator.ReduceTileValueZ();
        }
        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("SetObjectPosition"))
        {
            generator.SetObjectPosition();
        }
        if (GUILayout.Button("ResetObjectPosition"))
        {
            generator.ResetPosition();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("SetLandNumber"))
        {
            generator.SetLandNo();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("SetCost"))
        {
            generator.SetTileCost();
        }
        GUILayout.EndHorizontal();
       
    }

}
