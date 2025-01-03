#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: DaoCompetitions.cs
// * Edited on 2024/12/22 at 21:12:55
// --------------------------------

#endregion

#region

using CEX_Olimpiadi.Classes;
using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.DAO_Classes;

public class DaoCompetitions : IDAO
{
    /// <inheritdoc />
    public List<Entity> GetRecords()
    {
        const string query = $"SELECT * FROM Competitions";
        List<Entity> competitionsRecords = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return competitionsRecords;
        foreach (var singleResponse in fullResponse)
        {
            Entity entity = new Competition();
            entity.TypeSort(singleResponse);

            competitionsRecords.Add(entity);
        }

        return competitionsRecords;
    }

    /// <inheritdoc />
    public bool CreateRecord(Entity entity)
    {
        var competition = (Competition)entity;
        const string query =
            $"INSERT INTO Competitions (Type, IsIndoor, IsTeamComp, Category, EventId) VALUES (@Type, @IsIndoor, @IsTeamComp, @Category, @EventId)";

        var parameters = new Dictionary<string, object>
        {
            { "@Type", competition.Type },
            { "@IsIndoor", competition.IsIndoor ? 1 : 0 },
            { "@IsTeamComp", competition.IsTeamComp ? 1 : 0 },
            { "@Category", competition.Category.Replace("'", "''") },
            { "@EventId", competition.EventId }
        };
        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool UpdateRecord(Entity entity)
    {
        var competition = (Competition)entity;
        const string query =
            $"UPDATE Competitions SET Type = @Type, IsIndoor = @IsIndoor, IsTeamComp = @IsTeamComp, Category = @Category, EventId = @EventId WHERE Id = @Id";
        var parameters = new Dictionary<string, object>
        {
            { "@Type", competition.Type },
            { "@IsIndoor", competition.IsIndoor ? 1 : 0 },
            { "@IsTeamComp", competition.IsTeamComp ? 1 : 0 },
            { "@Category", competition.Category },
            { "@EventId", competition.EventId },
            { "@Id", entity.Id }
        };

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool DeleteRecord(int recordId)
    {
        const string query = "DELETE FROM Competitions WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public Entity? FindRecord(int recordId)
    {
        const string query = "SELECT * FROM Competitions WHERE Id = @id";
        var parameters = new Dictionary<string, object> { { "@id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Competition();
        entity.TypeSort(singleResponse);
        return entity;
    }

    public List<Competition> GetCompetitionsByEventId(int eventId)
    {
        const string query = "SELECT * FROM Competitions WHERE EventId = @EventId";
        var parameters = new Dictionary<string, object> { { "@EventId", eventId } };
        List<Competition> competitionsRecords = [];
        var fullResponse = _db.ReadDb(query, parameters);
        if (fullResponse == null)
            return competitionsRecords;
        foreach (var singleResponse in fullResponse)
        {
            var competition = new Competition();
            competition.TypeSort(singleResponse);
            competitionsRecords.Add(competition);
        }

        return competitionsRecords;
    }

    #region Singleton

    private static DaoCompetitions? _instance;
    private readonly IDatabase _db;

    private DaoCompetitions()
    {
        _db = new Database("Olimpiadi");
    }

    public static DaoCompetitions GetInstance()
    {
        return _instance ??= new DaoCompetitions();
    }

    #endregion
}
