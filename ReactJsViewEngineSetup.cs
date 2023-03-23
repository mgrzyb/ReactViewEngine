using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using ReactViewEngine;

public class ReactJsViewEngineSetup : IConfigureOptions<MvcViewOptions>
{
    private readonly IViewEngine viewEngine;

    public ReactJsViewEngineSetup(ReactJsViewEngine viewEngine)
    {
        this.viewEngine = viewEngine;
    }

    public void Configure(MvcViewOptions options)
    {
        options.ViewEngines.Clear();
        options.ViewEngines.Add(this.viewEngine);
    }
}