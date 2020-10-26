using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerFlare : MonoBehaviour
{
    // define the game object of the prefab flare
    GameObject flare;

    public GameObject helicopter;

    public bool heliCalled = false;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(heliCalled == true)
        {
            Vector3 targetDestination = new Vector3(73.0f, 19.0f, 63.0f);
            // move the heli
            helicopter.transform.position = Vector3.MoveTowards(helicopter.transform.position, targetDestination, Time.deltaTime * speed);
        }
    }

    // called on the first frame
    private void Awake()
    {
        // get the property of the current gameObject
        flare = this.gameObject;
        
        // get the interactable component
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        
        // create a listener to detect when the trigger is called, if true call the flareTrigger method
        grabInteractable.onActivate.AddListener(flareTrigger);
        
        // create a listener to detect when the trigger is deactivated, if true call the flareTriggerOff method
        grabInteractable.onDeactivate.AddListener(flareTriggerOff);
    }

    // method to get the child object on the flare prefab (the sparks) and set active, this will also call the helicopter when pressed for the first time
    public void flareTrigger(XRBaseInteractor interactable)
    {
        GameObject sparks = flare.transform.GetChild(0).gameObject;
        sparks.SetActive(true);

        // call the helicopter

        if(heliCalled == false)
        {
            callHelicopter();
        }

    }

    // method to deactivate the sparks on the flare, reversing the previous method
    public void flareTriggerOff(XRBaseInteractor interactable)
    {
        GameObject sparks = flare.transform.GetChild(0).gameObject;
        sparks.SetActive(false);
    }

    public void callHelicopter()
    {
        Instantiate(helicopter, new Vector3(0.0f, 17.0f, 0.0f), Quaternion.identity);

        // current position of helicopter
        //helicopter.transform.position = new Vector3(0, 17, 0);

        // Target destination of the helicopter
        // Vector3 targetDestination = new Vector3(73.0f, 19.0f, 63.0f);

        // move the heli
       // helicopter.transform.position = Vector3.MoveTowards(helicopter.transform.position, targetDestination, Time.deltaTime * speed);

        Debug.Log(helicopter.transform.position);

        // set helicopter to called
        heliCalled = true;
    }

}
