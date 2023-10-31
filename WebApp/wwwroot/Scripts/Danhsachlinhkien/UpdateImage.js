$(document).ready(function () {
    $("#btnXacNhanUpdateImage").on("click", function () {
        var id = $(this).data("id");
        var image = $("#image").val();
        console.log(image);
        $.ajax({
            url: "/Danhsachlinhkien/UpdateImage/",
            type: "POST",
            data: {
                id: id,
                image: image,
            },
            success: function (data) {
                if (data.success) {
                    Swal.fire(
                        "Thành công!",
                        "Bạn vừa sửa thông tin linh kiện thành công",
                        "success"
                    ).then(function () {
                        window.location.reload();
                    });
                } else {
                    Swal.fire("Không thành công!", data.message, "error");
                }
            },
            error: function () {
                Swal.fire("Không thành công!", "Lỗi máy chủ", "error");
            },
        });
    });
});
