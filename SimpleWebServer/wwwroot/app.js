
//----------------------------------------------------------
// Initialisierung
//----------------------------------------------------------

document.addEventListener("DOMContentLoaded", initialize);

async function initialize()
{
    registerEvents();

    await startSignalR();

    await loadHello();
    await loadMachineName();
    await loadTime();
    await loadSmartHome();
}

//----------------------------------------------------------
// Event-Registrierung
//----------------------------------------------------------

function registerEvents()
{
    document
        .getElementById("btnSave")
        .addEventListener("click", saveHello);

    document
        .getElementById("btnMachineName")
        .addEventListener("click", loadMachineName);

    document
        .getElementById("btnTime")
        .addEventListener("click", loadTime);
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
    connection.on("HelloChanged", async () =>
    {
        console.log("HelloChanged");

        await loadHello();
    });

    connection.on("TimeChanged", (time) =>
    {
        console.log("TimeChanged empfangen:", time);

        document.getElementById("currentTime").value = time;
    });

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
// REST - Hello
//----------------------------------------------------------

async function loadHello()
{
    const response = await fetch("/api/hello");

    const json = await response.json();

    document.getElementById("result").innerHTML = json.text + "<br>" + json.time;

    document.getElementById("text").value = json.text;
}

async function saveHello() {
    const text =
        document.getElementById("text").value;

    await fetch("/api/hello",
        {
            method: "POST",

            headers:
            {
                "Content-Type": "application/json"
            },

            body: JSON.stringify(
                {
                    text: text
                })
        });

    // Keine Aktualisierung notwendig.
    // Der Browser erhält automatisch ein "HelloChanged"
    // über SignalR.
}

//----------------------------------------------------------
// REST - System
//----------------------------------------------------------

async function loadMachineName()
{
    const response = await fetch("/api/system/machinename");

    const json = await response.json();

    document.getElementById("machineName").value = json.machineName;
}

//----------------------------------------------------------
// REST - Time
//----------------------------------------------------------

async function loadTime()
{
    const response =  await fetch("/api/time");

    const json = await response.json();

    document.getElementById("currentTime").value = json.time;
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