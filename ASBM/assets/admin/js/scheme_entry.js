$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/MstSchemeDetails";
    });

    $(document).on('click', '#btn_submit', function () {

        //var pay_hash = $(this).attr('rel');
        //var b_hash = $(this).attr('id');

        //$('#myForward').modal({ backdrop: 'static', keyboard: false });
        //$(".show_forward_btn_dtls").html('');
        //$(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        //var url = '/Home/ajax_SchemeEntryForm';
        //$.ajax({
        //    type: 'GET',
        //    url: url,
        //    data: $('#bill_form').serialize(),
        //    success: function (result) {
        //        $('.modal_forward_claim_body').html(result);
        //    },
        //    beforeSend: function () {
        //        $('.modal_forward_claim_body').html('<div class="loader_con"><div class="loader"></div>Loading ...</div>');
        //    }
        //});
        var schemeName = $("#txtSchemeName").val().trim();
        var schemeNo = $("#txtSchemeNo").val().trim();
        if (schemeNo == "") {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Scheme No!',
            })
            return;
        }
        if (schemeName == "") {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Scheme Name!',
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
                saveSchemeMasterMaster();
                $("#bill_form").trigger('reset');
            }
        })
    });

    function saveSchemeMasterMaster() {
        var info = {
            schemeName: $("#txtSchemeName").val().trim(),
            schemeNo: $("#txtSchemeNo").val().trim()
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();
     
        var url = '/Home/ajax_confirm_SchemeEntryForm';
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: info,
            success: function (data) {              
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

    //$(document).on('click', '.confirm_nodal_frwd_claim', function (event) {
    //    //event.preventDefault();

    //    var info = {
    //        schemeName: $("#txtSchemeName").val().trim()
    //    };

    //    //info = JSON.stringify(info)
    //    //var valdata = $("#bill_form").serialize();

    //    $(".confirm_nodal_frwd_claim").hide();
    //    $(".frwd_btn_no").hide();
    //    var url = '/Home/ajax_confirm_SchemeEntryForm';
    //    $.ajax({
    //        type: 'POST',
    //        url: url,
    //        dataType: 'json',
    //        data: info,
    //        success: function (data) {
    //            $(".frwd_btn_no").show();
    //            //$('.modal_forward_claim_body').html(data);

    //            var check_result = data;
    //            if (check_result == 1) {
    //                $('.modal_forward_claim_body').html('<div class="alert alert-success">Payee Details Submitted Successfully</div>');
    //            }
    //            else {
    //                $('.modal_forward_claim_body').html('<div class="alert alert-danger">Failed</div>');
    //            }
    //        },
    //        beforeSend: function () {
    //            $('.modal_forward_claim_body').html('<div class="loader_con"><div class="loader"></div>Loading ...</div>');
    //        }
    //    });
    //});
});