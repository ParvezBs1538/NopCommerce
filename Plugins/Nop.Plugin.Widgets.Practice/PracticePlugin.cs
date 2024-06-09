using Nop.Plugin.Widgets.Practice.Components;
using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.Practice
{
    public class PracticePlugin : BasePlugin, IWidgetPlugin
    {
        public bool HideInWidgetList => false;

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsPracticeViewComponent);
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>([PublicWidgetZones.HomepageBeforeProducts, PublicWidgetZones.HomepageBottom]);
        }

        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }
        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }
    }
}
