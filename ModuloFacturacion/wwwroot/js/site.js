// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#IdClienteNavigation_NombreCliente').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Home/BuscarCliente',
                type: 'POST',
                dataType: 'json',
                data: { nombre: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        console.log(item);
                        return { label: item.nombreCliente, value: item.idCliente };
                    }))
                },
                error: function (data) {
                    console.log(data);
                }
            })
        }
    });
});