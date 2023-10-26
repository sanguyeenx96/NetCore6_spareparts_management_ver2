$("#btnCreateUser").click(function () {
    Swal.fire({
        title: "Thêm tài khoản mới",
        html: `<input type="text" id="hoten" class="swal2-input" placeholder="Họ và tên">
                                 <input type="text" id="usn" class="swal2-input" placeholder="Tên đăng nhập">
                                 <input type="password" id="pwd" class="swal2-input" placeholder="Mật khẩu">`,
        confirmButtonText: "Xác nhận",
        focusConfirm: false,
        preConfirm: () => {
            const hoten = Swal.getPopup().querySelector("#hoten").value;
            const username = Swal.getPopup().querySelector("#usn").value;
            const password = Swal.getPopup().querySelector("#pwd").value;
            if (!username || !password || !hoten) {
                Swal.showValidationMessage(`Phải nhập đầy đủ thông tin`);
            }
            return { hoten: hoten, username: username, password: password };
        },
    }).then((result) => {
        var hoten = result.value.hoten.trim();
        var usn = result.value.username.trim();
        var pwd = result.value.password.trim();
        $.ajax({
            url: "/User/Create",
            type: "POST",
            data: {
                Name: hoten,
                UserName: usn,
                Password: pwd,
                ConfirmPassword: pwd,
            },
            success: function (data) {
                if (data.success) {
                    Swal.fire(
                        "Thành công!",
                        "Bạn vừa tạo tài khoản mới thành công",
                        "success"
                    ).then(function () {
                        window.location.reload();
                    });
                } else {
                    Swal.fire("Thất bại!", data.errors.join("<br>"), "error");
                }
            },
            error: function (xhr, t, error) {
                Swal.fire("Thất bại!", "Lỗi kết nối", "error");
            },
        });
    });
});
