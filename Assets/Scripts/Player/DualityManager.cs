using UnityEngine;
public class DualityManager : MonoBehaviour, IHaveDuality, IHaveDashTokens
{

    /// <summary>
    /// How much does light mode drain by per second?
    /// </summary>
    public float lightModeDrainRate;


    /// <summary>
    /// Forcibly override the duality manager thingy
    /// </summary>
    public enum DualityOverrideEnum
    {

        /// <summary>
        /// duality manager operates normally (usually in dark mode, have to fill up light mode meter in order to use light mode, being in light mode drains meter)
        /// </summary>
        NO_OVERRIDE,

        /// <summary>
        /// forced into dark mode (cannot accumulate light mode, stuck in dark mode)
        /// </summary>
        FORCE_DARK,
        /// <summary>
        /// forced into light mode (light mode stuck at maximum, cannot swap to dark mode)
        /// </summary>
        FORCE_LIGHT,
        /// <summary>
        /// freely able to swap between dark mode and light mode (light mode meter stuck at maximum, no restrictions on swapping)
        /// </summary>
        FORCE_FREE_SWAPPING
    }


    /// <summary>
    /// Choose how you want to override the behaviours of the duality manager.
    /// </summary>
    public DualityOverrideEnum dualityOverride = DualityOverrideEnum.NO_OVERRIDE;


    /// <summary>
    /// The maximum light mode possible
    /// </summary>
    public float maxLightMode;

    /// <summary>
    /// Current amount of light mode that the player has
    /// </summary>
    public float _lightModeRemaining;


    [SerializeField]
    public StateMovementStats _LightModeMoveStats;

    [SerializeField]
    public StateMovementStats _DarkModeMoveStats;


    /// <summary>
    /// Maximum number of DASHES that can be used
    /// </summary>
    public int _MaxDashes;

    /// <summary>
    /// Current dashes available
    /// </summary>
    public int _currentDashes;


    /// <summary>
    /// How many dash TOKENS are needed for a single dash
    /// </summary>
    public int _dashTokensNeededForADash;


    /// <summary>
    /// limit of how many dash TOKENS can be held
    /// </summary>
    private int _maxDashTokens;

    /// <summary>
    /// How many dash TOKENS are currently held?
    /// </summary>
    private int _currentDashTokens;


    /// <summary>
    /// Re-override the dualityoverrideenum with a different duality.
    /// </summary>
    /// <param name="newOverride">the new thing to override the duality with</param>
    public void SetNewOverride(DualityOverrideEnum newOverride)
    {
        dualityOverride = newOverride;
    }




    void Awake()
    {

        _maxDashTokens = _MaxDashes * _dashTokensNeededForADash;

        _currentDashes = Mathf.Min(_MaxDashes, _currentDashes);

        _currentDashTokens = _currentDashes * _dashTokensNeededForADash;




        //_lightModeRemaining = 100;

        //UnityEngine.Debug.Log(_LightModeMoveStats.ToString());

        //UnityEngine.Debug.Log(_DarkModeMoveStats.ToString());
    }

    void Start()
    {
        //UnityEngine.Debug.Log(_LightModeMoveStats.ToString());

        //UnityEngine.Debug.Log(_DarkModeMoveStats.ToString());
    }


    void Update()
    {

        switch (dualityOverride)
        {
            // normal behaviour if duality not being overridden
            case DualityOverrideEnum.NO_OVERRIDE:
                if (Lightmode)
                {
                    _lightModeRemaining -= lightModeDrainRate * Time.deltaTime;
                    ValidateLightMode();
                }
                break;
            // forced into 0 light remaining if forced into dark
            case DualityOverrideEnum.FORCE_DARK:
                _lightModeRemaining = 0;
                ValidateLightMode();
                break;
            // forced into maximum possible light remaining if forced into light or forced into freely being able to swap between the two
            case DualityOverrideEnum.FORCE_LIGHT:
            case DualityOverrideEnum.FORCE_FREE_SWAPPING:
                _lightModeRemaining = maxLightMode;
                ValidateLightMode();
                break;
        }
    }

    /// <summary>
    /// Take away a raw amount of light mode from the remaining light mode
    /// </summary>
    /// <param name="drainAmount">How much of the remaining light mode to drain</param>
    public void DrainLightModeRaw(float drainAmount)
    {
        _lightModeRemaining -= drainAmount;
        ValidateLightMode();
    }

    /// <summary>
    /// Take away a scaled amount of light mode (as a value between 0 and 1, 1=max possible) from the remaining light mode
    /// </summary>
    /// <param name="proportionalDrain">How much of the remaining light mode to drain</param>
    public void DrainLightMode(float proportionalDrain)
    {
        DrainLightModeRaw(proportionalDrain * maxLightMode);
    }

    /// <summary>
    /// Add a raw amount of light mode from the remaining light mode
    /// </summary>
    /// <param name="drainAmount">How much light mode to add</param>
    public void AddLightModeRaw(float addAmount)
    {
        _lightModeRemaining += addAmount;
        ValidateLightMode();
    }

    /// <summary>
    /// Add a away a scaled amount of light mode (as a value between 0 and 1, 1=max possible) to the remaining light mode
    /// </summary>
    /// <param name="proportionalAdd">How much light mode (proportionally to max possible) to add?</param>
    public void AddLightMode(float proportionalAdd)
    {
        AddLightModeRaw(proportionalAdd * maxLightMode);
    }


    /// <summary>
    /// Tries to swap form.
    /// </summary>
    /// <returns>True if now in light mode, false if now in dark mode</returns>
    public bool AttemptSwapForm()
    {

        switch (dualityOverride)
        {

            // normal behaviour (only allowed to swap to light mode if there's light mode available) if no override
            case DualityOverrideEnum.NO_OVERRIDE:
                if (Lightmode)
                {
                    UnityEngine.Debug.Log("Swap from light to dark");
                    _lightmode = false;
                    return false;
                }
                else if (LightModeRemaining > 0)
                {
                    UnityEngine.Debug.Log("Swap from dark to light");
                    _lightmode = true;
                    return true;
                }
                UnityEngine.Debug.Log(string.Format("Cannot swap to light. Remaining {0}", LightModeRemaining));
                _lightmode = false;
                return false;
            // just swaps it as-is if overridden to permit free swapping
            case DualityOverrideEnum.FORCE_FREE_SWAPPING:
                _lightmode = !_lightmode;
                return _lightmode;
            // forced into dark mode
            case DualityOverrideEnum.FORCE_DARK:
                _lightmode = false;
                return false;
            // forced into light mode
            case DualityOverrideEnum.FORCE_LIGHT:
                _lightmode = true;
                return true;
        }
        // you should not be here.
        throw new UnityEngine.Assertions.AssertionException("DualityManager", "unexpected fallthrough out of switch case in AttemptSwapForm()");


    }


    /// <summary>
    /// Attempts to perform a dash.
    /// Will update the mode after a dash is performed.
    /// Returns true if dash is successful, returns false if dash cannot be performed.
    /// </summary>
    /// <returns>true if the dash can be performed, false if a dash currently cannot be performed.</returns>
    public bool AttemptDash()
    {
        if (dualityOverride == DualityOverrideEnum.FORCE_DARK) // no dashing if forced into dark mode
        {
            return false;
        }
        if (Lightmode && (_currentDashes > 0) && (_currentDashTokens >= _dashTokensNeededForADash))
        {
            _currentDashes -= 1;
            _currentDashTokens -= _dashTokensNeededForADash;
            return true;
        }
        return false;
    }

    /// <summary>
    /// How many dashes are left?
    /// </summary>
    /// <value>count of remaining dashes</value>
    public int DashesRemaining
    {
        get { return _currentDashes; }
    }

    public int GetCurrentDashes()
    {
        return DashesRemaining;
    }

    /// <summary>
    /// DASH TOKEN GET! (maybe)
    /// </summary>
    /// <param name="tokensToAdd">how many tokens to add</param>
    /// <returns>However many of the new dash tokens could be added to the stockpile</returns>
    public int AddDashToken(int tokensToAdd)
    {
        if (_currentDashTokens == _maxDashTokens)
        {
            return 0;
        }
        _currentDashTokens += tokensToAdd;
        int not_added = 0;
        if (_currentDashTokens > _maxDashTokens)
        {

            not_added = _currentDashTokens - _maxDashTokens;
            _currentDashTokens = _maxDashTokens;
        }
        _currentDashes = Mathf.FloorToInt(_currentDashTokens / _dashTokensNeededForADash);

        if (_currentDashes > _MaxDashes)
        {
            _currentDashes = _MaxDashes;
        }
        return not_added;
    }


    /// <summary>
    /// How many dash TOKENS are currently available
    /// </summary>
    /// <returns>current count of dash TOKENS</returns>
    public int GetCurrentDashTokens()
    {
        return _currentDashTokens;
    }

    /// <summary>
    /// Tries to add a FULL dash (aka 'enough dash tokens to perform a full dash')
    /// </summary>
    /// <returns>true if they could be added.</returns>
    public bool AddAFullDash()
    {

        return AddDashToken(_dashTokensNeededForADash) == _dashTokensNeededForADash;
    }


    /// <summary>
    /// Ensures that _lightModeRemaining remains in the range between 0 and maxLightMode (inclusive)
    /// </summary>
    void ValidateLightMode()
    {
        if (_lightModeRemaining < 0)
        {
            _lightModeRemaining = 0;
        }
        else if (_lightModeRemaining > maxLightMode)
        {
            _lightModeRemaining = maxLightMode;
        }

    }

    /// <summary>
    /// returns how much light mode is left
    /// </summary>
    /// <value></value>
    public float LightModeRemaining
    {
        get { return _lightModeRemaining; }
    }

    /// <summary>
    /// returns remaining light mode as a value between 0 and 1
    /// </summary>
    /// <value></value>
    public float LightMode01
    {
        get { return _lightModeRemaining / maxLightMode; }
    }

    /// <summary>
    /// internal record of how much light mode is remaining
    /// </summary>
    bool _lightmode;

    /// <summary>
    /// Getter/setter for light mode.
    /// Light mode is automatically cancelled when no light mode is remaining.
    /// </summary>
    /// <value>new value for lightmode</value>
    public bool Lightmode
    {
        get
        {
            switch (dualityOverride)
            {
                case DualityOverrideEnum.NO_OVERRIDE:
                    if (_lightmode && _lightModeRemaining <= 0)
                    {
                        // lightmode forced to stop if there's no light mode left
                        _lightmode = false;
                    }
                    return _lightmode;
                case DualityOverrideEnum.FORCE_DARK:
                    return false; // no light if forced into dark
                case DualityOverrideEnum.FORCE_LIGHT:
                    return true; // always light if forced into light
                case DualityOverrideEnum.FORCE_FREE_SWAPPING:
                    // no funny business, returned as-is when free swapping
                    return _lightmode;
            }
            // you should not be here.
            throw new UnityEngine.Assertions.AssertionException("DualityManager", "unexpected fallthrough out of switch case in LightMode getter");
        }

        set
        {
            switch (dualityOverride)
            {
                // normal behaviour only permits light mode if there's light mode available
                case DualityOverrideEnum.NO_OVERRIDE:

                    if (_lightModeRemaining > 0)
                    {
                        this._lightmode = value;
                    }
                    else
                    {
                        this._lightmode = false;
                    }
                    break;
                // forced into light mode
                case DualityOverrideEnum.FORCE_LIGHT:
                    this._lightmode = true;
                    break;
                // forced into dark mode
                case DualityOverrideEnum.FORCE_DARK:
                    this._lightmode = false;
                    break;
                // allowed to swap freely
                case DualityOverrideEnum.FORCE_FREE_SWAPPING:
                    this._lightmode = value;
                    break;
            }


        }
    }

    public bool IsInLightMode()
    {
        // already handled by the DualityOverrideEnum
        return Lightmode;
    }

    /// <summary>
    /// Get the appropriate movement stats for the current state
    /// </summary>
    /// <value></value>
    public StateMovementStats CurrentMoveStats
    {
        get
        {
            // already handled by the DualityOverrideEnum
            if (Lightmode)
            {
                return _LightModeMoveStats.validate();
            }
            else
            {
                return _DarkModeMoveStats.validate();
            }
        }
    }




}