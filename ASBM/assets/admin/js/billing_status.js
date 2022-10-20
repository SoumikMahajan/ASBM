$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/MstBankDetails";
    });

    $(document).on('click', '#btn_submit', function () {

        var info = {
            docketNo: $("#docketNo").val().trim(),
            entryDate: $("#billing_date").val().trim()
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        //$(".confirm_nodal_frwd_claim").hide();
        //$(".frwd_btn_no").hide();
        var url = '/Home/ajax_check_billing_status';
        $.ajax({
            type: 'GET',
            url: url,
            dataType: 'json',
            data: info,
            success: function (data) {
                var check_result = data;
                if (check_result == 1) {
                    alert("Success");
                }
                else {
                    alert("Faild");
                }
            },
            beforeSend: function () {
                $('.modal_forward_claim_body').html('<div class="loader_con"><div class="loader"></div>Loading ...</div>');
            }
        });
    });

    $(document).on('click', '.confirm_nodal_frwd_claim', function (event) {
        //event.preventDefault();

        var info = {
            accNo: $("#txtAcNo").val().trim(),
            accName: $("#txtAcName").val().trim(),
            fundId: parseInt($("#ddlFund").val().trim()),
            bankName: $("#txtBankNm").val().trim(),
            ifsc: $("#txtIfsc").val().trim(),
        };

        //info = JSON.stringify(info)
        //var valdata = $("#bill_form").serialize();

        $(".confirm_nodal_frwd_claim").hide();
        $(".frwd_btn_no").hide();
        var url = '/Home/ajax_confirm_BankEntryForm';
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
                    $('.modal_forward_claim_body').html('<div class="alert alert-success">Payee Details Submitted Successfully</div>');
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