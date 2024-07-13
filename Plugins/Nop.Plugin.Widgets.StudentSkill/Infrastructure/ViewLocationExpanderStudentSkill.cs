using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Widgets.StudentSkill.Infrastructure
{
    public class ViewLocationExpanderStudentSkill : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            viewLocations = new[] { $"/Plugins/Widgets.StudentSkill/Views/{{1}}/{{0}}.cshtml" }
                .Concat(viewLocations);
            return viewLocations;
        }
    
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
