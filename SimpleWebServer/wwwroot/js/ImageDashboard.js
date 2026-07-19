"use strict";

let connection = null;

async function initialize() {

    await startSignalR();

    await loadImages();
}

async function startSignalR() {

    connection = new signalR.HubConnectionBuilder()
        .withUrl("/hub/webserver")
        .build();

    connection.on("ImagesChanged", async () => {

        console.log("ImagesChanged");

        await loadImages();
    });

    await connection.start();

    document.getElementById("connectionState").innerText = "Verbunden";
}

async function loadImages() {

    const response = await fetch("/api/images");

    if (!response.ok)
        return;

    const images = await response.json();

    const gallery = document.getElementById("gallery");

    gallery.innerHTML = "";

    for (const image of images) {

        const img = document.createElement("img");

        img.src = "/api/images/" + encodeURIComponent(image.name);

        img.className = "thumbnail";

        img.title = image.name;

        img.onclick = () => showImage(image);

        gallery.appendChild(img);
    }

    if (images.length > 0)
        showImage(images[0]);
}

function showImage(image) {

    document.getElementById("imageViewer").src =
        "/api/images/" + encodeURIComponent(image.name);

    document.getElementById("imageName").innerText =
        image.name;

    document.getElementById("imageSize").innerText =
        formatSize(image.size);

    document.getElementById("imageDate").innerText =
        new Date(image.lastWriteTime).toLocaleString();
}

function formatSize(size) {

    if (size < 1024)
        return size + " Byte";

    if (size < 1024 * 1024)
        return (size / 1024).toFixed(1) + " KB";

    return (size / (1024 * 1024)).toFixed(1) + " MB";
}

window.addEventListener("load", initialize);
