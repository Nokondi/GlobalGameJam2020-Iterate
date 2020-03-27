using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateIterator : MonoBehaviour
{
    public List<GameObject> templateHeads;
    public List<GameObject> templateBodies;
    public string seed = "";
    public Transform headLocation;
    public Transform bodyLocation;
    public Transform playerRoot;
    public Transform playerCamera;
    public Transform holdingPos;

    public InputField seedInput;
    public Button beginGame;

    GameObject generatedHead;
    GameObject generatedBody;
    Material generatedMaterial;

    // Start is called before the first frame update
    void Start()
    {
        //Generate(seed);
        KoanManager.Instance.TriggerStartup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate()
    {
        /*
         * Iterators have two generated parts- their head and body. Each
         *  are generated with random shapes and colors.
         * The player camera should always be able to see the body
         *  (but not the head) as well as their hands/grabbers/whatever.
         * */
        beginGame.interactable = true;

        var stringSeed = seedInput.text;

        playerCamera.transform.parent = playerRoot;
        Destroy(generatedHead);
        Destroy(generatedBody);

        //Debug.Log(stringSeed);
        stringSeed.GetHashCode();
        Random.InitState(stringSeed.GetHashCode());
        /*Debug.Log(Random.Range(
            0,
            Mathf.FloorToInt(Mathf.Pow(2, 16))
        ));*/

        // VERY necessary
        Random.Range(
            0,
            Mathf.FloorToInt(Mathf.Pow(2, 16))
        );

        // Generate head
        // Pick head shape
        // Pick x/y/z scale

        int headChoice = Random.Range(0, templateHeads.Count);
        Vector3 headScale = new Vector3(
            Random.Range(0.5f, 1.5f),
            Random.Range(0.5f, 1.5f),
            Random.Range(0.5f, 1.5f)
            );
        Color colorChoice = Random.ColorHSV();

        generatedHead = Instantiate(templateHeads[headChoice], headLocation.transform.position, Quaternion.identity);
        generatedHead.GetComponent<Renderer>().material.color = colorChoice;
        generatedHead.transform.localScale = headScale;
        generatedHead.transform.parent = playerRoot;
        playerCamera.transform.parent = generatedHead.transform;
        playerCamera.localPosition = Vector3.zero;

        // Generate body
        // Pick body color
        // Pick x/y/z scale

        int bodyChoice = Random.Range(0, templateBodies.Count);
        Vector3 bodyScale = new Vector3(
            Random.Range(0.5f, 1.5f),
            Random.Range(0.5f, 1.5f),
            Random.Range(0.5f, 1.5f)
            );
        generatedBody = Instantiate(templateBodies[bodyChoice], bodyLocation.position, Quaternion.identity);
        generatedBody.GetComponent<Renderer>().material.color = colorChoice;
        generatedBody.transform.localScale = bodyScale;
        generatedBody.transform.parent = playerRoot;
        holdingPos.transform.parent = generatedBody.transform;
    }
}
