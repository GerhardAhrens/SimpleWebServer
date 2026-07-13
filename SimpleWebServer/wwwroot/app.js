async function load() {

    let response = await fetch("/api/hello");

    let json = await response.json();

    document.getElementById("result").innerHTML =
        json.text + "<br>" + json.time;

    document.getElementById("text").value =
        json.text;
}

async function save() {

    let text =
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

    load();
}

load();