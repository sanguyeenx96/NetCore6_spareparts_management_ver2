$("#btnXacNhanCreateLinhkien").click(function () {
    var modelValue = $("select[name='Model']").val();
    var tenjigValue = $("#Tenjig").val();
    var majigValue = $("#Majig").val();
    var tenlinhkienValue = $("#Tenlinhkien").val();
    var malinhkienValue = $("#Malinhkien").val();
    var makerValue = $("#Maker").val();
    var donviValue = $("#Donvi").val();
    var dongiaValue = $("#Dongia").val();
    var tonkhoValue = $("#Tonkho").val();
    var ghichuValue = $("#Ghichu").val();
    var imageValue = $("#image").val();
    var data = {
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
        Image: imageValue,
    };
    $.ajax({
        url: "/Danhsachlinhkien/Create/",
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
            Swal.fire("Lỗi máy chủ!", data.message, "error");
        },
    });
});

$("#btnHuyboCreateLinhkien").click(function () {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger",
        },
        buttonsStyling: true,
    });

    swalWithBootstrapButtons
        .fire({
            title: "Huỷ bỏ thao tác?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Xác nhận",
            cancelButtonText: "Huỷ bỏ",
            reverseButtons: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
                window.location.reload();
            }
        });
});
