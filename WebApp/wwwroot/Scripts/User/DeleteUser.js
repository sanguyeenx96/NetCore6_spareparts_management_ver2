$(".btnDeleteUser").click(function () {
    var idStr = $(this).data("id");
    var usn = $(this).data("usn");
    var hoten = $(this).data("hoten");

    var id = parseInt(idStr);
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger",
        },
        buttonsStyling: true,
    });
    swalWithBootstrapButtons
        .fire({
            title: "Xoá tài khoản ?",
            text: "Dữ liệu tài khoản sẽ bị xoá vĩnh viễn",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Xác nhận xoá",
            cancelButtonText: "Huỷ bỏ",
            reverseButtons: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/User/Delete",
                    type: "POST",
                    data: {
                        id: id,
                        usn: usn,
                        hoten:hoten
                    },
                    success: function (data) {
                        if (!data.success) {
                            Swal.fire("Thất bại!", "Lỗi kết nối", "error");
                        } else {
                            Swal.fire(
                                "Thành công!",
                                "Bạn vừa xoá tài khoản thành công",
                                "success"
                            ).then(function () {
                                window.location.reload();
                            });
                        }
                    },
                    error: function () {
                        Swal.fire({
                            icon: "error",
                            title: "Oops...",
                            text: "Lỗi máy chủ",
                        });
                    },
                });
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                swalWithBootstrapButtons.fire(
                    "Đã huỷ",
                    "Thao tác đã được huỷ bỏ!",
                    "error"
                );
            }
        });
});
