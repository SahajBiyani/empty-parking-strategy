// ... other usings

public class TaskList : MonoBehaviour
{
    protected TaskInterface[] tasks = new TaskInterface[] {
        //new Debug_Task(),
        new Orientation_Task(),
        new Brake_Task(),
        new Movement_Task(),
        new Position_Task(),
        new Perception_Task(),
        new Parking_Task() // Add the Parking_Task
    };

    // ... (rest of the code remains the same)
}
return this.tasks;
    }
}
