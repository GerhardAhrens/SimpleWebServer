//-----------------------------------------------------------------------
// <copyright file="HelloWorldModel.cs" company="Lifeprojects.de">
//     Class: HelloWorldModel
//     Copyright © Lifeprojects.de 2026
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>13.07.2026</date>
//
// <summary>
// Model Klasse für Web-Server, REST API Schnittstelle
// </summary>
//-----------------------------------------------------------------------

namespace SimpleWebServer.WebFunction
{
    using System;
    using System.Collections.Generic;

    public class HelloWorldModel
    {
        public string Text { get; set; } = "Hallo World";
    }
}
