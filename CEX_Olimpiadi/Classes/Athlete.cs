#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: Athlete.cs
// * Edited on 2024/12/22 at 22:12:07
// --------------------------------

#endregion

#region

using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.Classes;

/// <summary>
///     Rappresenta un atleta
/// </summary>
public class Athlete : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public string Country { get; set; } = string.Empty;

    /// <summary>
    ///     Override per il metodo <see cref="Entity.ToString" />
    /// </summary>
    /// <returns>
    ///     Una stringa contenente le informazioni dell'atleta
    /// </returns>
    public override string ToString()
    {
        return $@"
                  --------------------------------
                  ATLETA
                  --------------------------------
                  Nome: {Name}
                  Cognome: {Surname}
                  Data di nascita: {Dob:dddd, dd/MM/yyyy}
                  Nazione: {Country}
                  --------------------------------";
    }
}
