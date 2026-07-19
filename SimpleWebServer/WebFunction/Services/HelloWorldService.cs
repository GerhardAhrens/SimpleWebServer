//-----------------------------------------------------------------------
// <copyright file="HelloWorldService.cs" company="Lifeprojects.de">
//     Class: HelloWorldService
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026</date>
//
// <summary>
// Der Service verwaltet den Text.
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;


    public class HelloWorldService : ObservableService<string>
    {
        public HelloWorldService() : base("Hello World")
        {
        }

        public string Text => Current;

        public void SetText(string text)
        {
            base.SetCurrent(text);
        }
    }
}
