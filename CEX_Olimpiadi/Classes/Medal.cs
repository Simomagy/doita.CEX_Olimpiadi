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

namespace CEX_Olimpiadi.Classes
{
    /// <summary>
    ///     Rappresenta una medaglia vinta da un atleta in una competizione
    /// </summary>
    public class Medal : Entity
    {
        public Athlete? Athlete { get; set; }
        public Competition? Competition { get; set; }
        public Event? Event { get; set; }
        public string MedalTier { get; set; } = string.Empty;

        /// <inheritdoc />
        public override string ToString() =>
            $@"
                  --------------------------------
                  MEDAGLIA {MedalTier}
                  --------------------------------";
    }
}
