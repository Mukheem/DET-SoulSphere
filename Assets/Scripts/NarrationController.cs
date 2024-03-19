using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NarrationController : MonoBehaviour
{
    public bool startNarration = false;
    public AudioSource audioSource;
    public AudioClip whatIsChiBallClip;
    public AudioClip welcomeClip;
    public AudioClip handsOnTableClip;
    public AudioClip liftHandsUpClip;
    public AudioClip throwChiBallClip;
    public AudioClip touchFingerTipsClip;
    public AudioClip adjustHandsClip;
    public AudioClip thankyouClip;
    public GameObject cameraRig;
    private OVRPassthroughLayer passthroughlayerScript;
    public GameObject menu;
    private GameObject[] ThumbsupPoses;
    private GameObject[] StopPoses;
    private GameObject demoChiBall;
    private GameObject ghostHands;
    public GameObject webSocketController;
    public GameObject ballController;
    public GameObject ghostHandsThrow;
    private GameObject ghostHandsJoin;
    private GameObject ghostHandsPlace;
    private GameObject ghostHandsDistance;
    private WebSocketController webSocketControllerScript;
    public bool turnPositionSwitch = true;

    public GameObject leftHandDistance;
    public GameObject rightHandDistance;

    private float leftHandDistanceStartingTransform;
    private float rightHandDistanceStartingTransform;

    // Start is called before the first frame update
    void Start()
    {
        ThumbsupPoses = GameObject.FindGameObjectsWithTag("PoseThumb");
        StopPoses = GameObject.FindGameObjectsWithTag("PoseStop");
        demoChiBall = GameObject.FindGameObjectWithTag("DemoChiBall");
        ghostHands = GameObject.FindGameObjectWithTag("GhostHands");
       ghostHandsJoin = GameObject.FindGameObjectWithTag("GhostHandsJoin");
        ghostHandsPlace = GameObject.FindGameObjectWithTag("GhostHandsPlace");
        ghostHandsDistance = GameObject.FindGameObjectWithTag("GhostHandsDistance");

        passthroughlayerScript = cameraRig.GetComponent<OVRPassthroughLayer>();
        webSocketControllerScript = webSocketController.GetComponent<WebSocketController>();

        menu.SetActive(true);
        demoChiBall.SetActive(false);
        ghostHands.SetActive(false);
        ghostHandsThrow.SetActive(false);
        ghostHandsJoin.SetActive(false);
        ghostHandsPlace.SetActive(false);
        ghostHandsDistance.SetActive(false);

        StopPoses[0].SetActive(false);
        StopPoses[1].SetActive(false);
        //leftHandDistance = GameObject.FindGameObjectWithTag("leftHandDistance");
        //rightHandDistance = GameObject.FindGameObjectWithTag("rightHandDistance");
        leftHandDistanceStartingTransform = leftHandDistance.transform.position.x;
        rightHandDistanceStartingTransform = rightHandDistance.transform.position.x;

    }

    // Update is called once per frame
   
    void Update()
    {
        Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward * 2;// Calculate the position of the object based on the camera's position and forward direction
        Quaternion newRotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up); // Calculate the target rotation of the object based on the camera's forward direction
        if (menu.activeInHierarchy)
        {
            /*menu.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 2;
            menu.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
            menu.transform.forward *= -1;*/

           

            menu.transform.position = newPosition;// Set the position of the object
            menu.transform.rotation = newRotation;// Set the rotation of the object

        }
        
        if (turnPositionSwitch)
        {
            demoChiBall.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 2;
            demoChiBall.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
            //demoChiBall.transform.forward *= -1;

            ghostHands.transform.position = newPosition;// Set the position of the object
            ghostHands.transform.rotation = newRotation;// Set the rotation of the object

            ghostHandsThrow.transform.position = newPosition;// Set the position of the object
            ghostHandsThrow.transform.rotation = newRotation;// Set the rotation of the object

            ghostHandsJoin.transform.position = newPosition;// Set the position of the object
            ghostHandsJoin.transform.rotation = newRotation;// Set the rotation of the object

            ghostHandsPlace.transform.position = newPosition;// Set the position of the object
            ghostHandsPlace.transform.rotation = newRotation;// Set the rotation of the object

            ghostHandsDistance.transform.position = newPosition;// Set the position of the object
            ghostHandsDistance.transform.rotation = newRotation;// Set the rotation of the object


            /* ghostHands.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 1;
             ghostHands.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
             ghostHands.transform.forward *= -1;

             ghostHandsThrow.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 1;
             ghostHandsThrow.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
             ghostHandsThrow.transform.forward *= -1;

             ghostHandsJoin.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 1;
             ghostHandsJoin.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
             ghostHandsJoin.transform.forward *= -1;

             ghostHandsPlace.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 1;
             ghostHandsPlace.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
             ghostHandsPlace.transform.forward *= -1;

             ghostHandsDistance.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 1;
             ghostHandsDistance.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
             ghostHandsDistance.transform.forward *= -1;*/

        }
        
        
        

    }

    IEnumerator startWhatIsChiBallNarration()
    {
       
        Debug.Log("Playing startWhatIsChiBallNarration coroutine ");
        yield return new WaitForSeconds(welcomeClip.length+1.0f); // Waits for the previous clip to finish
        audioSource.PlayOneShot(whatIsChiBallClip);
        yield return new WaitForSeconds(whatIsChiBallClip.length+1.0f);
        StartCoroutine(placeHandsOnTableNarrationAndDarkenTheScene());
        demoChiBall.SetActive(false);
    }
    public IEnumerator moveFromMixedReality()
    {
        

        while (passthroughlayerScript.textureOpacity != 0.0f)
        {
            passthroughlayerScript.textureOpacity -= 0.03868f;// 25 seconds to finish this while loop
            yield return new WaitForSeconds(1f);
            if(passthroughlayerScript.textureOpacity <= 0.15f)
            {
                Camera.main.clearFlags = CameraClearFlags.Skybox;
            }
        }
        

        
    }
    public void startWelcomeNarrationAndMoveTOMixedReality()
    {
        webSocketControllerScript.ws.Send("Need input");
        audioSource.PlayOneShot(welcomeClip);
        ThumbsupPoses[0].SetActive(false);
        ThumbsupPoses[1].SetActive(false);
        menu.SetActive(false);
        demoChiBall.SetActive(true);
        turnPositionSwitch = false;
        StartCoroutine(startWhatIsChiBallNarration());
        

    }
    
    public IEnumerator placeHandsOnTableNarrationAndDarkenTheScene()
    {
        turnPositionSwitch = true;
        Debug.Log("Playing placeHandsOnTableNarrationAndDarkenTheScene coroutine ");
        audioSource.PlayOneShot(handsOnTableClip);
        ghostHandsPlace.SetActive(true);
        turnPositionSwitch = false;
        webSocketControllerScript.ws.Send("Need input");
        StartCoroutine(moveFromMixedReality());
        yield return new WaitForSeconds(handsOnTableClip.length);
        ghostHandsPlace.SetActive(false);
        yield return new WaitForSeconds(1.9f);
        StartCoroutine(liftHandsFromTheTable());
    }

    public IEnumerator liftHandsFromTheTable()
    {
        turnPositionSwitch = true;
        Debug.Log("Playing liftHandsFromTheTable coroutine ");
        turnPositionSwitch = false;
        audioSource.PlayOneShot(liftHandsUpClip);
        ghostHands.SetActive(true);
        yield return new WaitForSeconds(liftHandsUpClip.length+5.0f);
        ghostHands.SetActive(false);
        demoChiBall.SetActive(false);
        audioSource.PlayOneShot(touchFingerTipsClip);
        yield return new WaitForSeconds(3);
        turnPositionSwitch = true;
        ghostHandsJoin.SetActive(true);
        turnPositionSwitch = false;
        yield return new WaitForSeconds(11);
        ghostHandsJoin.SetActive(false);
        ballController.GetComponent<BallController>().startChiBall = true;
        yield return new WaitForSeconds(touchFingerTipsClip.length-14);
        turnPositionSwitch = true;
        ghostHandsDistance.SetActive(true);
        turnPositionSwitch = false;
        StartCoroutine(translateHands());
        audioSource.PlayOneShot(adjustHandsClip);
        yield return new WaitForSeconds(adjustHandsClip.length);
        ghostHandsDistance.SetActive(false);
        yield return new WaitForSeconds(7);
        turnPositionSwitch = true;
        ghostHandsThrow.SetActive(true);
        StopPoses[0].SetActive(true);
        StopPoses[1].SetActive(true);
        turnPositionSwitch = false;
        audioSource.PlayOneShot(throwChiBallClip);
        yield return new WaitForSeconds(throwChiBallClip.length);
        ghostHandsThrow.SetActive(false);
        StartCoroutine(thankYou());
    }

    public IEnumerator thankYou()
    {
        while(ballController.GetComponent<BallController>().throwBall)
        {
            yield return new WaitForSeconds(4);
            audioSource.PlayOneShot(thankyouClip);
            yield return new WaitForSeconds(thankyouClip.length);
            break;
        }
       
    }

    public IEnumerator translateHands()
    {
       
        for (int i = 0; i < 7; i++)
            {
                leftHandDistance.transform.Translate(Vector3.left *2* Time.deltaTime, Camera.main.transform);
                rightHandDistance.transform.Translate(Vector3.right *2* Time.deltaTime, Camera.main.transform);
                yield return new WaitForSeconds(1f);
                leftHandDistance.transform.Translate(Vector3.right *2* Time.deltaTime, Camera.main.transform);
                rightHandDistance.transform.Translate(Vector3.left *2* Time.deltaTime, Camera.main.transform);
                yield return new WaitForSeconds(1f);
            }
            
           
       
        
    }
   
}
