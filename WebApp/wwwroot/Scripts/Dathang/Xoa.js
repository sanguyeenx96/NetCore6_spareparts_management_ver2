$(".btnXoaDon").click(function () {
    var id = $(this).data("id");
    var tenlinhkien = $(this).data("tenlinhkien");
    var tenmodel = $(this).data("model");


    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger",
        },
        buttonsStyling: true,
    });

    swalWithBootstrapButtons
        .fire({
            title: "Xoá thông tin đơn đặt hàng?",
            text: tenlinhkien,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Xác nhận xoá!",
            cancelButtonText: "Huỷ bỏ!",
            reverseButtons: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Dathang/Delete",
                    type: "POST",
                    data: {
                        id: id,
                        tenlinhkien: tenlinhkien,
                        tenmodel:tenmodel
                    },
                    success: function (data) {
                        if (data.success) {
                            Swal.fire(
                                "Thành công!",
                                "Đơn đặt hàng đã được xoá",
                                "success"
                            ).then(function () {
                                window.location.reload();
                            });
                        } else {
                            Swal.fire(
                                "Không thành công!",
                                data.message,
                                "error"
                            );
                        }
                    },
                });
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                swalWithBootstrapButtons.fire(
                    "Đã huỷ bỏ",
                    "Thao tác đã được huỷ bỏ",
                    "error"
                );
            }
        });
});
