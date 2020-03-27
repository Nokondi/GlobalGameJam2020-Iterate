using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance { get; private set; }

    public SceneTransitionScript transition;
    public Text countdownText;
    public Camera mainCamera;
    public Transform playerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogWarning("There are more than one IterationManagers" +
                " in the scene. Destroying this...");
        }
        else Instance = this;

        IteratorController currentIterator = 
            GameObject.FindGameObjectWithTag("Player")
            .GetComponent<IteratorController>();
        currentIterator.StartCycle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCountdownText(float timeRemaining)
    {
        var seconds = Mathf.Floor(timeRemaining);
        var milliseconds = (timeRemaining - seconds) * 100;
        countdownText.text = seconds.ToString() + " : "
            + milliseconds.ToString("F0");
    }

    public void Ending(Quaternion lastLook, Vector3 lastPos)
    {
        mainCamera.gameObject.SetActive(true);
        mainCamera.transform.rotation = lastLook;
        mainCamera.transform.position = lastPos;
        StartCoroutine(ReturnToMenu(8f));
    }

    IEnumerator ReturnToMenu(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        transition.GoFade();
    }

    public Transform GetPlayerSpawn()
    {
        return playerSpawn;
    }
}
