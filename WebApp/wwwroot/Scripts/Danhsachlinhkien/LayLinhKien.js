function formatDate(dateString) {
    const options = {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
    };
    return new Date(dateString).toLocaleDateString("vi-VN", options);
}

$(".btnLayLinhKien").on("click", function () {
    var id = $(this).data("id");
    var tenlinhkien = $(this).data("tenlinhkien");
    var malinhkien = $(this).data("malinhkien");
    var tenjig = $(this).data("tenjig");
    var majig = $(this).data("majig");
    var model = $(this).data("model");
    var tonkho = $(this).data("tonkho");
    var ghichu = $(this).data("ghichu");
    var hinhanh = $(this).data("hinhanh");
    var hoten = $(this).data("hoten");

    if (tonkho == 0) {
        Swal.fire("Linh kiện đang hết tồn kho!", "", "error");
    } else {
        var tableHtml = '<div class="col-12"> <div class="row">';

        tableHtml +=
            '<div class="col-12 d-flex justify-content-center align-items-center"  >';
        tableHtml += "<h4>XÁC NHẬN LẤY LINH KIỆN SỬ DỤNG</h4>";
        tableHtml += "</div>";
        tableHtml +=
            '<div class="col-12 d-flex justify-content-center align-items-center"  style="margin-bottom: 30px">';
        tableHtml +=
            '<a style="font-size:smaller">Người thao tác: <span style="font-size:medium" class="badge badge-warning">' +
            hoten +
            "</span></a>";
        tableHtml += "</div>";

        tableHtml +=
            '<div class="col-6 d-flex justify-content-center align-items-center">';
        if (hinhanh) {
            tableHtml += '<img src="' + hinhanh + '" class="img-fluid shadow">';
        } else {
            tableHtml +=
                '<span class="badge badge-secondary">Chưa có dữ liệu hình ảnh</span>';
        }
        tableHtml += "</div>";
        tableHtml +=
            '<div class="col-6 d-flex justify-content-center align-items-center">';
        tableHtml +=
            '<table class="table table-hover table-bordered" style="font-size: small;border-top: none !important">';
        tableHtml += "<tbody>";
        tableHtml += "<tr><td>Model</td><td>" + model + "</td></tr>";
        tableHtml +=
            "<tr><td>Tên linh kiện</td><td>" + tenlinhkien + "</td></tr>";
        tableHtml +=
            "<tr><td>Mã linh kiện</td><td>" + malinhkien + "</td></tr>";
        tableHtml += "<tr><td>Tên Jig</td><td>" + tenjig + "</td></tr>";
        tableHtml += "<tr><td>Mã Jig</td><td>" + majig + "</td></tr>";
        tableHtml += "<tr><td>Ghi chú</td><td>" + ghichu + "</td></tr>";
        tableHtml += "<tr><td>Tồn kho</td><td>" + tonkho + "</td></tr>";
        tableHtml += "</tbody></table>";
        tableHtml += "</div>";
        tableHtml += '<div class="col-12" style="margin-top:10px">';
        tableHtml +=
            '<input class="form-control  form-control-lg" type="number" id="soLuong" placeholder="Nhập số lượng lấy ra..." style="margin-top: 10px;">';
        tableHtml +=
            '<button class="btn btn-success btn-block" id="xacNhan" style="margin-top: 10px;"><i class="fa fa-check fa-fw"> </i> Xác nhận</button></div> </div>';
        Swal.fire({
            icon:'info',
            title: name,
            html: tableHtml, // Sử dụng biến HTML chứa bảng
            position: "top-start",
            showClass: {
                popup: `
                        animate__animated
                        animate__fadeInLeft
                        animate__faster
                    `,
            },
            hideClass: {
                popup: `
                        animate__animated
                        animate__fadeOutLeft
                        animate__faster
                    `,
            },
            grow: "column",
            width: 800,
            showConfirmButton: false,
            showCloseButton: false,
        });

        // Bắt đầu thêm xử lý cho nút xác nhận
        $("#xacNhan").on("click", function () {
            // Lấy giá trị từ input số lượng
            var soLuong = $("#soLuong").val();
            if (soLuong.length < 1) {
                Swal.fire(
                    "Không thành công",
                    "Bạn chưa nhập số lượng lấy ra",
                    "error"
                );
            } else {
                $.ajax({
                    url: "/Danhsachlinhkien/laylinhkien",
                    type: "POST",
                    data: {
                        id: id,
                        soluong: soLuong,
                    },
                    success: function (data) {
                        if (data.success) {
                            Swal.fire(
                                "Thành công!",
                                "Bạn vừa lấy linh kiện thành công",
                                "success"
                            ).then(function () {
                                window.location.reload();
                            });
                        } else {
                            Swal.fire(
                                "Không thành công!",
                                "Số lượng lấy ra không phù hợp",
                                "error"
                            );
                        }
                    },
                });
            }
        });
    }
});
