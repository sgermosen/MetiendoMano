using PsDataCore.Mods.Gen;

namespace PsDataCore.Mods.Pos
{
    public class WClientes
    {
        private readonly WGeneric _wG = new WGeneric();

        public int GeneraCodigo(int autor)
        {
            return _wG.GeneraCodigo(autor, "CLIENTES");

        }

    }
}
