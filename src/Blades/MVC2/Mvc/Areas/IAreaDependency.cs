namespace Mvc.Areas
{
    using System.Web.Mvc;

    public interface IAreaDependency
    {
        string GetAreaName(AreaRegistration registration);
    }
}