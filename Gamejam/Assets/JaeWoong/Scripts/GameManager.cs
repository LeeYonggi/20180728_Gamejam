using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Dictionary<string, List<int>> notePattern = new Dictionary<string, List<int>>();

    public static GameManager instance;
    public GameObject building;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        StartCoroutine("MakeBuilding");
    }
    
    void Update()
    {

    }

    IEnumerator MakeBuilding()
    {
        GameObject g = Instantiate<GameObject>(building);
        Building b = g.GetComponent<Building>();

        b.gravityScale = -10f;
        b.maxPatternNum = 4;

        b.notePattern = new int[b.maxPatternNum];
        b.notePattern[0] = 1;
        b.notePattern[1] = 2;
        b.notePattern[2] = 3;
        b.notePattern[3] = 4;
        
        PlayerNotePad.instance.buildingInfo = b;
        yield return null;
        
    }
}
