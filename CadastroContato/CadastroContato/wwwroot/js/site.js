﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    getDataTable('#table-usuarios');
    getDataTable('#table-contatos');
});

function getDataTable(id) {
    $(id).DataTable();
}

// Popup message auto fadeout
$('.alert').fadeIn().delay(2000).fadeOut(function () {
    $(this).remove()
});
