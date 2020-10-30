using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerFlare : MonoBehaviour
{
    // define the game object of the prefab flare
    GameObject flare;
    public GameObject helicopterPrefab;

    // boolean is the heli has been called into the scene by the player
    public bool heliCalled = false;

    // set the helicopter speed
    public float speed = 8;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

            // helicopter has not yet reached the player
            // Once the helicopter is called this will run, placing a helicopter into the scene and moving it to the players position
            if (heliCalled == true)
            {
                // get the heli 
                GameObject helicopter = GameObject.FindGameObjectWithTag("Heli");
                // get the target position (players position)
                float playerPositionX = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.x;
                float playerPositionZ = GameObject.FindGameObjectsWithTag("Player")[0].transform.position.z;
                // set the height of the heli
                float playerPositionY = 17.0f;

                // set the players position with custom height for the helicopter to be above the player
                // We add 3 to the players position to offset the helicopter from the player, othereise it is directly above
                Vector3 playerPosition = new Vector3(playerPositionX + 3, playerPositionY, playerPositionZ + 3);

                // move the heli
                helicopter.transform.position = Vector3.MoveTowards(helicopter.transform.position, playerPosition, Time.deltaTime * speed);

                // set helicalled back to false to not call the helicopter again once it is within 3m range of the player
                // Also show the rope now the heli has arrived
                if(Vector3.Distance(helicopter.transform.position, playerPosition) < 3.0f)
                {
                 heliCalled = false;
                // find rope and set active
                GameObject heliRope = helicopter.transform.Find("Rope").gameObject;
                heliRope.SetActive(true);


                }

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
        // set the flare sparks to active
        GameObject sparks = flare.transform.GetChild(0).gameObject;
        sparks.SetActive(true);

        // call the helicopter if is has not yet been called. If Helicalled = true, this will not run and not instatiate another Helicopter
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

    // method to call the helicopter into the scene and set called to True, the update() method will then pick this up and move the heli to the player
    public void callHelicopter()
    {
        //instantiate a new heli into the scene 17m high at 0,0.
        Instantiate(helicopterPrefab, new Vector3(0.0f, 17.0f, 0.0f), Quaternion.identity);

        // set helicopter to called
        heliCalled = true;
    }

}
