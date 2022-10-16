$(document).ready(function () {

    //$(document).on('click', '.swal2-confirm', function () {
    //    window.location.href = "/Home/BillSubmissionForm";
    //});

    fetchDepartment();
    fetchFund();
    inputboxshowhide()
    function inputboxshowhide() {
        $("#IndHideShow").hide();
        $("#HufHideShow").hide();
        $("#ltdHideShow").hide();
        $("#pvtHideShow").hide();
        $("#SoleHideShow").hide();
        $("#PartnershipHideShow").hide();
    }

    $(document).on("click", ".showhide", function () {
        //debugger;
        var companytype = $('input[type="radio"][name="inlineRadioOptions"]:checked').val().trim();

        if (companytype == 'Individual') {
            $("#IndHideShow").show(300);
            $("#HufHideShow").hide(300);
            $("#ltdHideShow").hide(300);
            $("#pvtHideShow").hide(300);
            $("#SoleHideShow").hide(300);
            $("#PartnershipHideShow").hide(300);
        }

        if (companytype == 'HUF') {
            $("#HufHideShow").show(300);
            $("#IndHideShow").hide(300);
            $("#ltdHideShow").hide(300);
            $("#pvtHideShow").hide(300);
            $("#SoleHideShow").hide(300);
            $("#PartnershipHideShow").hide(300);
        }

        if (companytype == 'Limited Company') {
            $("#ltdHideShow").show(300);
            $("#HufHideShow").hide(300);
            $("#IndHideShow").hide(300);
            $("#pvtHideShow").hide(300);
            $("#SoleHideShow").hide(300);
            $("#PartnershipHideShow").hide(300);
        }

        if (companytype == 'PVT LTD Company') {
            $("#pvtHideShow").show(300);
            $("#ltdHideShow").hide(300);
            $("#HufHideShow").hide(300);
            $("#IndHideShow").hide(300);
            $("#SoleHideShow").hide(300);
            $("#PartnershipHideShow").hide(300);
        }
        if (companytype == 'SOLE PROPRIETOR') {
            $("#SoleHideShow").show(300);
            $("#IndHideShow").hide(300);
            $("#HufHideShow").hide(300);
            $("#ltdHideShow").hide(300);
            $("#pvtHideShow").hide(300);
            $("#PartnershipHideShow").hide(300);
        }


        if (companytype == 'PARTNERSHIP FIRM') {
            $("#PartnershipHideShow").show(300);
            $("#SoleHideShow").hide(300);
            $("#IndHideShow").hide(300);
            $("#HufHideShow").hide(300);
            $("#ltdHideShow").hide(300);
            $("#pvtHideShow").hide(300);
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
        debugger;
        var CompanyCategoryName = $("input:radio[name=inlineRadioOptions]:checked").val();
        var IndividualCmpName = $("#txtIndividual").val().trim();
        var HUFCmpName = $("#txtHuf").val().trim();
        var LTDCmpName = $("#txtLtd").val().trim();
        var PVTCmpName = $("#txtPvt").val().trim();
        var SoleProprietorCmpName = $("#txtSoleProprietor").val().trim();
        var PartnershipCmpName = $("#txtpartnership").val().trim();
        var DepartmentId = parseInt($("#ddlDepartment").val());
        var Pan = $("#txt_pan").val().trim();
        var Gst = $("#txt_gst").val().trim();
        var FundId = parseInt($("#ddlFund").val());
        var WorkDesc = $("#txt_work_desc").val().trim();
        var Amount = $("#txt_amount").val().trim();

        if (CompanyCategoryName === undefined) {
            Swal.fire('Please Select Company category')
            return;
        }

        if (CompanyCategoryName == 'Individual' && IndividualCmpName == '') {
            Swal.fire('Please Type Your Company Name')
            return;
        }
        if (CompanyCategoryName == 'HUF' && HUFCmpName == '') {
            Swal.fire('Please Type Your Company Name')
            return;
        }
        if (CompanyCategoryName == 'Limited Company' && LTDCmpName == '') {
            Swal.fire('Please Type Your Company Name')
            return;
        }
        if (CompanyCategoryName == 'PVT LTD Company' && PVTCmpName == '') {
            Swal.fire('Please Type Your Company Name')
            return;
        }
        if (CompanyCategoryName == 'SOLE PROPRIETOR' && SoleProprietorCmpName == '') {
            Swal.fire('Please Type Your Company Name')
            return;
        }
        if (CompanyCategoryName == 'PARTNERSHIP FIRM' && PartnershipCmpName == '') {
            Swal.fire('Please Type Your Company Name')
            return;
        }
        if (DepartmentId == '0') {
            Swal.fire('Please Select Department')
            return;
        }
        if (Pan == '') {
            Swal.fire('Please Enter PAN Number')
            return;
        }
        if (Gst == '') {
            Swal.fire('Please Enter GST Number')
            return;
        }
        if (FundId == '0') {
            Swal.fire('Please Select Fund')
            return;
        }
        if (WorkDesc == '') {
            Swal.fire('Please Enter Work Description')
            return;
        }
        if (Amount == '') {
            Swal.fire('Please Enter Amount')
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
                saveBillSubmission();
                $("#bill_form").trigger('reset');
                //Swal.fire(
                //    'Deleted!',
                //    'Your file has been deleted.',
                //    'success'
                //)
            }
        })
        //$('#myForward').modal({ backdrop: 'static', keyboard: false });
        //$(".show_forward_btn_dtls").html('');
        //$(".show_forward_btn_dtls").append('<input type="submit" class="btn btn-md btn-success confirm_nodal_frwd_claim" value="Submit">');
        //var url = '/Home/ajax_BillSubmissionForm';
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

    function saveBillSubmission() {
        debugger;
        var cmp = '';
        var IndividualCmpName = $("#txtIndividual").val().trim();
        var HUFCmpName = $("#txtHuf").val().trim();
        var LTDCmpName = $("#txtLtd").val().trim();
        var PVTCmpName = $("#txtPvt").val().trim();
        var SoleProprietorCmpName = $("#txtSoleProprietor").val().trim();
        var PartnershipCmpName = $("#txtpartnership").val().trim();

        if (IndividualCmpName != '') {
            cmp = IndividualCmpName;
        }
        else if (HUFCmpName != '') {
            cmp = HUFCmpName;
        }
        else if (LTDCmpName != '') {
            cmp = LTDCmpName;
        }
        else if (PVTCmpName != '') {
            cmp = PVTCmpName;
        }
        else if (SoleProprietorCmpName != '') {
            cmp = SoleProprietorCmpName;
        }
        else if (PartnershipCmpName != '') {
            cmp = PartnershipCmpName;
        }

        var info = {

            //PropriterName: $("#txtSoleProprietorName").val(),
            CompanyCategoryName: $("input:radio[name=inlineRadioOptions]:checked").val(), //parseInt($("#inlineRadioOptions").val()),
            CompanyName: cmp,
            /*CompanyName: $("#txtCmpName").val(),*/
            DepartmentId: parseInt($("#ddlDepartment").val()),
            Pan: $("#txt_pan").val().trim(),
            Gst: $("#txt_gst").val().trim(),
            FundId: parseInt($("#ddlFund").val()),
            WorkDesc: $("#txt_work_desc").val().trim(),
            Amount: $("#txt_amount").val().trim()
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        //$(".confirm_nodal_frwd_claim").hide();
        //$(".frwd_btn_no").hide();
        var url = '/Home/ajax_confirm_BillSubmissionForm';
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


    $(document).on('click', '#BtnDelete', function (event) {
        debugger;
        var id = $(this).data("id");
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Home/ajax_Delete_Bill?id=" + id,
                    type: "POST",
                    dataType: "json",
                    async: false,
                    contentType: false,
                    processData: false,
                    cache: false,
                    success: function (data) {
                        location.reload();
                    },
                    error: function () {

                    }
                });
            }
        })

    });
    $(document).on('click', '#BtnEdit', function (event) {
        debugger;
        var id = $(this).data("id");
        $.ajax({
            url: "/Home/ajax_GetBillDetailsForUpdate?id=" + id,
            type: "POST",
            dataType: "json",
            async: false,
            contentType: false,
            processData: false,
            cache: false,
            success: function (data) {
                location.reload();
            },
            error: function () {

            }
        });

    });
});