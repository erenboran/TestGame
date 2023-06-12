using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public List<Pool> pools = new List<Pool>();

    public List<PoolObjects> poolObjects = new List<PoolObjects>();

    private void Awake()
    {

        instance = this;

        for (int i = 0; i < pools.Count; i++)
        {

            poolObjects.Add(new PoolObjects());


            GameObject newObject = new GameObject();

            newObject.transform.parent = transform;
            newObject.name = pools[i].name;

            for (int j = 0; j < pools[i].spawnCount; j++)
            {
                poolObjects[i].poolObjects.Add(Instantiate(pools[i].objectPrefab, newObject.transform));
                poolObjects[i].poolObjects[poolObjects[i].poolObjects.Count - 1].SetActive(false);

            }

        }

    }

    public CheckPool ControlPool(string poolName)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i].name.Equals(poolName))
            {


                return new CheckPool(true,i);

            }

        }

        return new CheckPool(false, 999);
    }

    public CheckPool ControlPool(int id)
    {
        if (pools.Count > id)
        {
            return new CheckPool(true,id);
        }

        return new CheckPool(false, 999);
    }


    public GameObject Pull(int id, Vector3 position, Quaternion rotation)
    {
        CheckPool checkPool = ControlPool(id);

        if (checkPool.isTherePool && poolObjects[checkPool.poolID].poolObjects.Count>0)
        {
            
            GameObject returnObject = poolObjects[checkPool.poolID].poolObjects[0];

            returnObject.transform.position = position;
            returnObject.transform.rotation = rotation;

            returnObject.SetActive(true);

            poolObjects[checkPool.poolID].poolObjects.RemoveAt(0);

            return returnObject;

        }


        return null;
    }



    public GameObject Pull(int id, Transform spawnPoint)
    {
        CheckPool checkPool = ControlPool(id);

        if (checkPool.isTherePool && poolObjects[checkPool.poolID].poolObjects.Count > 0)
        {
            GameObject returnObject = poolObjects[checkPool.poolID].poolObjects[0];

            returnObject.transform.position = spawnPoint.position;
            returnObject.transform.rotation = spawnPoint.rotation;

            returnObject.SetActive(true);

            poolObjects[checkPool.poolID].poolObjects.RemoveAt(0);

            return returnObject;

        }


        return null;
    }




    public GameObject Pull(string poolName,Vector3 position, Quaternion rotation)
    {
        CheckPool checkPool = ControlPool(poolName);

        if (checkPool.isTherePool && poolObjects[checkPool.poolID].poolObjects.Count > 0)
        {
            GameObject returnObject = poolObjects[checkPool.poolID].poolObjects[0];

            returnObject.transform.position = position;
            returnObject.transform.rotation = rotation;

            returnObject.SetActive(true);

            poolObjects[checkPool.poolID].poolObjects.RemoveAt(0);

            return returnObject;

        }


        return null;
    }



    public GameObject Pull(string poolName, Transform spawnPoint)
    {
        CheckPool checkPool = ControlPool(poolName);

        if (checkPool.isTherePool && poolObjects[checkPool.poolID].poolObjects.Count > 0)
        {
            GameObject returnObject = poolObjects[checkPool.poolID].poolObjects[0];

            returnObject.transform.position = spawnPoint.position;
            returnObject.transform.rotation = spawnPoint.rotation;

            returnObject.SetActive(true);

            poolObjects[checkPool.poolID].poolObjects.RemoveAt(0);

            return returnObject;

        }


        return null;
    }

    public void Push(GameObject poolObject,string poolName)
    {
        CheckPool checkPool = ControlPool(poolName);


        if (checkPool.isTherePool)
        {
            poolObject.SetActive(false);
            poolObjects[checkPool.poolID].poolObjects.Add(poolObject);
        }


    }

    public void Push(GameObject poolObject, int poolID)
    {
        CheckPool checkPool = ControlPool(poolID);

        if (checkPool.isTherePool)
        {
            poolObject.SetActive(false);
            poolObjects[checkPool.poolID].poolObjects.Add(poolObject);
        }


    }











    public bool FindPool(int id)
    {
        for (int i = 0; i < pools.Count; i++)
        {
            if (poolObjects[i].poolObjects.Count > 0)
            {
                return true;

            }

        }

        return false;

    }






}

[System.Serializable]
public class Pool
{
    public string name;
    public int spawnCount;
    public GameObject objectPrefab;


}
[System.Serializable]

public class PoolObjects
{
   public List<GameObject> poolObjects = new List<GameObject>();

    public PoolObjects()
    {

    }
}

[System.Serializable]
public class CheckPool
{
    public bool isTherePool;
    public int poolID;

   public CheckPool(bool isTherePool,int poolID)
    {
        this.isTherePool = isTherePool;
        this.poolID = poolID;

    }
}
