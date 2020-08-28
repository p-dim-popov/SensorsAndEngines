"use strict";

main();

async function main() {
    var connection = new signalR
        .HubConnectionBuilder()
        .withUrl("/sensorHub")
        .build();

    await connection.start();

    const serverData = await connection.invoke("LoadData");

    const data = serverData
        .reduce((acc, cur) => [...acc, [cur.time, ...cur.models.map(m => +m.data.value)]], []);

    const labels = ["Time", ...serverData[0].models.map(m => m.name)];

    var graph = new Dygraph(document.getElementById("graph"), data,
        {
            drawPoints: false,
            showRoller: true,
            labels: labels
        }
    );

    connection.on("ReceiveMessage", function (jsonData) {
        data.push([jsonData.time, ...jsonData.models.map(s => +s.data.value)]);
        graph.updateOptions({ 'file': data });
    });

    const stopBtn = document.getElementById("stop-btn");
    stopBtn.addEventListener('click', () => {
        fetch(stopBtn.value, { method: "POST" });
        stopBtn.classList.add("d-none");
    });

    const downloadBtn = document.getElementById("download-btn");
    downloadBtn.addEventListener("click", () => {

    });
}