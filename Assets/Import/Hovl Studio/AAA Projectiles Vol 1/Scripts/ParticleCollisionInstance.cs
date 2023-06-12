/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0,0,0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = true;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;
    public float damage;

    private Vector3 shootDir;

    [SerializeField]
    private float moveSpeed;

    


    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        

        if (other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().TakeHit(0.05f);

        }


        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);     
      
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
               GameObject instance = PoolManager.instance.Pull("Hit23", collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
              
                if (instance==null)
                {
                    instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;

                }


                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { instance.transform.rotation = Quaternion.Euler(rotationOffset); }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }

                //PoolManager.instance.Push(instance, "Hit23");
                StartCoroutine(PushToPull(instance, "Hit23", 1.2f));
            }
        }
        if (DestoyMainEffect == true)
        {
            StartCoroutine(PushToPull(gameObject, "Bullet", 2));



            //Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }

    public void Setup(Transform enemy)
    {
        Vector3 shootDirection  = (enemy.GetComponent<ITargetable>().TargetPoint().position - transform.position).normalized;
        this.shootDir = shootDirection;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(shootDir));
        transform.LookAt(enemy.GetComponent<ITargetable>().TargetPoint());

       

    }

    IEnumerator PushToPull(GameObject pushObject,string poolName , float delay)
    {
        
        yield return new WaitForSeconds(delay);
       PoolManager.instance.Push(pushObject, poolName);
        
    }

    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (n < 0)
        {
            n += 360;
        }
        return n;
    }


}
