using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public Animator transition;
	private float delay;

	public void Start()
	{
		delay = 1f;
		if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
	}

	public void reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void loadSceneString(string scene)
	{
		Debug.Log("loading "+ scene);
		SceneManager.LoadScene(scene);
	}

	public void loadSceneInt(int levelIndex)
	{
		StartCoroutine(LoadLevel(levelIndex));
	}

	public void loadNextScene()
	{
		StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
	}

	IEnumerator LoadLevel(int levelIndex){
		float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(levelIndex);
	}


}
