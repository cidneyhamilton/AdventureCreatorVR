using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

	[System.Serializable]
	public class ActionResetRotation : Action
	{

	    // Teleportation Marker
	    public int markerParameterID = -1;
	    public int markerID = 0;
	    public Marker teleporter;
	    protected Marker runtimeTeleporter;
	    
	    // Declare properties here
	    public override ActionCategory Category { get { return ActionCategory.Player; }}
	    public override string Title { get { return "Teleport XR player"; }}
	    public override string Description { get { return "Teleport XR player to a marker, matching forward and up vectors."; }}
	    
	    
	    // Declare variables here
	    
	    public override void AssignValues (List<ActionParameter> parameters)
	    {
			runtimeTeleporter = AssignFile <Marker> (parameters, markerParameterID, markerID, teleporter);		    
	    }

	    public override float Run ()
	    {
		if (runtimeTeleporter != null) {
		    XRRig xrRig = KickStarter.player.GetComponent<XRRig>();
		    if (xrRig != null) {

			xrRig.MatchRigUpCameraForward(runtimeTeleporter.Rotation * Vector3.up, runtimeTeleporter.Rotation * Vector3.forward);
			
			Vector3 heightAdjustment = xrRig.rig.transform.up * xrRig.cameraInRigSpaceHeight;
			Vector3 cameraDestination = runtimeTeleporter.Transform.position + heightAdjustment;
			xrRig.MoveCameraToWorldLocation(cameraDestination);
		    }		    
		}		
		
		if (!isRunning)
		{
		    isRunning = true;
		    return defaultPauseTime;
		}
		else
		{
		    isRunning = false;
		    return 0f;
		}
	    }

	    
	    public override void Skip ()
		{
		    /*
		     * This function is called when the Action is skipped, as a
		     * result of the player invoking the "EndCutscene" input.
		     * 
		     * It should perform the instructions of the Action instantly -
		     * regardless of whether or not the Action itself has been run
		     * normally yet.  If this method is left blank, then skipping
		     * the Action will have no effect.  If this method is removed,
		     * or if the Run() method call is left below, then skipping the
		     * Action will cause it to run itself as normal.
		     */
		    
		    Run ();
		}
	    
		#if UNITY_EDITOR
	    
	    public override void ShowGUI (List<ActionParameter> parameters)
	    {
		
		markerParameterID = Action.ChooseParameterGUI ("Teleport to:", parameters, markerParameterID, ParameterType.GameObject);
		if (markerParameterID >= 0)
		{
		    markerID = 0;
		    teleporter = null;
		}
		else
		{
		    teleporter = (Marker) EditorGUILayout.ObjectField ("Teleport to:", teleporter, typeof (Marker), true);
		    
		    markerID = FieldToID <Marker> (teleporter, markerID);
		    teleporter = IDToField <Marker> (teleporter, markerID, false);
		}
	    }
	    
	    
	    public override void AssignConstantIDs (bool saveScriptsToo, bool fromAssetFile)
	    {		
		AssignConstantID <Marker> (teleporter, markerID, markerParameterID);		    
		}
	    
	    
	    public override string SetLabel ()
	    {
		// (Optional) Return a string used to describe the specific action's job.
		
		return string.Empty;
	    }
	    
		#endif
	    
	}

}
