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
    private const string TableName = "Medals";

    /// <inheritdoc />
    public List<Entity> GetRecords()
    {
        const string query = $"SELECT * FROM {TableName}";
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
        var athleteId = ((Medal)entity).Athlete?.Id ?? 0;
        var competitionId = ((Medal)entity).Competition?.Id ?? 0;
        var eventId = ((Medal)entity).Event?.Id ?? 0;
        var medalTier = ((Medal)entity).MedalTier.Replace("'", "''");
        var query =
            $"INSERT INTO {TableName} (AthleteID, CompetitionId, EventId, MedalTier) VALUES ({athleteId}, {competitionId}, {eventId}, '{medalTier}')";

        return _db.UpdateDb(query);
    }

    /// <inheritdoc />
    public bool UpdateRecord(Entity entity)
    {
        var athleteId = ((Medal)entity).Athlete?.Id ?? 0;
        var competitionId = ((Medal)entity).Competition?.Id ?? 0;
        var eventId = ((Medal)entity).Event?.Id ?? 0;
        var medalTier = ((Medal)entity).MedalTier.Replace("'", "''");
        var query =
            $"UPDATE {TableName} SET AthleteID = {athleteId}, CompetitionId = {competitionId}, EventId = {eventId}, MedalTier = '{medalTier}' WHERE Id = {entity.Id}";

        return _db.UpdateDb(query);
    }

    /// <inheritdoc />
    public bool DeleteRecord(int recordId)
    {
        var query = $"DELETE FROM {TableName} WHERE Id = {recordId}";
        return _db.UpdateDb(query);
    }

    /// <inheritdoc />
    public Entity? FindRecord(int recordId)
    {
        var query = $"SELECT * FROM {TableName} WHERE Id = {recordId}";
        var singleResponse = _db.ReadOneDb(query);
        if (singleResponse == null)
            return null;
        Entity entity = new Athlete();
        entity.TypeSort(singleResponse);
        return entity;
    }

    public List<Medal> GetMedals()
    {
        var query = @"
                SELECT 
                    m.Id AS MedalId, m.MedalTier, 
                    a.Id AS AthleteId, a.Name AS AthleteName, a.Surname AS AthleteSurname, a.Dob AS AthleteDob, a.Country AS AthleteCountry,
                    c.Id AS CompetitionId, c.Type AS CompetitionType, c.IsIndoor, c.IsTeamComp, c.Category AS CompetitionCategory,
                    e.Id AS EventId, e.Name AS EventName, e.Year AS EventYear, e.Location AS EventLocation
                FROM Medals m
                JOIN Athletes a ON m.AthleteID = a.Id
                JOIN Competitions c ON m.CompetitionID = c.Id
                JOIN Events e ON m.EventID = e.Id";

        List<Medal> medalsRecords = new();
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return medalsRecords;

        foreach (var singleResponse in fullResponse)
        {
            var medal = new Medal();
            medal.TypeSort(singleResponse);

            // Populate Athlete
            var athlete = new Athlete
            {
                Id = int.Parse(singleResponse["athleteid"]),
                Name = singleResponse["athletename"],
                Surname = singleResponse["athletesurname"],
                Dob = DateTime.Parse(singleResponse["athletedob"]),
                Country = singleResponse["athletecountry"]
            };
            medal.Athlete = athlete;

            // Populate Competition
            var competition = new Competition
            {
                Id = int.Parse(singleResponse["competitionid"]),
                Type = singleResponse["competitiontype"],
                IsIndoor = bool.Parse(singleResponse["isindoor"]),
                IsTeamComp = bool.Parse(singleResponse["isteamcomp"]),
                Category = singleResponse["competitioncategory"]
            };
            medal.Competition = competition;

            // Populate Event
            var eventEntity = new Event
            {
                Id = int.Parse(singleResponse["eventid"]),
                Name = singleResponse["eventname"],
                Year = int.Parse(singleResponse["eventyear"]),
                Location = singleResponse["eventlocation"]
            };
            medal.Event = eventEntity;

            medalsRecords.Add(medal);
        }

        return medalsRecords;
    }

    public List<Medal> GetAthleteMedals(int athleteId)
    {
        var query = $@"
        SELECT 
            m.Id AS MedalId, m.MedalTier, 
            a.Id AS AthleteId, a.Name AS AthleteName, a.Surname AS AthleteSurname, a.Dob AS AthleteDob, a.Country AS AthleteCountry,
            c.Id AS CompetitionId, c.Type AS CompetitionType, c.IsIndoor, c.IsTeamComp, c.Category AS CompetitionCategory,
            e.Id AS EventId, e.Name AS EventName, e.Year AS EventYear, e.Location AS EventLocation
        FROM Medals m
        JOIN Athletes a ON m.AthleteID = a.Id
        JOIN Competitions c ON m.CompetitionID = c.Id
        JOIN Events e ON m.EventID = e.Id
        WHERE m.AthleteID = {athleteId}";

        List<Medal> medalsRecords = new();
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return medalsRecords;

        foreach (var singleResponse in fullResponse)
        {
            var medal = new Medal();
            medal.TypeSort(singleResponse);

            // Populate Athlete
            var athlete = new Athlete
            {
                Id = int.Parse(singleResponse["athleteid"]),
                Name = singleResponse["athletename"],
                Surname = singleResponse["athletesurname"],
                Dob = DateTime.Parse(singleResponse["athletedob"]),
                Country = singleResponse["athletecountry"]
            };
            medal.Athlete = athlete;

            // Populate Competition
            var competition = new Competition
            {
                Id = int.Parse(singleResponse["competitionid"]),
                Type = singleResponse["competitiontype"],
                IsIndoor = bool.Parse(singleResponse["isindoor"]),
                IsTeamComp = bool.Parse(singleResponse["isteamcomp"]),
                Category = singleResponse["competitioncategory"]
            };
            medal.Competition = competition;

            // Populate Event
            var eventEntity = new Event
            {
                Id = int.Parse(singleResponse["eventid"]),
                Name = singleResponse["eventname"],
                Year = int.Parse(singleResponse["eventyear"]),
                Location = singleResponse["eventlocation"]
            };
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
