using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AC 
{ 
    [System.Serializable]
    public class ActionSetSaveGameName : Action
    {

        // Declare properties here
        public override ActionCategory Category { get { return ActionCategory.Variable; } }
        public override string Title { get { return "Set Save Name"; } }
        public override string Description { get { return "Set save game name to the name of the current scene."; } }

        private GVar saveNameVar;
        const string SAVE_NAME = "Save name";

		public override float Run()
		{
            saveNameVar = GlobalVariables.GetVariable(SAVE_NAME);
            Debug.Log(saveNameVar.GetValue());
            if (saveNameVar.GetValue() == "New Save")
            {
                saveNameVar.SetStringValue(GetStartingText() + " " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString());
            }
            return 0f;
		}

		public override void Skip()
		{
			Run();
		}

		// Get the default name for a room
		// TODO: Use Ken's short and long names for rooms
		string GetStartingText()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }


    }

}
