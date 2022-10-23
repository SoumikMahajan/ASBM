$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Accountant/Index";
    });
    $("#VouterDetailsShowHide").hide();

    $(document).on("click", "#BtnView", function () {
        debugger;
        var id = parseInt($(this).data("id"));
        $.ajax({
            type: "GET",
            async: false,
            dataType: "json",
            /*data: id,*/
            contentType: "application/json; charset=utf-8",
            url: "/Accountant/Get_Vouter_Details/?id="+id,
            success: function (data) {
                $("#txtVouterNo").val(data.accountantModel.voucher_no);
                $("#txtVouterDate").val(data.accountantModel.CreateDate);
                $("#txtComapanyCategory").val(data.accountantModel.bill_category_id_fk);
                //$("#txtWorkType").val(data.accountantModel.voucher_no);
                $("#txtComapanyName").val(data.accountantModel.bill_company_name);
                $("#txtDeptName").val(data.accountantModel.department_name);
                $("#txtProprietorName").val(data.accountantModel.payee_name);
                $("#txtFundName").val(data.accountantModel.fund_scheme_name);
                $("#txtMobile").val(data.accountantModel.mobile_no);
                $("#txtGst").val(data.accountantModel.bill_gst);
                $("#txtDescWork").val(data.accountantModel.bill_description);

                $("#VouterDetailsShowHide").show(300);
                //alert(data.accountantModel.voucher_no);
            }
        });
        
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

});