using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KoanManager : MonoBehaviour
{
    public List<string> startupKoans;
    public List<string> beginKoans;
    public List<string> deathSuccessKoans;
    public List<string> deathNeutralKoans;

    public Text TitleFlavor;
    public Text StartText;
    public Text DeathText;

    public static KoanManager Instance { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogWarning("There are more than one KoanManagers" +
                " in the scene. Destroying this...");
        }
        else Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerStartup()
    {
        TitleFlavor.gameObject.SetActive(true);
        TitleFlavor.text = startupKoans[Random.Range(0, startupKoans.Count)];
        StartText.gameObject.SetActive(false);
        DeathText.gameObject.SetActive(false);
    }

    public void TriggerBegin()
    {
        StartText.gameObject.SetActive(true);
        StartText.text = beginKoans[Random.Range(0, beginKoans.Count)];
        TitleFlavor.gameObject.SetActive(false);
        DeathText.gameObject.SetActive(false);
    }

    public void TriggerDeath(bool success = false)
    {
        DeathText.gameObject.SetActive(true);
        if (success == true)
        {
            DeathText.text = deathSuccessKoans[Random.Range(0, deathSuccessKoans.Count)];
        }
        else
        {
            DeathText.text = deathNeutralKoans[Random.Range(0, deathNeutralKoans.Count)];
        }
        TitleFlavor.gameObject.SetActive(false);
        StartText.gameObject.SetActive(false);
    }
}
