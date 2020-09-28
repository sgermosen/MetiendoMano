using Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VueSpaApplication.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        /*
         * Aunque esto parezca un poco tonto funciona, lo que hacemos es matar la sesión actual
         * del usuario pero para el cliente (nuestro SPA). Al volver al home y al aún estar autenticado
         * con el Identity Server se vuelve a reconectar actualizando la información de los Claims.
         * 
         * Nota: de hecho que no es la mejor forma pero fue la única que encontré, IdentityServer 4 es 
         * un dolor de cabeza. Es cuestión de indagar más en su documentación o preguntar en los foros
         * para ver si llegamos a una solución más óptima.
         */
        public async Task<IActionResult> Refresh()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return Redirect("~/");
        }

        public async Task<IActionResult> Logout()
        {
            /*
             * El mecanismo para eliminar la sesión actual del usuario elimina pero solo para el cliente,
             * lo que yo busco es eliminar la sesión también para el Identity Server. Por lo tanto borro todas
             * las cookies.
             */

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return await Task.FromResult(View());
        }
    }
}
