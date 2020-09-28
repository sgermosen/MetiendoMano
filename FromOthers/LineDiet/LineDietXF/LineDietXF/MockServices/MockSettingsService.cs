using LineDietXF.Enumerations;
using LineDietXF.Interfaces;

namespace LineDietXF.MockServices
{
    public class MockSettingsService : ISettingsService
    {
        public bool HasDismissedStartupView { get; set; }
        public WeightUnitEnum WeightUnit { get; set; }

        public MockSettingsService()
        {
            WeightUnit = WeightUnitEnum.ImperialPounds;
        }

        public void Initialize()
        {
        }
    }
}