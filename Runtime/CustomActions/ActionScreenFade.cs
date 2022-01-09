using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC {

    [System.Serializable]
    public class ActionScreenFade : Action
    {
        
	public FadeType fadeType;
	private ScreenFade _screenFade = null;

	public ScreenFade screenFade
	{
	    get
	    {
		if (_screenFade == null)
		{
		    _screenFade = GameObject.FindObjectOfType<ScreenFade>();
		}
		return _screenFade;
	    }
	}
	
	float defaultPauseTime = 0.25f;

	public override ActionCategory Category { get { return ActionCategory.Camera; }}
	public override string Title { get { return "VR Fade"; }}
	public override string Description { get { return "Fades in and out in VR, using URP."; }}

	public override float Run() {
	    if (!isRunning)
	    {
		isRunning = true;		
		RunSelf();
		if (willWait)
		{
		    return defaultPauseTime;
		}
		return 0f;
	    } else {
		if (screenFade.IsFading()) {
		    return defaultPauseTime;
		}
		isRunning = false;
		return 0f;
	    }
	}

	
	public override void Skip ()
	{
	    RunSelf();
	}

	
	protected void RunSelf ()
	{
	    if (fadeType == FadeType.fadeIn) {
		defaultPauseTime = screenFade.FadeIn();
	    } else {
		defaultPauseTime = screenFade.FadeOut();
	    }
	}


	
		#if UNITY_EDITOR
	
	public override void ShowGUI (List<ActionParameter> parameters)
	{
	    fadeType = (FadeType) EditorGUILayout.EnumPopup ("Type:", fadeType);
	}
		
		
	public override string SetLabel ()
	{
	    if (fadeType == FadeType.fadeIn)
	    {
		return "In";
	    }
	    else
	    {
		return "Out";
	    }
	}

		#endif

	public static ActionScreenFade CreateNew (FadeType fadeType, bool waitUntilFinish = true)
	{
	    ActionScreenFade newAction = CreateNew<ActionScreenFade> ();
	    newAction.fadeType = fadeType;
	    newAction.willWait = waitUntilFinish;
	    return newAction;
	}
	
    }


}
