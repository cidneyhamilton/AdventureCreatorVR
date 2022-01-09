using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace CC.VR
{
    public class HandPresence : MonoBehaviour
    {

        // Specify the characteristics of the desired device
        public InputDeviceCharacteristics controllerCharacteristics;

        public GameObject handPrefab;
        private Animator animator;

        // Store a reference to the target device
        private InputDevice targetDevice;

        // Store this in case devices don't initialize immediately
        bool deviceFound;

        // Update is called once per frame
        void Update()
        {
            if (deviceFound)
            {
                UpdateHandAnimation();
                
            } else
            {
                TryInitializeDevice();
            }  
        }

        // Try to spawn a hand model for a device with the given controller characteristics
        void TryInitializeDevice()
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

            if (devices.Count > 0)
            {
                targetDevice = devices[0];
                deviceFound = true;
                GameObject handModel = Instantiate(handPrefab, transform);
                handModel.transform.parent = gameObject.transform;
                animator = handModel.GetComponent<Animator>();
            } 
        }
	
        void UpdateHandAnimation()
        {
            UpdateHandAnimation(CommonUsages.trigger, "Trigger");
            UpdateHandAnimation(CommonUsages.grip, "Grip");
        }

        // Update hand hanimation for a specified input feature use and animation name
        void UpdateHandAnimation(InputFeatureUsage<float> usage, string animationName)
        {
            if (targetDevice.TryGetFeatureValue(usage, out float useValue))
            {
                animator.SetFloat(animationName, useValue);
            }
            else
            {
                animator.SetFloat(animationName, 0);
            }

        }

    }

}
