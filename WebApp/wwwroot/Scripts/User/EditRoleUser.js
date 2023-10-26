$(".btnEditRole").click(async function () {
    var idStr = $(this).data("id");
    var id = parseInt(idStr);

    const inputOptions = new Promise((resolve) => {
        setTimeout(() => {
            resolve({
                false: "User",
                true: "Admin",
            });
        }, 1000);
    });

    const { value: role } = await Swal.fire({
        title: "Phân quyền cho tài khoản",
        input: "radio",
        inputOptions: inputOptions,
        inputValidator: (value) => {
            if (!value) {
                return "Bạn phải chọn phân quyền!";
            }
        },
    });

    if (role) {
        $.ajax({
            url: "/User/EditRole",
            type: "POST",
            data: {
                id: id,
                role: role,
            },
            success: function (data) {
                if (data.success) {
                    Swal.fire(
                        "Thành công!",
                        "Bạn vừa phân quyền tài khoản thành công",
                        "success"
                    ).then(function () {
                        window.location.reload();
                    });
                }
            },
        });
    }
});
