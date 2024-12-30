#region HEADER

// --------------------------------
// * Created by msstu on 2024/12/22 at 16:12
// * Project: CEX_Olimpiadi
// * --------------------------------
// * File: DaoAthletes.cs
// * Edited on 2024/12/22 at 21:12:55
// --------------------------------

#endregion

#region

using CEX_Olimpiadi.Classes;
using MSSTU.DB.Utility;

#endregion

namespace CEX_Olimpiadi.DAO_Classes;

public class DaoAthletes : IDAO
{
    /// <inheritdoc />
    public List<Entity> GetRecords()
    {
        const string query = $"SELECT * FROM Athletes";
        List<Entity> athletesRecords = [];
        var fullResponse = _db.ReadDb(query);
        if (fullResponse == null)
            return athletesRecords;
        foreach (var singleResponse in fullResponse)
        {
            Entity entity = new Athlete();
            entity.TypeSort(singleResponse);
            athletesRecords.Add(entity);
        }

        return athletesRecords;
    }

    /// <inheritdoc />
    public bool CreateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@Name", ((Athlete)entity).Name.Replace("'", "''") },
            { "@Surname", ((Athlete)entity).Surname.Replace("'", "''") },
            { "@Dob", ((Athlete)entity).Dob },
            { "@Country", ((Athlete)entity).Country.Replace("'", "''") }
        };
        var query =
            $"INSERT INTO Athletes (Name, Surname, Dob, Country) VALUES (@Name, @Surname, @Dob, @Country)";

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool UpdateRecord(Entity entity)
    {
        var parameters = new Dictionary<string, object>
        {
            { "@Name", ((Athlete)entity).Name.Replace("'", "''") },
            { "@Surname", ((Athlete)entity).Surname.Replace("'", "''") },
            { "@Dob", ((Athlete)entity).Dob },
            { "@Country", ((Athlete)entity).Country.Replace("'", "''") }
        };
        var query =
            $"UPDATE Athletes SET Name = @Name, Surname = @Surname, Dob = @Dob, Country = @Country WHERE Id = {entity.Id}";

        return _db.UpdateDb(query, parameters);
    }

    /// <inheritdoc />
    public bool DeleteRecord(int recordId)
    {
        var query = $"DELETE FROM Athletes WHERE Id = @Id";
        return _db.UpdateDb(query);
    }

    /// <inheritdoc />
    public Entity? FindRecord(int recordId)
    {
        var query = $"SELECT * FROM Athletes WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { { "@Id", recordId } };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Athlete();
        entity.TypeSort(singleResponse);
        return entity;
    }

    public Entity? GetAthleteByFullName(string name, string surname)
    {
        var query = $"SELECT * FROM Athletes WHERE Name = @Name AND Surname = @Surname";
        var parameters = new Dictionary<string, object>
        {
            { "@Name", name },
            { "@Surname", surname }
        };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return null;
        Entity entity = new Athlete();
        entity.TypeSort(singleResponse);

        return entity;
    }

    public int GetAthleteIdByFullName(string name, string surname)
    {
        var query = $"SELECT Id FROM Athletes WHERE Name = @Name AND Surname = @Surname";
        var parameters = new Dictionary<string, object>
        {
            { "@Name", name },
            { "@Surname", surname }
        };
        var singleResponse = _db.ReadOneDb(query, parameters);
        if (singleResponse == null)
            return -1;
        // Accedi al valore tramite la chiave
        return int.Parse(singleResponse["id"]);
    }

    public List<Athlete> GetAthletesByCountry(string country)
    {
        var query = $"SELECT * FROM Athletes WHERE Country = @Country";
        var parameters = new Dictionary<string, object> { { "@Country", country } };
        List<Athlete> athletes = [];
        var fullResponse = _db.ReadDb(query, parameters);
        if (fullResponse == null)
            return athletes;
        foreach (var singleResponse in fullResponse)
        {
            Entity entity = new Athlete();
            entity.TypeSort(singleResponse);
            athletes.Add((Athlete)entity);
        }

        return athletes;
    }

    public Athlete GetOldestGoldWinner()
    {
        const string query = @"
                    SELECT TOP 1
                        Athletes.Id, 
                        Athletes.Name, 
                        Athletes.Surname, 
                        Athletes.Dob, 
                        Athletes.Country, 
                        Medals.MedalTier
                    FROM 
                        Athletes
                        JOIN Medals ON Athletes.Id = Medals.AthleteId 
                    WHERE 
                        Medals.MedalTier = 'Oro' 
                    ORDER BY 
                        Athletes.Dob 
                    ";
        var singleResponse = _db.ReadOneDb(query);
        if (singleResponse == null)
            return new Athlete();
        Entity entity = new Athlete();
        entity.TypeSort(singleResponse);
        return (Athlete)entity;
    }

    #region Singleton

    private static DaoAthletes? _instance;
    private readonly IDatabase _db;

    private DaoAthletes()
    {
        _db = new Database("Olimpiadi");
    }

    /// <summary>
    ///     Restituisce l'istanza del DAO
    /// </summary>
    /// <returns>
    ///     Se l'istanza è <see langword="null" /> restituisce una nuova istanza, altrimenti restituisce l'istanza esistente
    /// </returns>
    public static DaoAthletes GetInstance()
    {
        return _instance ??= new DaoAthletes();
    }

    #endregion
}