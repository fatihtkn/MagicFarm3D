using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TaskAnim : MonoBehaviour
{
    Transform _parent;
    Vector3 _resetPos;
    Quaternion _resetRotation;
    Rigidbody _rb;
    public CollecTableType collectableType;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        SetKinematic(false);
    }
    private void Start()
    {


        if (collectableType == CollecTableType.Star)
        {
            _resetPos = transform.position;
            _resetRotation = transform.rotation;
            _parent = transform.parent.transform;
            //transform.parent= null;
            
           // SetGravityActivity(true);
        }
      
    }
    private void OnEnable()
    {
        if (collectableType != CollecTableType.Star)
        {
            _resetPos = transform.position;
            _resetRotation = transform.rotation;
            _parent = transform.parent.transform;
            _rb = GetComponent<Rigidbody>();
            SetGravityActivity(true);
        }
        
    }


    public async Task RotateForSeconds(float duration,Transform targetPos, InterractType interractType)
    {
        
        var end =  duration;
        float timer = 0;
        
        
        await Task.Delay(900);
        while (Vector3.Distance(transform.position,targetPos.position)>0.5f)
        {
            timer += Time.deltaTime;
            transform.position=Vector3.Lerp(transform.position,targetPos.position,timer/end);

            await Task.Yield();

        }
        OnTaskCompleted(interractType);
       
    }

    private void OnTaskCompleted(InterractType interractType)
    {
        if (collectableType == CollecTableType.Star) { PlayerManager.Instance.starParticle.Play(); AudioManager.Instance.PlaySound(AudioTypes.Star); } 
        if (collectableType == CollecTableType.Leaf) 
        { 
            if(interractType==InterractType.Player)
            { PlayerManager.Instance.leafParticle.Play(); }

        }
        if (collectableType == CollecTableType.Orb) { ParticleSystem orb =Instantiate(PlayerManager.Instance.orbParticle, PlayerManager.Instance.orbParticle.transform.position, Quaternion.identity, PlayerManager.Instance.transform); orb.Play(); AudioManager.Instance.PlaySound(AudioTypes.Orb); }
        ResetTransform();
        ResetRb();
        gameObject.SetActive(false);
        
    }
    void ResetTransform()
    {
        transform.parent = _parent;
        transform.position = _resetPos;
        transform.rotation = _resetRotation;
    }

    void ResetRb()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.useGravity = false;
        
    }

    public void SetGravityActivity(bool control)
    {
        _rb.useGravity=control;
    }
    public void AddVelocity(float sideForce, float upForce)
    {
        float xForce = Random.Range(-sideForce , sideForce);
        float yForce = Random.Range(upForce / 2, upForce);
        float zForce = Random.Range(-2 , 2);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        _rb.velocity = force;
    }
    public void SetKinematic(bool control)
    {
       

        
        _rb.isKinematic= control;
    }
    
}
