using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using AC;

namespace CC.VR
{

    // Attach to a canvas to set it up for VR
    [RequireComponent(typeof(Canvas))]
    public class VRCanvas : MonoBehaviour
    {

        public Vector3 worldspacePosition = new Vector3(-999f, 1f, -998f);
        public Vector3 worldspaceScale = new Vector3(0.005f, 0.005f, 1f);

        Canvas canvas;
	Camera vrCamera;

	void Start() {
	    // If VR is enabled
	    if (BaseXRToggle.IsXRPresent()) {
		SetupCanvasCamera();
                Debug.Log("XR is present.");
                transform.position = worldspacePosition;
                transform.localScale = worldspaceScale;
                canvas.renderMode = RenderMode.WorldSpace;
            } else {
                Debug.Log("XR is not present.");		
            }
	}

        // Setup the camera attached to this camear
        void SetupCanvasCamera()
        {
	    if (AC.KickStarter.player != null)
	    {
		vrCamera = AC.KickStarter.player.GetComponentInChildren<Camera>();
		canvas = GetComponent<Canvas>();
		canvas.worldCamera = vrCamera;
		VREvents.DebugLog("Set up VRCamera for Canvas " + gameObject.name);
	    } else {
		VREvents.DebugLog("No player found for Canvas " + gameObject.name);
	    }
	    
	}

    }
}
