$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/MstTreasuryDetails";
    });

    fetchSchemeName();

    function fetchSchemeName() {
        debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetAllSchemeName",
            success: function (data) {
                $("#ddlScheme").html(data);
            }
        });
    }

    $(document).on('click', '#btn_submit', function () {

        var schemeId = parseInt($("#ddlScheme").val());
        var adviceNo = $("#txtAdviceNo").val().trim();
        var adviceDate = $("#txtAdviceDate").val().trim();

        if (schemeId == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Scheme!',
            })
            return;
        }

        if (adviceNo == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Advice Number!',
            })
            return;
        }

        if (adviceDate == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Advice Date!',
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
                saveTreasuryEntryMaster();
                $("#bill_form").trigger('reset');
            }
        })
    });

    function saveTreasuryEntryMaster() {
        var info = {
            schemeId: parseInt($("#ddlScheme").val()),
            adviceNo: $("#txtAdviceNo").val().trim(),
            adviceDate: $("#txtAdviceDate").val().trim()
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();
    
        var url = '/Home/ajax_confirm_TreassuryEntryForm';
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
    //        adviceNo: $("#txtAdviceNo").val().trim(),
    //        adviceDate: $("#txtAdviceDate").val().trim()
    //    };

    //    //info = JSON.stringify(info)
    //    //var valdata = $("#bill_form").serialize();

    //    $(".confirm_nodal_frwd_claim").hide();
    //    $(".frwd_btn_no").hide();
    //    var url = '/Home/ajax_confirm_TreassuryEntryForm';
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