using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AC;

namespace CC.VR {
    
    public class Score : MonoBehaviour
    {
	Text uiText;
	
	// Start is called before the first frame update
	void Start()
	{
	    uiText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update()
	{
	    string score = GlobalVariables.GetVariable("Scoring/Score").GetValue();

	    // TODO: Localize
	    uiText.text = string.Format("Score: {0} out of 350", score);
	}
    }
    
}
