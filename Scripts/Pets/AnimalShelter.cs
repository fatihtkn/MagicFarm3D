using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalShelter : MonoBehaviour
{
    public GameObject petSelectingPage;
    public GameObject cam;
    public Transform playerPos;
    private PetSelection petSelectionScript;
    public GameObject collectablesCanvas;
    private Collider col;
    private void Awake()
    {
        col= GetComponent<Collider>();  
    }

    private void Start()
    {
        petSelectionScript=petSelectingPage.GetComponent<PetSelection>();
        StartCoroutine(CoolDown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectablesCanvas.SetActive(false);
            petSelectingPage.SetActive(true);
            petSelectionScript.DefaultPet(0);
            petSelectionScript.DeactivePetObjects();
            cam.SetActive(true);
            PlayerManager.Instance.SetCharacterMeshActivity(false);
            CharacterMovement.Instance.moveInputPanel.SetActive(false);
            PlayerManager.Instance.transform.position = playerPos.position;
        }
        
    }
   

    IEnumerator CoolDown()
    {
        col.enabled = false;
        yield return new WaitForSeconds(2f);
        col.enabled= true;
    }
}
