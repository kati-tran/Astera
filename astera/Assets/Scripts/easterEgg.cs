﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easterEgg : MonoBehaviour
{
	public ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = ps.GetComponent<ParticleSystem>();
    }

    void easter()
    {
    	if (ps.isPlaying)
    	{
    		ps.Stop();
    	}
    		
    	else
    	{
    		ps.Play();
    	}
    		
	}

}