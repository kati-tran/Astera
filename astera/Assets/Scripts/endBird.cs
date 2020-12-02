using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endBird : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col){

        SceneManager.LoadScene("FoxLevel");
    }
}
