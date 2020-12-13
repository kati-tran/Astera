using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{
	public GameObject ToggledPanel;

    public void TogglePanel()
    {
  
    	ToggledPanel.gameObject.SetActive(!ToggledPanel.gameObject.activeSelf);
    }
}
