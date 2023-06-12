using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Wall : MonoBehaviour
{
    public static Wall instance;

    [SerializeField]
    List<FenceObject> fenceObjects = new List<FenceObject>();

    List<FenceObject> brokenfenceObject = new List<FenceObject>();
    
    [SerializeField]
    List<Transform> fenceTransforms;

    [SerializeField]
    List<GameObject> fences;

    [SerializeField] GameObject wall;

    int fenceCount;

    public float maxWallHealth;
    public float currentWallHealth;

    Tweener wallTween;

    private void Start()
    {
        currentWallHealth = maxWallHealth;
        fenceCount = fenceObjects.Count;




    }
    private void OnEnable()
    {
        GameEvents.OnWallTakeHit += TakeDamage;
        GameEvents.OnRepairWall += RepairFence;
        instance = this;
    }

    private void OnDisable()
    {
        GameEvents.OnWallTakeHit -= TakeDamage;
        GameEvents.OnRepairWall -= RepairFence;

    }

    void TakeDamage(int damage)
    {
        currentWallHealth -= damage;

        

        for (int i = 0; i < (maxWallHealth - currentWallHealth) / ((float)maxWallHealth / fenceCount) - brokenfenceObject.Count; i++)
        {
            BreakFence();
        }

        if (currentWallHealth<=0)
        {
            for (int i = 0; i < fenceObjects.Count; i++)
            {
                Destroy(fenceObjects[i].fenceGameObject);
            }

            fenceObjects.Clear();
        }

    }

   

    void HealWall(int heal)
    {
        currentWallHealth += heal;

        if (currentWallHealth > maxWallHealth)
        {
            currentWallHealth = maxWallHealth;
        }
    }


    void RepairFence(Vector3 spawnPoint)
    {
        if (brokenfenceObject.Count>0)
        {
            int randomFence = Random.Range(0, brokenfenceObject.Count);
            GameObject newFence = Instantiate(brokenfenceObject[randomFence].fenceGameObject, spawnPoint, Quaternion.identity,transform);
            
            newFence.transform.DOShakePosition(0.924f, 1, 10, 90);
            newFence.transform.DOMove(brokenfenceObject[randomFence].fenceGameObject.transform.position, 0.925f);
            Destroy(brokenfenceObject[randomFence].fenceGameObject);
            brokenfenceObject.RemoveAt(randomFence);
            fenceObjects.Add(new FenceObject(newFence));

            HealWall((int)maxWallHealth / fenceObjects.Count);
            ResourceManager.instance.fenceCount--;

        }
        

       
    }



    void BreakFence()
    {
        int randomFence = Random.Range(0, fenceObjects.Count);
      

        if (fenceObjects[randomFence].isBroken)
        {
            BreakFence();
        }

        else
        {
            fenceObjects[randomFence].isBroken = true;
            fenceObjects[randomFence].fenceGameObject.transform.DORotate(new Vector3(-85, 0, 0), 0.2f);
            brokenfenceObject.Add(fenceObjects[randomFence]);
            fenceObjects.RemoveAt(randomFence);

          
            if (!wallTween.IsActive())
            {
                wallTween = wall.transform.DOShakeScale(0.125f, 0.0525f);
                wall.transform.DOShakeRotation(0.125f, 0.0525f);
            }
           


        }

    }


}



[System.Serializable]
public class FenceObject
{
    public GameObject fenceGameObject;
    public bool isBroken;

   public FenceObject()
    {

    }
    public FenceObject(GameObject fenceGameObject)
    {
        this.fenceGameObject = fenceGameObject;
        this.isBroken = false;

    }
}


