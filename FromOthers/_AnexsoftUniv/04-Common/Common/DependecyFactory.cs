namespace Common
{
    public class DependecyFactory
    {
        public static T GetInstance<T>()
        {
            return new LightInject.ServiceContainer()
                                  .GetInstance<T>();
        }
    }
}
