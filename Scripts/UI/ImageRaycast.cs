using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageRaycast : MonoSingleton<ImageRaycast> 
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;
    public bool controlTheRay;


    private void Start()
    {
        controlTheRay = false;
        m_Raycaster = GetComponent<GraphicRaycaster>();
       // m_EventSystem = GetComponent<EventSystem>();
       
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);
            
           
        }
    }

}
