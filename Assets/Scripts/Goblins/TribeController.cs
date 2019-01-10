using System.Collections.Generic;
using UnityEngine;

public class TribeController : MonoBehaviour
{
    public GameObject BossGoblin;
    public GameObject Goblin;

    public GameObject ScoutPoints;
    public GameObject BasePoints;
    public GameObject BossGoblinPoint;

    public List<GoblinClass> Classes = new List<GoblinClass>();

    private List<GameObject> ListOfScoutPoints = new List<GameObject>(); 

    private void Awake()
    {
        var stackOfScoutPoints = new Stack<GameObject>();

        var stackOfBasePoints = new Stack<GameObject>();

        foreach (Transform child in this.ScoutPoints.transform)
        {
            stackOfScoutPoints.Push(child.gameObject);
            ListOfScoutPoints.Add(child.gameObject);
        }

        foreach (Transform child in this.BasePoints.transform)
        {
            stackOfBasePoints.Push(child.gameObject);
        }

        for (int i = 0; i < Classes.Count; i++)
        {
            if (Classes[i].ToString() == "Boss")
            {
                var point = this.BossGoblinPoint;
                var vector = new Vector3(point.transform.localPosition.x, point.transform.localPosition.y, point.transform.localPosition.z);

                //Instantiate(this.BossGoblin, vector, Quaternion.identity, point.transform);
            }
            else if (Classes[i].ToString() == "Goblin")
            {
                //Promotion
                if (stackOfScoutPoints.Count != 0)
                {
                    var point = stackOfScoutPoints.Pop();
                    this.SpawnGoblin(point, GoblinClass.GoblinScout);
                }
                else if(stackOfBasePoints.Count != 0)
                {
                    var point = stackOfBasePoints.Pop();
                    this.SpawnGoblin(point, Classes[i]);
                }
                //else
                //{
                //    Instantiate(Goblin);
                //}
            }
        }
    }

    private void SpawnGoblin(GameObject point, GoblinClass goblinClass)
    {
        var vector = new Vector3(point.transform.localPosition.x, point.transform.localPosition.y, point.transform.localPosition.z);

        var currentGoblin = Instantiate(this.Goblin, vector, Quaternion.identity, point.transform);

        point.GetComponent<Points>().CurrentGoblin = currentGoblin;

        var config = currentGoblin.GetComponent<GoblinController>();
        config.Point = point;
        config.GoblinClass = goblinClass;
    }
}

public enum GoblinClass
{
    Boss = 1,
    Goblin = 2,
    GoblinScout = 3,
    Commander = 4,
};

