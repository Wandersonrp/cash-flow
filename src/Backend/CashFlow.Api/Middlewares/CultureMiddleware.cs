﻿using System.Globalization;

namespace CashFlow.Api.Middlewares;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .ToList();

        var requestedCulture = context
            .Request
            .Headers
            .AcceptLanguage
            .FirstOrDefault();

        Console.WriteLine($"Requested culture: {requestedCulture}");    

        var cultureInfo = new CultureInfo("en");

        if(!string.IsNullOrWhiteSpace(requestedCulture) 
            && supportedLanguages.Exists(x => x.Name.Equals(requestedCulture)))
        {
            cultureInfo = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}
