$(".btnXacNhanDatHang").click(function () {
    var id = $(this).data("id");
    var tenlinhkien = $(this).data("tenlinhkien");
    var malinhkien = $(this).data("malinhkien");
    var ngayyeucau = $(this).data("ngayyeucau");
    var tenjig = $(this).data("tenjig");
    var majig = $(this).data("majig");
    var model = $(this).data("model");
    var maker = $(this).data("maker");
    var donvi = $(this).data("donvi");
    var ghichu = $(this).data("ghichu");
    var dongia = $(this).data("tenlinhkien");
    var thanhtien = $(this).data("thanhtien");
    var soluongdathang = $(this).data("soluongdathang");
    var soluongtonkhokhidathang = $(this).data("soluongtonkhokhidathang");
    var nguoiyeucaudathang = $(this).data("nguoiyeucaudathang");
    var ngayhangvedot1 = $(this).data("ngayhangvedot1");
    var soluonghangvedot1 = $(this).data("soluonghangvedot1");
    var ngaydukienhangvedot2 = $(this).data("ngaydukienhangvedot2");
    var ngayhangvedot2 = $(this).data("ngayhangvedot2");
    var soluonghangvedot2 = $(this).data("soluonghangvedot2");
    var hoten = $(this).data("hoten");
    var hinhanh = $(this).data("hinhanh");
    var trangthai = $(this).data("trangthai");

    if (trangthai === "Yêu cầu đặt hàng") {
        var tableHtml = '<div class="col-12"> <div class="row">';
        tableHtml +=
            '<div class="col-12 d-flex justify-content-center align-items-center"  >';
        tableHtml += "<h4>XÁC NHẬN BẮT ĐẦU ĐẶT HÀNG</h4>";
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
        tableHtml +=
            '<tr><td>Ngày yêu cầu</td><td><span style="font-size:medium" class="badge badge-dark">' +
            ngayyeucau +
            "</td></tr>";
        tableHtml += "</tbody></table>";
        tableHtml += "</div>";
        tableHtml +=
            '<button class="btn btn-success btn-block" id="xacNhan" style="margin-top: 10px;"><i class="fa fa-check fa-fw"> </i> Xác nhận</button></div> </div>';
        Swal.fire({
            icon: "info",
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
            let datepicker;
            Swal.fire({
                title: "Chọn ngày dự kiến hàng về",
                input: "text",
                inputValue: new Date().toLocaleString(),
                stopKeydownPropagation: false,
                preConfirm: () => {
                    if (
                        datepicker.getDate() <
                        new Date(new Date().setHours(0, 0, 0, 0))
                    ) {
                        Swal.showValidationMessage(
                            `Ngày dự kiến không thể là quá khứ`
                        );
                    }
                    return datepicker.getDate();
                },
                didOpen: () => {
                    datepicker = new Pikaday({ field: Swal.getInput() });
                    setTimeout(() => datepicker.show(), 100); // show calendar after showing animation
                },
                didClose: () => {
                    datepicker.destroy();
                },
            }).then((result) => {
                const selectedDate = result.value;
                if (selectedDate) {
                    const formattedDate =
                        moment(selectedDate).format("YYYY/MM/DD");
                    console.log(formattedDate);
                    alert(formattedDate);
                }
            });
        });
    }
    if (trangthai === "Đang đặt hàng") {
        Swal.fire({
            icon: "question",
            text: "Tình trạng đơn hàng?",
            showDenyButton: true,
            confirmButtonText: "Hàng về đủ",
            denyButtonText: `Hàng về thiếu`,
        }).then((result) => {
            if (result.isConfirmed) {
            } else {
            }
        });
    }
    if (trangthai === "Hàng về thiếu") {
    }
});
