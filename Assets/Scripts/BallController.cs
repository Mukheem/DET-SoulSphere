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
    public bool startChiBall = false;
    public bool throwBall = false;
    public GameObject webSocketController;
    private WebSocketController webSocketControllerScript;
    private GameObject chiBallRounds;
    //public OVREyeGaze eyeGaze;
    //public GameObject caps;
    private GameObject[] StopPoses;

    void Start()
    {
        // set the scale of chiBall
        currentChiballScale = new Vector3(0.08f,0.08f,0.08f);
        chiBall.transform.localScale = currentChiballScale;
        StopPoses = GameObject.FindGameObjectsWithTag("PoseStop");

        chiBallRounds = GameObject.FindGameObjectWithTag("Rounds");
        chiBallRounds.SetActive(false);
        chiBall.SetActive(false);

        webSocketControllerScript = webSocketController.GetComponent<WebSocketController>();
    }

    void changeScaling(float distance){
         chiBall.transform.localScale = new Vector3(currentChiballScale.x+distance/4,currentChiballScale.y+distance/4,currentChiballScale.z+distance/4);
    }

    void FixedUpdate()
    {
        /*
        if (eyeGaze == null){
            Debug.Log("Gaze ia Null");
        }

        if (eyeGaze.EyeTrackingEnabled)
        {
            Debug.Log("Eye tracking enabled");
            Debug.Log(eyeGaze.Eye);
           caps.transform.rotation= eyeGaze.transform.rotation;
        }
        */
        
        if ((leftHand.IsTracked && rightHand.IsTracked) && !throwBall && startChiBall)
        {
            // Get positions of both hands
            Vector3 leftHandPos = leftHand.transform.position;
            Vector3 rightHandPos = rightHand.transform.position;


            // Calculate distance between hands
            float distance = Vector3.Distance(leftHandPos, rightHandPos);
            Vector3 midpoint = (leftHandPos + rightHandPos) / 2f; //Calculating the midpoint to place the chiBall
          
            //zOffset is used so that the ball appears exactly at the center of the hands. it is multiplied by 'Camera.main.transform.forward' so that the ball's Z axis is consistent when the user rotates or turns his/her head.
            midpoint += Camera.main.transform.forward * zOffset;
            //midpoint.z +=  zOffset;
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

    //Throwing the ball into space
    public void ThrowBall(){
        Debug.Log("Throwing ball...");
        throwBall = true;
       // StopPoses[0].SetActive(false);
       // StopPoses[1].SetActive(false);
        Debug.Log("Websocket state - " + webSocketControllerScript.ws.ReadyState);
        webSocketControllerScript.ws.Send("throw");
        StartCoroutine(SlideBallIntoSpace());
    }

    //CoRoutine to translate the ball wrt Worldspace
    public IEnumerator SlideBallIntoSpace(){
        chiBallRounds.SetActive(true);
        while (true){
            chiBall.transform.Translate(Vector3.forward * Time.deltaTime,Camera.main.transform);
            yield return new WaitForSeconds(.08f);
        }
       
    }
}
