/*
 * ActionCheckMenuOpen.cs
 *
 * Returns true if the Pause menu is open
 * Used to handle teleporting the player to interact with menus in VR
	*/

using System;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

				[System.Serializable]
				public class ActionCheckMenuOpen : ActionCheck
				{

								// Harcoded list of menus to include in the Pause Menu
								// TODO: Allow this to be specified in the editor?	    
								public enum MenuNames { Pause, Options, RetroSaves, Profiles, Inventory, Help, Document };

								// Declare properties here
								public override ActionCategory Category { get { return ActionCategory.Menu; }}
								public override string Title { get { return "Open"; }}
								public override string Description { get { return "Returns true if any of the menus are open."; }}


								// Declare variables here		
		
								public override bool CheckCondition ()
								{

												// Returns true if any of the other menus are open; false if they are not
												string[] menuNames = Enum.GetNames(typeof(MenuNames));
												foreach(string menuName in menuNames) {
																Menu menu = PlayerMenus.GetMenuWithName(menuName);
																if (menu != null && menu.IsVisible()) {
																				return true;
																}
												}
												return false;
								}

		
				}

}
