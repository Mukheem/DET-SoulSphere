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

    public void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        passthroughlayerScript = cameraRig.GetComponent<OVRPassthroughLayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startNarration)
        {
            StartCoroutine(startIntroNarration());
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
            passthroughlayerScript.textureOpacity += 0.1f;
            yield return new WaitForSeconds(0.2f);
        }
        

        
    }
    public void startWelcomeNarrationAndMoveTOMixedReality()
    {
        audioSource.PlayOneShot(welcomeClip);
        StartCoroutine(moveToMixedReality());
    }
    
}
