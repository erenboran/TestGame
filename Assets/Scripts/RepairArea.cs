using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RepairArea : MonoBehaviour
{
    [SerializeField]
    Image frameBar;


    bool isPlayerHere;
    IEnumerator FillTheBar() 
    {
        while (isPlayerHere && frameBar.fillAmount<1)
        {
            frameBar.fillAmount += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

       
    }

    IEnumerator RepairWall(Vector3 spawnPoint)
    {
        while (isPlayerHere && ResourceManager.instance.fenceCount>0)
        {
            GameEvents.instance.RepairWall(spawnPoint);
            
           yield return new WaitForSeconds(0.1f);
        }
    }


    public void Repair(Vector3 spawnPoint,bool isPlayerHere)
    {
        this.isPlayerHere = isPlayerHere;

        if (isPlayerHere)
        {
            StartCoroutine(RepairWall(spawnPoint));
        }
    }
}
