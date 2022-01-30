
public interface IHaveDashTokens
{

    /// <summary>
    /// Obtain the current number of dashes which are available
    /// </summary>
    /// <returns>however many dashes are available for use</returns>
    public int GetCurrentDashes();


    /// <summary>
    /// DASH TOKEN GET! (maybe)
    /// </summary>
    /// <param name="tokensToAdd">how many tokens to add</param>
    /// <returns>However many of the new dash tokens could be added to the stockpile</returns>
    public int AddDashToken(int tokensToAdd);


    /// <summary>
    /// How many dash TOKENS are currently available
    /// </summary>
    /// <returns>current count of dash TOKENS</returns>
    public int GetCurrentDashTokens();

    /// <summary>
    /// Tries to add a FULL dash (aka 'enough dash tokens to perform a full dash')
    /// </summary>
    /// <returns>true if they could be added.</returns>
    public bool AddAFullDash();
}