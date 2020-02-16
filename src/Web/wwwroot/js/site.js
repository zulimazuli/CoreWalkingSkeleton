// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {

    $('#items-table').DataTable({
        deferRender: true,
        processing: true,
        serverSide: true,
        stateSave: true, //change to true
        paging: true,
        ordering: true,
        searching: true,
        language:
        {
            processing: "<span>Loading data...</span>"
        },
        ajax: {
            url: "Items/LoadItems",
            type: "POST",
            contentType: "application/json",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        columns: [
            
            { data: "name" },
            { data: "description" },
            {
                data: "priority",
                render: function(data) {
                    switch (data) {
                        case 1:
                            return "High";
                        case 2:
                            return "Medium";
                        case 3:
                            return "Low";
                        default:
                            return "";
                    }
                }
            },
            {
                data: "id",
                render: function(data, type, row) {
                    return "<a href =\"/Items/EditPopup/" +
                        data +
                        "\" data-target=\"#myModal\" data-toggle=\"modal\">EDIT</a>";
                }                
            }
        ]
    });
    setTimeout(function () {
        table.processing(false);
    }, 2000);

    $('input[type="text"]').change(function () {
        this.value = $.trim(this.value);
    });


    $('#myModal').on('show.bs.modal', function (e) {

        var button = $(e.relatedTarget);
        var modal = $(this);

        // or, load content from value of data-remote url
        modal.find('.modal-content').load(button.attr('href'));

    });
});