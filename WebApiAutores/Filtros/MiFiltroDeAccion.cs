using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
namespace WebApiAutores.Filtros
{
    public class MiFiltroDeAccion : IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> logger;

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion>logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
            logger.LogInformation("Antes de Ejecutar la accion");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
            logger.LogInformation("Despues de Ejecutar la accion");
        }

       
    }
}
