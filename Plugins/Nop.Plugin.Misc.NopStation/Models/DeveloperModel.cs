using Nop.Plugin.Misc.NopStation.Domain;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;

namespace Nop.Plugin.Misc.NopStation.Models;

public record DeveloperModel : BaseNopEntityModel
{
    public DeveloperModel()
    {
        Skills = new List<string>();
        Picture = new PictureModel();
    }

    public IList<string> Skills { get; set; }

    public string Name { get; set; }

    public PictureModel Picture { get; set; }

    public bool IsMVP { get; set; }

    public bool IsNopCommerceCertified { get; set; }

    public DeveloperStatus DeveloperStatus { get; set; }

    public string DeveloperStatusStr { get; set; }

    public DeveloperDesignation DeveloperDesignation { get; set; }

    public string DeveloperDesignationStr { get; set; }
}
