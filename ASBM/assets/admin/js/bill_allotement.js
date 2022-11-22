$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/BillAllotment";
    });

    //fetchDocketNo();
    fetchDepartment();
    //fetchOfficer();

    //function fetchDocketNo() {
    //    //debugger;
    //    var html = "";
    //    $.ajax({
    //        type: "GET",
    //        async: false,
    //        dataType: "text",
    //        contentType: "application/json; charset=utf-8",
    //        url: "/Home/GetAllDocketNo",
    //        success: function (data) {
    //            html = data;
    //            $("#ddlDocketNo").html(html);
    //        }
    //    });
    //    return html;
    //}

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
    function fetchDepartmentById(deptId) {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetDeptById/?deptId=" + deptId,
            success: function (data) {
                html = data;
                $("#ddlDepartment").html(html);
            }
        });
        return html;
    }

    $(document).on("change", "#ddlDepartment", function () {
        var deptId = parseInt($("#ddlDepartment").val());

        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetAllOfficer/?deptId=" + deptId,
            success: function (data) {
                $("#ddlOfficer").html(data);
            }
        });
    });

    $(document).on("change", "#ddlDepartment", function () {
        var deptId = parseInt($("#ddlDepartment").val());

        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetAllDocketNoByDept/?deptId=" + deptId,
            success: function (data) {
                $("#ddlDocketNo").html(data);
            }
        });
    });

    //function fetchOfficer() {
    //    //debugger;
    //    var html = "";
    //    $.ajax({
    //        type: "GET",
    //        async: false,
    //        dataType: "text",
    //        contentType: "application/json; charset=utf-8",
    //        url: "/Home/GetAllOfficer",
    //        success: function (data) {
    //            html = data;
    //            $("#ddlOfficer").html(html);
    //        }
    //    });
    //    return html;
    //}

    $(document).on('click', '#btn_submit', function () {

        //var pay_hash = $(this).attr('rel');
        //var b_hash = $(this).attr('id');

        //$('#myForward').modal({ backdrop: 'static', keyboard: false });
        //$(".show_forward_btn_dtls").html('');
        //$(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        //var url = '/Home/ajax_BillAllotementForm';
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
        debugger;
        DocketNo = $("#ddlDocketNo").val().trim();
        DepartmentId = parseInt($("#ddlDepartment").val().trim());
        OfficerId = parseInt($("#ddlOfficer").val().trim());
        AllotedDatte = $("#AllottedDate").val().trim();

        if (DepartmentId == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Department!',
            })
            return;
        }

        if (DocketNo == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Docket No!',
            })
            return;
        }

        if (OfficerId == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Officer!',
            })
            return;
        }

        if (AllotedDatte == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Choose Alloted Date!',
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
                saveBillAllotement();
                $("#bill_form").trigger('reset');
            }
        })
    });

    function saveBillAllotement() {
        var info = {
            DocketNo: $("#ddlDocketNo").val().trim(),
            DepartmentId: parseInt($("#ddlDepartment").val().trim()),
            OfficerId: parseInt($("#ddlOfficer").val().trim()),
            AllotedDatte: $("#AllottedDate").val().trim()
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        //$(".confirm_nodal_frwd_claim").hide();
        //$(".frwd_btn_no").hide();
        var url = '/Home/ajax_confirm_BillAllotementForm';
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
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!',
                    })
                }
            },          
        });
    }

    $(document).on('click', '#BtnEdit', function (event) {
        debugger;
        var id = $(this).data("id");
        $.ajax({
            url: "/Home/ajax_GetBillAllotementDetailsForUpdate?id=" + id,
            type: "GET",
            dataType: "json",
            //async: false,            
            success: function (data) {
                $('#hiddenBillAllotmentId').val(data.bill_allotement_id_pk);
                $('#AllottedDate').val(data.bill_allotement_date);
                fetchDepartmentById(data.bill_allotement_dept_id_fk);               

                $("#btn_update").removeClass('d-lg-none');
                $("#btn_submit").addClass('d-lg-none');
            },
            error: function () {

            }
        });

    });
});