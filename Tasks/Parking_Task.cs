using UnityEngine;

public class Parking_Task : TaskInterface
{
    public void Execute(DeviceRegistry devices)
    {
        if (devices.memory[(int)Memory.parking] != 1) return;

        ParkStage stage = (ParkStage)devices.memory[(int)Memory.park_stage];

        switch (stage)
        {
            case ParkStage.Approach:
                // Approach the parking spot
                if (devices.lidar[4] > 4)
                {
                    devices.speedControl[0] = 1f;
                    devices.speedControl[1] = 2f; // Slow approach
                    devices.steeringControl[0] = 1f;
                    devices.steeringControl[1] = 0f; // Straight
                }
                else
                {
                    devices.speedControl[1] = 0f; // Stop
                    devices.memory[(int)Memory.park_stage] = (int)ParkStage.TurnIn;
                }
                break;

            case ParkStage.TurnIn:
                // Turn into the parking spot
                devices.steeringControl[1] = 90f; // Turn right
                if (devices.compass[0] > 85 && devices.compass[0] < 95) // Check if turned enough
                {
                    devices.steeringControl[1] = 0f; // Straighten out
                    devices.memory[(int)Memory.park_stage] = (int)ParkStage.Adjust;
                }
                break;

            case ParkStage.Adjust:
                // Adjust position within the parking spot
                if (devices.lidar[0] > 2)
                {
                    devices.speedControl[1] = 1f; // Move forward slowly
                }
                else
                {
                    devices.speedControl[1] = 0f; // Stop
                    devices.memory[(int)Memory.parking] = 0; // Parking complete
                    Debug.Log("Parking complete!");
                }
                break;
        }
    }
}
