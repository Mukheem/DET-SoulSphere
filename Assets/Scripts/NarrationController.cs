using System.Collections;
using System.Collections.Generic;
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
   
    private WebSocketController webSocketControllerScript;
    public bool turnPositionSwitch = true;
  
    // Start is called before the first frame update
    void Start()
    {
        ThumbsupPoses = GameObject.FindGameObjectsWithTag("PoseThumb");
        StopPoses = GameObject.FindGameObjectsWithTag("PoseStop");
        demoChiBall = GameObject.FindGameObjectWithTag("DemoChiBall");
        ghostHands = GameObject.FindGameObjectWithTag("GhostHands");
       
        passthroughlayerScript = cameraRig.GetComponent<OVRPassthroughLayer>();
        webSocketControllerScript = webSocketController.GetComponent<WebSocketController>();

        menu.SetActive(true);
        demoChiBall.SetActive(false);
        ghostHands.SetActive(false);
       

        StopPoses[0].SetActive(false);
        StopPoses[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (menu.activeInHierarchy)
        {
            menu.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 2;
            menu.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
            menu.transform.forward *= -1;
        }

        if (turnPositionSwitch)
        {
            demoChiBall.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 2;
            demoChiBall.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
            demoChiBall.transform.forward *= -1;

            ghostHands.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 1;
            ghostHands.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
            ghostHands.transform.forward *= -1;

        }


    }

    IEnumerator startWhatIsChiBallNarration()
    {
        Debug.Log("Playing startWhatIsChiBallNarration coroutine ");
        yield return new WaitForSeconds(welcomeClip.length+1.0f); // Waits for the previous clip to finish
        audioSource.PlayOneShot(whatIsChiBallClip);
        yield return new WaitForSeconds(whatIsChiBallClip.length+1.0f);
        StartCoroutine(placeHandsOnTableNarrationAndDarkenTheScene());
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
        Debug.Log("Playing placeHandsOnTableNarrationAndDarkenTheScene coroutine ");
        audioSource.PlayOneShot(handsOnTableClip);
        webSocketControllerScript.ws.Send("Need input");
        StartCoroutine(moveFromMixedReality());
        yield return new WaitForSeconds(handsOnTableClip.length+1.9f);
        StartCoroutine(liftHandsFromTheTable());
    }

    public IEnumerator liftHandsFromTheTable()
    {
        Debug.Log("Playing liftHandsFromTheTable coroutine ");
        audioSource.PlayOneShot(liftHandsUpClip);
        ghostHands.SetActive(true);
        yield return new WaitForSeconds(liftHandsUpClip.length+20.0f);
        ghostHands.SetActive(false);
        demoChiBall.SetActive(false);
        audioSource.PlayOneShot(touchFingerTipsClip);
        yield return new WaitForSeconds(14);
        ballController.GetComponent<BallController>().startChiBall = true;
        yield return new WaitForSeconds(touchFingerTipsClip.length-14);
        audioSource.PlayOneShot(adjustHandsClip);
        yield return new WaitForSeconds(adjustHandsClip.length);
        StopPoses[0].SetActive(true);
        StopPoses[1].SetActive(true);
        audioSource.PlayOneShot(throwChiBallClip);
        yield return new WaitForSeconds(throwChiBallClip.length);
        StartCoroutine(thankYou());
    }

    public IEnumerator thankYou()
    {
        yield return new WaitForSeconds(4);
        audioSource.PlayOneShot(thankyouClip);
        yield return new WaitForSeconds(thankyouClip.length);
    }
}
