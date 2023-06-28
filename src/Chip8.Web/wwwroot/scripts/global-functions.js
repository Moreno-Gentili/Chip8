function update(timeStamp) {
    window.requestAnimationFrame(update);
    window.emulator.invokeMethodAsync('Update', timeStamp);
}

function normalizeKey(key) {
    key = key.toUpperCase();
    switch (key) {
        case "ARROWLEFT":
            return "←";
        case "ARROWRIGHT":
            return "→";
        case "ARROWUP":
            return "↑";
        case "ARROWDOWN":
            return "↓";
        case " ":
            return "␣";
        case "1":
        case "2":
        case "3":
        case "4":
        case "Q":
        case "W":
        case "E":
        case "R":
        case "A":
        case "S":
        case "D":
        case "F":
        case "Z":
        case "X":
        case "C":
        case "V":
            return key;
        default:
            return null;
    }
}

function initEmulator(emulator) {
    window.emulator = emulator;

    window.addEventListener('keydown', evt => {
        const key = normalizeKey(evt.key);
        if (key !== null) {
            evt.stopImmediatePropagation();
            evt.preventDefault();
            window.emulator.invokeMethodAsync('HandleKeyDown', key);
        }
    }, true);

    window.addEventListener('keyup', evt => {
        const key = normalizeKey(evt.key);
        if (key !== null) {
            evt.stopImmediatePropagation();
            evt.preventDefault();
            window.emulator.invokeMethodAsync('HandleKeyUp', key);
        }
    }, true);

    window.requestAnimationFrame(update);
    let renderInterval;
    renderInterval = setInterval(function () {
        if ('renderCrt' in window) {
            clearInterval(renderInterval);
            renderCrt();
        }
    }, 200);
}

let audioContext;
let audioOscillator;

function enableAudio() {
    audioContext = new (window.AudioContext || window.webkitAudioContext)();
    audioOscillator = audioContext.createOscillator();
    audioOscillator.type = 'sawtooth';
    audioOscillator.frequency.value = 700;
    audioOscillator.start();
}

function disableAudio() {
    audioContext = null;
    audioOscillator = null;
}

function playAudio() {
    if (audioOscillator) {
        audioOscillator.connect(audioContext.destination);
    }
}

function stopAudio() {
    if (audioOscillator) {
        audioOscillator.disconnect();
    }
}

function promptFileSelection(element) {
    if (element) {
        element.click();
    }
}

function focusMainButton() {
    const element = document.querySelector('button[data-key="W"]');
    if (element) {
        element.focus();
    }
}

function setFullscreen(element) {
    if (element) {
        element.requestFullscreen();
    }
}

document.body.addEventListener('dragover', function (evt) {
    evt.preventDefault();
    this.dataset.dragging = '1';
}, true);

document.body.addEventListener('dragend', function (evt) {
    evt.preventDefault();
    this.dataset.dragging = null;
}, true);

document.body.addEventListener('dragleave', function (evt) {
    evt.preventDefault();
    this.dataset.dragging = null;
}, true);

document.body.addEventListener('drop', function (evt) {
    evt.preventDefault();
    this.dataset.dragging = null;
    if (evt.dataTransfer.files && evt.dataTransfer.files.length > 0) {

        const file = evt.dataTransfer.files[0];
        const reader = new FileReader();
        reader.onload = function (e) {
            const arrayBuffer = new Uint8Array(reader.result);
            window.emulator.invokeMethodAsync('HandleFile', file.name, arrayBuffer);
        }

        reader.readAsArrayBuffer(file);
    }
    
}, true);

function showError(element) {
    if (element) {
        element.showModal();
    }
}

function startBlazor() {
    let loadedCount = 0;
    const resourcesToLoad = [];
    Blazor.start({
        loadBootResource:
            function (type, filename, defaultUri, integrity) {
                if (type == "dotnetjs")
                    return defaultUri;

                const fetchResources = fetch(defaultUri, {
                    cache: 'no-cache',
                    integrity: integrity
                });

                resourcesToLoad.push(fetchResources);

                fetchResources.then((r) => {
                    loadedCount += 1;
                    if (filename == "blazor.boot.json")
                        return;
                    const totalCount = resourcesToLoad.length;
                    const percentLoaded = 10 + parseInt((loadedCount * 90.0) / totalCount);
                    const progressbar = document.getElementById('progressbar');
                    if (totalCount > 1) {
                        progressbar.hidden = false;
                    }
                    progressbar.value = percentLoaded;
                });

                return fetchResources;
            }
    });
}