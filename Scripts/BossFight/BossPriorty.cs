using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPriorty : MonoBehaviour
{
    [SerializeField] private int priortyRow;
    [SerializeField] private GameObject EnemyObject;
    private Register register;
        private void Start()
    {
        register=GetComponent<Register>();
    }


    private void Update()
    {
        if(priortyRow == PriortyManager.Instance.PriortyCount&register.isObjectSold==false)
        {
            EnemyObject.SetActive(true);
            this.enabled = false;
        }
    }
}
