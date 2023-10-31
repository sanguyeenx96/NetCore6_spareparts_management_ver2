$(document).ready(function () {
    $("#btnXacNhanUpdateLinhKien").on("click", function () {
        var id = $(this).data("id");
        // Lấy giá trị từ các trường form
        var modelValue = $("select[name='Model']").val();
        var tonkhoValue = $("#Tonkho").val();
        var tenjigValue = $("#Tenjig").val();
        var majigValue = $("#Majig").val();
        var tenlinhkienValue = $("#Tenlinhkien").val();
        var malinhkienValue = $("#Malinhkien").val();
        var makerValue = $("#Maker").val();
        var donviValue = $("#Donvi").val();
        var dongiaValue = $("#Dongia").val();
        var ghichuValue = $("#Ghichu").val();

        // Đóng gói thành một đối tượng dữ liệu JSON
        var data = {
            Id: id,
            Model: modelValue,
            Tonkho: tonkhoValue,
            Tenjig: tenjigValue,
            Majig: majigValue,
            Tenlinhkien: tenlinhkienValue,
            Malinhkien: malinhkienValue,
            Maker: makerValue,
            Donvi: donviValue,
            Dongia: dongiaValue,
            Ghichu: ghichuValue,
        };

        // Gửi request AJAX
        $.ajax({
            url: "/Danhsachlinhkien/Update/",
            type: "POST",
            data: data, // Dữ liệu từ form
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
                Swal.fire("Không thành công!", data.message, "error");
            },
        });
    });
});
