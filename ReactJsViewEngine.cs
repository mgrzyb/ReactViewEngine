using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Options;

namespace ReactViewEngine;

public class ReactJsViewEngine : RazorViewEngine
{
    public ReactJsViewEngine(IRazorPageFactoryProvider razorPageFactoryProvider, IRazorPageActivator pageActivator, HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, ILoggerFactory loggerFactory, DiagnosticListener diagnosticListener) : base(new ReactPageFactory(razorPageFactoryProvider), pageActivator, htmlEncoder, optionsAccessor, loggerFactory, diagnosticListener)
    {
    }
    
}

public class ReactPageFactory : IRazorPageFactoryProvider
{
    private readonly IRazorPageFactoryProvider defaultProvider;

    public ReactPageFactory(IRazorPageFactoryProvider defaultProvider)
    {
        this.defaultProvider = defaultProvider;
    }

    public RazorPageFactoryResult CreateFactory(string relativePath)
    {
        var result = this.defaultProvider.CreateFactory(relativePath);
        if (result.Success)
            return result;

        if (Path.GetFileName(relativePath).StartsWith("_"))
            return result;

        return new RazorPageFactoryResult(new CompiledViewDescriptor()
        {
            RelativePath = relativePath
        }, () => new ReactJsPage(relativePath));
    }
}

public class ReactJsPage : RazorPage
{
    private readonly string relativePath;

    public ReactJsPage(string relativePath)
    {
        this.relativePath = relativePath;
        //        this.Layout = "_Layout";
    }

    public override Task ExecuteAsync()
    {
        this.ViewContext.Writer.WriteLine($"Duuuuupa: {this.relativePath} Model: {ViewContext.ViewData.Model}");
        // React.Render("Home", Model)
        return Task.CompletedTask;
    }
}