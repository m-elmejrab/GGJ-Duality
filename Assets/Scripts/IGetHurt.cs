
/// <summary>
/// An interface to be implemented by things that can take environmental damage.
/// </summary>
public interface IGetHurt
{

    /// <summary>
    /// Take damage.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage);

    /// <summary>
    /// Kill the thing (takes 100% damage)
    /// </summary>
    public void Kill();
}