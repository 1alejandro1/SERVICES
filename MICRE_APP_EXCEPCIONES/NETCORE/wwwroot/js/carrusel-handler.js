document.querySelectorAll('.prev').forEach(button => {
    button.addEventListener('click', function () {
        $('#formCarousel').carousel('prev');
    });
});

document.querySelectorAll('.next').forEach(button => {
    button.addEventListener('click', function () {
        const currentForm = button.closest('.carousel-item').querySelector('form');
        if (currentForm.checkValidity()) {
            $('#formCarousel').carousel('next');
        } else {
            currentForm.reportValidity();
        }
    });
});

(() => {
    'use strict'
    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    const forms = document.querySelectorAll('.needs-validation')
    // Loop over them and prevent submission
    Array.from(forms).forEach(form => {

        const sextbutoon = form.querySelector('.send');
        sextbutoon.addEventListener('click', event => {
            if (!form.checkValidity()) {
                event.preventDefault()
                event.stopPropagation()
            }

            form.classList.add('was-validated')
        }, false)
    })
})()
