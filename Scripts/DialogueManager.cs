using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	private Queue<Sentence> sentences;
	private bool DialougeInProgress = false;
	
	public string playerName = "Haruto";
	
	private string objective;
	private string speaker;
	private string text;
	private float seconds;
	
	private float elapsedTime = 0.0f;
	
	public Text Objective;
	public Text ObjectiveShadow;
	public Text Speaker;
	public Text Dialouge;
	
	private float fadeSpeed = 0.5f;
	private int inOrOut = 0;
	
	
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
		Color transparent = Objective.color;
		transparent.a = 0;
		Objective.color = transparent;
    }

    // Update is called once per frame
    void Update()
    {
		if(inOrOut != 0)
		{
			objectiveFade(Time.deltaTime);
		}
		
        if (DialougeInProgress)
		{
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= seconds)
			{
				DisplayNextSentence();
			}
			
			if (elapsedTime >= 2)
			{
				//End Objective
				inOrOut = -1;
				//Objective.text = "";
			}
		}
    }
	
	public void Display(DialougeTrigger dialougeTrigger)
	{
		Dialouge[] dialougeList = dialougeTrigger.dialougeList;
		
		if(dialougeTrigger.hasBeenSpoken)
		{
			return;
		}
		dialougeTrigger.hasBeenSpoken = true;
		
		int indexOfDialogue = 0;
		
		sentences.Clear();
		
		foreach(Dialouge dialouge in dialougeList)
		{
			
			string audioClipName = dialougeTrigger.name + indexOfDialogue;
			//FindObjectOfType<AudioManager>().Play(audioClipName);
			indexOfDialogue += 1;
			
			//dialouge.hasBeenSpoken = true;
			
			//Debug.Log("DialougeManager Called By " + dialouge.speaker);
			
			
			objective = dialouge.objective;
			speaker = playerName;//dialouge.speaker;
			
			Objective.text = objective;
			ObjectiveShadow.text = objective;
			inOrOut = 1;
			if(objective != "")
				FindObjectOfType<AudioManager>().Play("Drum");
			Speaker.text = speaker;
			
			foreach(Sentence sentence in dialouge.sentences)
			{
				sentences.Enqueue(sentence);
			}
			
			DialougeInProgress = true;

		}
	}
	
	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			//End Dialogue
			DialougeInProgress = false;
			
			//Objective.text = "";
			inOrOut = -1;
			Speaker.text = "";
			Dialouge.text = "";
			
			//objective = "";
			speaker = "";
			text = "";
			seconds = 0f;
			
			elapsedTime = 0f;

			return;
		}
		
		Sentence currentSentence = sentences.Dequeue();
		
		text = currentSentence.text;
		seconds = currentSentence.seconds;
		elapsedTime = 0f;
		
		Dialouge.text = text;
	}
	
	public void objectiveFade(float timeElapsed)
	{
		Color old = Objective.color;
		
		if(inOrOut == -1 && old.a <= 0)
		{
			inOrOut = 0;
			Objective.text = "";
			ObjectiveShadow.text = "";
			objective = "";
			return;
		}
		else if(inOrOut == 1 && old.a >= 255)
		{
			inOrOut = 0;
			return;
		}
		
		float newA = old.a + inOrOut * fadeSpeed * timeElapsed;
		if (newA > 255) newA = 255;
		if (newA < 0) newA = 0;
		
		old.a = newA;
		Objective.color = old;
		
		Color shadow = ObjectiveShadow.color;
		shadow.a = newA;
		ObjectiveShadow.color = shadow;
	}
}