$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/PayeeEntry";
    });

    fetchDepartment();

    function fetchDepartment() {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetAllDept",
            success: function (data) {
                html = data;
                $("#ddlDepartment").html(html);
            }
        });
        return html;
    }

    $(document).on('click', '#btn_submit', function () {

        //var pay_hash = $(this).attr('rel');
        //var b_hash = $(this).attr('id');

        //$('#myForward').modal({ backdrop: 'static', keyboard: false });
        //$(".show_forward_btn_dtls").html('');
        //$(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        //var url = '/Home/ajax_PayeeEntryForm';
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

        var payeeName = $("#txt_payee_name").val().trim();
        var pan = $("#txt_pan").val().trim();
        var mobile = $("#txt_mobile").val().trim();
        var gst = $("#txt_gst").val().trim();
        var accno = $("#txt_ac").val().trim();
        var deptId = $("#ddlDepartment").val().trim();

        if (payeeName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Payee Name!',
            })
            return;
        }
        if (pan == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Pan Number!',
            })
            return;
        }
        if (mobile == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Mobile Number!',
            })
            return;
        }
        if (gst == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter GST Number!',
            })
            return;
        }
        if (accno == "") {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Account Number!',
            })
            return;
        }
        if (deptId == '0') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select DepartMent!',
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
                savePayeeEntryMaster();
                $("#bill_form").trigger('reset');
            }
        })
    });
    function savePayeeEntryMaster() {
        var info = {
            payeeName: $("#txt_payee_name").val().trim(),
            pan: $("#txt_pan").val().trim(),
            mobile: $("#txt_mobile").val().trim(),
            gst: $("#txt_gst").val().trim(),
            accno: $("#txt_ac").val().trim(),
            deptId: parseInt($("#ddlDepartment").val().trim())
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        //$(".confirm_nodal_frwd_claim").hide();
        //$(".frwd_btn_no").hide();
        var url = '/Home/ajax_confirm_PayeeEntryForm';
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

        
    //});
});