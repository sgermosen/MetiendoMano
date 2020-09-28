using LineDietXF.Enumerations;
using LineDietXF.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineDietXF.Interfaces
{
    // NOTE:: methods are written as asynchronous tasks so that the service could be rewritten to use a back-end service at some point,
    // and so that slow queries (ex: hundreds of entries) can stay responsive while loading

    /// <summary>
    /// Interface defining services which store weight related data (goal information and weight entry collection)
    /// </summary>
    public interface IDataService
    {
        bool HasBeenInitialized { get; }

        // TODO:: FUTURE:: somewhat hacky as some implementations may not require a path, consider having the SQLiteDataService use a
        // dependency service to get the path for the current platform
        Task Initialize(string dbPath);

        // Goal related methods
        Task<WeightLossGoal> GetGoal();
        Task<bool> SetGoal(WeightLossGoal weightLossGoal); // should remove any existing goal so there is only ever 1 goal
        Task<bool> RemoveGoal(); 

        // Weight Entry related methods
        Task<IList<WeightEntry>> GetLatestWeightEntries(int maxCount);
        Task<IList<WeightEntry>> GetAllWeightEntries();
        Task<WeightEntry> GetWeightEntryForDate(DateTime dt);
        Task<bool> WeightEntryForDateExists(DateTime dt);
        Task<bool> AddWeightEntry(WeightEntry newEntry);
        Task<bool> RemoveWeightEntryForDate(DateTime dt);

        // Conversion (user changes unit settings)
        Task<ResultWithErrorText> ChangeWeightAndGoalUnits(WeightUnitEnum newUnits, bool convertValues);

        event EventHandler UserDataUpdated;
    }
}