using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager instance;

    public Transform[] logPoints;

    public List<GameObject> resourcesList = new List<GameObject>();

    public int stackLimit;

    public int fenceCount;

    public int diamondCount;

    public int moneyCount;

    private void Awake()
    {
        instance = this;
    }

}
