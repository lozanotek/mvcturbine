namespace HtmlConventions {
    using System;
    using FubuMVC.UI;
    using FubuMVC.UI.Configuration;
    using FubuMVC.UI.Tags;
    using HtmlTags;

    public class CommonConventions : HtmlConventionRegistry
    {
        public CommonConventions()
        {
            Editors.Always.Attr("class", "test");
            Editors.IfPropertyIs<DateTime>().Modify(tag => tag.AddClass("picker"));
            Editors.IfPropertyIs<bool>().BuildBy(req => new CheckboxTag(req.Value<bool>()));

            // Relax, this is just the default fall through action
            Editors.Always.BuildBy(TagActionExpression.BuildTextbox);

            Editors.Always.Modify(AddElementName);
            Displays.Always.BuildBy(req => new HtmlTag("span").Text(req.StringValue()));
            Labels.Always.BuildBy(req => new HtmlTag("span").Text(req.Accessor.Name));
        }

        public static void AddElementName(ElementRequest request, HtmlTag tag)
        {
            if (tag.IsInputElement())
            {
                tag.Attr("name", request.ElementId);
            }
        }
    }
}