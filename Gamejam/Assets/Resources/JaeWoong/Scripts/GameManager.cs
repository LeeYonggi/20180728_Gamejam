using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject buildingPrefab;

    public List<Building> buildingList = new List<Building>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }
    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            buildingList.Add(MakeBuilding(i));
            //buildingList[i].transform.position = new Vector3(0, 6.7f + i * 1.7f, 0);
        }
    }
    void Update()
    {

    }

    public void AddBuilding(int num)
    {
        buildingList.RemoveAt(0);
        buildingList.Add(MakeLastBuilding(num));
    }

    public Building MakeBuilding(int num)
    {
        GameObject g = Instantiate<GameObject>(buildingPrefab);

        Building b = g.GetComponent<Building>();

        b.gravityScale = -10f;
        b.transform.position = new Vector3(0, 6.7f + num * 1.7f, 0);
        b.MakePattern();
        b.PatternToColor(b.notePattern[0]);
       
        return b;
    }
    public Building MakeLastBuilding(int num)
    {
        GameObject g = Instantiate<GameObject>(buildingPrefab);

        Building b = g.GetComponent<Building>();

        b.gravityScale = -10f;
        b.transform.position = new Vector3(0, buildingList[num].transform.position.y + 2f, 0);
        b.MakePattern();
        b.PatternToColor(b.notePattern[0]);

        return b;
    }
}
