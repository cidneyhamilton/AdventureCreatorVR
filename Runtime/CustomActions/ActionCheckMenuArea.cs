using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

				[System.Serializable]
				public class ActionCheckMenuArea : ActionCheck
				{


								// Position of the UI marker
								// Use this to determine if the player is offscreen	    
								const int MENU_POSITION_THRESHOLD = -900;
	    
								// Declare properties here
								public override ActionCategory Category { get { return ActionCategory.Menu; }}
								public override string Title { get { return "In Area"; }}
								public override string Description { get { return "Returns true if the player has been teleported to the Menu area."; }}
		
								public override bool CheckCondition ()
								{
												// Checks 
												return KickStarter.player.transform.position.x < MENU_POSITION_THRESHOLD;
								}

		
				}

}
