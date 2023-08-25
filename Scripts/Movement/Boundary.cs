using UnityEngine;

public class Boundary : MonoBehaviour
{
    public PetStats owlStats;   
    private void OnTriggerStay(Collider other)
    {
        bool control= other.gameObject.CompareTag("Floor");
        if (control)
        {
            CharacterMovement.Instance.MoveSpeed = owlStats.currentAttribute;


        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            CharacterMovement.Instance.MoveSpeed = 0f;
            
        }
    }
}
