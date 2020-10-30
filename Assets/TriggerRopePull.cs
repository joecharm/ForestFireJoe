using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerRopePull : MonoBehaviour
{
    private bool heliLift = false;

    // set the helicopter speed
    public float liftSpeed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        // get the interactable component
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        // create a listener to detect when the trigger is called, if true call the flareTrigger method
        grabInteractable.onActivate.AddListener(TriggerRope);
    }

    // Update is called once per frame
    void Update()
    {
        if(heliLift == true)
        {
            // lift the helicopter
            // get the heli 
            GameObject helicopter = GameObject.FindGameObjectWithTag("Heli");
            // heli target position
            Vector3 newHeliPosition = new Vector3(helicopter.transform.position.x, 50, helicopter.transform.position.z);

            // lift rope
            // get the rope
            GameObject rope = GameObject.FindGameObjectWithTag("Rope");
            // rope target position
            Vector3 newRopePosition = new Vector3(rope.transform.position.x, 50, rope.transform.position.z);

            // lift player
            // get the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // rope target position
            Vector3 newPlayerPosition = new Vector3(player.transform.position.x, 50, player.transform.position.z);

            // move the heli up
            helicopter.transform.position = Vector3.MoveTowards(helicopter.transform.position, newHeliPosition, Time.deltaTime * liftSpeed);
            // move the rope up
            rope.transform.position = Vector3.MoveTowards(rope.transform.position, newRopePosition, Time.deltaTime * liftSpeed);
            // move the player up
            player.transform.position = Vector3.MoveTowards(player.transform.position, newPlayerPosition, Time.deltaTime * liftSpeed);
        }
    }

    public void TriggerRope(XRBaseInteractor interactable)
    {
        // trigger the helicopter to lift
        heliLift = true;
    }
}
