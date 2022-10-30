$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Finance/Index";
    });

    $("#VouterDetailsShowHide").hide();

    $(document).on("click", "#BtnView", function () {
        //debugger;
        var voucherNo = $(this).data("id");
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Finance/Get_Vouter_Details/?voucherNo=" + voucherNo,
            success: function (data) {
                $(".netAmount").html(data.finance.total_net_amount);
                $("#txtVoucherNo").val(data.finance.payment_voucher_no)

                if (data.finance.payment_type_id == 1) {
                    var bankId = data.finance.bank_id_fk;
                    fetchBankName(bankId);
                }
                else {
                    var SchemeId = data.finance.scheme_id_pk;
                    fetchTreasury(SchemeId);
                }
                $("#VouterDetailsShowHide").show(300);
            }
        });
    });

    function fetchBankName(bankId) {
        //debugger;
        var html = "";
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Finance/Get_bank_acc_Details/?bankId=" + bankId,
            success: function (data) {
                $("#txt1").html(data.finance.bank_name);
                $("#txt2").html(data.finance.bank_account_no);
                $("#txt3").html(data.finance.fund_scheme_name);
                $("#txt4").val(data.finance.fund_scheme_id_pk);
            }
        });
    }

    function fetchTreasury(SchemeId) {
        //debugger;
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Finance/Get_Treasury_Details/?SchemeId=" + SchemeId,
            success: function (data) {
                $("#txt1").html(data.finance.scheme_name);
                $("#txt2").html(data.finance.treasury_advice_no);
                $("#txt3").html(data.finance.treasury_advice_date);
                $("#txt4").val(data.finance.scheme_id_pk);
            }
        });
    }

 
    $(document).on('click', '#btn_final_submit', function () {
        //debugger;
        PaymentMode = $("#ddlPaymentMode").val().trim();
        if (PaymentMode == '') {
            Swal.fire({
                icon: 'warning',
                text: 'Oops...!Please Select Payment Mode!',
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
        debugger;

        var info = {
            PaymentMode: $("#ddlPaymentMode").val().trim(),
            MemoNo: $("#txtMemo").val().trim(),
            NetAmount: parseFloat($("#netAmount").text()),
            VoucherNo: $("#txtVoucherNo").val()
            
            //PaymentTypeId: $('input[name=optradio]:checked').val(),
            //BankId: parseInt($("#ddlBank").val()),
            //BankAccNo: $("#txtBankAccNo").val(),
            //FundSchemeId: FundSchemeId,

            //TreasurySchemeId: parseInt($("#ddlScheme").val()),
            //TreasuryAdviceNo: $("#txtAdviceNo").val().trim(),
            //TreasuryAdviceDate: $("#txtAdviceDate").val().trim()
        };

        var url = '/Finance/ajax_Finilize_Payment';
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