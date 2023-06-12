using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TriggerManager : MonoBehaviour
{
    [SerializeField] Transform resourcesSpawnPoint;

    CutTree tree;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject gun, axe;

    [SerializeField]
    ResourceManager resourceManager;

    public void Cut()
    {
        if (tree!=null)
        {
            tree.Cut();

        }
    }

    IEnumerator LoadWoodSawMachine(Transform other)
    {
      
        
        while (resourceManager.resourcesList.Count > 0)
        {
            yield return new WaitForSeconds(0.325f);

            resourceManager.resourcesList[resourceManager.resourcesList.Count - 1].transform.parent = other;

            other.GetComponent<WoodCutterMachine>().LoadMachine(resourceManager.resourcesList[resourceManager.resourcesList.Count - 1]);

            resourceManager.resourcesList.RemoveAt(resourceManager.resourcesList.Count - 1);

            Debug.Log(resourceManager.resourcesList.Count);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IBuyable>()!=null)
        {
            other.GetComponent<IBuyable>().OnBuyArea(true, resourcesSpawnPoint.position);

        }

        if (other.GetComponent<RepairArea>() != null)
        {
            other.GetComponent<RepairArea>().Repair(resourcesSpawnPoint.position, true);
        }


        if (other.GetComponent<Fence>() != null)
        {
            other.transform.parent = resourceManager.logPoints[0];
            other.GetComponent<Fence>().MovePlayer(0.3f, resourceManager.logPoints[0].position);
        }



        if (other.GetComponent<CutTree>())
        {
            tree = other.GetComponent<CutTree>();

            animator.SetBool("isCutting",true);

            axe.SetActive(true);
            gun.SetActive(false);

        }

        if (other.GetComponent<WoodCutterMachine>())
        {
            if (resourceManager.resourcesList.Count>0)
            {
                resourceManager.resourcesList[resourceManager.resourcesList.Count-1].transform.parent = other.transform;
                
                other.GetComponent<WoodCutterMachine>().LoadMachine(resourceManager.resourcesList[resourceManager.resourcesList.Count - 1]);

                resourceManager.resourcesList.RemoveAt(resourceManager.resourcesList.Count - 1);

                StartCoroutine(LoadWoodSawMachine(other.transform));
            }

        }





        if (other.GetComponent<Log>())
        {
            if (resourceManager.resourcesList.Count < resourceManager.stackLimit)
            {
                Destroy(other.GetComponent<Rigidbody>());

                Destroy(other.GetComponent<BoxCollider>());

                Destroy(other.GetComponent<Log>());

                Destroy(other.GetComponent<BoxCollider>());

                other.transform.parent = resourceManager.logPoints[resourceManager.resourcesList.Count];

                other.transform.rotation = resourceManager.logPoints[resourceManager.resourcesList.Count].rotation;

                other.transform.DOLocalMove(new Vector3(0, 0, 0), 0.2f);

                //other.GetComponent<Log>().MovePlayer(0.2f, resourceManager.logPoints[resourceManager.resourcesList.Count].position);
                
                resourceManager.resourcesList.Add(other.gameObject);
            }
            
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IBuyable>() != null)
        {
            other.GetComponent<IBuyable>().OnBuyArea(false, resourcesSpawnPoint.position);

        }

        if (other.GetComponent<RepairArea>() != null)
        {
            other.GetComponent<RepairArea>().Repair(resourcesSpawnPoint.position, false);
        }

        if (other.GetComponent<CutTree>())
        {
            tree = other.GetComponent<CutTree>();

            animator.SetBool("isCutting", false);


            axe.SetActive(false);
            gun.SetActive(true);


        }

    }
}
