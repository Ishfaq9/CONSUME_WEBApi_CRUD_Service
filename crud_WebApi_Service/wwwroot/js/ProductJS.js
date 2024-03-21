
$(function () {
    
    GetProducts();
});

//read data

function GetProducts() {
    $.ajax({
        url: '/product/GetProducts',
        type: 'get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',

        success: function (response) {
            debugger
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="5">' + 'Products not available in the database' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            }
            else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.id + '</td >';
                    object += '<td>' + item.productName + '</td >';
                    object += '<td>' + item.price + '</td >';
                    object += '<td>' + item.qty + '</td >';
                    object += '<td><a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a></td>';


                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('unable to  read the data.');
        }
    });
}


// click the add new button
$('#btnAdd').on('click', function () {
    
    ClearData();
    $('#ProdcutModal').modal('show');
    $('#modalTitle').text('Add New product');
    $('#Save').show(); // Use .hide() and .show() for better readability
    $('#Update').hide();
    
});

// insert data into database
function Insert() {
    

    var isValid = Validate();

    if (!isValid) {
        return false;
    }

    var formData = {
        id: $('#Id').val(),
        productName: $('#ProductName').val(),
        price: $('#Price').val(),
        qty: $('#Qty').val()
    };

    $.ajax({
        url: '/product/Insert',
        data: formData,
        type: 'POST',
        success: function (response) {
            if (!response || response.length === 0) {
                alert('Unable to save the data');
            } else {
                HideModel();
                GetProducts();
                alert(response);
            }
        },
        error: function () {
            alert('Unable to save the data');
        }
    });
}
 //hide the modal
function HideModel() {
    ClearData();
    $('#ProdcutModal').modal('hide');
    location.reload();
}


function ClearData() {
    $('#ProductName, #Price, #Qty').val('').css('border-color', 'lightGrey');
    $('#Id').val(0);
}

//validate the data
function Validate() {
    var isValid = true;

    if ($('#ProductName').val().trim() === "") {
        $('#ProductName').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#ProductName').css('border-color', 'lightGrey');
    }

    if ($('#Price').val().trim() === "") {
        $('#Price').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Price').css('border-color', 'lightGrey');
    }

    if ($('#Qty').val().trim() === "") {
        $('#Qty').css('border-color', 'Red');
        isValid = false;
    } else {
        $('#Qty').css('border-color', 'lightGrey');
    }

    return isValid;
}

// Consolidated event handler for all three fields
$('#ProductName, #Price, #Qty').change(function () {
    Validate();
});


//$('#ProductName').change(function () {
//    Validate();
//});

//$('#Price').change(function () {
//    Validate();
//});

//$('#Qty').change(function () {
//    Validate();
//});


//edit the data
function Edit(id) {
    $.ajax({
        url: 'product/Edit?id=' + id,
        type: 'GET', // Specify the HTTP method in uppercase
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            if (!response) {
                alert('Unable to read the data');
            } else if (response.length === 0) {
                alert('Data is not available with the id ' + id);
            } else {
                
                $('#ProdcutModal').modal('show');
                $('#modalTitle').text('Update the product');
                $('#Save').hide(); // Use .hide() and .show() for better readability
                $('#Update').show();
                $('#Id').val(response.id);
                $('#ProductName').val(response.productName);
                $('#Price').val(response.price);
                $('#Qty').val(response.qty);
            }
        },
        error: function () {
            alert('Unable to read the data.');
        }
    });
}


function Update() {
    //validate the data
    var isValid = Validate();

    if (!isValid) {
        return false;
    }

    var formData = new Object();
    formData.id = $('#Id').val();
    formData.productName = $('#ProductName').val();
    formData.price = $('#Price').val();
    formData.qty = $('#Qty').val();

    $.ajax({
        url: '/product/Update',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                alert('Unable to save the data');
            }
            else {
                HideModel();
                GetProducts();
                alert(response);
            }

        },
        error: function () {
            alert('Unable to save the data');
        }
    });
}

//delte the record from database
function Delete(id) {
    if (confirm('Are you sure want to delete this record?')) {
        $.ajax({
            url: 'product/Delete?id=' + id,
            type: 'POST', // Specify the HTTP method in uppercase

            success: function (response) {
                if (!response) {
                    alert('Unable to Delete the data');
                }
                else {
                    GetProducts();
                    alert(response);
                }
            },
            error: function () {
                alert('Unable to Delete the data.');
            }
        });
    }
 
}


