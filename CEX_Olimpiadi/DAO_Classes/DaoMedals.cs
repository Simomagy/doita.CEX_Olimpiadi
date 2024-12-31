#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: DaoMedals.cs
// * Edited on 2024/12/22 at 21:12:55
// --------------------------------

#endregion

#region

using CEX_Olimpiadi.Classes;
using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.DAO_Classes;

public class DaoMedals : IDAO
{
    /// <inheritdoc />
    public List<Entity> GetRecords()
    {
        const string query = $"SELECT * FROM Medals";
        List<Entity> medalsRecords = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return medalsRecords;
        foreach (var singleResponse in fullResponse)
        {
            Entity entity = new Medal();
            entity.TypeSort(singleResponse);
            medalsRecords.Add(entity);
        }

        return medalsRecords;
    }

    /// <inheritdoc />
    public bool CreateRecord(Entity entity)
    {
        var medal = (Medal)entity;
        var parameters = new Dictionary<string, object>
        {
            { "@AthleteID", medal.Athlete?.Id ?? 0 },
            { "@CompetitionId", medal.Competition?.Id ?? 0 },
            { "@EventId", medal.Event?.Id ?? 0 },
            { "@MedalTier", medal.MedalTier.Replace("'", "''") }
        };
        const string query =
            $"INSERT INTO Medals (AthleteID, CompetitionId, EventId, MedalTier) VALUES (@AthleteID, @CompetitionId, @EventId, @MedalTier)";

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool UpdateRecord(Entity entity)
    {
        const string query =
            "UPDATE Medals SET AthleteID = @AthleteID, CompetitionId = @CompetitionId, EventId = @EventId, MedalTier = @MedalTier WHERE Id = @Id";
        var medal = (Medal)entity;
        var parameters = new Dictionary<string, object>
        {
            { "@AthleteID", medal.Athlete?.Id ?? 0 },
            { "@CompetitionId", medal.Competition?.Id ?? 0 },
            { "@EventId", medal.Event?.Id ?? 0 },
            { "@MedalTier", medal.MedalTier.Replace("'", "''") },
            { "@Id", medal.Id }
        };

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool DeleteRecord(int recordId)
    {
        const string query = "DELETE FROM Medals WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public Entity? FindRecord(int recordId)
    {
        const string query = "SELECT * FROM Medals WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Athlete();
        entity.TypeSort(singleResponse);
        return entity;
    }

    public List<Medal> GetAllMedals()
    {
        const string query = @"
                SELECT 
                    m.Id AS MedalId, m.MedalTier, 
                    a.Id AS AthleteId, a.Name AS AthleteName, a.Surname, a.Dob, a.Country,
                    c.Id AS CompetitionId, c.Type, c.IsIndoor, c.IsTeamComp, c.Category,
                    e.Id AS EventId, e.Name AS EventName, e.Year, e.Location
                FROM Medals m
                JOIN Athletes a ON m.AthleteID = a.Id
                JOIN Competitions c ON m.CompetitionID = c.Id
                JOIN Events e ON m.EventID = e.Id";

        List<Medal> medalsRecords = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return medalsRecords;

        foreach (var singleResponse in fullResponse)
        {
            var medal = new Medal();
            medal.TypeSort(singleResponse);

            // Populate Athlete
            var athlete = new Athlete();
            athlete.TypeSort(singleResponse);
            medal.Athlete = athlete;

            // Populate Competition
            var competition = new Competition();
            competition.TypeSort(singleResponse);
            medal.Competition = competition;

            // Populate Event
            var eventEntity = new Event();
            eventEntity.TypeSort(singleResponse);
            medal.Event = eventEntity;

            medalsRecords.Add(medal);
        }

        return medalsRecords;
    }

    public List<Medal> GetAthleteMedals(int athleteId)
    {
        const string query = @"
        SELECT 
            m.Id AS MedalId, m.MedalTier, 
            a.Id AS AthleteId, a.Name, a.Surname, a.Dob, a.Country,
            c.Id AS CompetitionId, c.Type, c.IsIndoor, c.IsTeamComp, c.Category,
            e.Id AS EventId, e.Name, e.Year, e.Location
        FROM Medals m
        JOIN Athletes a ON m.AthleteID = a.Id
        JOIN Competitions c ON m.CompetitionID = c.Id
        JOIN Events e ON m.EventID = e.Id
        WHERE m.AthleteID = @AthleteID";

        var parameters = new Dictionary<string, object> { { "@AthleteID", athleteId } };
        List<Medal> medalsRecords = [];
        var fullResponse = _db.ReadDb(query, parameters);
        if (fullResponse == null)
            return medalsRecords;

        foreach (var singleResponse in fullResponse)
        {
            var medal = new Medal();
            medal.TypeSort(singleResponse);

            // Populate Athlete
            var athlete = new Athlete();
            athlete.TypeSort(singleResponse);
            medal.Athlete = athlete;

            // Populate Competition
            var competition = new Competition();
            competition.TypeSort(singleResponse);
            medal.Competition = competition;

            // Populate Event
            var eventEntity = new Event();
            eventEntity.TypeSort(singleResponse);
            medal.Event = eventEntity;

            medalsRecords.Add(medal);
        }

        return medalsRecords;
    }

    #region Singleton

    private static DaoMedals? _instance;
    private readonly IDatabase _db;

    private DaoMedals()
    {
        _db = new Database("Olimpiadi");
    }

    /// <summary>
    ///     Restituisce l'istanza del DaoMedals
    /// </summary>
    /// <returns>
    ///     Se presente, restituisce l'istanza del DaoMedals, altrimenti ne crea una nuova
    /// </returns>
    public static DaoMedals GetInstance()
    {
        return _instance ??= new DaoMedals();
    }

    #endregion
}
