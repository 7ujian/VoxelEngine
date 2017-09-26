using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TestReg : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		string s = "aaa,bbb1,ccc";

		foreach (var l in Regex.Split(s, @"(?<!1),"))
		{
			Debug.Log(l);
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
