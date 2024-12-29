#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: Competition.cs
// * Edited on 2024/12/22 at 21:12:54
// --------------------------------

#endregion

#region

using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.Classes;

/// <summary>
///     Rappresenta una competizione sportiva
/// </summary>
public class Competition : Entity
{
    public string Type { get; set; } = string.Empty;
    public bool IsIndoor { get; set; }
    public bool IsTeamComp { get; set; }
    public string Category { get; set; } = string.Empty;
    public int EventId { get; set; }

    /// <summary>
    ///     Rappresenta i dati di una competizione sportiva in formato stringa
    /// </summary>
    /// <returns>
    ///     Una stringa contenente le informazioni della competizione
    /// </returns>
    public override string ToString()
    {
        return base.ToString() +
               $@"
                  --------------------------------
                  COMPETIZIONE
                  --------------------------------
                  Tipo: {Type}
                  Indoor: {(IsIndoor ? "Sì" : "No")}
                  Squadra: {(IsTeamComp ? "Sì" : "No")}
                  Categoria: {Category}
                  --------------------------------
                  ";
    }
}
