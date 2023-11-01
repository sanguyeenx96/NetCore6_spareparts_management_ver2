$(document).ready(function () {
    $("#btnThemModelMoi").click(function () {
        Swal.fire({
            title: "Thêm Model mới",
            html: `<input type="text" id="tenmodel" class="swal2-input" placeholder="Nhập tên Model">`,
            confirmButtonText: "Xác nhận",
            focusConfirm: false,
            preConfirm: () => {
                const ten = Swal.getPopup().querySelector("#tenmodel").value;
                if (!ten) {
                    Swal.showValidationMessage(`Phải nhập đầy đủ thông tin`);
                }
                return { ten: ten };
            },
        }).then((result) => {
            var newmodel = result.value.ten.trim();
            $.ajax({
                url: "/Model/CreateModel",
                type: "POST",
                data: {
                    Tenmodel: newmodel
                },
                success: function (data) {
                    if (data.success) {
                        Swal.fire(
                            "Thành công!",
                            "Bạn vừa tạo Model "+newmodel +" mới thành công",
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
