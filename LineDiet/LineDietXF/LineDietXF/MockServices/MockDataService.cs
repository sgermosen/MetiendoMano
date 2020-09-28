using LineDietXF.Enumerations;
using LineDietXF.Interfaces;
using LineDietXF.Types;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LineDietXF.MockServices
{
    public class MockDataService : IDataService
    {
        IEventAggregator EventAggregator { get; set; }

        public bool HasBeenInitialized { get; private set; }

        WeightLossGoal MockGoal { get; set; }
        List<WeightEntry> MockEntries { get; set; }

        public event EventHandler UserDataUpdated;

        public MockDataService(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            HasBeenInitialized = false;
        }

        public async Task Initialize(string path)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            MockGoal = new WeightLossGoal(
                startDate: new DateTime(2016, 04, 30),
                startWeight: 225.0M,
                goalDate: new DateTime(2016, 06, 08),
                goalWeight: 215.0M, units: 
                WeightUnitEnum.ImperialPounds);

            MockEntries = new List<WeightEntry>();
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 24), 234.4M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 25), 234.3M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 26), 234.2M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 27), 234.1M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 28), 234.0M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 29), 233.9M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 4, 30), 233.8M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 5, 1), 233.7M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 5, 2), 233.6M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 5, 3), 233.5M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 5, 4), 233.4M, WeightUnitEnum.ImperialPounds));
            MockEntries.Add(new WeightEntry(new DateTime(2016, 5, 5), 233.3M, WeightUnitEnum.ImperialPounds));

            HasBeenInitialized = true;
            FireUserDataUpdated();
        }

        public async Task<IList<WeightEntry>> GetLatestWeightEntries(int maxCount)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            var limitedSet = MockEntries.OrderByDescending(x => x.Date).Take(maxCount).ToList();
            return limitedSet as IList<WeightEntry>;
        }

        public async Task<IList<WeightEntry>> GetAllWeightEntries()
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            return await GetLatestWeightEntries(int.MaxValue);
        }

        void FireUserDataUpdated()
        {
            UserDataUpdated?.Invoke(this, null);
        }

        public async Task<WeightEntry> GetWeightEntryForDate(DateTime dt)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            var entry = MockEntries.Where(w => w.Date.Date == dt.Date).FirstOrDefault();
            return entry;
        }

        public async Task<bool> WeightEntryForDateExists(DateTime dt)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            var entry = await GetWeightEntryForDate(dt);
            return entry != null;
        }

        public async Task<bool> AddWeightEntry(WeightEntry newEntry)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            if (await WeightEntryForDateExists(newEntry.Date))
                return false;

            MockEntries.Add(newEntry);
            FireUserDataUpdated();

            return true;
        }

        public async Task<bool> RemoveWeightEntryForDate(DateTime dt)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            var entry = await GetWeightEntryForDate(dt);
            if (entry == null)
                return false;

            MockEntries.Remove(entry);
            FireUserDataUpdated();

            return true;
        }

        public async Task<WeightLossGoal> GetGoal()
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            return MockGoal;
        }

        public async Task<bool> SetGoal(WeightLossGoal weightLossGoal)
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            MockGoal = weightLossGoal;
            FireUserDataUpdated();

            return true;
        }

        public async Task<bool> RemoveGoal()
        {
#if DEBUG
            await SimulateSlowNetworkIfEnabled();
#endif

            MockGoal = null;
            FireUserDataUpdated();

            return true;
        }

        async Task SimulateSlowNetworkIfEnabled()
        {
#if DEBUG
            if (Constants.App.DEBUG_SimulateSlowResponseTimes)
                await Task.Delay(Constants.App.DEBUG_SimulatedSlowResponseTime);
#endif
        }

        public Task<ResultWithErrorText> ChangeWeightAndGoalUnits(WeightUnitEnum newUnits, bool convertValues)
        {
            return Task.FromResult(new ResultWithErrorText(true, string.Empty));
        }
    }
}