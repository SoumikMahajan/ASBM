$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/RandomBillGeneretorForm";
    });

    fetchDepartment();
    fetchFund();
    fetchBillType();

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

    function fetchFund() {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetAllFund",
            success: function (data) {
                html = data;
                $("#ddlFund").html(html);
            }
        });
        return html;
    }

    function fetchBillType() {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetAllBillType",
            success: function (data) {
                html = data;
                $("#ddlTypeBill").html(html);
            }
        });
        return html;
    }

    $(document).on('click', '#btn_submit', function () {

        Name = $("#txtName").val().trim();
        BillTypeId = parseInt($("#ddlTypeBill").val().trim());
        DepartmentId = parseInt($("#ddlDepartment").val().trim());
        FundId = parseInt($("#ddlFund").val().trim());
        WorkDesc = $("#txtDescriptionOfWork").val().trim();
        Mobile = $("#txtMobileNo").val().trim();

        if (Name == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Name!',
            })
            return;
        }

        if (BillTypeId == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Bill Type!',
            })
            return;
        }

        if (DepartmentId == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Department!',
            })
            return;
        }

        if (FundId == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Fund!',
            })
            return;
        }

        if (WorkDesc == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Description of Work!',
            })
            return;
        }

        if (Mobile == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Enter Mobile No!',
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
                saveOfferEntryMaster();
                $("#bill_form").trigger('reset');
            }
        })

        //$('#myForward').modal({ backdrop: 'static', keyboard: false });
        //$(".show_forward_btn_dtls").html('');
        //$(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        //var url = '/Home/ajax_RandomBillGeneratorForm';
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
    });

    function saveOfferEntryMaster() {
        var info = {
            Name: $("#txtName").val().trim(),
            BillTypeId: parseInt($("#ddlTypeBill").val().trim()),
            DepartmentId: parseInt($("#ddlDepartment").val().trim()),
            FundId: parseInt($("#ddlFund").val().trim()),
            WorkDesc: $("#txtDescriptionOfWork").val().trim(),
            Mobile: $("#txtMobileNo").val().trim()
        };

        var url = '/Home/ajax_confirm_RandomBillGeneratorForm';
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

    //$(document).on('click', '.confirm_nodal_frwd_claim', function (event) {
    //    //event.preventDefault();

    //    var info = {
    //        Name: $("#txtName").val().trim(),
    //        BillTypeId: parseInt($("#ddlTypeBill").val().trim()),
    //        DepartmentId: parseInt($("#ddlDepartment").val().trim()),
    //        FundId: parseInt($("#ddlFund").val().trim()),
    //        WorkDesc: $("#txtDescriptionOfWork").val().trim(),
    //        Mobile: $("#txtMobileNo").val().trim()
    //    };

    //    //info = JSON.stringify(info)
    //    //var valdata = $("#bill_form").serialize();

    //    $(".confirm_nodal_frwd_claim").hide();
    //    $(".frwd_btn_no").hide();
    //    var url = '/Home/ajax_confirm_RandomBillGeneratorForm';
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
    //                $('.modal_forward_claim_body').html('<div class="alert alert-success">Random Bill Generated Successfully</div>');
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