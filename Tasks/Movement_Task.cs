using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices) {
        float oriented = devices.memory[(int)Memory.oriented];
        float brake = devices.memory[(int)Memory.brake];

        // Only move forward if oriented correctly and brake is not applied
        if (oriented == 1 && brake == 0)
            MoveForward(devices);
        else if (oriented == 0)
            StopMovement(devices);
    }

    private void MoveForward(DeviceRegistry devices) {
        devices.speedControl[0] = 1f; // Enable movement
        devices.speedControl[1] = 5f; // Set forward speed
    }

    private void StopMovement(DeviceRegistry devices) {
        devices.speedControl[0] = 0f; // Stop movement
        devices.speedControl[1] = 0f; // Set speed to zero
	}
}
