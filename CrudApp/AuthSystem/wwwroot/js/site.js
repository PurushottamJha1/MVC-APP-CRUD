// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function openAddItemModal() {
    console.log("Here");
    $('#addItemModal').modal('show');
}

function editItem(button) {


    var row = $(button).closest('tr');
    var itemName = row.find('td:first-child').text();
    var itemValue = row.find('td:nth-child(2)').text();

    $('#editItemName').val(itemName);
    $('#editItemValue').val(itemValue);
    var itemId = $(button).data('item-id');
    $('#editItemId').val(itemId);
    $('#editItemModal .modal-title').text('Edit Item');
    $('#editItemForm').attr('asp-action', 'Edit');

    $('#editItemModal').modal('show');
}

function deleteItem(button) {

    $(button).closest('tr').remove();
}




    function openEditItemModal(button) {
            var row = $(button).closest('tr');
    var itemId = $(button).data('item-id');
    var itemName = row.find('td:first-child').text();
    var itemValue = row.find('td:nth-child(2)').text();

    $('#editItemId').val(itemId);
    $('#editItemName').val(itemName);
    $('#editItemValue').val(itemValue);

    $('#editItemModal').modal('show');

    }


function deleteItem(button) {


    console.log("hi")

    $(button).closest('tr').remove();

}