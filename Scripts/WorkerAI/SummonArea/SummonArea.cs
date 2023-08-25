using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonArea : MonoBehaviour
{
    [SerializeField] private GameObject summonerPanel;
    [SerializeField] private SummonerTemplate[] template;
    private SummonerBuilder[] summoner;
    private void Start()
    {
        summoner = new SummonerBuilder[template.Length];

        InitBuilders();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PanelActivity(true);
            ControlRequireTile();
        }

       
    }
    private void OnTriggerExit(Collider other)
    {
        PanelActivity(false);
    }


    private void PanelActivity(bool control)
    {
        summonerPanel.SetActive(control);  
    }

    private void InitBuilders()
    {
        

        for (int i = 0; i < summoner.Length; i++)
        {
            summoner[i] = new SummonerBuilder(template[i]);

        }
    }
    private void ControlRequireTile()
    {
        for (int i = 0; i < summoner.Length; i++)
        {
            summoner[i].ControlRequireTile();
        }
    }


}
