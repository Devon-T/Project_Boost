﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectX : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(Time.deltaTime * 30, 0, 0));
	}
}
