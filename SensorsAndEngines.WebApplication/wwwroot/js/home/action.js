"use strict";

main();

async function main() {
    var connection = new signalR
        .HubConnectionBuilder()
        .withUrl("/sensorHub")
        .build();

    await connection.start();

    const serverData = await connection.invoke("LoadData");
    console.log(serverData);

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
        console.log(jsonData.time);
    });

    const stopBtn = document.getElementById("stop-btn");
    stopBtn.addEventListener('click', () => fetch(stopBtn.value, { method: "POST"}));
}