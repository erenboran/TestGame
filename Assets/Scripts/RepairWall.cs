using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RepairWall : MonoBehaviour
{
    [SerializeField]
    GameObject[] fencePrefabs;

    [SerializeField]
    List<GameObject> spawnedFences = new List<GameObject>();

    [SerializeField]
    Transform[] targetPoints;

    [SerializeField]
    Transform[] fenceSpawnPoints;

    [SerializeField]
    List<ObjectToMove> objectToMoves = new List<ObjectToMove>();

    private void Start()
    {
        SpawnFences();

        for (int i = 0; i < spawnedFences.Count; i++)
        {
            objectToMoves.Add( new ObjectToMove(spawnedFences[i],i));
        }

        //StartCoroutine(StartMove(0));
    }

    //IEnumerator StartMove(int i)
    //{

    //    DoMove(objectToMoves[i]);



    //    yield return new WaitForSeconds(0.255f);

    //    if (i<objectToMoves.Count-1)
    //    {
    //        i++;
    //        StartCoroutine(StartMove(i));

    //    }
    //}

    //public void DoMove(ObjectToMove objectToMove)
    //{
    //    if (objectToMove.states == ObjectToMove.State.State0)
    //    {
    //        objectToMove.objectPrefab.SetActive(true);
    //    }

    //    if (objectToMove.states != ObjectToMove.State.State2)
    //    {
    //        objectToMove.objectPrefab.transform.DOMove(fenceSpawnPoints[(int)objectToMove.states].position, 0.2f).OnComplete(() => DoMove(objectToMove));

    //        objectToMove.states = (ObjectToMove.State)(int)objectToMove.states + 1;


    //    }

    //    else if (objectToMove.states == ObjectToMove.State.State2)
    //    {

    //        objectToMove.objectPrefab.transform.DOMove(targetPoints[objectToMove.index].position, 0.2f);

    //    }


    //}




    public void SpawnFences()
    {
        for (int i = 0; i < fencePrefabs.Length; i++)
        {
            spawnedFences.Add(Instantiate(fencePrefabs[i],fenceSpawnPoints[0].position, fenceSpawnPoints[0].rotation));
            
            spawnedFences[spawnedFences.Count-1].SetActive(false); 
        }

    }

   

   



}
[System.Serializable]
public class ObjectToMove
{
    public enum State { State0 = 0, State1 = 1, State2 = 2, State = 3 };

    public GameObject objectPrefab;
    public State states;
    public int index;

    public ObjectToMove(GameObject objectPrefab, int index)
    {
        this.objectPrefab = objectPrefab;
        this.index = index;
        this.states = State.State0;

    }

}

