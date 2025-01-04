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
    /// <summary>
    ///     Tipo di competizione
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    ///     Indica se la competizione è stata svolta al chiuso
    /// </summary>
    public bool IsIndoor { get; set; }

    /// <summary>
    ///     Indica se la competizione è a squadre
    /// </summary>
    public bool IsTeamComp { get; set; }

    /// <summary>
    ///     Categoria della competizione
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    ///     Id dell'evento a cui appartiene la competizione
    /// </summary>
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
