// Adapted from a Brackey's Tutorial on YouTube

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
	[TextArea(1,2)]
    public string text;
	public float seconds;
}
