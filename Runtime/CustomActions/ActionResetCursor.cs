/**
 * ActionToggleLocomotion.cs
 *
 * Enable or disable the VR locomotion system
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using CC.VR;

namespace AC {
    
    public class ActionResetCursor : Action
    {
	
	public override ActionCategory Category { get { return ActionCategory.Player; }}
	public override string Title { get { return "Reset XR Cursors"; }}
	public override string Description { get { return "Reset to the default XR cursor."; }}

	public override float Run() {
	    VREvents.ResetCursor();
	    return 0f;
	}
    }

}
