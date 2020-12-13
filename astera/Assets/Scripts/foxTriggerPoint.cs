using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxTriggerPoint : MonoBehaviour
{
    public GameObject player;
    public GameObject fox;
    public GameObject drawing;
    public GameObject crystalCluster;
    public Material gemMaterial;

    PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
            throw new System.NullReferenceException("Player GameObject reference not set or invalid on Fox Trigger Point script");
        if(fox == null)
            throw new System.NullReferenceException("Fox GameObject reference not set or invalid on Fox Trigger Point script");
    }

    // Update is called once per frame
    void Update()
    {
        playerMove = player.GetComponent<PlayerMove>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == player && col != playerMove.groundContactCollider)
        {
            playerMove.activeSpirit = PlayerMove.Spirit.Fox;
            fox.GetComponent<FoxScript>().Activate();
            drawing.SetActive(true);
            crystalCluster.GetComponentInChildren<Light>().enabled = true;
            foreach (MeshRenderer renderer in crystalCluster.GetComponentsInChildren<MeshRenderer>())
                renderer.material = gemMaterial;
            Destroy(gameObject);
        }
    }
}
