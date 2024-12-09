using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices) {
        if (devices.memory[(int)Memory.parking] == 1) return; // Let Parking_Task handle movement during parking

        float x = devices.gps[0];
        float z = devices.gps[1];
        float angle = devices.memory[(int)Memory.target_angle];
        float last_point = devices.memory[(int)Memory.points];

        // Basic wall following logic, prioritizing right turns
        if (devices.lidar[4] > 5 && devices.memory[(int)Memory.avoid_obstacle] == 0) { // Check right side for space
            devices.memory[(int)Memory.oriented] = 0f;
            devices.memory[(int)Memory.target_angle] = 90f; // Turn right
        } else if (devices.lidar[0] > 5 && devices.memory[(int)Memory.avoid_obstacle] == 0) { // Check forward
            devices.memory[(int)Memory.oriented] = 0f;
            devices.memory[(int)Memory.target_angle] = 179.9f; // Go straight
        } else if (devices.lidar[12] > 5 && devices.memory[(int)Memory.avoid_obstacle] == 0) { // Check left
            devices.memory[(int)Memory.oriented] = 0f;
            devices.memory[(int)Memory.target_angle] = -90f; // Turn left
        }

        // Check for parking spot
        if (inMiddleOfParkingSpace(x) && devices.memory[(int)Memory.found_spot] == 0) {
            devices.memory[(int)Memory.check_spot] = 1;
        }
    }

    // ... (rest of the code remains the same)
}
    }
        } else if (inMiddleOfParkingSpace(x))
            devices.memory[(int)Memory.check_spot] = 1;
    }

    // Return whether or not the Ego is in-between parking spaces, based on its
    // Z position.
    private bool inMiddleOfParkingSpace(float x_position) {
        for (int i=0; i<9; i++) {
            double middle_spot = -14.5 + 3.2*i + 1.6;
            Debug.Log($"{x_position} v {middle_spot}");
            if ((x_position > middle_spot    && Mathf.Abs((float)x_position - (float)middle_spot) < .25)
                || (middle_spot > x_position && Mathf.Abs((float)middle_spot - (float)x_position) < .25))
                return true;
        }
        return false;
    }

    private void navigateToStartPosition(DeviceRegistry devices, float x, float z) {
        if (aboveRows(x, z)) {
            if (devices.memory[(int)Memory.target_angle] != 180) {
                devices.memory[(int)Memory.oriented] = 0f;
                devices.memory[(int)Memory.target_angle] = 179.9f;
            }
        } else {
            devices.memory[(int)Memory.oriented] = 0f;
            devices.memory[(int)Memory.target_angle] = 90f;
            devices.memory[(int)Memory.points] = 1;
        }
    }

    private bool aboveRows(float x, float z)       { return z >  27; }
    private bool belowRows(float x, float z)       { return z < -26; }
    private bool leftBesideRows(float x, float z)  { return x < -13.4; }
    private bool rightBesideRows(float x, float z) { return x >  14.7; }
}
