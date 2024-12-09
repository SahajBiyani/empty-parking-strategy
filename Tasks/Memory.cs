// Contains a simple enum for keeping track of memory values.
public enum Memory : int {
    brake,        // The emergency brake is tripped due to off-road (1), alarm (2), or not at all (0).
    oriented,     // Whether the Ego is rotated (1) or rotating (0) toward the current goal.
    target_angle, // The angle we should rotate to, as dictated by Navigation to Orientation.
    check_spot,   // Whether (1) or not (0) the Ego’s in the Z-middle of a parking-spot, requiring a camera check.
    found_spot,   // Indicates if a spot has been found (1) or not (0)
    parking,      // Indicates if the parking procedure is in progress (1) or not (0)
    park_stage,   // Tracks the current stage of the parking procedure
    points,       // Point last attained: Spawn (0), first/second road (1/2), or across the bridge (3).
    avoid_obstacle // Flag to indicate obstacle avoidance is active
}

// Enum for the attained locations, as stored in “points” in memory.
public enum Points {
    spawn = 1,
    first_row_a,
    first_row_b,
    second_row_a,
    second_row_b,
    third_row_a,
    third_row_b
}

// Enum for parking stages
public enum ParkStage {
    Approach,
    TurnIn,
    Adjust
}
