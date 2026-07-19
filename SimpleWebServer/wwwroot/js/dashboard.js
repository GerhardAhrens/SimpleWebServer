
//----------------------------------------------------------
// Initialisierung
//----------------------------------------------------------

document.addEventListener("DOMContentLoaded", initialize);

async function initialize()
{
    registerEvents();

    await startSignalR();
    await loadSmartHome();
}

//----------------------------------------------------------
// Event-Registrierung
//----------------------------------------------------------

function registerEvents()
{
}

//----------------------------------------------------------
// SignalR
//----------------------------------------------------------

const connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/hub/webserver")
        .withAutomaticReconnect()
        .build();

async function startSignalR()
{
    connection.on("SmartHomeChanged", async () =>
    {
        await loadSmartHome();
    });

    connection.onreconnecting(() => {
        document.getElementById("connectionState").innerText = "Verbindung wird wiederhergestellt...";
    });

    connection.onreconnected(() => {
        document.getElementById("connectionState").innerText = "Verbunden";
    });

    connection.onclose(() => {
        document.getElementById("connectionState").innerText = "Getrennt";
    });

    try {
        await connection.start();

        document.getElementById("connectionState").innerText = "Verbunden";
    }
    catch (err) {
        console.error(err);

        document.getElementById("connectionState").innerText = "Fehler";
    }
}

//----------------------------------------------------------
// REST - Smart Home
//----------------------------------------------------------

async function loadSmartHome()
{
    const response = await fetch("/api/smarthomeaktor");

    if (!response.ok)
        return;

    const json = await response.json();

    document.getElementById("currentPower").value = json.currentPower;

    document.getElementById("currentVolt").value = json.currentVolt;

    document.getElementById("currentTemperature").value = json.currentTemperature;
}