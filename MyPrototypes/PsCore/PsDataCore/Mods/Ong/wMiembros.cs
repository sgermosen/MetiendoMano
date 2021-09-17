using PsDataCore.Mods.Gen;

namespace PsDataCore.Mods.Ong
{
    public class WMiembros
    {
        private readonly WGeneric _wG = new WGeneric();

        public int GeneraCodigo(int autor)
        {
            return _wG.GeneraCodigo(autor, "MIEMBROS");

        }
    }
}
