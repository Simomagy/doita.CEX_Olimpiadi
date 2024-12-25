#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: Event.cs
// * Edited on 2024/12/22 at 21:12:54
// --------------------------------

#endregion

#region

using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.Classes
{
    /// <summary>
    ///     Rappresenta un evento sportivo
    /// </summary>
    public class Event : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Location { get; set; } = string.Empty;

        /// <summary>
        ///     Rappresenta i dati di un evento sportivo in formato stringa
        /// </summary>
        /// <returns>
        ///     Una stringa contenente le informazioni dell'evento
        /// </returns>
        public override string ToString() =>
            base.ToString() +
            $@"
                  --------------------------------
                  EVENTO
                  --------------------------------
                  Nome: {Name}
                  Anno: {Year}
                  Luogo: {Location}
                  --------------------------------
                  ";
    }
}
