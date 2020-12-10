using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public void loadScene(string scene)
	{
		Debug.Log("loading "+ scene);
		SceneManager.LoadScene(scene);
	}

	public void loadNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
}
