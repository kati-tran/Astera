using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPoint : MonoBehaviour
{
    public GameObject bird;
    public GameObject player;
    PlayerMove playerScript;
    public GameObject birdsound;
    AudioSource birdS;

    void Start(){
        playerScript = player.GetComponent<PlayerMove>();
        birdS = birdsound.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col){
        bird.SetActive(true);
        birdS.Play(0);
        playerScript.activeSpirit = PlayerMove.Spirit.Bird;
        Object.Destroy(this.gameObject);
    }
}
