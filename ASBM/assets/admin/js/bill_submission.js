$(document).ready(function () {

    //$(document).on('click', '.swal2-confirm', function () {
    //    window.location.href = "/Home/BillSubmissionForm";
    //});

    fetchDepartment();
    fetchFund();
    fetchBillType();
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

    function fetchFundById(fundId) {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetFundById/?fundId=" + fundId,
            success: function (data) {
                html = data;
                $("#ddlFund").html(html);
            }
        });
        return html;
    }

    function fetchBillTypeById(billTypeId) {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Home/GetBillTypeById/?billTypeId=" + billTypeId,
            success: function (data) {
                html = data;
                $("#ddlTypeBill").html(html);
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
        var Mobile = $("#txt_mobile").val().trim();
        var Pan = $("#txt_pan").val().trim();
        var Gst = $("#txt_gst").val().trim();
        var FundId = parseInt($("#ddlFund").val());
        var WorkDesc = $("#txt_work_desc").val().trim();
        var Amount = $("#txt_amount").val().trim();
        var BillTypeId = parseInt($("#ddlTypeBill").val());

        if (CompanyCategoryName === undefined) {
            Swal.fire({
                icon: 'warning',
                text: 'Please Select Company category!',
            })
            return;
        }

        if (CompanyCategoryName == 'Individual' && IndividualCmpName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Type Your Company Name!',
            })
            return;
        }
        if (CompanyCategoryName == 'HUF' && HUFCmpName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Type Your Company Name!',
            })
            return;
        }
        if (CompanyCategoryName == 'Limited Company' && LTDCmpName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Type Your Company Name!',
            })
            return;
        }
        if (CompanyCategoryName == 'PVT LTD Company' && PVTCmpName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Type Your Company Name!',
            })
            return;
        }
        if (CompanyCategoryName == 'SOLE PROPRIETOR' && SoleProprietorCmpName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Type Your Company Name!',
            })
            return;
        }
        if (CompanyCategoryName == 'PARTNERSHIP FIRM' && PartnershipCmpName == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Type Your Company Name!',
            })
            return;
        }
        if (BillTypeId == '0') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Select Bill Type!',
            })
            return;
        }
        if (DepartmentId == '0') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Select Department!',
            })
            return;
        }
        if (Mobile == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Enter Mobile Number!',
            })
            return;
        }
        //if (Pan == '') {
        //    Swal.fire('Please Enter PAN Number')
        //    return;
        //}
        //if (Gst == '') {
        //    Swal.fire('Please Enter GST Number')
        //    return;
        //}
        if (FundId == '0') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Select Fund!',
            })
            //Swal.fire('Please Select Fund')
            return;
        }
        if (WorkDesc == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Enter Work Description!',
            })
            return;
        }
        if (Amount == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Please Enter Amount!',
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
            Amount: $("#txt_amount").val().trim(),
            BillTypeId: parseInt($("#ddlTypeBill").val()),
            Mobile: $("#txt_mobile").val().trim()
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
            type: "GET",
            dataType: "json",
            //async: false,            
            success: function (data) {
                $('#hiddenBillId').val(data.bill_details_id_pk);
                $('#txt_gst').val(data.bill_gst);
                $('#txt_pan').val(data.bill_pan);
                $('#txt_work_desc').val(data.bill_description);
                $('#txt_amount').val(data.bill_amount);
                fetchDepartmentById(data.bill_department_id_fk);
                fetchFundById(data.bill_fund_id_fk);
                fetchBillTypeById(data.bill_type_id_fk);

                var companytype = data.bill_category_id_fk;

                if (companytype == 'Limited Company') {
                    $('#inlineRadio3').prop("checked", true);
                    $("#ltdHideShow").show(300);
                    $("#HufHideShow").hide(300);
                    $("#IndHideShow").hide(300);
                    $("#pvtHideShow").hide(300);
                    $("#SoleHideShow").hide(300);
                    $("#PartnershipHideShow").hide(300);
                    $('#txtLtd').val(data.bill_company_name);
                }
                else if (companytype == 'Individual') {
                    $('#inlineRadio1').prop("checked", true);
                    $("#IndHideShow").show(300);
                    $("#HufHideShow").hide(300);
                    $("#ltdHideShow").hide(300);
                    $("#pvtHideShow").hide(300);
                    $("#SoleHideShow").hide(300);
                    $("#PartnershipHideShow").hide(300);
                    $('#txtIndividual').val(data.bill_company_name);
                }
                else if (companytype == 'HUF') {
                    $('#inlineRadio2').prop("checked", true);
                    $("#HufHideShow").show(300);
                    $("#IndHideShow").hide(300);
                    $("#ltdHideShow").hide(300);
                    $("#pvtHideShow").hide(300);
                    $("#SoleHideShow").hide(300);
                    $("#PartnershipHideShow").hide(300);
                    $('#txtHuf').val(data.bill_company_name);
                }
                else if (companytype == 'PVT LTD Company') {
                    $('#inlineRadio4').prop("checked", true);
                    $("#pvtHideShow").show(300);
                    $("#ltdHideShow").hide(300);
                    $("#HufHideShow").hide(300);
                    $("#IndHideShow").hide(300);
                    $("#SoleHideShow").hide(300);
                    $("#PartnershipHideShow").hide(300);
                    $('#txtPvt').val(data.bill_company_name);
                }
                else if (companytype == 'SOLE PROPRIETOR') {
                    $('#inlineRadio5').prop("checked", true);
                    $("#SoleHideShow").show(300);
                    $("#IndHideShow").hide(300);
                    $("#HufHideShow").hide(300);
                    $("#ltdHideShow").hide(300);
                    $("#pvtHideShow").hide(300);
                    $("#PartnershipHideShow").hide(300);
                    $('#txtSoleProprietor').val(data.bill_company_name);
                }
                else if (companytype == 'PARTNERSHIP FIRM') {
                    $('#inlineRadio6').prop("checked", true);
                    $("#SoleHideShow").hide(300);
                    $("#IndHideShow").hide(300);
                    $("#HufHideShow").hide(300);
                    $("#ltdHideShow").hide(300);
                    $("#pvtHideShow").hide(300);
                    $("#PartnershipHideShow").show(300);
                    $('#txtpartnership').val(data.bill_company_name);
                }

                $("#btn_update").removeClass('d-lg-none');
                $("#btn_submit").addClass('d-lg-none');
            },
            error: function () {

            }
        });

    });


    $(document).on('click', '#btn_update', function () {
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
        var BillTypeId = parseInt($("#ddlTypeBill").val());

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
                UpdateBillSubmission();
                $("#bill_form").trigger('reset');
            }
        })
    });

    function UpdateBillSubmission() {
        debugger;
        var cmp = '';
        var BillId = $('#hiddenBillId').val();
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
            BillId: BillId,
            CompanyCategoryName: $("input:radio[name=inlineRadioOptions]:checked").val(), //parseInt($("#inlineRadioOptions").val()),
            CompanyName: cmp,
            /*CompanyName: $("#txtCmpName").val(),*/
            DepartmentId: parseInt($("#ddlDepartment").val()),
            Pan: $("#txt_pan").val().trim(),
            Gst: $("#txt_gst").val().trim(),
            FundId: parseInt($("#ddlFund").val()),
            WorkDesc: $("#txt_work_desc").val().trim(),
            Amount: $("#txt_amount").val().trim(),
            BillTypeId: parseInt($("#ddlTypeBill").val())
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        //$(".confirm_nodal_frwd_claim").hide();
        //$(".frwd_btn_no").hide();
        var url = '/Home/ajax_Update_BillSubmissionForm';
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
                        text: 'Your file has been Updated.'
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