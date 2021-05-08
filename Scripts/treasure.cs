using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treasure : MonoBehaviour
{
    public bool hasFoundPower = false;
    public bool hasActivatedPower = false;

    public Text prompt;

    void Update()
    {
        if (hasFoundPower && !hasActivatedPower)
        {
            prompt.text = "Press RMB to use new found power";
			hasActivatedPower  = false;
        }
        if(hasFoundPower && Input.GetMouseButton(1))
        {
            hasActivatedPower  = true;
            prompt.text = "";
        }
    }
}