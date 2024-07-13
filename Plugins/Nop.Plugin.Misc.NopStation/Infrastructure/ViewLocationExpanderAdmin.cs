using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Misc.NopStation.Infrastructure
{
    public class ViewLocationExpanderAdmin : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            /*viewLocations = new[] { $"/Plugins/Misc.NopStation/Areas/{context.AreaName}/Views/{context.ControllerName}/{context.ViewName}.cshtml" }
            .Concat(viewLocations);*/
            /*if (context.AreaName == AreaNames.ADMIN)
            {
                viewLocations = new[] { $"/Plugins/Misc.NopStation/Areas/{context.AreaName}/Views/{{1}}/{{0}}.cshtml" }
                .Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] { $"/Plugins/Misc.NopStation/Views/Shared/{context.ViewName}.cshtml" }
                    .Concat(viewLocations);
            }*/

            viewLocations = new[] { $"/Plugins/Misc.NopStation/Areas/{context.AreaName}/Views/{{1}}/{{0}}.cshtml" }
                .Concat(viewLocations);

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}