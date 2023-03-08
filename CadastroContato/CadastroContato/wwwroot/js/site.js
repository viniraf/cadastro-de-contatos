// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    getDataTable('#table-usuarios');
    getDataTable('#table-contatos');

    $('.btn-total-contatos').click(function () {
        var usuarioId = $(this).attr('usuario-id');

        $.ajax({
            type: 'GET',
            url: '/Usuario/GetContactsByUserId/' + usuarioId,
            success: function (result) {
                $('#listaContatosUsuario').html(result);
                getDataTable('#table-contatos-usuario');
                $('#modalContatosUsuario').modal();
            }
        });
    });

})

function getDataTable(id) {
    $(id).DataTable();
}

// Popup message auto fadeout
$('.alert').fadeIn().delay(2000).fadeOut(function () {
    $(this).remove()
});
