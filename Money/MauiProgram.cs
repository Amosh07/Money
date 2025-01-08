﻿using DataAccess.Interface;
using DataAccess.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Money
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            //Service inject//
            builder.Services.AddScoped<IUserInterface,UserService>();
            builder.Services.AddScoped<ITransactionsInterface, TransactionsService>();
            builder.Services.AddScoped<ITagInterface, TagService>();
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            //Mudblazor service//
            builder.Services.AddMudServices();
            return builder.Build();
        }
    }
}
