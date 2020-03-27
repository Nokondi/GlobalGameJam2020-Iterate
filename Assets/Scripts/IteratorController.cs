using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;

public class IteratorController : MonoBehaviour
{
    public float maxZRotation;
    public float minZRotation;
    public float turnSpeed;
    public float moveSpeed;
    public bool playing = false;
    public Transform holdingPos;
    public Text reticleText;

    Transform head;
    GameObject heldObject = null;
    Rigidbody rb;

    float countdownRate = 0.01f;
    float throwStrength = 0.0f;
    float throwRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObject != null) PartOperation();
    }

    void FixedUpdate()
    {
        if (playing == true)
        {
            PlayerLook();
            PlayerMove();
        }
    }

    public void StartCycle()
    {
        playing = true;
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(CycleCountdown(20f));
        KoanManager.Instance.TriggerBegin();
        var spawn = IterationManager.Instance.GetPlayerSpawn();
        transform.position = spawn.position;
        SpawnManager.Instance.TriggerNewPartSpawn();
    }

    public void EndCycle()
    {
        if (heldObject != null)
        {
            var objRB = heldObject.GetComponent<Rigidbody>();
            var transColor = heldObject.GetComponent<Renderer>().material.color;
            transColor.a = 1f;
            heldObject.GetComponent<Renderer>().material.color = transColor;
            heldObject.transform.parent = null;
            objRB.constraints = RigidbodyConstraints.None;
            heldObject = null;
        }
        playing = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Destroy(this.gameObject);
        IterationManager.Instance.Ending(
            transform.GetChild(0).rotation,
            transform.GetChild(0).position);
        KoanManager.Instance.TriggerDeath();
    }

    IEnumerator CycleCountdown(float timeRemaining)
    {
        var tickedTime = timeRemaining;
        yield return new WaitForSecondsRealtime(countdownRate);
        tickedTime -= countdownRate;

        if (tickedTime <= 0)
        {
            Debug.Log("Death");
            IterationManager.Instance.SetCountdownText(0);
            EndCycle();
        }
        else
        {
            IterationManager.Instance.SetCountdownText(tickedTime);
            StartCoroutine(CycleCountdown(tickedTime));
        }

    }

    void PlayerLook()
    {
        var currentBodyRotation = transform.localEulerAngles;
        var currentHeadRotation = transform.GetChild(0).localEulerAngles;
        currentBodyRotation.y += Input.GetAxis("Mouse X") * turnSpeed;
        transform.localEulerAngles = currentBodyRotation;
        float nextTurnFrame = currentHeadRotation.x - (Input.GetAxis("Mouse Y") * turnSpeed);
        if (nextTurnFrame > 180) nextTurnFrame = 360 - nextTurnFrame;
        else if (nextTurnFrame < -180) nextTurnFrame = 360 + nextTurnFrame;
        if (nextTurnFrame < maxZRotation && nextTurnFrame > minZRotation)
        {
            currentHeadRotation.x -= Input.GetAxis("Mouse Y") * turnSpeed;
            transform.GetChild(0).localEulerAngles = currentHeadRotation;
        }
    }

    void PlayerMove()
    {
        rb.velocity += transform.right * moveSpeed * Input.GetAxis("Horizontal");
        rb.velocity += transform.forward * moveSpeed * Input.GetAxis("Vertical");
        rb.velocity += transform.up * moveSpeed * (Input.GetAxis("Jump") - Input.GetAxis("Descend"));
    }

    public void DetectedPart(GameObject obj)
    {
        var objRB = obj.GetComponent<Rigidbody>();
        
        if (Input.GetMouseButton(0) && heldObject == null)
        {
            obj.transform.parent = holdingPos;
            obj.transform.position = obj.transform.parent.position;
            obj.transform.localRotation = Quaternion.identity;
            objRB.constraints = RigidbodyConstraints.FreezeAll;
            heldObject = obj;
            var transColor = heldObject.GetComponent<Renderer>().material.color;
            transColor.a = 0.5f;
            heldObject.GetComponent<Renderer>().material.color = transColor;
        }
    }

    public void DetectReceptacle(GameObject obj)
    {
        if (Input.GetMouseButton(0) && heldObject != null)
        {
            if (obj.GetComponent<RepairReceptacle>().GetReceptacleType()
                == heldObject.GetComponent<RepairPart>().GetPartType())
            {
                var transColor = heldObject.GetComponent<Renderer>().material.color;
                transColor.a = 1f;
                heldObject.GetComponent<Renderer>().material.color = transColor;

                obj.GetComponent<RepairReceptacle>().PlacePart(heldObject);
                heldObject = null;
            }
        }
    }

    public void PartOperation()
    {
        var objRB = heldObject.GetComponent<Rigidbody>();

        if (Input.GetMouseButtonUp(1))
        {
            heldObject.transform.parent = null;
            objRB.constraints = RigidbodyConstraints.None;
            objRB.velocity = transform.GetChild(0).forward * throwStrength;
            var transColor = heldObject.GetComponent<Renderer>().material.color;
            transColor.a = 1f;
            heldObject.GetComponent<Renderer>().material.color = transColor;
            heldObject = null;
            throwStrength = 0f;
        }

        else if (Input.GetMouseButton(1))
        {
            IncreaseThrowStrength();
        }
    }

    void IncreaseThrowStrength()
    {
        if (throwStrength < 30) throwStrength += throwRate;
    }
}
