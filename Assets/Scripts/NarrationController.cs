using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationController : MonoBehaviour
{
    public bool startNarration = false;
    AudioSource audioSource;
    public AudioClip introClip;
    public AudioClip welcomeClip;
    public GameObject cameraRig;
    private OVRPassthroughLayer passthroughlayerScript;
    public GameObject menu;

    public void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        passthroughlayerScript = cameraRig.GetComponent<OVRPassthroughLayer>();
        menu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (startNarration)
        {
            StartCoroutine(startIntroNarration());
        }
        if (menu.activeInHierarchy)
        {
            menu.transform.position = Camera.main.transform.position + new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * 2;
            menu.transform.LookAt(new Vector3(Camera.main.transform.forward.x, menu.transform.position.y, Camera.main.transform.position.z));
            menu.transform.forward *= -1;
        }
    }

    IEnumerator startIntroNarration()
    {
        audioSource.PlayOneShot(introClip);
        yield return new WaitForSeconds(introClip.length);
    }
    public IEnumerator moveToMixedReality()
    {
        

        while (passthroughlayerScript.textureOpacity != 1.0f)
        {
            passthroughlayerScript.textureOpacity += 0.01f;//0.0009f;
            yield return new WaitForSeconds(0.01f);
        }
        

        
    }
    public void startWelcomeNarrationAndMoveTOMixedReality()
    {
        audioSource.PlayOneShot(welcomeClip);
        StartCoroutine(moveToMixedReality());
    }
    
}
