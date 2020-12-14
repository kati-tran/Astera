using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLevelAudioController : MonoBehaviour
{
    public GameObject player;
    bool aboveGround = true;
    AudioManager audioManager;
    float timeLastToggled;
    float toggleDelay = 1;
    bool toggled = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.fadeIn("Wind", toggleDelay);
    }

    void doUnderground()
    {
        audioManager.fadeOut("Wind", toggleDelay, true);
    }

    void doAboveGround()
    {
        audioManager.fadeIn("Wind", toggleDelay);
    }

    // Update is called once per frame
    void Update()
    {
        float now = Time.time;
        if (player.transform.position.y < -1f)
        {
            if (aboveGround)
            {
                aboveGround = false;
                toggled = true;
                timeLastToggled = now;
            }
            else if(toggled && now - timeLastToggled > toggleDelay)
            {
                toggled = false;
                doUnderground();
            }
        }
        else
        {
            if (!aboveGround)
            {
                aboveGround = true;
                toggled = true;
                timeLastToggled = now;
            }
            else if(toggled && now - timeLastToggled > toggleDelay)
            {
                toggled = false;
                doAboveGround();
            }
        }

    }
}
