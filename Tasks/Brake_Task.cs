using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The emergency-brake task.
// Polls color camera, to ensure the way ahead is safe.
public class Brake_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices) {
        if (!isFacingRoad(devices))
            enableBrake(devices, 1);
        else
            disableBrake(devices);
    }

    // Apply the breaks and set our target-speed to 0m/s, and
    // trips the brake flag in memory.
    private void enableBrake(DeviceRegistry devices, float value) {
        if (devices.memory[(int)Memory.brake] == value)
            return;
        devices.speedControl[0] = 1f;
        devices.speedControl[1] = 0f;
        devices.brakeControl[0] = 1f;
        devices.brakeControl[1] = 5f;

        devices.memory[(int)Memory.brake] = value;
        Debug.Log("Brake enabled!");
    }

    // Disables the brake, and un-trips the brake flag in memory.
    private void disableBrake(DeviceRegistry devices) {
        if (devices.memory[(int)Memory.brake] == 0f)
            return;
        devices.brakeControl[0] = 0f;
        devices.brakeControl[1] = 0f;

        devices.memory[(int)Memory.brake] = 0f;
        Debug.Log("Brake disabled!");
    }

    // Whether or not we are facing safe terrain: Road.
    private bool isFacingRoad(DeviceRegistry devices) {
        float r = devices.pixels[5,7,0];
        float b = devices.pixels[5,7,1];
        float g = devices.pixels[5,7,2];

        bool road = (r == 24 && b == 24 && g == 24);
        return road;
    }
}
