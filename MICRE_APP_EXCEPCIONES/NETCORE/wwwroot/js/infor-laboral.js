function toggleDivSelector(selectId, startIndex, ...divIds) {
    const select = document.getElementById(selectId);
    const divs = divIds.map(id => document.getElementById(id));

    select.addEventListener('change', function () {
        const value = select.value;

        divs.forEach((div, index) => {
            if (index + startIndex == value) {
                div.style.display = 'block';
                setRequiredAttributes(div, true);
            } else {
                div.style.display = 'none';
                setRequiredAttributes(div, false);
            }
        });
    });
}

function setRequiredAttributes(div, isRequired) {
    const inputs = div.querySelectorAll('input, select, textarea');
    inputs.forEach(input => {
        if (isRequired) {
            input.setAttribute('required', '');
            input.setAttribute('disabled', '');
        } else {
            input.removeAttribute('required');
            input.removeAttribute('disabled');
        }
    });
}

// Call the function with the IDs and starting index
toggleDivSelector('infLaboral', 7, 'Dependiente', 'Independiente', 'PyME', 'RentistaJubilado', 'Ingresomixto');

toggleDivSelector('infLaboralCon', 7, 'DependienteCon', 'IndependienteCon', 'PyMECon', 'RentistaJubiladoCon', 'IngresomixtoCon');
