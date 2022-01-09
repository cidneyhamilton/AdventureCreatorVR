/**
 * A Continuous Movement, custom for our game
 *
 * Designed by Roberta Williams to emulate wheelchair movement.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using AC;

namespace CC.VR {

    public class CustomMovement : ActionBasedContinuousMoveProvider
    {

	// Toggle to enable the controllers for movement
	public bool JoystickMovementEnabled;
	
	// Reference to the snap turn provider, which is enabled when paused
	public SnapTurnProviderBase snapTurnProvider;

	// Reference to the continuous turn provider, which is enabled when moving
	public ContinuousTurnProviderBase continuousTurnProvider;

	// Turn speed when rotating the joystick
	public float turnSpeed = 20.0f;

	// Speed when backing up
	public float backupMoveSpeed = 1.0f;

	// The max amount of time the player can back up
	const float maxBackupCount = 1.0f;

	const string GAME_MODE = "currentGameMode";
        const string SPEED_VAR = "Environment/walkSpeed";
        const string STOPPED = "stopped";
        const string WALKING = "walking";

        // How many seconds the player has been backing up for
        private float currentBackupCount = 0.0f;

	// True if the player can back up
	bool canBackup;

	// Used to tell if the move input has changed;
	// ie, if we're pressing down on the Back button
	Vector2 lastMoveInput;

	// Reference to the left hand position
	[SerializeField]
	InputActionProperty m_LeftHandPosition;

	// Reference to the left hand rotation
	[SerializeField]
	InputActionProperty m_LeftHandRotation;

	// Get the movement vector from the move action
	protected Vector2 MoveVector() {
	    return leftHandMoveAction.action?.ReadValue<Vector2>() ?? Vector2.zero;
	}

	// Get the rotation from the rotation action
	protected Quaternion Rotation() {
	    return m_LeftHandRotation.action?.ReadValue<Quaternion>() ?? Quaternion.identity;
	}

	// Get the controller position from the position action
	protected Vector3 ControllerVector() {
	    return m_LeftHandPosition.action?.ReadValue<Vector3>() ?? Vector3.zero;
	}
	
	void Start() {
	    if (snapTurnProvider == null) {
		snapTurnProvider = GetComponent<SnapTurnProviderBase>();
	    }

	    if (continuousTurnProvider == null) {
		continuousTurnProvider = GetComponent<ContinuousTurnProviderBase>();
	    }
	}

	// Enable backup for time seconds
	IEnumerator StartBackupTimer(float time) {
	    canBackup = true;	    
	    yield return new WaitForSeconds(time);
	    canBackup = false;
	}
	
	protected override void Update() {
	    Vector2 input = MoveVector();
	
	    if (KickStarter.stateHandler.gameState == GameState.Paused) {
		snapTurnProvider.enabled = false;
		continuousTurnProvider.enabled = false;
	    } else if (input == Vector2.zero) {
		Stop();
	    } else if (input == Vector2.down) {
		MoveBackward();
		if (canBackup)
                {
		    base.Update();
		}
				
	    }  else if (input == Vector2.up) {
		MoveForward();
		base.Update();
	    }

	    lastMoveInput = input;

	}

        void MoveForward()
        {
            // We're moving forward
            snapTurnProvider.enabled = false;
            continuousTurnProvider.enabled = true;

            // Needed by Ken's CC_FootstepHandler
            if (GlobalVariables.GetVariable(GAME_MODE) != null)
            {
                GlobalVariables.GetVariable(GAME_MODE).TextValue = WALKING;
            }

            if (GlobalVariables.GetVariable(SPEED_VAR) != null)
            {
                moveSpeed = GlobalVariables.GetVariable(SPEED_VAR).FloatValue;
            } else {
                // No speed variable defined; use a default
                moveSpeed = 2.0f;
            }
	    // If joystick locomotion is enabled, turn the rig
	    if (JoystickMovementEnabled)
	    {
		TurnRig(GetTurnAmount(Rotation()));
	    }
	}
		
	void MoveBackward()
	{
	    // We're backing up; no turning
	    snapTurnProvider.enabled = false;
	    continuousTurnProvider.enabled = false;
		    
	    // Needed by Ken's CC_FootstepHandler
	    if (GlobalVariables.GetVariable(GAME_MODE) != null)
	    {
		GlobalVariables.GetVariable(GAME_MODE).TextValue = WALKING;
	    }

	    if (lastMoveInput != Vector2.down)
	    {
		// Start backup timer
		// Only back up a few feet
		StartCoroutine(StartBackupTimer(maxBackupCount));
	    }

	    if (canBackup)
	    {
		moveSpeed = backupMoveSpeed;
	    }
	}

	void Stop()
	{
	    // We're not moving
	    snapTurnProvider.enabled = true;
	    continuousTurnProvider.enabled = false;

	    // Needed by Ken's CC_FootstepHandler
	    if (GlobalVariables.GetVariable(GAME_MODE) != null)
	    {
		GlobalVariables.GetVariable(GAME_MODE).TextValue = STOPPED;
	    }
	}

	// Update the forward source to point in the direction of the camera, so the player always moves straight ahead when they start moving
	void UpdateForwardSource()
        {
	    float rotY = GetComponentInChildren<Camera>().transform.rotation.eulerAngles.y;
	    forwardSource.rotation = Quaternion.Euler(0, rotY, 0);
        }

	// Get a human-readable turn amount from a Quaternion
	float GetTurnAmount(Quaternion input) {
	    float sign = 0;
	    float rotY = input.eulerAngles.y;
	    if (rotY > 180 && rotY < 350) {
		sign = -1;
	    } else if (rotY < 180 && rotY > 10){
		sign = 1; 
	    }
	    return sign * turnSpeed * Time.deltaTime;
	}
	
	void TurnRig(float turnAmount) {
	    system.xrRig.RotateAroundCameraUsingRigUp(turnAmount);	    
	}
	
	
    }
    
}
