using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject chiBall;
    public OVRHand leftHand;
    public OVRHand  rightHand;
    public Vector3 currentChiballScale;

    private float zOffset = 0.05f;
    private float yOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // set the scale of chiBall
        chiBall.transform.localScale = new Vector3(0.08f,0.08f,0.08f);
        chiBall.SetActive(false);
    }

    void FixedUpdate()
    {
        //
        if (leftHand.IsTracked && rightHand.IsTracked)
        {
            // Get positions of both hands
            Vector3 leftHandPos = leftHand.transform.position;
            Vector3 rightHandPos = rightHand.transform.position;
            // Get the scale of chiBall
            //Vector3 currentChiballScale = chiBall.transform.localScale;

            // Calculate distance between hands
            float distance = Vector3.Distance(leftHandPos, rightHandPos);
            Vector3 midpoint = (leftHandPos + rightHandPos) / 2f;
            midpoint += Camera.main.transform.forward * zOffset;
             midpoint.y += yOffset;
            Debug.Log("Mifpoint is "+midpoint); 
            // Output distance
            Debug.Log("Distance between hands: " + distance);
            //Condition to enable/disable chiball.
            if(distance>0.10){
                chiBall.transform.position = midpoint;
                chiBall.SetActive(true);
                chiBall.transform.localScale = new Vector3(currentChiballScale.x+distance/4,currentChiballScale.y+distance/4,currentChiballScale.z+distance/4);
            }
            else{
                chiBall.SetActive(false);
                currentChiballScale = new Vector3(0.08f,0.08f,0.08f);
            }


            //Increase the size of chiBall
            
        }
    }
}
