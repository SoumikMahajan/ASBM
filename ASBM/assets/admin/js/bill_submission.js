$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/BillSubmissionForm";
    });

 
    fetchDepartment();
    fetchFund();
    inputboxshowhide()
    function inputboxshowhide() {
        $("#ltdpvt").hide();
        $("#IndSole").hide();
        $("#Partnership").hide();
        $("#Huf").hide();
    }

    $(document).on("click", ".showhide", function () {
        debugger;
        var companytype = $('input[type="radio"][name="inlineRadioOptions"]:checked').val().trim();

        if ((companytype == 'Limited Company') || (companytype == 'PVT LTD Company')) {
            $("#ltdpvt").show(300);
            $("#IndSole").hide(300);
            $("#Partnership").hide(300);
            $("#Huf").hide(300);
        }

        if ((companytype == 'Individual') || (companytype == 'SOLE PROPRIETOR')) {
            $("#IndSole").show(300);
            $("#ltdpvt").hide(300);
            $("#Partnership").hide(300);
            $("#Huf").hide(300);
        }
        if (companytype == 'PARTNERSHIP FIRM') {
            $("#Partnership").show(300);
            $("#ltdpvt").hide(300);
            $("#IndSole").hide(300);
            $("#Huf").hide(300);
        }
        if (companytype == 'HUF') {
            $("#Huf").show(300);
            $("#Partnership").hide(300);
            $("#ltdpvt").hide(300);
            $("#IndSole").hide(300);
        }
            
    });

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

    $(document).on('click', '#btn_submit', function () {

        //var pay_hash = $(this).attr('rel');
        //var b_hash = $(this).attr('id');
        var gstno = $('#txt_gst').val().trim();

        if (gstno == '') {
            Swal.fire('Any fool can use a computer')
            return;
        }
        $('#myForward').modal({ backdrop: 'static', keyboard: false });
        $(".show_forward_btn_dtls").html('');
        $(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        var url = '/Home/ajax_BillSubmissionForm';
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
            CompanyName: $("#txtCmpName").val(),
            PropriterName: $("#txtSoleProprietorName").val(),
            CompanyCategoryName: $("input:radio[name=inlineRadioOptions]").val(), //parseInt($("#inlineRadioOptions").val()),
            DepartmentId: parseInt($("#ddlDepartment").val()),
            Pan: $("#txt_pan").val(),
            Gst: $("#txt_gst").val(),
            FundId: parseInt($("#ddlFund").val()),
            WorkDesc: $("#txt_work_desc").val(),
            Amount: $("#txt_amount").val()
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        $(".confirm_nodal_frwd_claim").hide();
        $(".frwd_btn_no").hide();
        var url = '/Home/ajax_confirm_BillSubmissionForm';
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
                    $('.modal_forward_claim_body').html('<div class="alert alert-success">Bill Allotement Submitted Successfully</div>');
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