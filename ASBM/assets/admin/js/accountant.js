$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Accountant/Index";
    });

    $("#VouterDetailsShowHide").hide();
    $("#block_payment_calculation").hide();
    $("#BankContainer").hide();
    $("#TreasuryContainer").hide();


    $(document).on("click", "#BtnView", function () {
        //debugger;
        var id = parseInt($(this).data("id"));
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/Get_Vouter_Details/?id=" + id,
            success: function (data) {
                $("#txtVouterNo").val(data.accountant.voucher_no);
                $("#txtVouterDate").val(data.accountant.entry_time);
                $("#txtComapanyCategory").val(data.accountant.bill_category_id_fk);
                //$("#txtWorkType").val(data.accountant.voucher_no);
                $("#txtComapanyName").val(data.accountant.bill_company_name);
                $("#txtDeptName").val(data.accountant.department_name);
                $("#txtProprietorName").val(data.accountant.payee_name);
                $("#txtFundName").val(data.accountant.fund_scheme_name);
                $("#txtMobile").val(data.accountant.mobile_no);
                $("#txtGst").val(data.accountant.bill_gst);
                $("#txtDescWork").val(data.accountant.bill_description);

                $("#VouterDetailsShowHide").show(300);
                $("#block_payment_calculation").hide();
                //alert(data.accountant.voucher_no);
            }
        });
    });

    $('input[name=gstradio]').change(function () {
        debugger;
        var value = $('input[name=gstradio]:checked').val();

        if (value == 'igst') {
            $("#txtSgst").attr("disabled", "disabled");
            $("#txtIgst").removeAttr("disabled");
            $("#txtSgst").val("");
            $("#txtCgst").val("");
            CalculateGrossAmount();
        }
        else if (value == 'sgst') {
            $("#txtIgst").attr("disabled", "disabled");
            $("#txtSgst").removeAttr("disabled");
            $("#txtIgst").val("");
            CalculateGrossAmount();
        }
    });

    $("#txtBasicBillAmount").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtSgst").keyup(function () {
        $("#txtCgst").val($("#txtSgst").val());
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtIgst").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtBasicCess").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    function CalculateGrossAmount() {
        //debugger;
        var gstVal;
        var radioValue = $('input[name=gstradio]:checked').val();

        if (radioValue == 'igst') {
            gstVal = parseFloat($("#txtIgst").val());
        }
        else {
            gstVal = parseFloat($("#txtSgst").val());
            gstVal = gstVal + gstVal;
        }

        if (isNaN(gstVal)) {
            gstVal = 0;
        }

        var basicBill = parseFloat($("#txtBasicBillAmount").val());
        if (isNaN(basicBill)) {
            basicBill = 0;
        }
        var basicCess = parseFloat($("#txtBasicCess").val());
        if (isNaN(basicCess)) {
            basicCess = 0;
        }

        var total = (basicBill + (basicBill / 100) * gstVal + (basicBill / 100) * basicCess).toFixed(2);
        $("#txtGrossAmount").val(total);
    }



    $("#txtItTds").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtSdMoney").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtGrossCess").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtTdsCgst").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtTdsSgst").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtPf").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtPfAdvance").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtPtax").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtCcsCount").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtCcsLic").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtCcsLoan").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtCoop").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtGi").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtLic").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    $("#txtFestival").keyup(function () {
        CalculateGrossAmount();
        CalculateNetAmount();
    });

    function CalculateNetAmount() {
        //debugger;

        var ItTds = parseFloat($("#txtItTds").val());
        if (isNaN(ItTds)) {
            ItTds = 0;
        }

        var SdMoney = parseFloat($("#txtSdMoney").val());
        if (isNaN(SdMoney)) {
            SdMoney = 0;
        }

        var GrossCess = parseFloat($("#txtGrossCess").val());
        if (isNaN(GrossCess)) {
            GrossCess = 0;
        }

        var TdsCgst = parseFloat($("#txtTdsCgst").val());
        if (isNaN(TdsCgst)) {
            TdsCgst = 0;
        }

        var TdsSgst = parseFloat($("#txtTdsSgst").val());
        if (isNaN(TdsSgst)) {
            TdsSgst = 0;
        }

        var Pf = parseFloat($("#txtPf").val());
        if (isNaN(Pf)) {
            Pf = 0;
        }

        var PfAdvance = parseFloat($("#txtPfAdvance").val());
        if (isNaN(PfAdvance)) {
            PfAdvance = 0;
        }

        var Ptax = parseFloat($("#txtPtax").val());
        if (isNaN(Ptax)) {
            Ptax = 0;
        }

        var CcsCount = parseFloat($("#txtCcsCount").val());
        if (isNaN(CcsCount)) {
            CcsCount = 0;
        }

        var CcsLic = parseFloat($("#txtCcsLic").val());
        if (isNaN(CcsLic)) {
            CcsLic = 0;
        }

        var CcsLoan = parseFloat($("#txtCcsLoan").val());
        if (isNaN(CcsLoan)) {
            CcsLoan = 0;
        }

        var Coop = parseFloat($("#txtCoop").val());
        if (isNaN(Coop)) {
            Coop = 0;
        }

        var Gi = parseFloat($("#txtGi").val());
        if (isNaN(Gi)) {
            Gi = 0;
        }

        var Lic = parseFloat($("#txtLic").val());
        if (isNaN(Lic)) {
            Lic = 0;
        }

        var Festival = parseFloat($("#txtFestival").val());
        if (isNaN(Festival)) {
            Festival = 0;
        }

        var totalDeductionAmount = ((ItTds / 100) + SdMoney + GrossCess + (TdsCgst / 100) + (TdsSgst / 100) + Pf + PfAdvance + Ptax + CcsCount + CcsLic + CcsLoan + Coop + Gi + Lic + Festival).toFixed(2);
        $("#txtTotalDeduction").val(totalDeductionAmount);

        var grossAmount = parseFloat($("#txtGrossAmount").val());
        if (isNaN(grossAmount)) {
            grossAmount = 0;
        }
        var netAmount = (grossAmount - totalDeductionAmount).toFixed(2);
        $("#txtNetAmountBill").val(netAmount);
    }

    $(document).on("click", "#btn_proceed_payment", function () {
        $("#block_payment_calculation").show(300);
    });

    $('input[name=optradio]').change(function () {
        debugger;
        var value = parseInt($('input[name=optradio]:checked').val());

        if (value == 1) {
            $("#BankContainer").show(300);
            $("#TreasuryContainer").hide();
        }
        else if (value == 2) {
            $("#BankContainer").hide();
            $("#TreasuryContainer").show(300);
        }
    });

    fetchVouter();

    function fetchVouter() {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/Get_All_Vouter_Details",
            success: function (data) {
                $("#prtCmsDataTable").html(data);
            }
        });
        return html;
    }

    fetchBankName();

    function fetchBankName() {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/GetAllBank",
            success: function (data) {
                $("#ddlBank").html(data);
            }
        });
        //return data;
    }

    $(document).on("change", "#ddlBank", function () {
        //debugger;
        var bankId = parseInt($("#ddlBank").val());
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/Get_bank_acc_Details/?bankId=" + bankId,
            success: function (data) {
                $("#txtBankAccNo").val(data.accountant.bank_account_no);
                $("#txtFundScheme").val(data.accountant.fund_scheme_name); 
                $("#txtFundSchemeId").val(data.accountant.fund_scheme_id_pk); 
            }
        });
    });

    fetchSchemeName();

    function fetchSchemeName() {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "text",
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/GetAllSchemeName",
            success: function (data) {
                $("#ddlScheme").html(data);
            }
        });
    }

    $(document).on("change", "#ddlScheme", function () {
        //debugger;
        var SchemeId = parseInt($("#ddlScheme").val());
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/Get_Treasury_Details/?SchemeId=" + SchemeId,
            success: function (data) {
                $("#txtAdviceNo").val(data.accountant.treasury_advice_no);
                $("#txtAdviceDate").val(data.accountant.treasury_advice_date);
            }
        });
    });


    $(document).on('click', '#btn_final_submit', function () {

        //debugger;
        /*DocketNo = $("#ddlDocketNo").val().trim();*/        

        //if (DocketNo == 0) {
        //    Swal.fire({
        //        icon: 'warning',
        //        text: 'Oops...!Please Select Docket No!',
        //    })
        //    return;
        //}

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
        debugger;

        var BasicBill = parseFloat($("#txtBasicBillAmount").val());
        if (isNaN(BasicBill)) {
            BasicBill = 0;
        }

        var SgstVal = parseFloat($("#txtSgst").val());
        if (isNaN(SgstVal)) {
            SgstVal = 0;
        }

        var CgstVal = parseFloat($("#txtCgst").val());
        if (isNaN(CgstVal)) {
            CgstVal = 0;
        }

        var Igst = parseFloat($("#txtIgst").val());
        if (isNaN(Igst)) {
            Igst = 0;
        }

        var BasicCess = parseFloat($("#txtBasicCess").val());
        if (isNaN(BasicCess)) {
            BasicCess = 0;
        }

        var GrossAmount = parseFloat($("#txtGrossAmount").val());
        if (isNaN(GrossAmount)) {
            GrossAmount = 0;
        }

        var ItTds = parseFloat($("#txtItTds").val());
        if (isNaN(ItTds)) {
            ItTds = 0;
        }

        var SdMoney = parseFloat($("#txtSdMoney").val());
        if (isNaN(SdMoney)) {
            SdMoney = 0;
        }

        var GrossCess = parseFloat($("#txtGrossCess").val());
        if (isNaN(GrossCess)) {
            GrossCess = 0;
        }

        var TdsCgst = parseFloat($("#txtTdsCgst").val());
        if (isNaN(TdsCgst)) {
            TdsCgst = 0;
        }

        var TdsSgst = parseFloat($("#txtTdsSgst").val());
        if (isNaN(TdsSgst)) {
            TdsSgst = 0;
        }

        var Pf = parseFloat($("#txtPf").val());
        if (isNaN(Pf)) {
            Pf = 0;
        }

        var PfAdvance = parseFloat($("#txtPfAdvance").val());
        if (isNaN(PfAdvance)) {
            PfAdvance = 0;
        }

        var Ptax = parseFloat($("#txtPtax").val());
        if (isNaN(Ptax)) {
            Ptax = 0;
        }

        var CcsCount = parseFloat($("#txtCcsCount").val());
        if (isNaN(CcsCount)) {
            CcsCount = 0;
        }

        var CcsLic = parseFloat($("#txtCcsLic").val());
        if (isNaN(CcsLic)) {
            CcsLic = 0;
        }

        var CcsLoan = parseFloat($("#txtCcsLoan").val());
        if (isNaN(CcsLoan)) {
            CcsLoan = 0;
        }

        var Coop = parseFloat($("#txtCoop").val());
        if (isNaN(Coop)) {
            Coop = 0;
        }

        var Gi = parseFloat($("#txtGi").val());
        if (isNaN(Gi)) {
            Gi = 0;
        }

        var Lic = parseFloat($("#txtLic").val());
        if (isNaN(Lic)) {
            Lic = 0;
        }

        var Festival = parseFloat($("#txtFestival").val());
        if (isNaN(Festival)) {
            Festival = 0;
        }

        var TotalDeduction = parseFloat($("#txtTotalDeduction").val());
        if (isNaN(TotalDeduction)) {
            TotalDeduction = 0;
        }

        var NetAmountBill = parseFloat($("#txtNetAmountBill").val());
        if (isNaN(NetAmountBill)) {
            NetAmountBill = 0;
        }

        var FundSchemeId = parseFloat($("#txtFundSchemeId").val());
        if (isNaN(FundSchemeId)) {
            FundSchemeId = 0;
        }

        var info = {
            VoucherNo: $("#txtVouterNo").val().trim(),
            BasicBill: BasicBill,
            SgstVal: SgstVal,
            CgstVal: CgstVal,
            Igst: Igst,
            BasicCess: BasicCess,
            GrossAmount: GrossAmount,
            ItTds: ItTds,
            SdMoney: SdMoney,
            GrossCess: GrossCess,
            TdsCgst: TdsCgst,
            TdsSgst: TdsSgst,
            Pf: Pf,
            PfAdvance: PfAdvance,
            Ptax: Ptax,
            CcsCount: CcsCount,
            CcsLic: CcsLic,
            CcsLoan: CcsLoan, 
            Coop: Coop,
            Gi: Gi,
            Lic: Lic,
            Festival: Festival,
            TotalDeduction: TotalDeduction,
            NetAmountBill: NetAmountBill,

            PaymentTypeId: $('input[name=optradio]:checked').val(),
            BankId: parseInt($("#ddlBank").val()),
            BankAccNo: $("#txtBankAccNo").val(),
            FundSchemeId: FundSchemeId,

            TreasurySchemeId: parseInt($("#ddlScheme").val()),
            TreasuryAdviceNo: $("#txtAdviceNo").val().trim(),
            TreasuryAdviceDate: $("#txtAdviceDate").val().trim()            
        };

        var url = '/Accountant/ajax_Finilize_Payment';
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
                        text: 'Payment Details has been Submitted Successfully.'
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