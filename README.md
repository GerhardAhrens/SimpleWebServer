# Simple Web-Server

![NET](https://img.shields.io/badge/NET-10-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)

## Projekt 
In diesem Projekt soll ein einfacher Web-Server mit REST-API Endpunkte entstehen. Das Projekt dient zur Demonstration wie ein Web-Server und REST-API funktionieren. Weiter wurde eine Fuktionalität in Verbindung mit **SignalR** für eine aktualisierende Uhr und auktualisierende Anzeige bei der änderung einer ASCII Datei implementiert.

<img src="WebServerOutput.png" style="width:650px;"/>


## Hinweis
Der Source ist soll auch einfache Art und Weise die Funktionen eines Features zeigen. Der Source ist so geschrieben, das so wenig wie möglich zusätzliche NuGet-Pakete benötigt werden.

## Beispielsource
### Projektstruktur

<img src="WebServerStruktur.png" style="width:350px;"/>

Start des Web Server
<img src="WebServerStart.png" style="width:650px;"/>

Nach dem der Web Server gestartet ist, kann von dort aus auch die Webseite aufgerufen werden.
Hierzu muß die Start URL mit STRG-Mausklick
<img src="Start_URL.png" style="width:650px;"/>
aufgerufen werden. Im Browser wird dann die Seite aufgerufen.

```csharp
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSimpleWebServer(builder);

    var app = builder.Build();

    /*
    var hostedServices = app.Services.GetServices<IHostedService>();
    foreach (var service in hostedServices)
    {
        Console.WriteLine(service.GetType().FullName);
    }
    */

    app.UseSimpleWebServer();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteError(ex.Message);
    Console.Wait();
}
```

### Konfiguration

```json
{
  "WebServer": {
    "Port": 8080,
    "Host": "self",
    "DisableBrowserCache": true
  }
}
```

- Host, hier kann entweder `localhost`, eine IP Adresse, oder bei **self** wird die aktuelle IP Adresse automatisch ermittelt.
- Port unter dem die Seite zu erreichen ist
- DisableBrowserCache, der Browser wird benachrichtigt, die webseite nicht zu cachen.

Zugriff über den Browser
<img src="BrwoserOutput.png" style="width:650px;"/>

Ergebnis beim Zugriff über die REST API Schnittstelle
<img src="JSONOutput.png" style="width:650px;"/>

Automatische Aktualisierung über SignalR für eine "laufende" Uhr
<img src="SignalR_Uhr.png" style="width:650px;"/>

Automatische Aktualisierung über SignalR für das Lesen einer ACII Datei
und aktualisierung wenn sich der Inhalt der Datei ändert.
<img src="SignalR_Aktor.png" style="width:650px;"/>

# Versionshistorie
![Version](https://img.shields.io/badge/Version-1.0.2026.2-yellow.svg)
- Erste Version
