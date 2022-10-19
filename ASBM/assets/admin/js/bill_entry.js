﻿$(document).ready(function () {

    $(document).on('click', '#btn_submit', function () {
        debugger;

        TypeOfBillName = $("#txtBillName").val().trim();

        if (TypeOfBillName == '') {
            Swal.fire({
                icon: 'warning',               
                text: 'Oops...!Please Enter Bill Name!',
            })
            return;
        }

        
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.isConfirmed) {
                saveBillMaster();
                $("#mstbill_form").trigger('reset');
                //Swal.fire(
                //    'Deleted!',
                //    'Your file has been deleted.',
                //    'success'
                //)
            }
        })
    });

    function saveBillMaster() {
        debugger;
        var info = {
            TypeOfBillName: $("#txtBillName").val().trim()
        };

        var url = '/Home/ajax_confirm_BillEntryForm';
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: info,
            success: function (data) {
                //$(".frwd_btn_no").show();
                //$('.modal_forward_claim_body').html(data);

                var check_result = data;
                if (check_result == 1) {
                    /*$('.modal_forward_claim_body').html('<div class="alert alert-success">Bill Allotement Submitted Successfully</div>');*/
                    swal.fire({
                        icon: 'success',
                        title: 'SAVED!',
                        text: 'Your file has been saved.'
                    }).then(function () {
                        location.reload();
                    });
                }
                else {
                    /*$('.modal_forward_claim_body').html('<div class="alert alert-danger">Failed</div>');*/
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                    })
                }
            },
        });
    }

});