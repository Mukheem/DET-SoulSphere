using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject chiBall;
    public OVRHand leftHand;
    public OVRHand  rightHand;
    public Vector3 currentChiballScale ;

    private float zOffset = 0.05f;
    private float yOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        // set the scale of chiBall
        currentChiballScale = new Vector3(0.08f,0.08f,0.08f);
        chiBall.transform.localScale = currentChiballScale;
        chiBall.SetActive(false);
    }

    void changeScaling(float distance){
         chiBall.transform.localScale = new Vector3(currentChiballScale.x+distance/4,currentChiballScale.y+distance/4,currentChiballScale.z+distance/4);
    }

    void FixedUpdate()
    {
        
        if (leftHand.IsTracked && rightHand.IsTracked)
        {
            // Get positions of both hands
            Vector3 leftHandPos = leftHand.transform.position;
            Vector3 rightHandPos = rightHand.transform.position;


            // Calculate distance between hands
            float distance = Vector3.Distance(leftHandPos, rightHandPos);
            Vector3 midpoint = (leftHandPos + rightHandPos) / 2f; //Calculating the midpoint to place the chiBall
          
            //zOffset is used so that the ball appears exactly at the center of the hands. it is multiplied by 'Camera.main.transform.forward' so that the ball's Z axis is consistent when the user rotates or turns his/her head.
            midpoint += Camera.main.transform.forward * zOffset;
            //yOffset is used so that ball appears slightly up to the hands y axis.
            midpoint.y += yOffset;
         
            //Condition to enable/disable chiball.
            if(distance>0.10){
                chiBall.transform.position = midpoint;//Give the position to Chiball relative to hands.
                chiBall.SetActive(true);
               changeScaling(distance);//Change the scale of ball.
            }
            else{
                chiBall.SetActive(false);
                currentChiballScale = new Vector3(0.08f,0.08f,0.08f);
            }


           
            
        }
    }
}
