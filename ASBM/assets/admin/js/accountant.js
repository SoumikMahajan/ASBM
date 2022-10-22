$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Accountant/Index";
    });
    $("#VouterDetailsShowHide").hide();

    $(document).on("click", "#BtnView", function () {
        $("#VouterDetailsShowHide").show(300);
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
                //html = data;
                //$("#ddlFund").html(html);
            }
        });
        return html;
    }

});