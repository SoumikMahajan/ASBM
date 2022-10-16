$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/OfficerEntry";
    });

    $(document).on('change', '#ddlUserType', function () {
        var typeId = parseInt($("#ddlUserType").val());
        if (typeId == 2) {
            $(".department_ddl").removeClass('d-lg-none');
        }
        else {
            $(".department_ddl").addClass('d-lg-none');
        }
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

        $('#myForward').modal({ backdrop: 'static', keyboard: false });
        $(".show_forward_btn_dtls").html('');
        $(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        var url = '/Home/ajax_OfficerEntryForm';
        $.ajax({
            type: 'GET',
            url: url,
            data: $('#bill_form').serialize(),
            success: function (result) {
                $('.modal_forward_claim_body').html(result);
            },
            beforeSend: function () {
                $('.modal_forward_claim_body').html('<div class="loader_con"><div class="loader"></div>Loading ...</div>');
            }
        });
    });

    $(document).on('click', '.confirm_nodal_frwd_claim', function (event) {
        //event.preventDefault();

        var info = {
            officerName: $("#txt_officer_name").val().trim(),
            pan: $("#txt_pan").val().trim(),
            mobile: $("#txt_mobile").val().trim(),
            gpf: $("#txt_gpf").val().trim(),
            DeptId: parseInt($("#ddlDepartment").val().trim()),
            pass: $("#txt_pass").val(),
            userTypeId: parseInt($("#ddlUserType").val().trim())
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        $(".confirm_nodal_frwd_claim").hide();
        $(".frwd_btn_no").hide();
        var url = '/Home/ajax_confirm_OfficerEntryForm';
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: info,
            success: function (data) {
                $(".frwd_btn_no").show();
                //$('.modal_forward_claim_body').html(data);

                var check_result = data;
                if (check_result == 1) {
                    $('.modal_forward_claim_body').html('<div class="alert alert-success">Officer Details Submitted Successfully</div>');
                }
                else {
                    $('.modal_forward_claim_body').html('<div class="alert alert-danger">Failed</div>');
                }
            },
            beforeSend: function () {
                $('.modal_forward_claim_body').html('<div class="loader_con"><div class="loader"></div>Loading ...</div>');
            }
        });
    });
});