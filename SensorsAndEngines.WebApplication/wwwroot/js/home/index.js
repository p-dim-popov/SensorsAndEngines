import { AnalogSensorComponent, DigitalSensorComponent, SensorCardComponent, Sensors } from "/js/home/modules/sensorCard.js";

main();

function main() {
    window.measurementUnits =
        window.fetch("/home/getMeasurementUnits").then(r => r.json());

    const sensors = new Sensors(document.getElementById("sensor-cards"));

    const portsDropdown = document.getElementById("port-select-dropdown");
    const [refreshBtn, downloadBtn, loadBtn, hiddenLoadBtn] = document
        .getElementById("pre-config-buttons").children;

    refreshBtn.addEventListener("click", homeRefreshPorts);

    downloadBtn.addEventListener("click", () => downloadObjectAsJson(sensors.asSerializable()));

    ////////////////////////
    //#region Load Config Handling
    loadBtn.addEventListener("click", () => hiddenLoadBtn.click());
    hiddenLoadBtn.addEventListener("change",
        e => handleFileUpload(e, content => {
            sensors.list
                .forEach(s => s.removeComponent());

            [...JSON.parse(content)]
                .map(s => SensorCardComponent.createLitElement(s))
                .forEach(s => sensors.appendSensor(s, null));
        }));
    //#endregion
    ////////////////////////

    const [addAnalogBtn, addDigitalBtn] = document.querySelectorAll(`a[id^="add-"]`);
    addAnalogBtn.addEventListener("click", () => sensors.appendSensor(null, AnalogSensorComponent));
    addDigitalBtn.addEventListener("click", () => sensors.appendSensor(null, DigitalSensorComponent));

    const startBtn = document.getElementById("start-btn");
    startBtn.addEventListener("click",
        async () => {
            const response = await fetch("/home/action",
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    redirect: "follow", // manual, *follow, error
                    body: JSON.stringify({
                        sensorCards: sensors.asSerializable(),
                        portName: portsDropdown.selectedOptions[0].value
                    })
                });

            window.location.href = response.url;
        }
    );
}

async function homeRefreshPorts(e) {
    e.preventDefault();
    const dropdown = document.getElementById("port-select-dropdown");
    [...dropdown.children]
        .forEach(c => c.remove());
    const response = await window.fetch(e.target.value);
    const ports = await response.json();
    [...ports]
        .forEach(p => dropdown.innerHTML += `<option value="${p}">${p}</option>`);
}

function handleFileUpload(evt, handler) {
    const files = evt.target.files;
    if (files.length < 1) {
        alert('select a file...');
        return;
    }
    const file = files[0];
    const reader = new FileReader();
    reader.onload = (e) => handler(e.target.result);

    reader.readAsText(file);
}

function downloadObjectAsJson(exportObj) {
    const dataStr = "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObj, null, 1));
    const downloadAnchorNode = document.createElement('a');
    downloadAnchorNode.setAttribute("href", dataStr);
    downloadAnchorNode.setAttribute("download", (prompt("Enter filename:", "config") || "config") + ".json");
    document.body.appendChild(downloadAnchorNode); // required for firefox
    downloadAnchorNode.click();
    downloadAnchorNode.remove();
}