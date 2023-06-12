using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class MiningMachine : MonoBehaviour
{
    [SerializeField]
    Transform startPoint,unloadPoint;

    [SerializeField]
    Transform[] path;

    [SerializeField]
    NavMeshAgent agent;

    [SerializeField]
    float remaningDistance;

    [SerializeField]
    int index;

    [SerializeField]
    int mineCount;

    [SerializeField]
    int tankCapacity;

    [SerializeField]
    float mineTimer;

    bool isUnloading;

    [SerializeField]
    Text tankCapacityText;

    private void Update()
    {
        if (agent.remainingDistance < remaningDistance)
        {
            if (!isUnloading)
            {
                MoveNextPoint();

            }

        }
    }

    public void TakePoints(Transform loadPoint,Transform startPoint,Transform[] path)
    {
        this.unloadPoint = loadPoint;
        this.startPoint = startPoint;
        this.path = path;
    }

   

    void MoveNextPoint()
    {

       

        if (index==0)
        {
            StartCoroutine(Mine());
        }

        if (index<path.Length && mineCount < tankCapacity)
        {
            agent.SetDestination(path[index].position);
            
        }

        else
        {
            agent.SetDestination(unloadPoint.position);
            index = 0;
        }

        index++;

    }

    IEnumerator EpmtyTheTank()
    {
        while (mineCount!=0 )
        {
            isUnloading = true;
            mineCount--;
            tankCapacityText.text = mineCount.ToString();

            ResourceManager.instance.diamondCount++;

            yield return new WaitForSeconds(0.1f);

        }
        agent.SetDestination(startPoint.position);
        index = 0;
        isUnloading = false;


    }


    IEnumerator Mine()
    {
        
        while (mineCount<tankCapacity && !isUnloading)
        {
            yield return new WaitForSeconds(mineTimer);
            
            mineCount++;
            tankCapacityText.text = mineCount.ToString();
        }

        agent.SetDestination(unloadPoint.position);
        


       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<UnloadMine>() !=null)
        {
            StartCoroutine(EpmtyTheTank());
        }
    }

}
