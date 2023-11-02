$(".dropdown-item.btnDeleteTTLK").on("click", function () {
    var id = $(this).data("id");
    var tenlinhkien = $(this).data("tenlinhkien");
    var model = $(this).data("model");

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger",
        },
        buttonsStyling: true,
    });

    swalWithBootstrapButtons
        .fire({
            title: "Xoá dữ liệu linh liện?",
            text: "Dữ liệu và lịch sử liên quan tới " + tenlinhkien + " sẽ bị xoá",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Xác nhận xoá!",
            cancelButtonText: "Huỷ bỏ!",
            reverseButtons: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Danhsachlinhkien/Delete",
                    type: "POST",
                    data: {
                        id: id,
                        tenlinhkien: tenlinhkien,
                        model:model
                    },
                    success: function (data) {
                        if (data.success) {
                            Swal.fire(
                                "Thành công!",
                                "Linh kiện đã được xoá",
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
