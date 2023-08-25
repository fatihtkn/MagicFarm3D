using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriortyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject desiredObject;


    private void Start()
    {
        desiredObject.SetActive(true);
    }
}
