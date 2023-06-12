using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyController : AIManager,ITargetable
{


    [SerializeField]
    EnemyType enemyType;

    [SerializeField]
    public GameObject target;

    public Player player;

    [SerializeField]
    Transform hitPoint;
    [SerializeField] 
    SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] 
    Material hitMaterial,normalMaterial,deathMaterial;

    //[SerializeField]
    AudioSource DeadSound;

    bool isAlive = true;
   

    public float health;

    float dissolveValue;

    float speed;
    int damage;
    int award;

    Material dissolveMaterial; 

    private void Start()
    {
        normalMaterial = skinnedMeshRenderer.material;

        DeadSound = GetComponent<AudioSource>();
        
    }

    private void Update()
    {

        MoveToTarget();



    }

    private void OnEnable()
    {
        LoadProperties();
    }

    public IEnumerator ChangeMaterial(float timer)
    {
        if (health>0)
        {
            skinnedMeshRenderer.material = hitMaterial;
            yield return new WaitForSeconds(timer);
            skinnedMeshRenderer.material = normalMaterial;
        }
        else
        {
            dissolveMaterial = new Material(deathMaterial); 
            skinnedMeshRenderer.material = dissolveMaterial;
        }

       

    }

    public void ShrukenHitBuy()
    {

        health+=5;

    }
    public void TakeHit(float timer)
    {
        health-=15;

        StartCoroutine(ChangeMaterial(timer));

        if (health <=0)
        {
            GoDie();
        }

        
    }

    public void GoDie()
    {
        if (isAlive)
        {
            WaveSpawner.instance.deadEnemyCount++;

            WaveSpawner.instance.totaldeadEnemyCount++;

            if (WaveSpawner.instance.deadEnemyCount == WaveSpawner.instance.dailyEnemyCount)
            {
                GameManager.instance.day++;
                
                WaveSpawner.instance.startDayButton.SetActive(true);
                
                WaveSpawner.instance.deadEnemyCount = 0;
            }
            float randomPitch = Random.Range(0.8f, 1.2f);

            DeadSound.pitch = randomPitch;
            DeadSound.Play();

            player.GetComponentInChildren<EnemyFinder>().enemiesInRange.Remove(gameObject);

            animator.SetBool("isDeath", true);

            isAlive = false;

            agent.isStopped = true;

            agent.ResetPath();

            GetComponent<Rigidbody>().useGravity = false;

            GetComponent<BoxCollider>().isTrigger = true;

            GetComponent<NavMeshAgent>().enabled = false;
        }

       

    }

    public void StartDissolve()
    {

        DOTween.To(() => 0.0f, x => dissolveValue = x, 0.5f, 2).SetEase(Ease.Linear).OnUpdate(() =>
        {
            dissolveMaterial.SetFloat("_Dissolve", dissolveValue);
        }).OnComplete(() => DestroyEnemy());
    }

    

    public void DestroyEnemy()
    {
        GameManager.instance.money += award;
        PoolManager.instance.Push(gameObject,enemyType.enemyName);
       
       

    }

    public void MoveToTarget()
    {
        if (isAlive)
        {

            //if (GoToWall(target)) 1
            //{
            //    CheckArrive(1.5f);
            //}

            
            {
                GoToTarget(player.gameObject);

                CheckArrive(2.2f);
            }
        }
       

    }


   

    public void LoadProperties()
    {
        isAlive = true;
        animator.SetBool("isDeath", false);
        skinnedMeshRenderer.material = normalMaterial;
        health = enemyType.health;
        speed = enemyType.speed;
        damage = enemyType.damage;
        award = enemyType.award;
        if (GameManager.instance!=null)
        {
            player = GameManager.instance.player;

        }

        GetComponent<Rigidbody>().useGravity = true;

        GetComponent<BoxCollider>().isTrigger = false;

        GetComponent<NavMeshAgent>().enabled = true;
    }

    public void Attack()
    {
        //if (Wall.instance.currentWallHealth>0)
        //{
        //    GameEvents.instance.HitWall(damage);
        //}

        
        {
            player.health -= damage;
        }
    }

    public Transform TargetPoint()
    {
        return hitPoint;
    }
}
