using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class OutlineObject : MonoBehaviour
{
    Outline ol;
    // Start is called before the first frame update
    void Start()
    {
        ol = GetComponent<Outline>();
        ol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerInteraction") ol.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteraction") ol.enabled = false;
    }
}
