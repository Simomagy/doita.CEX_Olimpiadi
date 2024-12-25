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
    private const string TableName = "Competitions";

    /// <inheritdoc />
    public List<Entity> GetRecords()
    {
        const string query = $"SELECT * FROM {TableName}";
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
        var type = ((Competition)entity).Type.Replace("'", "''");
        var isIndoor = ((Competition)entity).IsIndoor ? 1 : 0;
        var isTeamComp = ((Competition)entity).IsTeamComp ? 1 : 0;
        var category = ((Competition)entity).Category.Replace("'", "''");
        var query =
            $"INSERT INTO {TableName} (Type, IsIndoor, IsTeamComp, Category) VALUES ('{type}', {isIndoor}, {isTeamComp}, '{category}')";

        return _db.UpdateDb(query);
    }

    /// <inheritdoc />
    public bool UpdateRecord(Entity entity)
    {
        var type = ((Competition)entity).Type.Replace("'", "''");
        var isIndoor = ((Competition)entity).IsIndoor ? 1 : 0;
        var isTeamComp = ((Competition)entity).IsTeamComp ? 1 : 0;
        var category = ((Competition)entity).Category.Replace("'", "''");
        var query =
            $"UPDATE {TableName} SET Type = '{type}', IsIndoor = {isIndoor}, IsTeamComp = {isTeamComp}, Category = '{category}' WHERE Id = {entity.Id}";

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
        Entity entity = new Competition();
        entity.TypeSort(singleResponse);
        return entity;
    }

    public List<Entity> GetCompetitionsByEventId(int eventId)
    {
        var query = $"SELECT * FROM {TableName} WHERE EventId = {eventId}";
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
