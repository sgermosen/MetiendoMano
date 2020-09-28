using MarcelloDB.Platform;

namespace ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract
{
    public interface IPlatformFactory
    {
        IPlatform GetPlatform();

        MarcelloDB.Session GetSession();
    }
}
