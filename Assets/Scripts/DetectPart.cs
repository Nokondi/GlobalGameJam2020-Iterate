using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DetectPart : MonoBehaviour
{
    public Text reticleText;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Receptacle")
        {
            if (other.GetComponent<RepairReceptacle>().GetRepaired() == false)
            {
                transform.parent.parent.GetComponent<IteratorController>()
                .DetectReceptacle(other.gameObject);
                reticleText.transform.parent.gameObject.SetActive(true);
                reticleText.text = "Deposit " +
                    other.GetComponent<RepairReceptacle>().GetReceptacleType()
                    + " here";
            }
        }
        else if (other.tag == "Part")
        {
            if (other.GetComponent<RepairPart>().GetRepaired() == false)
            {
                transform.parent.parent.GetComponent<IteratorController>()
                .DetectedPart(other.gameObject);
                reticleText.transform.parent.gameObject.SetActive(true);
                reticleText.text = other.GetComponent<RepairPart>()
                    .GetPartType();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        reticleText.transform.parent.gameObject.SetActive(false);
    }

}
