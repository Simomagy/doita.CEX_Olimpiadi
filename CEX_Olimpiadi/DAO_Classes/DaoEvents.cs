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

namespace CEX_Olimpiadi.DAO_Classes
{
    public class DaoEvents : IDAO
    {
        const string TableName = "Events";

        public List<Entity> GetRecords()
        {
            const string query = $"SELECT * FROM {TableName}";
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
            var name = ((Event)entity).Name.Replace("'", "''");
            var year = ((Event)entity).Year;
            var location = ((Event)entity).Location.Replace("'", "''");
            var query = $"INSERT INTO {TableName} (Name, Surname, Dob, Country) VALUES ('{name}', '{year}', '{location}')";

            return _db.UpdateDb(query);
        }
        /// <inheritdoc />
        public bool UpdateRecord(Entity entity)
        {
            var name = ((Event)entity).Name.Replace("'", "''");
            var year = ((Event)entity).Year;
            var location = ((Event)entity).Location.Replace("'", "''");
            var query = $"UPDATE {TableName} SET Name = '{name}', Year = '{year}', Location = '{location}' WHERE Id = {entity.Id}";

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

        public int GetEventIdByNameAndYear(string name, int year)
        {
            var query = $"SELECT Id FROM {TableName} WHERE Name = '{name}' AND Year = '{year}'";
            var singleResponse = _db.ReadOneDb(query);
            if (singleResponse == null)
                return -1;
            return int.Parse(singleResponse["id"]);
        }

        public Entity GetEventById(int id)
        {
            var query = $"SELECT * FROM {TableName} WHERE Id = {id}";
            var singleResponse = _db.ReadOneDb(query);
            if (singleResponse == null)
                return null;
            Entity entity = new Event();
            entity.TypeSort(singleResponse);
            return entity;
        }
        #region Singleton

        static DaoEvents? _instance;
        readonly IDatabase _db;
        DaoEvents() => _db = new Database("Olimpiadi");
        public static DaoEvents GetInstance() => _instance ??= new DaoEvents();

        #endregion


    }
}
