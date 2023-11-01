$(document).ready(function () {
    $(".btnDeleteModel").click(function () {
        var id = $(this).data("id");
        var tenmodel = $(this).data("tenmodel");

        Swal.fire({
            title: "Nhập mật khẩu để tiếp tục",
            html: `<input type="password" id="pwd" class="swal2-input" placeholder="Nhập mật khẩu...">`,
            confirmButtonText: "Tiếp tục",
            focusConfirm: false,
            preConfirm: () => {
                const pwd = Swal.getPopup().querySelector("#pwd").value;
                if (!pwd) {
                    Swal.showValidationMessage(`Phải nhập mật khẩu`);
                }
                if (pwd !== "adminmfe") {
                    Swal.showValidationMessage(`Sai mật khẩu`);
                }
            },
        }).then((result) => {
            $.ajax({
                url: "/Model/Delete",
                type: "POST",
                data: {
                    id: id,
                },
                success: function (data) {
                    if (data.success) {
                        Swal.fire(
                            "Thành công!",
                            "Bạn vừa xoá Model " + tenmodel +" thành công",
                            "success"
                        ).then(function () {
                            window.location.reload();
                        });
                    } else {
                        Swal.fire("Thất bại!", data.message, "error");
                    }
                },
                error: function (xhr, t, error) {
                    Swal.fire("Thất bại!", "Lỗi máy chủ", "error");
                },
            });
        });
    });
});
