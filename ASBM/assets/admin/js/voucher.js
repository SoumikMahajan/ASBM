$(document).ready(function () {

    showhideForVendorGenerator();
    function showhideForVendorGenerator() {
        document.getElementById('cicrandomRadio2').checked = true;
        document.getElementById('eTenRadioOption2').checked = true;
        document.getElementById('medRadio2').checked = true;
        $("#cicnoHide").hide();
        $("#tennoHide").hide();
        $("#mednoHide").hide();
        $("#billDetailsShowHide").hide();
        $("#billDetailsShowHideForRandoms").hide();
        $("#billBody").hide();
    }
    $(document).on("click", "#cicrandomRadio1", function () {
        $("#cicnoHide").show(300);
        document.getElementById('cicrandomRadio2').checked = false;
    });
    $(document).on("click", "#cicrandomRadio2", function () {
        $("#cicnoHide").hide();
        document.getElementById('cicrandomRadio1').checked = false;
    });

    $(document).on("click", "#eTenRadioOption1", function () {
        $("#tennoHide").show(300);
        document.getElementById('eTenRadioOption2').checked = false;
    });
    $(document).on("click", "#eTenRadioOption2", function () {
        $("#tennoHide").hide();
        document.getElementById('eTenRadioOption1').checked = false;
    });
    $(document).on("click", "#medRadio1", function () {
        $("#mednoHide").show(300);
        document.getElementById('medRadio2').checked = false;
    });
    $(document).on("click", "#medRadio2", function () {
        $("#mednoHide").hide();
        document.getElementById('medRadio1').checked = false;
    });

    //$(document).on("click", "#btn_approved", function () {

    //});

    $('input[name=optradio]').change(function () {
        debugger;
        var value = parseInt($('input[name=optradio]:checked').val());
        fetchDocketNo(value);
    });

    function fetchDocketNo(value) {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/VoucherGenerator/GetAllDocketNo/?radioVal=" + value,
            success: function (data) {
                html = data;
                $("#ddlDocketNo").html(html);
            }
        });
        return html;
    }

    $(document).on('click', '#btn_proceed', function () {
        debugger;
        var BillDocketType = $("input:radio[name=optradio]:checked").val();
        var billdocketno = $("#ddlDocketNo").val();

        if (BillDocketType == null) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Choose Docket Type!',
            })
            return;
        }

        if (billdocketno == 0) {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Docket No!',
            })
            return;
        }

        $("#billBody").show(300);
    });

    $(document).on('click', '#btn_approved', function () {
        debugger;
        var BillDocketType = $("input:radio[name=optradio]:checked").val();
        var billdocketno = $("#ddlDocketNo option:selected").text();
        var iscic = $("input:radio[name=cicradio]:checked").val();
        var cicno = $("#cic_input_no").val().trim();
        var isetender = $("input:radio[name=tenradio]:checked").val();
        var tenno = $("#ten_input_no").val().trim();
        var ismed = $("input:radio[name=medradio]:checked").val();
        var medno = $("#med_input_no").val().trim();
        var MeetingTenderCommittee = $("#txtMeetingTendeCommittee").val();
        var MeetingChairman = $("#txtMeetingChairman").val();
        var Tenderrate = $("#txtAcceptanceTender").val().trim();
        var MeetingBOC = $("#txtMeetingBOC").val();
        var AAFSNo = $("#txtAAFSNo").val().trim();
        var WorkOrder = $("#txtWorkOrder").val().trim();
        var WorkOrderDate = $("#txtWorkOrderDate").val();
        var AmountEstimate = $("#txtAmountEstimate").val().trim();
        var MBBookNo = $("#txtMBBookNo").val().trim();
        var PageNo = $("#txtPageNo").val().trim();
        var WorkRegisterNo = $("#txtWorkRegisterNo").val().trim();
        var WorkRegisterDate = $("#txtWorkRegisterDate").val();

        if (BillDocketType == '1') {
            showNormalUserBillDetails();
            $("#billDetailsShowHide").show(300);
            $("#billDetailsShowHideForRandoms").hide();
        }
        if (BillDocketType == '2') {
            showRandomUserBillDetails();
            $("#billDetailsShowHideForRandoms").show(300);
            $("#billDetailsShowHide").hide();
        }

    });

    function showNormalUserBillDetails() {
        //debugger;
        var info = {
            billdocketno: $("#ddlDocketNo option:selected").text()
        };

        var url = '/VoucherGenerator/ajax_getbilldeatilsby_DocketNo';
        $.ajax({
            type: 'GET',
            async: false,
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: info,
            success: function (data) {
                if (data != "") {
                    //$("#afterVoucherApproved").html(data);
                    $('#txtCompanycat').val(data.bill_category_id_fk);
                    $('#txtCompanyname').val(data.bill_company_name);
                    $('#txtDepartment').val(data.department_name);
                    $('#txtpan').val(data.bill_pan);
                    $('#txtgst').val(data.bill_gst);
                    $('#txtdescription').val(data.bill_description);
                    $('#txtbillamount').val(data.bill_amount);
                    $('#txtFund').val(data.fund_scheme_name);
                }
            }
        });
    }

    function showRandomUserBillDetails() {
        var info = {
            billdocketno: $("#ddlDocketNo option:selected").text()
        };

        var url = '/VoucherGenerator/ajax_getbilldetailsForRandoms';
        $.ajax({
            type: 'GET',
            async: false,
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: info,
            success: function (data) {
                if (data != "") {
                    $('#txtRandomBillName').val(data.random_bill_name);
                    $('#txtDepartmentName').val(data.department_name);
                    $('#txtFundName').val(data.fund_scheme_name);
                    $('#txtMobileNumber').val(data.random_bill_mobile_no);
                    $('#txtWorkDescription').val(data.random_bill_work_desc);
                }
            }
        });
    }

    $(document).on('click', '#btn_submit', function () {
        debugger;
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
                ApprovedVoucher();
                $("#bill_form").trigger('reset');
            }
        })
    });

    function ApprovedVoucher() {
        debugger;
        var info = {
            BillDocketType: $("input:radio[name=optradio]:checked").val(),
            billdocketno: $("#ddlDocketNo option:selected").text(),
            iscic: $("input:radio[name=cicradio]:checked").val(),
            cicno: $("#cic_input_no").val().trim(),
            isetender: $("input:radio[name=tenradio]:checked").val(),
            tenno: $("#ten_input_no").val().trim(),
            ismed: $("input:radio[name=medradio]:checked").val(),
            medno: $("#med_input_no").val().trim(),
            MeetingTenderCommittee: $("#txtMeetingTendeCommittee").val(),
            MeetingChairman: $("#txtMeetingChairman").val(),
            Tenderrate: $("#txtAcceptanceTender").val().trim(),
            MeetingBOC: $("#txtMeetingBOC").val(),
            AAFSNo: $("#txtAAFSNo").val().trim(),
            WorkOrder: $("#txtWorkOrder").val().trim(),
            WorkOrderDate: $("#txtWorkOrderDate").val(),
            AmountEstimate: $("#txtAmountEstimate").val().trim(),
            MBBookNo: $("#txtMBBookNo").val().trim(),
            PageNo: $("#txtPageNo").val().trim(),
            WorkRegisterNo: $("#txtWorkRegisterNo").val().trim(),
            WorkRegisterDate: $("#txtWorkRegisterDate").val()
        };

        var url = '/VoucherGenerator/ajax_approved_VoucherSubmission';
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: info,
            success: function (data) {
                var check_result = data;
                if (check_result == 1) {
                    swal.fire({
                        icon: 'success',
                        title: 'SAVED!',
                        text: 'Your data has been saved.'
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

    $(document).on('click', '#btn_submit_random', function () {
        debugger;
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
                ApprovedVoucher();
                $("#bill_form").trigger('reset');
            }
        })
    });

    $(document).on('click', '#btn_reject', function () {
        debugger;
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
                RejectVoucher();
                $("#bill_form").trigger('reset');
            }
        })
    });

    function RejectVoucher() {
        debugger;
        var info = {
            BillDocketType: parseInt($("input:radio[name=optradio]:checked").val()),
            billdocketno: $("#ddlDocketNo option:selected").text()            
        };

        var url = '/VoucherGenerator/ajax_reject_Voucher';
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            data: info,
            success: function (data) {
                var check_result = data;
                if (check_result == 1) {
                    swal.fire({
                        icon: 'success',
                        title: 'SAVED!',
                        text: 'Voucher has been rejected.'
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

});