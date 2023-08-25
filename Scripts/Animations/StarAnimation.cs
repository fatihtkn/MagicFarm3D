using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public Transform targetTransform;
    public List<Transform> resetPosition;
    public float upForce = 1f;
    public float sideForce = .1f;
    public List<GameObject> startList;
    public Ease easeType;
    public float completeTime;
    public float completeDelay = 1f;

    public Transform parentObject;


    
    private void OnEnable()
    {
        

        GravityActivity(true);
        StartCoroutine(CreateStar());

    }
    public IEnumerator CreateStar()
    {
        for (int i = 0; i < startList.Count; i++)
        {
            startList[i].SetActive(true);

            SphereExplosion(startList[i], resetPosition[i]);
            yield return new WaitForSeconds(0.07f);
        }

    }
    private void SphereExplosion(GameObject sphere,Transform resetpos)
    {
        float xForce = Random.Range(sideForce - 2, sideForce);
        float yForce = Random.Range(upForce / 2, upForce);
        float zForce = Random.Range(sideForce - 2, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        sphere.GetComponent<Rigidbody>().velocity = force;
        StartCoroutine(ResetSphere(sphere,resetpos));
    }
    IEnumerator ResetSphere(GameObject sphere,Transform resetpos)
    {
        targetTransform.position = PlayerManager.Instance.collectableTarget.position;
        yield return new WaitForSeconds(completeDelay);
        sphere.transform.DOMove(targetTransform.position, completeTime).SetEase(easeType).OnComplete(() =>
        {
           
            ResetPositions(sphere,resetpos);
        });


    }
    private void ResetPositions(GameObject collectableObject,Transform resetpos)
    {
        collectableObject.transform.parent = parentObject.transform;
        collectableObject.transform.position = resetpos.position;
        collectableObject.transform.rotation = resetpos.rotation;
        Rigidbody rb = collectableObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        


    }


    public void GravityActivity(bool control)
    {
        for (int i = 0; i < startList.Count; i++)
        {
            startList[i].transform.parent = null;
            startList[i].GetComponent<Rigidbody>().useGravity = control;
            
        }
    }

}
