var excepciones = [];


// Añadir la nueva excepción al array
excepciones.push(nuevaExcepcion);

// Renderizar la nueva excepción en la vista
var excepcionHtml = `
                <div class="alert alert-warning" role="alert">
                    <div class="row">
                        <div class="col-2">
                            <strong>Exception</strong>
                            <p>${nuevaExcepcion.Motivo}</p>
                        </div>
                        <div class="col-4">
                            <strong>Detalle de la justificacion</strong>
                            <p>${nuevaExcepcion.Justificacion}</p>
                        </div>
                        <div class="col-3">
                            <strong>Producto</strong>
                            <p>${nuevaExcepcion.Producto}</p>
                        </div>
                        <div class="d-flex justify-content-end col-3">
                            <button class="btn custom-btn">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-fill" viewBox="0 0 16 16">
                                    <path d="M12.854.146a.5.5 0 0 0-.707 0L10.5 1.793 14.207 5.5l1.647-1.646a.5.5 0 0 0 0-.708zm.646 6.061L9.793 2.5 3.293 9H3.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.207zm-7.468 7.468A.5.5 0 0 1 6 13.5V13h-.5a.5.5 0 0 1-.5-.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.5-.5V10h-.5a.5.5 0 0 1-.175-.032l-.179.178a.5.5 0 0 0-.11.168l-2 5a.5.5 0 0 0 .65.65l5-2a.5.5 0 0 0 .168-.11z" />
                                </svg>
                            </button>
                            <button class="btn custom-btn">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                                    <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5" />
                                </svg>
                            </button>
                        </div>
                    </div>
                </div>
            `;

if (currentForm.checkValidity()) {
        document.getElementById('excepciones-container').insertAdjacentHTML('beforeend', excepcionHtml);

        // Actualizar el campo oculto con el array de excepciones
        document.getElementById('excepciones').value = JSON.stringify(excepciones);
    }

// Desmarcar todas las casillas de verificación
let checkboxes = document.querySelectorAll('.check-prod');
checkboxes.forEach(function (checkbox) {
        checkbox.checked = false;
    });

//});


const checkboxes = document.querySelectorAll('.check-prod');
const applyAllCheckbox = document.getElementById('check4');

applyAllCheckbox.addEventListener('change', function () {
    checkboxes.forEach(checkbox => {
        checkbox.checked = this.checked;
    });
});

checkboxes.forEach(checkbox => {
    if (checkbox !== applyAllCheckbox) {
        checkbox.addEventListener('change', function () {
            if (!this.checked) {
                applyAllCheckbox.checked = false;
            } else {
                const allChecked = Array.from(checkboxes).every(cb => cb.checked || cb === applyAllCheckbox);
                applyAllCheckbox.checked = allChecked;
            }
        });
    }
});
