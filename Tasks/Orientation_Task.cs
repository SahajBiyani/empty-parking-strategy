using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures the Ego vehicle is facing the correct initial direction
public class Orientation_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices) {
        float current_angle = devices.compass[0];
        if (devices.memory[(int)Memory.oriented] != 1)
            rotateToward(devices, devices.memory[(int)Memory.target_angle]);
    }

    // Rotate toward the given target angle, until we have rotates to
    // the target angle.
    // (I assume gradual rotation, but it seems the Ego rotates instantaneously.)
    private void rotateToward(DeviceRegistry devices, float target_angle) {
        float current_angle = devices.compass[0];
        float relative_angle = target_angle - current_angle;

        if (Mathf.Abs(relative_angle) < 1) { // Assume imprecise rotation.
            devices.memory[(int)Memory.oriented] = 1;
            Debug.Log($"Oriented toward {target_angle}.");
            return;
        }

        devices.steeringControl[0] = 1f;
        devices.steeringControl[1] = relative_angle;
    }
}
