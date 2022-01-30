
/// <summary>
/// An interface for easily checking duality state
/// </summary>
public interface IHaveDuality
{
    /// <summary>
    /// Whether or not the thing is in light mode
    /// </summary>
    /// <returns>true if in light mode, false otherwise</returns>
    public bool IsInLightMode();
}