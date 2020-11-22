using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPoint : MonoBehaviour
{
    public GameObject bird;
    public GameObject player;
    PlayerMove playerScript;

    void Start(){
        playerScript = player.GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter2D(Collider2D col){
        bird.SetActive(true);
        playerScript.activeSpirit = PlayerMove.Spirit.Bird;
        Object.Destroy(this.gameObject);
    }
}
