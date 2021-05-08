using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialouge
{
	public string speaker;
	
	[TextArea(1,2)]
	public string objective;
	
	
	public Sentence[] sentences;
	
	//public bool hasBeenSpoken = false;
}
