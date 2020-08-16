// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
main();

function main() {
    document
        .getElementById("refresh-ports-btn")
        .addEventListener("click", homeRefreshPorts);

    window.measurementUnits =
        fetch("/home/getMeasurementUnits").then(r => r.json());
}

async function homeRefreshPorts(e) {
    e.preventDefault();
    const dropdown = document.getElementById("port-select-dropdown");
    [...dropdown.children]
        .forEach(c => c.remove());
    const response = await fetch(e.target.value);
    const ports = await response.json();
    [...ports]
        .forEach(p => dropdown.innerHTML += `<option value="${p}">${p}</option>`);
}

function createSensor() {

}

function alterSensor() {
    
}