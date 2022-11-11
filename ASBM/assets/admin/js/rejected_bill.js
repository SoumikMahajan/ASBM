$(document).ready(function () {

    $(document).on('click', '.btn_auto_redirect', function () {
        window.location.href = "/Home/RejectedBill";
    });

    $(document).on('click', '#BtnReissue', function () {

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
                submitReissue();
                //window.location.href = "/Home/RejectedBill";
            }
        })
    });

    function submitReissue() {
        debugger;

        var voucherNo = $(this).data("id");
        alert(voucherNo);

        var info = {
            VoucherNo: voucherNo
        };

        var url = '/Home/ajax_Reissue_voucher';
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