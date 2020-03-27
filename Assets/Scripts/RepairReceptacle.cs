using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairReceptacle : MonoBehaviour
{
    public string receptacleType;
    public Transform receptaclePos;
    bool repaired = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetReceptacleType()
    {
        return receptacleType;
    }

    public void PlacePart(GameObject part)
    {
        Debug.Log("Placing part");
        Debug.Log(part.name);
        SpawnManager.Instance.RepairOccurred(this.gameObject, part);
        part.transform.parent = receptaclePos;
        part.transform.position = receptaclePos.position;
        part.transform.rotation = receptaclePos.rotation;
        part.GetComponent<RepairPart>().Repair();
        part.GetComponent<RepairPart>().enabled = false;
        Repair();
        this.enabled = false;
    }

    public void Repair()
    {
        repaired = true;
    }

    public bool GetRepaired()
    {
        return repaired;
    }
}
