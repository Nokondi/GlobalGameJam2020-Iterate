using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPart : MonoBehaviour
{
    public string partType;
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

    public string GetPartType()
    {
        return partType;
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
