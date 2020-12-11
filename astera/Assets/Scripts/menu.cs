using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
	public GameObject ToggledPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePanel()
    {
  
    	ToggledPanel.gameObject.SetActive(!ToggledPanel.gameObject.activeSelf);
    }
}
