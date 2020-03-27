using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public Transform RLocationParent;
    public Transform PLocationParent;
    List<Transform> receptacleLocations = new List<Transform>();
    List<Transform> partLocations = new List<Transform>();
    public List<GameObject> receptacles;
    public List<GameObject> parts;
    public int NParts;

    List<GameObject> existingReceptacles = new List<GameObject>();
    List<GameObject> existingParts = new List<GameObject>();

    public GameObject testRecep;
    public GameObject testPart;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogWarning("There are more than one SpawnManagers" +
                " in the scene. Destroying this...");
        }
        else Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateReceptacles();
    }

    void Update()
    {
        //if (Input.GetKeyDown("r")) RepairOccurred(testRecep, testPart);
        //if (Input.GetKeyDown("t")) TriggerNewPartSpawn();
    }

    void GenerateReceptacles()
    {
        // Instantiate receptacle in each location
        //  with corresponding rotation
        for (var i = 0; i < RLocationParent.childCount; i++)
        {
            receptacleLocations.Add(RLocationParent.GetChild(i));
        }
            for (var i = 0; i < receptacleLocations.Count; i++)
        {
            var r = Instantiate(receptacles[Random.Range(0, receptacles.Count)]);
            r.transform.position = receptacleLocations[i].position;
            r.transform.rotation = receptacleLocations[i].rotation;
            existingReceptacles.Add(r);
        }
    }

    public void TriggerNewPartSpawn()
    {
        // Make list of types of receptacles that exist
        // Spawn up to 3 parts (until part list is 3)
        // Add them to the existing part list
        List<string> recTypes = new List<string>();
        for (var j = 0; j < PLocationParent.childCount; j++)
        {
            partLocations.Add(PLocationParent.GetChild(j));
        }

        foreach (GameObject r in existingReceptacles)
        {
            recTypes.Add(r.GetComponent<RepairReceptacle>().GetReceptacleType());
        }
        List<string> randomList = new List<string>();
        while (recTypes.Count > 0)
        {
            var ind = Random.Range(0, recTypes.Count);
            randomList.Add(recTypes[ind]);
            recTypes.RemoveAt(ind);
        }
        var i = 0;
        while (existingParts.Count < NParts)
        {
            var r = randomList[i];
            var spawnedPart = GetPartPrefab(r);
            if (spawnedPart != null)
            {
                var p = Instantiate(spawnedPart);
                p.transform.position = partLocations[Random.Range(0, partLocations.Count)].position;
                existingParts.Add(p);
            }
            else Debug.LogError("Unknown part type " + r);
            i++;
        }
    }

    public void RepairOccurred(GameObject r, GameObject p)
    {
        if (existingReceptacles.Contains(r))
            existingReceptacles.Remove(r);
        else Debug.LogError("Receptacle not found");
        if (existingParts.Contains(p))
            existingParts.Remove(p);
        else Debug.LogError("Part not found");
    }

    GameObject GetPartPrefab(string type)
    {
        GameObject returnType = null;
        foreach(GameObject p in parts)
        {
            var partType = p.GetComponent<RepairPart>().GetPartType();
            if (partType == type) returnType = p;
        }
        return returnType;
    }
}
