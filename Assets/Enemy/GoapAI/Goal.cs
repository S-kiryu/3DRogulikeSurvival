using System.Collections.Generic;


public class Goal
{
    //—Dæ“x
    public int Priority { get; private set; }

    //—~‚µ‚¢ó‘Ô
    public Dictionary<string, bool> DesiredState { get; private set; }

    /// <summary>
    /// desiredState: —~‚µ‚¢ó‘Ô,priority: —Dæ“x
    /// </summary>
    /// <param name="desiredState"></param>
    /// <param name="priority"></param>
    public Goal(Dictionary<string, bool> desiredState, int priority)
    {
        DesiredState = desiredState;
        Priority = priority;
    }
}   
