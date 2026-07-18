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

    public class HelloWorldService
    {
        private readonly HelloWorldModel _model = new();

        public event EventHandler TextChanged;

        public HelloWorldService()
        {
            /* Console.WriteLine($"HelloWorldService: {GetHashCode()}");*/
        }

        public string Text
        {
            get => this._model.Text;

            set
            {
                if (this._model.Text == value)
                {
                    return;
                }

                this._model.Text = value;

                this.TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
