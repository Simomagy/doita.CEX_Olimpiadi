#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: DaoEvents.cs
// * Edited on 2024/12/22 at 21:12:55
// --------------------------------

#endregion

#region

using CEX_Olimpiadi.Classes;
using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.DAO_Classes;

/// <summary>
///     Gestisce le operazioni CRUD per la tabella Events del database
/// </summary>
public class DaoEvents : IDAO
{
    public List<Entity> GetRecords()
    {
        const string query = "SELECT * FROM Events";
        List<Entity> eventsRecords = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return eventsRecords;
        foreach (var singleResponse in fullResponse)
        {
            Entity entity = new Event();
            entity.TypeSort(singleResponse);
            eventsRecords.Add(entity);
        }

        return eventsRecords;
    }

    /// <inheritdoc />
    public bool CreateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@Name", ((Event)entity).Name.Replace("'", "''") },
            { "@Year", ((Event)entity).Year },
            { "@Location", ((Event)entity).Location.Replace("'", "''") }
        };
        const string query = "INSERT INTO Events (Name, Year, Location) VALUES (@Name, @Year, @Location)";

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@Name", ((Event)entity).Name.Replace("'", "''") },
            { "@Year", ((Event)entity).Year },
            { "@Location", ((Event)entity).Location.Replace("'", "''") },
            { "@Id", entity.Id }
        };
        const string query = "UPDATE Events SET Name = @Name, Year = @Year, Location = @Location WHERE Id = @Id";

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool DeleteRecord(int recordId)
    {
        const string query = "DELETE FROM Events WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public Entity? FindRecord(int recordId)
    {
        const string query = "SELECT * FROM Events WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Athlete();
        entity.TypeSort(singleResponse);
        return entity;
    }

    /// <summary>
    ///     Restituisce l'ID di un evento dato il nome e l'anno
    /// </summary>
    /// <param name="name">Nome dell'evento da ricercare</param>
    /// <param name="year">Anno dell'evento da ricercare</param>
    /// <returns>ID dell'evento</returns>
    public int GetEventIdByNameAndYear(string name, int year)
    {
        const string query = "SELECT Id FROM Events WHERE Name = @Name AND Year = @Year";
        var parameters = new Dictionary<string, object>
        {
            { "@Name", name },
            { "@Year", year }
        };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return -1;
        return int.Parse(singleResponse["id"]);
    }

    #region Singleton

    private static DaoEvents? _instance;
    private readonly IDatabase _db;

    private DaoEvents()
    {
        _db = new Database("Olimpiadi");
    }

    public static DaoEvents GetInstance()
    {
        return _instance ??= new DaoEvents();
    }

    #endregion
}
