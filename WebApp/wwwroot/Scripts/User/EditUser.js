$(".btnEditUser").click(function () {
    var usn = $(this).data("usn");
    var hoten = $(this).data("hoten");
    var pwd = $(this).data("pwd");
    var idStr = $(this).data("id");
    var id = parseInt(idStr);

    Swal.fire({
        title: "Sửa thông tin tài khoản",
        html: `
            <input type="text" id="hoten" class="swal2-input" value="${hoten}" autocomplete="off" >
            <input type="text" id="usn" class="swal2-input" value="${usn}" autocomplete="off" >
            <input type="text" id="pwd" class="swal2-input" value="${pwd}" autocomplete="off" >
            `,
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
            url: "/User/Update",
            type: "POST",
            data: {
                id: id,
                Name: hoten,
                UserName: usn,
                Password: pwd,
            },
            success: function (data) {
                if (data.success) {
                    Swal.fire(
                        "Thành công!",
                        "Bạn vừa sửa tài khoản thành công",
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
