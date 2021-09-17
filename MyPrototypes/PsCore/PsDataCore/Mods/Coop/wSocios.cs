using PsDataCore.Mods.Gen;

namespace PsDataCore.Mods.Coop
{
    public class WSocios
    {

        private readonly WGeneric _wG = new WGeneric();

        public int GeneraCodigo(int autor)
        {
            return _wG.GeneraCodigo(autor, "SOCIOS");

        }

    }
}
