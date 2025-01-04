#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: Medal.cs
// * Edited on 2024/12/22 at 22:12:07
// --------------------------------

#endregion

#region

using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.Classes;

/// <summary>
///     Rappresenta una medaglia vinta da un atleta in una competizione
/// </summary>
public class Medal : Entity
{
    /// <summary>
    ///     Dati dell'atleta che ha vinto la medaglia
    /// </summary>
    public Athlete? Athlete { get; set; }

    /// <summary>
    ///     Dati della competizione in cui è stata vinta la medaglia
    /// </summary>
    public Competition? Competition { get; set; }

    /// <summary>
    ///     Dati dell'evento in cui è stata vinta la medaglia
    /// </summary>
    public Event? Event { get; set; }

    /// <summary>
    ///     Tipo della medaglia vinta
    /// </summary>
    public string MedalTier { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $@"
                  --------------------------------
                  MEDAGLIA {MedalTier}
                  --------------------------------";
    }
}
