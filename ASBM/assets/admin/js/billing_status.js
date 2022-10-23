$(document).ready(function () {

    //$(document).on('click', '.btn_auto_redirect', function () {
    //    window.location.href = "/Home/MstBankDetails";
    //});

    $(document).on('click', '#btn_submit', function () {
        debugger;
        var info = {
            docketNo: $("#docketNo").val().trim(),
            entryDate: $("#billing_date").val().trim()
        };

        var url = '/Home/ajax_check_billing_status';
        $.ajax({
            type: 'GET',
            async: false,
            url: url,           
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            data: info,
            success: function (data) {
                //$('#billingDataTable').html(data);
                if (data != "") {
                    $("#billingDataTable").html(data);                 
                }
                //var html = '';
                //var i = 1;

                //$.each(data, function (key, item) {
                //    var date = new Date(parseInt(item.bill_allotement_date.substr(6)));

                //    html += '<tr>';
                //    html += '<td>' + i + '</td>';
                //    html += '<td>' + item.bill_allotement_docket_no + '</td>';
                //    html += '<td>' + date + '</td>';
                //    html += '</tr>';
                //    i++;
                //});
                //$('.tbody').html(html);
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