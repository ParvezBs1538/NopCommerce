using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Misc.NopStation.Infrastructure
{
    public class ViewLocationExpanderAdmin : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            /*viewLocations = new[] { $"/Plugins/Misc.NopStation/Areas/{context.AreaName}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }
            .Concat(viewLocations);*/
            viewLocations = new[] { $"/Plugins/Misc.NopStation/Areas/{context.AreaName}/Views/{{1}}/{{0}}.cshtml" }
            .Concat(viewLocations);

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            
        }
    }
}
