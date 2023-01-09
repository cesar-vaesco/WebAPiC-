﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAutores.Filtros
{

    //Requiere inyectar como dependencia par ser usado
    public class MiFiltroDeAccion : IActionFilter
    {
        private readonly ILogger<MiFiltroDeAccion> logger;

        public MiFiltroDeAccion(ILogger<MiFiltroDeAccion> logger) 
        {
            this.logger = logger;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("Después de ejecutar la acción");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Antes de ejecutar una acción");
        }
    }
}