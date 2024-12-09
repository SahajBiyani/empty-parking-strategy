using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices) {
        if (devices.memory[(int)Memory.parking] == 1) return; // Don't interfere with parking procedure

        bool in_middle_of_parking_space = devices.memory[(int)Memory.check_spot] == 1;
        float right_distance = devices.lidar[4];

        // Check for obstacles and initiate avoidance
        if (devices.lidar[0] < 4) {
            devices.memory[(int)Memory.avoid_obstacle] = 1; // Set obstacle avoidance flag
            devices.memory[(int)Memory.target_angle] = -90f; // Turn left to avoid
        } else if (devices.memory[(int)Memory.avoid_obstacle] == 1 && devices.lidar[0] > 5) {
            devices.memory[(int)Memory.avoid_obstacle] = 0; // Clear flag when obstacle is cleared
        }


        if (in_middle_of_parking_space && right_distance > 5 && devices.memory[(int)Memory.found_spot] == 0) {
            devices.memory[(int)Memory.found_spot] = 1;
            devices.memory[(int)Memory.parking] = 1; // Initiate parking procedure
            devices.memory[(int)Memory.park_stage] = (int)ParkStage.Approach;
            Debug.Log("Found spot, initiating parking procedure.");
        } else if (in_middle_of_parking_space) {
            devices.memory[(int)Memory.check_spot] = 0;
        }
    }

    // ... (rest of the code remains the same)
}
