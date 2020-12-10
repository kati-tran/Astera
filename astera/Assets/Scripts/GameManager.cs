using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public Animator transition;

	public void loadScene(string scene)
	{
		Debug.Log("loading "+ scene);
		SceneManager.LoadScene(scene);
	}

	public void loadNextScene()
	{
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
	}

	IEnumerator LoadLevel(int levelIndex){
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(levelIndex);
	}

}
